using System.Reactive;
using ReactiveUI;
using FleetManager.Models;

namespace FleetManager.ViewModels;

public class VehicleItemViewModel : ViewModelBase
{
    public Vehicle Vehicle { get; }

    
    public ReactiveCommand<Unit, Unit> RefuelCommand { get; }
    public ReactiveCommand<Unit, Unit> SendInRouteCommand { get; }
    public ReactiveCommand<Unit, Unit> SetAvailableCommand { get; }
    public ReactiveCommand<Unit, Unit> SetServiceCommand { get; }

    public VehicleItemViewModel(Vehicle vehicle)
    {
        Vehicle = vehicle;
        
        var canRefuel = this.WhenAnyValue(
            vm => vm.Vehicle.Status,
            status => status != VehicleStatus.InRoute);

        RefuelCommand = ReactiveCommand.Create(() => 
        {
            Vehicle.FuelLevel = 100;
        }, canRefuel);
        
        
        var canSendInRoute = this.WhenAnyValue(
            vm => vm.Vehicle.FuelLevel,
            vm => vm.Vehicle.Status,
            (fuel, status) => fuel >= 15 && status != VehicleStatus.Service && status != VehicleStatus.InRoute);

        SendInRouteCommand = ReactiveCommand.Create(() => 
        {
            Vehicle.Status = VehicleStatus.InRoute;
        }, canSendInRoute);
        
        
        var canSetAvailable = this.WhenAnyValue(
            vm => vm.Vehicle.Status,
            status => status != VehicleStatus.Available);

        SetAvailableCommand = ReactiveCommand.Create(() => 
        {
            Vehicle.Status = VehicleStatus.Available;
        }, canSetAvailable);

      
        var canSetService = this.WhenAnyValue(
            vm => vm.Vehicle.Status,
            status => status != VehicleStatus.Service);

        SetServiceCommand = ReactiveCommand.Create(() => 
        {
            Vehicle.Status = VehicleStatus.Service;
        }, canSetService);
    }
}