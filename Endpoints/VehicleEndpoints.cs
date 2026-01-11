using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.CustomModels.Vehicle;
using ParkingSystemAPI.Interfaces;
using ParkingSystemAPI.Models;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingSystemAPI.Endpoints
{
    public static class VehicleEndpoints
    {
        public static void MapVehicleEndpoints(this WebApplication app) 
        {
            app.MapGet("/api/vehicle", async (CancellationToken cancellationToken, IVehicleRepository _repository) =>
            {
                var datas = await _repository.GetVehicleList(cancellationToken);
                return Results.Ok(datas);
            });

            app.MapPost("/api/vehicle", async ([FromBody] VehicleMaster_Input data, CancellationToken cancellationToken, IVehicleRepository _repository) =>
            {
                VehicleMaster master = new VehicleMaster()
                {
                    VehicleName = data.VehicleName,
                    Code = data.Code,
                };

                var datas = await _repository.AddVehicle(master, cancellationToken);
                return Results.Ok(datas);
            });
        }
    }
}
