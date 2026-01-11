using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.CustomModels.Transaction;
using ParkingSystemAPI.CustomModels.Vehicle;
using ParkingSystemAPI.Interfaces;
using ParkingSystemAPI.Models;

namespace ParkingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _repository;

        public VehicleController(IVehicleRepository repository)
        {
            _repository = repository;
        }

        /*[HttpGet]
        //[Route("parkingfares")]
        public async Task<ActionResult<List<VehicleMaster>>> GetVehicleList(CancellationToken cancellationToken)
        {
            var datas = await _repository.GetVehicleList(cancellationToken);
            return Ok(datas);
        }*/

        /*[HttpPost]
        public async Task<ActionResult<bool>> AddVehicle([FromBody] VehicleMaster_Input data, CancellationToken cancellationToken)
        {
            VehicleMaster master = new VehicleMaster()
            {
                VehicleName = data.VehicleName,
                Code = data.Code,
            };

            var datas = await _repository.AddVehicle(master, cancellationToken);
            return Ok(datas);
        }*/

        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult<bool>> UpdateVehicle(byte Id, [FromBody] VehicleMaster_Input master, CancellationToken cancellationToken)
        {
            var datas = await _repository.UpdateVehicle(Id, master.VehicleName, master.Code, cancellationToken);
            return Ok(datas);
        }

        [HttpDelete]
        [Route("{Id}")]
        public async Task<ActionResult<bool>> DeleteVehicle(byte Id, CancellationToken cancellationToken)
        {
            var datas = await _repository.DeleteVehicle(Id, cancellationToken);
            return Ok(datas);
        }
    }
}
