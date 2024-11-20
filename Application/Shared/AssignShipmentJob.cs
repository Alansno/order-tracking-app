using Application.Shipping.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Shared;

public class AssignShipmentJob
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public AssignShipmentJob(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public async Task ExecuteAssignShipmentAsync()
    {
        // Crear un nuevo scope para resolver los servicios Scoped (como DbContext)
        using var scope = _serviceScopeFactory.CreateScope();
        var assignShipmentService = scope.ServiceProvider.GetRequiredService<AssignShipmentService>();
        await assignShipmentService.Execute();
    }
}