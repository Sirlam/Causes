using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Causes.UI.Web.Data
{
    public class SaltGenerator
    {
        private static RNGCryptoServiceProvider m_cryptoServiceProvider = null;

        private const int SALT_SIZE = 24;


        static SaltGenerator()
        {
            m_cryptoServiceProvider = new RNGCryptoServiceProvider();
        }

        public static string GetSaltString()
        {
            byte[] saltBytes = new byte[SALT_SIZE];

            m_cryptoServiceProvider.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }
    }
}