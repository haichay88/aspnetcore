using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace tube.Entities
{
    public class EntitiesBase
    {
        [Key]
        public int Id { get; set; }

        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
