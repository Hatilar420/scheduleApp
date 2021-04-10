using scheduleBackend.Models.ClientResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace scheduleBackend.Security
{
    public interface ISecurityService
    {
        public Task<InnerToken> CheckLoginGoogle(GoogleAuthResponse res);
    }
}
