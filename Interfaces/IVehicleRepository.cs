using ParkingSystemAPI.Models;

namespace ParkingSystemAPI.Interfaces
{
    public interface IVehicleRepository
    {
        Task<List<VehicleMaster>> GetVehicleList(CancellationToken cancellationToken);
        Task<bool> UpdateVehicle(byte Id, string Name, string Code, CancellationToken cancellationToken);
        Task<bool> AddVehicle(VehicleMaster data, CancellationToken cancellationToken);
        Task<bool> DeleteVehicle(byte Id, CancellationToken cancellationToken);
    }
}
