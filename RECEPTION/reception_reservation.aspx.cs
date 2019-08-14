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

public partial class RECEPTION_reception_reservation : System.Web.UI.Page
{
    string num1 = "0";
    SqlDataReader dr, rr;
    SqlConnection con;
    SqlCommand com;

    DataMathods OBJ_METHOD = new DataMathods();

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
                auto();
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
           
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select BOOKING_NO AS slno from RESERVATION_TABLE order by ID DESC";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();
        string str1 = "1";
        if (dr.Read())
        {
            num1 = dr["slno"].ToString();
            string str = num1.Substring(0, num1.Length - 10);//delete last 10 record
            string d = str.Substring(3);//delete first 3 record
            str1 = (Convert.ToInt64(d) + 1).ToString();//add in numbers
        }

        txtbookingno.Text = "BK-" + str1 + "-" + lblfyear.Text;
        dr.Close();
        con.Close();
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

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELCT_RESRV_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_RESERAVATION", false, true, SQL_PARAMS1);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.DataBind();
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[2];
            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPT");
            SQL_PARAMS2[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_RESERAVATION", false, true, SQL_PARAMS2);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropdept.DataSource = Ds;
                dropdept.DataTextField = "DeptName";
                dropdept.DataValueField = "id";
                dropdept.DataBind();
                dropdept.Items.Insert(0, new ListItem("Please Select", "0"));

            }
            
        }
        catch (Exception ex)
        {
            ErrorLog.Write(ex);
        }

    }
 

    public void clearcontrol()
    {
        txtbookingno.Text = "";
        txtbookingfor.Text = "";
        txtname.Text = "";
        txtpatientid.Text = "";
        dropgender.SelectedValue = "0";
        dropbloodgroup.SelectedValue = "0";
        txtdateofbirth.Text = "";
        txtaddress.Text = "";
        txtpin.Text = "";
        txtdistrict.Text = "";
        txtstate.Text = "";
        txtmobileno.Text = "";
        txtemailid.Text = "";
        txtfrom.Text = "";
        txtrefname.Text = "";
        txtcomplaint.Text = "";
        dropdept.SelectedIndex = 0;
        dropdoctor.SelectedIndex = 0;
        txttreatment.Text = "";
        droproomtype.Text = "";
        txtoperationdate.Text = "";
        //txtbookingdate.Text = "";
        txtdays.Text = "";
        txttelno.Text = "";
        dropbloodgroup.SelectedIndex = 0;
        dropgender.SelectedIndex = 0;
    }
    
    protected void  Button1_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            // Validation
            if (txtbookingfor.Text == "")
            {
                string message = "alert('*Please!! Enter The Reason For Booking..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbookingfor.Focus();
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('*Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (dropgender.SelectedItem.Text == "0")
            {
                string message = "alert('*Please!!Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropgender.Focus();
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('*Please!!Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtaddress.Focus();
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Please!! Enter The Mobile Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmobileno.Focus();
                return;
            }

            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Please!! Enter The Booking Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbookingdate.Focus();
                return;
            }
            else
            {

                auto();

                SqlParameter[] Sql_Params = new SqlParameter[28];

                Sql_Params[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
                Sql_Params[1] = OBJ_METHOD.createParams("@BOOKING_NO", SqlDbType.VarChar, 500, txtbookingno.Text);
                Sql_Params[2] = OBJ_METHOD.createParams("@BOOKING_DATE", SqlDbType.DateTime, 0, DateTime.Now.ToString());
                Sql_Params[3] = OBJ_METHOD.createParams("@BOOKING_FOR", SqlDbType.VarChar, 500, txtbookingfor.Text);
                Sql_Params[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
                Sql_Params[5] = OBJ_METHOD.createParams("@PATIENT_ID", SqlDbType.VarChar, 500, txtpatientid.Text);
                Sql_Params[6] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.Text);
                Sql_Params[7] = OBJ_METHOD.createParams("@DATEOFBIRTH", SqlDbType.DateTime, 0, txtdateofbirth.Text);
                Sql_Params[8] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
                Sql_Params[9] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 500, txtpin.Text);
                Sql_Params[10] = OBJ_METHOD.createParams("@DISTRICT", SqlDbType.VarChar, 500, txtdistrict.Text);
                Sql_Params[11] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
                Sql_Params[12] = OBJ_METHOD.createParams("@TELEPHONE", SqlDbType.VarChar, 500, txttelno.Text);
                Sql_Params[13] = OBJ_METHOD.createParams("@MOBILE", SqlDbType.VarChar, 500, txtmobileno.Text);
                Sql_Params[14] = OBJ_METHOD.createParams("@EMAIL", SqlDbType.VarChar, 500, txtemailid.Text);
                Sql_Params[15] = OBJ_METHOD.createParams("@FROM", SqlDbType.VarChar, 500, txtfrom.Text);
                Sql_Params[16] = OBJ_METHOD.createParams("@REFERALNAME", SqlDbType.VarChar, 500, txtrefname.Text);
                Sql_Params[17] = OBJ_METHOD.createParams("@COMPLAINT", SqlDbType.VarChar, 500, txtcomplaint.Text);
                Sql_Params[18] = OBJ_METHOD.createParams("@CONSULTINGDEPT", SqlDbType.VarChar, 500, dropdept.SelectedValue);
                Sql_Params[19] = OBJ_METHOD.createParams("@DOCTORNAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
                Sql_Params[20] = OBJ_METHOD.createParams("@TREATEMENT", SqlDbType.VarChar, 500, txttreatment.Text);
                Sql_Params[21] = OBJ_METHOD.createParams("@ROOMTYPE", SqlDbType.VarChar, 500, droproomtype.Text);
                Sql_Params[22] = OBJ_METHOD.createParams("@OPERATIONDATE", SqlDbType.DateTime, 0, txtoperationdate.Text);
                Sql_Params[23] = OBJ_METHOD.createParams("@BOOKINGFORDATE", SqlDbType.DateTime, 0, txtbookingdate.Text);
                Sql_Params[24] = OBJ_METHOD.createParams("@DAYS", SqlDbType.VarChar, 500, txtdays.Text);
                Sql_Params[25] = OBJ_METHOD.createParams("@BLOODGROUP", SqlDbType.VarChar, 500, dropbloodgroup.Text);
                Sql_Params[26] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["BRANCH_FYR"]);
                Sql_Params[27] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["Branch"]);

                OBJ_METHOD.ExecuteProceedure("USP_RESERVATION", "", "@msg", SqlDbType.VarChar, Sql_Params, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                }
                msg = "alert('" + OBJ_METHOD._objOut + "')";

            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            msg = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
          
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);

        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        string msg = string.Empty;
        try
        {
            // Validation
            if (txtbookingfor.Text == "")
            {
                string message = "alert('*Please!! Enter The Reason For Booking..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbookingfor.Focus();
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('*Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (dropgender.SelectedItem.Text == "0")
            {
                string message = "alert('*Please!!Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropgender.Focus();
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('*Please!!Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtaddress.Focus();
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Please!! Enter The Mobile Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmobileno.Focus();
                return;
            }

            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Please!! Enter The Booking Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbookingdate.Focus();
                return;
            }
            
                
                SqlParameter[] Sql_Params = new SqlParameter[26];

                Sql_Params[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
                Sql_Params[1] = OBJ_METHOD.createParams("@BOOKING_NO", SqlDbType.VarChar, 500, txtbookingno.Text);
                Sql_Params[2] = OBJ_METHOD.createParams("@BOOKING_DATE", SqlDbType.DateTime, 0, DateTime.Now.ToString());
                Sql_Params[3] = OBJ_METHOD.createParams("@BOOKING_FOR", SqlDbType.VarChar, 500, txtbookingfor.Text);
                Sql_Params[4] = OBJ_METHOD.createParams("@NAME", SqlDbType.VarChar, 500, txtname.Text);
                Sql_Params[5] = OBJ_METHOD.createParams("@PATIENT_ID", SqlDbType.VarChar, 500, txtpatientid.Text);
                Sql_Params[6] = OBJ_METHOD.createParams("@GENDER", SqlDbType.VarChar, 500, dropgender.Text);
                Sql_Params[7] = OBJ_METHOD.createParams("@DATEOFBIRTH", SqlDbType.DateTime, 0, txtdateofbirth.Text);
                Sql_Params[8] = OBJ_METHOD.createParams("@ADDRESS", SqlDbType.VarChar, 500, txtaddress.Text);
                Sql_Params[9] = OBJ_METHOD.createParams("@PIN", SqlDbType.VarChar, 500, txtpin.Text);
                Sql_Params[10] = OBJ_METHOD.createParams("@DISTRICT", SqlDbType.VarChar, 500, txtdistrict.Text);
                Sql_Params[11] = OBJ_METHOD.createParams("@STATE", SqlDbType.VarChar, 500, txtstate.Text);
                Sql_Params[12] = OBJ_METHOD.createParams("@TELEPHONE", SqlDbType.VarChar, 500, txttelno.Text);
                Sql_Params[13] = OBJ_METHOD.createParams("@MOBILE", SqlDbType.VarChar, 500, txtmobileno.Text);
                Sql_Params[14] = OBJ_METHOD.createParams("@EMAIL", SqlDbType.VarChar, 500, txtemailid.Text);
                Sql_Params[15] = OBJ_METHOD.createParams("@FROM", SqlDbType.VarChar, 500, txtfrom.Text);
                Sql_Params[16] = OBJ_METHOD.createParams("@REFERALNAME", SqlDbType.VarChar, 500, txtrefname.Text);
                Sql_Params[17] = OBJ_METHOD.createParams("@COMPLAINT", SqlDbType.VarChar, 500, txtcomplaint.Text);
                Sql_Params[18] = OBJ_METHOD.createParams("@CONSULTINGDEPT", SqlDbType.VarChar, 500, dropdept.SelectedValue);
                Sql_Params[19] = OBJ_METHOD.createParams("@DOCTORNAME", SqlDbType.VarChar, 500, dropdoctor.SelectedValue);
                Sql_Params[20] = OBJ_METHOD.createParams("@TREATEMENT", SqlDbType.VarChar, 500, txttreatment.Text);
                Sql_Params[21] = OBJ_METHOD.createParams("@ROOMTYPE", SqlDbType.VarChar, 500, droproomtype.Text);
                Sql_Params[22] = OBJ_METHOD.createParams("@OPERATIONDATE", SqlDbType.DateTime, 0, txtoperationdate.Text);
                Sql_Params[23] = OBJ_METHOD.createParams("@BOOKINGFORDATE", SqlDbType.DateTime, 0, txtbookingdate.Text);
                Sql_Params[24] = OBJ_METHOD.createParams("@DAYS", SqlDbType.VarChar, 500, txtdays.Text);
                Sql_Params[25] = OBJ_METHOD.createParams("@BLOODGROUP", SqlDbType.VarChar, 500, dropbloodgroup.Text);

                OBJ_METHOD.ExecuteProceedure("USP_RESERVATION", "", "@msg", SqlDbType.VarChar, Sql_Params, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    binddata();
                    btnSubmit.Visible = true;
                    btnupdate.Visible = false;
                }
                msg = "alert('" + OBJ_METHOD._objOut + "')";
            }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            msg = "alert('Due to some issues, Data not saved.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", msg, true);
        }

        
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        clearcontrol();
    }
   
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            string slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();

            SqlParameter[] SQL_PARAMS = new SqlParameter[3];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_EVENT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 500, slno);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_RESERAVATION", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {

                btnSubmit.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = slno;
                txtbookingno.Text =  Ds.Tables[0].Rows[0]["BOOKING_NO"].ToString();
                txtdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["BOOKINGFORDATE"]).ToString("dd-MM-yyyy");
                txtbookingfor.Text =  Ds.Tables[0].Rows[0]["BOOKING_FOR"].ToString();
                txtname.Text =  Ds.Tables[0].Rows[0]["NAME"].ToString();
                txtpatientid.Text =  Ds.Tables[0].Rows[0]["PATIENT_ID"].ToString();
                dropgender.Text =  Ds.Tables[0].Rows[0]["GENDER"].ToString();
                dropbloodgroup.Text =  Ds.Tables[0].Rows[0]["BLOODGROUP"].ToString();
                txtdateofbirth.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DATEOFBIRTH"]).ToString("dd-MM-yyyy");
                txtaddress.Text =  Ds.Tables[0].Rows[0]["ADDRESS"].ToString();
                txtpin.Text =  Ds.Tables[0].Rows[0]["PIN"].ToString();
                txtdistrict.Text =  Ds.Tables[0].Rows[0]["DISTRICT"].ToString();
                txtstate.Text =  Ds.Tables[0].Rows[0]["STATE"].ToString();
                txttelno.Text =  Ds.Tables[0].Rows[0]["TELEPHONE"].ToString();
                txtmobileno.Text =  Ds.Tables[0].Rows[0]["MOBILE"].ToString();
                txtemailid.Text =  Ds.Tables[0].Rows[0]["EMAIL"].ToString();
                txtfrom.Text =  Ds.Tables[0].Rows[0]["FROM"].ToString();
                txtrefname.Text =  Ds.Tables[0].Rows[0]["REFERALNAME"].ToString();
                txtcomplaint.Text =  Ds.Tables[0].Rows[0]["COMPLAINT"].ToString();
                dropdept.SelectedValue =  Ds.Tables[0].Rows[0]["CONSULTINGDEPT"].ToString();
                Session["D_ID"] =  Ds.Tables[0].Rows[0]["DOCTORNAME"].ToString();
                txttreatment.Text =  Ds.Tables[0].Rows[0]["TREATEMENT"].ToString();
                droproomtype.Text =  Ds.Tables[0].Rows[0]["ROOMTYPE"].ToString();
                txtoperationdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["OPERATIONDATE"]).ToString("dd-MM-yyyy");
                txtbookingdate.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["BOOKINGFORDATE"]).ToString("dd-MM-yyyy");
                txtdays.Text =  Ds.Tables[0].Rows[0]["DAYS"].ToString();
            }
            
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[3];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFF");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Deptid", SqlDbType.VarChar, 500, dropdept.SelectedValue);
            SQL_PARAMS1[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_RESERAVATION", false, true, SQL_PARAMS1);
            if (Ds1.Tables[0].Rows.Count > 0)
            {
                    dropdoctor.DataSource = Ds1;
                    dropdoctor.DataTextField = "Sname";
                    dropdoctor.DataValueField = "id";
                    dropdoctor.DataBind();
                    dropdoctor.Items.Insert(0, "---select---");
                    if (IsPostBack && Session["D_ID"] != "")
                    {
                        dropdoctor.SelectedValue = Session["D_ID"].ToString();
                    }
            }
                       
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

            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELCT_RESRV_PAGE");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

            DataSet Ds1 = OBJ_METHOD.Get_DataSet("RECP_RESERAVATION", false, true, SQL_PARAMS1);


            if (Ds1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = Ds1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
         string message1 = string.Empty;
            try
            {
                string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
                SqlParameter[] SQL_PARAMS = new SqlParameter[3];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@ID", SqlDbType.VarChar, 100, slno);
                SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);

                OBJ_METHOD.ExecuteProceedure("USP_RESERVATION", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

                if (OBJ_METHOD._RESULT > 0)
                {
                    OBJ_METHOD.commitOrRollbackTran("commit");
                    // clearcontrol();
                }
                message1 = "alert('" + OBJ_METHOD._objOut + "')";


            }
            catch (Exception ex)
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not deleted.')";
            }
            finally
            {
                binddata();
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            }
                      
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[4].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
            }
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void dropdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
           
            SqlParameter[] SQL_PARAMS = new SqlParameter[3];
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFF");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Deptid", SqlDbType.VarChar, 500, dropdept.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.VarChar, 500, Session["Branch"]);
           
            DataSet Ds = OBJ_METHOD.Get_DataSet("RECP_RESERAVATION", false, true, SQL_PARAMS);
            if (Ds.Tables[0].Rows.Count > 0)
            {
                dropdoctor.DataSource = Ds;
                dropdoctor.DataTextField = "Sname";
                dropdoctor.DataValueField = "id";
                dropdoctor.DataBind();
                dropdoctor.Items.Insert(0, new ListItem ("Please Select","0"));

            }
            else
            {
                string message = "alert('No Record Found..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}