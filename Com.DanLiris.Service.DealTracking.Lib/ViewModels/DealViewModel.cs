using Com.DanLiris.Service.DealTracking.Lib.Utilities;
using Com.DanLiris.Service.DealTracking.Lib.ViewModels.Integration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.DanLiris.Service.DealTracking.Lib.ViewModels
{
    public class DealViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double Quantity { get; set; }
        public CompanyViewModel Company { get; set; }
        public ContactViewModel Contact { get; set; }
        public UomViewModel Uom { get; set; }
        public DateTimeOffset? CloseDate { get; set; }
        public string Description { get; set; }
        public string Reason { get; set; }
        public long StageId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(this.Name))
            {
                yield return new ValidationResult("Name is required", new List<string> { "Name" });
            }

            if (this.Quantity == 0)
            {
                yield return new ValidationResult("Quantity must be greater than zero", new List<string> { "Quantity" });
            }

            if (this.Uom == null)
            {
                yield return new ValidationResult("UOM is required", new List<string> { "Uom" });
            }

            if (this.Amount == 0)
            {
                yield return new ValidationResult("Amount must be greater than zero", new List<string> { "Amount" });
            }

            if (this.Company == null)
            {
                yield return new ValidationResult("Company is required", new List<string> { "Company" });
            }

            if (this.Contact == null)
            {
                yield return new ValidationResult("Contact is required", new List<string> { "Contact" });
            }

            if (this.CloseDate == null)
            {
                yield return new ValidationResult("Close Date is required", new List<string> { "CloseDate" });
            }
        }
    }
}
