using System.Text;

namespace src
{
    public class Crypt
    {
        public static byte[] GetEncrypted(string key, string data)
        {
            return Encoding.Unicode.GetBytes(data);
        }

        public static string GetDecrypted(string key, byte[] data)
        {
            return Encoding.Unicode.GetString(data);
        }
    }
}
