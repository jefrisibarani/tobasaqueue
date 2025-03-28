#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2015-2025  Jefri Sibarani
 
    This library is free software; you can redistribute it and/or
    modify it under the terms of the GNU Lesser General Public
    License as published by the Free Software Foundation; either
    version 2.1 of the License, or (at your option) any later version.

    This library is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
    Lesser General Public License for more details.

    You should have received a copy of the GNU Lesser General Public
    License along with this library; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
*/
#endregion

using System;
using System.Text;
using System.IO;
using BlowFishCS;
using System.Security.Cryptography;

namespace Tobasa
{
    public class Util
    {
        public static string GetPasswordHash(string password, string username)
        {
            string result = "";
            result = ComputeHashAsString(password, username, true);
            return result;
        }

        public static string EncryptPassword(string clearPassword, string salt)
        {
            string key = "pemudaharapanbangsa";
            string encryptedPassword = Util.EncryptBlowFish(key, salt, clearPassword);
            return encryptedPassword;
        }

        public static string DecryptPassword(string encryptedPassword,string salt)
        {
            string key = "pemudaharapanbangsa";
            string clearPwd = Util.DecryptBlowFish(key, salt, encryptedPassword);
            return clearPwd;
        }

        public static string ComputeHashAsString(string input, string salt, bool useSalt = true)
        {
            Byte[] hashedBytes = ComputeHashAsByte(input, salt, useSalt);
            return BitConverter.ToString(hashedBytes).Replace("-", "");
        }

        private static Byte[] ComputeHashAsByte(string input, string salt, bool useSalt = true)
        {
            Byte[] bInput = Encoding.UTF8.GetBytes(input);
            Byte[] bSalt = Encoding.UTF8.GetBytes(salt);

            Byte[] hashedBytes = ComputeHash(bInput, bSalt, useSalt);
            return hashedBytes;
        }

        private static Byte[] ComputeHash(Byte[] input, Byte[] salt, bool useSalt = true)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            Byte[] hashedBytes = null;

            if (useSalt)
            {
                // Combine salt and input bytes
                Byte[] saltedInput = new Byte[salt.Length + input.Length];
                salt.CopyTo(saltedInput, 0);
                input.CopyTo(saltedInput, salt.Length);
                hashedBytes = algorithm.ComputeHash(saltedInput);
            }
            else
            {
                hashedBytes = algorithm.ComputeHash(input);
            }

            return hashedBytes;
        }

        /** Encrypt with Blowfish
            @param key string with key value
            @param salt string with salt
            @param data string with actual original data
            @returns encrypted string
            Note: key max 56 char
        */
        public static string EncryptBlowFish(string key, string salt, string data)
        {
            string result = "";
            try
            {
                // Create SHA256 Hash
                Byte[] ba = ComputeHashAsByte(key, salt,true);
                BlowFish b = new BlowFish(ba);

                result = b.Encrypt_CBC(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return result;
        }

        /** Derypt with Blowfish
            @param key string with key value
            @param salt string with salt
            @param data string with actual decrypted data
            @returns decrypted string
        */
        public static string DecryptBlowFish(string key, string salt, string data)
        {
            string clearText = "";
            try
            {
                Byte[] ba = ComputeHashAsByte(key, salt, true);
                BlowFish b = new BlowFish(ba);

                clearText = b.Decrypt_CBC(data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return clearText;
        }

        static public string ProcessPath
        {
            get
            {
                // Assembly.GetExecutingAssembly().CodeBase return the executing assembly, which mean
                // if this code compiled inside a .dll,it will return the dll name

                return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            }
        }

        static public string ProcessDir
        {
            get
            {
                return Path.GetDirectoryName(Util.ProcessPath);
            }
        }

        static public string ProcessName
        {
            get
            {
                string exepath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                return Path.GetFileNameWithoutExtension(exepath);
            }
        }

    }
}
