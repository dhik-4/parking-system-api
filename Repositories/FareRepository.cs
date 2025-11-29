using Microsoft.EntityFrameworkCore;
using ParkingSystemAPI.CustomModels;
using ParkingSystemAPI.CustomModels.Fare;
using ParkingSystemAPI.CustomModels.Transaction;
using ParkingSystemAPI.Interfaces;
using ParkingSystemAPI.Models;
using System.Text.Json;

namespace ParkingSystemAPI.Repositories
{
    public class FareRepository : IFareRepository
    {
        CustomAppDbContext _custContext;
        AppDbContext _Context;

        public FareRepository(CustomAppDbContext custContext, AppDbContext dbContext)
        {
            _custContext = custContext;
            _Context = dbContext;
        }

        public async Task<List<ParkingFare_Board>> GetParkingFares(CancellationToken cancellationToken)
        {
            string _sql = "select " +
                //"ROW_NUMBER() OVER (ORDER BY pf.ParkingFareId) AS RowNumber," +
                "pf.ParkingFareID as ID, vm.VehicleName, vm.Code, pf.Fare,  pf.isActive, pf.FareSchemeJson " +
                "FROM ParkingFare pf " +
                "left join UnitMaster um on um.UnitMasterId = pf.UnitMasterID " +
                "left join VehicleMaster vm on vm.VehicleMasterID = pf.VehicleMasterID";

            var Result = await _custContext.ParkingFare_Boards.FromSqlRaw<ParkingFare_Board>(_sql)
                .ToListAsync(cancellationToken);

            for (int i = 0; i < Result.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(Result[i].FareSchemeJson))
                {
                    Result[i].FareSchemes = JsonSerializer.Deserialize<List<FareScheme>>(Result[i].FareSchemeJson);
                }
            }

            return Result;
        }

        public async Task<int> UpdateParkingFare(int Id, ParkingFare_Input input, CancellationToken cancellationToken)
        {
            int ResultAffected = 0;
            try
            {
                string fareSchemeJson = JsonSerializer.Serialize(input.FareScheme);

                ResultAffected = await _Context.ParkingFares.Where(t => t.ParkingFareId == Id)
                    .ExecuteUpdateAsync(s =>
                        s.SetProperty(v => v.VehicleMasterId, input.VehicleMasterId)
                        .SetProperty(v => v.IsActive, input.IsActive)
                        .SetProperty(v => v.FareSchemeJson, fareSchemeJson)
                    );
                //await _Context.SaveChangesAsync(cancellationToken);

                _Context.ChangeTracker.Clear();

            }
            catch (Exception ex)
            {
            }

            return ResultAffected;
        }
    }
}
