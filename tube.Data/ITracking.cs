using System;
using System.Collections.Generic;
using System.Text;

namespace tube.Data
{
    public interface ITracking
    {
        string CreatedBy { get; set; }
        DateTimeOffset? CreatedDate { get; set; }

        int Version { get; set; }
        string ModifiedBy { get; set; }
        DateTimeOffset? ModifiedDate { get; set; }

        bool IsDeleted { get; set; }
        string DeletedBy { get; set; }
        DateTimeOffset? DeletedDate { get; set; }
    }
}
