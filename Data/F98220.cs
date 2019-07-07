using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMWReporting.Data
{
    public class F98220GroupBy
    {
        public long OMWCD { get; set; }
        public string OMWPS { get; set; }
    }
    public class F98220Output
    {
        public F98220GroupBy groupBy { get; set; }
        public int COUNT { get; set; }
    }
    public class F98220Data
    {
        public F98220Output[] output { get; set; }
    }
    public class F98220Response
    {
        public F98220Data ds_F98220 { get; set; }
    }
    public class F98220DatabrowserRequest : Celin.AIS.DatabrowserRequest
    {
        public static Celin.AIS.Condition UPMJ(DateTime from)
        {
            return new Celin.AIS.Condition
            {
                value = new[]
                {
                    new Celin.AIS.Value
                    {
                        content = from.ToString("MM/dd/yyyy"),
                        specialValueId = "LITERAL"
                    }
                },
                controlId = "F98220.OMWCD",
                @operator = "GREATER_EQUAL"
            };
        }
        public static Celin.AIS.Condition OMWPS
        {
            get
            {
                return new Celin.AIS.Condition
                {
                    value = new[]
                    {
                        new Celin.AIS.Value
                        {
                            content = "11",
                            specialValueId = "LITERAL"
                        },
                        new Celin.AIS.Value
                        {
                            content = "37X",
                            specialValueId = "LITERAL"
                        }
                    },
                    controlId = "F98220.OMWPS",
                    @operator = "BETWEEN"

                };
            }
        }
        public F98220DatabrowserRequest(string[] groupBy, string[][] orderBy)
        {

            dataServiceType = "AGGREGATION";
            targetName = "F98220";
            targetType = "table";
            aggregation = new Celin.AIS.Aggregation();
            aggregation.aggregations.Add(new Celin.AIS.AggregationItem
            {
                aggregation = "COUNT",
                column = "*"
            });
            if (groupBy != null)
            {
                aggregation.groupBy = new List<Celin.AIS.AggregationItem>(
                    groupBy.Select(c => new Celin.AIS.AggregationItem { column = c })
                );
            }
            if (orderBy != null)
            {
                aggregation.orderBy = new List<Celin.AIS.AggregationItem>(
                    orderBy.Select(c => new Celin.AIS.AggregationItem { column = c[0], direction = c[1] })
                );
            }
            query = new Celin.AIS.Query
            {
                condition = new List<Celin.AIS.Condition>(),
                matchType = "MATCH_ALL"
            };
            query.condition.Add(new Celin.AIS.Condition
            {
                value = new[]
                {
                    new Celin.AIS.Value
                    {
                        content = "Default",
                        specialValueId = "LITERAL"
                    }
                },
                controlId = "F98220.OMWDESC",
                @operator = "NOT_EQUAL"
            });
        }
    }
}
