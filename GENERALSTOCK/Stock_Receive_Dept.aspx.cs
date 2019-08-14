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

public partial class GENERALSTOCK_Stock_Receive_Dept : System.Web.UI.Page
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
            //lblGRNNoS.Text = Session["MRINDID"].ToString();
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
            //dropfrdept.DataSource = Dt;
            //dropfrdept.DataTextField = "DeptName";
            //dropfrdept.DataValueField = "id";
            //dropfrdept.DataBind();

            droptodept.DataSource = Dt;
            droptodept.DataTextField = "DeptName";
            droptodept.DataValueField = "id";
            droptodept.DataBind();
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
            //SqlCommand COM = new SqlCommand("SELECT * from STOCK_TRANSFER_DEPT", con);
            using (SqlCommand com = new SqlCommand("DTSTOCK_STRECIVEDEPT", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPMIN";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                SqlDataReader dr = com.ExecuteReader();
                if (dr.Read())
                {
                    lbltransferno.Text = dr["TRANSFER_NO"].ToString();
                    lbltransferdate.Text = dr["DATE"].ToString();
                    dropfrdept.SelectedValue = dr["FROM_DEPT"].ToString();
                    droptodept.SelectedValue = dr["TRANSFER_TO"].ToString();
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
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        //SqlCommand COM = new SqlCommand("SELECT * FROM DEPTGRN_TABLE where MINNO='" + lblminno.Text + "' ", con);
        //dr = COM.ExecuteReader();
        //if (dr.Read())
        //{
        //    //lblminno.Text = dr["MRNO"].ToString();  
        //    txtgrnno.Text = dr["GRNNO"].ToString();
        //}
        //dr.Close();
        //con.Close();
    }
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            string qry1 = "select ID from STOCK_RECEIVE_DEPT";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            if (dr.Read() && dr["ID"].ToString() != "")
            {
                num1 = dr["ID"].ToString();
            }

            //num1 = string.Format("INV{0}", (Convert.ToUInt64(num1.Substring(3)) + 1).ToString("D4"));
            lblAuto.Text = "SRN-" + num1 + 1 + "-" + lblfyear.Text;

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

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand cmd = new SqlCommand("STOCK_RECEIVE_OPERATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@RECEIVE_NO", SqlDbType.VarChar).Value = lblAuto.Text;
                cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                cmd.Parameters.Add("@TRANSFER_NO", SqlDbType.VarChar).Value = lbltransferno.Text;
                cmd.Parameters.Add("@RECV_DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtrecvDate.Text).ToString("yyyy-MM-dd");
                cmd.Parameters.Add("@FROM_DEPT", SqlDbType.VarChar).Value = dropfrdept.SelectedValue;
                cmd.Parameters.Add("@TRANSFER_TO", SqlDbType.VarChar).Value = droptodept.SelectedValue;

                cmd.Parameters.Add("@ITEM_NAME", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@QTY", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@CHECKK", SqlDbType.VarChar).Value = "";
                cmd.ExecuteNonQuery();
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
        Response.Redirect("~/GENERALSTOCK/GRNfrDept.aspx");
    }
    public void GENITEMINS()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            foreach (GridViewRow row in grvStockRecv.Rows)
            {

                CheckBox chkRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                Label nameIt = (row.Cells[1].FindControl("lbl_name") as Label);
                Label quntIt = (row.Cells[2].FindControl("lbl_qunty") as Label);
                Label unitIt = (row.Cells[3].FindControl("lbl_unit") as Label);

                //-------------------------------------------------------
                using (SqlCommand cmd1 = new SqlCommand("STOCK_RECEIVE_OPERATION", con))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                    cmd1.Parameters.Add("@RECEIVE_NO", SqlDbType.VarChar).Value = lblAuto.Text;
                    cmd1.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    cmd1.Parameters.Add("@TRANSFER_NO", SqlDbType.VarChar).Value = lbltransferno.Text;
                    cmd1.Parameters.Add("@RECV_DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtrecvDate.Text).ToString("yyyy-MM-dd");
                    cmd1.Parameters.Add("@FROM_DEPT", SqlDbType.VarChar).Value = dropfrdept.SelectedValue;
                    cmd1.Parameters.Add("@TRANSFER_TO", SqlDbType.VarChar).Value = droptodept.SelectedValue;

                    cmd1.Parameters.Add("@ITEM_NAME", SqlDbType.VarChar).Value = nameIt.Text;
                    cmd1.Parameters.Add("@QTY", SqlDbType.VarChar).Value = quntIt.Text;
                    cmd1.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unitIt.Text;
                    cmd1.Parameters.Add("@CHECKK", SqlDbType.VarChar).Value = chkRow.Checked;
                    cmd1.ExecuteNonQuery();
                }
                using (SqlCommand stock_cmd = new SqlCommand("DEPT_MRN_Tran", con))
                {
                    stock_cmd.CommandType = CommandType.StoredProcedure;
                    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    stock_cmd.Parameters.Add("@DATE", SqlDbType.VarChar).Value = Convert.ToDateTime(txtrecvDate.Text).ToString("yyyy-MM-dd");
                    stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = nameIt.Text.ToString();
                    stock_cmd.Parameters.Add("@OPENING", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@GBST", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@GRST", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ISSBDT", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ISSRDT", SqlDbType.VarChar).Value = quntIt.Text;
                    stock_cmd.Parameters.Add("@CLOSING", SqlDbType.VarChar).Value = "0.00";
                    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = droptodept.SelectedValue;
                    stock_cmd.ExecuteNonQuery();
                }
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
            //SqlDataAdapter da = new SqlDataAdapter("select * from STOCK_TRANSFER_DEPT_ITEM", con);
            using (SqlCommand com = new SqlCommand("DTSTOCK_STRECIVEDEPT", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPGRD";
                com.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                com.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = "";
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grvStockRecv.DataSource = dt;
                //grvDeptGrnItm.DataKeyNames = new string[] { "ID" };
                grvStockRecv.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}