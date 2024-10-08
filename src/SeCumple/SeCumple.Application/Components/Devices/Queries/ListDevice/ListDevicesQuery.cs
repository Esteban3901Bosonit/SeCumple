using MediatR;
using SeCumple.Application.Components.Devices.Dtos;
using SeCumple.Domain.Entities;
using SeCumple.Infrastructure.Persistence.Interfaces;

namespace SeCumple.Application.Components.Devices.Queries.ListDevice;

public class ListDevicesQuery(IGenericRepository<Device> repository)
    : IRequestHandler<ListDeviceCommand, IEnumerable<DeviceResponse>>
{
    public async Task<IEnumerable<DeviceResponse>> Handle(ListDeviceCommand request, CancellationToken cancellationToken)
    {
        var devices = await repository.GetAllAsync<Device>();
        return null!;
    }
}

public class ListDeviceCommand: IRequest<IEnumerable<DeviceResponse>>
{
    
}