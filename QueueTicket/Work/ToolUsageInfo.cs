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
                Text = "Information",
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
                <h3>QueueTicket</h3>
                <p>Secara default modul ini akan running full screen.<br/><br/>
                   Untuk keluar dari full screen, tekan tombol keyboard <b>F</b> <br/>
                   Untuk kembali full screen, tekan tombol keyboard <b>F</b> <br/>
                   Untuk masuk ke bagian pengaturan, tekan tombol <b>Ctrl</b> dan <b>O</b> secara bersamaan<br/>
                   Untuk menutup tekan tombol <b>Alt</b> dan <b>F4</b> secara bersamaan<br/>
                </p>
                <p>Untuk info lebih lanjut buka <a href='#' onclick='openBrowser()'>www.mangapul.net</a></p>
            </body>
            </html>";

            webBrowser.DocumentText = htmlContent;

            // Attach event to allow JavaScript to call C# method
            webBrowser.ObjectForScripting = new ExplorerHelper();


            Panel bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                Padding = new Padding(20, 2, 20, 2)
            };

            CheckBox showNextTimeCheckBox = new CheckBox
            {
                Text = "Show this info dialog next time",
                AutoSize = true,
                Left = 20,
                Top = 25,
                Checked = true,
            };

            Button closeButton = new Button
            {
                Text = "Close",
                AutoSize = true,
                Top = 15,
                Left = bottomPanel.Width - 120, 
                Anchor = AnchorStyles.Right | AnchorStyles.Top
            };
            closeButton.Click += (sender, e) =>
            {
                bool showNextTime = showNextTimeCheckBox.Checked;
                Properties.Settings.Default.ShowInfoDialog = showNextTime;
                Properties.Settings.Default.Save();
                infoForm.Close();
            };

            bottomPanel.Controls.Add(showNextTimeCheckBox);
            bottomPanel.Controls.Add(closeButton);

            infoForm.Controls.Add(webBrowser);
            infoForm.Controls.Add(bottomPanel);
            

            // Adjust closeButton position after form loads
            infoForm.Load += (s, e) =>
            {
                closeButton.Left = bottomPanel.Width - closeButton.Width - 20;
                closeButton.Focus();
            };
            infoForm.AcceptButton = closeButton;

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


