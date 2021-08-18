using System;

namespace PowerBI.ReportingServices.Security.Entities
{
    public interface IBaseEntity
    {
        DateTime? CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
