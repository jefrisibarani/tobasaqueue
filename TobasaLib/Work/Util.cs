#region License
/*
    Tobasa Library - Provide Async TCP server, DirectShow wrapper and simple Logger class
    Copyright (C) 2021  Jefri Sibarani
 
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
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

        public static string QuoteString(string val)
        {
            return SurroundWith(val, "'");
        }
        
        public static string DoubleQuoteString(string val)
        {
            return SurroundWith(val, "\"");
        }

        public static string SurroundWith(string text, string ends)
        {
            return ends + text + ends;
        }

        public static Bitmap MakeGrayscale3(Bitmap original)
        {
            // create a blank bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            // get a graphics object from the new image
            Graphics g = Graphics.FromImage(newBitmap);

            // create the grayscale ColorMatrix
            ColorMatrix colorMatrix = new ColorMatrix(
               new float[][] 
              {
                 new float[] {.3f, .3f, .3f, 0, 0},
                 new float[] {.59f, .59f, .59f, 0, 0},
                 new float[] {.11f, .11f, .11f, 0, 0},
                 new float[] {0, 0, 0, 1, 0},
                 new float[] {0, 0, 0, 0, 1}
              });

            // create some image attributes
            ImageAttributes attributes = new ImageAttributes();

            // set the color matrix attribute
            attributes.SetColorMatrix(colorMatrix);

            // draw the original image on the new image
            // using the grayscale color matrix
            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
               0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

            //dispose the Graphics object
            g.Dispose();
            return newBitmap;
        }


		/** Check User Configuration Setting File.
            Fungsi ini dijalankan pertama kali oleh aplikasi untuk memeriksa apakah file konfigurasi
            user setting dalam kondisi corrupt atau OK.
            Bila file konfigurasi dalam kondisi OK, fungsi ini akan membuat backup
            yang akan digunakan pada waktu file konfigurasi corrupt
        */
		public static void CheckUserConfigurationFile()
		{
			try
			{
				Configuration con = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
				string confOri = con.FilePath;
				string confBak = confOri + ".backup";

				try
				{
					if (File.Exists(confOri))
					{
						// Configuration file exxists and valid, backup!
						File.Copy(confOri, confBak, true);
					}
				}
				catch (DirectoryNotFoundException e)
				{
					// First time run, no configuration file exists
					return;
				}
				catch (Exception e)
				{
					MessageBox.Show(e.ToString(), "Error");
				}

			}
			catch (ConfigurationErrorsException e)
			{
				string fileOri = e.Filename;
				string fileBak = fileOri + ".backup";
				string pesan;

				if (File.Exists(fileBak))
				{
					pesan = "File konfigurasi USER SETTINGS sepertinya rusak. \n\n" +
								fileOri + "\n\n" +
								"Ini bisa terjadi bila sebelumnya komputer crash. \n\n" +
								"Untuk melanjutkan, TobasaQueue akan me-restore " +
								"file konfigurasi USER SETTINGS dari backup yang tersedia\n\n" +
								"Click Yes untuk me-restore file atau\n" +
								"Click No untuk memperbaiki secara manual \n\n";
				}
				else
				{
					pesan = "File konfigurasi USER SETTINGS sepertinya rusak. \n\n" +
								fileOri + "\n\n" +
								"Ini bisa terjadi bila sebelumnya komputer crash. \n" +
								"Untuk melanjutkan, TobasaQueue harus me-reset file konfigurasi USER SETTINGS \n\n" +
								"Click Yes untuk me-reset USER SETTINGS atau\n" +
								"Click No untuk memperbaiki secara manual \n\n";
				}

				if (MessageBox.Show(pesan, "File user settings corrupt",
									MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
				{
					// Check for existing backup. Restore if exists
					if (File.Exists(fileBak))
					{
						try
						{
							File.Copy(fileBak, fileOri, true);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString(), "Error");
						}
					}
					else
						File.Delete(fileOri);
				}
				else
					Process.GetCurrentProcess().Kill();
			}
		}

        public static bool StrToBool(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            if (value.ToUpper() == "T"
               || value.ToUpper() == "TRUE"
               || value.ToUpper() == "ON"
               || value.ToUpper() == "Y"
               || value.ToUpper() == "YES"
               || value == "1")
                return true;

            return false;
        }

        public static string BoolToStr(bool value, string valIfTrue = "1", string valIfFalse = "0")
        {
            return value ? valIfTrue : valIfFalse;
        }

        public static void ShowConnectionError(IWin32Window owner)
        {
            MessageBox.Show(owner,"Not connected with server\r\nPlease restart application", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #region unmanaged

        [DllImport("kernel32.dll")] static extern IntPtr GetModuleHandleW(IntPtr _);
        
        //! check if we are running in GUI 
        //  NOTE: https://stackoverflow.com/a/8711036
        public static bool IsRunInGUI()
        {
            var p = GetModuleHandleW(default);
            return Marshal.ReadInt16(p, Marshal.ReadInt32(p, 0x3C) + 0x5C) == 2;
        }

        #endregion
    }
}
