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

public partial class RECEPTION_RadiologyRequs : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlConnection con;
    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da, da1, da2;
    DataTable dt;
    decimal total_amt = 0;
    decimal total_vat_amt = 0;
    int i, no, no1, sl;
    GridViewRow gr;
    string SS;
    decimal amount = 0;

    protected void Page_Load(object sender, EventArgs e)
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
        // lbldate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
          //  testype();
            binddata();
            //  auto();
        }
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID as ID from TBL_RADIOLGYREQ";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("RL{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }

    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd1 = new SqlCommand("RECP_RAD_REC", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELCT_RAD";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = "NULL";
            da1 = new SqlDataAdapter(cmd1);
            //da1 = new SqlDataAdapter("select ID,RADIOLOGYNAME FROM TBL_RADIOLOGYADM", con);
            DataTable ds1 = new DataTable();
            da1.Fill(ds1);
            dropPackage.DataSource = ds1;
            dropPackage.DataTextField = "RADIOLOGYNAME";
            dropPackage.DataValueField = "ID";
            dropPackage.DataBind();
            dropPackage.Items.Insert(0, "Please Select");
        }
        //----------------------
        using (SqlCommand cmd = new SqlCommand("RECP_RAD_REC", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RADREQ_PAGE";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //SqlDataAdapter Adp = new SqlDataAdapter("select ID,OPDNO,NAME,TOTAMT,DATE from TBL_RADIOLGYREQ where OPDNO not in(select VN from BED_TABLE) order by ID desc", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            grdpackge.DataSource = Dt;
            grdpackge.DataBind();
        }

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

        using (SqlCommand cmd = new SqlCommand("RECP_RAD_REC", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RAD_CATA";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da = new SqlDataAdapter("select ID,NAME from RADIOLOGY_CATEGORY_TABLE", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropTestype.DataSource = ds;
            dropTestype.DataTextField = "NAME";
            dropTestype.DataValueField = "ID";
            dropTestype.DataBind();
            dropTestype.Items.Insert(0, "Please Select");

            txtdisc.Text = "0";
            lbltotalamt.Text = "0";
            lbltotalprice.Text = "0";
        }
        con.Close();
    }
    protected void txtopdno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RAD_REC", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REG_ID";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtopdno.Text;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("select ID,PNAME from  REGISTRATION_TBL where ID='" + txtopdno.Text + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtname.Text = dr["PNAME"].ToString();
                    //droptesttype.Text = dr["SELECTYPE"].ToString();
                }
                else
                {
                    string message = "alert('* OPD NO Is Not Valid..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
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
    protected void chkTestType_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkTestType.Checked)
            {
                dropTestype.Visible = true;
                dropPackage.Visible = false;
                chkPackage.Checked = false;
                divinner2.Visible = false;
                dropPackage.SelectedIndex = 0;
                lbltotalprice.Text = "0";
                lbltotalamt.Text = "0";
            }
            else
            {

                dropTestype.Visible = false;
                divinner2.Visible = false;
                chkPackage.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropTestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RAD_REC", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CAT_COMP";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text.ToString();
                cmd.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter("select a.ID,b.INV,b.PRICE,b.ID as IDD from RADIOLOGY_CATEGORY_TABLE a,RADIOLOGY_COMPONENT_TABLE b where a.ID=b.ID and a.NAME='" + dropTestype.SelectedItem.Text + "'", con);
                da.Fill(dt);
                grdtestype.DataSource = dt;
                grdtestype.DataBind();
                //  ViewState["ITEM"] = dt;
                // this.BindGrid();
                //divinner1.Visible = true;
                divinner2.Visible = true;
                //  hdntype.Value = dt.Rows[0]["SELECTYPE"].ToString();
                //lbltotalprice.Text = dt.Rows[0]["PRICE"].ToString();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void chkPackage_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkPackage.Checked)
            {
                dropPackage.Visible = true;
                dropTestype.Visible = false;
                chkTestType.Checked = false;
                divinner2.Visible = false;
                dropTestype.SelectedIndex = 0;
                lbltotalprice.Text = "0";
                lbltotalamt.Text = "0";
                // divinner1.Visible = false;
            }
            else
            {
                dropPackage.Visible = false;
                divinner2.Visible = false;
                chkPackage.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void dropPackage_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RAD_REC", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_MULTI";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text.ToString();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter("select a.ID,b.TESTID as IDD,b.INV,b.PRICE,b.CHKSEL from  TBL_RADIOLOGYADM a,TBL_RADIOLGADMITEM b where a.ID=b.ID and a.RADIOLOGYNAME='" + dropPackage.SelectedItem.Text + "'", con);
                da.Fill(dt);
                grdtestype.DataSource = dt;
                grdtestype.DataBind();
            }

            divinner2.Visible = true;
            //  hdntype.Value = dt.Rows[0]["SELECTYPE"].ToString();
            //lbltotalprice.Text = dt.Rows[0]["PRICE"].ToString();
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
            if (txtopdno.Text == "")
            {
                string message = "alert('* Please Enter OPDNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            auto();
            radiolgy_item();
            if (SS == "FALSE")
            {
                string message = "alert('*Please select any item to save.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand stock_cmd = new SqlCommand("USP_RADIOLOGYRQ", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text.ToUpper();
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;

                stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                stock_cmd.Parameters.Add("@TESTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@TOTDISC", SqlDbType.VarChar).Value = txtdisc.Text;
                stock_cmd.Parameters.Add("@TOTAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;

                stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;

                stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PRICEIT", SqlDbType.Decimal).Value = "0.00";
                stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            Session["RADID"] = txtid.Text;
            Response.Redirect("~/RECEPTION/Radiologbill.aspx");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void radiolgy_item()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SS = "FALSE";
        foreach (GridViewRow row in grdtestype.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                //  Label typeIt = (row.Cells[0].FindControl("lbltype") as Label);
                //Label nameIt = (row.Cells[0].FindControl("lblname") as Label);
                Label invIt = (row.Cells[0].FindControl("lblinv") as Label);
                TextBox priceIt = (row.Cells[1].FindControl("lblprice") as TextBox);
                CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);
                    SS = "TRUE";
                    using (SqlCommand stock_cmd = new SqlCommand("USP_RADIOLOGYRQ", con))
                    {
                        stock_cmd.CommandType = CommandType.StoredProcedure;
                        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDINSERT";

                         stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                         stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text;
                         stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                         stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                         stock_cmd.Parameters.Add("@TESTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                         stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                         stock_cmd.Parameters.Add("@TOTDISC", SqlDbType.VarChar).Value =txtdisc.Text ;
                         stock_cmd.Parameters.Add("@TOTAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
                         stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text; 
                        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        stock_cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
                        // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = nameIt.Text;  
                        stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = INV.ToString();
                        stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = invIt.Text;
                        stock_cmd.Parameters.Add("@PRICEIT", SqlDbType.Decimal).Value = priceIt.Text;
                        stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.Bit).Value = chkRow.Checked;

                        stock_cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdtestype.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        //var value = row.FindControl("lblname") as Label;
                        // var inv = row.FindControl("lblinv") as Label;
                        var AMT = row.FindControl("lblprice") as TextBox;
                        //var re = row.FindControl("txtref") as TextBox;
                        //var unit = row.FindControl("txtunit") as TextBox;
                        //var AMT = row.FindControl("lblprice") as TextBox;                   
                        amount = amount + Convert.ToDecimal(AMT.Text);
                    }
                }
            }
            lbltotalprice.Text = amount.ToString();
            // lblbalanceamt.Text = amount.ToString();
            decimal d = (amount / 100) * Convert.ToDecimal(txtdisc.Text);
            //  lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdisc.Text)).ToString();
            lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(d)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void grdpackge_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = grdpackge.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RAD_REC", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = "";
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("select a.OPDNO,a.NAME,a.ID,a.TOTAMT,a.DATE,a.PRICE,a.TOTAMT,a.TOTDISC,a.PACKAGE,a.TESTYPE,b.INV,b.PRICEIT,b.CHKSEL from TBL_RADIOLGYREQ a,TBL_RADIOLGYRE_ITEM b  where a.ID=b.ID and a.ID='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = slno.ToString();
                    txtopdno.Text = dr["OPDNO"].ToString();
                    txtname.Text = dr["NAME"].ToString();
                    lbltotalprice.Text = dr["PRICE"].ToString();
                    txtdisc.Text = dr["TOTDISC"].ToString();
                    lbltotalamt.Text = dr["TOTAMT"].ToString();
                    dropPackage.SelectedItem.Text = dr["PACKAGE"].ToString();
                    dropTestype.SelectedItem.Text = dr["TESTYPE"].ToString();
                    dropPackage.Visible = true;
                    dropTestype.Visible = true;
                    dr.Close();

                    SqlDataAdapter da = new SqlDataAdapter(com);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    // grdtestype.SelectedIndex = 0;
                    grdtestype.DataSource = dt;
                    grdtestype.DataKeyNames = new string[] { "ID" };
                    grdtestype.DataBind();
                    divinner2.Visible = true;
                    //  chkPackage.Checked = true;
                    //   chkPackage.Checked = true;
                }
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtopdno.Text == "")
            {
                string message = "alert('* Please Enter OPDNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();


            radiolgy_itemUpdat();
            //if (SS == "FALSE")
            //{
            //    string message = "alert('*Please select any item to save.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            using (SqlCommand stock_cmd = new SqlCommand("USP_RADIOLOGYRQ", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";

                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text.ToUpper();
                stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;

                stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                stock_cmd.Parameters.Add("@TESTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                stock_cmd.Parameters.Add("@TOTDISC", SqlDbType.VarChar).Value = txtdisc.Text;
                stock_cmd.Parameters.Add("@TOTAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;

                stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;

                stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.Parameters.Add("@PRICEIT", SqlDbType.Decimal).Value = "0.00";
                stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = "0.00";
                stock_cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            Session["RADID"] = txtid.Text;
          
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        Response.Redirect("~/RECEPTION/Radiologbill.aspx");
    }
    public void radiolgy_itemUpdat()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
      //  SS = "FALSE";
        foreach (GridViewRow row in grdtestype.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                //  Label typeIt = (row.Cells[0].FindControl("lbltype") as Label);
                //Label nameIt = (row.Cells[0].FindControl("lblname") as Label);
                Label invIt = (row.Cells[0].FindControl("lblinv") as Label);
                TextBox priceIt = (row.Cells[1].FindControl("lblprice") as TextBox);
                CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                if (chkRow.Checked)
                {
                    var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);
                  //  SS = "TRUE";
                    using (SqlCommand stock_cmd = new SqlCommand("USP_RADIOLOGYRQ", con))
                    {
                        stock_cmd.CommandType = CommandType.StoredProcedure;
                        stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "GRIDUPDATE";

                        stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                        stock_cmd.Parameters.Add("@OPDNO", SqlDbType.VarChar).Value = txtopdno.Text;
                        stock_cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                        stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = dropPackage.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@TESTYPE", SqlDbType.VarChar).Value = dropTestype.SelectedItem.Text;
                        stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltotalprice.Text;
                        stock_cmd.Parameters.Add("@TOTDISC", SqlDbType.VarChar).Value = txtdisc.Text;
                        stock_cmd.Parameters.Add("@TOTAMT", SqlDbType.VarChar).Value = lbltotalamt.Text;
                        stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                        stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                        stock_cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
                        // stock_cmd.Parameters.Add("@TESTNAME", SqlDbType.VarChar).Value = nameIt.Text;  
                        stock_cmd.Parameters.Add("@INVID", SqlDbType.VarChar).Value = INV.ToString();
                        stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = invIt.Text;
                        stock_cmd.Parameters.Add("@PRICEIT", SqlDbType.Decimal).Value = priceIt.Text;
                        stock_cmd.Parameters.Add("@CHKSEL", SqlDbType.VarChar).Value = chkRow.Checked;

                        stock_cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
          SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            Session["RADID"] = gr.Cells[1].Text;
            Response.Redirect("~/RECEPTION/Radiologbill.aspx");
            con.Close();
     
    }
    protected void txtdisc_TextChanged(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in grdtestype.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[2].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        //var value = row.FindControl("lblname") as Label;
                        // var inv = row.FindControl("lblinv") as Label;
                        var AMT = row.FindControl("lblprice") as TextBox;
                        //var re = row.FindControl("txtref") as TextBox;
                        //var unit = row.FindControl("txtunit") as TextBox;
                        //var AMT = row.FindControl("lblprice") as TextBox;                   
                        amount = amount + Convert.ToDecimal(AMT.Text);
                    }
                }
            }
            lbltotalprice.Text = amount.ToString();
            // lblbalanceamt.Text = amount.ToString();
            decimal d = (amount / 100) * Convert.ToDecimal(txtdisc.Text);
            //  lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdisc.Text)).ToString();
            lbltotalamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(d)).ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/RadiologyRequs.aspx");
    }
    protected void grdpackge_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RAD_REC", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RADREQ_PAGE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@RADIOLOGYNAME", SqlDbType.VarChar).Value = "NULL";
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp = new SqlDataAdapter("select ID,OPDNO,NAME,TOTAMT,DATE from TBL_RADIOLGYREQ where OPDNO not in(select VN from BED_TABLE) order by ID desc", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                grdpackge.DataSource = Dt;
                grdpackge.PageIndex = e.NewPageIndex;
                grdpackge.DataKeyNames = new string[] { "ID" };
                grdpackge.DataBind();
            }
            //SqlDataAdapter adp = new SqlDataAdapter("select ID,OPDNO,NAME,TOTAMT,DATE from TBL_RADIOLGYREQ where OPDNO not in(select VN from BED_TABLE) order by ID desc", con);
            //DataTable dt1 = new DataTable();
            //adp.Fill(dt1);
            //grdpackge.DataSource = dt1;
            //grdpackge.PageIndex = e.NewPageIndex;
            //grdpackge.DataKeyNames = new string[] { "ID" };
            //grdpackge.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}