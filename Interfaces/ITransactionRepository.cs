using ParkingSystemAPI.CustomModels.Transaction;

namespace ParkingSystemAPI.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionParkingEnter_Output> ParkingEnter(TransactionParkingEnter_Input input, CancellationToken cancellationToken);
        Task<TransactionParkingExit_Output> ParkingExit(TransactionParkingExit_Input input, CancellationToken cancellationToken);
        Task<List<TransactionParking_Trx>> GetParkingData(string PlateNumber, string RefNumber, string TimeIn, string TimeOut,
            string VehicleName, CancellationToken cancellationToken);
    }
}