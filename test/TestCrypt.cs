using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace test
{
    public class TestCrypt
    {
        [Fact]
        public void Crypt()
        {
            string data = "Askamsk";
            string key = "hotelonby";

            byte[] encryptedData = src.Crypt.GetEncrypted(key, data);
            string decryptedData = src.Crypt.GetDecrypted(key, encryptedData);

            Assert.Equal(data, decryptedData);
        }
    }
}
