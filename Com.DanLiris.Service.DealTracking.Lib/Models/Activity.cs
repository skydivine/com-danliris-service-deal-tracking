using Com.DanLiris.Service.DealTracking.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.Models
{
    public class Activity : BaseModel
    {
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string DealCode { get; set; }
        [MaxLength(4000)]
        public string DealName { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }

        public string Notes { get; set; }

        /* TASK */
        public bool Status { get; set; }
        [MaxLength(4000)]
        public string TaskTitle { get; set; }
        [MaxLength(255)]
        public string AssignedTo { get; set; }
        public DateTimeOffset? DueDate { get; set; }

        /* NOTES */
        public virtual ICollection<ActivityAttachment> Attachments { get; set; }

        /* MOVE */
        public long? StageFromId { get; set; }
        [MaxLength(4000)]
        public string StageFromName { get; set; }
        public long? StageToId { get; set; }
        [MaxLength(4000)]
        public string StageToName { get; set; }

        public virtual long DealId { get; set; }
        [ForeignKey("DealId")]
        public virtual Deal Deal { get; set; }
    }
}
