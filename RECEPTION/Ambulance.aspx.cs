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

public partial class RECEPTION_Ambulance : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;
    DataTable Dt;

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
            lblorgid.Text = Session["ORGID"].ToString();
            lbluid.Text = Session["UID"].ToString();

            if (!IsPostBack)
            {
                binddata();
                using (SqlCommand cm = new SqlCommand("RECP_AMBULANCE", con))
                {
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_AMBULANCE";
                    cm.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                    cm.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = "NULL";
                    cm.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = "NULL";
                    cm.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = "NULL";
                    cm.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = "NULL";
                    cm.Parameters.Add("@Fee", SqlDbType.VarChar).Value = "NULL";
                    SqlDataAdapter Adp = new SqlDataAdapter(cm);
                    DataTable Dt = new DataTable();
                    Adp.Fill(Dt);
                    dropambulanceno.DataSource = Dt;
                    dropambulanceno.DataTextField = "AmbulanceNo";
                    dropambulanceno.DataValueField = "AmbulanceNo";
                    dropambulanceno.DataBind();
                    dropambulanceno.Items.Insert(0, "Please Select");
                }

                using (SqlCommand cmd = new SqlCommand("RECP_AMBULANCE", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DRIVER";
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
                    cmd.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
                    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
                    cmd.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
                    cmd.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
                    SqlDataAdapter Adp1 = new SqlDataAdapter(cmd);
                    DataTable Dt1 = new DataTable();
                    Adp1.Fill(Dt1);
                    dropdriver.DataSource = Dt1;
                    dropdriver.DataTextField = "Sname";
                    dropdriver.DataValueField = "id";
                    dropdriver.DataBind();
                    dropdriver.Items.Insert(0, "Please Select");
                }
            }
            con.Close();

            //DateTime d = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            //TimeZone zone = TimeZone.CurrentTimeZone;
            //TimeSpan local = zone.GetUtcOffset(d);


            //txtAdate.Text = local.ToString();
            txtAdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
                LinkButton db = (LinkButton)e.Row.Cells[9].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {

        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
            //Session["id"] = gr.Cells[0].Text.Trim();
            Session["id"] = gr.Cells[0].Text;
            Response.Redirect("Ambulance_receipt.aspx");
    
       
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
        using (SqlCommand cmd = new SqlCommand("RECP_AMBULANCE", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_AMBULANCE";
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
            cmd.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
            cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
            cmd.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
            cmd.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
            SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //SqlDataAdapter Adp = new SqlDataAdapter("select id,ADate,PatientName,AttendentName,ContactNo,[From],[To],ApproxKm,Fee from tblAmbulance order by id desc", con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
        }
        con.Close();
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();
            using (SqlCommand cm = new SqlCommand("RECP_AMBULANCE", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "delete";
                cm.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
                cm.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
                cm.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
                cm.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
                cm.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
                cm.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
                //SqlDataAdapter Adp = new SqlDataAdapter(cm);
                //SqlCommand cm = new SqlCommand("delete from tblAmbulance where id='" + slno + "'", con);
                cm.ExecuteNonQuery();
            }

            binddata();
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
            using (SqlCommand cmd = new SqlCommand("RECP_AMBULANCE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_AMBULANCE";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
                cmd.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
                cmd.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
                cmd.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("select id,ADate,PatientName,AttendentName,ContactNo,[From],[To],ApproxKm,Fee from tblAmbulance", con);
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

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand com = new SqlCommand("RECP_AMBULANCE", con))
            {
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SLECTED_EVENT";
                com.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
                com.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
                com.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
                com.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
                com.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
                com.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("SELECT * from tblAmbulance where id='" + slno + "'", con);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    btnSubmit.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtAdate.Text = dr["ADate"].ToString();
                    dropdriver.Text = dr["Driver"].ToString();
                    dropambulanceno.SelectedItem.Text = dr["AmbulanceNo"].ToString();
                    txtpatient.Text = dr["PatientName"].ToString();
                    txtattend.Text = dr["AttendentName"].ToString();
                    txtcont.Text = dr["ContactNo"].ToString();
                    txtfrom.Text = dr["From"].ToString();
                    txtto.Text = dr["To"].ToString();
                    txtkm.Text = dr["ApproxKm"].ToString();
                    txtfee.Text = dr["Fee"].ToString();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropambulanceno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Ambulance No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtpatient.Text == "")
            {
                string message = "alert('* Please Enter Patient Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropdriver.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Driver.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtcont.Text == "")
            {
                string message = "alert('* Please Enter Contact No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtfrom.Text == "")
            {
                string message = "alert('* Please!! Enter The From Address.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtto.Text == "")
            {
                string message = "alert('* PLease!! Enter To Address')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtkm.Text == "" || txtkm.Text == "0")
            {
                string message = "alert('* Please!! Enter Approximate Km.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //else if (txtkm.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            else if (txtfee.Text == "" || txtfee.Text =="0")
            {
                string message = "alert('* Please!! Enter The Fees.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();

            using (SqlCommand cmd = new SqlCommand("sp_Ambulance", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtAdate.Text;
                cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
                cmd.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
                cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
                cmd.Parameters.Add("@From", SqlDbType.VarChar).Value = txtfrom.Text;
                cmd.Parameters.Add("@To", SqlDbType.VarChar).Value = txtto.Text;
                cmd.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
                cmd.Parameters.Add("@Fee", SqlDbType.Decimal).Value = Convert.ToDecimal(txtfee.Text);
                cmd.Parameters.Add("@AmbulanceNo", SqlDbType.VarChar).Value = dropambulanceno.Text;
                cmd.Parameters.Add("@Driver", SqlDbType.VarChar).Value = dropdriver.SelectedValue.ToString();
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;

                cmd.ExecuteNonQuery();
            }
            using (SqlCommand cmd1 = new SqlCommand("RECP_AMBULANCE", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "report";
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = "";
                cmd1.Parameters.Add("@Fee", SqlDbType.VarChar).Value = "";
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    txtid.Text = dr["id"].ToString();
                }
            }

            clearcontrol();
            con.Close();
            binddata();

            string message1 = "alert('*Successfully Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            Session["idAmb"] = txtid.Text;
          
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("Ambulance_receipt.aspx");
    }

    public void clearcontrol()
    {
        txtAdate.Text = "";
        txtpatient.Text = "";
        txtattend.Text = "";
        txtcont.Text = "";
        txtfrom.Text = "";
        txtto.Text = "";
        txtkm.Text = "";
        txtfee.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropambulanceno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Ambulance No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtpatient.Text == "")
            {
                string message = "alert('* Please Enter Patient Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //else if (txtattend.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            else if (dropdriver.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Driver.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtcont.Text == "")
            {
                string message = "alert('* Please Enter Contact No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtfrom.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtto.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //else if (txtkm.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            else if (txtfee.Text == "")
            {
                string message = "alert('* Please Enter Fee.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_AMBULANCE", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                //SqlCommand cmd1 = new SqlCommand("UPDATE tblAmbulance SET PatientName=@PatientName,AttendentName=@AttendentName,ContactNo=@ContactNo,ApproxKm=@ApproxKm,Fee=@Fee WHERE id=@id", con);
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                cmd1.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
                cmd1.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
                cmd1.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
                //cmd1.Parameters.Add("@[From]", SqlDbType.VarChar).Value = txtfrom.Text;
                //cmd1.Parameters.Add("@[To]", SqlDbType.VarChar).Value = txtto.Text;
                cmd1.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
                cmd1.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
                //cmd1.Parameters.Add("@AmbulanceNo", SqlDbType.VarChar).Value = dropambulanceno.Text;
                //cmd1.Parameters.Add("@Driver", SqlDbType.VarChar).Value = dropdriver.SelectedValue.ToString();
                cmd1.ExecuteNonQuery();
                binddata();
            }
            con.Close();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

        Response.Redirect("~/Reception/Ambulance.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        
            binddata();
            Response.Redirect("~/Reception/Ambulance.aspx");
       
    }
}