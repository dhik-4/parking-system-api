using ParkingSystemAPI.CustomModels.Fare;
using ParkingSystemAPI.CustomModels.Transaction;

namespace ParkingSystemAPI.Interfaces
{
    public interface IFareRepository
    {
        Task<List<ParkingFare_Board>> GetParkingFares(CancellationToken cancellationToken);
        Task<int> UpdateParkingFare(int Id, ParkingFare_Input input, CancellationToken cancellationToken);
    }
}
