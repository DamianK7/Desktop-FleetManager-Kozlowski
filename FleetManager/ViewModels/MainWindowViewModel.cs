using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using FleetManager.Models;
using FleetManager.Services;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IVehicleService? _vehicleService;
    
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();

    public MainWindowViewModel(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
        _ = LoadDataAsync();
    }

    public MainWindowViewModel() { }

    private async Task LoadDataAsync()
    {
        if (_vehicleService == null) return;

        var vehiclesFromDb = await _vehicleService.GetVehiclesAsync();
        var vehiclesList = vehiclesFromDb.ToList();
        
        
        if (!vehiclesList.Any())
        {
            vehiclesList.Add(new Vehicle { Name = "Ford Transit", RegistrationNumber = "WA 12345", FuelLevel = 80, Status = VehicleStatus.Available });
            vehiclesList.Add(new Vehicle { Name = "Mercedes Sprinter", RegistrationNumber = "KR 98765", FuelLevel = 10, Status = VehicleStatus.InRoute });
            vehiclesList.Add(new Vehicle { Name = "Renault Master", RegistrationNumber = "GD 45678", FuelLevel = 50, Status = VehicleStatus.Service });
            
            await _vehicleService.SaveVehiclesAsync(vehiclesList);
        }

      
        foreach (var vehicle in vehiclesList)
        {
            var viewModel = new VehicleItemViewModel(vehicle);
            
            viewModel.Vehicle.PropertyChanged += Vehicle_PropertyChanged;
            
            Vehicles.Add(viewModel);
        }
    }

    private async void Vehicle_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_vehicleService != null)
        {
            var allVehicles = Vehicles.Select(vm => vm.Vehicle);
            await _vehicleService.SaveVehiclesAsync(allVehicles);
        }
    }
}