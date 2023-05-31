using System;
using System.Collections.Generic;
using System.Text;
using SolumInfraestructure.Domain.Entities;

namespace SolumInfraestructure.Interface
{
    public interface IPrintSpool
    {
        public List<PrintSpool> GetPrintData(PrintSpool obj);
        public void SetPrintStatus(PrintSpool obj);
    }
}
