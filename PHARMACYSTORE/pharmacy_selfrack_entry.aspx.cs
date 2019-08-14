using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
public partial class PHARMACYSTORE_pharmacy_selfrack_entry : System.Web.UI.Page
{
    string num1 = "SJ000";

    SqlCommand com, cmd;
    SqlDataReader dr;
    SqlDataAdapter da;
    DataTable dt;
    int i, no, no1, sl, j;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();
    public void auto()
    {
        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con1.Open();
        string qry1 = "select max(SECT_TYP) AS slno  from SELF_HDR";
        com = new SqlCommand(qry1, con1);
        dr = null;
        dr = com.ExecuteReader();
        string str1 = "1";
        if (dr.Read())
        {
            num1 = dr["slno"].ToString();
            if (num1 == "")
            {
                txtid.Text = "SEC" + "1";
            }
            else
            {
                //string str = num1.Substring(0, num1.Length - 0);//delete last 10 record
                string d = num1.Substring(3);//delete first 3 record
                str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
                txtid.Text = "SEC" + str1;
            }
        }
               dr.Close();
        con1.Close();
    }
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
            Session["SortedView"] = null;
            //binddata();
            auto();
            bindgridview();
        }
    }
    public void bindgridview()
    {
        try
        {
            DataSet DS = OBJ_METHOD.Get_DataSet("select A.SELF_ID,A.SECT_TYP,A.SELF_NM,A.RACK_NO from SELF_HDR as A where A.STORE_ID=" + Session["STOREID"].ToString() + " and A.Branch_ID='" + Session["Branch"].ToString() + "'", false, false);
            if (DS.Tables[0].Rows.Count > 0)
            {
                GridView1.SelectedIndex = 0;
                GridView1.DataSource = DS;
                GridView1.DataKeyNames = new string[] { "SELF_ID" };
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void clear_control()
    {
        txtname.Text = "";
        txtnoofbed.Text = "0";
        btncreate.Visible = true;
        //btnupdate.Visible = false;
        auto();
    }
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            string str = "select a.SELF_ID, a.SELF_NM, a.SECT_TYP, a.RACK_NO from SELF_HDR a  where a.STORE_ID="+Session["STOREID"].ToString() +" and a.Branch_ID='" + Session["Branch"].ToString() + "'";
            DataSet ds = OBJ_METHOD.Get_DataSet(str);

            DataTable dt = new DataTable();

            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }

            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "SELF_ID" };
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
            if (Session["SortedView"] != null)
            {

                GridView1.DataSource = Session["SortedView"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {

            if (txtname.Text.Trim() == "")
            {
                string message = "alert('* Ward name is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txtnoofbed.Text == "")
            {
                string message = "alert('* No of Bed mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnoofbed.Focus();
                return;
            }
           

            else if (Convert.ToDecimal(txtnoofbed.Text) <= 0)
            {
                string message = "alert('* Bed is mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtnoofbed.Focus();
                return;
            }
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[7];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@STORE_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["STOREID"]));
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@SELF_NM", SqlDbType.VarChar, 500, txtname.Text.ToUpper());
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@SECT_TYP", SqlDbType.VarChar, 500, txtid.Text.ToUpper());
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@RACK_NO", SqlDbType.Int, 0, txtnoofbed.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

            OBJ_METHOD.ExecuteProceedure("SELF_MASTER_ENTRY", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                string SLNO = OBJ_METHOD._objOut.ToString().Split('@')[0];
                string msg = OBJ_METHOD._objOut.ToString().Split('@')[1];
                no = 0;
                int cnt = 0;

                for (i = 1; i <= Convert.ToInt32(txtnoofbed.Text); i++)
                {
                    no = i;

                    SqlParameter[] SQL_PARAMS1 = new SqlParameter[6];

                    SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                    SQL_PARAMS1[1] = OBJ_METHOD.createParams("@SELF_ID", SqlDbType.VarChar, 500, SLNO);
                    SQL_PARAMS1[2] = OBJ_METHOD.createParams("@RACK_NO", SqlDbType.VarChar, 500, txtname.Text.ToUpper() + i);
                    SQL_PARAMS1[3] = OBJ_METHOD.createParams("@STORE_ID", SqlDbType.VarChar, 500, Convert.ToInt32(Session["STOREID"]));
                    SQL_PARAMS1[4] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                    SQL_PARAMS1[5] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));

                    OBJ_METHOD.ExecuteProceedure("SELF_DTL_ENTRY", "", "", SqlDbType.VarChar, SQL_PARAMS1, true);
                    if (OBJ_METHOD._RESULT > 0)
                    {
                        cnt++;
                    }
                }


                if (cnt == Convert.ToInt32(txtnoofbed.Text))
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    message1 = "alert('" + msg + "')";
                    auto();
                    clear_control();
                }



            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
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
    }
    
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/pharmacy_selfrack_entry.aspx");
    }
}