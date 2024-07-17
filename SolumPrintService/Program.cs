using System;
using SolumInfraestructure.Service;
using SolumInfraestructure.Domain.Entities;
//using System.ServiceProcess;

namespace SolumPrintService
{
    public class Program
    {
        public void Servicio() {
            Console.WriteLine("Iniciando programa de impresion!.");

            PrintService obj = new PrintService();
            PrintSpool model = new PrintSpool();
            model.Sec_key = 0;
            model.User_print = "";
            try
            {
                Console.WriteLine("Iniciando servicio de impresion!.");

                obj.ZebraPrint(obj.GetPrintData(model));
                Console.WriteLine("Termino!.");
            }
            catch (Exception ex)
            {
                obj = null;
                model = null;
                throw new Exception(ex.Message);
            }
        }
        static void Main(string[] args)
        {
           
        }


    }
}
