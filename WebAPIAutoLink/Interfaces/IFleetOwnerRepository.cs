﻿using WebAPIAutoLink.Models;

namespace WebAPIAutoLink.Interfaces
{
    public interface IFleetOwnerRepository
    {
        ICollection<FleetOwner> GetFleetOwners();
        FleetOwner GetFleetOwner(int fleetOwnerId);
        FleetOwner GetFleetOwnerOfACar(int carId);
        ICollection<Car> GetCarsByOwner(int ownerId);
        bool FleetOwnerExists(int fleetOwnerId);
        bool CreateFleetOwner(FleetOwner fleetOwner);
        bool UpdateFleetOwner(FleetOwner fleetOwner);
        bool DeleteFleetOwner(FleetOwner fleetOwner);
        bool Save();
    }
}