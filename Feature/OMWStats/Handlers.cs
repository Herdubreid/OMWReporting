using BlazorState;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OMWReporting.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OMWReporting.Feature.OMWStats
{
    public partial class OMWState
    {
        const int TIMEZONE_ADJUST = -36000;
        public class GetOMWStatsHandler : RequestHandler<GetOMWStatsAction, OMWState>
        {
            E1Service E1Service { get; set; }
            OMWState OMWState => Store.GetState<OMWState>();
            public async override Task<OMWState> Handle(GetOMWStatsAction aRequest, CancellationToken aCancellationToken)
            {
                var resp = await E1Service.Request<OMWStatsResponse>(new OMWStatsRequest());
                var Min = resp.ds_1_F98210.output.Select(r => r.COUNT).Max();
                var Max = resp.ds_1_F98210.output.Select(r => r.COUNT).Min();
                OMWState.LogFrom = OMWStatsRequest.SixMonthsAgo;
                OMWState.CheckIns = new LogHeatMap
                {
                    Max = resp.ds_1_F98210.output
                    .Where(r => r.groupBy.OMWAC == "02")
                    .Max(r => r.COUNT),
                    Points = resp.ds_1_F98210.output
                    .Where(r => r.groupBy.OMWAC == "02")
                    .Aggregate(new Dictionary<string, int>(), (a, r) =>
                    {
                        a.Add((r.groupBy.UPMJ / 1000 + TIMEZONE_ADJUST).ToString(), r.COUNT);
                        return a;
                    }),
                };
                OMWState.Transfers = new LogHeatMap
                {
                    Max = resp.ds_1_F98210.output
                    .Where(r => r.groupBy.OMWAC == "38")
                    .Max(r => r.COUNT),
                    Points = resp.ds_1_F98210.output
                    .Where(r => r.groupBy.OMWAC == "38")
                    .Aggregate(new Dictionary<string, int>(), (a, r) =>
                    {
                        a.Add((r.groupBy.UPMJ / 1000 + TIMEZONE_ADJUST).ToString(), r.COUNT);
                        return a;
                    })
                };
                OMWState.UserFrom = OMWStatsRequest.FiveWeeksAgo;
                OMWState.UserMax = resp.ds_2_F98210.output.Max(r => r.COUNT);
                OMWState.Users = resp.ds_2_F98210.output
                    .Aggregate(new List<UserHeatMap>(), (a, r) =>
                    {
                        var user = a.Find(u => u.User == r.groupBy.USER);
                        if (user == null)
                        {
                            user = new UserHeatMap { User = r.groupBy.USER, Points = new Dictionary<string, int>() };
                            a.Add(user);
                        }
                        user.Points.Add((r.groupBy.UPMJ / 1000 + TIMEZONE_ADJUST).ToString(), r.COUNT);
                        return a;
                    });
                return OMWState;
            }
            public GetOMWStatsHandler(IStore aStore, E1Service e1Service) : base(aStore)
            {
                E1Service = e1Service;
            }
        }
    }
}
