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

public partial class RECEPTION_reception_advance_payment : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlDataReader dr, dr1;
    SqlConnection con;
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    string AMOUNT;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select id from tblAdvancePayment";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["id"].ToString();
        }
        num1 = string.Format("AP{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["ADID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/Reception_advancebill.aspx");

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                binddata();

                div1.Visible = true;
                div2.Visible = false;
                txtcard.Visible = false;
                dropemp.Visible = false;
            }
          
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFF");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropemp.DataSource = DS2;
                dropemp.DataTextField = "Sname";
                dropemp.DataValueField = "EMPID";
                dropemp.DataBind();
                dropemp.Items.Insert(0, new ListItem("Please Select"));
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        #region oldcode
        //using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PAGE";
        //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //SqlDataAdapter Adp = new SqlDataAdapter("SELECT tblAdvancePayment.id,ADMISSION_TABLE.ID as PID,ADMISSION_TABLE.NAME,ADMISSION_TABLE.BEDNO,tblAdvancePayment.Amount,tblAdvancePayment.Adate FROM ADMISSION_TABLE INNER JOIN tblAdvancePayment ON ADMISSION_TABLE.VN = tblAdvancePayment.PID and tblAdvancePayment.PID='" + c_id + "'", con);
        //    DataTable Dt = new DataTable();
        //    da.Fill(Dt);
        //    GridView1.DataSource = Dt;
        //    GridView1.DataBind();
        //}
        //using (SqlCommand cmd1 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STAFF";
        //    cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //    cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //    //SqlDataAdapter da1 = new SqlDataAdapter("SELECT EMPID,Sname FROM tblStaff", con);
        //    DataTable dt1 = new DataTable();
        //    da1.Fill(dt1);
        //    //dropbedno.SelectedIndex = 0;
        //    dropemp.DataSource = dt1;
        //    dropemp.DataTextField = "Sname";
        //    dropemp.DataValueField = "EMPID";
        //    dropemp.DataBind();
        //    dropemp.Items.Insert(0, "Please Select");
        //}
        #endregion
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
                  
            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ID");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, slno);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                btnupdate.Visible = true;
                btnDELETE.Visible = true;
                txtid.Text = DS2.Tables[0].Rows[0]["ID"].ToString();
                lblname.Text = DS2.Tables[0].Rows[0]["NAME"].ToString();
                lblip.Text = DS2.Tables[0].Rows[0]["PID"].ToString();
                lblbed.Text = DS2.Tables[0].Rows[0]["BEDNO"].ToString();
                txtamount.Text = DS2.Tables[0].Rows[0]["Amount"].ToString();
                LBPAIDAMT.Text = DS2.Tables[0].Rows[0]["Amount"].ToString();
                droppayment.Text = DS2.Tables[0].Rows[0]["PMODE"].ToString();
                dropemp.SelectedValue = DS2.Tables[0].Rows[0]["UserId"].ToString();
                txtcard.Text = DS2.Tables[0].Rows[0]["PNO"].ToString();
                if (droppayment.SelectedItem.Text == "Cash")
                {
                    txtcard.Visible = false;
                    dropemp.Visible = false;

                }
                else 
                {
                    txtcard.Visible = true;
                    dropemp.Visible = true;
                }

            }

            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PA_MASTER");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lbltotalamt.Text = DS.Tables[0].Rows[0]["CREDIT"].ToString();
                lblpaidamt.Text = DS.Tables[0].Rows[0]["DEBIT"].ToString();
                lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
                txtamount.Text = lblremainamt.Text;
            }
            else
            {
                string message = "alert('* No Data Found In Transactions..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            #region old code
            //using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ID";
            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlCommand com = new SqlCommand("SELECT tblAdvancePayment.id, tblAdvancePayment.ORGID, tblAdvancePayment.PID as PID, tblAdvancePayment.Bedno, tblAdvancePayment.Adate, tblAdvancePayment.Amount,tblAdvancePayment.PMODE,tblAdvancePayment.PNO,tblAdvancePayment.UserId,ADMISSION_TABLE.NAME as NAME FROM tblAdvancePayment INNER JOIN ADMISSION_TABLE ON tblAdvancePayment.PID = ADMISSION_TABLE.VN WHERE tblAdvancePayment.id ='" + slno + "'", con);
            //    dr = cmd.ExecuteReader();
            //    if (dr.Read())
            //    {
                   
            //    }
            //}
            //dr.Close();
            //using (SqlCommand cmd3 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
            //    cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

            //    //SqlCommand cm1 = new SqlCommand("SELECT CREDIT,DEBIT FROM PA_MASTER WHERE VN='" + lblip.Text + "'", con);
            //    dr1 = cmd3.ExecuteReader();
            //    if (dr1.Read())
            //    {
            //        lbltotalamt.Text = dr1["CREDIT"].ToString();
            //        lblpaidamt.Text = dr1["DEBIT"].ToString();
            //        lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
            //        txtamount.Text = lblremainamt.Text;
            //    }
            //    else
            //    {
            //        string message = "alert('* No Data Found In Transactions..')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //    dr1.Close();
            //}
            #endregion

            div1.Visible = false;
            div2.Visible = true;
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PAGE");
        
            DataSet DS = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            //using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PAGE";
            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter da = new SqlDataAdapter("SELECT tblAdvancePayment.id,ADMISSION_TABLE.ID as PID,ADMISSION_TABLE.NAME,ADMISSION_TABLE.BEDNO,tblAdvancePayment.Amount,tblAdvancePayment.Adate FROM ADMISSION_TABLE INNER JOIN tblAdvancePayment ON ADMISSION_TABLE.VN = tblAdvancePayment.PID", con);

            //    DataTable dt = new DataTable();
            //    da.Fill(dt);
            //    GridView1.DataSource = dt;
            //    GridView1.PageIndex = e.NewPageIndex;
            //    GridView1.DataKeyNames = new string[] { "id" };
            //    GridView1.DataBind();
            //    con.Close();
            //}
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void btnshow_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            
            if (txtspid.Text == "")
            {
                string message = "alert('*Please!!Enter The IPD No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
             SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

             SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtspid.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                div2.Visible = true;
                div1.Visible = false;
                lblname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                //txtamount.Text = dr["Amount"].ToString();
                lblip.Text = Ds.Tables[0].Rows[0]["VN"].ToString();

                SQL_PARAMS1 = new SqlParameter[3];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PA_MASTER");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtspid.Text);

                DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS1);
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    lbltotalamt.Text = Ds1.Tables[0].Rows[0]["CREDIT"].ToString();
                    lblpaidamt.Text = Ds1.Tables[0].Rows[0]["DEBIT"].ToString();
                    lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
                    txtamount.Text = lblremainamt.Text;
                }
                else
                {
                    string message = "alert('* No Data Found In Transactions..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            else
            {
                string message = "alert('*Invalid IPNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dr.Close();
            }
            #region OLDCODE
            //using (SqlCommand cmd2 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd2.CommandType = CommandType.StoredProcedure;
            //    cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION";
            //    cmd2.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd2.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtspid.Text;
            //    cmd2.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

            //    //SqlCommand com = new SqlCommand("select * from ADMISSION_TABLE where VN='" + txtspid.Text + "'", con);
            //    dr = cmd2.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        div2.Visible = true;
            //        div1.Visible = false;
            //        lblname.Text = dr["NAME"].ToString();
            //        lblbed.Text = dr["BEDNO"].ToString();
            //        //txtamount.Text = dr["Amount"].ToString();
            //        lblip.Text = dr["VN"].ToString();
            //        dr.Close();
            //        using (SqlCommand cmd3 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //        {
            //            cmd3.CommandType = CommandType.StoredProcedure;
            //            cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
            //            cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //            cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //            cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";

            //            //SqlCommand cm1 = new SqlCommand("SELECT CREDIT,DEBIT FROM PA_MASTER WHERE VN='" + lblip.Text + "'", con);
            //            dr = cmd3.ExecuteReader();
            //            if (dr.Read())
            //            {
            //                lbltotalamt.Text = dr["CREDIT"].ToString();
            //                lblpaidamt.Text = dr["DEBIT"].ToString();
            //                lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
            //                txtamount.Text = lblremainamt.Text;
            //            }
            //            else
            //            {
            //                string message = "alert('* No Data Found In Transactions..')";
            //                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //                return;
            //            }
            //            dr.Close();
            //        }
            //    }
            //    else
            //    {
            //        string message = "alert('*Invalid IPNO.')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        dr.Close();
            //    }
            //    dr.Close();
            //}

            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtbedno.Text == "")
            {
                string message = "alert('*Please!!Enter The Bed No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_BED");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtbedno.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS1);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                div2.Visible = true;
                div1.Visible = false;
                lblname.Text = Ds.Tables[0].Rows[0]["NAME"].ToString();
                lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                //txtamount.Text = dr["Amount"].ToString();
                lblip.Text = Ds.Tables[0].Rows[0]["VN"].ToString();

                SQL_PARAMS1 = new SqlParameter[3];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_PA_MASTER");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
                SQL_PARAMS1[2] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, txtspid.Text);

                DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECEPTION_ADVANCEPAY_SELEDELE", false, true, SQL_PARAMS1);
                if (Ds1.Tables[0].Rows.Count > 0)
                {
                    lbltotalamt.Text = Ds1.Tables[0].Rows[0]["CREDIT"].ToString();
                    lblpaidamt.Text = Ds1.Tables[0].Rows[0]["DEBIT"].ToString();
                    lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
                    txtamount.Text = lblremainamt.Text;
                }
                else
                {
                    string message = "alert('* No Data Found In Transactions..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            else
            {
                string message = "alert('*Invalid IPNO.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dr.Close();
            }
            #region oldcode
            //using (SqlCommand cmd3 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd3.CommandType = CommandType.StoredProcedure;
            //    cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            //    cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtbedno.Text;
            //    cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            //    //SqlCommand com = new SqlCommand("select B.PNAME AS NAME,B.BEDNO AS BEDNO,B.PID AS ID,B.VN AS VN from BED_TABLE B where B.BEDNO='" + txtbedno.Text + "'", con);
            //    dr = cmd3.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        div2.Visible = true;
            //        div1.Visible = false;
            //        lblname.Text = dr["NAME"].ToString();
            //        lblbed.Text = dr["BEDNO"].ToString();
            //        //txtamount.Text = dr["Amount"].ToString();
            //        lblip.Text = dr["VN"].ToString();
            //        dr.Close();
            //        using (SqlCommand cm1 = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //        {
            //            cm1.CommandType = CommandType.StoredProcedure;
            //            cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_PA_MASTER";
            //            cm1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //            cm1.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //            cm1.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            //            //SqlCommand cm1 = new SqlCommand("SELECT CREDIT,DEBIT FROM PA_MASTER WHERE VN='" + lblip.Text + "'", con);
            //            dr = cm1.ExecuteReader();
            //            if (dr.Read())
            //            {
            //                lbltotalamt.Text = dr["CREDIT"].ToString();
            //                lblpaidamt.Text = dr["DEBIT"].ToString();
            //                lblremainamt.Text = (Convert.ToDouble(lbltotalamt.Text) - Convert.ToDouble(lblpaidamt.Text)).ToString();
            //                txtamount.Text = lblremainamt.Text;
            //            }
            //            else
            //            {
            //                string message = "alert('* No Data Found..')";
            //                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //                return;
            //            }
            //            dr.Close();
            //        }
            //    }

            //    else
            //    {
            //        string message = "alert('*Bed no is not found')";
            //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //        return;
            //    }
            //    dr.Close();
            //}
            //con.Close();
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            // Validation
            if (txtamount.Text == "")
            {
                string message = "alert('*Please!! Enter The Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) <= 0)
            {
                string message = "alert('* There Is No Due Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) > 0 && Convert.ToDecimal(txtamount.Text) == 0 || txtamount.Text == "")
            {
                string message = "alert('Please!! Enter The Amount To Be Paid..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[9];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lblid.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@CAMOUNT", SqlDbType.VarChar, 500, txtamount.Text);

            SQL_PARAMS[7] = OBJ_METHOD.createParams("@PAMOUNT", SqlDbType.Decimal, 0, "0.00");
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "Collection Against Advance");

            OBJ_METHOD.ExecuteProceedure("USP_COLLECTION", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[14];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtdate.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);

                SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, lblbed.Text);

                SQL_PARAMS[7] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, txtamount.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PAYMENT");
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, '-' + txtamount.Text);

                SQL_PARAMS[11] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, "0");
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "ADVANCE DETAILS");

                OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_PAYMENT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    SQL_PARAMS = new SqlParameter[11];

                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "insert_credit");
                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, txtdate.Text);

                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Amount", SqlDbType.Decimal, 500, txtamount.Text);

                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropemp.SelectedValue);
                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@PMODE", SqlDbType.VarChar, 500, droppayment.Text);
                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@IPD", SqlDbType.VarChar, 500, lblip.Text);
                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@TOTALAMT", SqlDbType.Decimal, 500, lbltotalamt.Text);

                    OBJ_METHOD.ExecuteProceedure("RECEP_ADVANCEPAY_INSUP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                    if (OBJ_METHOD._RESULT > 0)
                    {
                        SQL_PARAMS = new SqlParameter[12];

                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "insert_advnc");
                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, txtdate.Text);

                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Amount", SqlDbType.Decimal, 500, txtamount.Text);

                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropemp.SelectedValue);
                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, lblbed.Text);

                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);
                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@PMODE", SqlDbType.VarChar, 500, droppayment.Text);
                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@PNO", SqlDbType.VarChar, 500, txtcard.Text);

                        OBJ_METHOD.ExecuteProceedure("RECEP_ADVANCEPAY_INSUP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                        if (OBJ_METHOD._RESULT > 0)
                        {
                            OBJ_METHOD.commitOrRollbackTran("commit");
                            clearcontrol();
                        }
                    }
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region OLDCODE
            //using (SqlCommand cmd = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert_advnc";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
            //    cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
            //    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
            //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
            //    cmd.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@IPD", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = "0.00";
            //    cmd.ExecuteNonQuery();
            //}

            //using (SqlCommand cmd1 = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "insert_credit";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
            //    cmd1.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
            //    cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = dropemp.SelectedValue;
            //    cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.SelectedValue;
            //    cmd1.Parameters.Add("@IPD", SqlDbType.VarChar).Value = lblip.Text;
            //    cmd1.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                
            //    cmd1.ExecuteNonQuery();
            //}

            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtamount.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //    cm.ExecuteNonQuery();
            //}
            //using (SqlCommand cm = new SqlCommand("USP_COLLECTION", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
            //    cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@USERID", SqlDbType.VarChar).Value = lblid.Text;
            //    cm.Parameters.Add("@CAMOUNT", SqlDbType.VarChar).Value = txtamount.Text;
            //    cm.Parameters.Add("@PAMOUNT", SqlDbType.Decimal).Value = "0.00";
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "Collection Against Advance";
            //    cm.ExecuteNonQuery();
            //}

            //Session["ADID"] = txtid.Text;
            //binddata();
            //con.Close();
            //clearcontrol();
            #endregion

            Session["ADID"] = txtid.Text;
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
        Response.Redirect("~/RECEPTION/Reception_advancebill.aspx");
    }

    public void clearcontrol()
    {
        txtamount.Text = lblremainamt.Text;
        div1.Visible = true;
        div2.Visible = false;
        btnupdate.Visible = false;
        btnSubmit.Visible = true;
        btnDELETE.Visible = false;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        OBJ_METHOD = new DataMathods();
        string message1 = string.Empty;
        try
        {
            if (txtamount.Text == "")
            {
                string message = "alert('*Please!! Enter The Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtamount.Text = "0";
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) <= 0)
            {
                string message = "alert('* There Is No Due Amount..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (Convert.ToDecimal(lblremainamt.Text) > 0 && Convert.ToDecimal(txtamount.Text) == 0 || txtamount.Text == "")
            {
                string message = "alert('Please!! Enter The Amount To Be Paid..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[6];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@BEDNO", SqlDbType.VarChar, 500, lblbed.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@DEBIT", SqlDbType.VarChar, 500, '-' + txtamount.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);

            SQL_PARAMS[4] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, "PAYMENT");

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_PAYMENT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                SQL_PARAMS = new SqlParameter[6];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "update");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@Amount", SqlDbType.Decimal, 500, txtamount.Text);
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, dropemp.SelectedValue);
                
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@PMODE", SqlDbType.VarChar, 500, droppayment.Text);
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@PNO", SqlDbType.VarChar, 500, txtcard.Text);

                OBJ_METHOD.ExecuteProceedure("RECEP_ADVANCEPAY_INSUP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearcontrol();
                }
            }
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            #region Oldcode
            //using (SqlCommand cmd1 = new SqlCommand("RECEP_ADVANCEPAY_INSUP", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "update";
            //    cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
            //    cmd1.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtdate.Text;
            //    cmd1.Parameters.Add("@Amount", SqlDbType.Decimal).Value = txtamount.Text;
            //    cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = dropemp.SelectedValue;
            //    cmd1.Parameters.Add("@PMODE", SqlDbType.VarChar).Value = droppayment.SelectedItem.Text;
            //    cmd1.Parameters.Add("@IPD", SqlDbType.VarChar).Value = lblip.Text;
            //    cmd1.Parameters.Add("@TOTALAMT", SqlDbType.Decimal).Value = lbltotalamt.Text;
                
            //    cmd1.ExecuteNonQuery();
            //}

            //using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //    cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //    cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
            //    cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = txtamount.Text;
            //    cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //    //cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
            //    cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + txtamount.Text;
            //    cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //    cm.ExecuteNonQuery();
            //}
            #endregion
            Session["ADID"] = txtid.Text;
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);

        }
        Response.Redirect("~/RECEPTION/Reception_advancebill.aspx");
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }

    protected void btndelete_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[4];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 200, "PAYMENT");
            SQL_PARAMS1[3] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 200, lblip.Text);

            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_PAYMENT", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);
            if (OBJ_METHOD._RESULT > 0)
            {
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE_ID");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);

                OBJ_METHOD.ExecuteProceedure("RECEP_ADVANCEPAY_INSUP", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Due to some issues, Data not Deleted.')";
                }

            }
            #region oldcode
            //using (SqlCommand cmd = new SqlCommand("RECEPTION_ADVANCEPAY_SELEDELE", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE_ID";
            //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;

            //    //SqlCommand cmd = new SqlCommand("delete from tblAdvancePayment where id=@id", con);
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.ExecuteNonQuery();
            //    using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_PAYMENT", con))
            //    {
            //        cm.CommandType = CommandType.StoredProcedure;
            //        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            //        cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
            //        cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
            //        cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //        cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
            //        cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = "PAYMENT";
            //        cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = LBPAIDAMT.Text;
            //        cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
            //        cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = '-' + LBPAIDAMT.Text;
            //        cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = "0";
            //        cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //        cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "ADVANCE DETAILS";
            //        cm.ExecuteNonQuery();
            //    }
            //}

            //binddata();
            #endregion
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }

    protected void droppayment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (droppayment.SelectedIndex == 0)
            {
                txtcard.Visible = false;
                txtcard.Text = "";
            }
            else if (droppayment.SelectedIndex == 3)
            {
                dropemp.Visible = true;
                // txtcard.Visible = false;
                //txtcard.Text = "";
            }
            else
            {
                txtcard.Visible = true;
                dropemp.Visible = true;
                txtcard.Text = "";
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

    }
}