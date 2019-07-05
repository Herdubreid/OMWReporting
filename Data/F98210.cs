using System;
using System.Collections.Generic;

namespace OMWReporting.Data
{
    public class F98210GroupBy
    {
        public string OMWAC { get; set; }
        public string USER { get; set; }
        public long UPMJ { get; set; }
    }
    public class F98210Output
    {
        public F98210GroupBy groupBy { get; set; }
        public int COUNT { get; set; }
    }
    public class F98210Data
    {
        public F98210Output[] output { get; set; }
    }
    public class F98210Response
    {
        public F98210Data ds_F98210 { get; set; }
    }
    public class F98210DatabrowserRequest : Celin.AIS.DatabrowserRequest
    {
        public F98210DatabrowserRequest(DateTime from)
        {
            dataServiceType = "AGGREGATION";
            targetName = "F98210";
            targetType = "table";
            aggregation = new Celin.AIS.Aggregation();
            aggregation.aggregations.Add(new Celin.AIS.AggregationItem
            {
                aggregation = "COUNT",
                column = "*"
            });
            aggregation.groupBy.Add(new Celin.AIS.AggregationItem { column = "UPMJ" });
            query = new Celin.AIS.Query {
                condition = new List<Celin.AIS.Condition>(),
                matchType = "MATCH_ALL"
            };
            query.condition.Add(new Celin.AIS.Condition
            {
                value = new[]
                {
                    new Celin.AIS.Value
                    {
                        content = "02",
                        specialValueId = "LITERAL"
                    },
                    new Celin.AIS.Value
                    {
                        content= "38",
                        specialValueId= "LITERAL"
                    }
                },
                controlId = "F98210.OMWAC",
                @operator = "LIST"
            });
            query.condition.Add(new Celin.AIS.Condition
            {
                value = new[]
                {
                    new Celin.AIS.Value
                    {
                        content = from.ToString("dd/MM/yyyy"),
                        specialValueId = "LITERAL"
                    }
                },
                controlId = "F98210.UPMJ",
                @operator = "GREATER_EQUAL"
            });
        }
    }
}
