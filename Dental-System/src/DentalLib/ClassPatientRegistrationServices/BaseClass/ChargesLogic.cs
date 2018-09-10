using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DentalLib
{
    public class ChargesLogic : Patient
    {
        #region Class Member Declarations
        private DataTable _registrationDetailsTable;
        private DataTable _paymentDetailsTable;
        #endregion

        #region Class Constructor
        public ChargesLogic(CommonExchange.SysAccess userInfo)
            : base(userInfo)
        {
        }
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure prints the receipt
        public void PrintPatientChargesReceipt(CommonExchange.SysAccess userInfo, CommonExchange.Patient patientInfo, CommonExchange.Registration regInfo)
        {
            DataTable regTable = new DataTable("RegistrationTable");
            regTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));

            DataRow regRow = regTable.NewRow();

            regRow["sysid_registration"] = regInfo.RegistrationSystemId;

            regTable.Rows.Add(regRow);

            regTable.AcceptChanges();

            DataTable procedureTable = new DataTable("ProceduresTakenTable");
            procedureTable.Columns.Add("details_id", System.Type.GetType("System.String"));
            procedureTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            procedureTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
            procedureTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));
            procedureTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));

            if (_registrationDetailsTable != null)
            {
                foreach (DataRow detailsRow in _registrationDetailsTable.Rows)
                {
                    DataRow newRow = procedureTable.NewRow();

                    newRow["details_id"] = detailsRow["details_id"].ToString();
                    newRow["sysid_registration"] = regInfo.RegistrationSystemId;
                    newRow["date_administered"] = "Date Administered: " + DateTime.Parse(detailsRow["date_administered"].ToString()).ToShortDateString() +
                        ((!String.IsNullOrEmpty(DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "tooth_no", ""))) ?
                        "   Tooth No: " + DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "tooth_no", "").ToString() : "");
                    newRow["procedure_name"] = detailsRow["procedure_name"].ToString();
                    newRow["amount"] = Decimal.Parse(detailsRow["amount"].ToString());

                    procedureTable.Rows.Add(newRow);
                }

                procedureTable.AcceptChanges();
            }

            DataTable paymentTable = new DataTable("PaymentTable");
            paymentTable.Columns.Add("receipt_no", System.Type.GetType("System.String"));
            paymentTable.Columns.Add("sysid_registration", System.Type.GetType("System.String"));
            paymentTable.Columns.Add("receipt_details", System.Type.GetType("System.String"));
            paymentTable.Columns.Add("payment_details", System.Type.GetType("System.String"));
            paymentTable.Columns.Add("discount_details", System.Type.GetType("System.String"));
            paymentTable.Columns.Add("payment_amount", System.Type.GetType("System.Decimal"));
            paymentTable.Columns.Add("discount_amount", System.Type.GetType("System.Decimal"));

            if (_paymentDetailsTable != null)
            {
                foreach (DataRow payRow in _paymentDetailsTable.Rows)
                {
                    DataRow newRow = paymentTable.NewRow();

                    newRow["receipt_no"] = payRow["receipt_no"].ToString();
                    newRow["sysid_registration"] = regInfo.RegistrationSystemId;
                    newRow["receipt_details"] =  "Date Posted: " + DateTime.Parse(payRow["date_paid"].ToString()).ToShortDateString() + 
                        "   Receipt No: " + payRow["receipt_no"].ToString();
                    newRow["payment_details"] = "Amount Paid:" + " " + Decimal.Parse(payRow["amount"].ToString()).ToString("N") + 
                        "   Discount: " + Decimal.Parse(payRow["discount"].ToString()).ToString("N");
                    newRow["discount_details"] = "Discount";
                    newRow["payment_amount"] = Decimal.Parse(payRow["amount"].ToString()) + Decimal.Parse(payRow["discount"].ToString());

                    paymentTable.Rows.Add(newRow);
                }

                paymentTable.AcceptChanges();
            }

            using (DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalPaymentSummary rptReceipt = 
                new DentalLib.ClassPatientRegistrationServices.CrystalReport.CrystalPaymentSummary())
            {
                CommonExchange.ClinicInformation clinicInfo = new CommonExchange.ClinicInformation();

                rptReceipt.Database.Tables["registration_table"].SetDataSource(regTable);
                rptReceipt.Database.Tables["procedures_table"].SetDataSource(procedureTable);
                rptReceipt.Database.Tables["payments_table"].SetDataSource(paymentTable);

                rptReceipt.SetParameterValue("@clinic_name", clinicInfo.ClinicName);
                rptReceipt.SetParameterValue("@address", clinicInfo.Address);
                rptReceipt.SetParameterValue("@contact_no", clinicInfo.PhoneNos);
                rptReceipt.SetParameterValue("@patient_name", patientInfo.LastName.ToUpper() + ", " + patientInfo.FirstName.ToUpper() + " " +
                    patientInfo.MiddleName.ToUpper());
                rptReceipt.SetParameterValue("@patient_address", patientInfo.HomeAddress);
                rptReceipt.SetParameterValue("@patient_age", patientInfo.Age);
                rptReceipt.SetParameterValue("@registration_no", regInfo.RegistrationSystemId);
                rptReceipt.SetParameterValue("@registration_date", regInfo.RegistrationDate.ToString());

                CommonExchange.PaymentSummary summary = this.GetPaymentSummary();

                rptReceipt.SetParameterValue("@total_notice", "Your current balance is >> ");
                rptReceipt.SetParameterValue("@total_amount", summary.Balance.ToString("N"));

                rptReceipt.SetParameterValue("@runtime_date", "as of " + DateTime.Parse(s_serverDateTime).ToShortDateString() + " " + 
                    DateTime.Parse(s_serverDateTime).ToShortTimeString());

                rptReceipt.SetParameterValue("@printed_details", "Prepared by: " + userInfo.LastName + ", " + userInfo.FirstName + " " + userInfo.MiddleName);
                rptReceipt.SetParameterValue("@approved_details", "Approved by: " + clinicInfo.ApprovedBy);
                    

                using (CrystalReportViewer frmViewer = new CrystalReportViewer(rptReceipt))
                {
                    frmViewer.ShowDialog();
                }
            }
        } //-----------------------

        //this procedure inserts a new registration details
        public void InsertRegistrationDetails(CommonExchange.SysAccess userInfo, CommonExchange.RegistrationDetails detailsInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.InsertRegistrationDetails(userInfo, detailsInfo);
            }

        } //---------------------------------

        //this procedure updates a registration details
        public void UpdateRegistrationDetails(CommonExchange.SysAccess userInfo, CommonExchange.RegistrationDetails detailsInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.UpdateRegistrationDetails(userInfo, detailsInfo);
            }

            if (_registrationDetailsTable != null)
            {
                Int32 index = 0;

                foreach (DataRow detailsRow in _registrationDetailsTable.Rows)
                {
                    if (detailsInfo.DetailsId == DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "details_id", Int64.Parse("0")))
                    {
                        DataRow editRow = _registrationDetailsTable.Rows[index];

                        editRow.BeginEdit();

                        editRow["sysid_procedure"] = detailsInfo.ProcedureSystemId;
                        editRow["date_administered"] = detailsInfo.DateAdministered;
                        editRow["tooth_no"] = detailsInfo.ToothNo;
                        editRow["amount"] = detailsInfo.Amount;
                        editRow["remarks"] = detailsInfo.Remarks;
                        editRow["procedure_name"] = detailsInfo.ProcedureName;

                        editRow.EndEdit();

                        break;
                    }

                    index++;
                }

                _registrationDetailsTable.AcceptChanges();
            }
        } //-----------------------------

        //this procedure deletes a registration details
        public void DeleteRegistrationDetails(CommonExchange.SysAccess userInfo, CommonExchange.RegistrationDetails detailsInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.DeleteRegistrationDetails(userInfo, detailsInfo);
            }

            if (_registrationDetailsTable != null)
            {
                Int32 index = 0;

                foreach (DataRow detailsRow in _registrationDetailsTable.Rows)
                {
                    if (detailsInfo.DetailsId == DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "details_id", Int64.Parse("0")))
                    {
                        DataRow delRow = _registrationDetailsTable.Rows[index];

                        delRow.Delete();

                        break;
                    }

                    index++;
                }

                _registrationDetailsTable.AcceptChanges();
            }
        } //--------------------------------

        //this procedure inserts a new payment details
        public void InsertPaymentDetails(CommonExchange.SysAccess userInfo, CommonExchange.PaymentDetails paymentInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.InsertPaymentDetails(userInfo, ref paymentInfo);
            }

            if (_paymentDetailsTable != null)
            {
                DataRow newRow = _paymentDetailsTable.NewRow();

                newRow["receipt_no"] = paymentInfo.ReceiptNo;
                newRow["date_paid"] = paymentInfo.DatePaid;
                newRow["amount"] = paymentInfo.Amount;
                newRow["discount"] = paymentInfo.Discount;
                newRow["payment_type"] = paymentInfo.PaymentType;
                newRow["bank_name"] = paymentInfo.BankName;
                newRow["check_no"] = paymentInfo.CheckNo;
                newRow["card_number"] = paymentInfo.CardNumber;
                newRow["card_type"] = paymentInfo.CardType;
                newRow["card_expire"] = paymentInfo.CardExpire;

                _paymentDetailsTable.Rows.Add(newRow);

                _paymentDetailsTable.AcceptChanges();
            }

        } //--------------------------------------

        //this procedure inserts a new payment details
        public void UpdatePaymentDetails(CommonExchange.SysAccess userInfo, CommonExchange.PaymentDetails paymentInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.UpdatePaymentDetails(userInfo, paymentInfo);
            }

            if (_paymentDetailsTable != null)
            {
                Int32 index = 0;

                foreach (DataRow payRow in _paymentDetailsTable.Rows)
                {
                    if (String.Equals(paymentInfo.ReceiptNo, payRow["receipt_no"].ToString()))
                    {
                        DataRow editRow = _paymentDetailsTable.Rows[index];

                        editRow.BeginEdit();

                        editRow["date_paid"] = paymentInfo.DatePaid;
                        editRow["amount"] = paymentInfo.Amount;
                        editRow["discount"] = paymentInfo.Discount;
                        editRow["payment_type"] = paymentInfo.PaymentType;
                        editRow["bank_name"] = paymentInfo.BankName;
                        editRow["check_no"] = paymentInfo.CheckNo;
                        editRow["card_number"] = paymentInfo.CardNumber;
                        editRow["card_type"] = paymentInfo.CardType;
                        editRow["card_expire"] = paymentInfo.CardExpire;

                        editRow.EndEdit();

                        break;
                    }

                    index++;
                }

                _paymentDetailsTable.AcceptChanges();
            }

        } //------------------------------------

        //this procedure inserts a new payment details
        public void DeletePaymentDetails(CommonExchange.SysAccess userInfo, CommonExchange.PaymentDetails paymentInfo)
        {
            using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
            {
                dbLib.DeletePaymentDetails(userInfo, paymentInfo);
            }

            if (_paymentDetailsTable != null)
            {
                Int32 index = 0;

                foreach (DataRow payRow in _paymentDetailsTable.Rows)
                {
                    if (String.Equals(paymentInfo.ReceiptNo, payRow["receipt_no"].ToString()))
                    {
                        DataRow delRow = _paymentDetailsTable.Rows[index];

                        delRow.Delete();

                        break;
                    }

                    index++;
                }

                _paymentDetailsTable.AcceptChanges();
            }

        } //------------------------------------
        #endregion

        #region Programmer-Defined Function Procedures
        //this function returns the patient registration details
        public DataTable SelectByRegistrationIDRegistrationDetails(CommonExchange.SysAccess userInfo, String registrationSysId, Boolean isNewQuery)
        {
            if (isNewQuery)
            {
                using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
                {
                    _registrationDetailsTable = dbLib.SelectByRegistrationIDRegistrationDetails(userInfo, registrationSysId);
                }
            }

            DataTable dbTable = new DataTable("PatientRegistrationDetailsTemp");
            dbTable.Columns.Add("details_id", System.Type.GetType("System.Int64"));
            dbTable.Columns.Add("sysid_procedure", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_administered", System.Type.GetType("System.DateTime"));
            dbTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("remarks", System.Type.GetType("System.String"));
            dbTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));

            if (_registrationDetailsTable != null)
            {
                foreach (DataRow detailsRow in _registrationDetailsTable.Rows)
                {
                    DataRow newRow = dbTable.NewRow();

                    newRow["details_id"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "details_id", Int64.Parse("0"));
                    newRow["sysid_procedure"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "sysid_procedure", "");
                    newRow["date_administered"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "date_administered", DateTime.Parse(s_serverDateTime));
                    newRow["tooth_no"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "tooth_no", "");
                    newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "amount", Decimal.Parse("0"));
                    newRow["remarks"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "remarks", "");
                    newRow["procedure_name"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "procedure_name", "");

                    dbTable.Rows.Add(newRow);
                }

                dbTable.AcceptChanges();
            }

            DataTable regTable = new DataTable("PatientRegistrationDetailsSortByDate");
            regTable.Columns.Add("details_id", System.Type.GetType("System.Int64"));
            regTable.Columns.Add("date_administered", System.Type.GetType("System.String"));
            regTable.Columns.Add("procedure_name", System.Type.GetType("System.String"));
            regTable.Columns.Add("tooth_no", System.Type.GetType("System.String"));
            regTable.Columns.Add("amount", System.Type.GetType("System.String"));

            DataRow[] selectRow = dbTable.Select("", "date_administered DESC");

            foreach (DataRow detailsRow in selectRow)
            {
                DataRow newRow = regTable.NewRow();

                newRow["details_id"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "details_id", Int64.Parse("0"));
                newRow["date_administered"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "date_administered", 
                    DateTime.Parse(s_serverDateTime)).ToLongDateString();
                newRow["procedure_name"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "procedure_name", "");
                newRow["tooth_no"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "tooth_no", "");
                newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "amount", Decimal.Parse("0")).ToString("N");                

                regTable.Rows.Add(newRow);
            }

            regTable.AcceptChanges();

            return regTable;
        } //------------------------------------

        //this function returns the registration details
        public CommonExchange.RegistrationDetails GetRegistrationDetails(Int64 detailsId)
        {
            CommonExchange.RegistrationDetails detailsInfo = new CommonExchange.RegistrationDetails();

            if (_registrationDetailsTable != null)
            {
                String strFilter = "details_id = '" + detailsId.ToString() + "'";
                DataRow[] selectRow = _registrationDetailsTable.Select(strFilter);

                foreach (DataRow detailsRow in selectRow)
                {
                    detailsInfo.DetailsId = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "details_id", Int64.Parse("0"));
                    detailsInfo.ProcedureSystemId = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "sysid_procedure", "");
                    detailsInfo.DateAdministered = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "date_administered", 
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    detailsInfo.ToothNo = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "tooth_no", "");
                    detailsInfo.Amount = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "amount", Decimal.Parse("0"));
                    detailsInfo.Remarks = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "remarks", "");
                    detailsInfo.ProcedureName = DatabaseLib.ProcStatic.DataRowConvert(detailsRow, "procedure_name", "");
                }
            }

            return detailsInfo;
        } //-----------------------------

        //this function returns the payment details table
        public DataTable SelectByRegistrationIDPaymentDetails(CommonExchange.SysAccess userInfo, String registrationSysId, Boolean isNewQuery)
        {
            if (isNewQuery)
            {
                using (DatabaseLib.DbLibRegistrationManager dbLib = new DatabaseLib.DbLibRegistrationManager())
                {
                    _paymentDetailsTable = dbLib.SelectByRegistrationIDPaymentDetails(userInfo, registrationSysId);
                }
            }

            DataTable dbTable = new DataTable("PaymentDetailsTemp");
            dbTable.Columns.Add("receipt_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("date_paid", System.Type.GetType("System.DateTime"));
            dbTable.Columns.Add("amount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("discount", System.Type.GetType("System.Decimal"));
            dbTable.Columns.Add("payment_type", System.Type.GetType("System.Byte"));
            dbTable.Columns.Add("bank_name", System.Type.GetType("System.String"));
            dbTable.Columns.Add("check_no", System.Type.GetType("System.String"));
            dbTable.Columns.Add("card_number", System.Type.GetType("System.String"));
            dbTable.Columns.Add("card_type", System.Type.GetType("System.String"));
            dbTable.Columns.Add("card_expire", System.Type.GetType("System.String"));

            if (_paymentDetailsTable != null)
            {
                foreach (DataRow payRow in _paymentDetailsTable.Rows)
                {
                    DataRow newRow = dbTable.NewRow();

                    newRow["receipt_no"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "receipt_no", "");
                    newRow["date_paid"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "date_paid", DateTime.Parse(s_serverDateTime));
                    newRow["amount"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "amount", Decimal.Parse("0"));
                    newRow["discount"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "discount", Decimal.Parse("0"));
                    newRow["payment_type"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "payment_type", Byte.Parse("0"));
                    newRow["bank_name"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "bank_name", "");
                    newRow["check_no"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "check_no", "");
                    newRow["card_number"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "card_number", "");
                    newRow["card_type"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "card_type", "");
                    newRow["card_expire"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "card_expire", "");

                    dbTable.Rows.Add(newRow);
                }

                dbTable.AcceptChanges();
            }

            DataTable payTable = new DataTable("PaymentDetailsSortByDate");
            payTable.Columns.Add("receipt_no", System.Type.GetType("System.String"));
            payTable.Columns.Add("date_paid", System.Type.GetType("System.String"));
            payTable.Columns.Add("amount_paid", System.Type.GetType("System.String"));
            payTable.Columns.Add("discount", System.Type.GetType("System.String"));
            payTable.Columns.Add("amount_balance", System.Type.GetType("System.String"));

            Decimal amountPayable = this.GetTotalAmountPayable();

            DataRow[] selectRow = dbTable.Select("", "date_paid ASC");

            foreach (DataRow payRow in selectRow)
            {
                DataRow newRow = payTable.NewRow();

                newRow["receipt_no"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "receipt_no", "");
                newRow["date_paid"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "date_paid", DateTime.Parse(s_serverDateTime)).ToLongDateString();
                newRow["amount_paid"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "amount", Decimal.Parse("0")).ToString("N");
                newRow["discount"] = DatabaseLib.ProcStatic.DataRowConvert(payRow, "discount", Decimal.Parse("0")).ToString("N");

                amountPayable = this.GetAmountBalance(amountPayable, DatabaseLib.ProcStatic.DataRowConvert(payRow, "receipt_no", ""));

                newRow["amount_balance"] = amountPayable.ToString("N");

                payTable.Rows.Add(newRow);

            }

            payTable.AcceptChanges();

            return payTable;

        } //-----------------------------------------

        //this function returns the payment details
        public CommonExchange.PaymentDetails GetPaymentDetails(String receiptNo)
        {
            CommonExchange.PaymentDetails paymentInfo = new CommonExchange.PaymentDetails();

            if (_paymentDetailsTable != null)
            {
                String strFilter = "receipt_no = '" + receiptNo + "'";
                DataRow[] selectRow = _paymentDetailsTable.Select(strFilter);

                foreach (DataRow payRow in selectRow)
                {
                    paymentInfo.ReceiptNo = DatabaseLib.ProcStatic.DataRowConvert(payRow, "receipt_no", "");
                    paymentInfo.DatePaid = DatabaseLib.ProcStatic.DataRowConvert(payRow, "date_paid",
                        DateTime.Parse(s_serverDateTime)).ToLongDateString();
                    paymentInfo.Amount = DatabaseLib.ProcStatic.DataRowConvert(payRow, "amount", Decimal.Parse("0"));
                    paymentInfo.Discount = DatabaseLib.ProcStatic.DataRowConvert(payRow, "discount", Decimal.Parse("0"));
                    paymentInfo.PaymentType = DatabaseLib.ProcStatic.DataRowConvert(payRow, "payment_type", Byte.Parse("0"));
                    paymentInfo.BankName = DatabaseLib.ProcStatic.DataRowConvert(payRow, "bank_name", "");
                    paymentInfo.CheckNo = DatabaseLib.ProcStatic.DataRowConvert(payRow, "check_no", "");
                    paymentInfo.CardNumber = DatabaseLib.ProcStatic.DataRowConvert(payRow, "card_number", "");
                    paymentInfo.CardType = DatabaseLib.ProcStatic.DataRowConvert(payRow, "card_type", "");
                    paymentInfo.CardExpire = DatabaseLib.ProcStatic.DataRowConvert(payRow, "card_expire", "");
                }
            }

            return paymentInfo;
        } //----------------------------

        //this function returns the payment summary
        public CommonExchange.PaymentSummary GetPaymentSummary()
        {
            CommonExchange.PaymentSummary paySummary = new CommonExchange.PaymentSummary();

            paySummary.AmountPayable = this.GetTotalAmountPayable();
            paySummary.AmountPaid = 0;
            paySummary.Discount = 0;

            if (_paymentDetailsTable != null)
            {
                foreach (DataRow payRow in _paymentDetailsTable.Rows)
                {
                    Decimal payAmount = 0;
                    Decimal discount = 0;

                    if (Decimal.TryParse(payRow["amount"].ToString(), out payAmount))
                    {
                        paySummary.AmountPaid += payAmount;
                    }

                    if (Decimal.TryParse(payRow["discount"].ToString(), out discount))
                    {
                        paySummary.Discount += discount;
                    }
                }
            }

            paySummary.Balance = paySummary.AmountPayable - (paySummary.AmountPaid + paySummary.Discount);

            return paySummary;
        } //---------------------------        

        //this function returns the amount paid
        public Decimal GetTotalPaymentsMade(String receiptNo)
        {
            Decimal amountPaid = 0;

            if (_paymentDetailsTable != null)
            {
                String strFilter = "receipt_no <> '" + ((String.IsNullOrEmpty(receiptNo)) ? "" : receiptNo).ToString() + "'";
                DataRow[] selectRow = _paymentDetailsTable.Select(strFilter);

                foreach (DataRow payRow in selectRow)
                {
                    Decimal payAmount = 0;
                    Decimal discount = 0;

                    if (Decimal.TryParse(payRow["amount"].ToString(), out payAmount))
                    {
                        amountPaid += payAmount;
                    }

                    if (Decimal.TryParse(payRow["discount"].ToString(), out discount))
                    {
                        amountPaid += discount;
                    }
                }
            }

            return amountPaid;
        } //--------------------------

        //this function returns the total amount payable
        private Decimal GetTotalAmountPayable()
        {
            Decimal amountPayable = 0;

            if (_registrationDetailsTable != null)
            {
                foreach (DataRow detailsRow in _registrationDetailsTable.Rows)
                {
                    Decimal result = 0;

                    if (Decimal.TryParse(detailsRow["amount"].ToString(), out result))
                    {
                        amountPayable += result;
                    }
                }
            }

            return amountPayable;
        } //-----------------------

        //this function returns the amount paid
        private Decimal GetAmountBalance(Decimal amountPayable, String receiptNo)
        {
            Decimal amountPaid = 0;

            if (_paymentDetailsTable != null)
            {
                String strFilter = "receipt_no = '" + receiptNo + "'";
                DataRow[] selectRow = _paymentDetailsTable.Select(strFilter);

                foreach (DataRow payRow in selectRow)
                {
                    Decimal payAmount = 0;
                    Decimal discount = 0;

                    if (Decimal.TryParse(payRow["amount"].ToString(), out payAmount))
                    {
                        amountPaid += payAmount;
                    }

                    if (Decimal.TryParse(payRow["discount"].ToString(), out discount))
                    {
                        amountPaid += discount;
                    }
                }
            }

            return amountPayable - amountPaid;
        } //--------------------------
        #endregion
    }
}
