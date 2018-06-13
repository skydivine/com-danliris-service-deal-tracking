using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.ViewModels
{
    public class StageDealViewModel
    {
        public long Id { get; set; }
        public string CreatedBy { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public double Quantity { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string UomUnit { get; set; }
        public DateTimeOffset CloseDate { get; set; }
        public long StageId { get; set; }
    }
}
