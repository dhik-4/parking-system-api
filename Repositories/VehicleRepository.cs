using Microsoft.EntityFrameworkCore;
using ParkingSystemAPI.CustomModels;
using ParkingSystemAPI.CustomModels.Transaction;
using ParkingSystemAPI.CustomModels.Vehicle;
using ParkingSystemAPI.Interfaces;
using ParkingSystemAPI.Models;

namespace ParkingSystemAPI.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        //CustomAppDbContext _custContext;
        AppDbContext _Context;

        public VehicleRepository(AppDbContext Context)
        {
            _Context = Context;
        }

        public async Task<List<VehicleMaster>> GetVehicleList(CancellationToken cancellationToken)
        {
            var Result = await _Context.VehicleMasters.ToListAsync(cancellationToken);

            return Result;
        }

        public async Task<bool> AddVehicle(VehicleMaster data, CancellationToken cancellationToken)
        {
            bool Result = false;
            try
            {
                _Context.VehicleMasters.Add(data);
                await _Context.SaveChangesAsync(cancellationToken);

                _Context.ChangeTracker.Clear();

                Result = true;
            }
            catch (Exception ex)
            {
            }

            return Result;
        }

        public async Task<bool> UpdateVehicle(byte Id, string Name, string Code, CancellationToken cancellationToken)
        {
            bool Result = false;
            try
            {
                _Context.VehicleMasters.Where(t => t.VehicleMasterId == Id)
                    .ExecuteUpdateAsync(s =>
                        s.SetProperty(v => v.VehicleName, Name)
                        .SetProperty(v => v.Code, Code)
                    );
                await _Context.SaveChangesAsync(cancellationToken);

                _Context.ChangeTracker.Clear();

                Result = true;
            }
            catch (Exception ex)
            {
            }

            return Result;
        }

        public async Task<bool> DeleteVehicle(byte Id, CancellationToken cancellationToken)
        {
            bool Result = false;
            try
            {
                _Context.VehicleMasters.Where(t => t.VehicleMasterId == Id).ExecuteDelete();
                await _Context.SaveChangesAsync(cancellationToken);

                _Context.ChangeTracker.Clear();

                Result = true;
            }
            catch (Exception ex)
            {
            }

            return Result;
        }
    }
}
