using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace DndServerBase.Models
{
    public class MySqlTableModel
    {
        public string server { get; set; }
        public string table { get; set; }
        public List<string> collumns { get; set; }

        public int collumnAmt { get; set; }
    }
}