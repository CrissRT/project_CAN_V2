using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using project_CAN.BusinessLogic.DBModel;
using project_CAN.Domain.Entities.Admin;
using project_CAN.Domain.Entities.Moderator;

namespace project_CAN.BusinessLogic.Core
{
    public class ModeratorApi : UserApi
    {

        protected DBTutorialTable GetContentById(int id)
        {
            using (var db = new DBTutorialContext())
            {
                var contentTutorialTable = db.Content
                    .Include(itemDB => itemDB.Image)
                    .Include(itemDB => itemDB.Video)
                    .FirstOrDefault(itemDB => itemDB.tutorialId == id);

                if (contentTutorialTable == null) return null;

                return contentTutorialTable;
            }
        }

        protected ContentResponse EditContent(ContentDomainData data, string pathImagesContent)
        {
            if (data == null) return new ContentResponse { Status = false, StatusMsg = "Datele nu au fost gasite!" };
            DBTutorialTable tutorial;
            using (var db = new DBTutorialContext())
            {
                tutorial = db.Content.Include(t => t.Image)
                    .Include(t => t.Video)
                    .FirstOrDefault(t => t.tutorialId == data.tutorialId);
                if (tutorial == null) return new ContentResponse { Status = false, StatusMsg = "Tutorialul nu a fost gasit!" };
            }

            if (data.videoLink != null && IsYouTubeLink(data.videoLink))
            {

                using (var db = new DBVideoContext())
                {
                    var existingVideo = db.Videos.FirstOrDefault(itemDB => itemDB.videoLinkId == tutorial.videoLinkId);
                    if (existingVideo != null)
                    {
                        existingVideo.videoLink = data.videoLink;
                        db.SaveChanges();
                    }
                }
            }

            if (data.title != null)
            {
                tutorial.title = data.title;
            }

            if (data.description != null)
            {
                tutorial.description = data.description;
            }

            if (data.image != null && data.image.ContentLength != 0 && data.image.ContentType.StartsWith("image"))
            {
                using (var db = new DBImageContext())
                {
                    var image = db.Images.FirstOrDefault(itemDB => itemDB.imageId == tutorial.imageId);
                    if (image != null)
                    {
                        try
                        {
                            string imagePath = Path.Combine(pathImagesContent, image.imageName);
                            // Check if the file exists
                            if (File.Exists(imagePath))
                            {
                                // Delete the file
                                File.Delete(imagePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            return new ContentResponse { Status = false, StatusMsg = "Exception occurred while deleting image: " + ex.Message };
                        }

                        // Generate a unique ID (GUID)
                        string uniqueId = Guid.NewGuid().ToString();

                        // Get the file extension of the uploaded file
                        string fileExtension = Path.GetExtension(data.image.FileName);

                        // Combine the unique ID and file extension to create the new file name
                        string newFileName = uniqueId + fileExtension;

                        // Construct the new file path with the new file name
                        string newFilePath = Path.Combine(pathImagesContent, newFileName);

                        image.imageName = newFileName;

                        // Saving image on server
                        try
                        {
                            if (!Directory.Exists(pathImagesContent))
                            {
                                Directory.CreateDirectory(pathImagesContent);
                            }

                            data.image.SaveAs(newFilePath);
                        }
                        catch (Exception ex)
                        {
                            // Log the exception
                            return new ContentResponse
                                { Status = false, StatusMsg = "Exception occurred while saving image: " + ex.Message };
                        }

                        db.SaveChanges();
                    }
                }
            }

            using (var db = new DBTutorialContext())
            {
                db.Entry(tutorial).State = EntityState.Modified;
                db.SaveChanges();
                return new ContentResponse { Status = true, StatusMsg = "Tutorialul a fost editat!" };
            }
            return null;
        }


        protected TutorialsAllData GetAllContent()
        {
            using (var db = new DBTutorialContext())
            {
                var allTutorials = db.Content
                    .Include(itemDB => itemDB.Image)
                    .Include(itemDB => itemDB.Video)
                    .ToList();
                return new TutorialsAllData { TutorialTable = allTutorials };
            }
        }

        protected internal ContentResponse AddContent(ContentDomainData data, string pathImagesContent)
        {
            if (data == null) return new ContentResponse { Status = false, StatusMsg = "Datele nu au fost gasite!" };

            if (data.videoLink == null || !IsYouTubeLink(data.videoLink)) return new ContentResponse { Status = false, StatusMsg = "Video linkul nu a fost gasit!" };

            if (data.title == null) return new ContentResponse { Status = false, StatusMsg = "Titlul nu a fost gasit!" };

            if (data.description == null) return new ContentResponse { Status = false, StatusMsg = "Descrierea tutorialului nu a fost gasita!" };

            if (data.image == null || data.image.ContentLength == 0 || !data.image.ContentType.StartsWith("image")) return new ContentResponse { Status = false, StatusMsg = "Imaginea nu a fost gasita!" };

            int insertedImageId, insertedVideoLinkId;
            using (var db = new DBImageContext())
            {
                // Generate a unique ID (GUID)
                string uniqueId = Guid.NewGuid().ToString();

                // Get the file extension of the uploaded file
                string fileExtension = Path.GetExtension(data.image.FileName);

                // Combine the unique ID and file extension to create the new file name
                string newFileName = uniqueId + fileExtension;

                // Construct the new file path with the new file name
                string newFilePath = Path.Combine(pathImagesContent, newFileName);

                var image = new DBImageTable
                {
                    imageName = newFileName,
                };

                // Saving image on server
                try
                {
                    if (!Directory.Exists(pathImagesContent))
                    {
                        Directory.CreateDirectory(pathImagesContent);
                    }

                    data.image.SaveAs(newFilePath);
                }
                catch (Exception ex)
                {
                    // Log the exception
                    return new ContentResponse
                    { Status = false, StatusMsg = "Exception occurred while saving image: " + ex.Message };
                }

                //Saving image path and name on DB
                db.Images.Add(image);
                db.SaveChanges();
                // Retrieve the ID of the inserted image
                insertedImageId = image.imageId;
            }

            using (var db = new DBVideoContext())
            {
                var video = new DBVideoTable { videoLink = data.videoLink };
                db.Videos.Add(video);
                db.SaveChanges();
                insertedVideoLinkId = video.videoLinkId;
            }

            using (var db = new DBTutorialContext())
            {
                var contentTutorialTable = db.Content.FirstOrDefault(itemDB => itemDB.title == data.title);

                if (contentTutorialTable != null) return new ContentResponse { Status = false, StatusMsg = "Content Found with this title" };

                contentTutorialTable = new DBTutorialTable
                {
                    title = data.title,
                    description = data.description,
                    imageId = insertedImageId,
                    videoLinkId = insertedVideoLinkId
                };

                db.Content.Add(contentTutorialTable);
                db.SaveChanges();
            }

            return new ContentResponse { Status = true, StatusMsg = "Content adaugat!" };
        }

        protected internal void RemoveContent(int id, string pathImagesContent)
        {
            int imageId, videoId;
            using (var db = new DBTutorialContext())
            {
                var tutorial = db.Content.FirstOrDefault(t => t.tutorialId == id);
                imageId = tutorial.imageId;
                videoId = tutorial.videoLinkId;

                var countImages = db.Content.Count(o => o.imageId == imageId);
                var countVideosLink = db.Content.Count(s => s.videoLinkId == videoId);

                db.Content.Remove(tutorial);
                db.SaveChanges();


                if (countImages == 1)
                {
                    using (var imagesDB = new DBImageContext())
                    {
                        var imageEntity = imagesDB.Images.FirstOrDefault(i => i.imageId == imageId);
                        try
                        {
                            string imagePath = Path.Combine(pathImagesContent, imageEntity.imageName);
                            // Check if the file exists
                            if (File.Exists(imagePath))
                            {
                                // Delete the file
                                File.Delete(imagePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred while deleting the image: " + ex.Message);
                        }

                        imagesDB.Images.Remove(imageEntity);
                        imagesDB.SaveChanges();
                    }
                }
                if (countVideosLink == 1)
                {
                    using (var videoDB = new DBVideoContext())
                    {
                        videoDB.Videos.Remove(videoDB.Videos.FirstOrDefault(v => v.videoLinkId == videoId));
                        videoDB.SaveChanges();
                    }
                }
            }
        }


        private bool IsYouTubeLink(string link)
        {
            // Regular expression pattern to match YouTube video links
            string pattern = @"(?:https?:\/\/)?(?:www\.)?(?:youtube\.com\/(?:[^\/\n\s]+\/\S+\/|(?:v|e(?:mbed)?)\/|\S*?[?&]v=)|youtu\.be\/)([a-zA-Z0-9_-]{11})";

            // Create a regular expression object
            Regex regex = new Regex(pattern);

            // Match the link against the regular expression pattern
            Match match = regex.Match(link);

            // Return true if the link matches the pattern, false otherwise
            return match.Success;
        }
    }
}
