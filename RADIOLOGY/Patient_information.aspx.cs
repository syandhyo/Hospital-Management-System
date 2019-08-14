using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


public partial class RADIOLOGY_Patient_information : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
        if (!IsPostBack)
        {
            
            BindDoctorname();
            BindRlogyTest();
        }

    }
    private void BindGrid()
    {
        string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("select RPH.Patient_Id,AT.NAME,ts.Sname from RDO_PTINF_HDR RPH inner join ADMISSION_TABLE AT on AT.ID = RPH.Patient_Id or AT.VN = RPH.Patient_Id  inner join tblStaff ts on ts.EMPID = RPH.DoctorName ", con);
        DataSet ds = new DataSet();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridView2.DataSource = ds.Tables[0];
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
    
    }
    private void BindDoctorname()
    {
        string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("select Sname,EMPID from tblStaff", con);
        DataSet ds = new DataSet();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            dropdoctorname.DataTextField = ds.Tables[0].Columns["Sname"].ToString();
            dropdoctorname.DataValueField = ds.Tables[0].Columns["EMPID"].ToString();
            dropdoctorname.DataSource = ds.Tables[0];
            dropdoctorname.DataBind();
            dropdoctorname.Items.Insert(0, "---Select---");

        }
    }

    private void BindRlogyTest()
    {
        string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("select INV,slno from RADIOLOGY_COMPONENT_TABLE", con);
        DataSet ds = new DataSet();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            drpRadiologyTest.DataTextField = ds.Tables[0].Columns["INV"].ToString();
            drpRadiologyTest.DataValueField = ds.Tables[0].Columns["slno"].ToString();
            drpRadiologyTest.DataSource = ds.Tables[0];
            drpRadiologyTest.DataBind();
            drpRadiologyTest.Items.Insert(0, "---Select---");

        }
    }

    [WebMethod]
    public static string[] GetAutoCompleteData(string prefix)
    {
        List<string> result = new List<string>();
        //DataSet DS_GROUP = DA_LIB.Class1.SELECT_MASTER(bookname, "", "", "", "", "", "", "", "", "", 102);
        string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("select ID,NAME,PADDRESS from ADMISSION_TABLE where ID like @searchtext+'%'", con);
        cmd.Parameters.AddWithValue("@searchtext", prefix);
        DataSet DS_GROUP = new DataSet();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(DS_GROUP);

        if (DS_GROUP.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < DS_GROUP.Tables[0].Rows.Count; i++)
            {
                result.Add(string.Format("{0}/{1}/{2}", DS_GROUP.Tables[0].Rows[i]["ID"].ToString(), DS_GROUP.Tables[0].Rows[i]["NAME"].ToString(), DS_GROUP.Tables[0].Rows[i]["PADDRESS"].ToString()));

            }
        }
        return result.ToArray();
    }
    [WebMethod]
    public static string[] GetAutoCompleteData2(string prefix)
    {
        List<string> result = new List<string>();
        //DataSet DS_GROUP = DA_LIB.Class1.SELECT_MASTER(bookname, "", "", "", "", "", "", "", "", "", 102);
        string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("select VN,NAME,PADDRESS from ADMISSION_TABLE where VN like @searchtext+'%'", con);
        cmd.Parameters.AddWithValue("@searchtext", prefix);
        DataSet DS_GROUP = new DataSet();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(DS_GROUP);

        if (DS_GROUP.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < DS_GROUP.Tables[0].Rows.Count; i++)
            {
                result.Add(string.Format("{0}/{1}/{2}", DS_GROUP.Tables[0].Rows[i]["VN"].ToString(), DS_GROUP.Tables[0].Rows[i]["NAME"].ToString(), DS_GROUP.Tables[0].Rows[i]["PADDRESS"].ToString()));

            }
        }
        return result.ToArray();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox1.Checked == true)
        {
            txtDoctorName.Visible = true;
        }
        else
        {
            txtDoctorName.Visible = false;
        }
    }
    protected void drpRadiologyTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("select RCT.INV,RPT.Price from RADIOLOGY_COMPONENT_TABLE RCT inner join RADIOLOGY_PRICE_TABLE RPT ON RCT.slno = RPT.ID WHERE RCT.slno = '" + drpRadiologyTest.SelectedValue.ToString()+ "'", con);
        DataSet ds = new DataSet();
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        sda.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblRate.Text = ds.Tables[0].Rows[0]["Price"].ToString();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        dt.Columns.Add("TestName");
        dt.Columns.Add("Note");
        dt.Columns.Add("Price");
        //First fill all the data present in the grid
        for (int intCnt = 0; intCnt < GridView1.Rows.Count; intCnt++)
        {
            int slno = GridView1.Rows.Count + 1;

            if (GridView1.Rows[intCnt].RowType == DataControlRowType.DataRow)
            {
                dr = dt.NewRow();
                TextBox tn = (TextBox)GridView1.Rows[intCnt].FindControl("txtTestName");
                TextBox tnote = (TextBox)GridView1.Rows[intCnt].FindControl("txtNote");
                TextBox tp = (TextBox)GridView1.Rows[intCnt].FindControl("txtPrice");
                dr["TestName"] = tn.Text;
                dr["Note"] = tnote.Text;
                dr["Price"] = tp.Text;
                dt.Rows.Add(dr);
            }
        }
        dr = dt.NewRow();
        dr["TestName"] = drpRadiologyTest.SelectedItem.ToString();
        dr["Note"] = txtDetailNote.Text;
        dr["Price"] = lblRate.Text;
        dt.Rows.Add(dr);
        if (dt.Rows.Count > 0)
        {
            ViewState["PARTICULARS"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();
           // GridView1.Visible = true;
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            //GridView1.Visible = false;
        }
        drpRadiologyTest.SelectedIndex = -1;
        txtDetailNote.Text = lblRate.Text = "";
        GrandTotal();
    }
    private void GrandTotal()
    {
        decimal GTotal = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            //String total = (GridView1.Rows[i].FindControl("txtPrice") as Label).Text;
            TextBox total = (TextBox)GridView1.Rows[i].Cells[0].FindControl("txtPrice");
            string s = total.Text;
            GTotal += Convert.ToDecimal(s);
        }
        txtTotAmount.Text = GTotal.ToString();
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        dt.Clear();
        GridView1.DataSource = null;
        GridView1.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //
        string barCode = txtBarcode.Text;
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

        //
        string cs = ConfigurationManager.ConnectionStrings["abcd"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);
        con.Open();
        SqlCommand cmd = new SqlCommand("SP_PATIENT_INFN", con);
        cmd.CommandType = CommandType.StoredProcedure;
        if(txtPatientId.Text != "")
        {
        cmd.Parameters.AddWithValue("@Patient_Id",txtPatientId.Text);
        }
        if(txtPatientIpd.Text != "")
        {
         cmd.Parameters.AddWithValue("@Patient_Id",txtPatientIpd.Text);
        }
        cmd.Parameters.AddWithValue("@Barcode",txtBarcode.Text);
       // string str = dropdoctorname.SelectedItem.ToString();
        if (dropdoctorname.SelectedItem.ToString() != "---Select---")
        {
            cmd.Parameters.AddWithValue("@DoctorName", dropdoctorname.SelectedValue.ToString());
        }
        else 
        {
            cmd.Parameters.AddWithValue("@DoctorName",txtDoctorName.Text);
        }
        cmd.Parameters.AddWithValue("@Price_BefDis", txtTotAmount.Text);
        cmd.Parameters.AddWithValue("@Dis_inPer", txtDiscount.Text);
        cmd.Parameters.AddWithValue("@Dis_Amt", lblDiscountamt.Text);
        cmd.Parameters.AddWithValue("@Price_AftDis", txtTotalAmount.Text);
        
        cmd.Parameters.AddWithValue("@FLAG",1);
        cmd.ExecuteNonQuery();
        con.Close();

        for (int i = 1; i < GridView1.Rows.Count + 1; i++)
        {
            //Label g = (Label)GridView1.Rows[i - 1].FindControl("lblITEMID");
            //TextBox sn = (TextBox)GridView1.Rows[i - 1].FindControl("txtSerialNo");
            TextBox tn = (TextBox)GridView1.Rows[i - 1].FindControl("txtTestName");
            TextBox tnote = (TextBox)GridView1.Rows[i - 1].FindControl("txtNote");
            TextBox tprice = (TextBox)GridView1.Rows[i - 1].FindControl("txtPrice");
            con.Open();
            //SqlCommand cmd1 = new SqlCommand("insert into S_SRVC_ORD_DETAILS(So_No,Wo_Id,Serial_No,Date,Vehicle_No,Time) values(@So_No,@Wo_Id,@Serial_No,@Date,@Vehicle_No,@Time)", con);
            SqlCommand cmd1 = new SqlCommand("SP_PATIENT_INFN", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@TEST", tn.Text);
            cmd1.Parameters.AddWithValue("@NOTE", tnote.Text);
            cmd1.Parameters.AddWithValue("@PRICE", decimal.Parse(tprice.Text));
            cmd1.Parameters.AddWithValue("@FLAG", 2);
            cmd1.ExecuteNonQuery();
            con.Close();
            BindGrid();
            // DA.Class1.C_INSERT_MASTER(g.Text, gvProduct.Rows[i - 1].Cells[2].Text, gvProduct.Rows[i - 1].Cells[3].Text, gvProduct.Rows[i - 1].Cells[4].Text, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 31);

        }
    }
    protected void lbGenerateBill_Click(object sender, EventArgs e)
    {
       // GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        LinkButton lb1 = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb1.NamingContainer;
        Session["RADID"] = row.Cells[0].Text;
        Response.Redirect("~/RADIOLOGY/Radiology2nd.aspx");
    }
    protected void txtAddress_TextChanged(object sender, EventArgs e)
    {
        //string str1 = ddlPatientCategory.SelectedValue.ToString();
        //string str2 = lblPatientName.Text.Substring(0, 3);
        //string str3 = txtPatientIpd.Text;
        if (ddlPatientCategory.SelectedValue.ToString() == "IPD")
        {
            txtBarcode.Text = ddlPatientCategory.SelectedValue.ToString() + lblPatientName.Text.Substring(0, 3) + txtPatientIpd.Text;
        }
        if (ddlPatientCategory.SelectedValue.ToString() == "OPD")
        {
            txtBarcode.Text = ddlPatientCategory.SelectedValue.ToString() + lblPatientName.Text.Substring(0, 3) + txtPatientId.Text;
        }
    }
    protected void txtDiscount_TextChanged(object sender, EventArgs e)
    {
        string price = txtTotAmount.Text;
        string disc = txtDiscount.Text;
        decimal discamt = (Convert.ToDecimal(price) * Convert.ToDecimal(disc)) / 100;
        decimal totPrice = Convert.ToDecimal(price) - discamt;
        lblDiscountamt.Text = discamt.ToString();
        txtTotalAmount.Text = Convert.ToString(Math.Round(totPrice));

    }
}