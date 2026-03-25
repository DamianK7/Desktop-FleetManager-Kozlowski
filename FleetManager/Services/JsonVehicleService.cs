using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services;

public class JsonVehicleService : IVehicleService
{
    private readonly string _filePath = "vehicles.json"; 

    public async Task<IEnumerable<Vehicle>> GetVehiclesAsync()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Vehicle>();
        }

        using var stream = File.OpenRead(_filePath);
        var vehicles = await JsonSerializer.DeserializeAsync<IEnumerable<Vehicle>>(stream);
        
        return vehicles ?? new List<Vehicle>();
    }

    public async Task SaveVehiclesAsync(IEnumerable<Vehicle> vehicles)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, vehicles, options);
    }
}