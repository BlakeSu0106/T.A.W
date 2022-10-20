using Telligent.Admin.Domain.Shared;
using Telligent.Core.Application.DataTransferObjects;

namespace Telligent.Admin.Application.Dtos.Channel;

public class CreateChannelDto : EntityDto
{
    internal new Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public ChannelType ChannelType { get; set; }

    public string Name { get; set; }
}
