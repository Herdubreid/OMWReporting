using BlazorState;
using OMWReporting.Data;
using System.Threading;
using System.Threading.Tasks;

namespace OMWReporting.Feature.UDC
{
    public partial class UdcState
    {
        public class GetUDCHandler : RequestHandler<GetUDCAction, UdcState>
        {
            E1Service E1Service { get; set; }
            UdcState UdcState => Store.GetState<UdcState>();
            public async override Task<UdcState> Handle(GetUDCAction aRequest, CancellationToken aCancellationToken)
            {
                UdcState.Udcs = await E1Service.Request<F0005Response>(new F0005DatabrowserRequest(aRequest.SY, aRequest.RT));
                return UdcState;
            }
            public GetUDCHandler(IStore aStore, E1Service e1Service) : base(aStore)
            {
                E1Service = e1Service;
            }
        }
    }
}
