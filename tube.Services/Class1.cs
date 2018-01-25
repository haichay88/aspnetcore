using System;

namespace tube.Services
{
    public  class APIKEY
    {
        public int Id { get; set; }
        public string KeyAPI { get; set; }
        public bool IsOverLimit { get; set; }
        public string Website { get; set; }
        public string AccountResgister { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
