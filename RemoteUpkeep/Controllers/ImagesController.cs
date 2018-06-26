using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteUpkeep.Helpers;
using RemoteUpkeep.Models;
using RemoteUpkeep.ViewModels;

namespace Bionet4.Admin.Controllers
{
    public class ImagesController : Controller
    {
        [Route("Image/{id}")]
        public ActionResult Image(string id)
        {
            using (var context = new ApplicationDbContext())
            {
                Image image = context.Images.FirstOrDefault(x => x.Id == new Guid(id));

                string key = "File_" + id;

                byte[] binary = this.HttpContext.Cache[key] as byte[];
                if (binary == null)
                {
                    binary = ImageHelper.GetImageCroped(image.Binary, 100, 100, true);
                    this.HttpContext.Cache.Insert(key, binary);
                }

                return File(new MemoryStream(binary), ImageHelper.GetContentType(Path.GetExtension(image.Name)), image.Name);
            }
        }

        [HttpPost]
        public virtual ActionResult FileUpload(HttpPostedFileBase[] files)
        {
            List<PostedFile> postedFiles = new List<PostedFile>();

            foreach (HttpPostedFileBase file in files)
            {
                var memStream = new MemoryStream();
                file.InputStream.CopyTo(memStream);

                byte[] fileData = memStream.ToArray();

                using (var context = new ApplicationDbContext())
                {
                    Image image = context.Images.Where(x => x.Name == file.FileName).ToList().FirstOrDefault(x => x.Binary.Length == fileData.Length);
                    if (image == null)
                    {
                        image = new Image { Binary = fileData, Name = file.FileName, CreatedDateTime = DateTime.Now };
                        image = context.Images.Add(image);
                        context.SaveChanges();
                    }

                    postedFiles.Add(new PostedFile
                    {
                        id = image.Id.ToString(),
                        url = "/Images/Image/" + image.Id,
                        type = ImageHelper.GetContentType(Path.GetExtension(image.Name)),
                        thumbnailUrl = "/Content/images/image.png",
                        size = image.Binary.Length,
                        name = image.Name
                    });
                }
            }

            return Json(new { files = postedFiles });
        }
    }
}