using MediatR;
using SeCumple.Application.Components.Devices.Dtos;

namespace SeCumple.Application.Components.Devices.Commands.CreateDevice;

public class CreateDeviceHandler: IRequestHandler<CreateDeviceCommand, DeviceResponse>
{
    public Task<DeviceResponse> Handle(CreateDeviceCommand request, CancellationToken cancellationToken)
    {

        return null!;
    }
}

public class CreateDeviceCommand: IRequest<DeviceResponse>
{
    
}