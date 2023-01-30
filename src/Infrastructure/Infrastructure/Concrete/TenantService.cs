using Application.Abstractions;
using Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Concrete
{
    public class TenantService : ITenantService
    {
        readonly TenantSettings _tenantSettings;
        readonly Tenant _tenant;

        readonly HttpContext _httpContext;
        public TenantService(IOptions<TenantSettings> tenantSettings, IHttpContextAccessor httpContextAccessor)
        {
            _tenantSettings = tenantSettings.Value;
            _httpContext = httpContextAccessor.HttpContext;
            if (_httpContext != null)
            {
                if (_httpContext.Request.Headers.TryGetValue("TenantId", out var tenantId))
                {
                    _tenant = _tenantSettings.Tenants.FirstOrDefault(t => t.TenantId == tenantId);
                    if (_tenant == null) throw new Exception("Invalid tenant!");

                    if (string.IsNullOrEmpty(_tenant.ConnectionString))
                        _tenant.ConnectionString = _tenantSettings.Defaults.ConnectionString;
                }
            }
        }
        public string GetConnectionString() => _tenant?.ConnectionString;
        public string GetDatabaseProvider() => _tenantSettings.Defaults?.DBProvider;
        public Tenant GetTenant() => _tenant;
    }
}
