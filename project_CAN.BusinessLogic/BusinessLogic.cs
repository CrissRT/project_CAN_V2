﻿using project_CAN.BusinessLogic.Interfaces;

namespace project_CAN.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
        }
    }
}
