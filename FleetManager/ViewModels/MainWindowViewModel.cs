using System.Collections.ObjectModel;
using FleetManager.Models;

namespace FleetManager.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; } = new();

    public MainWindowViewModel()
    {
        Vehicles.Add(new VehicleItemViewModel(new Vehicle { Name = "Ford Transit", RegistrationNumber = "WA 12345", FuelLevel = 80, Status = VehicleStatus.Available }));
        Vehicles.Add(new VehicleItemViewModel(new Vehicle { Name = "Mercedes Sprinter", RegistrationNumber = "KR 98765", FuelLevel = 10, Status = VehicleStatus.InRoute }));
        Vehicles.Add(new VehicleItemViewModel(new Vehicle { Name = "Renault Master", RegistrationNumber = "GD 45678", FuelLevel = 50, Status = VehicleStatus.Service }));
    }
}