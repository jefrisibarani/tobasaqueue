using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Tobasa
{
    public class ToolUsageInfo
    {
        public static void ShowUsageInfo()
        {
            Form infoForm = new Form
            {
                Text = "Tool Usage Information",
                Width = 500,
                Height = 450,
                StartPosition = FormStartPosition.CenterScreen,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            WebBrowser webBrowser = new WebBrowser
            {
                Dock = DockStyle.Top,
                Height = 350, // Keep space for the button
                AllowNavigation = false,
                IsWebBrowserContextMenuEnabled = false,
                ScriptErrorsSuppressed = true
            };


            string htmlContent = @"
            <html>
            <head>
                <style>
                    body { font-family: Arial, sans-serif; padding: 10px; }
                    h2 { color: #0078D7; }
                    ul { margin: 0; padding-left: 20px; }
                </style>
                <script>
                    function openFolder() {
                        window.external.OpenFolder();
                    }
                    function openBrowser() {{
                        window.external.OpenBrowser();
                    }}
                </script>
            </head>
            <body>
                <h3>QueueConfig.exe</h3>
                <p>Tool ini harus dijalankan dari dalam folder Aplikasi Antrian Tobasa, 
                   yang berisi semua modul aplikasi.<br/>
                   Tool ini akan membuat file konfigurasi default untuk semua modul-modul tadi.<br/>
                    <ul>
                    <li>QueueCaller.exe.config</li>
                    <li>QueueAdmin.exe.config</li>
                    <li>QueueDisplay.exe.config</li>
                    <li>QueueService.exe.config</li>
                    <li>QueueTicket.exe.config</li>
                    </ul>
                Setelah menggunakan tool ini,<br/>
                hapus semua file/folder konfigurasi yang mungkin sudah ada yang ada di dalam folder<br/>
                <b><a href='#' onclick='openFolder()'>%HOMEPATH%\AppData\Local\Mangapul,</a></b></br/>
                <b><a href='#' onclick='openFolder()'>%HOMEPATH%\AppData\Roaming\Mangapul,</a></b></br/>
                agar aplikasi Antrian Tobasa menggunakan file-file konfigurasi yang baru.
                </p>
                <p>Untuk info lebih lanjut buka <a href='#' onclick='openBrowser()'>www.mangapul.net</a></p>
            </body>
            </html>";


            webBrowser.DocumentText = htmlContent;

            // Attach event to allow JavaScript to call C# method
            webBrowser.ObjectForScripting = new ExplorerHelper();

            Button closeButton = new Button
            {
                Text = "OK",
                Dock = DockStyle.Bottom,
                Width = 100,
                Height = 40
            };
            closeButton.Click += (sender, e) => infoForm.Close();

            infoForm.Controls.Add(webBrowser);
            infoForm.Controls.Add(closeButton);

            infoForm.ShowDialog();
        }
    }

    // Class to handle JavaScript call
    [System.Runtime.InteropServices.ComVisible(true)]
    public class ExplorerHelper
    {
        public void OpenFolder()
        {
            // Resolve environment variable %HOMEPATH%
            string homePath = Environment.ExpandEnvironmentVariables("C:%HOMEPATH%\\AppData\\Local\\Mangapul");

            if (Directory.Exists(homePath))
            {
                Process.Start("explorer.exe", homePath);
            }
            else
            {
                MessageBox.Show("Folder does not exist: " + homePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenBrowser()
        {
            try
            {
                Process.Start("www.mangapul.net");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open browser: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}


