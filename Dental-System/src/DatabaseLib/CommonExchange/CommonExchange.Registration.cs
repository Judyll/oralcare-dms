using System;
using System.Collections.Generic;
using System.Text;

namespace CommonExchange
{
    #region Common Structure Exchange

    [Serializable()]
    public struct Registration
    {
        private String _registrationSystemId;
        public String RegistrationSystemId
        {
            get { return _registrationSystemId; }
            set { _registrationSystemId = value; }
        }

        private String _patientSystemId;
        public String PatientSystemId
        {
            get { return _patientSystemId; }
            set { _patientSystemId = value; }
        }

        private String _registrationDate;
        public String RegistrationDate
        {
            get { return _registrationDate; }
            set { _registrationDate = value; }
        }

        private String _medicalPrescription;
        public String MedicalPrescription
        {
            get { return _medicalPrescription; }
            set { _medicalPrescription = value; }
        }
    }

    [Serializable()]
    public struct RegistrationDetails
    {
        private Int64 _detailsId;
        public Int64 DetailsId
        {
            get { return _detailsId; }
            set { _detailsId = value; }
        }

        private String _registrationSystemId;
        public String RegistrationSystemId
        {
            get { return _registrationSystemId; }
            set { _registrationSystemId = value; }
        }

        private String _procedureSystemId;
        public String ProcedureSystemId
        {
            get { return _procedureSystemId; }
            set { _procedureSystemId = value; }
        }

        private String _procedureName;
        public String ProcedureName
        {
            get { return _procedureName; }
            set { _procedureName = value; }
        }

        private String _dateAdministered;
        public String DateAdministered
        {
            get { return _dateAdministered; }
            set { _dateAdministered = value; }
        }

        private String _toothNo;
        public String ToothNo
        {
            get { return _toothNo; }
            set { _toothNo = value; }
        }

        private Decimal _amount;
        public Decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private String _remarks;
        public String Remarks
        {
            get { return _remarks; }
            set { _remarks = value; }
        }

    }

    [Serializable()]
    public struct PaymentDetails
    {
        private String _receiptNo;
        public String ReceiptNo
        {
            get { return _receiptNo; }
            set { _receiptNo = value; }
        }

        private String _registrationSystemId;
        public String RegistrationSystemId
        {
            get { return _registrationSystemId; }
            set { _registrationSystemId = value; }
        }

        private String _datePaid;
        public String DatePaid
        {
            get { return _datePaid; }
            set { _datePaid = value; }
        }

        private Decimal _amount;
        public Decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private Decimal _discount;
        public Decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        private Byte _paymentType;
        public Byte PaymentType
        {
            get { return _paymentType; }
            set { _paymentType = value; }
        }

        private String _bankName;
        public String BankName
        {
            get { return _bankName; }
            set { _bankName = value; }
        }

        private String _checkNo;
        public String CheckNo
        {
            get { return _checkNo; }
            set { _checkNo = value; }
        }

        private String _cardNumber;
        public String CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        private String _cardType;
        public String CardType
        {
            get { return _cardType; }
            set { _cardType = value; }
        }

        private String _cardExpire;
        public String CardExpire
        {
            get { return _cardExpire; }
            set { _cardExpire = value; }
        }
    }

    [Serializable()]
    public struct PaymentSummary
    {
        private Decimal _amountPayable;
        public Decimal AmountPayable
        {
            get { return _amountPayable; }
            set { _amountPayable = value; }
        }

        private Decimal _amountPaid;
        public Decimal AmountPaid
        {
            get { return _amountPaid; }
            set { _amountPaid = value; }
        }

        private Decimal _discount;
        public Decimal Discount
        {
            get { return _discount; }
            set { _discount = value; }
        }

        private Decimal _balance;
        public Decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }
    }

    [Serializable()]
    public enum PaymentType
    {
        Cash = 0,
        Check = 1,
        CreditCard = 2
    }
    #endregion
}
