using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using DndServerBase.Models;
using MySql.Data.MySqlClient;

namespace DndServerBase.Models
{
    public class NpcList
    {
        public List<NpcModel> Npcs = new List<NpcModel>();

        public void ResetList()
        {
            Npcs = new List<NpcModel>()
            {
                //enter no data
            };
        }

        //here we will populate the list using our MySql database.
        public void PopulateList()
        {

        }
    }
}