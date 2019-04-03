using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Proxy.Pages
{
    public class IndexModel : PageModel
    {
        public ProxySetting Setting  { get; private set; }
        public IndexModel(IOptions<ProxySetting> setting)
        {
            Setting = setting.Value;            
        }
        public void OnGetAsync()
        {
            
        }
    }
}
