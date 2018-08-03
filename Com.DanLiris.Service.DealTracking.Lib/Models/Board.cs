using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class Board : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(4000)]
        public string Title { get; set; }
        public int CurrencyId { get; set; }
        [MaxLength(255)]
        public string CurrencyCode { get; set; }
        [MaxLength(255)]
        public string CurrencySymbol { get; set; }
        public virtual ICollection<Stage> Stages { get; set; }
    }
}
