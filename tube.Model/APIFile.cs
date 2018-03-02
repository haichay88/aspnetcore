using System;
using System.Collections.Generic;
using System.Text;

namespace tube.Model
{
   public class APIFile
    {

        public int Id { get; set; }
        public string KeyAPI { get; set; }
        public bool IsOverLimit { get; set; }
        public string Website { get; set; }
        public string AccountResgister { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
