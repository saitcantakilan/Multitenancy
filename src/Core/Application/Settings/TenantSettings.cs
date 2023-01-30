using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Settings
{
    public class TenantSettings
    {
        public DefaultSettings Defaults { get; set; }
        public List<Tenant> Tenants { get; set; }
    }

    public class Tenant
    {
        public string Name { get; set; }
        public string TenantId { get; set; }
        public string ConnectionString { get; set; }
    }
}
