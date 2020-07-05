using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class Stage : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(4000)]
        public string Name { get; set; }
        public string DealsOrder { get; set; }
        public virtual long BoardId { get; set; }
        [ForeignKey("BoardId")]
        public virtual Board Board { get; set; }
		public virtual ICollection<Deal> Deals { get; set; }
	}
}
