using Microsoft.AspNetCore.Mvc;
using Telligent.Admin.Application.AppServices;
using Telligent.Admin.Application.Dtos.Channel;

namespace Telligent.Admin.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChannelController : ControllerBase
{
    private readonly ChannelAppService _channelAppService;

    public ChannelController(ChannelAppService channelAppService)
    {
        _channelAppService = channelAppService;
    }

    /// <summary>
    /// 透過CompanyId取得渠道
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetChannelByCompanyIdAsync(Guid companyId)
    {
        return Ok(await _channelAppService.GetChanelByCompanyIdAsync(companyId));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateMultiChannelDto dto)
    {
        return Ok(await _channelAppService.CreateAsync(dto));
    }
}
