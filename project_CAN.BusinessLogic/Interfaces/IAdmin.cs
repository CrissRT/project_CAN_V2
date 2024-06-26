﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Entities.Admin;
using project_CAN.Domain.Entities.User;

namespace project_CAN.BusinessLogic.Interfaces
{
    public interface IAdmin : IModerator
    {
        UsersAllData GetAllUsersExceptAdminFromDB(int excludeId);
        void DeleteUserFromDB(int id);
        UDBTable GetUserByIdFromDB(int id);
        OperationOnUserResponse EditUserInDB(UserEdit data);


    }
}
