using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proxy
{
    public class ProxySetting
    {
        public string BaseUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsOnline
        {
            get
            {
                return BaseUrl.ToLower().Contains(".sharepoint.com");
            }
        }

    }
}
