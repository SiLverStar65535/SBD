using System.Threading.Tasks;
using SBD.Infrastructure.Interface;
using Intel.RealSense;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;



namespace SBD.Infrastructure.Service
{
    public class DimensionCameraService(IWMIService wmiService) : IDimensionCameraService
    {


        #region IDevice
        public string ID { get; } = Config.DimensionCameraID;
        public bool IsConnected() => GetDeviceInformation() != null;
        public object GetDeviceInformation()
        {
            var result = wmiService.QueryDevice(typeof(WMIQuery.SerialPortQuery), ID);
            return !string.IsNullOrEmpty(result.Key) ? result : null;
        }
        #endregion

        #region IDimensionCameraService
        public async Task<string> GetSize()
        {
            
            if (File.Exists(fileName))
                LoadParameters(fileName);//讀取
            GetImageFormCamera();
            await Task.Delay(3000);
            var size = calculateSize();

            return size;
        }
        public async Task<int?> GetWieght()
        {
            //await Task.Delay(1000);

            return await Task.FromResult(0); ;
        }
        #endregion


        #region Get image and process
        //--
        private bool isRunning = false;                 //攝影機檢查旗標
        private Pipeline pipeline;                      //攝影機連線設定
        private int CaptureTime = 1000;                 //攝影機啟動時間
        private BitmapSource CaptureImage;              //單張原始圖
        private BitmapSource CropedImage;               //裁切後的圖片
        private BitmapSource ProcessedImage;            //前處理的圖片
        private BitmapSource VolumeResImage;            //最終結果
        private double rectWidth, rectHeight;           //目標物的長寬
        private double rectWidthRatio = 0.14;           //寬度校正值
        private double rectHeightRatio = 0.14;          //長度校正值
        private string fileName = "Realsense.config";   //讀取路徑
        private ConfigParams configParams;              //設定檔的名稱
        //public event Action ImageCaptureCompleted;      //如果要檢查讀取的圖片可以使用

        /// <summary>
        /// 參數框架初始化
        /// </summary>
        private struct ConfigParams()
        {
            /// <summary>
            /// 框選的範圍,(p1,p2).(p3,p4)
            /// </summary>
            public string CropRect = "5,5,5,5";
            /// <summary>
            /// 膨脹處理次數
            /// </summary>
            public int Morphology_D = 0;
            /// <summary>
            /// 侵蝕數理參數
            /// </summary>
            public int Morphology_E = 0;
            /// <summary>
            /// 深度過濾值,遠離距離超過設定值(公分)的物體會被排除
            /// </summary>
            public int High = 255;
            /// <summary>
            /// 深度過濾值,接近距離超過設定值(公分)的物體會被排除
            /// </summary>
            public int Low = 0;
        }

        public void LoadParameters(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (line.StartsWith("[") || string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('=');
                if (parts.Length != 2)
                    continue;

                string key = parts[0].Trim();
                string value = parts[1].Trim();

                switch (key)
                {
                    case "CropRect":
                        configParams.CropRect = value;
                        break;
                    case "Morphology_D":
                        configParams.Morphology_D = int.Parse(value);
                        break;
                    case "Morphology_E":
                        configParams.Morphology_E = int.Parse(value);
                        break;
                    case "High":
                        configParams.High = int.Parse(value);
                        break;
                    case "Low":
                        configParams.Low = int.Parse(value);
                        break;
                    default:
                        break;
                }
            }
        }

        public void GetImageFormCamera()
        {
            if (isRunning)
            {
                return;
            }

            isRunning = true;
            pipeline = new Pipeline();

            // 非同步取得攝影機影像，一段時間後結束攝影機並釋放
            Task.Run(async () =>
            {
                try
                {
                    var RSconfig = new Intel.RealSense.Config();
                    RSconfig.EnableStream(Intel.RealSense.Stream.Depth, 640, 480, Format.Z16, 30);
                    pipeline.Start(RSconfig); // 開始連線

                    var stopTask = Task.Delay(CaptureTime).ContinueWith(_ => StopCamera()); // 設定自動結束

                    while (isRunning)
                    {
                        using (var frames = pipeline.WaitForFrames())
                        using (var depth = frames.DepthFrame)
                        {
                            if (depth != null)
                            {
                                var depthWidth = depth.Width;
                                var depthHeight = depth.Height;
                                var latestDepthData = new ushort[depthWidth * depthHeight];
                                depth.CopyTo(latestDepthData);

                                CaptureImage = ColorizeDepth(depth);
                                CaptureImage.Freeze();//固定source,避免釋放


                            }
                        }
                        await Task.Delay(33); //30PFS
                    }
                    //ImageCaptureCompleted?.Invoke();//如果UI要看，可以使用
                }
                catch (Exception ex)
                {
                    //錯誤訊息處理
                }
            });
        }
        public void StopCamera()
        {
            if (!isRunning) return;
            isRunning = false;

            // 確保在異步操作結束後才處置pipeline
            Task.Run(() =>
            {
                lock (this)
                {
                    if (pipeline != null)
                    {
                        pipeline.Stop();
                        pipeline.Dispose();
                        pipeline = null;

                    }
                }
            });


        }

        public string calculateSize()
        {
            if (CaptureImage != null)
            {
                var (x1, y1, w, h) = ParseCropRect(configParams.CropRect);//解析裁切區域rect的值

                CropedImage = CropImage(CaptureImage, x1, y1, w, h);//裁切目標區域

                var Depth = GetMinPixelValue(CropedImage, configParams.High, configParams.Low);//取的目標物件深度
                var DepthReverse = configParams.High - Depth;//根據深度逆算目標物件高度

                ProcessedImage = ImageThreshold(CropedImage, (int)configParams.Low, (int)configParams.High);//根據深度過濾目標物

                //去除雜訊，鎖定目標物
                for (int i = 0; i < configParams.Morphology_E; i++)
                    ProcessedImage = Erosion(ProcessedImage);//侵蝕處理
                for (int i = 0; i < configParams.Morphology_D; i++)
                    ProcessedImage = Dilation(ProcessedImage);//膨脹處理

                ProcessedImage = FindLargestWhiteObject(ProcessedImage);//最終結果，框選目標物

                VolumeResImage = DrawMinAreaRect(ProcessedImage, out rectWidth, out rectHeight);//返回物件框選的尺寸

                var ResText = $"{Math.Round(rectWidth * rectWidthRatio, 1)},{Math.Round(rectHeight * rectHeightRatio, 1)},{DepthReverse}";//顯示最終結果，注意w與h需校正。
                return ResText;
            }
            else
            {
                var ResText = "Error.";
                return ResText;
            }

        }

        /// <summary>
        /// 輔助讀取參數,將點的4個值切開。
        /// </summary>
        /// <param name="cropRect"></param>
        /// <returns></returns>
        private (int, int, int, int) ParseCropRect(string cropRect)
        {
            // 將字串以逗號分隔並轉換成整數陣列
            string[] parts = cropRect.Split(',');

            // 檢查是否有四個部分
            if (parts.Length != 4)
            {
                //throw new ArgumentException("CropRect must contain exactly four comma-separated values.");
            }

            // 轉換每個部分為整數
            int x1 = int.Parse(parts[0]);
            int y1 = int.Parse(parts[1]);
            int w = int.Parse(parts[2]);
            int h = int.Parse(parts[3]);

            return (x1, y1, w, h);
        }

        /// <summary>
        /// 將深度幀數據轉換為灰階影像
        /// </summary>
        /// <param name="depthFrame">深度幀數據</param>
        /// <returns>灰階影像的 BitmapSource</returns>
        private BitmapSource ColorizeDepth(DepthFrame depthFrame)
        {
            // 獲取深度幀的寬度和高度
            int width = depthFrame.Width;
            int height = depthFrame.Height;

            // 創建一個字節數組，用於存儲灰階化後的深度像素
            byte[] depthPixels = new byte[width * height];

            // 遍歷深度幀的每個像素
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // 獲取當前像素的深度值，並轉換
                    ushort depth = (ushort)(depthFrame.GetDistance(x, y) * 100);

                    // 將深度值轉換為8位的強度值，取模以保持在0-255範圍內
                    byte intensity = (byte)(depth % 256);

                    // 將強度值分配給灰階像素
                    depthPixels[y * width + x] = intensity;
                }
            }

            // 創建並返回灰階的BitmapSource
            BitmapSource grayBitmap = BitmapSource.Create(width, height, 96, 96, PixelFormats.Gray8, null, depthPixels, width);
            return grayBitmap;
        }

        /// <summary>
        /// 輸入起始位置與尺寸，裁切圖片
        /// </summary>
        /// <param name="source"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private BitmapSource CropImage(BitmapSource source, int x1, int y1, int width, int height)
        {
            // 創建剪裁位圖
            return new CroppedBitmap(source, new Int32Rect(x1, y1, width, height));
        }

        /// <summary>
        /// 根據low與high值來過濾圖像上的值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <returns></returns>
        private BitmapSource ImageThreshold(BitmapSource source, int low, int high)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int stride = width * ((source.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            source.CopyPixels(pixelData, stride, 0);

            for (int i = 0; i < pixelData.Length; i++)
            {
                byte intensity = pixelData[i];

                if (intensity < low || intensity > high)
                {
                    pixelData[i] = 0;
                }
            }

            return BitmapSource.Create(width, height, source.DpiX, source.DpiY, PixelFormats.Gray8, null, pixelData, stride);
        }

        /// <summary>
        /// 取得目標物最高值，此數值代表了物體與攝影機的距離，使用時須減去底板高度，再加上一個補償。
        /// </summary>
        /// <param name="bitmapSource"></param>
        /// <param name="high">請帶入config檔的high</param>
        /// <param name="low">請帶入config檔的low</param>
        /// <returns></returns>
        private int GetMinPixelValue(BitmapSource bitmapSource, int high, int low)
        {
            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            int stride = (width * bitmapSource.Format.BitsPerPixel + 7) / 8;
            byte[] pixels = new byte[height * stride];
            bitmapSource.CopyPixels(pixels, stride, 0);

            int minPixelValue = int.MaxValue;
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i] >= low && pixels[i] <= high && pixels[i] < minPixelValue)
                {
                    minPixelValue = pixels[i];
                }
            }

            return minPixelValue;
        }

        /// <summary>
        /// 侵蝕處理
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private BitmapSource Erosion(BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int stride = width * ((source.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            byte[] resultData = new byte[height * stride];
            source.CopyPixels(pixelData, stride, 0);

            int[,] structuringElement =
            {
               { 0, 1, 0 },
               { 1, 1, 1 },
               { 0, 1, 0 }
            };

            int elementSize = structuringElement.GetLength(0);
            int elementRadius = elementSize / 2;

            for (int y = elementRadius; y < height - elementRadius; y++)
            {
                for (int x = elementRadius; x < width - elementRadius; x++)
                {
                    bool erode = false;

                    for (int j = -elementRadius; j <= elementRadius; j++)
                    {
                        for (int i = -elementRadius; i <= elementRadius; i++)
                        {
                            if (structuringElement[j + elementRadius, i + elementRadius] == 1)
                            {
                                int index = (y + j) * stride + (x + i);
                                if (pixelData[index] == 0)
                                {
                                    erode = true;
                                    break;
                                }
                            }
                        }
                        if (erode) break;
                    }

                    int resultIndex = y * stride + x;
                    resultData[resultIndex] = erode ? (byte)0 : pixelData[resultIndex];
                }
            }

            BitmapSource erodedBitmap = BitmapSource.Create(width, height, source.DpiX, source.DpiY, source.Format, null, resultData, stride);
            return erodedBitmap;
        }

        /// <summary>
        /// 膨脹處理
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private BitmapSource Dilation(BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int stride = width * ((source.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            byte[] resultData = new byte[height * stride];
            source.CopyPixels(pixelData, stride, 0);

            int[,] structuringElement =
            {
                 { 0, 1, 0 },
                 { 1, 1, 1 },
                 { 0, 1, 0 }
            };

            int elementSize = structuringElement.GetLength(0);
            int elementRadius = elementSize / 2;

            for (int y = elementRadius; y < height - elementRadius; y++)
            {
                for (int x = elementRadius; x < width - elementRadius; x++)
                {
                    bool dilate = false;

                    for (int j = -elementRadius; j <= elementRadius; j++)
                    {
                        for (int i = -elementRadius; i <= elementRadius; i++)
                        {
                            if (structuringElement[j + elementRadius, i + elementRadius] == 1)
                            {
                                int index = (y + j) * stride + (x + i);
                                if (pixelData[index] > 0)
                                {
                                    dilate = true;
                                    break;
                                }
                            }
                        }
                        if (dilate) break;
                    }

                    int resultIndex = y * stride + x;
                    resultData[resultIndex] = dilate ? (byte)255 : pixelData[resultIndex];
                }
            }

            BitmapSource dilatedBitmap = BitmapSource.Create(width, height, source.DpiX, source.DpiY, source.Format, null, resultData, stride);
            return dilatedBitmap;
        }

        /// <summary>
        /// 找出圖片中最大體積的物件
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private BitmapSource FindLargestWhiteObject(BitmapSource source)
        {
            int width = source.PixelWidth;
            int height = source.PixelHeight;
            int stride = width * ((source.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[height * stride];
            source.CopyPixels(pixelData, stride, 0);

            // 連通性檢測，找到最大的白色物體
            int[,] labels = new int[height, width];
            int label = 0;
            Dictionary<int, int> labelSizes = new Dictionary<int, int>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * stride + x;
                    if (pixelData[index] == 255 && labels[y, x] == 0) // 255 表示白色
                    {
                        label++;
                        int size = FloodFill(pixelData, labels, x, y, width, height, label, stride);
                        labelSizes[label] = size;
                    }
                }
            }

            // 找到最大物體的標籤
            int largestLabel = labelSizes.OrderByDescending(ls => ls.Value).First().Key;

            // 創建結果影像並標記最大物體
            byte[] resultData = new byte[height * stride];
            Array.Copy(pixelData, resultData, pixelData.Length);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (labels[y, x] != largestLabel)
                    {
                        int index = y * stride + x;
                        resultData[index] = 0; // 將非最大物體的像素設為黑色
                    }
                }
            }

            BitmapSource resultBitmap = BitmapSource.Create(width, height, source.DpiX, source.DpiY, source.Format, null, resultData, stride);
            return resultBitmap;
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="pixelData"></param>
        /// <param name="labels"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="label"></param>
        /// <param name="stride"></param>
        /// <returns></returns>
        private static int FloodFill(byte[] pixelData, int[,] labels, int x, int y, int width, int height, int label, int stride)
        {
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((x, y));
            labels[y, x] = label;
            int size = 0;

            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                (int cx, int cy) = queue.Dequeue();
                size++;

                for (int i = 0; i < 4; i++)
                {
                    int nx = cx + dx[i];
                    int ny = cy + dy[i];

                    if (nx >= 0 && nx < width && ny >= 0 && ny < height && labels[ny, nx] == 0)
                    {
                        int index = ny * stride + nx;
                        if (pixelData[index] == 255)
                        {
                            labels[ny, nx] = label;
                            queue.Enqueue((nx, ny));
                        }
                    }
                }
            }

            return size;
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="source"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public BitmapSource DrawMinAreaRect(BitmapSource source, out double width, out double height)
        {
            int imgWidth = source.PixelWidth;
            int imgHeight = source.PixelHeight;
            int stride = imgWidth * ((source.Format.BitsPerPixel + 7) / 8);
            byte[] pixelData = new byte[imgHeight * stride];
            source.CopyPixels(pixelData, stride, 0);

            List<System.Windows.Point> points = new List<System.Windows.Point>();

            // 找到所有白色像素的坐標
            for (int y = 0; y < imgHeight; y++)
            {
                for (int x = 0; x < imgWidth; x++)
                {
                    int index = y * stride + x;
                    if (pixelData[index] == 255)
                    {
                        points.Add(new System.Windows.Point(x, y));
                    }
                }
            }

            if (points.Count == 0)
            {
                throw new InvalidOperationException("No white object found.");
            }

            // 計算最小外接矩形
            var minAreaRect = GetMinimumBoundingBox(points);

            // 將灰階影像轉換為彩色影像
            byte[] colorData = new byte[imgHeight * imgWidth * 3];
            for (int y = 0; y < imgHeight; y++)
            {
                for (int x = 0; x < imgWidth; x++)
                {
                    byte intensity = pixelData[y * imgWidth + x];
                    int colorIndex = (y * imgWidth + x) * 3;
                    colorData[colorIndex] = intensity; // R
                    colorData[colorIndex + 1] = intensity; // G
                    colorData[colorIndex + 2] = intensity; // B
                }
            }

            // 畫紅色外接矩形
            DrawRotatedRectangle(colorData, minAreaRect, imgWidth);

            BitmapSource colorBitmap = BitmapSource.Create(imgWidth, imgHeight, source.DpiX, source.DpiY, PixelFormats.Rgb24, null, colorData, imgWidth * 3);
            width = minAreaRect.Width;
            height = minAreaRect.Height;
            return colorBitmap;
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private (System.Windows.Point Center, double Width, double Height, double Angle, List<System.Windows.Point> Corners) GetMinimumBoundingBox(List<System.Windows.Point> points)
        {
            var hull = ConvexHull(points);

            double minArea = double.MaxValue;
            System.Windows.Point center = new System.Windows.Point();
            double width = 0;
            double height = 0;
            double angle = 0;
            List<System.Windows.Point> bestCorners = null;

            for (int i = 0; i < hull.Count; i++)
            {
                var p1 = hull[i];
                var p2 = hull[(i + 1) % hull.Count];
                double theta = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X);
                var rect = GetBoundingRectangle(hull, theta);

                double area = rect.Width * rect.Height;
                if (area < minArea)
                {
                    minArea = area;
                    center = rect.Center;
                    width = rect.Width;
                    height = rect.Height;
                    angle = theta;
                    bestCorners = rect.Corners;
                }
            }

            return (center, width, height, angle, bestCorners);
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="points"></param>
        /// <param name="theta"></param>
        /// <returns></returns>
        private (System.Windows.Point Center, double Width, double Height, List<System.Windows.Point> Corners) GetBoundingRectangle(List<System.Windows.Point> points, double theta)
        {
            double cosTheta = Math.Cos(theta);
            double sinTheta = Math.Sin(theta);

            double minX = double.MaxValue;
            double maxX = double.MinValue;
            double minY = double.MaxValue;
            double maxY = double.MinValue;

            foreach (var point in points)
            {
                double x = point.X * cosTheta + point.Y * sinTheta;
                double y = -point.X * sinTheta + point.Y * cosTheta;

                if (x < minX) minX = x;
                if (x > maxX) maxX = x;
                if (y < minY) minY = y;
                if (y > maxY) maxY = y;
            }

            double width = maxX - minX;
            double height = maxY - minY;

            List<System.Windows.Point> corners = new List<System.Windows.Point>
        {
            new System.Windows.Point(minX, minY),
            new System.Windows.Point(maxX, minY),
            new System.Windows.Point(maxX, maxY),
            new System.Windows.Point(minX, maxY)
        };

            for (int i = 0; i < corners.Count; i++)
            {
                double x = corners[i].X * cosTheta - corners[i].Y * sinTheta;
                double y = corners[i].X * sinTheta + corners[i].Y * cosTheta;
                corners[i] = new System.Windows.Point(x, y);
            }

            var center = new System.Windows.Point((corners[0].X + corners[2].X) / 2, (corners[0].Y + corners[2].Y) / 2);

            return (center, width, height, corners);
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private List<System.Windows.Point> ConvexHull(List<System.Windows.Point> points)
        {
            if (points.Count < 3)
                return points;

            List<System.Windows.Point> hull = new List<System.Windows.Point>();

            points = points.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
            System.Windows.Point leftMost = points.First();

            System.Windows.Point current = leftMost;
            System.Windows.Point next;

            do
            {
                hull.Add(current);
                next = points[0];

                for (int i = 1; i < points.Count; i++)
                {
                    if (next == current || IsCounterClockwise(current, next, points[i]))
                        next = points[i];
                }

                current = next;
            } while (current != leftMost);

            return hull;
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        private bool IsCounterClockwise(System.Windows.Point a, System.Windows.Point b, System.Windows.Point c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X) > 0;
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="colorData"></param>
        /// <param name="rect"></param>
        /// <param name="width"></param>
        private void DrawRotatedRectangle(byte[] colorData, (System.Windows.Point Center, double Width, double Height, double Angle, List<System.Windows.Point> Corners) rect, int width)
        {
            List<Tuple<System.Windows.Point, System.Windows.Point>> edges = new List<Tuple<System.Windows.Point, System.Windows.Point>>
        {
            Tuple.Create(rect.Corners[0], rect.Corners[1]),
            Tuple.Create(rect.Corners[1], rect.Corners[2]),
            Tuple.Create(rect.Corners[2], rect.Corners[3]),
            Tuple.Create(rect.Corners[3], rect.Corners[0])
        };

            foreach (var edge in edges)
            {
                DrawLine(colorData, edge.Item1, edge.Item2, width);
            }
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="colorData"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="imgWidth"></param>
        private void DrawLine(byte[] colorData, System.Windows.Point p1, System.Windows.Point p2, int imgWidth)
        {
            int x0 = (int)p1.X;
            int y0 = (int)p1.Y;
            int x1 = (int)p2.X;
            int y1 = (int)p2.Y;

            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                SetPixel(colorData, x0, y0, imgWidth);

                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        /// <summary>
        /// 輔助FindLargestWhiteObject
        /// </summary>
        /// <param name="colorData"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="imgWidth"></param>
        private void SetPixel(byte[] colorData, int x, int y, int imgWidth)
        {
            int index = (y * imgWidth + x) * 3;
            colorData[index] = 255; // R
            colorData[index + 1] = 0; // G
            colorData[index + 2] = 0; // B
        }

        #endregion
    }

}
