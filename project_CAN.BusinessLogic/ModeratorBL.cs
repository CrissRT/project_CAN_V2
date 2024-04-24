using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using project_CAN.BusinessLogic.Core;
using project_CAN.BusinessLogic.Interfaces;
using project_CAN.Domain.Entities.Admin;

namespace project_CAN.BusinessLogic
{
    public class ModeratorBL : ModeratorApi, IModerator
    {
        public ContentResponse AddContentInDB(ContentDomainData data, string pathImagesContent)
        {
            return AddContent(data, pathImagesContent);
        }

        public TutorialsAllData GetAllContentFromDB()
        {
            return GetAllContent();
        }

        public void RemoveContentFromDB(int id, string pathImagesContent)
        {
            RemoveContent(id, pathImagesContent);
        }
    }
}
