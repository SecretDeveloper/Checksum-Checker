using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ChecksumLib
{
    public class Checker
    {

        public string GetChecksum(string filePath, ChecksumType type)
        {
            if(!File.Exists(filePath))
                return "";
            string hash = "";
            using(var fs = new FileStream(filePath, FileMode.Open))
            {
                using (var cryptoProvider = GetCryptoProvider(type))
                {
                    hash = BitConverter.ToString(cryptoProvider.ComputeHash(fs));                
                }
            }

            return hash;
        }

        private HashAlgorithm GetCryptoProvider(ChecksumType type)
        {
            switch (type)
            {
                case ChecksumType.SHA1:
                    return new SHA1Managed();                    

                case ChecksumType.SHA256:
                    return new SHA256Managed();                    

                case ChecksumType.MD5:
                    return new MD5CryptoServiceProvider();                    

                default:
                    throw new ApplicationException("Unsupported Checksum type");
            }
        }

    }
}
