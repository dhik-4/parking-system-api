using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.CustomModels.Fare;
using ParkingSystemAPI.CustomModels.Transaction;
using ParkingSystemAPI.Interfaces;

namespace ParkingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FareController : ControllerBase
    {
        private readonly IFareRepository _repository;
        public FareController(IFareRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<ParkingFare_Board>>> GetParkingFares(CancellationToken cancellationToken)
        {
            var datas = await _repository.GetParkingFares(cancellationToken);
            return Ok(datas);
        }

        [HttpPut]
        [Route("{Id}")]
        public async Task<ActionResult<List<ParkingFare_Board>>> UpdateParkingFare(int Id, [FromBody] ParkingFare_Input input, CancellationToken cancellationToken)
        {
            var datas = await _repository.UpdateParkingFare(Id, input, cancellationToken);
            return Ok(datas);
        }
    }
}
