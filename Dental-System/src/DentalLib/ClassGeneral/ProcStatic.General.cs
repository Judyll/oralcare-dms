using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Data;

namespace DentalLib
{
    [Serializable()]
    public partial class ProcStatic
    {
        #region Programmer-Defined Void Procedures

        //this procedure sets the dataview headers
        public static void SetDataGridViewColumns(DataGridView dgvBase, Boolean useSize)
        {
            Int32 width = 0;

            //general datagridview settings
            dgvBase.ReadOnly = true;
            dgvBase.MultiSelect = false;
            //----------------------

            foreach (DataGridViewColumn column in dgvBase.Columns)
            {
                //general column settings
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                //----------------------------

                //individual column setting
                switch (column.HeaderText)
                {
                    case "sysid_procedure":
                        column.HeaderText = "Procedure ID";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "procedure_name":
                        column.HeaderText = "Procedure Name";
                        column.Width = 400;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case "sysid_patient":
                        column.HeaderText = "Patient ID";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "patient_name":
                        column.HeaderText = "Patient Name";
                        column.Width = 400;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;    
                    case "sysid_registration":
                        column.HeaderText = "Registration ID";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "registration_date":
                        column.HeaderText = "Registration Date";
                        column.Width = 200;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "amount_payable":
                        column.HeaderText = "Amount Payable";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "amount_paid":
                        column.HeaderText = "Amount Paid";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "discount":
                        column.HeaderText = "Discount";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "amount_balance":
                        column.HeaderText = "Balance";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "details_id":
                        column.HeaderText = "System ID";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "date_administered":
                        column.HeaderText = "Date Administered";
                        column.Width = 200;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "amount":
                        column.HeaderText = "Amount";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "receipt_no":
                        column.HeaderText = "Receipt No";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "date_paid":
                        column.HeaderText = "Date Posted";
                        column.Width = 200;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "tooth_no":
                        column.HeaderText = "Tooth No";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                    case "system_user_id":
                        column.HeaderText = "User ID";
                        column.Width = 75;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case "system_user_name":
                        column.HeaderText = "User Name";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case "system_user_full_name":
                        column.HeaderText = "Full Name";
                        column.Width = 400;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case "access_rights":
                        column.HeaderText = "Access Rights";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "system_user_status":
                        column.HeaderText = "Status";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case "position":
                        column.HeaderText = "Position";
                        column.Width = 150;
                        column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    default:
                        column.Visible = false;
                        column.Width = 0;
                        break;
                }

                width += column.Width;
            }

            if (useSize)
            {
                dgvBase.Width = width;
            }

        } //---------------------------

        //this procedure sets the textbox initial message
        public static void TextBoxMessageTip(TextBox txtInput, String strMessage, Boolean showMessage)
        {
            String strInput = txtInput.Text.Trim();

            if (showMessage && (String.Equals(strInput, "") || strInput == null))
            {
                txtInput.Text = strMessage;
                txtInput.Font = new Font(txtInput.Font, FontStyle.Italic);
                txtInput.ForeColor = Color.DarkCyan;
            }
            else
            {
                if (String.Equals(strInput, strMessage) || strInput == null)
                {
                    txtInput.Text = "";
                }

                txtInput.Font = new Font(txtInput.Font, FontStyle.Regular);
                txtInput.ForeColor = Color.Black;
            }

        } //--------------------------

        //this procedure makes the textbox accept on numbers
        public static void TextBoxAmountOnly(TextBox txtInput, KeyPressEventArgs e)
        {
            if (e.KeyChar == '.' || e.KeyChar == ',' || char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                txtInput.ReadOnly = false;
            }
            else
            {
                txtInput.ReadOnly = true;
            }

        } //------------------------------

        //this procedure makes the textbox accept letters only
        public static void TextBoxLettersOnly(TextBox txtInput, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                txtInput.ReadOnly = true;
            }
            else
            {
                txtInput.ReadOnly = false;
            }
        } //-----------------------------

        //this procedure makes the textbox accept integers only
        public static void TextBoxIntegersOnly(TextBox txtInput, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                txtInput.ReadOnly = false;
            }
            else
            {
                txtInput.ReadOnly = true;
            }

        } //------------------------------

        //this procedure validates the textbox
        public static void TextBoxValidateAmount(TextBox txtInput)
        {
            Decimal input;

            if (Decimal.TryParse(txtInput.Text, out input))
            {
                txtInput.Text = input.ToString("N");
            }
            else
            {
                txtInput.Text = "0.00";
            }

        } //--------------------------------------

        //this procedure validates the textbox
        public static void TextBoxValidateInteger(TextBox txtInput)
        {
            Int32 input;

            if (Int32.TryParse(txtInput.Text, out input))
            {
                txtInput.Text = input.ToString();
            }
            else
            {
                txtInput.Text = "0";
            }

        } //--------------------------------------

        //this function deletes a specified directory
        public static void DeleteDirectory(String dirPath)
        {

            DirectoryInfo infoDir = new DirectoryInfo(dirPath);

            if (infoDir.Exists)
            {
                infoDir.Delete(true);
            }

        } //-----------------------------------

        //this function shows an error message
        public static void ShowErrorDialog(String errMsg, String errCaption)
        {
            MessageBox.Show("A business rule has been violated... \nDetails: " + errMsg, errCaption,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        } //-------------------------

        #endregion

        #region Programmer-Defined Function Procedures  
      
        //this function returns the network information
        public static String GetNetworkInformation()
        {
            StringBuilder strNetInfo = new StringBuilder();

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

                strNetInfo.Append("Windows IP Configuration: " + "\n\n");
                strNetInfo.Append("\t" + "Host Name...........................: " + computerProperties.HostName + "\n");
                strNetInfo.Append("\t" + "Domain Name.........................: " + computerProperties.DomainName + "\n");

                if (nics == null || nics.Length < 1)
                {
                    strNetInfo.Append("\n" + "No network interface found.");
                }
                else
                {
                    foreach (NetworkInterface adapter in nics)
                    {
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        IPv4InterfaceProperties ipV4 = properties.GetIPv4Properties();

                        strNetInfo.Append("\n" + adapter.NetworkInterfaceType + " adapter " + adapter.Name + "\n");
                        strNetInfo.Append("\t" + "Description.........................: " + adapter.Description + "\n");
                        strNetInfo.Append("\t" + "Physical Address....................: ");

                        PhysicalAddress address = adapter.GetPhysicalAddress();
                        Byte[] bytes = address.GetAddressBytes();

                        for (int i = 0; i < bytes.Length; i++)
                        {
                            strNetInfo.Append(bytes[i].ToString("X2"));

                            if (i != bytes.Length - 1)
                            {
                                strNetInfo.Append("-");
                            }
                        }

                        strNetInfo.Append("\n");
                        strNetInfo.Append("\t" + "IP Address..........................: ");

                        foreach (UnicastIPAddressInformation ipInfo in properties.UnicastAddresses)
                        {
                            strNetInfo.Append(ipInfo.Address);
                            strNetInfo.Append("   ");
                        }

                        strNetInfo.Append("\n");
                        strNetInfo.Append("\t" + "Gateway Addresses...................: ");

                        foreach (GatewayIPAddressInformation gateway in properties.GatewayAddresses)
                        {
                            strNetInfo.Append(gateway.Address);
                            strNetInfo.Append("   ");
                        }
                        strNetInfo.Append("\n");
                        strNetInfo.Append("\t" + "DNS Servers.........................: ");

                        foreach (IPAddress ipInfo in properties.DnsAddresses)
                        {
                            bytes = ipInfo.GetAddressBytes();

                            for (int i = 0; i < bytes.Length; i++)
                            {
                                strNetInfo.Append(bytes[i].ToString());

                                if (i != bytes.Length - 1)
                                {
                                    strNetInfo.Append(".");
                                }
                            }

                            strNetInfo.Append("   ");
                        }

                        strNetInfo.Append("\n");
                    }
                }

            }
            else
            {
                strNetInfo.Append("Using LOCALHOST network information");
            }

            return strNetInfo.ToString();

        } //----------------------------

        //this function gets the array of bytes of an image
        public static Byte[] GetImageByte(String imagePath)
        {
            if (File.Exists(imagePath))
            {
                FileStream fileStr = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
                BinaryReader binReader = new BinaryReader(fileStr);

                Byte[] imageByte = binReader.ReadBytes((Int32)fileStr.Length);

                fileStr.Close();
                binReader.Close();

                fileStr = null;
                binReader = null;

                return imageByte;
            }
            else
            {
                return null;
            }
            

        } //--------------------------

        //this function trims the special characters
        public static String TrimStartEndString(String strBase)
        {
            if (!String.IsNullOrEmpty(strBase))
            {
                return strBase.TrimStart(null).TrimEnd(null);
            }
            else
            {
                return "";
            }
        } //-----------------------

        //this function gets the complete name
        public static String GetCompleteNameMiddleInitial(DataRow srcRow, String colLName, String colFName, String colMName)
        {
            return DatabaseLib.ProcStatic.DataRowConvert(srcRow, colLName, "").ToUpper() + ", " +
                        DatabaseLib.ProcStatic.DataRowConvert(srcRow, colFName, "") + " " +
                        (String.IsNullOrEmpty(DatabaseLib.ProcStatic.DataRowConvert(srcRow, colMName, "")) ? "" :
                        DatabaseLib.ProcStatic.DataRowConvert(srcRow, colMName, "").Substring(0, 1).ToUpper() + ".");
        } //------------------------------        

        #endregion
    }
}
