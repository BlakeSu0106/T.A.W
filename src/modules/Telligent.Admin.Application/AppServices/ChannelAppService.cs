using Telligent.Admin.Domain.Channel;
using Telligent.Admin.Application.Dtos.Channel;
using Telligent.Core.Application.Services;
using Telligent.Core.Domain.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Telligent.Core.Infrastructure.Generators;

namespace Telligent.Admin.Application.AppServices;

public class ChannelAppService : CrudAppService<Channel, ChannelDto, CreateChannelDto, ChannelDto>
{
    private readonly UnitOfWork _uow;
    private readonly AdminUserAppService _adminUserAppService;
    private readonly CompanyAppService _companyAppService;

    private string _userId;

    public ChannelAppService(
        IRepository<Channel> repository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        AdminUserAppService adminUserAppService,
        CompanyAppService companyAppService,
        UnitOfWork uow)
        : base(repository, mapper, httpContextAccessor)
    {
        _uow = uow;
        _adminUserAppService = adminUserAppService;
        _companyAppService = companyAppService;

        _userId = httpContextAccessor.HttpContext.Request.Headers["User"].ToString();

        DataInitializeAsync().Wait();
    }

    /// <summary>
    /// 透過CompanyId取得渠道
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    public async Task<IList<ChannelDto>> GetChanelByCompanyIdAsync(Guid companyId)
    {
        return await GetListAsync(c => c.CompanyId.Equals(companyId) && c.EntityStatus);
    }

    /// <summary>
    /// 建立渠道
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="ValidationException"></exception>
    public async Task<IList<ChannelDto>> CreateAsync(CreateMultiChannelDto dto)
    {
        if (!Guid.TryParse(_userId, out var creatorId)) throw new ValidationException("無法取得維護人員資訊");

        var channels = new List<Guid>();

        foreach (var createChannelDto in dto.Channels)
        {
            var channel = await Repository.GetAsync(c => c.CompanyId.Equals(createChannelDto.CompanyId)
                                                         && c.ChannleType.Equals(createChannelDto.ChannelType)
                                                         && c.Name.Equals(createChannelDto.Name)
                                                         && c.EntityStatus);

            if (channel == null)
            {
                var channelEntity = Mapper.Map<Channel>(createChannelDto);
                channelEntity.Id = SequentialGuidGenerator.Instance.GetGuid();
                channelEntity.CreatorId = creatorId;

                await _uow.ChannelRepository.CreateAsync(channelEntity);
                channels.Add(channelEntity.Id);
            }
            else
                throw new ValidationException("渠道名稱重複");
        }

        await _uow.SaveChangeAsync();

        return await GetListAsync(m => channels.Contains(m.Id));
    }

    private async Task DataInitializeAsync()
    {
        if (!string.IsNullOrEmpty(_userId))
        {
            var userDto = await _adminUserAppService.GetAdminAsync(_userId);

            if (userDto != null)
                _userId = userDto.Id.ToString();
            else
                _userId = null;

        }
    }
}
