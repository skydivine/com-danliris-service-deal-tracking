using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class Contact : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(1000)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Email { get; set; }
        [MaxLength(255)]
        public string PhoneNumber { get; set; }
        [MaxLength(8000)]
        public string JobTitle { get; set; }
        public string Information { get; set; }
        public virtual long CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
    }
}
