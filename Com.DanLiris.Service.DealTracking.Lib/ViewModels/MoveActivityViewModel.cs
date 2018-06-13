using System;
using System.Collections.Generic;
using System.Text;

namespace Com.DanLiris.Service.DealTracking.Lib.ViewModels
{
    public class MoveActivityViewModel
    {
        public long SourceStageId { get; set; }
        public string SourceDealsOrder { get; set; }
        public string SourceStageName { get; set; }
        public string Type { get; set; }
        public long TargetStageId { get; set; }
        public string TargetDealsOrder { get; set; }
        public string TargetStageName { get; set; }
        public long DealId { get; set; }
    }
}
