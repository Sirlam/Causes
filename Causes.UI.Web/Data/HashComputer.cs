using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Causes.UI.Web.Data
{
    public class HashComputer
    {
        public string GetPassordHashAndSalt(string message)
        {
            HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(message);

            byte[] bytHash = hashAlg.ComputeHash(bytValue);

            string base64 = Convert.ToBase64String(bytHash);

            return base64;
        }
    }
}