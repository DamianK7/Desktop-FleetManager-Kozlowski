using System.Collections.Generic;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public interface IVehicleService
{
    Task<IEnumerable<Vehicle>> GetVehiclesAsync();
    Task SaveVehiclesAsync(IEnumerable<Vehicle> vehicles);
}