using Microsoft.AspNetCore.Mvc;
using Telligent.Admin.Application.AppServices;
using Telligent.Admin.Application.Dtos.BusinessUser;

namespace Telligent.Admin.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BusinessUserController : ControllerBase
{
    private readonly BusinessUserAppService _businessUserAppService;

    public BusinessUserController(BusinessUserAppService businessUserAppService)
    {
        _businessUserAppService = businessUserAppService;
    }

    /// <summary>
    /// 建立企業後臺使用者帳號
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateBusinessUserDto dto)
    {
        return Ok(await _businessUserAppService.CreateBusinessUserAsync(dto));
    }
}
