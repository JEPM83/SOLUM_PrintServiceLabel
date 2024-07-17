using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using SolumInfraestructure.Domain.Entities;
using SolumInfraestructure.Interface;
using System.Data;
using System.Data.SqlClient;

namespace SolumInfraestructure.Domain.DBContext
{
    public class DataContextDB:IPrintSpool
    {
        protected string cnxStringCRM = ConfigurationManager.ConnectionStrings["conexionCRM"].ToString();

        public List<PrintSpool> GetPrintData(PrintSpool obj)
        {
            var printListDetail = new List<PrintSpool>();
            try
            {

                using (SqlConnection conn = new SqlConnection(cnxStringCRM))
                using (SqlCommand cmd = new SqlCommand(ObjectsDA.PrintDataService, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SEC_KEY", SqlDbType.Int).Value = obj.Sec_key;
                    cmd.Parameters.Add("@USER_PRINT", SqlDbType.NVarChar).Value = obj.User_print;
                    conn.Open();
                    SqlDataReader sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        var printDetail = new PrintSpool();
                        printDetail.Ide_spool = int.Parse(sqlReader["IDE_SPOOL"].ToString());
                        printDetail.Cod_process = sqlReader["COD_PROCESS"].ToString();
                        printDetail.Ide_doc = int.Parse(sqlReader["IDE_DOC"].ToString());
                        printDetail.Ide_linedoc = int.Parse(sqlReader["IDE_LINEDOC"].ToString());
                        printDetail.Sec_key = int.Parse(sqlReader["SEC_KEY"].ToString());
                        printDetail.Docnum = sqlReader["DOCNUM"].ToString();
                        printDetail.Linenum = int.Parse(sqlReader["LINENUM"].ToString());
                        printDetail.Itemcode = sqlReader["ITEMCODE"].ToString();
                        printDetail.Itemname = sqlReader["ITEMNAME"].ToString();
                        printDetail.Suppliername = sqlReader["SUPPLIERNAME"].ToString();
                        printDetail.Batchnum = String.IsNullOrEmpty(sqlReader["BATCHNUM"].ToString()) ? String.IsNullOrEmpty(sqlReader["SERIALNUMBER"].ToString()) ? "" : sqlReader["SERIALNUMBER"].ToString() : sqlReader["BATCHNUM"].ToString();
                        printDetail.Expdate = String.IsNullOrEmpty(sqlReader["EXPDATE"].ToString()) ? "" : (sqlReader["EXPDATE"].ToString());
                        printDetail.Barcode = sqlReader["BARCODE"].ToString();
                        printDetail.Sprint = sqlReader["SPRINT"].ToString();
                        printDetail.User_print = sqlReader["USER_PRINT"].ToString();
                        printDetail.Printerip = sqlReader["PRINTERIP"].ToString();
                        printDetail.Printerport = int.Parse(sqlReader["PRINTERPORT"].ToString());
                        printDetail.Quantity = Decimal.Parse(sqlReader["QUANTITY"].ToString());
                        printDetail.Uxc = Decimal.Parse(sqlReader["UXC"].ToString());
                        printDetail.Dateauditcreate = DateTime.Parse(sqlReader["DATEAUDIT_CREATE"].ToString());
                        //
                        printListDetail.Add(printDetail);
                    }
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            return printListDetail;
        }

        public void SetPrintStatus(PrintSpool obj)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(cnxStringCRM))
                using (SqlCommand cmd = new SqlCommand(ObjectsDA.PatchPrintService, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDE_SPOOL", SqlDbType.Int).Value = obj.Ide_spool;
                    cmd.Parameters.Add("@SPRINT", SqlDbType.Char).Value = obj.Sprint;
                    conn.Open();
                    int srow = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
