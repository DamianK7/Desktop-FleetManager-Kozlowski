using FleetManager.Models;

namespace FleetManager.ViewModels;

public class VehicleItemViewModel : ViewModelBase
{
    public Vehicle Vehicle { get; }

    public VehicleItemViewModel(Vehicle vehicle)
    {
        Vehicle = vehicle;
    }
}