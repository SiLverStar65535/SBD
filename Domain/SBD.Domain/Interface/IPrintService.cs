using System.Collections.Generic;

namespace SBD.Domain.Interface
{
    public interface IPrintService
    {
        void PrintReceipt(List<string> InputTex);
        void PrintBageReceipt(List<string> InputTex);
    }
}
