using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace DentalLib
{
    public class Patient
    {
        #region Class General Variable Declarations
        private String _imagePath;
        #endregion

        #region Class Properties Declarations
        protected static String s_serverDateTime;
        public String ServerDateTime
        {
            get { return s_serverDateTime; }
        }       

        #endregion

        #region Class Constructor
        public Patient(CommonExchange.SysAccess userInfo)
        {
            this.InitializeClass(userInfo);

            _imagePath = "\\PatientImages";
        }
        #endregion

        #region Programmer-Defined Void Procedures

        //this procedure deletes the image directory
        public void DeleteImageDirectory(String startUp)
        {
            String imagePath = startUp + _imagePath;

            DentalLib.ProcStatic.DeleteDirectory(imagePath);

        } //--------------------

        //this procedure initializes the class
        private void InitializeClass(CommonExchange.SysAccess userInfo)
        {
            //gets the server date and time
            using (DatabaseLib.DbLibGeneral dbLib = new DatabaseLib.DbLibGeneral())
            {
                s_serverDateTime = dbLib.GetServerDateTime(userInfo.Connection);
            } //----------------------

        } //------------------------------

        #endregion

        #region Programmer-Defined Function Procedures

        //this function returns a selected patient information by patient id
        public CommonExchange.Patient SelectByPatientSystemIdPatientInformation(CommonExchange.SysAccess userInfo, String patientId, String startUp)
        {
            CommonExchange.Patient patientInfo = new CommonExchange.Patient();

            using (DatabaseLib.DbLibPatientManager dbLib = new DatabaseLib.DbLibPatientManager())
            {
                patientInfo = dbLib.SelectByPatientSystemIdPatientInformation(userInfo, patientId);
                patientInfo.ImagePath = this.GetEmployeeImagePath(userInfo, patientId, startUp);
                patientInfo.ImageBytes = DentalLib.ProcStatic.GetImageByte(patientInfo.ImagePath);
                patientInfo.ImageExtension = this.GetImageExtensionName(patientInfo.ImagePath);
                patientInfo.Age = this.GetPatientAge(DateTime.Parse(patientInfo.BirthDate), DateTime.Parse(s_serverDateTime));
            }

            return patientInfo;
        } //-----------------------

        //this function returns the extension name of the file
        public String GetImageExtensionName(String imagePath)
        {
            String strExt = "";

            if (File.Exists(imagePath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(imagePath);
                strExt = dirInfo.Extension;
            }

            return strExt;
        } //----------------------------------

        //this function gets the employee image path
        private String GetEmployeeImagePath(CommonExchange.SysAccess userInfo, String patientId, String startUp)
        {
            String imagePath = startUp + _imagePath;

            if (!Directory.Exists(imagePath))
            {
                //creates the directory
                DirectoryInfo dirInfo = new DirectoryInfo(imagePath);
                dirInfo.Create();
                dirInfo.Attributes = FileAttributes.Hidden;
            }            

            DataTable imageTable;

            using (DatabaseLib.DbLibPatientManager dbLib = new DatabaseLib.DbLibPatientManager())
            {
                imageTable = dbLib.SelectImagePatientInformation(userInfo, patientId);
            }

            using (DataTableReader tableReader = new DataTableReader(imageTable))
            {
                if (tableReader.HasRows)
                {
                    Int32 picColumn = 2;

                    while (tableReader.Read())
                    {
                        if (!tableReader.IsDBNull(picColumn))
                        {
                            imagePath += "\\" + tableReader["sysid_patient"].ToString() + tableReader["extension_name"].ToString();

                            if (!File.Exists(imagePath))
                            {
                                Int64 len = tableReader.GetBytes(picColumn, 0, null, 0, 0);
                                // Create a buffer to hold the bytes, and then
                                // read the bytes from the DataTableReader.
                                Byte[] buffer = new Byte[len];
                                tableReader.GetBytes(picColumn, 0, buffer, 0, (Int32)len);

                                // Create a new Bitmap object, passing the array 
                                // of bytes to the constructor of a MemoryStream.
                                using (Bitmap image = new Bitmap(new MemoryStream(buffer)))
                                {
                                    image.Save(imagePath);
                                }
                            }
                            
                        }
                    }
                }
                else
                {
                    imagePath = null;
                }

                tableReader.Close();
            }

            return imagePath;

        } //------------------------------

        private String GetPatientAge(DateTime dateBday, DateTime dateToday)
        {
            Int32 diffYear = dateToday.Year - dateBday.Year;
            Int32 diffMonth = 0;
            Int32 diffDay = 0;

            if (DateTime.Compare(dateToday, dateBday.AddYears(diffYear)) < 0 && diffYear != 0)
            {
                diffYear--;
            }

            dateBday = dateBday.AddYears(diffYear);

            if (dateBday.Year == dateToday.Year)
            {
                diffMonth = dateToday.Month - dateBday.Month;
            }
            else
            {
                diffMonth = (dateToday.Month + 12) - dateBday.Month;
            }

            if (DateTime.Compare(dateToday, dateBday.AddMonths(diffMonth)) < 0 && diffMonth != 0)
            {
                diffMonth--;
            }

            dateBday = dateBday.AddMonths(diffMonth);

            diffDay = (dateToday - dateBday).Days;

            return diffYear.ToString() + "Y" + diffMonth.ToString() + "M" + diffDay.ToString() + "D";
        }

        #endregion
    }
}
