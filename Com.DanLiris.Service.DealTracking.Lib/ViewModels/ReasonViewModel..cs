using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.ViewModels
{
    public class ReasonViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string LoseReason { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.LoseReason))
            {
                yield return new ValidationResult("Lose Reason is required", new List<string> { "Lose Reason" });
            }
        }
    }
}
