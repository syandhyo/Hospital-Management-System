using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class RECEPTION_reception_IP_Charges : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7, da8;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16, cmd17;
    SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl;
    int flag = 0, stock = 0, stock_tran = 0, trans = 0, stock1 = 0, S, CLOSEING, slno, CLOSE;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    public string id_hist, PRICE;
    public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b, c, d, e, f, g, h, j, F;
    decimal amount = 0, amountu = 0;
    DateTime DT;
    double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();
    public void auto()
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            
            string qry1 = "select C_ID from tblAllCharge";

            com = new SqlCommand(qry1, con);
            dr = null;

            dr = com.ExecuteReader();

            while (dr.Read())
            {
                num1 = dr["C_ID"].ToString();
            }
            num1 = string.Format("CG{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
            txtid.Text = num1;

            dr.Close();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string c_id = Session["s_id"].ToString();
            //string sc_id = Session["p_id"].ToString();
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
            //Sqlcmdmand cmd = new Sqlcmdmand("select * from ADMISSION_TABLE where VN='" + c_id+"'",con);
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION_4TABLE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, c_id);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                lblname.Text = DS2.Tables[0].Rows[0]["NAME"].ToString();
                lblage.Text = DS2.Tables[0].Rows[0]["AGE"].ToString();
                lbladd.Text = DS2.Tables[0].Rows[0]["PADDRESS"].ToString();
                lblgender.Text = DS2.Tables[0].Rows[0]["GENDER"].ToString();
                lblip.Text = DS2.Tables[0].Rows[0]["VN"].ToString();
                lbladdate.Text = Convert.ToDateTime(DS2.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
                lblcorpid.Text = DS2.Tables[0].Rows[0]["CORPORATE"].ToString();
            }
            if (lblcorpid.Text != "0")
            {
                SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_COEPORATE_TABLE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, lblcorpid.Text);

                DataSet DS = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    lblcorp.Text = DS.Tables[0].Rows[0]["CNAME"].ToString();
                }
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELCT_BED_TABLE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                lblbed.Text = DS1.Tables[0].Rows[0]["BEDNO"].ToString();
                lblward.Text = DS1.Tables[0].Rows[0]["WARD"].ToString();
            }
            else
            {
                SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION_4TABLE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);

                DataSet Ds = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
                if (Ds.Tables[0].Rows.Count > 0)
                {
                    lblbed.Text = Ds.Tables[0].Rows[0]["BEDNO"].ToString();
                    lblward.Text = Ds.Tables[0].Rows[0]["WARD"].ToString();
                }
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELCT_PA_MASTER");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                lbltoamt.Text = Ds1.Tables[0].Rows[0]["CREDIT"].ToString();
                lblpaid.Text = Ds1.Tables[0].Rows[0]["DEBIT"].ToString();
            }
            #region Oldcode
            //using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
            //{
            //    try
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_4TABLE";
            //        cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = c_id;
            //        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //        cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
            //        cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
            //        dr = cmd.ExecuteReader();
            //        if (dr.Read())
            //        {
            //            lblname.Text = dr["NAME"].ToString();
            //            lblage.Text = dr["AGE"].ToString();
            //            lbladd.Text = dr["PADDRESS"].ToString();
            //            lblgender.Text = dr["GENDER"].ToString();
            //            lblip.Text = dr["VN"].ToString();
            //            lbladdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy");
            //            lblcorpid.Text = dr["CORPORATE"].ToString();
            //            dr.Close();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("An error occurred: '{0}'", ex);
            //    }

            //}
            //if (lblcorpid.Text != "0")
            //{

            //using (SqlCommand cmd1 = new SqlCommand("SP_IP_CHARGE", con))
            //{
            //    try
            //    {
            //        cmd1.CommandType = CommandType.StoredProcedure;
            //        cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_COEPORATE_TABLE";
            //        cmd1.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = lblcorpid.Text;
            //        cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //        cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //        cmd1.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
            //        cmd1.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
            //        //SqlCommand cmd1 = new SqlCommand("SELECT * FROM Corporate_Table WHERE ID='" + lblcorpid.Text + "'", con1);
            //        dr1 = cmd1.ExecuteReader();
            //        if (dr1.Read())
            //        {
            //            lblcorp.Text = dr1["CNAME"].ToString();
            //        }
            //        dr1.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("An error occurred: '{0}'", ex);
            //    }
            //}
            //}

            //using (SqlCommand cmd2 = new SqlCommand("SP_IP_CHARGE", con))
            //{

            //    try
            //    {
            //        cmd2.CommandType = CommandType.StoredProcedure;
            //        cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELCT_BED_TABLE";
            //        cmd2.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
            //        cmd2.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //        cmd2.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //        cmd2.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
            //        cmd2.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
            //        dr2 = cmd2.ExecuteReader();
            //        if (dr2.Read())
            //        {
            //            lblbed.Text = dr2["BEDNO"].ToString();
            //            lblward.Text = dr2["WARD"].ToString();
            //            dr2.Close();
            //        }

            //        else
            //        {
            //            dr2.Close();
            //            using (SqlCommand cmd3 = new SqlCommand("SP_IP_CHARGE", con))
            //            {
            //                cmd3.CommandType = CommandType.StoredProcedure;
            //                cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_4TABLE";
            //                cmd3.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //                cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //                cmd3.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //                cmd3.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
            //                cmd3.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
            //                dr3 = cmd3.ExecuteReader();
            //                if (dr3.Read())
            //                {
            //                    lblbed.Text = dr3["BEDNO"].ToString();
            //                    lblward.Text = dr3["WARD"].ToString();
            //                    dr3.Close();
            //                    //con3.Close();
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("An error occurred: '{0}'", ex);
            //    }
            //}

            //using (SqlCommand cm = new SqlCommand("SP_IP_CHARGE", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELCT_PA_MASTER";
            //    cm.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
            //    cm.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //    cm.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
            //    cm.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
            //    dr4 = cm.ExecuteReader();
            //    if (dr4.Read())
            //    {
            //        lbltoamt.Text = dr4["CREDIT"].ToString();
            //        lblpaid.Text = dr4["DEBIT"].ToString();
            //        dr4.Close();

            //    }
            //}
            #endregion

            lbldue.Text = ((Convert.ToDouble(lbltoamt.Text)) - (Convert.ToDouble(lblpaid.Text))).ToString();
            if (!IsPostBack)
            {
                binddata();
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ALL");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS2;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
                Grvcharge.Visible = true;
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELCT_CATAGORY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                ddlcate.DataSource = Ds;
                ddlcate.DataTextField = "CATEGORY";
                ddlcate.DataValueField = "ID";
                ddlcate.DataBind();
                ddlcate.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        #region oldcode
        //using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ALL";
        //    cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //    DataTable dt = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);

        //    da.Fill(dt);
        //    GridView1.SelectedIndex = 0;
        //    GridView1.DataSource = dt;
        //    GridView1.DataKeyNames = new string[] { "ID" };
        //    GridView1.DataBind();
        //    Grvcharge.Visible = true;
        //}

        ////SqlDataAdapter da1 = new SqlDataAdapter("select * from CATEGORY_MASTER", con);
        ////DataTable ds = new DataTable();
        //using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELCT_CATAGORY";
        //    cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //    DataTable ds = new DataTable();
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //    da1.Fill(ds);
        //    ddlcate.DataSource = ds;
        //    ddlcate.DataTextField = "CATEGORY";
        //    ddlcate.DataValueField = "ID";
        //    ddlcate.DataBind();
        //    ddlcate.Items.Insert(0,new ListItem("Please Select","0"));
        //}
        //con.Close();
        #endregion
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {

            auto();
            foreach (GridViewRow row in Grvcharge.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                    string dropcharge = (row.Cells[1].FindControl("lblcharge") as Label).Text;
                    string txtqty = (row.Cells[2].FindControl("txtqty") as TextBox).Text;
                    string i_price = (row.Cells[3].FindControl("lblprice") as Label).Text;
                    string charge = (row.Cells[4].FindControl("lbltotal") as Label).Text;
                    if (txtqty == "" || txtqty == "0")
                    {
                        string message = "alert('Please!! Enter The Quantity For '" + charge + "' In Table')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        dr.Close();
                    }

                }
            }
            SqlParameter[] SQL_PARAMS = new SqlParameter[13];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT_TBLCHARGE2");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, lblbed.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@BDate", SqlDbType.VarChar, 500, lbladdate.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, lbluid.Text);
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@CHARGETYPE", SqlDbType.VarChar, 500, ddlcate.SelectedItem.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@DSR", SqlDbType.VarChar, 500, "OTHER CHARGES");
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, lbltot.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@corporate", SqlDbType.VarChar, 500, lblcorp.Text);

            OBJ_METHOD.ExecuteProceedure("SP_IP_CHARGE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            int chkedcounter = 0;
            int correctinput = 0;
            if (OBJ_METHOD._RESULT > 0)
            {
                foreach (GridViewRow row in Grvcharge.Rows)
                {
                    chkedcounter++;
                    if (row.RowType == DataControlRowType.DataRow)
                    {

                        CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                        string dropcharge = (row.Cells[1].FindControl("lblcharge") as Label).Text;
                        string txtqty = (row.Cells[2].FindControl("txtqty") as TextBox).Text;
                        string i_price = (row.Cells[3].FindControl("lblprice") as Label).Text;
                        string charge = (row.Cells[4].FindControl("lbltotal") as Label).Text;
                        if (txtqty == "" || txtqty == "0")
                        {
                            //string message = "alert('Please!! Enter The Quantity For '" + charge + "' In Table')";
                            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            //dr.Close();
                        }
                        else
                        {
                            SQL_PARAMS = new SqlParameter[13];

                            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DATETIME", SqlDbType.DateTime, 0, txtdate.Text);//invIt.Text
                            SQL_PARAMS[2] = OBJ_METHOD.createParams("@VOUCHERNO", SqlDbType.VarChar, 500, txtid.Text);
                            SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);
                            SQL_PARAMS[4] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, lblbed.Text);
                            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DESCRIPTION", SqlDbType.VarChar, 500, dropcharge.ToString());
                            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                            SQL_PARAMS[8] = OBJ_METHOD.createParams("@CHARGES", SqlDbType.VarChar, 500, charge.ToString());
                            SQL_PARAMS[9] = OBJ_METHOD.createParams("@PTYPE", SqlDbType.VarChar, 500, "INPATIENT");
                            SQL_PARAMS[10] = OBJ_METHOD.createParams("@CREDIT", SqlDbType.VarChar, 500, charge.ToString());
                            SQL_PARAMS[11] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);
                            SQL_PARAMS[12] = OBJ_METHOD.createParams("@CATEGORY", SqlDbType.VarChar, 500, "OTHER CHARGES");

                            OBJ_METHOD.ExecuteProceedure("USP_PA_TRAN_CREDIT", "", "", SqlDbType.VarChar, SQL_PARAMS, true);


                            if (OBJ_METHOD._RESULT > 0)
                            {
                                if (dropcharge.ToString() == "BED CHARGE")
                                {
                                    SQL_PARAMS = new SqlParameter[3];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "BEDUPDATE");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@UHID", SqlDbType.VarChar, 0, UHID.Text);//invIt.Text
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@BCHARGE", SqlDbType.Decimal, 0, Convert.ToDecimal(txtqty.ToString()));

                                    OBJ_METHOD.ExecuteProceedure("per_tran", "", "", SqlDbType.VarChar, SQL_PARAMS, true);


                                    if (OBJ_METHOD._RESULT > 0)
                                    {
                                        SQL_PARAMS = new SqlParameter[14];

                                        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT_TBLCHARGE");
                                        SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                                        SQL_PARAMS[2] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, txtid.Text);
                                        SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);
                                        SQL_PARAMS[4] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, lblbed.Text);
                                        SQL_PARAMS[5] = OBJ_METHOD.createParams("@BDate", SqlDbType.DateTime, 0, lbladdate.Text);
                                        SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                        SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                                        SQL_PARAMS[8] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, lbluid.Text);
                                        SQL_PARAMS[9] = OBJ_METHOD.createParams("@CHARGETYPE", SqlDbType.VarChar, 500, ddlcate.SelectedItem.Text);
                                        SQL_PARAMS[10] = OBJ_METHOD.createParams("@DSR", SqlDbType.VarChar, 500, "OTHER CHARGES");
                                        SQL_PARAMS[11] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, charge.ToString());
                                        SQL_PARAMS[12] = OBJ_METHOD.createParams("@quantity", SqlDbType.VarChar, 500, Convert.ToDecimal(txtqty));
                                        SQL_PARAMS[13] = OBJ_METHOD.createParams("@initial_price", SqlDbType.VarChar, 500, Convert.ToDecimal(i_price));

                                        OBJ_METHOD.ExecuteProceedure("SP_IP_CHARGE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


                                        if (OBJ_METHOD._RESULT > 0)
                                        {
                                            correctinput++;
                                        }
                                    }
                                }
                                else
                                {
                                    SQL_PARAMS = new SqlParameter[14];

                                    SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT_TBLCHARGE");
                                    SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);//invIt.Text
                                    SQL_PARAMS[2] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, txtid.Text);
                                    SQL_PARAMS[3] = OBJ_METHOD.createParams("@PID", SqlDbType.VarChar, 500, lblip.Text);
                                    SQL_PARAMS[4] = OBJ_METHOD.createParams("@Bedno", SqlDbType.VarChar, 500, lblbed.Text);
                                    SQL_PARAMS[5] = OBJ_METHOD.createParams("@BDate", SqlDbType.DateTime, 0, lbladdate.Text);
                                    SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                                    SQL_PARAMS[7] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));
                                    SQL_PARAMS[8] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, lbluid.Text);
                                    SQL_PARAMS[9] = OBJ_METHOD.createParams("@CHARGETYPE", SqlDbType.VarChar, 500, ddlcate.SelectedItem.Text);
                                    SQL_PARAMS[10] = OBJ_METHOD.createParams("@DSR", SqlDbType.VarChar, 500, "OTHER CHARGES");
                                    SQL_PARAMS[11] = OBJ_METHOD.createParams("@PRICE", SqlDbType.VarChar, 500, charge.ToString());
                                    SQL_PARAMS[12] = OBJ_METHOD.createParams("@quantity", SqlDbType.VarChar, 500, Convert.ToDecimal(txtqty));
                                    SQL_PARAMS[13] = OBJ_METHOD.createParams("@initial_price", SqlDbType.VarChar, 500, Convert.ToDecimal(i_price));

                                    OBJ_METHOD.ExecuteProceedure("SP_IP_CHARGE_INSERT", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);


                                    if (OBJ_METHOD._RESULT > 0)
                                    {
                                        correctinput++;
                                    }
                                }
                            }
                        }

                    }
                    //message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
                if (chkedcounter == correctinput && chkedcounter > 0)
                {

                    OBJ_METHOD.commitOrRollbackTran("commit");
                    clearControl();
                    message1 = "alert('" + OBJ_METHOD._objOut + "')";
                }
                else
                {
                    OBJ_METHOD.commitOrRollbackTran("rollback");
                    message1 = "alert('Error occurred while processing data... Rolling back...')";
                }
            }
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
        #region oldcode
        //insert();

        //using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT_TBLCHARGE2";
        //    cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = txtid.Text;
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
        //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
        //    cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = lbladdate.Text;
        //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
        //    cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
        //    cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "OTHER CHARGES";
        //    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = lbltot.Text;
        //    cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = lblcorp.Text;
        //    cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.ExecuteNonQuery();
        //    binddata();
        //}
        //con.Close();

        //Response.Redirect("~/nurse_IP_Charges.aspx");
        #endregion
    }
    public void clearControl()
    {
        ddlcate.SelectedIndex = 0;
        Grvcharge.DataSource = null;
        Grvcharge.DataBind();
        Grvcharge.Visible = false;
        Grvchargeupdate.DataSource = null;
        Grvchargeupdate.DataBind();
        btndelete.Visible = false;
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
        binddata();
    }
    //public void insert()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    foreach (GridViewRow row in Grvcharge.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

    //            if (chkRow.Checked)
    //            {
    //                string dropcharge = (row.Cells[1].FindControl("lblcharge") as Label).Text;
    //                string txtqty = (row.Cells[2].FindControl("txtqty") as TextBox).Text;
    //                string i_price = (row.Cells[3].FindControl("lblprice") as Label).Text;
    //                string charge = (row.Cells[4].FindControl("lbltotal") as Label).Text;
    //                if (txtqty == "" || txtqty == "0")
    //                {
    //                    //string message = "alert('Please!! Enter The Quantity For '" + charge + "' In Table')";
    //                    //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    //                    //dr.Close();
    //                }
    //                else
    //                {
    //                    using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
    //                    {
    //                        cmd.CommandType = CommandType.StoredProcedure;
    //                        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT_TBLCHARGE";
    //                        cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = txtid.Text;
    //                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
    //                        cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                        cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
    //                        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                        cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
    //                        cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = lbladdate.Text;
    //                        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
    //                        cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
    //                        cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //                        cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = charge.ToString();
    //                        cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = Convert.ToDecimal(txtqty);
    //                        cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = Convert.ToDecimal(i_price);
    //                        cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
    //                        cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
    //                        cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = lblcorp.Text;
    //                        cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
    //                        //SqlCommand cmd = new SqlCommand("insert into tblAllCharge(C_ID,ORGID,PID,Bedno,BDate,UserId,CHARGETYPE,DSR,quantity,initial_price,PRICE)values(@C_ID,@ORGID,@PID,@Bedno,@BDate,@UserId,@CHARGETYPE,@DSR,@quantity,@initial_price,@PRICE)", con);
    //                        //cmd.Parameters.Add("@C_ID", SqlDbType.VarChar).Value = txtid.Text;
    //                        //cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                        //cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                        //cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
    //                        //cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = txtdate.Text;
    //                        //cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
    //                        //cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
    //                        //cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = dropcharge.ToString();
    //                        //cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = Convert.ToDecimal(txtqty);
    //                        //cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = Convert.ToDecimal(i_price);
    //                        //cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = charge.ToString();
    //                        cmd.ExecuteNonQuery();

    //                    }
    //                    using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
    //                    {
    //                        cm.CommandType = CommandType.StoredProcedure;
    //                        cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
    //                        cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
    //                        cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
    //                        cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                        cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
    //                        cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = dropcharge.ToString();
    //                        cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = charge.ToString();
    //                        cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
    //                        cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
    //                        cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = charge.ToString();
    //                        cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
    //                        cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //                        cm.ExecuteNonQuery();
    //                    }


    //                    if (dropcharge.ToString() == "BED CHARGE")
    //                    {
    //                        using (SqlCommand cm1 = new SqlCommand("per_tran", con))
    //                        {
    //                            cm1.CommandType = CommandType.StoredProcedure;
    //                            cm1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "BEDUPDATE";
    //                            cm1.Parameters.Add("@REF", SqlDbType.VarChar).Value = "";
    //                            cm1.Parameters.Add("@UHID", SqlDbType.VarChar).Value = UHID.Text;
    //                            cm1.Parameters.Add("@MISCHARGE", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@PCHARGE", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@PP", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@LCHARGE", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@LP", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@BCHARGE", SqlDbType.Decimal).Value = Convert.ToDecimal(txtqty);
    //                            cm1.Parameters.Add("@BP", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@MP", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@RCHARGE", SqlDbType.Decimal).Value = "0.00";
    //                            cm1.Parameters.Add("@RP", SqlDbType.Decimal).Value = "0.00";

    //                            cm1.ExecuteNonQuery();
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    con.Close();
    //}
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //    con.Open();
        //    using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "PERMISSION_UPDATE";
        //        cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = txtid.Text;
        //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
        //        cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
        //        cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
        //        cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = lbladdate.Text;
        //        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
        //        cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
        //        cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "OTHER CHARGES";
        //        cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "";
        //        cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "";
        //        cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "";
        //        cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //        cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = lblcorp.Text;
        //        cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //        //SqlCommand com = new SqlCommand("select * from  USER_PERMISSION_TABLE where ID='" + Session["UID"].ToString() + "' and PER='Edit' and selected='False'", con);
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            string message = "alert('* You Cant Edit.')";
        //            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //            return;

        //        }

        //        else
        //        {
        //            foreach (GridViewRow row in Grvchargeupdate.Rows)
        //            {
        //                dr.Close();
        //                if (row.RowType == DataControlRowType.DataRow)
        //                {

        //                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow1") as CheckBox);
        //                    string dropcharge = (row.Cells[1].FindControl("lblcharge1") as Label).Text;
        //                    string txtqty = (row.Cells[2].FindControl("txtqty1") as TextBox).Text;
        //                    string i_price = (row.Cells[3].FindControl("lblprice1") as Label).Text;
        //                    string charge = (row.Cells[4].FindControl("lbltotal1") as Label).Text;
        //                    if (chkRow.Checked && (txtqty == "" || txtqty == "0"))
        //                    {
        //                        string message = "alert('Please!! Enter The Quantity For '" + charge + "' In Table')";
        //                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        //                        return;
        //                        //dr.Close();
        //                    }
        //                }
        //            }

        //            UPDATE();
        //            binddata();

        //        }

        //    }
        //    Response.Write("<script LANGUAGE='JavaScript' >alert('Update Successful')</script>");
        //    con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
        ////Response.Redirect("~/NURSE/Ip_charges.aspx");
    }
    #region  oldcode
    //public void UPDATE()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    SqlCommand cm = new SqlCommand("delete from tblAllCharge where C_ID='" + txtid.Text + "'", con);
    //    cm.ExecuteNonQuery();

    //    //SqlCommand cm1 = new SqlCommand("delete from tblAllCharge where C_ID='" + txtid.Text + "'", con);
    //    //cm1.ExecuteNonQuery();

    //    //SqlCommand cm2 = new SqlCommand("delete from tblAllCharge where C_ID='" + txtid.Text + "'", con);
    //    //cm2.ExecuteNonQuery();
    //    foreach (GridViewRow row in Grvchargeupdate.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {

    //            CheckBox chkRow = (row.Cells[0].FindControl("chkRow1") as CheckBox);
    //            string dropcharge = (row.Cells[1].FindControl("lblcharge1") as Label).Text;
    //            string txtqty = (row.Cells[2].FindControl("txtqty1") as TextBox).Text;
    //            string i_price = (row.Cells[3].FindControl("lblprice1") as Label).Text;
    //            string charge = (row.Cells[4].FindControl("lbltotal1") as Label).Text;
    //            if (chkRow.Checked)
    //            {
    //                using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
    //                {
    //                    cmd.CommandType = CommandType.StoredProcedure;
    //                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT_TBLCHARGE";
    //                    cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = txtid.Text;
    //                    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
    //                    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
    //                    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
    //                    cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = lbladdate.Text;
    //                    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
    //                    cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
    //                    cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //                    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = charge.ToString();
    //                    cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = Convert.ToDecimal(txtqty);
    //                    cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = Convert.ToDecimal(i_price);
    //                    cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
    //                    cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
    //                    cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = lblcorp.Text;
    //                    cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
    //                    //SqlCommand cmd = new SqlCommand("insert into tblAllCharge(C_ID,ORGID,PID,Bedno,BDate,UserId,CHARGETYPE,DSR,quantity,initial_price,PRICE)values(@C_ID,@ORGID,@PID,@Bedno,@BDate,@UserId,@CHARGETYPE,@DSR,@quantity,@initial_price,@PRICE)", con);
    //                    //cmd.Parameters.Add("@C_ID", SqlDbType.VarChar).Value = txtid.Text;
    //                    //cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
    //                    //cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                    //cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = lblbed.Text;
    //                    //cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = txtdate.Text;
    //                    //cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;
    //                    //cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
    //                    //cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = dropcharge.ToString();
    //                    //cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = Convert.ToDecimal(txtqty);
    //                    //cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = Convert.ToDecimal(i_price);
    //                    //cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = charge.ToString();
    //                    cmd.ExecuteNonQuery();
    //                }

    //                using (SqlCommand cm3 = new SqlCommand("USP_PA_TRAN_CREDIT", con))
    //                {
    //                    cm3.CommandType = CommandType.StoredProcedure;
    //                    cm3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE1";
    //                    cm3.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
    //                    cm3.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
    //                    cm3.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                    cm3.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
    //                    cm3.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = dropcharge;
    //                    cm3.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = charge.ToString();
    //                    cm3.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
    //                    cm3.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
    //                    cm3.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lbltot.Text;
    //                    cm3.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
    //                    cm3.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //                    cm3.ExecuteNonQuery();
    //                }
    //                using (SqlCommand cm4 = new SqlCommand("USP_PA_TRAN_CREDIT", con))
    //                {
    //                    cm4.CommandType = CommandType.StoredProcedure;
    //                    cm4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
    //                    cm4.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
    //                    cm4.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
    //                    cm4.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                    cm4.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
    //                    cm4.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = dropcharge;
    //                    cm4.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = charge.ToString();
    //                    cm4.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
    //                    cm4.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
    //                    cm4.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lbltot.Text;
    //                    cm4.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
    //                    cm4.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //                    cm4.ExecuteNonQuery();
    //                }
    //            }
    //        }
    //    }
    //    con.Close();
    //}
    #endregion
    protected void btndelete_Click(object sender, EventArgs e)
    {
        
    }
    //public void delete()
    //{
    //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
    //    con.Open();
    //    foreach (GridViewRow row in Grvchargeupdate.Rows)
    //    {
    //        if (row.RowType == DataControlRowType.DataRow)
    //        {
    //            string dropcharge = (row.Cells[1].FindControl("lblcharge1") as Label).Text;
    //            string txtqty = (row.Cells[2].FindControl("txtqty1") as TextBox).Text;
    //            string charge = (row.Cells[3].FindControl("lblprice1") as Label).Text;
    //            using (SqlCommand cm = new SqlCommand("USP_PA_TRAN_CREDIT", con))
    //            {
    //                cm.CommandType = CommandType.StoredProcedure;
    //                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
    //                cm.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = txtdate.Text;
    //                cm.Parameters.Add("@VOUCHERNO", SqlDbType.VarChar).Value = txtid.Text;
    //                cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = lblip.Text;
    //                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = lblbed.Text;
    //                cm.Parameters.Add("@DESCRIPTION", SqlDbType.VarChar).Value = dropcharge;
    //                cm.Parameters.Add("@CHARGES", SqlDbType.VarChar).Value = Convert.ToDecimal(charge);
    //                cm.Parameters.Add("@PTYPE", SqlDbType.VarChar).Value = "INPATIENT";
    //                cm.Parameters.Add("@DEBIT", SqlDbType.VarChar).Value = "0";
    //                cm.Parameters.Add("@CREDIT", SqlDbType.VarChar).Value = lbltot.Text; ;
    //                cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
    //                cm.Parameters.Add("@CATEGORY", SqlDbType.VarChar).Value = "OTHER CHARGES";
    //                cm.ExecuteNonQuery();
    //            }
    //        }
    //    }

    //    con.Close();
    //}
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearControl();
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP_BY_cid");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, slno);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                btndelete.Visible = true;
                btnupdate.Visible = true;
                txtid.Text = DS.Tables[0].Rows[0]["C_ID"].ToString();
                txtdate.Text = DS.Tables[0].Rows[0]["Bdate"].ToString();
                lblip.Text = DS.Tables[0].Rows[0]["PID"].ToString();
                ddlcate.SelectedItem.Text = DS.Tables[0].Rows[0]["CHARGETYPE"].ToString();
                lblcorp.Text = DS.Tables[0].Rows[0]["corporate"].ToString();
                //LBPAIDAMT.Text = ds.Tables[0].Rows[0]["PRICE"].ToString();
                Grvchargeupdate.DataSource = DS;
                Grvchargeupdate.DataBind();
                Grvcharge.DataSource = null;
                Grvcharge.DataBind();
            }
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION_4TABLE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);

            DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                lblage.Text = DS1.Tables[0].Rows[0]["AGE"].ToString();
                lbladd.Text = DS1.Tables[0].Rows[0]["PADDRESS"].ToString();
                lblname.Text = DS1.Tables[0].Rows[0]["NAME"].ToString();
                lblward.Text = DS1.Tables[0].Rows[0]["WARD"].ToString();
                lblgender.Text = DS1.Tables[0].Rows[0]["GENDER"].ToString();
                lblbed.Text = DS1.Tables[0].Rows[0]["BEDNO"].ToString();
                lbladdate.Text = Convert.ToDateTime(DS1.Tables[0].Rows[0]["DATE"]).ToString("dd-MM-yyyy");
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region old code
        //using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
        //{
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP_BY_cid";
        //    cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = slno;
        //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
        //    cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //    //DataTable ds = new DataTable();
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    //SqlDataAdapter da = new SqlDataAdapter("select * from tblAllCharge  where C_ID='" + slno + "'", con);
        //    ds = new DataSet();
        //    da.Fill(ds);
        //    btnSubmit.Visible = false;
        //    btndelete.Visible = true;
        //    btnupdate.Visible = true;
        //    txtid.Text = ds.Tables[0].Rows[0]["C_ID"].ToString();
        //    txtdate.Text = ds.Tables[0].Rows[0]["Bdate"].ToString();
        //    lblip.Text = ds.Tables[0].Rows[0]["PID"].ToString();
        //    ddlcate.SelectedItem.Text = ds.Tables[0].Rows[0]["CHARGETYPE"].ToString();
        //    lblcorp.Text = ds.Tables[0].Rows[0]["corporate"].ToString();
        //    //LBPAIDAMT.Text = ds.Tables[0].Rows[0]["PRICE"].ToString();
        //    Grvchargeupdate.DataSource = ds;
        //    Grvchargeupdate.DataBind();
        //    Grvcharge.Visible = false;
        //    using (SqlCommand c = new SqlCommand("SP_IP_CHARGE", con))
        //    {
        //        c.CommandType = CommandType.StoredProcedure;
        //        c.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_4TABLE";
        //        c.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
        //        c.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //        c.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //        c.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
        //        c.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //        //SqlCommand C = new SqlCommand("select * from ADMISSION_TABLE WHERE VN='" + lblip.Text + "'", con);
        //        dr = c.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            lblage.Text = dr["AGE"].ToString();
        //            lbladd.Text = dr["PADDRESS"].ToString();
        //            lblname.Text = dr["NAME"].ToString();
        //            lblward.Text = dr["WARD"].ToString();
        //            lblgender.Text = dr["GENDER"].ToString();
        //            lblbed.Text = dr["BEDNO"].ToString();
        //            lbladdate.Text = Convert.ToDateTime(dr["DATE"]).ToString("dd-MM-yyyy");
        //        }
        //        dr.Close();
        //    }
        //}

        //con.Close();
        #endregion
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ALL");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet DS2 = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS2;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
                Grvcharge.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        #region Oldcode
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //    con.Open();
        //    using (SqlCommand cmd = new SqlCommand("SP_IP_CHARGE", con))
        //    {
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ALL";
        //        cmd.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //        cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //        cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
        //        cmd.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);

        //        da.Fill(dt);
        //        GridView1.SelectedIndex = 0;
        //        GridView1.DataSource = dt;
        //        GridView1.PageIndex = e.NewPageIndex;
        //        GridView1.DataKeyNames = new string[] { "ID" };
        //        GridView1.DataBind();
        //        Grvcharge.Visible = true;
        //    }
        //    con.Close();
        #endregion
    }
    protected void ddlcate_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlParameter[] SQL_PARAMS = new SqlParameter[2];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_ADMISSION_4TABLE");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@VN", SqlDbType.VarChar, 500, lblip.Text);

        DataSet DS = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
        if (DS.Tables[0].Rows.Count > 0)
        {
            txtcorpo.Text = DS.Tables[0].Rows[0]["CORPORATE"].ToString();
        }
        if (txtcorpo.Text == "0")
        {
            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP_BY_CATA");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@catagory", SqlDbType.VarChar, 500, ddlcate.SelectedItem.Text);

            DataSet Ds = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                Grvcharge.DataSource = null;
                Grvcharge.DataBind();
                Grvcharge.DataSource = Ds;
                Grvcharge.DataBind();
                Grvcharge.Visible = true;
            }
        }
        else
        {
            SQL_PARAMS = new SqlParameter[4];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DROP_BY_CATACORPO");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@catagory", SqlDbType.VarChar, 500, ddlcate.SelectedItem.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@CorporateID", SqlDbType.VarChar, 500, txtcorpo.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@APPLY_DATE", SqlDbType.DateTime, 0, DateTime.Now.ToString("yyyy-MM-dd"));

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SP_IP_CHARGE", false, true, SQL_PARAMS);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                Grvcharge.DataSource = null;
                Grvcharge.DataBind();
                Grvcharge.DataSource = Ds1;
                Grvcharge.DataBind();
                Grvcharge.Visible = true;
            }
        }
        #region oldcode
        //using (SqlCommand c = new SqlCommand("SP_IP_CHARGE", con))
        //{
        //    c.CommandType = CommandType.StoredProcedure;
        //    c.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_4TABLE";
        //    c.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@VN", SqlDbType.VarChar).Value = lblip.Text;
        //    c.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    c.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //    c.Parameters.Add("@catagory", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
        //    c.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //    //SqlCommand COM = new SqlCommand("select CORPORATE from ADMISSION_TABLE where VN='" + lblip.Text + "'", con);
        //    dr = c.ExecuteReader();
        //    if (dr.Read())
        //    {
        //        txtcorpo.Text = dr["CORPORATE"].ToString();
        //    }
        //    dr.Close();

        //    if (txtcorpo.Text == "0")
        //    {
        //        using (SqlCommand cm = new SqlCommand("SP_IP_CHARGE", con))
        //        {
        //            cm.CommandType = CommandType.StoredProcedure;
        //            cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP_BY_CATA";
        //            cm.Parameters.Add("@c_id", SqlDbType.VarChar).Value = slno;
        //            cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //            cm.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yy");
        //            cm.Parameters.Add("@catagory", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
        //            cm.Parameters.Add("@corporate", SqlDbType.VarChar).Value = "NULL";
        //            cm.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = "NULL";
        //            //SqlCommand cmd = new SqlCommand("select * from tblChargeMaster where CorporateID is NULL and catagory=@catagory", con);
        //            //cmd.Parameters.Add("@catagory", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text;
        //            DataTable dt = new DataTable();
        //            SqlDataAdapter da = new SqlDataAdapter(cm);

        //            da.Fill(dt);
        //            Grvcharge.DataSource = null;
        //            Grvcharge.DataBind();
        //            Grvcharge.DataSource = dt;
        //            //GridView1.SelectedIndex = 0;
        //            Grvcharge.DataKeyNames = new string[] { "ID" };
        //            Grvcharge.DataBind();
        //        }
        //    }
        //    else
        //    {
        //        using (SqlCommand cmd1 = new SqlCommand("SP_IP_CHARGE", con))
        //        {
        //            cmd1.CommandType = CommandType.StoredProcedure;
        //            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP_BY_CATACORPO";
        //            cmd1.Parameters.Add("@c_id", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@PID", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@Bedno", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@BDate", SqlDbType.DateTime).Value = DateTime.Now.ToString("yy-MM-dd");
        //            cmd1.Parameters.Add("@UserId", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@CHARGETYPE", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@DSR", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@quantity", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@initial_price", SqlDbType.VarChar).Value = "NULL";
        //            cmd1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd");
        //            cmd1.Parameters.Add("@catagory", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text.ToString();
        //            cmd1.Parameters.Add("@corporate", SqlDbType.VarChar).Value = txtcorpo.Text;
        //            cmd1.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = txtcorpo.Text;

        //            //SqlCommand cmd1 = new SqlCommand("select * from tblChargeMaster where APPLY_DATE=(select MAX(APPLY_DATE)AS LDATE from tblChargeMaster where APPLY_DATE<=@APPLY_DATE) and catagory=@catagory and CorporateID=@CorporateID", con);
        //            //cmd1.Parameters.Add("@APPLY_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yy-MM-dd");
        //            //cmd1.Parameters.Add("@catagory", SqlDbType.VarChar).Value = ddlcate.SelectedItem.Text.ToString();
        //            //cmd1.Parameters.Add("@CorporateID", SqlDbType.VarChar).Value = txtcorpo.Text;
        //            DataTable dt1 = new DataTable();
        //            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

        //            da1.Fill(dt1);
        //            Grvcharge.DataSource = null;
        //            Grvcharge.DataBind();
        //            Grvcharge.DataSource = dt1;
        //            //GridView1.SelectedIndex = 0;
        //            Grvcharge.DataKeyNames = new string[] { "ID" };
        //            Grvcharge.DataBind();
        //        }
        //    }
        //}
        //con.Close();
        #endregion
    }

    protected void txtqty_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grvcharge.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[1].FindControl("chkRow") as CheckBox);
                var txtqty = row.FindControl("txtqty") as TextBox;
                var charge = row.FindControl("lblprice") as Label;
                var total = row.FindControl("lbltotal") as Label;
                if (chkRow.Checked)
                {
                    total.Text = ((Convert.ToDouble(txtqty.Text)) * (Convert.ToDouble(charge.Text))).ToString();
                }
                else
                {
                    total.Text = "0";
                }
                amount = amount + Convert.ToDecimal(total.Text);
                //tot.Text = (Convert.ToDouble(total.Text)+ Convert.ToDouble(lbltot.Text)).ToString();
            }
        }
        lbltot.Text = amount.ToString();

    }
    protected void chkRow_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grvcharge.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);
                var txtqty = row.FindControl("txtqty") as TextBox;
                {
                    if (chkRow.Checked)
                    {
                        txtqty.Enabled = true;
                    }
                    else
                    {
                        txtqty.Text = "0";
                        txtqty.Enabled = false;
                    }
                }
            }
        }
    }
    protected void chkRow1_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grvchargeupdate.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkRow = (row.Cells[1].FindControl("chkRow1") as CheckBox);
                var txtqty = row.FindControl("txtqty1") as TextBox;
                {
                    if (chkRow.Checked)
                    {
                        txtqty.Enabled = true;
                    }
                    else
                    {
                        txtqty.Text = "0";
                        txtqty.Enabled = false;
                    }
                }
            }
        }
    }
    protected void txtqty1_TextChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in Grvchargeupdate.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                CheckBox chkRow = (row.Cells[1].FindControl("chkRow1") as CheckBox);
                var txtqty = row.FindControl("txtqty1") as TextBox;
                var charge = row.FindControl("lblprice1") as Label;
                var total = row.FindControl("lbltotal1") as Label;
                if (chkRow.Checked)
                {
                    total.Text = ((Convert.ToDouble(txtqty.Text)) * (Convert.ToDouble(charge.Text))).ToString();
                }
                else
                {
                    total.Text = "0";
                }
                amountu = amountu + Convert.ToDecimal(total.Text);
                //tot.Text = (Convert.ToDouble(total.Text)+ Convert.ToDouble(lbltot.Text)).ToString();
            }
        }
        lbltot.Text = amountu.ToString();
    }

    protected void btnprov_Click(object sender, EventArgs e)
    {
        Session["i_id"] = lblip.Text;
    }
    protected void advncepay_Click(object sender, EventArgs e)
    {
        Session["i_id"] = lblip.Text;
    }
}