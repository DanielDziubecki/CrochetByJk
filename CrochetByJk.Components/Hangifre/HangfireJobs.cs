using System.IO;
using System.Linq;
using System.Web.Hosting;
using CrochetByJk.Common.Constants;

namespace CrochetByJk.Components.Hangifre
{
    public static class HangfireJobs
    {
        public static void DeleteGallery()
        {
           var dirsToDelete = Directory.EnumerateDirectories(HostingEnvironment.MapPath(GalleryConstants.ProductGalleriesPath))
                .Where(x => x.Contains(GalleryConstants.ToDeleteMark));

            foreach (var dirPath in dirsToDelete)
            {
                var dirToDelete = new DirectoryInfo(dirPath);
                foreach (var file in dirToDelete.GetFiles())
                {
                    file.Delete();
                }
                dirToDelete.Delete(true);
            }
        }
    }
}
