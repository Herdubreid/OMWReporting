using MediatR;
using System;

namespace OMWReporting.Feature.OMWStats
{
    public class GetOMWStatsAction : IRequest<OMWState>
    {
        public DateTime From { get; set; } 
    }
}
