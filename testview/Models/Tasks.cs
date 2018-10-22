using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testview.Models
{
    public class Tasks
    {
        public int id { get; set; }
        public string text { get; set; }
        public Boolean completed { get; set; }
        public string secret { get; set; }
    }
}