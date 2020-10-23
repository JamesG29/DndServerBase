using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace DndServerBase.Models
{
    public class NpcModel
    {
        public int NPCid { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public Image image { get; set; }
        public string Birthtown { get; set; }
        public string ShortBio { get; set; }
        public int popularity { get; set; }
    }
}