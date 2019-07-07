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
        const int TIMEZONE_ADJUST = 0; //-36000;
        public class GetOMWStatsHandler : RequestHandler<GetOMWStatsAction, OMWState>
        {
            E1Service E1Service { get; set; }
            OMWState OMWState => Store.GetState<OMWState>();
            LogHeatMap LogHeatMap(F98210Output[] rows, string OMWAC)
            {
                return new LogHeatMap
                {
                    Max = rows
                    .Where(r => r.groupBy.OMWAC == OMWAC)
                    .Max(r => r.COUNT as int?) ?? 0,
                    Points = rows
                    .Where(r => r.groupBy.OMWAC == OMWAC)
                    .Aggregate(new Dictionary<string, int>(), (a, r) =>
                    {
                        a.Add((r.groupBy.UPMJ / 1000 + TIMEZONE_ADJUST).ToString(), r.COUNT);
                        return a;
                    })
                };
            }
            public async override Task<OMWState> Handle(GetOMWStatsAction aRequest, CancellationToken aCancellationToken)
            {
                var resp = await E1Service.Request<OMWStatsResponse>(new OMWStatsRequest());
                OMWState.LogFrom = OMWStatsRequest.SixMonthsAgo;
                OMWState.CheckIns = LogHeatMap(resp.ds_1_F98210.output, "02");
                OMWState.Transfers = LogHeatMap(resp.ds_1_F98210.output, "38");
                OMWState.UserFrom = OMWStatsRequest.FiveWeeksAgo;
                OMWState.UserMax = resp.ds_2_F98210.output.Max(r => r.COUNT as int?) ?? 0;
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
                OMWState.Projects = new List<Project>(
                    resp.ds_3_F98220.output
                    .Select(r =>
                    {
                        var Name = resp.fs_0_DATABROWSE_F0005.data.gridData.rowset
                            .Where(e => e.F0005_RT == "PS")
                            .Single(e => e.F0005_KY.TrimStart() == r.groupBy.OMWPS);
                        return new Project
                        {
                            Status = r.groupBy.OMWPS,
                            Name = Name == null ? "N/A" : Name.F0005_DL01,
                            Count = r.COUNT
                        };
                    }));
                OMWState.ProjectsAdded = new LogHeatMap
                {
                    Max = resp.ds_4_F98220.output
                    .Max(r => r.COUNT as int?) ?? 0,
                    Points = resp.ds_4_F98220.output
                    .Aggregate(new Dictionary<string, int>(), (a, r) =>
                    {
                        a.Add((r.groupBy.OMWCD / 1000 + TIMEZONE_ADJUST).ToString(), r.COUNT);
                        return a;
                    })
                };
                return OMWState;
            }
            public GetOMWStatsHandler(IStore aStore, E1Service e1Service) : base(aStore)
            {
                E1Service = e1Service;
            }
        }
    }
}
