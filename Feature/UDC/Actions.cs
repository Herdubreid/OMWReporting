using MediatR;

namespace OMWReporting.Feature.UDC
{
    public class GetUDCAction : IRequest<UdcState>
    {
        public string SY { get; set; }
        public string [] RT { get; set; }
    }
}
