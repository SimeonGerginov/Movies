using System.IO;
using System.Web;

using Movies.Services.Contracts;

namespace Movies.Services
{
    public class FileConverter : IFileConverter
    {
        public byte[] PostedToByteArray(HttpPostedFileBase postedFile)
        {
            byte[] imageData = null;
            HttpPostedFileBase postedImgFile = postedFile;

            using (var binary = new BinaryReader(postedImgFile.InputStream))
            {
                imageData = binary.ReadBytes(postedImgFile.ContentLength);
            }

            return imageData;
        }

        public byte[] GetDefaultPicture()
        {
            string fileName = HttpContext.Current.Server.MapPath(@"~/Content/Images/no_profile.png");
            byte[] imageData = null;

            FileInfo fileInfo = new FileInfo(fileName);
            long imageFileLength = fileInfo.Length;

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);

            imageData = binaryReader.ReadBytes((int)imageFileLength);

            return imageData;
        }
    }
}
