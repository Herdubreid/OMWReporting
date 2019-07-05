using BlazorState;

namespace OMWReporting.Feature.UDC
{
    public partial class UdcState : State<UdcState>
    {
        public Data.F0005Response Udcs { get; set; }
        protected override void Initialize() => Udcs = null;
    }
}
