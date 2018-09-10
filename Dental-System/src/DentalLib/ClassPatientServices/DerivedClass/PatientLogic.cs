using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DentalLib
{
    public class PatientLogic : Patient
    {
        #region Class Properties Declarations
        private CommonExchange.Patient _patientInfo;
        public CommonExchange.Patient PatientInfo
        {
            get { return _patientInfo; }
        }
        #endregion

        #region Class Constructor
        public PatientLogic(CommonExchange.SysAccess userInfo)
            : base(userInfo)
        {
            _patientInfo = new CommonExchange.Patient();
        }
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure inserts a new patient information
        public void InsertPatientInformation(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo)
        {
            using (DatabaseLib.DbLibPatientManager dbLib = new DatabaseLib.DbLibPatientManager())
            {
                dbLib.InsertPatientInformation(userInfo, ref patientInfo);
            }

            _patientInfo = patientInfo;

        } //------------------------------------

        //this procedure updates a patient information
        public void UpdatePatientInformation(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo)
        {
            using (DatabaseLib.DbLibPatientManager dbLib = new DatabaseLib.DbLibPatientManager())
            {
                dbLib.UpdatePatientInformation(userInfo, patientInfo);
            }

            _patientInfo = patientInfo;

        } //----------------------------
        #endregion

        #region Programmer-Defined Function Procedures
        #endregion
    }
}
