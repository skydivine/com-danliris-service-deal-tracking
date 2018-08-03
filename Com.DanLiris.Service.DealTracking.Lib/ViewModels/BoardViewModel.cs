using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels.Integration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.ViewModels
{
    public class BoardViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public CurrencyViewModel Currency { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Title))
            {
                yield return new ValidationResult("Title is required", new List<string> { "Title" });
            }

            if (Currency == null)
            {
                yield return new ValidationResult("Currency is required", new List<string> { "Currency" });
            }
        }
    }
}
