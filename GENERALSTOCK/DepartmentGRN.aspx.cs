using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

public partial class GENERALSTOCK_DepartmentGRN : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["out"] == "INACTIVE")
            {
                Response.Redirect("~/index.aspx");
            }
            Response.Buffer = true;

            Response.CacheControl = "no-cache";
            if (Session["NAME"] == null)
            {
                Response.Redirect("~/index.aspx");
            }
            lblid.Text = Session["NAME"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();
            lblGRNNoS.Text = Session["MRINDID"].ToString();
            if (!IsPostBack)
            {
                auto();
                BindMin();
                BindGrnNo();
                binddYear();
                bindGridItem();
                BindDepartment();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void BindDepartment()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter Adp = new SqlDataAdapter("select id,DeptName from tblDepartment ", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            ddDeptname.DataSource = Dt;
            ddDeptname.DataTextField = "DeptName";
            ddDeptname.DataValueField = "id";
            ddDeptname.DataBind();

            //ddDeptname.Items.Insert(0, "Please Select");
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void BindMin()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
           // SqlCommand COM = new SqlCommand("SELECT a.ID,a.MRNO,CONVERT(varchar, MINDATE, 105) as MINDATE,a.ISSUEDTO,b.DeptName,b.id as did FROM MIN_TABLE a,tblDepartment b where a.ISSUEDTO=b.id and a.ID='" + lblGRNNoS.Text + "' ", con);
            using (SqlCommand com = new SqlCommand("DTSTOCK_MATRECDPTGRN", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPMIN";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblGRNNoS.Text;
                com.Parameters.Add("@MINO", SqlDbType.VarChar).Value = "";
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    lblminno.Text = dr["ID"].ToString();
                    lblMrno.Text = dr["MRNO"].ToString();
                    lblmindate.Text = dr["MINDATE"].ToString();
                    ddDeptname.SelectedValue = dr["did"].ToString();
                    hdnIssuefrom.Value = dr["ISSUEDTO"].ToString();
                }
                dr.Close();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void BindGrnNo()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //SqlCommand COM = new SqlCommand("SELECT * FROM DEPTGRN_TABLE where MINNO='" + lblminno.Text + "' ", con);
            using (SqlCommand com = new SqlCommand("DTSTOCK_MATRECDPTGRN", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPGRNO";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@MINO", SqlDbType.VarChar).Value = lblminno.Text;
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    //lblminno.Text = dr["MRNO"].ToString();  
                    txtgrnno.Text = dr["GRNNO"].ToString();
                }
                dr.Close();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select SLNO as ID from DEPTGRN_TABLE";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            if (dr.Read() && dr["ID"].ToString() != "")
            {
                num1 = dr["ID"].ToString();
            }

            //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
            lblAuto.Text = "DGRN-" + num1 + 1 + "-" + lblfyear.Text;

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void binddYear()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                SqlDataReader dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,PNAME AS PARTY FROM SA_TABLE WHERE  ORGID='" + lblorgid.Text + "' AND FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView2.SelectedIndex = 0;
            //GridView2.DataSource = dt;
            //GridView2.DataKeyNames = new string[] { "ID" };
            //GridView2.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    //public void BindMin()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    SqlCommand COM = new SqlCommand("SELECT a.ID,a.MRNO,CONVERT(varchar, MINDATE, 105) as MINDATE,a.ISSUEDTO,b.DeptName,b.id as did FROM MIN_TABLE a,tblDepartment b where a.ISSUEDTO=b.id and a.ID='" + lblGRNNoS.Text + "' ", con);
    //    dr = COM.ExecuteReader();
    //    if (dr.Read())
    //    {
    //        lblminno.Text = dr["ID"].ToString();
    //        lblMrno.Text = dr["MRNO"].ToString();
    //        lblmindate.Text = dr["MINDATE"].ToString();
    //        ddDeptname.SelectedValue = dr["did"].ToString();
    //        hdnIssuefrom.Value = dr["ISSUEDTO"].ToString();
    //    }
    //    dr.Close();
    //    con.Close();
    //}
     public bool ifexit(string val)
    {
        bool ret;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        da = new SqlDataAdapter("SELECT * FROM DEPT_MIN_TABLE where ID='" + val + "'", con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            ret = true;
        }
        else
        {
            ret = false;
        }
        con.Close();
        return ret;
       
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (ifexit(lblminno.Text) == true)
            {
                string message = "alert('MIN Is Already Exists !')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            using (SqlCommand GRN_cmd = new SqlCommand("USP_DEPTGRN", con))
            {
                GRN_cmd.CommandType = CommandType.StoredProcedure;
                GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                GRN_cmd.Parameters.Add("@GRNNO", SqlDbType.VarChar).Value = lblAuto.Text;
                GRN_cmd.Parameters.Add("@MINNO", SqlDbType.VarChar).Value = lblminno.Text;
                GRN_cmd.Parameters.Add("@MINDATE", SqlDbType.Date).Value = Convert.ToDateTime(lblmindate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrnDate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@MRNO", SqlDbType.VarChar).Value = lblMrno.Text;
                GRN_cmd.Parameters.Add("@DEPT_ID", SqlDbType.VarChar).Value = hdnIssuefrom.Value;
                GRN_cmd.ExecuteNonQuery();
            }
            GENITEMINS();
            //string message = "alert('*GRN Create Successfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            con.Close();
            BindGrnNo();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/GENERALSTOCK/DeptGRNView.aspx");
    }
    public void GENITEMINS()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            foreach (GridViewRow row in grvDeptGrnItm.Rows)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                Label nameIt = (row.Cells[1].FindControl("lbl_name") as Label);
                Label quntIt = (row.Cells[2].FindControl("lbl_qunty") as Label);
                Label unitIt = (row.Cells[3].FindControl("lbl_unit") as Label);

                //-------------------------------------------------------
                using (SqlCommand GRN_cmd = new SqlCommand("USP_DEPTGRNITEM", con))
                {
                    GRN_cmd.CommandType = CommandType.StoredProcedure;
                    GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    GRN_cmd.Parameters.Add("@GRNNOITM", SqlDbType.VarChar).Value = lblAuto.Text;
                    GRN_cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = nameIt.Text;
                    GRN_cmd.Parameters.Add("@QUANTY", SqlDbType.VarChar).Value = quntIt.Text;
                    GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
                    GRN_cmd.Parameters.Add("@CHECKK", SqlDbType.VarChar).Value = chkRow.Checked;
                    GRN_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = hdnIssuefrom.Value;
                    GRN_cmd.ExecuteNonQuery();
                }
                using (SqlCommand stock_cmd = new SqlCommand("DEPT_GRN_Tran", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtgrnDate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@GBST", SqlDbType.VarChar).Value = quntIt.Text;
                    stock_cmd.Parameters.Add("@GRST", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ISSBDT", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ISSRDT", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@CLOSING ", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = hdnIssuefrom.Value;
                    stock_cmd.ExecuteNonQuery();
                }

                //}
            }

            //string message = "alert('*GRN Create Successfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    
        public void bindGridItem()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                con.Open();
              //  SqlDataAdapter da = new SqlDataAdapter("select * from MINITEM_TABLE   where ID='" + lblminno.Text + "' ORDER BY ID DESC", con);

                using (SqlCommand com = new SqlCommand("DTSTOCK_MATRECDPTGRN", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPGRID";
                    com.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblminno.Text;
                    com.Parameters.Add("@MINO", SqlDbType.VarChar).Value ="";
                    SqlDataAdapter adp = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);

                    grvDeptGrnItm.DataSource = dt;
                    //grvDeptGrnItm.DataKeyNames = new string[] { "ID" };
                    grvDeptGrnItm.DataBind();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
        }
}