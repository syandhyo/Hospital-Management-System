using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.Services;

public partial class RADIOLOGY_TestwiseConspRado : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    GridViewRow gr;
    string PAIDMAT;
    decimal amount = 0;
    [WebMethod]

    public static string[] GetCustomers(string prefix)
    {
        List<string> customers = new List<string>();
        using (SqlConnection conn = new SqlConnection())
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISTINCT_DEPT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                //cmd.CommandText = "select DISTINCT ITEMNAME from TBL_DEPT_MAT_STOCK where ITEMNAME like @SearchText+'%' AND ID=(select id from tblDepartment where DeptName like 'Radi%')";
                cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value=prefix;
                cmd.Connection = con;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(string.Format("{0}", sdr["ITEMNAME"]));
                    }
                }
                con.Close();
            }
        }
        return customers.ToArray();
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from RADIO_CONSUMP_TEST";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("RC{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;
        dr.Close();
        con.Close();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
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
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (!IsPostBack)
        {
            binddata();
        }
    }
    public void binddata()
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

        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[2] { new DataColumn("NAME"), new DataColumn("QTY") });
        ViewState["ITEM"] = dt;
        this.BindGrid();
        using (SqlCommand cmd = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISTINCT_CONSUM_PAGE";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            //SqlDataAdapter da1 = new SqlDataAdapter("select DISTINCT B.INV RADTESTNAME,A.DATE,A.ID AS ID from  RADIO_CONSUMP_TEST A,RADIOLOGY_COMPONENT_TABLE B WHERE A.RADTESTNAME=B.slno ORDER BY A.ID ASC", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            grvtestCons.SelectedIndex = 0;
            grvtestCons.DataSource = dt1;
            grvtestCons.DataKeyNames = new string[] { "ID" };
            grvtestCons.DataBind();
        }
        using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_COMPO";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //SqlDataAdapter Adp = new SqlDataAdapter("select slno,INV from RADIOLOGY_COMPONENT_TABLE", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            droptesname.DataSource = Dt;
            droptesname.DataTextField = "INV";
            droptesname.DataValueField = "slno";
            droptesname.DataBind();
            droptesname.Items.Insert(0, "Please Select");
        }
        //-----------------------------------
      
        con.Close();
    }
    protected void BindGrid()
    {
        try
        {
            GridView1.DataSource = (DataTable)ViewState["ITEM"];
            GridView1.DataBind();
        }
        catch (Exception x)
        {
            string var = x.Message;
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (txtmaterialnm.Text == "")
        {
            string message = "alert('* Material Name are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        if (txtquant.Text == "" || txtquant.Text == "0.00")
        {
            string message = "alert('* Quantity are mandatory.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            return;
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        DataTable dt = (DataTable)ViewState["ITEM"];
        dt.Rows.Add(txtmaterialnm.Text.Trim(), txtquant.Text.Trim());
        ViewState["ITEM"] = dt;
        this.BindGrid();

        con.Close();
        txtmaterialnm.Text = "";
        txtquant.Text = "";

        //btncreate.Visible = true;
        //btncancel.Visible = true;
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = (DataTable)ViewState["ITEM"];
        GridViewRow row = (GridViewRow)GridView1.Rows[e.RowIndex];

        Label name = (Label)row.FindControl("lblname");

        Label quantity = (Label)row.FindControl("lblqty");

        string name1 = name.Text.ToString();
        // string unit1 = unit.Text.ToString();
        string quantity1 = quantity.Text.ToString();

        dt.Rows[index].Delete();
        ViewState["ITEM"] = dt;
        //  lblgrandtotal.Text = (Convert.ToDecimal(lblgrandtotal.Text) - Convert.ToDecimal(TOTALAMT)).ToString();

        this.BindGrid();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (droptesname.SelectedIndex == 0)
            {
                string message = "alert('* Test Name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (GridView1.Rows.Count <= 0)
            {

                string message = "alert('*Add item For Test.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            auto();
            using (SqlCommand MR_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            {
                MR_cmd.CommandType = CommandType.StoredProcedure;
                MR_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                MR_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                MR_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                MR_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = droptesname.SelectedValue;

                MR_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MR_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = "";
                MR_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = "0.00";

                MR_cmd.ExecuteNonQuery();

            }
            ItemMr();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message = "alert('*Please select any test to save.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        Response.Redirect("~/RADIOLOGY/TestwiseConspRado.aspx");
    }
    public void ItemMr()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        foreach (GridViewRow gv1 in GridView1.Rows)
        {
            //var lbname = (gv1.FindControl("chkRow") as CheckBox);
            var nameGr = (gv1.FindControl("lblname") as Label);
            var quantyGr = (gv1.FindControl("lblqty") as Label);
            using (SqlCommand MRitem_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            {
                MRitem_cmd.CommandType = CommandType.StoredProcedure;
                MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";
                MRitem_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                MRitem_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = "";

                MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MRitem_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = nameGr.Text;
                MRitem_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = quantyGr.Text;

                MRitem_cmd.ExecuteNonQuery();
            }
        }
        con.Close();
    }

    protected void grvtestCons_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DISTINCT_CONSUM_PAGE";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //SqlDataAdapter Adp = new SqlDataAdapter("select DISTINCT B.INV RADTESTNAME,A.DATE,A.ID AS ID from  RADIO_CONSUMP_TEST A,RADIOLOGY_COMPONENT_TABLE B WHERE A.RADTESTNAME=B.slno ORDER BY A.ID ASC", con);
            DataTable dt = new DataTable();
            Adp.Fill(dt);
            grvtestCons.DataSource = dt;
            grvtestCons.PageIndex = e.NewPageIndex;
            grvtestCons.DataKeyNames = new string[] { "ID" };
            grvtestCons.DataBind();
        }
        con.Close();
    }
    protected void grvtestCons_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = grvtestCons.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT1";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            cmd1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //SqlDataAdapter Adp = new SqlDataAdapter(cmd1);
            //SqlCommand com = new SqlCommand("select a.ID as RID,a.date as DATE,a.RADTESTNAME from  RADIO_CONSUMP_TEST a where a.ID='" + slno + "'", con);
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                btncancel.Visible = true;
                btndelete.Visible = false;
                txtselid.Text = slno.ToString();
                txtdate.Text = dr["DATE"].ToString();
                droptesname.Text = dr["RADTESTNAME"].ToString();
                txtAutoid.Text = dr["RID"].ToString();
                dr.Close();
            }
        }
        using (SqlCommand com1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            com1.CommandType = CommandType.StoredProcedure;
            com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2EVENT2";
            com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            com1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            //SqlCommand com1 = new SqlCommand("select MATERIAL_NAME NAME,QUANTY QTY from  RAD_CONSUM_TESTITEM  where ID='" + slno + "'", con);
            SqlDataAdapter da1 = new SqlDataAdapter(com1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = ds1;
            // GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();

            DataTable dt = ds1.Tables["Table"];
            ViewState["ITEM"] = dt;

            //SqlCommand com2 = new SqlCommand("select MATERIAL_NAME NAME,QUANTY QTY from  RAD_CONSUM_TESTITEM  where ID='" + slno + "'", con);
            //SqlDataAdapter da2 = new SqlDataAdapter(com1);
            //DataSet ds2 = new DataSet();
            da1.Fill(ds1);
            grvhidden.SelectedIndex = 0;
            grvhidden.DataSource = ds1;
            // GridView1.DataKeyNames = new string[] { "ID" };
            grvhidden.DataBind();
        }
        

        con.Close();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RADIOLOGY/TestwiseConspRado.aspx");
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (droptesname.SelectedIndex == 0)
            {
                string message = "alert('* Test Name are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (GridView1.Rows.Count <= 0)
            {

                string message = "alert('*Add item For Test.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //  auto();
            using (SqlCommand MR_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            {
                MR_cmd.CommandType = CommandType.StoredProcedure;
                MR_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                MR_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
                MR_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                MR_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = droptesname.SelectedValue;

                MR_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MR_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = "";
                MR_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = "0.00";

                MR_cmd.ExecuteNonQuery();

            }
            ItemMrUp();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //string message = "alert('*Please select any test to save.')";
        //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        Response.Redirect("~/RADIOLOGY/TestwiseConspRado.aspx");
    }
    public void ItemMrUp()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand com1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            com1.CommandType = CommandType.StoredProcedure;
            com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE3_UPDATE";
            com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
            com1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(com1);
            //SqlDataAdapter da = new SqlDataAdapter("delete from RAD_CONSUM_TESTITEM where ID='" + txtselid.Text + "'", con);
            DataSet ds1 = new DataSet();
            da.Fill(ds1);

            DataTable dt1 = ViewState["ITEM"] as DataTable;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
        }

        foreach (GridViewRow gv1 in GridView1.Rows)
        {
            //var lbname = (gv1.FindControl("chkRow") as CheckBox);
            var nameGr = (gv1.FindControl("lblname") as Label);
            var quantyGr = (gv1.FindControl("lblqty") as Label);
            using (SqlCommand MRitem_cmd = new SqlCommand("USP_RADO_CONSUMP", con))
            {
                MRitem_cmd.CommandType = CommandType.StoredProcedure;
                MRitem_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";
                MRitem_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
                MRitem_cmd.Parameters.Add("@DATE", SqlDbType.Date).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd HH:mm");
                MRitem_cmd.Parameters.Add("@RADTESTNAME", SqlDbType.VarChar).Value = "";

                MRitem_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;

                MRitem_cmd.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = nameGr.Text;
                MRitem_cmd.Parameters.Add("@QUANTY", SqlDbType.Decimal).Value = quantyGr.Text;

                MRitem_cmd.ExecuteNonQuery();
            }
        }

        //foreach (GridViewRow gv2 in grvhidden.Rows)
        //{
        //    SqlCommand cmd2 = new SqlCommand("insert into RAD_CONSUM_TESTITEM (ID,MATERIAL_NAME ,QUANTY) values (@ID,@MATERIAL_NAME ,@QUANTY)", con);

        //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtselid.Text;
        //    cmd2.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = gv2.Cells[0].Text;
        //    cmd2.Parameters.Add("@QUANTY", SqlDbType.VarChar).Value = gv2.Cells[1].Text;


        //    cmd2.ExecuteNonQuery();
        //}
        con.Close();
    }

    protected void grvtestCons_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
        }
    }
    protected void grvtestCons_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        var slno = grvtestCons.DataKeys[e.RowIndex].Values["ID"].ToString();
        using (SqlCommand com1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            com1.CommandType = CommandType.StoredProcedure;
            com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE3_UPDATE";
            com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            com1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(com1);
            //SqlDataAdapter da = new SqlDataAdapter("delete from RADIO_CONSUMP_TEST where ID='" + slno + "'", con);
            DataSet d = new DataSet();
            da.Fill(d);
            hdndel.Value = slno.ToString();
            CONS_ITEM_DEL();
        }
        con.Close();
        Response.Redirect("~/RADIOLOGY/TestwiseConspRado.aspx");
    }
    public void CONS_ITEM_DEL()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        DataTable dt1 = ViewState["ITEM"] as DataTable;
        GridView1.DataSource = dt1;
        GridView1.DataBind();
        using (SqlCommand com1 = new SqlCommand("RADIO_TESTWISE_CONSUMP", con))
        {
            com1.CommandType = CommandType.StoredProcedure;
            com1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE3_UPDATE";
            com1.Parameters.Add("@ID", SqlDbType.VarChar).Value = hdndel.Value;
            com1.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(com1);
            //SqlCommand cmd2 = new SqlCommand("delete from RAD_CONSUM_TESTITEM where ID='" + hdndel.Value + "'", con);
            //SqlDataAdapter da = new SqlDataAdapter(com1);
            DataSet d = new DataSet();
            da.Fill(d);
        }
        con.Close();
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {

    }
}