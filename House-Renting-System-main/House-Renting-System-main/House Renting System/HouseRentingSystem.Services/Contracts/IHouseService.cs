using House_Renting_System.Services.Models.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystem.Services.Contracts
{
  public interface IHouseService
    {
        IEnumerable<HouseViewModel> GetHouseByUserId(string userId);
    }
}
