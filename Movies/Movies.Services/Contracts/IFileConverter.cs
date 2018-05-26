using System.Web;

namespace Movies.Services.Contracts
{
    public interface IFileConverter
    {
        byte[] PostedToByteArray(HttpPostedFileBase postedFile);

        byte[] GetDefaultPicture();
    }
}