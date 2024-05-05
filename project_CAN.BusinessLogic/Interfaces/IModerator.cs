using project_CAN.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.Domain.Entities.Moderator;

namespace project_CAN.BusinessLogic.Interfaces
{
    public interface IModerator : IUser
    {
        
        TutorialResponse AddTutorialInDB(TutorialDomainData data, string pathImagesTutorial);
        void RemoveTutorialFromDB(int id, string pathImagesTutorial);

        TutorialResponse EditTutorialInDB(TutorialDomainData data, string pathImagesTutorial);
    }
}
