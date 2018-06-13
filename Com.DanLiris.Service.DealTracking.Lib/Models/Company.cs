using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class Company : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(4000)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Website { get; set; }
        [MaxLength(1000)]
        public string Industry { get; set; }
        [MaxLength(255)]
        public string PhoneNumber { get; set; }
        [MaxLength(255)]
        public string City { get; set; }
        public string Information { get; set; }
    }
}
