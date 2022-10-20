using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telligent.Admin.Application.AppServices;
using Telligent.Admin.Application.Dtos.Company;
using Telligent.Admin.Application.Dtos.Tenant;

namespace Telligent.Admin.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly CompanyAppService _companyAppService;

    public CompanyController(CompanyAppService companyAppService)
    {
        _companyAppService = companyAppService;
    }

    /// <summary>
    /// 建立公司
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateCompanyDto dto)
    {
        return Ok(await _companyAppService.CreateCompanyAsync(dto));
    }
    
    /// <summary>
    /// 取得全部公司
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _companyAppService.GetAllAsync());
    }
}
