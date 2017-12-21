using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class TripleSecurityCryptor
    {
        public static string Encript(string inputText)
        {
            byte[] encryted = null;
            string key = "Kasperok_Rigter_24_BYTES!";

            using (TripleDESCryptoServiceProvider myCryptoServiceProvider=new TripleDESCryptoServiceProvider())
            {
                myCryptoServiceProvider.Key = Encoding.ASCII.GetBytes(key);
                myCryptoServiceProvider.IV = Encoding.ASCII.GetBytes("_8BYTES");

                encryted = EncryptingToBytes(inputText, myCryptoServiceProvider.Key, myCryptoServiceProvider.IV);

            }

            return Convert.ToBase64String(encryted);
        }

        public static string Descript(string inputText)
        {
            var encrypted = Convert.FromBase64String(inputText);

            string roundtrip = null;
            string key = "Kasperok_Rigter_24_BYTES!";

            using (TripleDESCryptoServiceProvider myTripleDES = new TripleDESCryptoServiceProvider())
            {
                myTripleDES.Key = Encoding.ASCII.GetBytes(key);
                myTripleDES.IV = Encoding.ASCII.GetBytes("_8BYTES_");

                // Decrypt the bytes to a string.
                roundtrip = DecryptStringFromBytes(encrypted, myTripleDES.Key, myTripleDES.IV);
            }

            return roundtrip;
        }


        static byte[] EncryptingToBytes(string plainText,byte[] Key,byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentException("PlainText Empty");
            }
            if (Key==null||Key.Length<=0)
            {
                throw new ArgumentException("Key Empty");
            }
            if (IV==null||IV.Length<=0)
            {
                throw new ArgumentException("IV Empty");
            }
            byte[] encrypted;
            using (TripleDESCryptoServiceProvider tripleDesCrypto=new TripleDESCryptoServiceProvider())
            {
                tripleDesCrypto.Key = Key;
                tripleDesCrypto.IV = Key;
                ICryptoTransform encryptor = tripleDesCrypto.CreateEncryptor(tripleDesCrypto.Key, tripleDesCrypto.IV);

                using (MemoryStream msEncrypt=new MemoryStream())
                {
                    using (CryptoStream csCryptoStream=new CryptoStream(msEncrypt,encryptor,CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt=new StreamWriter(csCryptoStream))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        static string DecryptStringFromBytes(byte[] lolText,byte[] Key,byte[] IV)
        {
            if (lolText == null || lolText.Length <= 0)
            {
                throw new ArgumentException("LolText Empty");
            }
            if (Key == null || Key.Length <= 0)
            {
                throw new ArgumentException("Key Empty");
            }
            if (IV == null || IV.Length <= 0)
            {
                throw new ArgumentException("IV Empty");
            }
            string plainText = null;

            using (TripleDESCryptoServiceProvider myCryptoServiceProvider=new TripleDESCryptoServiceProvider())
            {
                myCryptoServiceProvider.Key = Key;
                myCryptoServiceProvider.IV = IV;
                ICryptoTransform descryptor =
                    myCryptoServiceProvider.CreateDecryptor(myCryptoServiceProvider.Key, myCryptoServiceProvider.IV);
                using (MemoryStream msDescript=new MemoryStream(lolText))
                {
                    using (CryptoStream csDecrypt=new CryptoStream(msDescript,descryptor,CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt=new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plainText;
        }
    }
}
