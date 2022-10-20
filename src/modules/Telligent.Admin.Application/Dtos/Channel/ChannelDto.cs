using Telligent.Admin.Domain.Shared;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Channel;

public class ChannelDto : EntityDto
{
    public Guid CompanyId { get; set; }

    public ChannelType ChannelType { get; set; }

    public string Name { get; set; }
}
