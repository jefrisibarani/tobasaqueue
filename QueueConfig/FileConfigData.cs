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

        public string connString
        {
            get
            {
                _connStr = $"Provider=SQLOLEDB;Data Source={hostAddr},{tcpPort};User ID={username};Initial Catalog={database};";
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
}
