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

public partial class STOREKEEPER_store_DeptGRNStock : System.Web.UI.Page
{
    string num1 = "0";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    string SS;

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
            lblGRNNoS.Text = Session["ID"].ToString();
            if (!IsPostBack)
            {
                auto();

                BindDepartment();
                binddYear();
                BindMin();
                bindGridItem();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void BindMin()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("STORE_GOODS_RECEIVE_NOTES", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblGRNNoS.Text;
            //SqlCommand COM = new SqlCommand("SELECT a.ID, CONVERT(VARCHAR(10),a.MRNDATE,105) AS MRNDATE,a.RETURN_TO,a.RECBY,b.id as did,b.DeptName FROM MRN_STORE_TABLE a, tblDepartment b where a.RETURN_TO=b.id and a.ID='" + lblGRNNoS.Text + "' ", con);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                lblminno.Text = dr["ID"].ToString();
                lblreceive.Text = dr["RECBY"].ToString();
                lblmindate.Text = dr["MRNDATE"].ToString();
                hdnRetrnTo.Value = dr["RETURN_TO"].ToString();
                ddDeptname.SelectedValue = dr["did"].ToString();

            }
            dr.Close();
        }
        con.Close();
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select slno as ID from DEPT_GRNST_TBL";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        if (dr.Read() && dr["ID"].ToString() != "")
        {
            num1 = dr["ID"].ToString();
        }

        //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
        lblAuto.Text = "STGRN-" + num1 + 1 + "-" + lblfyear.Text;

        dr.Close();
        con.Close();
    }
    public void binddYear()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            using (SqlCommand COM = new SqlCommand("FINANCIAL_YEAR", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                dr = COM.ExecuteReader();
                if (dr.Read())
                {
                    lblfyear.Text = dr["FYEAR"].ToString();
                }
                dr.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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

    public void bindGridItem()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("STORE_GOODS_RECEIVE_NOTES", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BINDITEM";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblminno.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("select * from MRN_ITEM_STORE_TABLE   where ID='" + lblminno.Text + "' ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            grvDeptGrnItm.DataSource = dt;
            //grvDeptGrnItm.DataKeyNames = new string[] { "ID" };
            grvDeptGrnItm.DataBind();
        }
        con.Close();
    }
    public void BindDepartment()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("STORE_GOODS_RECEIVE_NOTES", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BIND_DEPT";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblminno.Text;
            SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //SqlDataAdapter Adp = new SqlDataAdapter("select id,DeptName from tblDepartment ", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            ddDeptname.DataSource = Dt;
            ddDeptname.DataTextField = "DeptName";
            ddDeptname.DataValueField = "id";
            ddDeptname.DataBind();
        }

        //ddDeptname.Items.Insert(0, "Please Select");
        con.Close();
    }
    public bool ifexit(string val)
    {
        bool ret;
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd = new SqlCommand("STORE_GOODS_RECEIVE_NOTES", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPT";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = val;
            da = new SqlDataAdapter(cmd);
            //da = new SqlDataAdapter("select * from DEPT_GRNST_TBL where ID='" + val + "'", con);
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

    }
    protected void btnAccept_Click(object sender, EventArgs e)
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
            GENITEMINS();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand GRN_cmd = new SqlCommand("USP_DEPT_GRN_STRET", con))
            {
                GRN_cmd.CommandType = CommandType.StoredProcedure;
                GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                GRN_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblAuto.Text;
                GRN_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrnDate.Text).ToString("yyyy-MM-dd");
                GRN_cmd.Parameters.Add("@RETURNTO", SqlDbType.VarChar).Value = hdnRetrnTo.Value;
                GRN_cmd.Parameters.Add("@RECBY", SqlDbType.VarChar).Value = lblreceive.Text;
                GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@checkk", SqlDbType.VarChar).Value = "";
                GRN_cmd.Parameters.Add("@MRNO", SqlDbType.VarChar).Value = lblminno.Text;
                GRN_cmd.Parameters.Add("@MRDATE", SqlDbType.Date).Value = Convert.ToDateTime(lblmindate.Text).ToString("yyyy-MM-dd");

                GRN_cmd.ExecuteNonQuery();

            }

            //string message = "alert('*GRN Create Successfully.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        // BindGrnNo();
        Response.Redirect("~/STOREKEEPER/Store_DeptGRNView.aspx");
    }
    public void GENITEMINS()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SS = "FALSE";
        foreach (GridViewRow row in grvDeptGrnItm.Rows)
        {

            CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
            Label nameIt = (row.Cells[1].FindControl("lbl_name") as Label);
            Label quntIt = (row.Cells[2].FindControl("lbl_qunty") as Label);
            Label unitIt = (row.Cells[3].FindControl("lbl_unit") as Label);
            if (chkRow.Checked)
            {
                SS = "TRUE";
                using (SqlCommand GRN_cmd = new SqlCommand("USP_DEPT_GRN_STRET", con))
                {
                    GRN_cmd.CommandType = CommandType.StoredProcedure;
                    GRN_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                    GRN_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblAuto.Text;
                    GRN_cmd.Parameters.Add("@GRNDATE", SqlDbType.Date).Value = Convert.ToDateTime(txtgrnDate.Text).ToString("yyyy-MM-dd");
                    GRN_cmd.Parameters.Add("@RETURNTO", SqlDbType.VarChar).Value = "";
                    GRN_cmd.Parameters.Add("@RECBY", SqlDbType.VarChar).Value = "";
                    GRN_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                    GRN_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
                    GRN_cmd.Parameters.Add("@QUANTITY", SqlDbType.VarChar).Value = quntIt.Text;
                    GRN_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
                    GRN_cmd.Parameters.Add("@checkk", SqlDbType.VarChar).Value = chkRow.Checked;
                    GRN_cmd.Parameters.Add("@MRNO", SqlDbType.VarChar).Value = lblminno.Text;
                    GRN_cmd.Parameters.Add("@MRDATE", SqlDbType.Date).Value = Convert.ToDateTime(lblmindate.Text).ToString("yyyy-MM-dd");
                    GRN_cmd.ExecuteNonQuery();
                }
            }
            //-------------------------------------------------------

            using (SqlCommand stock_cmd = new SqlCommand("STORE_MRN_Tran_Department", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtgrnDate.Text).ToString("yyyy-MM-dd");
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text;
                stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PURCHES", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@RETN", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@ISSUE", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = quntIt.Text;
                stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }


        }

        //string message = "alert('*GRN Create Successfully.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        con.Close();
    }


    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/STOREKEEPER/store_material_receive_dept.aspx");
    }
}