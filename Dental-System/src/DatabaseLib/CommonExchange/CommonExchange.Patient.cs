using System;
using System.Collections.Generic;
using System.Text;

namespace CommonExchange
{
    #region Common Structure Exchange

    [Serializable()]
    public struct Patient
    {
        private String _patientSystemId;
        public String PatientSystemId
        {
            get { return _patientSystemId; }
            set { _patientSystemId = value; }
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

        private String _homeAddress;
        public String HomeAddress
        {
            get { return _homeAddress; }
            set { _homeAddress = value; }
        }

        private String _phoneNos;
        public String PhoneNos
        {
            get { return _phoneNos; }
            set { _phoneNos = value; }
        }

        private String _birthDate;
        public String BirthDate
        {
            get { return _birthDate; }
            set { _birthDate = value; }
        }

        private String _eMail;
        public String Email
        {
            get { return _eMail; }
            set { _eMail = value; }
        }

        private String _medicalHistory;
        public String MedicalHistory
        {
            get { return _medicalHistory; }
            set { _medicalHistory = value; }
        }

        private String _emergencyInfo;
        public String EmergencyInfo
        {
            get { return _emergencyInfo; }
            set { _emergencyInfo = value; }
        }
        
        private String _imagePath;
        public String ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        private String _imageExt;
        public String ImageExtension
        {
            get { return _imageExt; }
            set { _imageExt = value; }
        }

        private Byte[] _image;
        public Byte[] ImageBytes
        {
            get { return _image; }
            set { _image = value; }
        }

        private String _age;
        public String Age
        {
            get { return _age; }
            set { _age = value; }
        }
    }

    #endregion
}
