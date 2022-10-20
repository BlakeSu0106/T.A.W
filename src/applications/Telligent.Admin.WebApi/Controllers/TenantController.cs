using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telligent.Admin.Application.AppServices;
using Telligent.Admin.Application.Dtos.Tenant;

namespace Telligent.Admin.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TenantController : ControllerBase
{
    private readonly TenantAppService _tenantAppService;

    public TenantController(TenantAppService tenantAppService)
    {
        _tenantAppService = tenantAppService;
    }

    /// <summary>
    /// 建立租戶
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateTenantDto dto)
    {
        return Ok(await _tenantAppService.CreateTenantAsync(dto));
    }
    
    /// <summary>
    /// 取得全部租戶
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _tenantAppService.GetAsync());
    }
}
