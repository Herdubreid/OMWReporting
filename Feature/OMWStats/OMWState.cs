using BlazorState;
using OMWReporting.Data;
using System;
using System.Collections.Generic;

namespace OMWReporting.Feature.OMWStats
{
    public partial class OMWState : State<OMWState>
    {
        public DateTime LogFrom { get; set; }
        public LogHeatMap CheckIns { get; set; }
        public LogHeatMap Transfers { get; set; }
        public int UserMax { get; set; }
        public DateTime UserFrom { get; set; }
        public IEnumerable<UserHeatMap> Users { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public LogHeatMap ProjectsAdded { get; set; }
        protected override void Initialize() { }
    }
}
