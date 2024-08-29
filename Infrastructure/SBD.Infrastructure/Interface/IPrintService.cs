using System.Collections.Generic;

namespace SBD.Infrastructure.Interface
{
    public interface IPrintService
    {
        void PrintReceipt(List<string> InputTex);
        void PrintBageReceipt(List<string> InputTex);
    }
}
