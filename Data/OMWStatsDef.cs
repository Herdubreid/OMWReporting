using System;
using System.Collections.Generic;
using moment.net;

namespace OMWReporting.Data
{
    public class HeatPoint
    {
        public long Date { get; set; }
        public int Count { get; set; }
    }

    public class LogHeatMap
    {
        public int Max { get; set; }
        public IDictionary<string, int> Points { get; set; }
    }

    public class UserHeatMap
    {
        public string User { get; set; }
        public IDictionary<string, int> Points { get; set; }
    }

    public class Project
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class OMWStatsRequest : Celin.AIS.DatabrowserRequest
    {
        static public DateTime SixMonthsAgo
        {
            get
            {
                var dt = DateTime.Now.AddMonths(-5);
                return new DateTime(dt.Year, dt.Month, 1);
            }
        }
        static public DateTime FiveWeeksAgo
        {
            get
            {
                var dt = DateTime.Now.AddDays(-28);
                return dt.StartOf(moment.net.Enums.DateTimeAnchor.Week);
            }
        }
        public OMWStatsRequest()
        {
            var omwac = new F98210DatabrowserRequest(SixMonthsAgo);
            omwac.aggregation.groupBy.Add(new Celin.AIS.AggregationItem { column = "OMWAC" });

            var user = new F98210DatabrowserRequest(FiveWeeksAgo);
            user.aggregation.groupBy.Add(new Celin.AIS.AggregationItem { column = "USER" });

            outputType = "GRID_DATA";
            batchDataRequest = true;
            dataRequests = new List<Celin.AIS.DatabrowserRequest>
            {
                new F0005DatabrowserRequest("H92", new [] { "AC", "PS" }),
                omwac,
                user
            };
        }
    }

    public class OMWStatsResponse
    {
        public Celin.AIS.Form<Celin.AIS.FormData<F0005Row>> fs_0_DATABROWSE_F0005;
        public F98210Data ds_1_F98210 { get; set; }
        public F98210Data ds_2_F98210 { get; set; }
    }
}
