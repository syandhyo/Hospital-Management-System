using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;



public partial class RADIOLOGY_radio_bill : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("SP_PATIENT_INFN", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Patient_Id", Session["RADID"].ToString());
            cmd.Parameters.Add("@FLAG", SqlDbType.Int).Value = 3;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da = new SqlDataAdapter("select A.ID,'OUTPATIENT' AS PTYPE,A.PNAME AS NAME,A.AGE,A.GENDER,A.MOBNO AS PHONE,A.GURDINMOBLE AS ECONTACT,A.EMAILID AS EMAIL,'' AS PREF,A.DATETIME AS DATE,A.ADDRES AS PADDRESS,'' AS TADDRESS,A.CHARGES AS RGFEE,'' AS DISEASE,B.NAME AS ORGNAME,B.PHONE AS ORGPHONE,B.GSTNO,B.ADDRESS AS ADDRESS,A.UHID AS UHNID FROM REGISTRATION_TBL A, ORG_TABLE B WHERE  A.ID='" + S.ToString() + "'", con);
            DataSet ds = new DataSet();
            //DataTable dt1 = new DataTable();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();  
            }

            lblOrgName.Text = ds.Tables[0].Rows[0]["orgName"].ToString();
            lblAddress.Text = ds.Tables[0].Rows[0]["ADDRESS"].ToString();
            lblPhone.Text = ds.Tables[0].Rows[0]["PHONE"].ToString();
            lblIpdopd.Text = ds.Tables[0].Rows[0]["Patient_Id"].ToString(); //patient_name
            lblPatientName.Text = ds.Tables[0].Rows[0]["patient_name"].ToString();
            lblDate.Text = ds.Tables[0].Rows[0]["Date"].ToString();

            lblDiscountamt.Text = ds.Tables[0].Rows[0]["Dis_Amt"].ToString();
            txtTotalAmount.Text = ds.Tables[0].Rows[0]["Price_AftDis"].ToString();
            txtPreparedBy.Text = ds.Tables[0].Rows[0]["doctor_name"].ToString();
            txtTotalAmount2.Text = ds.Tables[0].Rows[0]["Price_BefDis"].ToString();
            lblCurword.Text = CurrencyToWord(txtTotalAmount.Text);
            
            string barCode = ds.Tables[0].Rows[0]["Barcode"].ToString();

           // string barCode = txtBarcode.Text;
            //string barCode = txtCode.Text;
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
            {
                using (Graphics graphics = Graphics.FromImage(bitMap))
                {
                    Font oFont = new Font("IDAutomationHC39M", 16);
                    PointF point = new PointF(2f, 2f);
                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();

                    Convert.ToBase64String(byteImage);
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                }
                plBarCode.Controls.Add(imgBarCode);
            }
        }
    }
    public string CurrencyToWord(string number)
    {

        string temp;
        string rupees, paisa;
        rupees = paisa = string.Empty;
        int decimalPlace, count;
        string hundreds, words;
        hundreds = words = string.Empty;
        string[] places = new string[9];
        places[0] = "Thousand";
        places[2] = "Lakh";
        places[4] = "Crore";
        places[6] = "Arab";
        places[8] = "Kharab";
        if (number.Split('.').Count() < 2)
            number = number + ".00";

        string[] no = number.Split('.');
        decimalPlace = number.IndexOf('.');
        if (Convert.ToInt32(no[1]) > 0)
        {
            if (decimalPlace > 0)
            {
                temp = no[1].PadLeft(2, '0');
                paisa = " and " + ConvertTens(temp).Trim() + " Paisa";
            }
        }
        no[0] = no[0].PadLeft(3, '0');
        hundreds = ConvertHundreds(no[0].Substring(no[0].Length - 3));
        no[0] = no[0].Substring(0, no[0].Length - 3);
        count = 0;
        while (no[0] != string.Empty)
        {
            if (no[0].Length == 1)
            {
                if (words == " Thousand ")
                {
                    words = ConvertDigit(no[0]) + " " + places[count];
                    no[0] = no[0].Substring(0, no[0].Length - 1);
                }
                else if (words == " Lakh  Thousand ")
                {
                    words = ConvertDigit(no[0]) + " " + places[count];
                    no[0] = no[0].Substring(0, no[0].Length - 1);
                }
                else
                {
                    words = ConvertDigit(no[0]) + " " + places[count] + " " + words;
                    no[0] = no[0].Substring(0, no[0].Length - 1);
                }
            }
            else
            {
                temp = no[0].Substring(no[0].Length - 2);
                if (Convert.ToInt32(temp) > 0)
                {
                    words = ConvertTens(temp) + " " + places[count] + " " + words;
                }
                no[0] = no[0].Substring(0, no[0].Length - 2);
            }
            count += 2;
        }
        return words.Trim() + " " + hundreds.Trim() + " " + paisa.Trim() + " Only";
    }
    public string ConvertHundreds(string value)
    {
        string result = string.Empty;
        if (decimal.Parse(value) == 0)
            return string.Empty;
        value = value.PadRight(3, '0');
        if (int.Parse(value.Substring(0, 1)) != 0)
            result = ConvertDigit(value.Substring(0, 1)) + " Hundred";
        if (int.Parse(value.Substring(1, 1)) != 0)
            result += " " + ConvertTens(value.Substring(1, 2));
        else
            result += " " + ConvertDigit(value.Substring(2, 1));
        return result.Trim();
    }
    public string ConvertTens(string value)
    {
        string result = string.Empty;
        try
        {
            if (decimal.Parse(value.Substring(0, 1)) == 1)
            {
                switch (int.Parse(value))
                {
                    case 10: result = "Ten"; break;
                    case 11: result = "Eleven"; break;
                    case 12: result = "Twelve"; break;
                    case 13: result = "Thirteen"; break;
                    case 14: result = "Fourteen"; break;
                    case 15: result = "Fifteen"; break;
                    case 16: result = "Sixteen"; break;
                    case 17: result = "Seventeen"; break;
                    case 18: result = "Eighteen"; break;
                    case 19: result = "Nineteen"; break;
                }
            }
            else
            {
                switch (int.Parse(value.Substring(0, 1)))
                {
                    case 2: result = "Twenty"; break;
                    case 3: result = "Thirty"; break;
                    case 4: result = "Fourty"; break;
                    case 5: result = "Fifty"; break;
                    case 6: result = "Sixty"; break;
                    case 7: result = "Seventy"; break;
                    case 8: result = "Eighty"; break;
                    case 9: result = "Ninety"; break;
                }
                result += " " + ConvertDigit(value.Substring(1, 1));
            }
        }
        catch
        {
        }
        return result.Trim();
    }
    private string ConvertDigit(string p)
    {
        if (p == "-")
        {
            p = "0";
        }
        switch (int.Parse(p))
        {
            case 1: return "One";
            case 2: return "Two";
            case 3: return "Three";
            case 4: return "Four";
            case 5: return "Five";
            case 6: return "Six";
            case 7: return "Seven";
            case 8: return "Eight";
            case 9: return "Nine";
            default: return string.Empty;
        }

    }
}