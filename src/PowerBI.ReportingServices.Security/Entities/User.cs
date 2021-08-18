using System;
using System.ComponentModel.DataAnnotations;

namespace PowerBI.ReportingServices.Security.Entities
{
    public sealed class User : IBaseEntity
    {
        [Key]
        public string UserName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
