using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Tobasa
{
    public partial class FormServerConfig : Form
    {
        // Default options

        private readonly Options optionsDefault = new Options()
        {
            securitySalt = "C4BC3A3AC2D6D367A74580388B20BC069C96B048DFEAF5CCDC0CE1E25BF23F39",
            providerType = "SQLITE"
        };

        private readonly SqliteOptions sqliteOptionsDefault = new SqliteOptions()
        {
            dbFilePath  = "..\\Database\\antri.db3",
            connString  = "Data Source=..\\Database\\antri.db3;Version=3;"
        };

        private readonly SqlOptions sqlOptionsDefault = new SqlOptions()
        {
            hostAddr    = "127.0.0.1",
            tcpPort     = "1433",
            database    = "antri",
            username    = "antrian",
            password    = "TOBASA",
            passwordEnc = "ad7415644add93d6e719d2b593da6e6e",
            connString  = "Provider=SQLOLEDB;Data Source=127.0.0.1,1433;User ID=antrian;Initial Catalog=antri;"
        };

        private readonly QueOptions queOptionsDefault = new QueOptions()
        {
            hostAddr    = "127.0.0.1",
            tcpPort     = "2345",
            username    = "tobasaqueue",
            password    = "TOBASA",
            passwordEnc = "e4ee0e54215f5e3dd7683923abe1dac8"
        };

        private readonly Dictionary<string, ConfigFile> configFileDict = new Dictionary<string, ConfigFile>()
        {
            { "Admin",    new ConfigFile(){ name = "QueueAdmin",    path = ".\\QueueAdmin\\QueueAdmin.exe.config"  }        },
            { "Caller",   new ConfigFile(){ name = "QueueCaller",   path = ".\\QueueCaller\\QueueCaller.exe.config"  }      },
            { "Display",  new ConfigFile(){ name = "QueueDisplay",  path = ".\\QueueDisplay\\QueueDisplay.exe.config"  }    },
            { "Service",  new ConfigFile(){ name = "QueueService",  path = ".\\QueueService\\QueueService.exe.config"  }    },
            { "Ticket",   new ConfigFile(){ name = "QueueTicket",   path = ".\\QueueTicket\\QueueTicket.exe.config"  }      },
        };


        // User options
        private Options optionsUser;
        private SqliteOptions sqliteOptionsUser;
        private SqlOptions sqlOptionsUser;
        private QueOptions queOptionsUser;

        private bool startupcompleted = false;

        public FormServerConfig()
        {
            // set default security salt
            sqlOptionsDefault.securitySalt = optionsDefault.securitySalt;
            queOptionsDefault.securitySalt = optionsDefault.securitySalt;

            // user options gets default value first
            optionsUser       = optionsDefault;
            sqliteOptionsUser = sqliteOptionsDefault;
            sqlOptionsUser    = sqlOptionsDefault;
            queOptionsUser    = queOptionsDefault;

            InitializeComponent();

            // Database SQLite is default option
            rbSqlite.Checked    = true;         
            chkSqlUseDefault.Enabled = false;
            // Set form initial values from user options
            tbSecuritySalt.Text = optionsDefault.securitySalt;
            tbSqliteDbPath.Text = sqliteOptionsDefault.dbFilePath;
            SetFormValuesFromSqlOptionsDefault();
            SetFormValuesFromQueOptionsDefault();

            startupcompleted = true;
        }

        private void SetFormValuesFromSqlOptionsDefault()
        {
            tbSqlIPAddr.Text     = sqlOptionsDefault.hostAddr;
            tbSqlTcp.Text        = sqlOptionsDefault.tcpPort;
            tbSqlDatabase.Text   = sqlOptionsDefault.database;
            tbSqlUserName.Text   = sqlOptionsDefault.username;
            tbSqlPwd.Text        = sqlOptionsDefault.password;
            tbSqlPwdConfirm.Text = sqlOptionsDefault.password;

            if (startupcompleted)
            { 
                tbSqlPwd.BackColor          = Color.White;
                tbSqlPwdConfirm.BackColor   = Color.White;
            }
        }

        private void SetFormValuesFromQueOptionsDefault()
        {
            tbQueueIPAddr.Text     = queOptionsDefault.hostAddr;
            tbQueueTcp.Text        = queOptionsDefault.tcpPort;
            tbQueueUserName.Text   = queOptionsDefault.username;
            tbQueuePwd.Text        = queOptionsDefault.password;
            tbQueuePwdConfirm.Text = queOptionsDefault.password;

            if (startupcompleted)
            {
                tbQueuePwd.BackColor        = Color.White;
                tbQueuePwdConfirm.BackColor = Color.White;
            }
        }

        private void TransferFormValuesToSqlOptionsUser()
        {
            sqlOptionsUser.hostAddr = tbSqlIPAddr.Text;
            sqlOptionsUser.tcpPort  = tbSqlTcp.Text;
            sqlOptionsUser.database = tbSqlDatabase.Text;
            sqlOptionsUser.username = tbSqlUserName.Text;
            sqlOptionsUser.password = tbSqlPwd.Text;
            
            sqlOptionsUser.securitySalt = optionsUser.securitySalt;
        }

        private void TransferFormValuesToQueOptionsUser()
        {
            queOptionsUser.hostAddr     = tbQueueIPAddr.Text;
            queOptionsUser.tcpPort      = tbQueueTcp.Text;
            queOptionsUser.username     = tbQueueUserName.Text;
            queOptionsUser.password     = tbQueuePwd.Text;
            queOptionsUser.securitySalt = optionsUser.securitySalt;
        }

        private bool ValidateSecuritySalt()
        {
            if (chkUseSecuritySaltDefault.Checked)
                return true;

            if(string.IsNullOrWhiteSpace(tbSecuritySalt.Text))
            {
                tbSecuritySalt.BackColor = Color.LightPink;
                return false;
            }
            else
            {
                tbSecuritySalt.BackColor = Color.LightGreen;
                return true;
            }
        }

        private bool ValidateSqliteDb()
        {
            if (rbSqlserver.Checked)
                return true;

            if(rbSqlite.Checked)
            {
                if (chkSqliteUseDefault.Checked)
                    return true;

                if (string.IsNullOrWhiteSpace(tbSqliteDbPath.Text))
                {
                    tbSqliteDbPath.BackColor = Color.LightPink;
                    return false;
                }
                else
                {
                    tbSqliteDbPath.BackColor = Color.LightGreen;
                    return true;
                }
            }

            return false;
        }

        private bool ValidateSqlPasswordInput()
        {
            if (rbSqlserver.Checked)
            {
                if (chkSqlUseDefault.Checked)
                    return true;

                if (tbSqlPwd.Text == tbSqlPwdConfirm.Text)
                {
                    tbSqlPwd.BackColor = Color.LightGreen;
                    tbSqlPwdConfirm.BackColor = Color.LightGreen;
                    return true;
                }
                else
                {
                    tbSqlPwd.BackColor = Color.LightPink;
                    tbSqlPwdConfirm.BackColor = Color.LightPink;
                    return false;
                }
            }
            else
            {
                //if (chkSqliteUseDefault.Checked)
                    return true;
            }
        }

        private bool ValidateQueuePasswordInput()
        {
            if (tbQueuePwd.Text == tbQueuePwdConfirm.Text)
            {
                tbQueuePwd.BackColor = Color.LightGreen;
                tbQueuePwdConfirm.BackColor = Color.LightGreen;
                return true;
            }
            else
            {
                tbQueuePwd.BackColor = Color.LightPink;
                tbQueuePwdConfirm.BackColor = Color.LightPink;
                return false;
            }
        }

        private bool ValidateTobasaModules()
        {
            string moduleNames = "";
            foreach (KeyValuePair<string, ConfigFile> kv in configFileDict)
            {
                ConfigFile cfg = kv.Value;
                try
                {
                    if( !File.Exists(cfg.path))
                    {
                        if (moduleNames.Length > 0)
                            moduleNames += ", ";

                        moduleNames += cfg.name;
                    }
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message, ex.GetType().Name);
                }
            }

            if (moduleNames.Length == 0)
                return true;
            else
            {
                MessageBox.Show($"Module {moduleNames} tidak ditemukan. Tool ini harus dijalankan pada folder bundling Software Antrian Tobasa",
                    "Informasi",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                return false;
            }
        }

        private void OnApplyConfig(object sender, EventArgs e)
        {
            if( !ValidateTobasaModules())
            {
                return;
            }


            if (!ValidateSecuritySalt())
            {
                MessageBox.Show("Please check security salt input", "Security salt invalid");
                return;
            }

            if (!ValidateSqliteDb())
            {
                MessageBox.Show("Please check SQLite Database input", "SQLite Database invalid");
                return;
            }

            if (!ValidateSqlPasswordInput())
            {
                MessageBox.Show("Please check SQL Server password input", "Password does not match");
                return;
            }

            if (!ValidateQueuePasswordInput())
            {
                MessageBox.Show("Please check QueueServer password input", "Password does not match");
                return;
            }


            DialogResult dlgResult = MessageBox.Show("Apply configuration?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dlgResult != DialogResult.Yes)
            {
                return;
            }

            if (rbSqlserver.Checked)
                optionsUser.providerType = "OLEDB";
            else
                optionsUser.providerType = "SQLITE";

            if (chkUseSecuritySaltDefault.Checked)
                optionsUser.securitySalt = optionsDefault.securitySalt;
            else
                optionsUser.securitySalt = tbSecuritySalt.Text;

            if (chkSqliteUseDefault.Checked)
                sqliteOptionsUser = sqliteOptionsDefault;
            else
                sqliteOptionsUser.dbFilePath = tbSqliteDbPath.Text;

            if (chkSqlUseDefault.Checked)
                sqlOptionsUser = sqlOptionsDefault;
            else
                TransferFormValuesToSqlOptionsUser();

            if (chkQueueUseDefault.Checked)
                queOptionsUser = queOptionsDefault;
            else
                TransferFormValuesToQueOptionsUser();


            foreach (KeyValuePair<string, ConfigFile> kv in configFileDict)
            {
                ConfigFile cfg = kv.Value;

                try
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(cfg.path);

                    XmlNode connStrNode = xmlDoc.DocumentElement.SelectSingleNode("/configuration/connectionStrings");
                    if (connStrNode != null)
                    {
                        var child = connStrNode.FirstChild;
                        if (child != null)
                        {
                            var attribut = child.Attributes.GetNamedItem("connectionString");
                            if (attribut != null)
                            {
                                if (rbSqlserver.Checked)
                                    attribut.Value = sqlOptionsUser.connString;
                                else
                                    attribut.Value = sqliteOptionsUser.connString;
                            }
                        }
                    }

                    XmlNode tobasaSettings = xmlDoc.DocumentElement.SelectSingleNode("/configuration/userSettings/Tobasa.Properties.Settings");
                    foreach (XmlNode node in tobasaSettings.ChildNodes)
                    {
                        var settingName = node.Attributes.GetNamedItem("name");

                        // Security salt
                        if (settingName != null && settingName.Value == "SecuritySalt")
                        {
                            var settingVal = node.FirstChild;
                            settingVal.InnerText = optionsUser.securitySalt;
                        }

                        // Queueserver connection info
                        if (settingName != null && settingName.Value == "QueueServerHost")
                        {
                            var settingVal = node.FirstChild;
                            settingVal.InnerText = queOptionsUser.hostAddr;
                        }
                        if (settingName != null && settingName.Value == "QueueServerPort")
                        {
                            var settingVal = node.FirstChild;
                            settingVal.InnerText = queOptionsUser.tcpPort;
                        }
                        if (settingName != null && settingName.Value == "QueueUserName")
                        {
                            var settingVal = node.FirstChild;
                            settingVal.InnerText = queOptionsUser.username;
                        }
                        if (settingName != null && settingName.Value == "QueuePassword")
                        {
                            var settingVal = node.FirstChild;
                            settingVal.InnerText = queOptionsUser.passwordEnc;
                        }

                        // SQL Server connection password and provider
                        if (settingName != null && settingName.Value == "ConnectionStringPassword")
                        {
                            var settingVal = node.FirstChild;

                            if (rbSqlserver.Checked)
                                settingVal.InnerText = sqlOptionsUser.passwordEnc;
                            else
                                settingVal.InnerText = "";
                        }
                        if (settingName != null && settingName.Value == "ProviderType")
                        {
                            var settingVal = node.FirstChild;

                            settingVal.InnerText = optionsUser.providerType;
                        }
                    }

                    xmlDoc.Save(cfg.path);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.GetType().Name);
                }

            }

            MessageBox.Show("Configurations applied", "Information");
          
        }

        private void OnUseSecuritySaltDefault(object sender, EventArgs e)
        {
            if(chkUseSecuritySaltDefault.Checked)
            {
                tbSecuritySalt.Text      = optionsDefault.securitySalt;
                tbSecuritySalt.BackColor = Color.White;
            }

            tbSecuritySalt.Enabled = !chkUseSecuritySaltDefault.Checked;

        }

        private void OnSqliteUseDefault(object sender, EventArgs e)
        {
            if (chkSqliteUseDefault.Checked)
            {
                tbSqliteDbPath.Text      = sqliteOptionsDefault.dbFilePath;
                tbSqliteDbPath.BackColor = Color.White;
            }

            tbSqliteDbPath.Enabled    = !chkSqliteUseDefault.Checked;
            btnBrowseSqliteDb.Enabled = !chkSqliteUseDefault.Checked;
        }

        private void OnSqlUseDefault(object sender, EventArgs e)
        {
            if( chkSqlUseDefault.Checked)
                SetFormValuesFromSqlOptionsDefault();

            tbSqlIPAddr.Enabled     = !chkSqlUseDefault.Checked;
            tbSqlTcp.Enabled        = !chkSqlUseDefault.Checked;
            tbSqlDatabase.Enabled   = !chkSqlUseDefault.Checked;
            tbSqlUserName.Enabled   = !chkSqlUseDefault.Checked;
            tbSqlPwd.Enabled        = !chkSqlUseDefault.Checked;
            tbSqlPwdConfirm.Enabled = !chkSqlUseDefault.Checked;
        }


        private void OnQueueUseDefault(object sender, EventArgs e)
        {
            if(chkQueueUseDefault.Checked)
                SetFormValuesFromQueOptionsDefault();

            tbQueueIPAddr.Enabled     = !chkQueueUseDefault.Checked;
            tbQueueTcp.Enabled        = !chkQueueUseDefault.Checked;
            tbQueueUserName.Enabled   = !chkQueueUseDefault.Checked;
            tbQueuePwd.Enabled        = !chkQueueUseDefault.Checked;
            tbQueuePwdConfirm.Enabled = !chkQueueUseDefault.Checked;
        }

        private void OnBrowseSqliteDb(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            fileDlg.InitialDirectory = Util.ProcessDir;
            fileDlg.Filter = "SQLite3 Database(*.db3;*.DB3)|*.db3;*.DB3|All files (*.*)|*.*";

            DialogResult result = fileDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbSqliteDbPath.Text = fileDlg.FileName;
            }
        }

        private void OnDBTypeCheckedChanged(object sender, EventArgs e)
        {
            var radio = (RadioButton)sender;

            if (radio == rbSqlserver )
            {
                chkSqlUseDefault.Enabled = rbSqlserver.Checked;
            }

            if (radio == rbSqlite)
            {
                chkSqliteUseDefault.Enabled = rbSqlite.Checked;
            }
        }
    }
}
