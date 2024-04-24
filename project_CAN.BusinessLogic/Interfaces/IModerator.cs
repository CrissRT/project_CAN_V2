using project_CAN.Domain.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_CAN.BusinessLogic.Interfaces
{
    public interface IModerator : IUser
    {
        TutorialsAllData GetAllContentFromDB();
        ContentResponse AddContentInDB(ContentDomainData data, string pathImagesContent);
        void RemoveContentFromDB(int id, string pathImagesContent);
    }
}
