using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Entities.Admin;

namespace project_CAN.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        //public void AddUser();
        //public void DeleteUser();
        //public void UpdateUser();
       UsersAllData GetAllUsersFromDB();
    }
}
