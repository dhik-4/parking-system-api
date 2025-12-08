using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ParkingSystemAPI.CustomModels;
using ParkingSystemAPI.CustomModels.Fare;
using ParkingSystemAPI.CustomModels.Transaction;
using ParkingSystemAPI.Interfaces;
using ParkingSystemAPI.Models;
using System;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParkingSystemAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        CustomAppDbContext _custContext;
        AppDbContext _Context;

        public TransactionRepository(CustomAppDbContext custContext, AppDbContext dbContext)
        {
            _custContext = custContext;
            _Context = dbContext;
        }

        public async Task<List<ParkingFare_Board>> GetParkingFares(CancellationToken cancellationToken)
        {
            string _sql = "select " +
                //"ROW_NUMBER() OVER (ORDER BY pf.ParkingFareId) AS RowNumber," +
                "vm.VehicleName, vm.Code, pf.Fare, um.Unit " +
                "FROM ParkingFare pf " +
                "left join UnitMaster um on um.UnitMasterId = pf.UnitMasterID " +
                "left join VehicleMaster vm on vm.VehicleMasterID = pf.VehicleMasterID";

            var Result = await _custContext.ParkingFare_Boards.FromSqlRaw<ParkingFare_Board>(_sql)
                .ToListAsync(cancellationToken);

            return Result;
        }

        public async Task<TransactionParkingEnter_Output> ParkingEnter(TransactionParkingEnter_Input input, CancellationToken cancellationToken)
        {
            TransactionParkingEnter_Output Result = new TransactionParkingEnter_Output();
            TransactionParking data = new TransactionParking()
            {
                PlateNumber = input.PlateNumber,
                UnitMasterId = 1
            };

            try
            {
                var _vehicleMaster = await _Context.VehicleMasters.FirstOrDefaultAsync(v => v.Code == input.VehicleCode);
                if (_vehicleMaster is not null)
                {
                    var parkingFare = await _Context.ParkingFares
                        .FirstOrDefaultAsync(p => p.IsActive == 1 && p.VehicleMasterId == _vehicleMaster.VehicleMasterId);
                    if (parkingFare != null)
                    {
                        string refNumber = _vehicleMaster.Code + DateTime.Now.ToString("yyyyMMddHHmmss");
                        data.RefNumber = refNumber;

                        data.TimeIn = DateTime.Now;
                        data.VehicleMasterId = _vehicleMaster.VehicleMasterId;
                        data.Tariff = 0;
                        data.IsMember = 0; // membership temporarily not available
                        data.ParkingFareId = parkingFare.ParkingFareId;

                        _Context.TransactionParkings.Add(data);
                        await _Context.SaveChangesAsync(cancellationToken);

                        _Context.ChangeTracker.Clear();

                        Result.RefNumber = refNumber;
                        Result.TimeIn = data.TimeIn;
                        Result.Vehicle = _vehicleMaster.VehicleName;
                        Result.PlateNumber = data.PlateNumber;
                    }
                }

                
            }
            catch (Exception ex)
            {
            }

            return Result;
        }

        public async Task<TransactionParkingExit_Output> ParkingExit(TransactionParkingExit_Input input, CancellationToken cancellationToken)
        {
            TransactionParkingExit_Output Result = new TransactionParkingExit_Output();

            try
            {
                var parkingData = await _Context.TransactionParkings
                        .FirstOrDefaultAsync(p => p.PlateNumber == input.PlateNumber && p.RefNumber == input.RefNumber);
                if (parkingData is not null)
                {
                    var parkingFareData = await _Context.ParkingFares.FirstOrDefaultAsync(v => v.ParkingFareId == parkingData.ParkingFareId);
                    if (parkingFareData is not null && !string.IsNullOrWhiteSpace(parkingFareData.FareSchemeJson))
                    {
                        DateTime _timeOut = input.TimeOut ?? DateTime.Now;
                        decimal totalPay = 0;
                        TimeSpan _duration = _timeOut - parkingData.TimeIn;
                        double diffHour = _duration.TotalHours;
                        double diffMinute = _duration.Minutes;


                        List<FareScheme> _fareSchemes = JsonSerializer.Deserialize<List<FareScheme>>(parkingFareData.FareSchemeJson);
                        for (int i = 0; i < _fareSchemes.Count; i++)
                        {
                            double hrPerScheme = diffHour / _fareSchemes[i].UnitHour;
                            if (_fareSchemes[i].MaxScheme > 0 && hrPerScheme > _fareSchemes[i].MaxScheme)
                            {
                                totalPay += _fareSchemes[i].MaxScheme * _fareSchemes[i].Fare;
                                diffHour -= _fareSchemes[i].MaxScheme * _fareSchemes[i].UnitHour;
                            }
                            else
                            {
                                totalPay += Math.Ceiling(Convert.ToDecimal(hrPerScheme)) * _fareSchemes[i].Fare;
                                break;
                            }
                        }

                        Result.Duration = _duration.Hours + " hour(s) " + _duration.Minutes + " minute(s)";
                        if (_duration.Days >= 1)
                        {
                            Result.Duration = _duration.Days + " day(s) " + Result.Duration;
                        }

                        await _Context.TransactionParkings.Where(t => t.TransactionId == parkingData.TransactionId)
                            .ExecuteUpdateAsync(s =>
                                s.SetProperty(s1 => s1.TimeOut, _timeOut)
                                .SetProperty(s2 => s2.TotalPay, totalPay)
                            );
                        //await _Context.SaveChangesAsync(cancellationToken);

                        _Context.ChangeTracker.Clear();

                        Result.RefNumber = parkingData.RefNumber;
                        Result.TimeIn = parkingData.TimeIn;
                        Result.TimeOut = _timeOut;
                        Result.Vehicle = _Context.VehicleMasters.Where(v => v.VehicleMasterId == parkingFareData.VehicleMasterId).Select(v => v.VehicleName).FirstOrDefault();
                        Result.PlateNumber = parkingData.PlateNumber;
                        Result.TotalPay = totalPay;
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return Result;
        }

        public async Task<TransactionParkingExit_Output> ParkingPaymentCheck(TransactionParkingExit_Input input, CancellationToken cancellationToken)
        {
            TransactionParkingExit_Output Result = new TransactionParkingExit_Output();

            try
            {
                var parkingData = await _Context.TransactionParkings
                        .FirstOrDefaultAsync(p => p.PlateNumber == input.PlateNumber && p.RefNumber == input.RefNumber);
                if (parkingData is not null)
                {
                    var parkingFareData = await _Context.ParkingFares.FirstOrDefaultAsync(v => v.ParkingFareId == parkingData.ParkingFareId);
                    if (parkingFareData is not null && !string.IsNullOrWhiteSpace(parkingFareData.FareSchemeJson) )
                    {
                        DateTime _timeOut = DateTime.Now;
                        decimal totalPay = 0;
                        TimeSpan _duration = _timeOut - parkingData.TimeIn;
                        double diffHour = _duration.TotalHours;
                        double diffMinute = _duration.Minutes;


                        List<FareScheme> _fareSchemes = JsonSerializer.Deserialize<List<FareScheme>>(parkingFareData.FareSchemeJson);
                        for (int i = 0; i < _fareSchemes.Count; i++)
                        {
                            double hrPerScheme = diffHour / _fareSchemes[i].UnitHour;
                            if (_fareSchemes[i].MaxScheme > 0 && hrPerScheme > _fareSchemes[i].MaxScheme)
                            {
                                totalPay += _fareSchemes[i].MaxScheme * _fareSchemes[i].Fare;
                                diffHour -= _fareSchemes[i].MaxScheme * _fareSchemes[i].UnitHour;
                            }
                            else
                            {
                                totalPay += Math.Ceiling(Convert.ToDecimal(hrPerScheme)) * _fareSchemes[i].Fare;
                                break;
                            }
                        }

                        Result.Duration = _duration.Hours + " hour(s) " + _duration.Minutes + " minute(s)";
                        if (_duration.Days >= 1)
                        {
                            Result.Duration = _duration.Days + " day(s) " + Result.Duration;
                        }


                        _Context.ChangeTracker.Clear();

                        Result.RefNumber = parkingData.RefNumber;
                        Result.TimeIn = parkingData.TimeIn;
                        Result.TimeOut = _timeOut;
                        Result.Vehicle = _Context.VehicleMasters.Where(v => v.VehicleMasterId == parkingFareData.VehicleMasterId).Select(v => v.VehicleName).FirstOrDefault();
                        Result.PlateNumber = parkingData.PlateNumber;
                        Result.TotalPay = totalPay;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return Result;
        }

        public async Task<List<TransactionParking_Trx>> GetParkingData(string PlateNumber, string RefNumber, string TimeIn, string TimeOut,
            string VehicleName, CancellationToken cancellationToken)
        {
            var parameters = new[]
            {
                new SqlParameter("@PlateNumber", PlateNumber ?? (object)DBNull.Value),
                new SqlParameter("@RefNumber", RefNumber ?? (object)DBNull.Value),
                new SqlParameter("@TimeIn", TimeIn ?? (object)DBNull.Value),

                new SqlParameter("@TimeOut", TimeOut ?? (object)DBNull.Value),
                new SqlParameter("@VehicleName", VehicleName ?? (object)DBNull.Value)
            };

            var Result = await _custContext.TransactionParking_Trxs.FromSqlRaw("EXEC TransactionParkingTrx " +
                "@PlateNumber ,@RefNumber, @TimeIn, @TimeOut, @VehicleName ", parameters).ToListAsync();

            return Result;
        }
    }
}