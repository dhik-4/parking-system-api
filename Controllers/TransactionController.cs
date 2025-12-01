using Microsoft.AspNetCore.Mvc;
using ParkingSystemAPI.CustomModels.Transaction;
using ParkingSystemAPI.Interfaces;

namespace ParkingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _repository;

        public TransactionController(ITransactionRepository repository)
        {
            //_context = context;
            //_CustomContext = CustomContext;
            _repository = repository;
        }

        [HttpPost]
        [Route("parking/enter")]
        public async Task<ActionResult<TransactionParkingEnter_Output>> ParkingEnter([FromBody] TransactionParkingEnter_Input input, CancellationToken cancellationToken)
        {
            var datas = await _repository.ParkingEnter(input, cancellationToken);
            return Ok(datas);
        }
        [HttpPut]
        [Route("parking/exit")]
        public async Task<ActionResult<TransactionParkingExit_Output>> ParkingExit([FromBody] TransactionParkingExit_Input input, CancellationToken cancellationToken)
        {
            var datas = await _repository.ParkingExit(input, cancellationToken);
            return Ok(datas);
        }

        [HttpGet]
        [Route("parking/data")]
        public async Task<ActionResult<List<TransactionParking_Trx>>> GetParkingData(string? PlateNumber, string? RefNumber, string? TimeIn,
            string? TimeOut, string? VehicleName, CancellationToken cancellationToken)
        {
            var datas = await _repository.GetParkingData( PlateNumber, RefNumber, TimeIn, TimeOut,
             VehicleName, cancellationToken);
            return Ok(datas);
        }
    }
}
