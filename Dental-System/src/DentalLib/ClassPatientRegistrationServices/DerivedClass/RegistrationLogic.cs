using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Collections;

namespace DentalLib
{
    public class RegistrationLogic : Patient
    {
        #region Class Member Declarations
        private DataTable _registrationTable;
        private DataTable _cashReportTable;
        private DataTable _receivableTable;
        private DataTable _salesTable;
        private DataTable _procedureTable;
        #endregion

        #region Class Constructor
        public RegistrationLogic(CommonExchange.SysAccess userInfo)
            : base(userInfo)
        {
            
        }
        #endregion

        #region Programmer-Defined Void Procedures
        //this procedure inserts a new patient registration
        public void InsertPatientRegistration(CommonExchange.SysAccess userInfo, CommonExchange.Registration regInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.InsertPatientRegistration(userInfo, ref regInfo);
            }

            if (_registrationTable != null)
            {
                DataRow newRow = _registrationTable.NewRow();

                newRow["sysid_registration"] = regInfo.RegistrationSystemId;
                newRow["registration_date"] = regInfo.RegistrationDate;
                newRow["amount_payable"] = Decimal.Parse("0");
                newRow["amount_paid"] = Decimal.Parse("0");
                newRow["discount"] = Decimal.Parse("0");
                newRow["amount_balance"] = Decimal.Parse("0");

                _registrationTable.Rows.Add(newRow);

                _registrationTable.AcceptChanges();
            }

        } //--------------------------------

        //this procedure updates a patient registration
        public void UpdatePatientRegistration(CommonExchange.SysAccess userInfo, CommonExchange.Registration regInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.UpdatePatientRegistration(userInfo, regInfo);
            }

            if (_registrationTable != null)
            {
                Int32 index = 0;

                foreach (DataRow regRow in _registrationTable.Rows)
                {
                    if (String.Equals(regInfo.RegistrationSystemId, regRow["sysid_registration"].ToString()))
                    {
                        DataRow editRow = _registrationTable.Rows[index];

                        editRow.BeginEdit();

                        editRow["registration_date"] = regInfo.RegistrationDate;

                        editRow.EndEdit();

                        break;
                    }

                    index++;
                }

                _registrationTable.AcceptChanges();
            }
            
        } //-----------------------------

        //this procedure updates a patient medical prescription
        public void UpdateMedicalPrescriptionPatientRegistration(CommonExchange.SysAccess userInfo, CommonExchange.Registration regInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.UpdateMedicalPrescriptionPatientRegistration(userInfo, regInfo);
            }

            if (_registrationTable != null)
            {
                Int32 index = 0;

                foreach (DataRow regRow in _registrationTable.Rows)
                {
                    if (String.Equals(regInfo.RegistrationSystemId, regRow["sysid_registration"].ToString()))
                    {
                        DataRow editRow = _registrationTable.Rows[index];

                        editRow.BeginEdit();

                        editRow["medical_prescription"] = regInfo.MedicalPrescription;

                        editRow.EndEdit();

                        break;
                    }

                    index++;
                }

                _registrationTable.AcceptChanges();
            }
        } //---------------------------------

        //this procedure prints the sales report
        public void PrintSalesReport(CommonExchange.SysAccess userInfo, DateTime dateFrom, DateTime dateTo)
        {
            if (_salesTable != null)
            {
                DataTable sTable = new DataTable("SalesTableSortByDate");
                sTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
                sTable.Columns.Add("patient_name", System.Type.GetType("System.String"));
                sTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));
                sTable.Columns.Add("procedure_details", System.Type.GetType("System.String"));
                sTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));

                if (_salesTable != null)
                {
                    foreach (DataRow sRow in _salesTable.Rows)
                    {
                        DataRow newRow = sTable.NewRow();

                        newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "sysid_registration", "");
                        newRow["patient_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(sRow, "last_name", "first_name", "middle_name");
                        newRow["procedure_name"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "procedure_name", "");
                        newRow["procedure_details"] = "REG: " + DatabaseLib.ProcStatic.DataRowConvert(sRow, "sysid_registration", "") +
                            "   DA: " + DatabaseLib.ProcStatic.DataRowConvert(sRow, "date_administered",
                            DateTime.Parse(s_serverDateTime)).ToShortDateString() +
                            ((!String.IsNullOrEmpty(DatabaseLib.ProcStatic.DataRowConvert(sRow, "tooth_no", ""))) ?
                            "   TN: " + DatabaseLib.ProcStatic.DataRowConvert(sRow, "tooth_no", "").ToString() : "");
                        newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "amount", Decimal.Parse("0"));

                        sTable.Rows.Add(newRow);
                    }

                    sTable.AcceptChanges();
                }

                DataTable regTable = new DataTable("RegistrationTable");
                regTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));

                if (_salesTable != null)
                {
                    foreach (DataRow sRow in _salesTable.Rows)
                    {
                        String regSysId = DatabaseLib.ProcStatic.DataRowConvert(sRow, "sysid_registration", "");
                        Boolean isExist = false;

                        foreach (DataRow tempRow in regTable.Rows)
                        {
                            if (String.Equals(regSysId, tempRow["sysid_registration"].ToString()))
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            DataRow newRow = regTable.NewRow();

                            newRow["sysid_registration"] = regSysId;

                            regTable.Rows.Add(newRow);
                        }

                    }

                    regTable.AcceptChanges();
                }

                using (DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalSalesReport rptReport =
                    new DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalSalesReport())
                {
                    CommonExchange.ClinicInformation clinicInfo = new CommonExchange.ClinicInformation();

                    rptReport.Database.Tables["registration_table"].SetDataSource(regTable);
                    rptReport.Database.Tables["sales_table"].SetDataSource(sTable);

                    rptReport.SetParameterValue("@clinic_name", clinicInfo.ClinicName);
                    rptReport.SetParameterValue("@address", clinicInfo.Address);
                    rptReport.SetParameterValue("@contact_no", clinicInfo.PhoneNos);
                    rptReport.SetParameterValue("@date_covered", dateFrom.ToLongDateString() + "   to   " + dateTo.ToLongDateString());

                    rptReport.SetParameterValue("@printed_details", "Prepared by: " + userInfo.LastName + ", " +
                        userInfo.FirstName + " " + userInfo.MiddleName + "   " + DateTime.Parse(s_serverDateTime).ToShortDateString() + "  " +
                        DateTime.Parse(s_serverDateTime).ToShortTimeString());

                    using (CrystalReportViewer frmViewer = new CrystalReportViewer(rptReport))
                    {
                        frmViewer.ShowDialog();
                    }
                }
            }
        } //---------------------------------------

        //this procedure prints the receipt
        public void PrintCashReport(CommonExchange.SysAccess userInfo, DateTime dateFrom, DateTime dateTo)
        {
            if (_cashReportTable != null)
            {
                DataTable cashTable = new DataTable("CashTableSortByDate");
                cashTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
                cashTable.Columns.Add("patient_name", System.Type.GetType("System.String"));
                cashTable.Columns.Add("receipt_details", System.Type.GetType("System.String"));
                cashTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));

                if (_cashReportTable != null)
                {
                    DataRow[] selectRow = _cashReportTable.Select("", "sysid_registration DESC");

                    foreach (DataRow cashRow in selectRow)
                    {
                        DataRow newRow = cashTable.NewRow();

                        newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(cashRow, "sysid_registration", "");
                        newRow["patient_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(cashRow, "last_name", "first_name", "middle_name");
                        newRow["receipt_details"] = "Date Posted: " + DatabaseLib.ProcStatic.DataRowConvert(cashRow, "date_paid",
                            DateTime.Parse(s_serverDateTime)).ToShortDateString() + "   Receipt No: " +
                            DatabaseLib.ProcStatic.DataRowConvert(cashRow, "receipt_no", "");
                        newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(cashRow, "amount", Decimal.Parse("0"));

                        cashTable.Rows.Add(newRow);
                    }

                    cashTable.AcceptChanges();
                }

                DataTable regTable = new DataTable("RegistrationTable");
                regTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));

                if (_cashReportTable != null)
                {
                    foreach (DataRow regRow in _cashReportTable.Rows)
                    {
                        String regSysId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                        Boolean isExist = false;

                        foreach (DataRow tempRow in regTable.Rows)
                        {
                            if (String.Equals(regSysId, tempRow["sysid_registration"].ToString()))
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            DataRow newRow = regTable.NewRow();

                            newRow["sysid_registration"] = regSysId;

                            regTable.Rows.Add(newRow);
                        }

                    }

                    regTable.AcceptChanges();
                }

                using (DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalCashReport rptReport =
                    new DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalCashReport())
                {
                    CommonExchange.ClinicInformation clinicInfo = new CommonExchange.ClinicInformation();

                    rptReport.Database.Tables["registration_table"].SetDataSource(regTable);
                    rptReport.Database.Tables["cash_report_table"].SetDataSource(cashTable);

                    rptReport.SetParameterValue("@clinic_name", clinicInfo.ClinicName);
                    rptReport.SetParameterValue("@address", clinicInfo.Address);
                    rptReport.SetParameterValue("@contact_no", clinicInfo.PhoneNos);
                    rptReport.SetParameterValue("@date_covered", dateFrom.ToLongDateString() + "   to   " + dateTo.ToLongDateString());

                    rptReport.SetParameterValue("@printed_details", "Prepared by: " + userInfo.LastName + ", " +
                        userInfo.FirstName + " " + userInfo.MiddleName + "   " + DateTime.Parse(s_serverDateTime).ToShortDateString() + "  " +
                        DateTime.Parse(s_serverDateTime).ToShortTimeString());

                    using (CrystalReportViewer frmViewer = new CrystalReportViewer(rptReport))
                    {
                        frmViewer.ShowDialog();
                    }
                }

            }

        } //------------------------------

        //this procedure prints the receipt
        public void PrintAccountsReceivableReport(CommonExchange.SysAccess userInfo)
        {
            if (_receivableTable != null)
            {
                DataTable receivableTable = new DataTable("AccountsReceivableTableSortByDate");
                receivableTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
                receivableTable.Columns.Add("patient_name", System.Type.GetType("System.String"));
                receivableTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
                receivableTable.Columns.Add("payment_details", System.Type.GetType("System.String"));
                receivableTable.Columns.Add("amount_balance", System.Type.GetType("System.Decimal"));

                if (_receivableTable != null)
                {
                    foreach (DataRow recRow in _receivableTable.Rows)
                    {
                        DataRow newRow = receivableTable.NewRow();

                        newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "sysid_registration", "");
                        newRow["patient_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(recRow, "last_name", "first_name", "middle_name");
                        newRow["registration_date"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "registration_date",
                            DateTime.Parse(s_serverDateTime)).ToShortDateString();
                        newRow["payment_details"] = "Total: " + DatabaseLib.ProcStatic.DataRowConvert(recRow, "amount_payable", Decimal.Parse("0")).ToString("N") +
                            "   Paid: " + DatabaseLib.ProcStatic.DataRowConvert(recRow, "amount_paid", Decimal.Parse("0")).ToString("N") +
                            "   Discount: " + DatabaseLib.ProcStatic.DataRowConvert(recRow, "discount", Decimal.Parse("0")).ToString("N");
                        newRow["amount_balance"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "amount_balance", Decimal.Parse("0"));

                        receivableTable.Rows.Add(newRow);
                    }

                    receivableTable.AcceptChanges();
                }

                DataTable regTable = new DataTable("RegistrationTable");
                regTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));

                if (_receivableTable != null)
                {
                    foreach (DataRow regRow in _receivableTable.Rows)
                    {
                        String regSysId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                        Boolean isExist = false;

                        foreach (DataRow tempRow in regTable.Rows)
                        {
                            if (String.Equals(regSysId, tempRow["sysid_registration"].ToString()))
                            {
                                isExist = true;
                                break;
                            }
                        }

                        if (!isExist)
                        {
                            DataRow newRow = regTable.NewRow();

                            newRow["sysid_registration"] = regSysId;

                            regTable.Rows.Add(newRow);
                        }

                    }

                    regTable.AcceptChanges();
                }

                using (DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalAccountsReceivableReport rptReport =
                    new DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalAccountsReceivableReport())
                {
                    CommonExchange.ClinicInformation clinicInfo = new CommonExchange.ClinicInformation();

                    rptReport.Database.Tables["registration_table"].SetDataSource(regTable);
                    rptReport.Database.Tables["accounts_receivable_table"].SetDataSource(receivableTable);

                    rptReport.SetParameterValue("@clinic_name", clinicInfo.ClinicName);
                    rptReport.SetParameterValue("@address", clinicInfo.Address);
                    rptReport.SetParameterValue("@contact_no", clinicInfo.PhoneNos);
                    rptReport.SetParameterValue("@date_covered", "as of " + DateTime.Parse(s_serverDateTime).ToLongDateString());

                    rptReport.SetParameterValue("@printed_details", "Prepared by: " + userInfo.LastName + ", " +
                        userInfo.FirstName + " " + userInfo.MiddleName + "   " + DateTime.Parse(s_serverDateTime).ToShortDateString() + "  " +
                        DateTime.Parse(s_serverDateTime).ToShortTimeString());

                    using (CrystalReportViewer frmViewer = new CrystalReportViewer(rptReport))
                    {
                        frmViewer.ShowDialog();
                    }
                }

            }

        } //-----------------------------------------

        //this procedure prints the receipt
        public void PrintRegistrationList(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo)
        {
            if (_registrationTable != null)
            {
                DataTable dbTable = new DataTable("PatientRegistrationTemp");
                dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
                dbTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
                dbTable.Columns.Add("amount_payable", System.Type.GetType("System.Decimal"));
                dbTable.Columns.Add("amount_paid", System.Type.GetType("System.Decimal"));
                dbTable.Columns.Add("discount", System.Type.GetType("System.Decimal"));
                dbTable.Columns.Add("amount_balance", System.Type.GetType("System.Decimal"));

                if (_registrationTable != null)
                {
                    foreach (DataRow regRow in _registrationTable.Rows)
                    {
                        DataRow newRow = dbTable.NewRow();

                        newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                        newRow["registration_date"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "registration_date", 
                            DateTime.Parse(s_serverDateTime)).ToLongDateString();
                        newRow["amount_payable"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_payable", Decimal.Parse("0"));
                        newRow["amount_paid"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_paid", Decimal.Parse("0"));
                        newRow["discount"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "discount", Decimal.Parse("0"));
                        newRow["amount_balance"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_balance", Decimal.Parse("0"));

                        dbTable.Rows.Add(newRow);
                    }

                    dbTable.AcceptChanges();
                }

                DataTable regTable = new DataTable("RegistrationTable");
                regTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));

                foreach (DataRow regRow in _registrationTable.Rows)
                {
                    String regSysId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                    Boolean isExist = false;

                    foreach (DataRow tempRow in regTable.Rows)
                    {
                        if (String.Equals(regSysId, tempRow["sysid_registration"].ToString()))
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        DataRow newRow = regTable.NewRow();

                        newRow["sysid_registration"] = regSysId;

                        regTable.Rows.Add(newRow);
                    }

                }

                regTable.AcceptChanges();

                using (DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalRegistrationList rptReport =
                    new DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalRegistrationList())
                {
                    CommonExchange.ClinicInformation clinicInfo = new CommonExchange.ClinicInformation();

                    rptReport.Database.Tables["registration_table"].SetDataSource(regTable);
                    rptReport.Database.Tables["registration_list"].SetDataSource(dbTable);

                    rptReport.SetParameterValue("@clinic_name", clinicInfo.ClinicName);
                    rptReport.SetParameterValue("@address", clinicInfo.Address);
                    rptReport.SetParameterValue("@contact_no", clinicInfo.PhoneNos);
                    rptReport.SetParameterValue("@date_covered", "as of " + DateTime.Parse(s_serverDateTime).ToLongDateString());
                    rptReport.SetParameterValue("@patient_name", patientInfo.LastName.ToUpper() + ", " + patientInfo.FirstName.ToUpper() + " " +
                            patientInfo.MiddleName.ToUpper());
                    rptReport.SetParameterValue("@patient_address", patientInfo.HomeAddress);
                    rptReport.SetParameterValue("@patient_age", patientInfo.Age);

                    rptReport.SetParameterValue("@printed_details", "Prepared by: " + userInfo.LastName + ", " +
                        userInfo.FirstName + " " + userInfo.MiddleName + "   " + DateTime.Parse(s_serverDateTime).ToShortDateString() + "  " +
                        DateTime.Parse(s_serverDateTime).ToShortTimeString());

                    using (CrystalReportViewer frmViewer = new CrystalReportViewer(rptReport))
                    {
                        frmViewer.ShowDialog();
                    }
                }


            }
        } //--------------------------

        //this procedure sets th sales report table
        public void SetSalesReportTable(CommonExchange.SysAccess userInfo, String dateFrom, String dateTo,
            CheckedListBox cbxBase, ProgressBar pgbBase)
        {
            pgbBase.Maximum = cbxBase.CheckedItems.Count;
            pgbBase.Value = 0;
            pgbBase.Step = 1;

            if (_procedureTable != null && cbxBase.CheckedItems.Count != 0)
            {
                _salesTable = new DataTable("SelectedSalesTable");
                _salesTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("sysid_patient", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("medical_prescription", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
                _salesTable.Columns.Add("last_name", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("first_name", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("middle_name", System.Type.GetType("System.String"));
                _salesTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));

                IEnumerator myEnum = cbxBase.CheckedIndices.GetEnumerator();
                Int32 i;

                while (myEnum.MoveNext() != false)
                {
                    pgbBase.PerformStep();

                    i = (Int32)myEnum.Current;

                    DataRow procRow = _procedureTable.Rows[i];
                    String procSysId = DatabaseLib.ProcStatic.DataRowConvert(procRow, "sysid_procedure", "");

                    using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
                    {
                        DataTable sTable = dbLib.SelectForSalesPatientRegistration(userInfo, dateFrom, dateTo, procSysId);

                        foreach (DataRow salesRow in sTable.Rows)
                        {
                            DataRow newRow = _salesTable.NewRow();

                            newRow["sysid_registration"] = salesRow["sysid_registration"];
                            newRow["sysid_patient"] = salesRow["sysid_patient"];
                            newRow["registration_date"] = salesRow["registration_date"];
                            newRow["medical_prescription"] = salesRow["medical_prescription"];
                            newRow["date_administered"] = salesRow["date_administered"];
                            newRow["tooth_no"] = salesRow["tooth_no"];
                            newRow["amount"] = salesRow["amount"];
                            newRow["last_name"] = salesRow["last_name"];
                            newRow["first_name"] = salesRow["first_name"];
                            newRow["middle_name"] = salesRow["middle_name"];
                            newRow["procedure_name"] = salesRow["procedure_name"];

                            _salesTable.Rows.Add(newRow);

                        }
                    }
                }

                _salesTable.AcceptChanges();
            }

        } //--------------------------------
        
        #endregion

        #region Programmer-Defined Function Procedures
        //this function returns the patient registration table
        public DataTable SelectByPatientIDPatientRegistration(CommonExchange.SysAccess userInfo, String patientId, Boolean isNewQuery)
        {
            if (isNewQuery)
            {
                using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
                {
                    _registrationTable = dbLib.SelectByPatientIDPatientRegistration(userInfo, patientId);
                }
            }            

            DataTable dbTable = new DataTable("PatientRegistrationTemp");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            dbTable.Columns.Add("registration_date", System.Type.GetType("System.DateTime"));
            dbTable.Columns.Add("amount_payable", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("amount_paid", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("discount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("amount_balance", System.Type.GetType("System.Decimal"));

            if (_registrationTable != null)
            {
                foreach (DataRow regRow in _registrationTable.Rows)
                {
                    DataRow newRow = dbTable.NewRow();

                    newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                    newRow["registration_date"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "registration_date", DateTime.Parse(s_serverDateTime));
                    newRow["amount_payable"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_payable", Decimal.Parse("0"));
                    newRow["amount_paid"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_paid", Decimal.Parse("0"));
                    newRow["discount"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "discount", Decimal.Parse("0"));
                    newRow["amount_balance"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_balance", Decimal.Parse("0"));

                    dbTable.Rows.Add(newRow);
                }

                dbTable.AcceptChanges();
            }

            DataTable regTable = new DataTable("PatientRegistrationSortByDate");
            regTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            regTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
            regTable.Columns.Add("amount_payable", System.Type.GetType("System.String"));
            regTable.Columns.Add("amount_paid", System.Type.GetType("System.String"));
            regTable.Columns.Add("discount", System.Type.GetType("System.String"));
            regTable.Columns.Add("amount_balance", System.Type.GetType("System.String"));

            DataRow[] selectRow = dbTable.Select("", "registration_date DESC");

            foreach (DataRow regRow in selectRow)
            {
                DataRow newRow = regTable.NewRow();

                newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                newRow["registration_date"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "registration_date", 
                    DateTime.Parse(s_serverDateTime)).ToLongDateString();
                newRow["amount_payable"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_payable", Decimal.Parse("0")).ToString("N");
                newRow["amount_paid"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_paid", Decimal.Parse("0")).ToString("N");
                newRow["discount"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "discount", Decimal.Parse("0")).ToString("N");
                newRow["amount_balance"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_balance", Decimal.Parse("0")).ToString("N");

                regTable.Rows.Add(newRow);
            }

            return regTable;

        } //-----------------------------

        //this function returns the patient registration table
        public DataTable SelectByProcedureTakenPatientRegistration(CommonExchange.SysAccess userInfo, String patientId, Boolean isNewQuery)
        {
            if (isNewQuery)
            {
                using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
                {
                    _registrationTable = dbLib.SelectByProcedureTakenPatientRegistration(userInfo, patientId);
                }
            }

            DataTable dbTable = new DataTable("ByProcedureTakenTemp");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_administered", System.Type.GetType("System.DateTime"));
            dbTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));

            if (_registrationTable != null)
            {
                foreach (DataRow regRow in _registrationTable.Rows)
                {
                    DataRow newRow = dbTable.NewRow();

                    newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                    newRow["date_administered"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "date_administered", DateTime.Parse(s_serverDateTime));
                    newRow["procedure_name"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "procedure_name", "");
                    newRow["tooth_no"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "tooth_no", "");
                    newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount", Decimal.Parse("0"));

                    dbTable.Rows.Add(newRow);
                }

                dbTable.AcceptChanges();
            }

            DataTable regTable = new DataTable("PatientRegistrationSortByDate");
            regTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            regTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
            regTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));
            regTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            regTable.Columns.Add("amount", System.Type.GetType("System.String"));

            DataRow[] selectRow = dbTable.Select("", "date_administered DESC");

            foreach (DataRow regRow in selectRow)
            {
                DataRow newRow = regTable.NewRow();

                newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                newRow["date_administered"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "date_administered", 
                    DateTime.Parse(s_serverDateTime)).ToLongDateString();
                newRow["procedure_name"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "procedure_name", "");
                newRow["tooth_no"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "tooth_no", "");
                newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount", Decimal.Parse("0")).ToString("N");

                regTable.Rows.Add(newRow);
            }

            return regTable;

        } //-----------------------------
        
        //this function returns the patient registration table
        public DataTable SelectForCashReportPatientRegistration(CommonExchange.SysAccess userInfo, String dateFrom, String dateTo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                _cashReportTable = dbLib.SelectForCashReportPatientRegistration(userInfo, dateFrom, dateTo);
            }

            DataTable cashTable = new DataTable("CashTableSortByDate");
            cashTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            cashTable.Columns.Add("patient_name", System.Type.GetType("System.String"));
            cashTable.Columns.Add("date_paid", System.Type.GetType("System.String"));
            cashTable.Columns.Add("receipt_no", System.Type.GetType("System.String"));
            cashTable.Columns.Add("amount", System.Type.GetType("System.String"));

            if (_cashReportTable != null)
            {
                foreach (DataRow cashRow in _cashReportTable.Rows)
                {
                    DataRow newRow = cashTable.NewRow();

                    newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(cashRow, "sysid_registration", "");
                    newRow["patient_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(cashRow, "last_name", "first_name", "middle_name");
                    newRow["date_paid"] = DatabaseLib.ProcStatic.DataRowConvert(cashRow, "date_paid", 
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    newRow["receipt_no"] = DatabaseLib.ProcStatic.DataRowConvert(cashRow, "receipt_no", "");
                    newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(cashRow, "amount", Decimal.Parse("0")).ToString("N");

                    cashTable.Rows.Add(newRow);
                }

                cashTable.AcceptChanges();
            }

            return cashTable;

        } //--------------------------------

        //this function returns the accounts receivable table
        public DataTable SelectForAccountsReceivablePatientRegistration(CommonExchange.SysAccess userInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                _receivableTable = dbLib.SelectForAccountsReceivablePatientRegistration(userInfo);
            }

            DataTable receivableTable = new DataTable("AccountsReceivableTableSortByDate");
            receivableTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            receivableTable.Columns.Add("patient_name", System.Type.GetType("System.String"));
            receivableTable.Columns.Add("registration_date", System.Type.GetType("System.String"));
            receivableTable.Columns.Add("amount_payable", System.Type.GetType("System.String"));
            receivableTable.Columns.Add("amount_paid", System.Type.GetType("System.String"));
            receivableTable.Columns.Add("discount", System.Type.GetType("System.String"));
            receivableTable.Columns.Add("amount_balance", System.Type.GetType("System.String"));

            if (_receivableTable != null)
            {
                foreach (DataRow recRow in _receivableTable.Rows)
                {
                    DataRow newRow = receivableTable.NewRow();

                    newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "sysid_registration", "");
                    newRow["patient_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(recRow, "last_name", "first_name", "middle_name");
                    newRow["registration_date"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "registration_date", 
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    newRow["amount_payable"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "amount_payable", Decimal.Parse("0")).ToString("N");
                    newRow["amount_paid"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "amount_paid", Decimal.Parse("0")).ToString("N");
                    newRow["discount"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "discount", Decimal.Parse("0")).ToString("N");
                    newRow["amount_balance"] = DatabaseLib.ProcStatic.DataRowConvert(recRow, "amount_balance", Decimal.Parse("0")).ToString("N");

                    receivableTable.Rows.Add(newRow);
                }

                receivableTable.AcceptChanges();
            }

            return receivableTable;
        } //-----------------------------

        //this function returns the sales table
        public DataTable SelectForSalesPatientRegistration()
        {
            DataTable dbTable = new DataTable("SalesTable");
            dbTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            dbTable.Columns.Add("patient_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
            dbTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));

            if (_salesTable != null)
            {
                foreach (DataRow sRow in _salesTable.Rows)
                {
                    DataRow newRow = dbTable.NewRow();

                    newRow["sysid_registration"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "sysid_registration", "");
                    newRow["patient_name"] = DentalLib.ProcStatic.GetCompleteNameMiddleInitial(sRow, "last_name", "first_name", "middle_name");
                    newRow["procedure_name"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "procedure_name", "");
                    newRow["date_administered"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "date_administered",
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    newRow["tooth_no"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "tooth_no", "");
                    newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(sRow, "amount", Decimal.Parse("0"));

                    dbTable.Rows.Add(newRow);

                }

                dbTable.AcceptChanges();
            }

            return dbTable;            

        } //-----------------------------

        //this function returns the registration details
        public CommonExchange.Registration GetPatientRegistrationDetails(String regSysId)
        {
            CommonExchange.Registration regInfo = new CommonExchange.Registration();

            if (_registrationTable != null)
            {
                String strFiler = "sysid_registration = '" + regSysId + "'";
                DataRow[] selectRow = _registrationTable.Select(strFiler);

                foreach (DataRow regRow in selectRow)
                {
                    regInfo.RegistrationSystemId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                    regInfo.RegistrationDate = DatabaseLib.ProcStatic.DataRowConvert(regRow, "registration_date",
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    regInfo.MedicalPrescription = DatabaseLib.ProcStatic.DataRowConvert(regRow, "medical_prescription", "");

                    break;
                }
            }

            return regInfo;
            
        } //----------------------------

        //this function returns the registration details for reporting
        public CommonExchange.Registration GetRegistrationDetailsForCashReport(String regSysId)
        {
            CommonExchange.Registration regInfo = new CommonExchange.Registration();

            if (_cashReportTable != null)
            {
                String strFiler = "sysid_registration = '" + regSysId + "'";
                DataRow[] selectRow = _cashReportTable.Select(strFiler);

                foreach (DataRow regRow in selectRow)
                {
                    regInfo.RegistrationSystemId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                    regInfo.RegistrationDate = DatabaseLib.ProcStatic.DataRowConvert(regRow, "registration_date",
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    regInfo.MedicalPrescription = DatabaseLib.ProcStatic.DataRowConvert(regRow, "medical_prescription", "");

                    break;
                }
            }

            return regInfo;
        } //---------------------------------

        //this function returns the registration details for reporting
        public CommonExchange.Registration GetRegistrationDetailsForAccountsReceivableReport(String regSysId)
        {
            CommonExchange.Registration regInfo = new CommonExchange.Registration();

            if (_receivableTable != null)
            {
                String strFiler = "sysid_registration = '" + regSysId + "'";
                DataRow[] selectRow = _receivableTable.Select(strFiler);

                foreach (DataRow regRow in selectRow)
                {
                    regInfo.RegistrationSystemId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_registration", "");
                    regInfo.RegistrationDate = DatabaseLib.ProcStatic.DataRowConvert(regRow, "registration_date",
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    regInfo.MedicalPrescription = DatabaseLib.ProcStatic.DataRowConvert(regRow, "medical_prescription", "");

                    break;
                }
            }

            return regInfo;
        } //---------------------------------

        //this function returns the registration details for reporting
        public CommonExchange.Registration GetRegistrationDetailsForSalesReport(String regSysId)
        {
            CommonExchange.Registration regInfo = new CommonExchange.Registration();

            if (_salesTable != null)
            {
                String strFiler = "sysid_registration = '" + regSysId + "'";
                DataRow[] selectRow = _salesTable.Select(strFiler);

                foreach (DataRow sRow in selectRow)
                {
                    regInfo.RegistrationSystemId = DatabaseLib.ProcStatic.DataRowConvert(sRow, "sysid_registration", "");
                    regInfo.RegistrationDate = DatabaseLib.ProcStatic.DataRowConvert(sRow, "registration_date",
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    regInfo.MedicalPrescription = DatabaseLib.ProcStatic.DataRowConvert(sRow, "medical_prescription", "");

                    break;
                }
            }

            return regInfo;
        } //---------------------------------


        //this function returns the patients system id for reporting
        public String GetPatientSystemIdForCashReport(String regSysId)
        {
            String patientId = "";

            if (_cashReportTable != null)
            {
                String strFiler = "sysid_registration = '" + regSysId + "'";
                DataRow[] selectRow = _cashReportTable.Select(strFiler);

                foreach (DataRow regRow in selectRow)
                {
                    patientId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_patient", "");

                    break;
                }
            }

            return patientId;
        } //------------------------------

        //this function returns the patients system id for reporting
        public String GetPatientSystemIdForAccountsReceivableReport(String regSysId)
        {
            String patientId = "";

            if (_receivableTable != null)
            {
                String strFiler = "sysid_registration = '" + regSysId + "'";
                DataRow[] selectRow = _receivableTable.Select(strFiler);

                foreach (DataRow regRow in selectRow)
                {
                    patientId = DatabaseLib.ProcStatic.DataRowConvert(regRow, "sysid_patient", "");

                    break;
                }
            }

            return patientId;
        } //------------------------------

        //this function returns the patients system id for reporting
        public String GetPatientSystemIdForSalesReport(String regSysId)
        {
            String patientId = "";

            if (_salesTable != null)
            {
                String strFiler = "sysid_registration = '" + regSysId + "'";
                DataRow[] selectRow = _salesTable.Select(strFiler);

                foreach (DataRow sRow in selectRow)
                {
                    patientId = DatabaseLib.ProcStatic.DataRowConvert(sRow, "sysid_patient", "");

                    break;
                }
            }

            return patientId;
        } //------------------------------

        //this function return the total for cash report
        public Decimal GetTotalForCashReport()
        {
            Decimal total = 0;

            if (_cashReportTable != null)
            {
                foreach (DataRow regRow in _cashReportTable.Rows)
                {
                    total += DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount", Decimal.Parse("0"));
                }
            }

            return total;
        } //---------------------------------

        //this function return the total for accounts receivable
        public Decimal GetTotalForAccountsReceivableReport()
        {
            Decimal total = 0;

            if (_receivableTable != null)
            {
                foreach (DataRow regRow in _receivableTable.Rows)
                {
                    total += DatabaseLib.ProcStatic.DataRowConvert(regRow, "amount_balance", Decimal.Parse("0"));
                }
            }

            return total;
        } //---------------------------------

        //this function return the total for sales
        public Decimal GetTotalForSalesReport()
        {
            Decimal total = 0;

            if (_salesTable != null)
            {
                foreach (DataRow sRow in _salesTable.Rows)
                {
                    total += DatabaseLib.ProcStatic.DataRowConvert(sRow, "amount", Decimal.Parse("0"));
                }
            }

            return total;
        } //---------------------------------

        //this function returns the procedure table
        public DataTable SelectProcedureInformation(CommonExchange.SysAccess userInfo)
        {
            using (DatabaseLib.DbLibProcedureManager dbLib = new DatabaseLib.DbLibProcedureManager())
            {
                _procedureTable = dbLib.SelectProcedureInformation(userInfo);
            }

            return _procedureTable;
        } //------------------------------
        #endregion
    }
}
