using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;

namespace DatabaseLib
{
    [Serializable()]
    public partial class ProcStatic
    {
        #region Programmer-Defined Function Procedures

        //this function determines if the access code is admin
        public static Boolean IsSystemAccessAdmin(CommonExchange.SysAccess userInfo)
        {
            if (String.Equals(userInfo.AccessCode, "@u^xBe$#HygPQ!&^FdCm<?IPaSq%{|!gFcI+_Te@"))
            {
                return true;
            }
            else
            {
                return false;
            }
        } //---------------------------

        //this function determines if the access code is dental user
        public static Boolean IsSystemAccessDentalUser(CommonExchange.SysAccess userInfo)
        {
            if (String.Equals(userInfo.AccessCode, "@%PlMQ[!`;R25*2cX[$6nBQi+cTla#HB^-=QpZs@"))
            {
                return true;
            }
            else
            {
                return false;
            }
        } //--------------------------------

        //this function returns a two digit number
        public static String TwoDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase <= 9)
            {
                strPrefix = "0";
            }

            return strPrefix + numBase.ToString();
        } //-----------------------------------

        //this function returns a three digit number
        public static String ThreeDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 99)
            {
                strPrefix = "";
            }
            else if (numBase > 9)
            {
                strPrefix = "0";
            }
            else
            {
                strPrefix = "00";
            }

            return strPrefix + numBase.ToString();

        } //-------------------------------------

        //this function returns a four digit number
        public static String FourDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 999)
            {
                strPrefix = "";
            }
            else if (numBase > 99)
            {
                strPrefix = "0";
            }
            else if (numBase > 9)
            {
                strPrefix = "00";
            }
            else
            {
                strPrefix = "000";
            }

            return strPrefix + numBase.ToString();

        } //-------------------------------

        //this function return a five digit number
        public static String FiveDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 9999)
            {
                strPrefix = "";
            }
            else if (numBase > 999)
            {
                strPrefix = "0";
            }
            else if (numBase > 99)
            {
                strPrefix = "00";
            }
            else if (numBase > 9)
            {
                strPrefix = "000";
            }
            else
            {
                strPrefix = "0000";
            }

            return strPrefix + numBase.ToString();
        } //---------------------------------------

        //this function return a six digit number
        public static String SixDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 99999)
            {
                strPrefix = "";
            }
            else if (numBase > 9999)
            {
                strPrefix = "0";
            }
            else if (numBase > 999)
            {
                strPrefix = "00";
            }
            else if (numBase > 99)
            {
                strPrefix = "000";
            }
            else if (numBase > 9)
            {
                strPrefix = "0000";
            }
            else
            {
                strPrefix = "00000";
            }

            return strPrefix + numBase.ToString();
        } //---------------------------------------

        //this function return a seven digit number
        public static String SevenDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 999999)
            {
                strPrefix = "";
            }
            else if (numBase > 99999)
            {
                strPrefix = "0";
            }
            else if (numBase > 9999)
            {
                strPrefix = "00";
            }
            else if (numBase > 999)
            {
                strPrefix = "000";
            }
            else if (numBase > 99)
            {
                strPrefix = "0000";
            }
            else if (numBase > 9)
            {
                strPrefix = "00000";
            }
            else
            {
                strPrefix = "000000";
            }

            return strPrefix + numBase.ToString();
        } //---------------------------------------

        //this function return a eight digit number
        public static String EightDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 9999999)
            {
                strPrefix = "";
            }
            else if (numBase > 999999)
            {
                strPrefix = "0";
            }
            else if (numBase > 99999)
            {
                strPrefix = "00";
            }
            else if (numBase > 9999)
            {
                strPrefix = "000";
            }
            else if (numBase > 999)
            {
                strPrefix = "0000";
            }
            else if (numBase > 99)
            {
                strPrefix = "00000";
            }
            else if (numBase > 9)
            {
                strPrefix = "000000";
            }
            else
            {
                strPrefix = "0000000";
            }

            return strPrefix + numBase.ToString();
        } //---------------------------------------

        //this function return a nine digit number
        public static String NineDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 99999999)
            {
                strPrefix = "";
            }
            else if (numBase > 9999999)
            {
                strPrefix = "0";
            }
            else if (numBase > 999999)
            {
                strPrefix = "00";
            }
            else if (numBase > 99999)
            {
                strPrefix = "000";
            }
            else if (numBase > 9999)
            {
                strPrefix = "0000";
            }
            else if (numBase > 999)
            {
                strPrefix = "00000";
            }
            else if (numBase > 99)
            {
                strPrefix = "000000";
            }
            else if (numBase > 9)
            {
                strPrefix = "0000000";
            }
            else
            {
                strPrefix = "00000000";
            }

            return strPrefix + numBase.ToString();
        } //---------------------------------------

        //this function return a ten digit number
        public static String TenDigitZero(Int32 numBase)
        {
            String strPrefix = "";

            if (numBase > 999999999)
            {
                strPrefix = "";
            }
            else if (numBase > 99999999)
            {
                strPrefix = "0";
            }
            else if (numBase > 9999999)
            {
                strPrefix = "00";
            }
            else if (numBase > 999999)
            {
                strPrefix = "000";
            }
            else if (numBase > 99999)
            {
                strPrefix = "0000";
            }
            else if (numBase > 9999)
            {
                strPrefix = "00000";
            }
            else if (numBase > 999)
            {
                strPrefix = "000000";
            }
            else if (numBase > 99)
            {
                strPrefix = "0000000";
            }
            else if (numBase > 9)
            {
                strPrefix = "00000000";
            }
            else
            {
                strPrefix = "000000000";
            }

            return strPrefix + numBase.ToString();
        } //---------------------------------------
        
        //this function converts a datarow to strings
        public static String DataRowConvert(DataRow rowName, String columnName, String defaultValue)
        {
            if ((rowName[columnName] != null) && !(String.IsNullOrEmpty(rowName[columnName].ToString().Trim())))
            {
                return rowName[columnName].ToString();
            }
            else
            {
                return defaultValue;
            }

        } //----------------------------

        //this function converts a datarow to datetime
        public static DateTime DataRowConvert(DataRow rowName, String columnName, DateTime defaultValue)
        {
            DateTime result;

            if ((rowName[columnName] != null) && !(String.IsNullOrEmpty(rowName[columnName].ToString().Trim())) &&
                DateTime.TryParse(rowName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------

        //this function converts a datarow to decimal
        public static Decimal DataRowConvert(DataRow rowName, String columnName, Decimal defaultValue)
        {
            Decimal result;

            if ((rowName[columnName] != null) && !(String.IsNullOrEmpty(rowName[columnName].ToString().Trim())) &&
                Decimal.TryParse(rowName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------

        //this function converts a datarow to byte
        public static Byte DataRowConvert(DataRow rowName, String columnName, Byte defaultValue)
        {
            Byte result;

            if ((rowName[columnName] != null) && !(String.IsNullOrEmpty(rowName[columnName].ToString().Trim())) &&
                Byte.TryParse(rowName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------

        //this function converts a datarow to byte
        public static Int16 DataRowConvert(DataRow rowName, String columnName, Int16 defaultValue)
        {
            Int16 result;

            if ((rowName[columnName] != null) && !(String.IsNullOrEmpty(rowName[columnName].ToString().Trim())) &&
                Int16.TryParse(rowName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------

        //this function converts a datarow to byte
        public static Int64 DataRowConvert(DataRow rowName, String columnName, Int64 defaultValue)
        {
            Int64 result;

            if ((rowName[columnName] != null) && !(String.IsNullOrEmpty(rowName[columnName].ToString().Trim())) &&
                Int64.TryParse(rowName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------


        //this function converts a datarow to byte
        public static Boolean DataRowConvert(DataRow rowName, String columnName, Boolean defaultValue)
        {
            Boolean result;

            if ((rowName[columnName] != null) && !(String.IsNullOrEmpty(rowName[columnName].ToString().Trim())) &&
                Boolean.TryParse(rowName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------

        //this function converts a datareader to string
        public static String DataReaderConvert(SqlDataReader readerName, String columnName, String defaultValue)
        {
            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())))
            {
                return readerName[columnName].ToString();
            }
            else
            {
                return defaultValue;
            }
        } //-----------------------------

        //this function converts a datareader to datetime
        public static DateTime DataReaderConvert(SqlDataReader readerName, String columnName, DateTime defaultValue)
        {
            DateTime result;

            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())) && 
                DateTime.TryParse(readerName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------------

        //this function converts a datareader to Int64
        public static Int64 DataReaderConvert(SqlDataReader readerName, String columnName, Int64 defaultValue)
        {
            Int64 result;

            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())) && 
                Int64.TryParse(readerName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------------

        //this function converts a datareader to Int32
        public static Int32 DataReaderConvert(SqlDataReader readerName, String columnName, Int32 defaultValue)
        {
            Int32 result;

            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())) && 
                Int32.TryParse(readerName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------------

        //this function converts a datareader to Boolean
        public static Boolean DataReaderConvert(SqlDataReader readerName, String columnName, Boolean defaultValue)
        {
            Boolean result;

            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())) && 
                Boolean.TryParse(readerName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------------

        //this function converts a datareader to Decimal
        public static Decimal DataReaderConvert(SqlDataReader readerName, String columnName, Decimal defaultValue)
        {
            Decimal result;

            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())) &&
                Decimal.TryParse(readerName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------------

        //this function converts a datareader to Byte
        public static Byte DataReaderConvert(SqlDataReader readerName, String columnName, Byte defaultValue)
        {
            Byte result;

            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())) &&
                Byte.TryParse(readerName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------------

        //this function converts a datareader to Byte
        public static Int16 DataReaderConvert(SqlDataReader readerName, String columnName, Int16 defaultValue)
        {
            Int16 result;

            if ((readerName[columnName] != null) && !(String.IsNullOrEmpty(readerName[columnName].ToString().Trim())) &&
                Int16.TryParse(readerName[columnName].ToString(), out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }

        } //-----------------------------


        #endregion
    }
}
