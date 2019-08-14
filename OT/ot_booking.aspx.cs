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

public partial class OT_ot_booking : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataMathods OBJ_METHOD = new DataMathods();

    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select id from Booking_surgery";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["id"].ToString();
        }
        num1 = string.Format("SG{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
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

        lblid.Text = Session["NAME"].ToString();
        lbluid.Text = Session["UID"].ToString();
        lblorgid.Text = Session["ORGID"].ToString();
        txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }
            DataSet Ds = OBJ_METHOD.Get_DataSet("select VN, NAME, AGE, GENDER from ADMISSION_TABLE  WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropipno.DataSource = Ds;
                dropipno.DataTextField = "VN";
                dropipno.DataValueField = "VN";
                dropipno.DataBind();
                dropipno.Items.Insert(0, new ListItem("Please Select","0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
        }
      protected void btncreate_Click(object sender, EventArgs e)
      {
           string message1 = string.Empty;
        OBJ_METHOD = new DataMathods();
        try
        {
            if (txtanaesthesia.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropipno.SelectedIndex == 0)
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
           
            auto();
            SqlParameter[] SQL_PARAMS = new SqlParameter[20];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
                //SQL_PARAMS[3] = OBJ_METHOD.createParams("@USERID", SqlDbType.VarChar, 500, lbluid.Text);
           
                SQL_PARAMS[3] = OBJ_METHOD.createParams("@DATE", SqlDbType.DateTime, 0, Convert.ToDateTime(txtinvdate.Text).ToString("yyyy-MM-dd"));
                SQL_PARAMS[4] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 50, dropipno.SelectedValue);
                SQL_PARAMS[5] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 50, lblname.Text);
                SQL_PARAMS[6] = OBJ_METHOD.createParams("@AGE", SqlDbType.VarChar, 50, lblage.Text);
                SQL_PARAMS[7] = OBJ_METHOD.createParams("@SEX", SqlDbType.VarChar, 20, lblsex.Text);
                SQL_PARAMS[8] = OBJ_METHOD.createParams("@DOCID", SqlDbType.Int, 0, Dropdoc.SelectedValue);
                SQL_PARAMS[9] = OBJ_METHOD.createParams("@SURID", SqlDbType.Int, 0, dropsurgerytype.SelectedValue);
                SQL_PARAMS[10] = OBJ_METHOD.createParams("@Surgeon", SqlDbType.VarChar, 500, txtsurgeon.Text);
                SQL_PARAMS[11] = OBJ_METHOD.createParams("@Anaesthesia", SqlDbType.VarChar, 500, txtanaesthesia.Text);
                SQL_PARAMS[12] = OBJ_METHOD.createParams("@Anaesthesist", SqlDbType.VarChar, 500, txtanaesthesist.Text);
                SQL_PARAMS[13] = OBJ_METHOD.createParams("@Pharmacist", SqlDbType.VarChar, 500, txtpharm.Text);
                SQL_PARAMS[14] = OBJ_METHOD.createParams("@staffid", SqlDbType.VarChar, 5000, txtstaff.Text);
                SQL_PARAMS[15] = OBJ_METHOD.createParams("@testID", SqlDbType.Int, 0, Droptestname.SelectedValue);
                SQL_PARAMS[16] = OBJ_METHOD.createParams("@MEDID", SqlDbType.Int, 0, Dropmedicine.SelectedValue);
                SQL_PARAMS[17] = OBJ_METHOD.createParams("@EqupID", SqlDbType.Int, 0, Dropequip.SelectedValue);
                SQL_PARAMS[18] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
                SQL_PARAMS[19] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));




                OBJ_METHOD.ExecuteProceedure("Surgery_Details", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                  
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
            }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Saved.')";
        }
        finally
        {
            binddata();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
      }
      protected void dropipno_SelectedIndexChanged(object sender, EventArgs e)
      {
          try
          {
              DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT * FROM ADMISSION_TABLE WHERE VN='" + dropipno.SelectedValue + "'", false, false);

              if (Ds1.Tables[0].Rows.Count > 0)
              {
                  lblname.Text = Ds1.Tables[0].Rows[0]["NAME"].ToString();
                  lblage.Text = Ds1.Tables[0].Rows[0]["AGE"].ToString();
                  lblsex.Text = Ds1.Tables[0].Rows[0]["GENDER"].ToString();
                  
              }
              else
              {
                  string message = "alert('No Record Found.')";
                  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                  return;
              }
          }
          catch (Exception ex)
          {
              Console.WriteLine("An error occurred: '{0}'", ex);
          }
      }
      public void clearcontrol()
      {
          txtanaesthesia.Text = txtanaesthesist.Text = txtpharm.Text = txtsurgeon.Text = lblname.Text = lblage.Text = lblsex.Text =  "";

          dropipno.SelectedIndex = dropsurgerytype.SelectedIndex = 0;
          btncreate.Visible = true;
          btnupdate.Visible = false;
          btndelete.Visible = false;
      }
}