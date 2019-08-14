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

public partial class RECEPTION_PatientSearch : System.Web.UI.Page
{
    SqlConnection con,con1,con2,con3;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7, da8;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3, cmd4, cmd5, cmd6, cmd7, cmd8, cmd9, cmd10, cmd11, cmd12, cmd13, cmd14, cmd15, cmd16, cmd17;
    SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
    DataTable dt;
    DataRow dtr;

    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
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
                div1.Visible = true;
                div2.Visible = true;
            }
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Response.Write(ex.Message);
        //}
    }
    public void binddata()
    {
        //try
        //{
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
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
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);

        //}

        //try
        //{
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
           // con.Open();

            //using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@DATETIME", SqlDbType.DateTime).Value = DateTime.Now.ToString("dd-MM-yyyy");
            //    cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
            //    cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";

            //    DataTable dt = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(dt);

            //    DropDownList1.DataSource = dt;
            //    DropDownList1.DataTextField = "BEDNO";
            //    DropDownList1.DataValueField = "BEDNO";
            //    DropDownList1.DataBind();
            //    DropDownList1.Items.Insert(0, "Please Select");

            //}
            SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO FROM BED_TABLE", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //dropbedno.SelectedIndex = 0;
            DropDownList1.DataSource = dt1;
            DropDownList1.DataTextField = "BEDNO";
            DropDownList1.DataValueField = "BEDNO";
            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, "Please Select");
          
            con.Close();
        //}
        //catch (Exception ex)
        //{
        // Console.WriteLine("An error occurred: '{0}'", ex);

        //}
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        //try
        //{
            if (txtspid.Text == "")
            {
                string message = "alert('* Please Enter OPD No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtspid.Text != "")
            {
                SqlCommand cmd2 = new SqlCommand("select * from ADMISSION_TABLE where ID='" + txtspid.Text + "'", con);
                SqlDataReader dr1 = cmd2.ExecuteReader();
                if (dr1.Read() == false)
                {
                    string message = "alert('Invalid OPD Number.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                dr1.Close();
            }
        
            using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REGISTRATION_ID";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtspid.Text;
                cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {

                        string message = "alert(' No Record Found')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    }
                    else
                    {
                        GridView1.SelectedIndex = 0;
                        GridView1.DataSource = dt;
                        GridView1.DataKeyNames = new string[] { "ID" };
                        GridView1.DataBind();
                    }
                }
                
            }
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void btnshowph_Click(object sender, EventArgs e)
    {
        //try
        //{

            if (txtsmobile.Text == "")
            {
                string message = "alert('* Please Enter Phone Number..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_REGISTRATION_PHNO";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = txtsmobile.Text;
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Rows.Count == 0)
                    {

                        string message1 = "alert(' No Data is there..')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                    }
                    else
                    {
                        GridView1.SelectedIndex = 0;
                        GridView1.DataSource = dt;
                        GridView1.DataKeyNames = new string[] { "ID" };
                        GridView1.DataBind();
                    }
                }
                else
                {
                    string message = "alert('No Record Found..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                }

                
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,PNAME,TELPHNO,MOBNO,DATETIME FROM REGISTRATION_TBL WHERE TELPHNO= '" + txtsmobile.Text + "' ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView1.SelectedIndex = 0;
            //GridView1.DataSource = dt;
            //GridView1.DataKeyNames = new string[] { "ID" };
            //GridView1.DataBind();
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
          
        //}
    }
    protected void btnshowip_Click(object sender, EventArgs e)
    {
       //try
       // {
            if (txtip.Text == "")
            {
                string message = "alert('* Please Enter A Valid Ip No..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("SP_PATIENT_SEARCH", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISCHARGE";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtip.Text;
                cmd1.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtip.Text;
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    string message = "alert('* This Pateint Is Discharged..')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                    return;
                  //  dr1.Close();
                }
                else
                {
                    dr1.Close();
                    using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_VN";
                        cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                        cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                        cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                        cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                        cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = txtip.Text;
                        cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                        dr = cmd.ExecuteReader();
                        if (dr.Read())
                        {
                            dr.Close();
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            if (dt.Rows.Count == 0)
                            {

                                string message = "alert(' No Record Found')";
                                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                            }
                            else
                            {
                                GridView2.SelectedIndex = 0;
                                GridView2.DataSource = dt;
                                GridView2.DataKeyNames = new string[] { "VN" };
                                GridView2.DataBind();
                            }
                        }
                        else
                        {
                            string message = "alert('Ip Number Is not Valid..')";
                            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                            return;
                        }
                    }
                }
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT A.VN,A.BEDNO,A.PNAME,B.ID AS ID,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.VN= '" + txtip.Text + "' and A.ORGID='" + lblorgid.Text + "' ORDER BY ID DESC ", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView2.SelectedIndex = 0;
            //GridView2.DataSource = dt;
            //GridView2.DataBind();
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void btnshowbed_Click(object sender, EventArgs e)
    {
        //try
        //{
            if (DropDownList1.SelectedIndex == 0)
            {
                string message = "alert('* Please Select The Bed..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_BED";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = DropDownList1.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    GridView2.SelectedIndex = 0;
                    GridView2.DataSource = dt;
                    GridView2.DataKeyNames = new string[] { "VN" };
                    GridView2.DataBind();
                }
                
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT A.ID AS ID,A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.BEDNO= '" + DropDownList1.Text + "' and A.ORGID='" + lblorgid.Text + "'ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView2.SelectedIndex = 0;
            //GridView2.DataSource = dt;
            //GridView2.DataBind();
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void btnshowname_Click(object sender, EventArgs e)
    {
        //try
        //{
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            if (txtname.Text == "")
            {
                string message = "alert('* Please Enter Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ADMISSION_NAME";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = txtname.Text;
                cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    GridView2.SelectedIndex = 0;
                    GridView2.DataSource = dt;
                    GridView2.DataKeyNames = new string[] { "VN" };
                    GridView2.DataBind();
                }
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT A.ID AS ID,A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ID AS ID,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN AND A.PNAME Like '" + txtname.Text + "%' and A.ORGID='" + lblorgid.Text + "' ORDER BY ID DESC ", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView2.SelectedIndex = 0;
            //GridView2.DataSource = dt;
            //GridView2.DataBind();
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void btnshowall_Click(object sender, EventArgs e)
    {
    //    try
    //    {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SP_PATIENT_SEARCH", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ALL";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@TELPHNO", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@VN", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count == 0)
                {

                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
                else
                {
                    GridView2.SelectedIndex = 0;
                    GridView2.DataSource = dt;
                    GridView2.DataKeyNames = new string[] { "VN" };
                    GridView2.DataBind();
                }
            }
            //SqlDataAdapter da = new SqlDataAdapter("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ID AS ID,B.ECONTACT,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN ORDER BY ID DESC", con);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //GridView2.SelectedIndex = 0;
            //GridView2.DataSource = dt;
            //GridView2.DataBind();
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT A.VN,A.BEDNO,A.PNAME,B.PHONE,B.ECONTACT,B.ID AS ID,B.DISEASE,B.DATE FROM BED_TABLE A, ADMISSION_TABLE B WHERE A.VN=B.VN ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView2.SelectedIndex = 0;
            GridView2.DataSource = dt;
            GridView2.PageIndex = e.NewPageIndex;
            //GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //try
        //{
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM REGISTRATION_TBL  ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            //GridView2.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
            con.Close();
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("An error occurred: '{0}'", ex);
        //}
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
          var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["VN"].ToString();
            Session["s_id"] = slno;
            Response.Redirect("~/RECEPTION/IP_Charges.aspx");
       
    }
}