using System.Collections.Generic;
using System.Linq;

namespace OMWReporting.Data
{
    public class F0005Row : Celin.AIS.Row
    {
        public string F0005_KY;
        public string F0005_RT;
        public string F0005_DL01;
        public string F0005_SY;
    }
    public class F0005Response : Celin.AIS.FormResponse
    {
        public Celin.AIS.Form<Celin.AIS.FormData<F0005Row>> fs_DATABROWSE_F0005;
    }
    public class F0005DatabrowserRequest : Celin.AIS.DatabrowserRequest
    {
        public F0005DatabrowserRequest(string SY, string[] RTs)
        {
            dataServiceType = "BROWSE";
            targetName = "F0005";
            targetType = "table";
            returnControlIDs = "SY|RT|KY|DL01";
            maxPageSize = "500";
            query = new Celin.AIS.Query()
            {
                matchType = "MATCH_ALL"
            };
            query.condition = new List<Celin.AIS.Condition>();
            query.condition.Add(new Celin.AIS.Condition
            {
                value = new[]
                {
                    new Celin.AIS.Value
                    {
                        content = SY,
                        specialValueId = "LITERAL"
                    },
                },
                controlId = "F0005.SY",
                @operator = "EQUAL"
            });
            query.condition.Add(new Celin.AIS.Condition
            {
                value = RTs.Select(RT => new Celin.AIS.Value
                {
                    content = RT,
                    specialValueId = "LITERAL"
                }).ToArray(),
                controlId = "F0005.RT",
                @operator = RTs.Length > 1 ? "LIST" : "EQUAL"
            });
        }
    }
}
