using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DndServerBase.Models;

namespace DndServerBase.Models
{
    public class MSDictionaries
    {
        //this is where we will store all of our database connections through a hashtable
        public static Dictionary<string, MySqlConnection> MySqlDataBaseConnections = new Dictionary<string, MySqlConnection>()
        {
            {"dndserver",  new MySqlConnection("SERVER=localhost;DATABASE=dndserver;UID=root;PASSWORD=")},

        };

        public static Dictionary<string, MySqlTableModel> MySqlTables = new Dictionary<string, MySqlTableModel>()
        {
            {"npcs", npcsTable},
            {"npcsfullname", npcsFullnameTable}
        };


        //WHOGLAHUGHUETHI BORDER

        public static MySqlTableModel npcsTable = new MySqlTableModel()
        {
            server = "dndserver",
            table = "npcs",
            collumns = new List<string>{"NPCid", "Firstname", "Middlename", "Lastname",
                             "Image", "Birthtown", "ShortBio"},
            collumnAmt = 7
        };

        public static MySqlTableModel npcsFullnameTable = new MySqlTableModel()
        {
            server = "dndserver",
            table = "npcfullname",
            collumns = new List<string> { "NPCid", "npcfullname" },
            collumnAmt = 2
        };


    }
}