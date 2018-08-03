using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.ViewModels
{
    public class ActivityViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public long DealId { get; set; }
        public string DealCode { get; set; }
        public string DealName { get; set; }
        public string Type { get; set; }

        public string Notes { get; set; }

        /* TASK */
        public bool Status { get; set; }
        public string TaskTitle { get; set; }
        public string AssignedTo { get; set; }
        public DateTimeOffset? DueDate { get; set; }

        /* NOTES */
        public virtual List<ActivityAttachmentViewModel> Attachments { get; set; }

        /* MOVE */
        public long? StageFromId { get; set; }
        public string StageFromName { get; set; }
        public long? StageToId { get; set; }
        public string StageToName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Type.Equals("TASK"))
            {
                if (string.IsNullOrWhiteSpace(this.Notes))
                {
                    yield return new ValidationResult("Notes is required", new List<string> { "Notes" });
                }

                if (string.IsNullOrWhiteSpace(this.TaskTitle))
                {
                    yield return new ValidationResult("Task Title is required", new List<string> { "TaskTitle" });
                }

                if (string.IsNullOrWhiteSpace(this.AssignedTo))
                {
                    yield return new ValidationResult("Assigned To is required", new List<string> { "AssignedTo" });
                }

                if (DueDate == null)
                {
                    yield return new ValidationResult("Due Date To is required", new List<string> { "DueDate" });
                }
            }
        }
    }
}
