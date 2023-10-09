using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SolumInfraestructure.Domain.DBContext;
using SolumInfraestructure.Domain.Entities;
using SolumInfraestructure.Interface;
using Zebra.Sdk.Comm;
using Zebra.Sdk.Printer;
using Zebra.Sdk.Printer.Discovery;

namespace SolumInfraestructure.Service
{
    public class PrintService : IPrintSpool
    {
        
        public List<PrintSpool> GetPrintData(PrintSpool obj)
        {
            DataContextDB printObjects = new DataContextDB();
            List<PrintSpool> resp = new List<PrintSpool>();
            try
            {
                resp = printObjects.GetPrintData(obj);
                Console.WriteLine("Starting printer discovery.");
                //ZebraPrint(resp);
            }
            catch (Exception ex) {
                resp = null;
                throw new Exception(ex.Message);
            }
            return resp;
        }

        public void SetPrintStatus(PrintSpool obj)
        {
            DataContextDB printObjects = new DataContextDB();
            try
            {
                printObjects.SetPrintStatus(obj);
            }
            catch (Exception ex)
            {
                printObjects = null;
                throw new Exception(ex.Message);
            }
        }

        
        public void ZebraPrint(List<PrintSpool> printList) 
        {
            //Connection connection = new TcpConnection(printList[0].Printerip, TcpConnection.DEFAULT_CPCL_TCP_PORT);
            try
            {
                int cont = 1;
                foreach (PrintSpool obj in printList)
                {
                    try
                    {
                        cont = Decimal.ToInt32(obj.Quantity);
                        for (int i = 0; i < cont; i++)
                        {
                            //Connection connection = new TcpConnection("127.0.0.1", 9100);
                            try
                            {
                                Connection connection = new TcpConnection(obj.Printerip, obj.Printerport);
                                connection.Open();
                                ZebraPrinter printer = ZebraPrinterFactory.GetInstance(PrinterLanguage.LINE_PRINT, connection);
                                string[] strZPL = zplFormat(obj);
                                printer.PrintStoredFormat("E:FORMAT3.ZPL", strZPL);
                                Console.WriteLine("Imprimiendo etiqueta en impresora: " + obj.Printerip.ToString());
                                Thread.Sleep(500);
                                if (i == 0)
                                {
                                    // set print status Yes
                                    obj.Sprint = "Y";
                                    SetPrintStatus(obj);
                                }
                                connection.Close();
                            }
                            catch (ConnectionException ex)
                            {
                                Console.WriteLine("Error al imprimir 1: " + ex.StackTrace.ToString());
                            }
                        }
                    }
                    catch (ConnectionException ex) {
                        Console.WriteLine("Error al imprimir 2: " + ex.StackTrace.ToString());
                    }
                }
            }
            catch (ConnectionException ex)
            {
                Console.WriteLine("Error al imprimir 3: " + ex.StackTrace.ToString());
            }
        }

        private string[] zplFormat(PrintSpool obj)
        {
            int uxc = 0;
            int cont = 1;
            uxc = Decimal.ToInt32(obj.Uxc);
            cont = Decimal.ToInt32(obj.Quantity);
            string[] strZPL = new string[33];
            strZPL[0] = "^XA";
            strZPL[1] = "^PR5,4";
            strZPL[2] = "^PW799";
            strZPL[3]= "^CF0,20";
            strZPL[4]= "^FO30,60^FDCODIGO:^FS";
            strZPL[5]= "^CF0,30";
            strZPL[6]= "^FO350,30^FD" + obj.Itemcode.Trim() + "^FS";
            strZPL[7]= "^CFA,20";
            strZPL[8] = "^BY2,2,80";
            strZPL[9]= "^FO140,60^BC^FD" + obj.Itemcode.Trim() + "^FS";

            strZPL[10] = "^CF0,20";
            strZPL[11] = "^FO30,180^FDDESCRIPCION:^FS";
            strZPL[12] = "^CFA,20";
            strZPL[13]= "^FO30,200^FD" + obj.Itemname.Trim() + "^FS";

            strZPL[14]= "^CF0,20";
            strZPL[15] = "^FO30,240^FDEAN CAJA:^FS";
            strZPL[16] = "^CFA,20";
            strZPL[17] = "^BY2,2,80";
            if (!String.IsNullOrEmpty(obj.Barcode))
            {
                strZPL[18] = "^FO140,240^BC^FD" + obj.Barcode.Trim() + "^FS";
            }
            else {
                strZPL[18] = "";
            }

            strZPL[19] = "^CF0,20";
            strZPL[20] = "^FO30,370^FDLOTE/SERIE:^FS";
            strZPL[21] = "^CFA,20";
            strZPL[22] = "^BY2,2,80";
            if (!String.IsNullOrEmpty(obj.Batchnum))
            {
                strZPL[23] = "^FO140,370^BC^FD" + obj.Batchnum.Trim() + "^FS";
            }
            else {
                strZPL[23] = "";
            }

            strZPL[24] = "^FO30,480^FD" + obj.Expdate.ToString().Trim() + "^FS";
            strZPL[25] = "^CF0,20";
            strZPL[26]= "^FO290,480^FD" + obj.Docnum.Trim() + "^FS";
            strZPL[27]= "^CF0,20";
            strZPL[28] = "^FO560,480^FDUnd x Caja:^FS";
            strZPL[29] = "^CFA,20";
            strZPL[30]= "^FO660,480^FD" + uxc + "^FS";
            //strZPL[29] = "^PQ" + cont.ToString() + ",0,0,N,Y";
            //strZPL[29] = "^PQ1,1,0,N,Y";
            strZPL[31] = "";
            strZPL[32] = "^XZ";
            return strZPL;
        }

    }
}
