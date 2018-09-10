using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CommonExchange
{
    #region Common Structure Exchange

    [Serializable()]
    public struct ClinicInformation
    {
        private String _strInfo;

        public String ClinicName
        {
            get
            {
                _strInfo = "OREVILLO'S DENTAL CLINIC";
                return _strInfo;
            }
        }

        public String Address
        {
            get
            {
                _strInfo = "EVERMALL, Ground Floor, Perdices St., Dumaguete City";
                return _strInfo;
            }
        }

        public String Province
        {
            get
            {
                _strInfo = "Oriental Negros, Philippines";
                return _strInfo;
            }
        }

        public String PhoneNos
        {
            get
            {
                _strInfo = "Tel. Nos.: 0917 300 9862; 0905 961 2191; (035) 422 4272";
                return _strInfo;
            }
        }

        public String ApprovedBy
        {
            get
            {
                _strInfo = "Dr. Ian John B. Orevillo";
                return _strInfo;
            }
        }
    }

    [Serializable()]
    public struct SysAccess
    {
        private String _userId;
        public String UserId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        private String _userName;
        public String UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private String _password;
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        private Boolean _userStatus;
        public Boolean UserStatus
        {
            get { return _userStatus; }
            set { _userStatus = value; }
        }

        private String _accessCode;
        public String AccessCode
        {
            get { return _accessCode; }
            set { _accessCode = value; }
        }

        private String _lastName;
        public String LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private String _firstName;
        public String FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private String _middleName;
        public String MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        private String _position;
        public String Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private SqlConnection _sqlConn;
        public SqlConnection Connection
        {
            get { return _sqlConn; }
            set { _sqlConn = value; }
        }
               
    }
    
    #endregion
}
