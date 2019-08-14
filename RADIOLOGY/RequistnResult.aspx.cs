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

public partial class RADIOLOGY_RequistnResult : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b;
    decimal amount = 0;
    decimal amount1 = 0;
    decimal gstamount = 0;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;

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
        lblorgid.Text = Session["ORGID"].ToString();
        lblSesion.Text = Session["REQID"].ToString();

        if (!IsPostBack)
        {
            binddata();
        }
        con.Close();
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
        using (SqlCommand cmd = new SqlCommand("RADIO_VIEW_REQRESULT", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            da1 = new SqlDataAdapter(cmd);
            //da1 = new SqlDataAdapter("select ID ,INV FROM TBL_RADIOLGYRE_ITEM  where ID='" + lblSesion.Text + "'", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            droppackge.DataSource = dt1;
            droppackge.DataTextField = "INV";
            droppackge.DataValueField = "ID";
            droppackge.DataBind();
            droppackge.Items.Insert(0, "Please Select");
        }
        using (SqlCommand cmd1 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RAD";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            cmd1.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            da = new SqlDataAdapter(cmd1);
            //da = new SqlDataAdapter("select ID from  TBL_RADIOLGYREQ  where ID='" + lblSesion.Text + "'", con);
            DataTable ds1 = new DataTable();
            da.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                txtreqno.Text = ds1.Rows[0]["ID"].ToString();
            }
        }
        //SqlDataAdapter da = new SqlDataAdapter("SELECT ID as ID,CONVERT(VARCHAR(10),INVDATE,105) AS DATE,VENDOR AS VENDOR FROM GRN_TABLE WHERE FYEAR='" + lblfyear.Text + "' ORDER BY ID DESC", con);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //GridView2.SelectedIndex = 0;
        //GridView2.DataSource = dt;
        //GridView2.DataKeyNames = new string[] { "ID" };
        //GridView2.DataBind();
        using (SqlCommand cmd2 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
        {
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2DOCT";
            cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            cmd2.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            da2 = new SqlDataAdapter(cmd2);
            //da2 = new SqlDataAdapter("select ID,DOCTORNAME FROM TBL_DOCTORENTY", con);
            DataTable ds2 = new DataTable();
            da2.Fill(ds2);
            dropconsltRadio.DataSource = ds2;
            dropconsltRadio.DataTextField = "DOCTORNAME";
            dropconsltRadio.DataValueField = "ID";
            dropconsltRadio.DataBind();
            dropconsltRadio.Items.Insert(0, "Please Select");
        }
        //-------------------------------------------
        using (SqlCommand cmd3 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
        {
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2DOCT";
            cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            cmd3.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            da3 = new SqlDataAdapter(cmd3);
            //da3 = new SqlDataAdapter("select ID,DOCTORNAME FROM TBL_DOCTORENTY", con);
            DataTable ds3 = new DataTable();
            da3.Fill(ds3);
            dropradiogrph.DataSource = ds3;
            dropradiogrph.DataTextField = "DOCTORNAME";
            dropradiogrph.DataValueField = "ID";
            dropradiogrph.DataBind();
            dropradiogrph.Items.Insert(0, "Please Select");
        }
        con.Close();
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID as ID from TBL_RESULTREQN";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("RS{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }

    protected void btncreate_Click(object sender, EventArgs e)
    {
        if (droppackge.SelectedIndex == 0)
        {
            string message = "alert('* Please!!Select The Package..')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        else if (dropconsltRadio.SelectedIndex == 0)
        {
            string message = "alert('* Please!!Select The Consult Radiology.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        else if (dropradiogrph.SelectedIndex == 0)
        {
            string message = "alert('* Please!!Select The Radiographer..')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
        else if (txtResult.Text == "")
        {
            string message = "alert('* Please Enter Result.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            return;
        }
      
        //----------------------------------------------------------------

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
       //----------------------------------------------------------------------------------------
        //foreach (GridViewRow gr in GridView1.Rows)
        //{
        //    string name1 = GridView1.Rows[gr.RowIndex].Cells[0].Text;
        //    string qty1 = GridView1.Rows[gr.RowIndex].Cells[1].Text;
        //    //var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);
        //    SqlCommand com = new SqlCommand("select STOCK from TBL_DEPT_MAT_STOCK where  ID=(SELECT id from tblDepartment WHERE DeptName like'radio%') and ITEMNAME='" + name1 + "'", con);
        //    dr = com.ExecuteReader();
        //    decimal QTY1 = 0;
        //    if (dr.Read())
        //    {
        //        QTY1 = Convert.ToDecimal(dr["STOCK"].ToString());
        //    }
        //    dr.Close();
        //    if (QTY1 < Convert.ToDecimal(qty1))
        //    {
        //        string message = "alert('Insufficient Stock.')";
        //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //        return;
        //    }
           

        //}

        auto();
        try
        {
            using (SqlCommand stock_cmd = new SqlCommand("USP_RESULTREQN", con))
            {
                stock_cmd.CommandType = CommandType.StoredProcedure;
                stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

                stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = droppackge.SelectedItem.Text;
                stock_cmd.Parameters.Add("@CONSULTRADIO", SqlDbType.VarChar).Value = dropconsltRadio.SelectedValue;
                stock_cmd.Parameters.Add("@RADIOGRAPH", SqlDbType.VarChar).Value = dropradiogrph.SelectedValue;
                stock_cmd.Parameters.Add("@TESTRESULT", SqlDbType.VarChar).Value = txtResult.Text;
                stock_cmd.Parameters.Add("@FINDING", SqlDbType.VarChar).Value = txtfinding.Text;
                stock_cmd.Parameters.Add("@IMPRESSION", SqlDbType.VarChar).Value = txtimpresion.Text;
                stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
                stock_cmd.Parameters.Add("@REQID", SqlDbType.VarChar).Value = txtreqno.Text;
                stock_cmd.ExecuteNonQuery();
            }
            requStock_item();
            binddata();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
        Session["RADID"] = txtid.Text;
        //Response.Redirect("~/RADIOLOGY/ViewRequstion.aspx");
        Response.Redirect("~/RADIOLOGY/Resultrequbill.aspx");
    }
    public void requStock_item()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
      //  SS = "FALSE";
        foreach (GridViewRow gr in GridView1.Rows)
        {
            string name1 = GridView1.Rows[gr.RowIndex].Cells[0].Text;
                string qty1 = GridView1.Rows[gr.RowIndex].Cells[1].Text;
                    //var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);

                using (SqlCommand cmdUp = new SqlCommand("USP_REQUIS_STOCK_UP", con))
                    {
                        cmdUp.CommandType = CommandType.StoredProcedure;
                        cmdUp.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "STOKUPDATE";

                        cmdUp.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = name1.ToString();
                        cmdUp.Parameters.Add("@STOCK", SqlDbType.VarChar).Value =qty1.ToString(); 
                        cmdUp.ExecuteNonQuery();
                    }
        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    protected void droppackge_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd3 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
        {
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DROP_EVENT";
            cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            cmd3.Parameters.Add("@INV", SqlDbType.VarChar).Value = droppackge.SelectedItem.Text;
            da6 = new SqlDataAdapter(cmd3);
            //da6 = new SqlDataAdapter("select b.MATERIAL_NAME NAME,b.QUANTY QTY from RADIO_CONSUMP_TEST a,RAD_CONSUM_TESTITEM b where a.ID=b.ID and a.RADTESTNAME in (select slno from RADIOLOGY_COMPONENT_TABLE where INV='" + droppackge.SelectedItem.Text + "')", con);
            DataTable dt = new DataTable();
            da6.Fill(dt);

            GridView1.DataSource = dt;

            GridView1.DataBind();
            GridView1.Visible = false;
        }
        con.Close();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RADIOLOGY/RequistnResult.aspx");
    }
}