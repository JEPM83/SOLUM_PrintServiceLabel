using System;
using System.Collections.Generic;
using System.Text;

namespace SolumInfraestructure.Domain.Entities
{
    public class PrintSpool
    {
        private int _ide_spool;
        private string _cod_process;
        private int _ide_doc;
        private int _ide_linedoc;
        private int _sec_key;
        private string _docnum;
        private int _linenum;
        private string _itemcode;
        private string _itemname;
        private string _suppliername;
        private string _batchnum;
        private string _serialnum;
        private DateTime _expdate;
        private string _barcode;
        private string _sprint;
        private string _user_print;
        private DateTime _dateauditcreate;
        private string _printerip;
        private int _printerport;
        private decimal _quantity;
        private decimal _uxc;
        public int Ide_spool { get => _ide_spool; set => _ide_spool = value; }
        public string Cod_process { get => _cod_process; set => _cod_process = value; }
        public int Ide_doc { get => _ide_doc; set => _ide_doc = value; }
        public int Ide_linedoc { get => _ide_linedoc; set => _ide_linedoc = value; }
        public int Sec_key { get => _sec_key; set => _sec_key = value; }
        public string Docnum { get => _docnum; set => _docnum = value; }
        public int Linenum { get => _linenum; set => _linenum = value; }
        public string Itemcode { get => _itemcode; set => _itemcode = value; }
        public string Itemname { get => _itemname; set => _itemname = value; }
        public string Suppliername { get => _suppliername; set => _suppliername = value; }
        public string Batchnum { get => _batchnum; set => _batchnum = value; }
        public string Serialnum { get => _serialnum; set => _serialnum = value; }
        public DateTime Expdate { get => _expdate; set => _expdate = value; }
        public string Barcode { get => _barcode; set => _barcode = value; }
        public string Sprint { get => _sprint; set => _sprint = value; }
        public string User_print { get => _user_print; set => _user_print = value; }
        public DateTime Dateauditcreate { get => _dateauditcreate; set => _dateauditcreate = value; }
        public string Printerip { get => _printerip; set => _printerip = value; }
        public int Printerport { get => _printerport; set => _printerport = value; }
        public decimal Quantity { get => _quantity; set => _quantity = value; }
        public decimal Uxc { get => _uxc; set => _uxc = value; }
    }
}
