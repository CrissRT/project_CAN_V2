using project_CAN.BusinessLogic.Interfaces;

namespace project_CAN.BusinessLogic
{
    public class BussinesLogic
    {
        public IUser GetSessionBL()
        {
            return new UserBL();
        }

        public IAdmin GetAdminBL()
        {
            return new AdminBL();
        }

        public IModerator GetModeratorBL()
        {
            return new ModeratorBL();
        }
    }
}
