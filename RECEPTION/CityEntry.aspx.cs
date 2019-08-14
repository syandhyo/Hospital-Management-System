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

public partial class RECEPTION_CityEntry : System.Web.UI.Page
{
    SqlDataReader dr;
    SqlConnection con;
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

            binddata();

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
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
        con.Close();
        using (SqlCommand cmd = new SqlCommand("RECP_CITY_ENTRY", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CITYMASTER";
            cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter Adp = new SqlDataAdapter("select ID,CITY from CITYMASTER_TABLE", con);
            DataTable Dt = new DataTable();
            da.Fill(Dt);
            GridView1.DataSource = Dt;
            GridView1.DataBind();
        }
        con.Close();

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[1].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
            }
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
            using (SqlCommand cmd = new SqlCommand("RECP_CITY_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand cm = new SqlCommand("delete from CITYMASTER_TABLE where id='" + slno + "'", con);
                cmd.ExecuteNonQuery();
            }

            binddata();
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
            using (SqlCommand cmd = new SqlCommand("RECP_CITY_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CITYMASTER";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("select ID,CITY from CITYMASTER_TABLE", con);
                //DataTable dt = new DataTable();
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
            Session["id"] = slno;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("RECP_CITY_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ID";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                //DataTable dt = new DataTable();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //SqlCommand com = new SqlCommand("select * from CITYMASTER_TABLE where id='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtcity.Text = dr["CITY"].ToString();

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
            if (txtcity.Text == "")
            {
                string message = "alert('* Please Enter City.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_CITY_ENTRY", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CITY";
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                //SqlCommand cmd1 = new SqlCommand("select CITY from CITYMASTER_TABLE where CITY='" + txtcity.Text + "'", con);
                object i = cmd1.ExecuteScalar();
                if (i != null)
                {
                    string message = "alert('* City " + txtcity.Text + " Already Exist.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
            }
            using (SqlCommand cmd = new SqlCommand("RECP_CITY_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                //cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                //SqlCommand cmd = new SqlCommand("insert into CITYMASTER_TABLE(ORGID,CITY)values(@ORGID,@CITY)", con);
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;

                cmd.ExecuteNonQuery();
            }
            clearcontrol();
            string message1 = "alert('Successfully Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            binddata();
            con.Close();
         
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/CityEntry.aspx");

    }

    public void clearcontrol()
    {
        txtcity.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtcity.Text == "")
            {
                string message = "alert('* Please Enter City.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_CITY_ENTRY", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_CITY";
                cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd1.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd1);
                da.Fill(dt);
                using (SqlCommand cmd4 = new SqlCommand("RECP_CITY_ENTRY", con))
                {
                    cmd4.CommandType = CommandType.StoredProcedure;
                    cmd4.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE_FOR";
                    cmd4.Parameters.Add("@id", SqlDbType.VarChar).Value = Session["id"].ToString();
                    cmd4.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                    cmd4.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                    dr = cmd4.ExecuteReader();
                    if (dr.Read())
                    {
                        txtcityupdate.Text = dr["CITY"].ToString();
                    }
                    dr.Close();
                }
                
                if (dt.Rows.Count == 0)
                {
                    using (SqlCommand cmd2 = new SqlCommand("RECP_CITY_ENTRY", con))
                    {
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                        cmd2.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                        //cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                        //cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                        //SqlCommand cmd3 = new SqlCommand("UPDATE CITYMASTER_TABLE SET CITY=@CITY WHERE ID=@ID", con);
                        cmd2.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                        cmd2.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                        cmd2.ExecuteNonQuery();
                    }
                    //SqlCommand cmd1 = new SqlCommand("select CITY from CITYMASTER_TABLE where CITY='" + txtcity.Text + "'", con);
                   
                }
                else if (dt.Rows.Count == 1 && txtcityupdate.Text==txtcity.Text)
                {
                    using (SqlCommand cmd3 = new SqlCommand("RECP_CITY_ENTRY", con))
                    {
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                        cmd3.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                        //cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
                        //cmd.Parameters.Add("@CITY", SqlDbType.VarChar).Value = "NULL";
                        //SqlCommand cmd3 = new SqlCommand("UPDATE CITYMASTER_TABLE SET CITY=@CITY WHERE ID=@ID", con);
                        cmd3.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                        cmd3.Parameters.Add("@CITY", SqlDbType.VarChar).Value = txtcity.Text;
                        cmd3.ExecuteNonQuery();
                    }
                }
                else
                {
                    
                    object i = cmd1.ExecuteScalar();
                    if (i != null)
                    {
                        string message = "alert('* City " + txtcity.Text + " Already Exist.')";
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                        return;
                    }
                }
            }
            
            binddata();
            con.Close();
          
        }
        catch (Exception ex)
        {
           // Response.Write(ex.Message);
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/CityEntry.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/RECEPTION/CityEntry.aspx");
    }
}