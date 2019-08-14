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

public partial class RECEPTION_Reservation : System.Web.UI.Page
{
    string num1 = "0";
    SqlDataReader dr,rr;
    SqlConnection con;
    SqlCommand com;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
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

            if (!IsPostBack)
            {
                binddata();
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
            con.Close();
            
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
            string str = num1.Substring(0,num1.Length - 10);//delete last 10 record
            string d = str.Substring(3);//delete first 3 record
             str1 =(Convert.ToInt64(d)+1).ToString();//add in numbers
        }

        txtbookingno.Text = "BK-" + str1 + "-" + lblfyear.Text;
        dr.Close();
        con.Close();
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
        using (SqlCommand cmd = new SqlCommand("RECP_RESERAVATION", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELCT_RESRV_PAGE";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "NULL";
            DataTable Dt = new DataTable();
            SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //SqlDataAdapter Adp = new SqlDataAdapter("SELECT * from RESERVATION_TABLE order by ID desc", con);
            //DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            GridView1.DataSource = Dt;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
        }


        using (SqlCommand cmd1 = new SqlCommand("RECP_RESERAVATION", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DEPT";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter Adp2 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter Adp2 = new SqlDataAdapter("select id,DeptName from tblDepartment", con);
            DataTable Dt2 = new DataTable();
            Adp2.Fill(Dt2);
            dropdept.DataSource = Dt2;
            dropdept.DataTextField = "DeptName";
            dropdept.DataValueField = "id";
            dropdept.DataBind();
            dropdept.Items.Insert(0, "---select---");
        }
        con.Close();
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
        dropdept.SelectedIndex=0;
        dropdoctor.SelectedIndex = 0;
        txttreatment.Text = "";
        droproomtype.Text = "";
        txtoperationdate.Text = "";
        txtbookingdate.Text = "";
        txtdays.Text = "";
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            // Validation
            if (txtbookingfor.Text == "")
            {
                string message = "alert('*Please!! Enter The Reason For Booking..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('*Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropgender.SelectedItem.Text == "0")
            {
                string message = "alert('*Please!!Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('*Please!!Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Please!! Enter The Mobile Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Please!! Enter The Booking Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
               
                    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                    //con.Open();
                    //using (SqlCommand cmd = new SqlCommand("RECP_RESERAVATION", con))
                    //{
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_RESRV_ID";
                    //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtpatientid.Text;
                    //    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                    //    cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "NULL";
                    //    SqlDataAdapter Adp2 = new SqlDataAdapter(cmd);
                    //    //SqlCommand cmd = con.CreateCommand();
                    //    //cmd.CommandText = "  Select * from [[ADMISSION_TABLE]] where ID='" + txtpatientid.Text + "'";
                    //    rr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //    if (rr.Read() == true)
                    //    {
                    //        rr.Close();
                    //        con.Close();
                    //        //Response.Write("<script>alert('" + TextBox1.Text + ", Already Exist !! ')</script>");
                    //        string message4 = "alert(' Patient With OPD Number-  " + txtpatientid.Text + " Is already Admitted.')";
                    //        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message4, true);
                    //        //clr();
                    //    }
                    //}
               
          
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
                con.Open();
                auto();
                using (SqlCommand cmd = new SqlCommand("USP_RESERVATION", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                    // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
                    cmd.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = txtbookingno.Text;
                    cmd.Parameters.Add("@BOOKING_DATE", SqlDbType.Date).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@BOOKING_FOR", SqlDbType.VarChar).Value = txtbookingfor.Text;
                    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                    cmd.Parameters.Add("@PATIENT_ID", SqlDbType.VarChar).Value = txtpatientid.Text;
                    cmd.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.Text;
                    cmd.Parameters.Add("@BLOODGROUP", SqlDbType.VarChar).Value = dropbloodgroup.Text;
                    cmd.Parameters.Add("@DATEOFBIRTH", SqlDbType.Date).Value = txtdateofbirth.Text;
                    cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                    cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                    cmd.Parameters.Add("@DISTRICT", SqlDbType.VarChar).Value = txtdistrict.Text;
                    cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                    cmd.Parameters.Add("@TELEPHONE", SqlDbType.VarChar).Value = txttelno.Text;
                    cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = txtmobileno.Text;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemailid.Text;
                    cmd.Parameters.Add("@FROM", SqlDbType.VarChar).Value = txtfrom.Text;
                    cmd.Parameters.Add("@REFERALNAME", SqlDbType.VarChar).Value = txtrefname.Text;
                    cmd.Parameters.Add("@COMPLAINT", SqlDbType.VarChar).Value = txtcomplaint.Text;
                    cmd.Parameters.Add("@CONSULTINGDEPT", SqlDbType.VarChar).Value = dropdept.SelectedValue;
                    cmd.Parameters.Add("@DOCTORNAME", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
                    cmd.Parameters.Add("@TREATEMENT", SqlDbType.VarChar).Value = txttreatment.Text;
                    cmd.Parameters.Add("@ROOMTYPE", SqlDbType.VarChar).Value = droproomtype.Text;
                    cmd.Parameters.Add("@OPERATIONDATE", SqlDbType.Date).Value = txtoperationdate.Text;
                    cmd.Parameters.Add("@BOOKINGFORDATE", SqlDbType.Date).Value = txtbookingdate.Text;
                    cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = txtdays.Text;
                    cmd.ExecuteNonQuery();
                }
                binddata();
                con.Close();
                clearcontrol();
                string message1 = "alert('Successfully Saved.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/Reservation.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            // Validation
            if (txtbookingfor.Text == "")
            {
                string message = "alert('*Please!! Enter The Reason For Booking..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtname.Text == "")
            {
                string message = "alert('*Please!! Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (dropgender.SelectedItem.Text == "0")
            {
                string message = "alert('*Please!!Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtaddress.Text == "")
            {
                string message = "alert('*Please!!Enter The Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtmobileno.Text == "")
            {
                string message = "alert('* Please!! Enter The Mobile Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
           
            else if (txtbookingdate.Text == "")
            {
                string message = "alert('* Please!! Enter The Booking Date..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else
            {
                
                auto();
                using (SqlCommand cmd = new SqlCommand("USP_RESERVATION", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                    // cmd.Parameters.Add("@ID", SqlDbType.Int).Value = txtid.Text;
                    cmd.Parameters.Add("@BOOKING_NO", SqlDbType.VarChar).Value = txtid.Text;
                    cmd.Parameters.Add("@BOOKING_DATE", SqlDbType.DateTime).Value = DateTime.Now.ToString("yyyy-MM-dd");
                    cmd.Parameters.Add("@BOOKING_FOR", SqlDbType.VarChar).Value = txtbookingfor.Text;
                    cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = txtname.Text;
                    cmd.Parameters.Add("@PATIENT_ID", SqlDbType.VarChar).Value = txtpatientid.Text;
                    cmd.Parameters.Add("@GENDER", SqlDbType.VarChar).Value = dropgender.Text;
                    cmd.Parameters.Add("@BLOODGROUP", SqlDbType.VarChar).Value = dropbloodgroup.Text;
                    cmd.Parameters.Add("@DATEOFBIRTH", SqlDbType.Date).Value = txtdateofbirth.Text;
                    cmd.Parameters.Add("@ADDRESS", SqlDbType.VarChar).Value = txtaddress.Text;
                    cmd.Parameters.Add("@PIN", SqlDbType.VarChar).Value = txtpin.Text;
                    cmd.Parameters.Add("@DISTRICT", SqlDbType.VarChar).Value = txtdistrict.Text;
                    cmd.Parameters.Add("@STATE", SqlDbType.VarChar).Value = txtstate.Text;
                    cmd.Parameters.Add("@TELEPHONE", SqlDbType.VarChar).Value = txttelno.Text;
                    cmd.Parameters.Add("@MOBILE", SqlDbType.VarChar).Value = txtmobileno.Text;
                    cmd.Parameters.Add("@EMAIL", SqlDbType.VarChar).Value = txtemailid.Text;
                    cmd.Parameters.Add("@FROM", SqlDbType.VarChar).Value = txtfrom.Text;
                    cmd.Parameters.Add("@REFERALNAME", SqlDbType.VarChar).Value = txtrefname.Text;
                    cmd.Parameters.Add("@COMPLAINT", SqlDbType.VarChar).Value = txtcomplaint.Text;
                    cmd.Parameters.Add("@CONSULTINGDEPT", SqlDbType.VarChar).Value = dropdept.SelectedValue;
                    cmd.Parameters.Add("@DOCTORNAME", SqlDbType.VarChar).Value = dropdoctor.SelectedValue;
                    cmd.Parameters.Add("@TREATEMENT", SqlDbType.VarChar).Value = txttreatment.Text;
                    cmd.Parameters.Add("@ROOMTYPE", SqlDbType.VarChar).Value = droproomtype.Text;
                    cmd.Parameters.Add("@OPERATIONDATE", SqlDbType.Date).Value = txtoperationdate.Text;
                    cmd.Parameters.Add("@BOOKINGFORDATE", SqlDbType.Date).Value = txtbookingdate.Text;
                    cmd.Parameters.Add("@DAYS", SqlDbType.VarChar).Value = txtdays.Text;
                    cmd.ExecuteNonQuery();
                }
            }
            binddata();
            con.Close();
            clearcontrol();
            string message1 = "alert('Successfully Updated.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
          
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/Reservation.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RECEPTION/Reservation.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            string slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            //var slno = GridView1.DataKeys[e.NewSelectedIndex].Values[""].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RESERAVATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "NULL";
                //DataTable Dt = new DataTable();
                //SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("SELECT * from RESERVATION_TABLE where id='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btnSubmit.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = slno;
                    txtbookingno.Text = dr["BOOKING_NO"].ToString();
                    txtdate.Text = dr["BOOKING_FOR"].ToString();
                    txtbookingfor.Text = dr["BOOKING_FOR"].ToString();
                    txtname.Text = dr["NAME"].ToString();
                    txtpatientid.Text = dr["PATIENT_ID"].ToString();
                    dropgender.Text = dr["GENDER"].ToString();
                    dropbloodgroup.Text = dr["BLOODGROUP"].ToString();
                    txtdateofbirth.Text = dr["DATEOFBIRTH"].ToString();
                    txtaddress.Text = dr["ADDRESS"].ToString();
                    txtpin.Text = dr["PIN"].ToString();
                    txtdistrict.Text = dr["DISTRICT"].ToString();
                    txtstate.Text = dr["STATE"].ToString();
                    txttelno.Text = dr["TELEPHONE"].ToString();
                    txtmobileno.Text = dr["MOBILE"].ToString();
                    txtemailid.Text = dr["EMAIL"].ToString();
                    txtfrom.Text = dr["FROM"].ToString();
                    txtrefname.Text = dr["REFERALNAME"].ToString();
                    txtcomplaint.Text = dr["COMPLAINT"].ToString();
                    dropdept.SelectedValue = dr["CONSULTINGDEPT"].ToString();
                    Session["D_ID"] = dr["DOCTORNAME"].ToString();
                    txttreatment.Text = dr["TREATEMENT"].ToString();
                    droproomtype.Text = dr["ROOMTYPE"].ToString();
                    txtoperationdate.Text = dr["OPERATIONDATE"].ToString();
                    txtbookingdate.Text = dr["BOOKINGFORDATE"].ToString();
                    txtdays.Text = dr["DAYS"].ToString();
                }
                dr.Close();
                using (SqlCommand cmd1 = new SqlCommand("RECP_RESERAVATION", con))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STAFF";
                    cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                    cmd1.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                    cmd1.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = dropdept.SelectedValue;
                    //DataTable Dt = new DataTable();
                    SqlDataAdapter Adp1 = new SqlDataAdapter(cmd1);
                    //SqlDataAdapter Adp1 = new SqlDataAdapter("select id,Sname from tblStaff where Deptid='" + dropdept.SelectedValue + "'", con);
                    DataTable Dt1 = new DataTable();
                    Adp1.Fill(Dt1);
                    dropdoctor.DataSource = Dt1;
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
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RESERAVATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELCT_RESRV_PAGE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "NULL";
                //DataTable Dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT * from RESERVATION_TABLE", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            using (SqlCommand cmd = new SqlCommand("RECP_RESERAVATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = "NULL";
                //DataTable Dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand cm = new SqlCommand("delete from RESERVATION_TABLE where ID='" + slno + "'", con);
                cmd.ExecuteNonQuery();
            }

            binddata();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
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
            Console.WriteLine("An error occurred: '{0}'", ex);
            //Response.Write(ex.Message);
        }
    }
    protected void dropdept_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_RESERAVATION", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_STAFF";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@NAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Deptid", SqlDbType.VarChar).Value = dropdept.SelectedValue;
                //DataTable Dt = new DataTable();
                SqlDataAdapter Adp1 = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp1 = new SqlDataAdapter("select id,Sname from tblStaff where Deptid='" + dropdept.SelectedValue + "'", con);
                DataTable Dt1 = new DataTable();
                Adp1.Fill(Dt1);
                dropdoctor.DataSource = Dt1;
                dropdoctor.DataTextField = "Sname";
                dropdoctor.DataValueField = "id";
                dropdoctor.DataBind();
                dropdoctor.Items.Insert(0, "---select---");
                //if (IsPostBack && Session["D_ID"] != "")
                //{
                //    dropdoctor.SelectedValue = Session["D_ID"].ToString();
                //}
            }
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
          //  Response.Write(ex.Message);
        }
    }
}