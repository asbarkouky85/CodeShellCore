using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using CodeShellCore.Text;
using CodeShellCore.Files.Logging;
using System.Threading.Tasks;

namespace CodeShellCore.Security.Cryptography
{

    public class Encryptor
    {
        private byte[] Key { get; set; }
        private byte[] IVString { get; set; }
        public Encryptor(string key)
        {
            Key = key.ToMD5Bytes();
        }

        TripleDES GetEncryption()
        {
            TripleDES triple = TripleDES.Create();
            triple.Key = Key;
            triple.IV = new byte[triple.BlockSize / 8];
            return triple;
        }

        private Task<byte[]> EncryptToBytesAsync(byte[] inArray)
        {
            return new Task<byte[]>(() => {
                byte[] ret = null;
                try
                {
                    using (TripleDES enc = GetEncryption())
                    {
                        using (ICryptoTransform trns = enc.CreateEncryptor(enc.Key, enc.IV))
                        {
                            using (MemoryStream outStream = new MemoryStream())
                            {
                                using (CryptoStream encStream = new CryptoStream(outStream, trns, CryptoStreamMode.Write))
                                {
                                    encStream.Write(inArray, 0, inArray.Length);
                                    encStream.FlushFinalBlock();
                                    ret = outStream.ToArray();
                                }
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.WriteException(ex);
                }

                return ret;
            });
        }

        private Task<byte[]> DecryptToBytesAsync(byte[] byts) {
            return new Task<byte[]>(() => {
                byte[] ret = null;
                try
                {
                    using (TripleDES enc = GetEncryption())
                    {
                        using (ICryptoTransform trns = enc.CreateDecryptor())
                        {
                            using (MemoryStream mStr = new MemoryStream(byts))
                            {
                                using (CryptoStream str = new CryptoStream(mStr, trns, CryptoStreamMode.Read))
                                {
                                    using (MemoryStream res = new MemoryStream())
                                    {
                                        str.CopyTo(res);
                                        ret = res.ToArray();

                                    }
                                    //str.FlushFinalBlock();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteException(ex);
                }
                return ret;
            });
        }

        public byte[] EncryptToBytes(byte[] inArray)
        {
            var t = EncryptToBytesAsync(inArray);
            t.Start();
            t.Wait();
            return t.Result;
        }

        public byte[] DecryptToBytes(byte[] byts)
        {
            var t = DecryptToBytesAsync(byts);
            t.Start();
            t.Wait();
            return t.Result;
        }

        public byte[] DecryptToBytes(string st)
        {
            if (string.IsNullOrEmpty(st)) return null;
            byte[] b = Convert.FromBase64String(st);
            return DecryptToBytes(b);
        }

        public byte[] EncryptToBytes(string st)
        {
            if (string.IsNullOrEmpty(st)) return null;
            byte[] b = Encoding.UTF8.GetBytes(st);
            return EncryptToBytes(b);
        }

        public string Decrypt(byte[] byts)
        {
            byte[] b = DecryptToBytes(byts);
            if (b == null) return null;
            return Encoding.UTF8.GetString(b);
        }

        public string Encrypt(byte[] byts)
        {
            byte[] b = EncryptToBytes(byts);
            if (b == null) return null;
            return Convert.ToBase64String(b);
        }

        public string Decrypt(string st)
        {
            byte[] b = DecryptToBytes(st);
            if (b == null) return null;
            return Encoding.UTF8.GetString(b);
        }

        public string Encrypt(string st)
        {
            byte[] b = EncryptToBytes(st);
            if (b == null) return null;
            return Convert.ToBase64String(b);
        }
    }
}
