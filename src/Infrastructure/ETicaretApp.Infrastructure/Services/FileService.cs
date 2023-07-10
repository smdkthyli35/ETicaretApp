using System.Text.RegularExpressions;

namespace ETicaretApp.Infrastructure.Services
{
    public class FileService
    {
        async Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);

            Regex regex = new Regex("[*'\",+-._&#^@|/<>~]");
            DateTime dateTimeNow = DateTime.UtcNow;

            string newFileName = regex.Replace(oldName, string.Empty);
            string dateTime = dateTimeNow.ToString("yyyyMMddHHmmss");

            string fullName = $"{dateTime}-{newFileName}{extension}";
            return fullName;
        }
    }
}
