using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace apicallfromcontroller_18721.Models
{
    public class Emp
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Salary { get; set; }
    }
}