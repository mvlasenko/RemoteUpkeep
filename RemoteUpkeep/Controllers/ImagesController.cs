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
                var imageEntity = context.Images.FirstOrDefault(x => x.Id == new Guid(id));

                string key = "File_" + id;

                byte[] binary = this.HttpContext.Cache[key] as byte[];
                if (binary == null)
                {
                    binary = ImageHelper.GetImageCroped(imageEntity.Binary, 100, 100, true);
                    this.HttpContext.Cache.Insert(key, binary);
                }

                return File(new MemoryStream(binary), ImageHelper.GetContentType(Path.GetExtension(imageEntity.Name)), imageEntity.Name);
            }
        }

        [HttpPost]
        public virtual ActionResult FileUpload(HttpPostedFileBase[] httpFiles)
        {
            List<PostedFile> files = new List<PostedFile>();

            foreach (HttpPostedFileBase httpFile in httpFiles)
            {
                var memStream = new MemoryStream();
                httpFile.InputStream.CopyTo(memStream);

                byte[] fileData = memStream.ToArray();

                using (var context = new ApplicationDbContext())
                {
                    Image newImage = new Image { Binary = fileData, Name = httpFile.FileName, CreatedDateTime = DateTime.Now };
                    newImage = context.Images.Add(newImage);
                    context.SaveChanges();

                    files.Add(new PostedFile
                    {
                        id = newImage.Id.ToString(),
                        url = "/admin/Images/Image/" + newImage.Id,
                        type = ImageHelper.GetContentType(Path.GetExtension(newImage.Name)),
                        thumbnailUrl = "/Content/images/image.png",
                        size = newImage.Binary.Length,
                        name = newImage.Name
                    });
                }
            }

            return Json(new { files });
        }
    }
}