using project_CAN.BusinessLogic.Interfaces;

namespace project_CAN.BusinessLogic
{
    public class BussinesLogic
    {
        public ISession GetSessionBL()
        {
            return new SessionBL();
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
