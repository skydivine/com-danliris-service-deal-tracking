using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class Deal : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(4000)]
        public string Name { get; set; }
        public double Amount { get; set; }
        public double Quantity { get; set; }
        public long CompanyId { get; set; }
        [MaxLength(255)]
        public string CompanyCode { get; set; }
        [MaxLength(4000)]
        public string CompanyName { get; set; }
        public long ContactId { get; set; }
        [MaxLength(255)]
        public string ContactCode { get; set; }
        [MaxLength(4000)]
        public string ContactName { get; set; }
        public DateTimeOffset CloseDate { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public string UomUnit { get; set; }
        public virtual long StageId { get; set; }
		[ForeignKey("StageId")]
		public virtual Stage Stage { get; set; }
	}
}
