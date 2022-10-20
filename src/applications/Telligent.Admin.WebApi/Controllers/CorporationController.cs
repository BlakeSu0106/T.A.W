using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telligent.Admin.Application.AppServices;
using Telligent.Admin.Application.Dtos.Corporation;
using Telligent.Admin.Application.Dtos.Tenant;

namespace Telligent.Admin.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CorporationController : ControllerBase
{
    private readonly CorporationAppService _corporationAppService;

    public CorporationController(CorporationAppService corporationAppService)
    {
        _corporationAppService = corporationAppService;
    }

    /// <summary>
    /// 建立集團
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCorporationDto dto)
    {
        return Ok(await _corporationAppService.CreateCorporationAsync(dto));
    }
    
    /// <summary>
    /// 取得全部集團
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _corporationAppService.GetAsync());
    }
}
