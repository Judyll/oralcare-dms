using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DentalSystem
{
    internal class DentalSystemLogic : DentalLib.Patient
    {
        #region Class Member Declarations
        private DataTable _patientTable;
        #endregion

        #region Class Constructor
        public DentalSystemLogic(CommonExchange.SysAccess userInfo)
            : base(userInfo)
        {
        }
        #endregion

        #region Programmer-Defined Void Procedures
        //this procedure inserts a new patient information
        public void InsertPatientInformation(CommonExchange.Patient patientInfo)
        {
            if (_patientTable != null)
            {
                DataRow newRow = _patientTable.NewRow();

                newRow["sysid_patient"] = patientInfo.PatientSystemId;
                newRow["last_name"] = patientInfo.LastName;
                newRow["first_name"] = patientInfo.FirstName;
                newRow["middle_name"] = patientInfo.MiddleName;
                newRow["home_address"] = patientInfo.HomeAddress;
                newRow["phone_nos"] = patientInfo.PhoneNos;
                newRow["birthdate"] = patientInfo.BirthDate;
                newRow["e_mail"] = patientInfo.Email;
                newRow["medical_history"] = patientInfo.MedicalHistory;
                newRow["emergency_info"] = patientInfo.EmergencyInfo;

                _patientTable.Rows.Add(newRow);

                _patientTable.AcceptChanges();
            }

        } //-----------------------------

        //this procedure update a patient information
        public void UpdatePatientInformation(CommonExchange.Patient patientInfo)
        {
            Int32 index = 0;

            if (_patientTable != null)
            {
                foreach (DataRow patientRow in _patientTable.Rows)
                {
                    if (String.Equals(patientRow["sysid_patient"].ToString(), patientInfo.PatientSystemId))
                    {
                        DataRow editRow = _patientTable.Rows[index];

                        editRow.BeginEdit();

                        editRow["sysid_patient"] = patientInfo.PatientSystemId;
                        editRow["last_name"] = patientInfo.LastName;
                        editRow["first_name"] = patientInfo.FirstName;
                        editRow["middle_name"] = patientInfo.MiddleName;
                        editRow["home_address"] = patientInfo.HomeAddress;
                        editRow["phone_nos"] = patientInfo.PhoneNos;
                        editRow["birthdate"] = patientInfo.BirthDate;
                        editRow["e_mail"] = patientInfo.Email;
                        editRow["medical_history"] = patientInfo.MedicalHistory;
                        editRow["emergency_info"] = patientInfo.EmergencyInfo;

                        editRow.EndEdit();

                        break;
                    }

                    index++;
                }
            }

        } //------------------------------
        #endregion

        #region Programmer-Defined Function Procedures
        //this procedure gets the searched patient information
        public DataTable GetSearchedPatientInformation(CommonExchange.SysAccess userInfo, String queryString, Boolean isNewSearch)
        {
            if (isNewSearch)
            {
                using (DatabaseLib.DbLibPatientManager dbLib = new DatabaseLib.DbLibPatientManager())
                {
                    _patientTable = dbLib.SelectPatientInformation(userInfo, queryString);
                }
            }

            DataTable dbTable = new DataTable("PatientInformationTable");
            dbTable.Columns.Add("sysid_patient", System.Type.GetType("System.String"));
            dbTable.Columns.Add("patient_name", System.Type.GetType("System.String"));

            if (_patientTable != null)
            {
                foreach (DataRow patientRow in _patientTable.Rows)
                {
                    DataRow newRow = dbTable.NewRow();

                    newRow["sysid_patient"] = patientRow["sysid_patient"];
                    newRow["patient_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(patientRow, "last_name", "first_name", "middle_name");

                    dbTable.Rows.Add(newRow);
                }
            }

            return dbTable;
            
        } //-------------------------
        #endregion
    }
}
