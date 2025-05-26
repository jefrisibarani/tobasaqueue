using System.Collections.Generic;

namespace Tobasa
{
    public class Options
    {
        public string securitySalt;
        public string providerType;
    }

    public class SqliteOptions
    {
        public string dbFilePath;

        private string _connStr;
        public string connString
        {
            get
            {
                _connStr = $"Data Source={dbFilePath};Version=3;";
                return _connStr;
            }
            set
            {
                _connStr = value;
            }
        }
    }

    public struct SqlOptions
    {
        public string hostAddr;
        public string tcpPort;
        public string database;
        public string username;
        public string password;

        public string securitySalt;

        private string _passEnc;
        private string _connStr;

        public string providerType;

        public string connString
        {
            get
            {
                if (providerType == "MSSQL")
                    _connStr = $"Server={hostAddr},{tcpPort};Database={database};User ID={username};Trusted_Connection=False";
                else if (providerType == "MYSQL")
                    _connStr = $"Data Source={hostAddr},{tcpPort};User ID={username};Initial Catalog={database};";
                else if (providerType == "PGSQL")
                    _connStr = $"Host={hostAddr};Username={username};Database={database};Port={tcpPort};";

                return _connStr;
            }
            set
            {
                _connStr = value;
            }
        }

        public string passwordEnc
        {
            get
            {
                _passEnc = Util.EncryptPassword(password, securitySalt);
                return _passEnc;
            }
            set
            {
                _passEnc = value;
            }
        }
    }

    public struct QueOptions
    {
        public string hostAddr;
        public string tcpPort;
        public string username;
        public string password;

        public string securitySalt;
        public string passwordEnc
        {
            get
            {
                _passEnc = Util.EncryptPassword(password, securitySalt);
                return _passEnc;
            }
            set
            {
                _passEnc = value;
            }
        }

        private string _passEnc;
    }

    public class ConfigFile
    {
        public string name;
        public string path;
        public bool backup = false;
        public bool exists = false;
    }

    public class PostConfig
    {
        public string code; 
        public string name;
        public string caption;
    }

    public class PostConfigDisplay : PostConfig
    {
        public bool visible;
        public bool playAudio;
        public string infoText;
    }

    public class PostConfigTicket : PostConfig
    {
        public bool enabled;
        public bool visible;
        public int printCopies;
        public string ticketHeader;
    }

    public class PostConfigCaller : PostConfig
    {
    }

    public class DefaultData
    {
        public static Dictionary<string, PostConfigDisplay> CreatePostConfigDisplayMap()
        {
            Dictionary<string, PostConfigDisplay> map = new Dictionary<string, PostConfigDisplay>
            {
                {
                    "POST0",
                    new PostConfigDisplay
                    {
                        code = "POST0",
                        name = "Apotik",
                        caption = "APOTIK",
                        visible = true,
                        playAudio = true,
                        infoText = "Apotik"
                    }
                },
                {
                    "POST1",
                    new PostConfigDisplay
                    {
                        code = "POST1",
                        name = "Laboratorium",
                        caption = "LABORATORIUM",
                        visible = true,
                        playAudio = true,
                        infoText = "Laboratorium"
                    }
                },
                {
                    "POST2",
                    new PostConfigDisplay
                    {
                        code = "POST2",
                        name = "Pendaftaran RJ",
                        caption = "PENDAFTARAN RJ",
                        visible = true,
                        playAudio = true,
                        infoText = "Pendaftaran RJ"
                    }
                },
                {
                    "POST3",
                    new PostConfigDisplay
                    {
                        code = "POST3",
                        name = "BPJS Kesehatan",
                        caption = "BPJS KESEHATAN",
                        visible = true,
                        playAudio = true,
                        infoText = "BPJS Kesehatan"
                    }
                },
                {
                    "POST4",
                    new PostConfigDisplay
                    {
                        code = "POST4",
                        name = "Customer Service",
                        caption = "CUSTOMER SERVICE",
                        visible = true,
                        playAudio = true,
                        infoText = "Customer Service"
                    }
                },
                {
                    "POST5",
                    new PostConfigDisplay
                    {
                        code = "POST5",
                        name = "Poli Gigi",
                        caption = "POLI GIGI",
                        visible = true,
                        playAudio = true,
                        infoText = "Poli Gigi"
                    }
                },
                {
                    "POST6",
                    new PostConfigDisplay
                    {
                        code = "POST6",
                        name = "Poli Anak",
                        caption = "POLI ANAK",
                        visible = true,
                        playAudio = true,
                        infoText = "Poli Anak"
                    }
                },
                {
                    "POST7",
                    new PostConfigDisplay
                    {
                        code = "POST7",
                        name = "Poli Mata",
                        caption = "POLI MATA",
                        visible = true,
                        playAudio = true,
                        infoText = "Poli Mata"
                    }
                },
                {
                    "POST8",
                    new PostConfigDisplay
                    {
                        code = "POST8",
                        name = "Poli Kulit",
                        caption = "POLI KULIT",
                        visible = true,
                        playAudio = true,
                        infoText = "Poli Kulit"
                    }
                },
                {
                    "POST9",
                    new PostConfigDisplay
                    {
                        code = "POST9",
                        name = "Poli THT",
                        caption = "POLI THT",
                        visible = true,
                        playAudio = true,
                        infoText = "Poli THT"
                    }
                }
            };

            return map;
        }

        public static Dictionary<string, PostConfigTicket> CreatePostConfigTicketMap()
        {
            Dictionary<string, PostConfigTicket> map = new Dictionary<string, PostConfigTicket>
            {
                {
                    "POST0",
                    new PostConfigTicket
                    {
                        code = "POST0",
                        name = "Apotik",
                        caption = "Apotik - Penyerahan Resep",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Apotik"
                    }
                },
                {
                    "POST1",
                    new PostConfigTicket
                    {
                        code = "POST1",
                        name = "Laboratorium",
                        caption = "Daftar Laboratorium",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Laboratorium"
                    }
                },
                {
                    "POST2",
                    new PostConfigTicket
                    {
                        code = "POST2",
                        name = "Pendaftaran",
                        caption = "Pendaftaran Rawat Jalan",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Pendaftaran"
                    }
                },
                {
                    "POST3",
                    new PostConfigTicket
                    {
                        code = "POST3",
                        name = "BPJS Kesehatan",
                        caption = "BPJS Kesehatan",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "BPJS Kesehatan"
                    }
                },
                {
                    "POST4",
                    new PostConfigTicket
                    {
                        code = "POST4",
                        name = "Customer Service",
                        caption = "Customer Service",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Customer Service"
                    }
                },
                {
                    "POST5",
                    new PostConfigTicket
                    {
                        code = "POST5",
                        name = "Poli Gigi",
                        caption = "POLI GIGI",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Poli Gigi"
                    }
                },
                {
                    "POST6",
                    new PostConfigTicket
                    {
                        code = "POST6",
                        name = "Poli Anak",
                        caption = "POLI ANAK",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Poli Anak"
                    }
                },
                {
                    "POST7",
                    new PostConfigTicket
                    {
                        code = "POST7",
                        name = "Poli Mata",
                        caption = "POLI MATA",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Poli Mata"
                    }
                },
                {
                    "POST8",
                    new PostConfigTicket
                    {
                        code = "POST8",
                        name = "Poli Kulit",
                        caption = "POLI KULIT",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Poli Kulit"
                    }
                },
                {
                    "POST9",
                    new PostConfigTicket
                    {
                        code = "POST9",
                        name = "Poli THT",
                        caption = "POLI THT",
                        enabled = true,
                        visible= true,
                        printCopies = 1,
                        ticketHeader = "Poli THT"
                    }
                }
            };

            return map;
        }

        public static Dictionary<string, PostConfigCaller> CreatePostConfigCallerMap()
        {
            Dictionary<string, PostConfigCaller> map = new Dictionary<string, PostConfigCaller>
            {
                {
                    "POST0",
                    new PostConfigCaller
                    {
                        code = "POST0",
                        name = "Apotik",
                        caption = "Apotik"
                    }
                },
                {
                    "POST1",
                    new PostConfigCaller
                    {
                        code = "POST1",
                        name = "Laboratorium",
                        caption = "Laboratorium"
                    }
                },
                {
                    "POST2",
                    new PostConfigCaller
                    {
                        code = "POST2",
                        name = "Pendaftaran RJ",
                        caption = "Pendaftaran RJ"
                    }
                },
                {
                    "POST3",
                    new PostConfigCaller
                    {
                        code = "POST3",
                        name = "BPJS Kesehatan",
                        caption = "BPJS Kesehatan"
                    }
                },
                {
                    "POST4",
                    new PostConfigCaller
                    {
                        code = "POST4",
                        name = "Customer Service",
                        caption = "Customer Service"
                    }
                },
                {
                    "POST5",
                    new PostConfigCaller
                    {
                        code = "POST5",
                        name = "Poli Gigi",
                        caption = "Poli Gigi"
                    }
                },
                {
                    "POST6",
                    new PostConfigCaller
                    {
                        code = "POST6",
                        name = "Poli Anak",
                        caption = "Poli Anak"
                    }
                },
                {
                    "POST7",
                    new PostConfigCaller
                    {
                        code = "POST7",
                        name = "Poli Mata",
                        caption = "Poli Mata"
                    }
                },
                {
                    "POST8",
                    new PostConfigCaller
                    {
                        code = "POST8",
                        name = "Poli Kulit",
                        caption = "Poli Kulit"
                    }
                },
                {
                    "POST9",
                    new PostConfigCaller
                    {
                        code = "POST9",
                        name = "Poli THT",
                        caption = "Poli THT"
                    }
                }
            };

            return map;
        }
    }
}
