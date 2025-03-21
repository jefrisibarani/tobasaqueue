using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

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
            dbFilePath = ".\\Database\\antri.db3",
            connString = "Data Source=.\\Database\\antri.db3;Version=3;"
        };

        private readonly SqlOptions mysqlOptionsDefault = new SqlOptions()
        {
            hostAddr = "127.0.0.1",
            tcpPort = "3306",
            database = "antri",
            username = "antrian",
            password = "TOBASA",
            passwordEnc = "ad7415644add93d6e719d2b593da6e6e",
            connString = "Data Source=127.0.0.1,3306;User ID=antrian;Initial Catalog=antri;"
        };

        private readonly SqlOptions mssqlOptionsDefault = new SqlOptions()
        {
            hostAddr = "127.0.0.1",
            tcpPort = "1433",
            database = "antri",
            username = "antrian",
            password = "TOBASA",
            passwordEnc = "ad7415644add93d6e719d2b593da6e6e",
            connString = "Provider=SQLOLEDB;Data Source=127.0.0.1,1433;User ID=antrian;Initial Catalog=antri;"
        };

        private readonly QueOptions queOptionsDefault = new QueOptions()
        {
            hostAddr = "127.0.0.1",
            tcpPort = "2345",
            username = "tobasaqueue",
            password = "TOBASA",
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

        private Dictionary<string, PostConfigDisplay> displayPostConfigs = DefaultData.CreatePostConfigDisplayMap();
        private Dictionary<string, PostConfigTicket> ticketPostConfigs = DefaultData.CreatePostConfigTicketMap();
        private Dictionary<string, PostConfigCaller> callerPostConfigs = DefaultData.CreatePostConfigCallerMap();

        private string _currentPostConfigCode = "";
        List<Button> _btnPostConfigList = new List<Button>();
        public FormServerConfig()
        {
            // set default security salt
            mysqlOptionsDefault.securitySalt = optionsDefault.securitySalt;
            mssqlOptionsDefault.securitySalt = optionsDefault.securitySalt;
            queOptionsDefault.securitySalt = optionsDefault.securitySalt;

            // user options gets default value first
            optionsUser = optionsDefault;
            sqliteOptionsUser = sqliteOptionsDefault;
            sqlOptionsUser = mssqlOptionsDefault;
            queOptionsUser = queOptionsDefault;

            InitializeComponent();

            // Database SQLite is default option
            rbSQLITE.Checked = true;
            chkSqlUseDefault.Enabled = false;
            // Set form initial values from user options
            tbSecuritySalt.Text = optionsDefault.securitySalt;
            tbSqliteDbPath.Text = sqliteOptionsDefault.dbFilePath;
            SetFormValuesFromSqlOptionsDefault();
            SetFormValuesFromQueOptionsDefault();

            _btnPostConfigList.Add(btnPost0);
            _btnPostConfigList.Add(btnPost1);
            _btnPostConfigList.Add(btnPost2);
            _btnPostConfigList.Add(btnPost3);
            _btnPostConfigList.Add(btnPost4);
            _btnPostConfigList.Add(btnPost5);
            _btnPostConfigList.Add(btnPost6);
            _btnPostConfigList.Add(btnPost7);
            _btnPostConfigList.Add(btnPost8);
            _btnPostConfigList.Add(btnPost9);



            SetFormValuesForPost("POST0");
            _currentPostConfigCode = "POST0";
            btnPost0.BackColor = Color.LightGreen;

            startupcompleted = true;
        }

        private void SetFormValuesFromSqlOptionsDefault()
        {
            tbSqlIPAddr.Text = mssqlOptionsDefault.hostAddr;
            tbSqlTcp.Text = mssqlOptionsDefault.tcpPort;
            tbSqlDatabase.Text = mssqlOptionsDefault.database;
            tbSqlUserName.Text = mssqlOptionsDefault.username;
            tbSqlPwd.Text = mssqlOptionsDefault.password;
            tbSqlPwdConfirm.Text = mssqlOptionsDefault.password;

            if (startupcompleted)
            {
                tbSqlPwd.BackColor = Color.White;
                tbSqlPwdConfirm.BackColor = Color.White;
            }
        }

        private void SetFormValuesFromQueOptionsDefault()
        {
            tbQueueIPAddr.Text = queOptionsDefault.hostAddr;
            tbQueueTcp.Text = queOptionsDefault.tcpPort;
            tbQueueUserName.Text = queOptionsDefault.username;
            tbQueuePwd.Text = queOptionsDefault.password;
            tbQueuePwdConfirm.Text = queOptionsDefault.password;

            if (startupcompleted)
            {
                tbQueuePwd.BackColor = Color.White;
                tbQueuePwdConfirm.BackColor = Color.White;
            }
        }

        private void TransferFormValuesToSqlOptionsUser()
        {
            if (rbMSSQL.Checked)
                sqlOptionsUser.providerType = "MSSQL";

            if (rbMYSQL.Checked)
                sqlOptionsUser.providerType = "MYSQL";

            sqlOptionsUser.hostAddr = tbSqlIPAddr.Text;
            sqlOptionsUser.tcpPort = tbSqlTcp.Text;
            sqlOptionsUser.database = tbSqlDatabase.Text;
            sqlOptionsUser.username = tbSqlUserName.Text;
            sqlOptionsUser.password = tbSqlPwd.Text;

            sqlOptionsUser.securitySalt = optionsUser.securitySalt;
        }

        private void TransferFormValuesToQueOptionsUser()
        {
            queOptionsUser.hostAddr = tbQueueIPAddr.Text;
            queOptionsUser.tcpPort = tbQueueTcp.Text;
            queOptionsUser.username = tbQueueUserName.Text;
            queOptionsUser.password = tbQueuePwd.Text;
            queOptionsUser.securitySalt = optionsUser.securitySalt;
        }

        private bool ValidateSecuritySalt()
        {
            if (chkUseSecuritySaltDefault.Checked)
                return true;

            if (string.IsNullOrWhiteSpace(tbSecuritySalt.Text))
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
            if (rbMSSQL.Checked || rbMYSQL.Checked)
                return true;

            if (rbSQLITE.Checked)
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
            if (rbMSSQL.Checked || rbMYSQL.Checked)
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
                    if (!File.Exists(cfg.path))
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
                    "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }
        }

        private void OnApplyConfig(object sender, EventArgs e)
        {
            if (!ValidateTobasaModules())
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

            if (rbMSSQL.Checked)
                optionsUser.providerType = "MSSQL";
            else if (rbMYSQL.Checked)
                optionsUser.providerType = "MYSQL";
            else if (rbSQLITE.Checked)
                optionsUser.providerType = "SQLITE";


            if (chkUseSecuritySaltDefault.Checked)
                optionsUser.securitySalt = optionsDefault.securitySalt;
            else
                optionsUser.securitySalt = tbSecuritySalt.Text;


            if (chkSqliteUseDefault.Checked)
                sqliteOptionsUser = sqliteOptionsDefault;
            else
                sqliteOptionsUser.dbFilePath = tbSqliteDbPath.Text;


            if (chkSqlUseDefault.Checked && rbMSSQL.Checked)
                sqlOptionsUser = mssqlOptionsDefault;
            else if (chkSqlUseDefault.Checked && rbMYSQL.Checked)
                sqlOptionsUser = mysqlOptionsDefault;
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
                        var node = connStrNode.FirstChild;
                        while (node != null)
                        {
                            var attrName = node.Attributes.GetNamedItem("name");
                            var attribut = node.Attributes.GetNamedItem("connectionString");
                            if (attrName != null && attrName.Value == "Tobasa.Properties.Settings.ConnectionString_MSSQL")
                            {
                                if (attribut != null && rbMSSQL.Checked)
                                {
                                    attribut.Value = sqlOptionsUser.connString;
                                    break;
                                }
                            }
                            else if (attrName != null && attrName.Value == "Tobasa.Properties.Settings.ConnectionString_SQLITE")
                            {
                                if (attribut != null && rbSQLITE.Checked)
                                {
                                    attribut.Value = sqliteOptionsUser.connString;
                                    break;
                                }
                            }
                            else if (attrName != null && attrName.Value == "Tobasa.Properties.Settings.ConnectionString_MYSQL")
                            {
                                if (attribut != null && rbMYSQL.Checked)
                                {
                                    attribut.Value = sqlOptionsUser.connString;
                                    break;
                                }
                            }

                            node = node.NextSibling;
                        }
                    }


                    // deal with applicationSettings first
                    XmlNode appSettingsNode  = xmlDoc.DocumentElement.SelectSingleNode("/configuration/applicationSettings/Tobasa.Properties.Settings");
                    if (appSettingsNode != null)
                    {
                        foreach (XmlNode node in appSettingsNode.ChildNodes)
                        {
                            var settingName = node.Attributes.GetNamedItem("name");

                            // Security salt
                            if (settingName != null && settingName.Value == "SecuritySalt")
                            {
                                var target = node.FirstChild;
                                target.InnerText = optionsUser.securitySalt;
                            }

                            // UI Post List
                            if (cfg.name == "QueueCaller" && settingName != null && settingName.Value == "UIPostList")
                            {
                                var valueNode = node.SelectSingleNode("value");
                                var arrayNode = valueNode.SelectSingleNode("ArrayOfString");
                                arrayNode.RemoveAll();

                                foreach (var item in callerPostConfigs)
                                {
                                    var newElement = xmlDoc.CreateElement("string");
                                    newElement.InnerText = $"{item.Key}|{item.Value.caption}";
                                    arrayNode.AppendChild(newElement);
                                }
                            }

                            // QueueService => Listen Port
                            if (cfg.name == "QueueService" && settingName != null && settingName.Value == "ListenPort")
                            {
                                var target = node.FirstChild;
                                target.InnerText = queOptionsUser.tcpPort;
                            }
                            if (cfg.name == "QueueService" && settingName != null && settingName.Value == "ProviderType")
                            {
                                var target = node.FirstChild;
                                target.InnerText = optionsUser.providerType;
                            }
                            // QueueService => SQL Server connection password and provider
                            if (cfg.name == "QueueService" && settingName != null && settingName.Value == "ConnectionStringPassword")
                            {
                                var target = node.FirstChild;

                                if (rbMSSQL.Checked || rbMYSQL.Checked)
                                    target.InnerText = sqlOptionsUser.passwordEnc;
                                else
                                    target.InnerText = "";
                            }
                        }
                    }


                    // Deal with userSettings
                    XmlNode userSettingsNode = xmlDoc.DocumentElement.SelectSingleNode("/configuration/userSettings/Tobasa.Properties.Settings");
                    if (userSettingsNode != null)
                    {
                        foreach (XmlNode node in userSettingsNode.ChildNodes)
                        {
                            var settingName = node.Attributes.GetNamedItem("name");

                            // Queueserver connection info
                            if (settingName != null && settingName.Value == "QueueServerHost")
                            {
                                var target = node.FirstChild;
                                target.InnerText = queOptionsUser.hostAddr;
                            }
                            if (settingName != null && settingName.Value == "QueueServerPort")
                            {
                                var target = node.FirstChild;
                                target.InnerText = queOptionsUser.tcpPort;
                            }
                            if (settingName != null && settingName.Value == "QueueUserName")
                            {
                                var target = node.FirstChild;
                                target.InnerText = queOptionsUser.username;
                            }
                            if (settingName != null && settingName.Value == "QueuePassword")
                            {
                                var target = node.FirstChild;
                                target.InnerText = queOptionsUser.passwordEnc;
                            }

                            if (cfg.name == "QueueDisplay" && settingName != null)
                            {
                                if (settingName.Value == "Post0Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST0"].caption;
                                if (settingName.Value == "Post0Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST0"].name;
                                if (settingName.Value == "Post0RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST0"].infoText;
                                if (settingName.Value == "Post0Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST0"].visible ? "True" : "False";
                                if (settingName.Value == "Post0PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST0"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post1Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST1"].caption;
                                if (settingName.Value == "Post1Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST1"].name;
                                if (settingName.Value == "Post1RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST1"].infoText;
                                if (settingName.Value == "Post1Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST1"].visible ? "True" : "False";
                                if (settingName.Value == "Post1PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST1"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post2Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST2"].caption;
                                if (settingName.Value == "Post2Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST2"].name;
                                if (settingName.Value == "Post2RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST2"].infoText;
                                if (settingName.Value == "Post2Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST2"].visible ? "True" : "False";
                                if (settingName.Value == "Post2PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST2"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post3Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST3"].caption;
                                if (settingName.Value == "Post3Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST3"].name;
                                if (settingName.Value == "Post3RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST3"].infoText;
                                if (settingName.Value == "Post3Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST3"].visible ? "True" : "False";
                                if (settingName.Value == "Post3PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST3"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post4Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST4"].caption;
                                if (settingName.Value == "Post4Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST4"].name;
                                if (settingName.Value == "Post4RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST4"].infoText;
                                if (settingName.Value == "Post4Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST4"].visible ? "True" : "False";
                                if (settingName.Value == "Post4PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST4"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post5Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST5"].caption;
                                if (settingName.Value == "Post5Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST5"].name;
                                if (settingName.Value == "Post5RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST5"].infoText;
                                if (settingName.Value == "Post5Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST5"].visible ? "True" : "False";
                                if (settingName.Value == "Post5PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST5"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post6Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST6"].caption;
                                if (settingName.Value == "Post6Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST6"].name;
                                if (settingName.Value == "Post6RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST6"].infoText;
                                if (settingName.Value == "Post6Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST6"].visible ? "True" : "False";
                                if (settingName.Value == "Post6PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST6"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post7Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST7"].caption;
                                if (settingName.Value == "Post7Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST7"].name;
                                if (settingName.Value == "Post7RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST7"].infoText;
                                if (settingName.Value == "Post7Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST7"].visible ? "True" : "False";
                                if (settingName.Value == "Post7PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST7"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post8Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST8"].caption;
                                if (settingName.Value == "Post8Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST8"].name;
                                if (settingName.Value == "Post8RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST8"].infoText;
                                if (settingName.Value == "Post8Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST8"].visible ? "True" : "False";
                                if (settingName.Value == "Post8PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST8"].playAudio ? "True" : "False";

                                if (settingName.Value == "Post9Caption")
                                    node.FirstChild.InnerText = displayPostConfigs["POST9"].caption;
                                if (settingName.Value == "Post9Name")
                                    node.FirstChild.InnerText = displayPostConfigs["POST9"].name;
                                if (settingName.Value == "Post9RunText")
                                    node.FirstChild.InnerText = displayPostConfigs["POST9"].infoText;
                                if (settingName.Value == "Post9Visible")
                                    node.FirstChild.InnerText = displayPostConfigs["POST9"].visible ? "True" : "False";
                                if (settingName.Value == "Post9PlayAudio")
                                    node.FirstChild.InnerText = displayPostConfigs["POST9"].playAudio ? "True" : "False";
                            }

                            if (cfg.name == "QueueTicket" && settingName != null)
                            {
                                if (settingName.Value == "Post0Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST0"].caption;
                                if (settingName.Value == "Post0Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST0"].name;
                                if (settingName.Value == "Post0PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST0"].ticketHeader;
                                if (settingName.Value == "Post0PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST0"].printCopies.ToString();
                                if (settingName.Value == "Post0Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST0"].visible ? "True" : "False";
                                if (settingName.Value == "Post0Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST0"].enabled ? "True" : "False";

                                if (settingName.Value == "Post1Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST1"].caption;
                                if (settingName.Value == "Post1Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST1"].name;
                                if (settingName.Value == "Post1PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST1"].ticketHeader;
                                if (settingName.Value == "Post1PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST1"].printCopies.ToString();
                                if (settingName.Value == "Post1Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST1"].visible ? "True" : "False";
                                if (settingName.Value == "Post1Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST1"].enabled ? "True" : "False";

                                if (settingName.Value == "Post2Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST2"].caption;
                                if (settingName.Value == "Post2Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST2"].name;
                                if (settingName.Value == "Post2PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST2"].ticketHeader;
                                if (settingName.Value == "Post2PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST2"].printCopies.ToString();
                                if (settingName.Value == "Post2Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST2"].visible ? "True" : "False";
                                if (settingName.Value == "Post2Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST2"].enabled ? "True" : "False";

                                if (settingName.Value == "Post3Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST3"].caption;
                                if (settingName.Value == "Post3Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST3"].name;
                                if (settingName.Value == "Post3PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST3"].ticketHeader;
                                if (settingName.Value == "Post3PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST3"].printCopies.ToString();
                                if (settingName.Value == "Post3Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST3"].visible ? "True" : "False";
                                if (settingName.Value == "Post3Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST3"].enabled ? "True" : "False";

                                if (settingName.Value == "Post4Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST4"].caption;
                                if (settingName.Value == "Post4Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST4"].name;
                                if (settingName.Value == "Post4PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST4"].ticketHeader;
                                if (settingName.Value == "Post4PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST4"].printCopies.ToString();
                                if (settingName.Value == "Post4Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST4"].visible ? "True" : "False";
                                if (settingName.Value == "Post4Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST4"].enabled ? "True" : "False";

                                if (settingName.Value == "Post5Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST5"].caption;
                                if (settingName.Value == "Post5Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST5"].name;
                                if (settingName.Value == "Post5PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST5"].ticketHeader;
                                if (settingName.Value == "Post5PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST5"].printCopies.ToString();
                                if (settingName.Value == "Post5Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST5"].visible ? "True" : "False";
                                if (settingName.Value == "Post5Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST5"].enabled ? "True" : "False";

                                if (settingName.Value == "Post6Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST6"].caption;
                                if (settingName.Value == "Post6Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST6"].name;
                                if (settingName.Value == "Post6PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST6"].ticketHeader;
                                if (settingName.Value == "Post6PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST6"].printCopies.ToString();
                                if (settingName.Value == "Post6Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST6"].visible ? "True" : "False";
                                if (settingName.Value == "Post6Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST6"].enabled ? "True" : "False";

                                if (settingName.Value == "Post7Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST7"].caption;
                                if (settingName.Value == "Post7Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST7"].name;
                                if (settingName.Value == "Post7PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST7"].ticketHeader;
                                if (settingName.Value == "Post7PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST7"].printCopies.ToString();
                                if (settingName.Value == "Post7Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST7"].visible ? "True" : "False";
                                if (settingName.Value == "Post7Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST7"].enabled ? "True" : "False";

                                if (settingName.Value == "Post8Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST8"].caption;
                                if (settingName.Value == "Post8Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST8"].name;
                                if (settingName.Value == "Post8PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST8"].ticketHeader;
                                if (settingName.Value == "Post8PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST8"].printCopies.ToString();
                                if (settingName.Value == "Post8Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST8"].visible ? "True" : "False";
                                if (settingName.Value == "Post8Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST8"].enabled ? "True" : "False";

                                if (settingName.Value == "Post9Caption")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST9"].caption;
                                if (settingName.Value == "Post9Name")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST9"].name;
                                if (settingName.Value == "Post9PrintHeader")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST9"].ticketHeader;
                                if (settingName.Value == "Post9PrintCopies")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST9"].printCopies.ToString();
                                if (settingName.Value == "Post9Visible")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST9"].visible ? "True" : "False";
                                if (settingName.Value == "Post9Enabled")
                                    node.FirstChild.InnerText = ticketPostConfigs["POST9"].enabled ? "True" : "False";
                            }

                            if (cfg.name == "QueueCaller" && settingName != null)
                            {
                                //callerPostConfigs
                            }
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
            if (chkUseSecuritySaltDefault.Checked)
            {
                tbSecuritySalt.Text = optionsDefault.securitySalt;
                tbSecuritySalt.BackColor = Color.White;
            }

            tbSecuritySalt.Enabled = !chkUseSecuritySaltDefault.Checked;

        }

        private void OnSqliteUseDefault(object sender, EventArgs e)
        {
            if (chkSqliteUseDefault.Checked)
            {
                tbSqliteDbPath.Text = sqliteOptionsDefault.dbFilePath;
                tbSqliteDbPath.BackColor = Color.White;
            }

            tbSqliteDbPath.Enabled = !chkSqliteUseDefault.Checked;
            btnBrowseSqliteDb.Enabled = !chkSqliteUseDefault.Checked;
        }

        private void OnSqlUseDefault(object sender, EventArgs e)
        {
            if (chkSqlUseDefault.Checked)
                SetFormValuesFromSqlOptionsDefault();

            tbSqlIPAddr.Enabled = !chkSqlUseDefault.Checked;
            tbSqlTcp.Enabled = !chkSqlUseDefault.Checked;
            tbSqlDatabase.Enabled = !chkSqlUseDefault.Checked;
            tbSqlUserName.Enabled = !chkSqlUseDefault.Checked;
            tbSqlPwd.Enabled = !chkSqlUseDefault.Checked;
            tbSqlPwdConfirm.Enabled = !chkSqlUseDefault.Checked;
        }

        private void OnQueueUseDefault(object sender, EventArgs e)
        {
            if (chkQueueUseDefault.Checked)
                SetFormValuesFromQueOptionsDefault();

            tbQueueIPAddr.Enabled = !chkQueueUseDefault.Checked;
            tbQueueTcp.Enabled = !chkQueueUseDefault.Checked;
            tbQueueUserName.Enabled = !chkQueueUseDefault.Checked;
            tbQueuePwd.Enabled = !chkQueueUseDefault.Checked;
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

            if (radio == rbMSSQL)
            {
                if (rbMSSQL.Checked)
                    groupBoxSqlServer.Text = "Microsoft SQL Server Connnection Info";

                chkSqlUseDefault.Enabled = rbMSSQL.Checked;
                SetDefaultSqlServerOptionForm("MSSQL");
            }
            else if (radio == rbMYSQL)
            {
                if (rbMYSQL.Checked)
                    groupBoxSqlServer.Text = "MySql Server Connnection Info";

                chkSqlUseDefault.Enabled = rbMYSQL.Checked;
                SetDefaultSqlServerOptionForm("MYSQL");
            }
            else if (radio == rbSQLITE)
            {
                chkSqliteUseDefault.Enabled = rbSQLITE.Checked;
            }
        }

        private void SetDefaultSqlServerOptionForm(string dbtype)
        {
            if (dbtype == "MYSQL")
            {
                tbSqlIPAddr.Text = mysqlOptionsDefault.hostAddr;
                tbSqlTcp.Text = mysqlOptionsDefault.tcpPort;
                tbSqlDatabase.Text = mysqlOptionsDefault.database;
                tbSqlUserName.Text = mysqlOptionsDefault.username;
                tbSqlPwd.Text = mysqlOptionsDefault.password;
                tbSqlPwdConfirm.Text = mysqlOptionsDefault.password;
            }
            else if (dbtype == "MSSQL")
            {
                tbSqlIPAddr.Text = mssqlOptionsDefault.hostAddr;
                tbSqlTcp.Text = mssqlOptionsDefault.tcpPort;
                tbSqlDatabase.Text = mssqlOptionsDefault.database;
                tbSqlUserName.Text = mssqlOptionsDefault.username;
                tbSqlPwd.Text = mssqlOptionsDefault.password;
                tbSqlPwdConfirm.Text = mssqlOptionsDefault.password;
            }
        }

        private void OnBtnPostClick(object sender, EventArgs e)
        {
            NormalizeBtnPostSettingColor((Button)sender);

            if (sender == btnPost0)
            {
                _currentPostConfigCode = "POST0";
                SetFormValuesForPost("POST0");
            }
            else if (sender == btnPost1)
            {
                _currentPostConfigCode = "POST1";
                SetFormValuesForPost("POST1");
            }
            else if (sender == btnPost2)
            {
                _currentPostConfigCode = "POST2";
                SetFormValuesForPost("POST2");
            }
            else if (sender == btnPost3)
            {
                _currentPostConfigCode = "POST3";
                SetFormValuesForPost("POST3");
            }
            else if (sender == btnPost4)
            {
                _currentPostConfigCode = "POST4";
                SetFormValuesForPost("POST4");
            }
            else if (sender == btnPost5)
            {
                _currentPostConfigCode = "POST5";
                SetFormValuesForPost("POST5");
            }
            else if (sender == btnPost6)
            {
                _currentPostConfigCode = "POST6";
                SetFormValuesForPost("POST6");
            }
            else if (sender == btnPost7)
            {
                _currentPostConfigCode = "POST7";
                SetFormValuesForPost("POST7");
            }
            else if (sender == btnPost8)
            {
                _currentPostConfigCode = "POST8";
                SetFormValuesForPost("POST8");
            }
            else if (sender == btnPost9)
            {
                _currentPostConfigCode = "POST9";
                SetFormValuesForPost("POST9");
            }
        }

        private void SetFormValuesForPost(string postCode)
        {
            displayPostNameTb.Text = displayPostConfigs[postCode].name;
            displayPostCaptionTb.Text = displayPostConfigs[postCode].caption;
            displayInfoTextTb.Text = displayPostConfigs[postCode].infoText;
            displayPostVisibleChk.Checked = displayPostConfigs[postCode].visible;
            displayPostPlayAudioChk.Checked = displayPostConfigs[postCode].playAudio;

            ticketPostNameTb.Text = ticketPostConfigs[postCode].name;
            ticketPostCaptionTb.Text = ticketPostConfigs[postCode].caption;
            ticketPostTicketHeaderTb.Text = ticketPostConfigs[postCode].ticketHeader;
            ticketPostVisible.Checked = ticketPostConfigs[postCode].visible;
            ticketPostEnabled.Checked = ticketPostConfigs[postCode].enabled;
            ticketPostPrintCopies.Value = ticketPostConfigs[postCode].printCopies;

            callerPostNameTb.Text = callerPostConfigs[postCode].name;
            callerPostCaptionTb.Text = callerPostConfigs[postCode].caption;

            if (postCode == "POST0" || postCode == "POST1" || postCode == "POST2" ||
                postCode == "POST5" || postCode == "POST6" || postCode == "POST7")
            {
                displayPostVisibleChk.Enabled = false;
                ticketPostVisible.Enabled = false;
            }
            else
            {
                displayPostVisibleChk.Enabled = true;
                ticketPostVisible.Enabled = true;
            }
        }

        private void OnTextChanged(object sender, EventArgs e)
        {

        }

        private void OnFocusLeaveFromTextBox(object sender, EventArgs e)
        {
            if (sender == displayPostNameTb)
            {
                displayPostConfigs[_currentPostConfigCode].name = displayPostNameTb.Text;
            }
            else if (sender == displayPostCaptionTb) 
            {
                displayPostConfigs[_currentPostConfigCode].caption = displayPostCaptionTb.Text;
            }
            else if (sender == displayInfoTextTb)
            {
                displayPostConfigs[_currentPostConfigCode].infoText = displayInfoTextTb.Text;
            }
            else if (sender == ticketPostNameTb)
            {
                ticketPostConfigs[_currentPostConfigCode].name = ticketPostNameTb.Text;
            }
            else if (sender == ticketPostCaptionTb)
            {
                ticketPostConfigs[_currentPostConfigCode].caption = ticketPostCaptionTb.Text;
            }
            else if (sender == ticketPostTicketHeaderTb)
            {
                ticketPostConfigs[_currentPostConfigCode].ticketHeader = ticketPostTicketHeaderTb.Text;
            }
            else if (sender == callerPostNameTb)
            {
                callerPostConfigs[_currentPostConfigCode].name = callerPostNameTb.Text;
            }
            else if (sender == callerPostCaptionTb)
            {
                callerPostConfigs[_currentPostConfigCode].caption = callerPostCaptionTb.Text;
            }
        }

        private void OnPostCheckBoxClick(object sender, EventArgs e)
        {
            if (sender == displayPostVisibleChk)
            {
                displayPostConfigs[_currentPostConfigCode].visible = displayPostVisibleChk.Checked;
            }
            else if (sender == displayPostPlayAudioChk)
            {
                displayPostConfigs[_currentPostConfigCode].playAudio = displayPostPlayAudioChk.Checked;
            }
            else if (sender == ticketPostVisible)
            {
                ticketPostConfigs[_currentPostConfigCode].visible = ticketPostVisible.Checked;
            }
            else if (sender == ticketPostEnabled)
            {
                ticketPostConfigs[_currentPostConfigCode].enabled = ticketPostEnabled.Checked;
            }
        }

        private void OnTicketPrintCopiesValueChanged(object sender, EventArgs e)
        {
            ticketPostConfigs[_currentPostConfigCode].printCopies = (int)ticketPostPrintCopies.Value;
        }

        private void NormalizeBtnPostSettingColor(Button activeButton)
        {
            foreach ( var button in _btnPostConfigList)
            {
                if (activeButton == button)
                    button.BackColor = Color.LightGreen;
                else
                    button.BackColor = Button.DefaultBackColor;
            }
        }
    }
}
