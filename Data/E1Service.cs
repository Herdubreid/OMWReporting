using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMWReporting.Data
{
    public class E1Service
    {
        private Celin.AIS.Server _e1;
        public bool IsAuthenticated => _e1.AuthResponse != null;
        public async Task<T> Request<T>(Celin.AIS.Request request)
            where T : new()
        {
            if (!IsAuthenticated)
            {
                await _e1.AuthenticateAsync();
            }
            return await _e1.RequestAsync<T>(request);
        }
        public E1Service(IConfiguration configuration)
        {
            _e1 = new Celin.AIS.Server(configuration["baseUrl"]);
            _e1.AuthRequest.username = configuration["user"];
            _e1.AuthRequest.password = configuration["password"];
        }
    }
}
