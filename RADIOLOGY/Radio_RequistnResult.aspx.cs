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

public partial class RADIOLOGY_Radio_RequistnResult : System.Web.UI.Page
{
    string num1 = "PK000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    DataMathods OBJ_METHOD = new DataMathods();

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
        lblid.Text = Session["NAME"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        lblSesion.Text = Session["REQID"].ToString();

        if (!IsPostBack)
        {
            binddata();
        }
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
    }
    public void binddata()
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblSesion.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQRESULT", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                droppackge.DataSource = Ds;
                droppackge.DataTextField = "INV";
                droppackge.DataValueField = "ID";
                droppackge.DataBind();
                droppackge.Items.Insert(0,new ListItem("Please Select","0"));
            }
            SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_RAD");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblSesion.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQRESULT", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                txtreqno.Text = Ds1.Tables[0].Rows[0]["ID"].ToString();
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_2DOCT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds2 = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQRESULT", false, true, SQL_PARAMS);
            if (Ds2.Tables[0].Rows.Count > 0)
            {
                dropconsltRadio.DataSource = Ds2;
                dropconsltRadio.DataTextField = "Sname";
                dropconsltRadio.DataValueField = "id";
                dropconsltRadio.DataBind();
                dropconsltRadio.Items.Insert(0,new ListItem ("Please Select","0"));

                dropradiogrph.DataSource = Ds2;
                dropradiogrph.DataTextField = "Sname";
                dropradiogrph.DataValueField = "id";
                dropradiogrph.DataBind();
                dropradiogrph.Items.Insert(0, new ListItem("Please Select", "0"));
            }
            #region old code
            //using (SqlCommand cmd = new SqlCommand("RADIO_VIEW_REQRESULT", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            //    cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    da1 = new SqlDataAdapter(cmd);
            //    //da1 = new SqlDataAdapter("select ID ,INV FROM TBL_RADIOLGYRE_ITEM  where ID='" + lblSesion.Text + "'", con);
            //    DataTable dt1 = new DataTable();
            //    da1.Fill(dt1);
            //    droppackge.DataSource = dt1;
            //    droppackge.DataTextField = "INV";
            //    droppackge.DataValueField = "ID";
            //    droppackge.DataBind();
            //    droppackge.Items.Insert(0, "Please Select");
            //}
            //using (SqlCommand cmd1 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RAD";
            //    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            //    cmd1.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    da = new SqlDataAdapter(cmd1);
            //    //da = new SqlDataAdapter("select ID from  TBL_RADIOLGYREQ  where ID='" + lblSesion.Text + "'", con);
            //    DataTable ds1 = new DataTable();
            //    da.Fill(ds1);
            //    if (ds1.Rows.Count > 0)
            //    {
            //        txtreqno.Text = ds1.Rows[0]["ID"].ToString();
            //    }
            //}
            //using (SqlCommand cmd2 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2DOCT";
            //    cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            //    cmd2.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    da2 = new SqlDataAdapter(cmd2);
            //    //da2 = new SqlDataAdapter("select ID,DOCTORNAME FROM TBL_DOCTORENTY", con);
            //    DataTable ds2 = new DataTable();
            //    da2.Fill(ds2);
            //    dropconsltRadio.DataSource = ds2;
            //    dropconsltRadio.DataTextField = "DOCTORNAME";
            //    dropconsltRadio.DataValueField = "ID";
            //    dropconsltRadio.DataBind();
            //    dropconsltRadio.Items.Insert(0, "Please Select");
            //}
            //-------------------------------------------
            //using (SqlCommand cmd3 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
            //{
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_2DOCT";
            //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            //    cmd3.Parameters.Add("@INV", SqlDbType.VarChar).Value = "";
            //    da3 = new SqlDataAdapter(cmd3);
            //    //da3 = new SqlDataAdapter("select ID,DOCTORNAME FROM TBL_DOCTORENTY", con);
            //    DataTable ds3 = new DataTable();
            //    da3.Fill(ds3);
            //    dropradiogrph.DataSource = ds3;
            //    dropradiogrph.DataTextField = "Sname";
            //    dropradiogrph.DataValueField = "id";
            //    dropradiogrph.DataBind();
            //    dropradiogrph.Items.Insert(0, "Please Select");
            //}
            #endregion
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }

    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1=string.Empty;
        try
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
            int chkedcounter = 0;
            int correctinput = 0;
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[13];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PACKAGE", SqlDbType.VarChar, 500, droppackge.SelectedItem.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@CONSULTRADIO", SqlDbType.VarChar, 500, dropconsltRadio.SelectedValue);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@TESTRESULT", SqlDbType.VarChar, 500, txtResult.Text);
            

            SQL_PARAMS[5] = OBJ_METHOD.createParams("@FINDING", SqlDbType.VarChar, 500, txtfinding.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@IMPRESSION", SqlDbType.VarChar, 500, txtimpresion.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, txtdate.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@FYEAR", SqlDbType.VarChar, 500, lblfyear.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@REQID", SqlDbType.VarChar, 500, txtreqno.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@RADIOGRAPH", SqlDbType.VarChar, 500, dropradiogrph.SelectedValue);

            OBJ_METHOD.ExecuteProceedure("USP_RESULTREQN", "", "", SqlDbType.VarChar, SQL_PARAMS);

            if (OBJ_METHOD._RESULT > 0)
            {

                foreach (GridViewRow gr in GridView1.Rows)
                {
                    string name1 = GridView1.Rows[gr.RowIndex].Cells[0].Text;
                    string qty1 = GridView1.Rows[gr.RowIndex].Cells[1].Text;
                    {
                        chkedcounter++;
                        SQL_PARAMS = new SqlParameter[4];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "STOKUPDATE");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ITEMNAME", SqlDbType.VarChar, 500, name1.ToString());
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@STOCK", SqlDbType.Decimal, 0, qty1.ToString());
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                        OBJ_METHOD.ExecuteProceedure("USP_REQUIS_STOCK_UP", "", "", SqlDbType.VarChar, SQL_PARAMS);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            correctinput++;
                        }
                    }

                }
                if (chkedcounter == correctinput && chkedcounter > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    clearfield();
                    Session["RADID"] = txtid.Text;
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
            #region old code
            //auto();
        
            //using (SqlCommand stock_cmd = new SqlCommand("USP_RESULTREQN", con))
            //{
            //    stock_cmd.CommandType = CommandType.StoredProcedure;
            //    stock_cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";

            //    stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    stock_cmd.Parameters.Add("@PACKAGE", SqlDbType.VarChar).Value = droppackge.SelectedItem.Text;
            //    stock_cmd.Parameters.Add("@CONSULTRADIO", SqlDbType.VarChar).Value = dropconsltRadio.SelectedValue;
            //    stock_cmd.Parameters.Add("@RADIOGRAPH", SqlDbType.VarChar).Value = dropradiogrph.SelectedValue;
            //    stock_cmd.Parameters.Add("@TESTRESULT", SqlDbType.VarChar).Value = txtResult.Text;
            //    stock_cmd.Parameters.Add("@FINDING", SqlDbType.VarChar).Value = txtfinding.Text;
            //    stock_cmd.Parameters.Add("@IMPRESSION", SqlDbType.VarChar).Value = txtimpresion.Text;
            //    stock_cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
            //    stock_cmd.Parameters.Add("@FYEAR", SqlDbType.VarChar).Value = lblfyear.Text;
            //    stock_cmd.Parameters.Add("@REQID", SqlDbType.VarChar).Value = txtreqno.Text;
            //    stock_cmd.ExecuteNonQuery();
            //}
            //requStock_item();
            //binddata();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
        
        Response.Redirect("~/RADIOLOGY/Radio_Resultrequbill.aspx");
    }
    #region old code
    //public void requStock_item()
    //{
    //    try
    //    {
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    //  SS = "FALSE";
    //        foreach (GridViewRow gr in GridView1.Rows)
    //        {
    //            string name1 = GridView1.Rows[gr.RowIndex].Cells[0].Text;
    //            string qty1 = GridView1.Rows[gr.RowIndex].Cells[1].Text;
    //            //var INV = Convert.ToInt32(grdtestype.DataKeys[row.RowIndex].Values[0]);

    //            using (SqlCommand cmdUp = new SqlCommand("USP_REQUIS_STOCK_UP", con))
    //            {
    //                cmdUp.CommandType = CommandType.StoredProcedure;
    //                cmdUp.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "STOKUPDATE";

    //                cmdUp.Parameters.Add("@ITEMNAME", SqlDbType.VarChar).Value = name1.ToString();
    //                cmdUp.Parameters.Add("@STOCK", SqlDbType.VarChar).Value = qty1.ToString();
    //                cmdUp.ExecuteNonQuery();
    //            }
    //        }
    //        con.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine("An error occurred: '{0}'", ex);

    //    }
        
    //}
    #endregion
    protected void droppackge_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DROP_EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@INV", SqlDbType.VarChar, 500, droppackge.SelectedItem.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RADIO_VIEW_REQRESULT", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds;

                GridView1.DataBind();
                GridView1.Visible = false;
            }
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            //using (SqlCommand cmd3 = new SqlCommand("RADIO_VIEW_REQRESULT", con))
            //{
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DROP_EVENT";
            //    cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblSesion.Text;
            //    cmd3.Parameters.Add("@INV", SqlDbType.VarChar).Value = droppackge.SelectedItem.Text;
            //    da6 = new SqlDataAdapter(cmd3);
            //    //da6 = new SqlDataAdapter("select b.MATERIAL_NAME NAME,b.QUANTY QTY from RADIO_CONSUMP_TEST a,RAD_CONSUM_TESTITEM b where a.ID=b.ID and a.RADTESTNAME in (select slno from RADIOLOGY_COMPONENT_TABLE where INV='" + droppackge.SelectedItem.Text + "')", con);
            //    DataTable dt = new DataTable();
            //    da6.Fill(dt);

            //    GridView1.DataSource = dt;

            //    GridView1.DataBind();
            //    GridView1.Visible = false;
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    public void clearfield()
    {
        txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        txtfinding.Text = "";
        txtimpresion.Text = "";
        txtResult.Text = "";
        txtselid.Text = "";
        dropconsltRadio.SelectedIndex = 0;
        droppackge.SelectedIndex = 0;
        dropradiogrph.SelectedIndex = 0;
       
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RADIOLOGY/Radio_RequistnResult.aspx");
    }
}