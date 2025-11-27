using ParkingSystemAPI.CustomModels;
using ParkingSystemAPI.Interfaces;
using ParkingSystemAPI.Models;

namespace ParkingSystemAPI.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        CustomAppDbContext _custContext;
        AppDbContext _Context;

        public MemberRepository(CustomAppDbContext custContext, AppDbContext dbContext)
        {
            _custContext = custContext;
            _Context = dbContext;
        }
    }
}
