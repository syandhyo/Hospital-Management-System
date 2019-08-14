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

public partial class ADMIN_ASSET_AssetsEntry : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
                binddata();
            }

            con.Close();
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
            using (SqlCommand cmd = new SqlCommand("ADMIN_ASSET_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Assets", SqlDbType.VarChar).Value = "SELECT_DEPT";
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter Adp = new SqlDataAdapter("SELECT id,Assets,Quantity,ADate from tblAssetsEntry order by id desc", con);
                DataTable Dt = new DataTable();
                Adp.Fill(Dt);
                GridView1.DataSource = Dt;
                GridView1.DataBind();
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
            using (SqlCommand cmd = new SqlCommand("ADMIN_ASSET_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Assets", SqlDbType.VarChar).Value = "SELECT_DEPT";
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                //SqlCommand cm = new SqlCommand("delete from tblAssetsEntry where id='" + slno + "'", con);
                cmd.ExecuteNonQuery();

                binddata();
            }
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
                LinkButton db = (LinkButton)e.Row.Cells[3].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this item ?');";
            }
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
            using (SqlCommand cmd = new SqlCommand("ADMIN_ASSET_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_EVENT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Assets", SqlDbType.VarChar).Value = "SELECT_DEPT";
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                //SqlCommand com = new SqlCommand("SELECT id,Assets,Quantity,ADate from tblAssetsEntry where id='" + slno + "'", con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    txtassets.Text = dr["Assets"].ToString();
                    txtqty.Text = dr["Quantity"].ToString();
                    txtdate.Text = Convert.ToDateTime(dr["ADate"]).ToString("dd-MM-yyyy");
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

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("ADMIN_ASSET_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_GRID_PAGE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@Assets", SqlDbType.VarChar).Value = "SELECT_DEPT";
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = "NULL";
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                SqlDataAdapter Adp = new SqlDataAdapter(cmd);
                //SqlDataAdapter da = new SqlDataAdapter("SELECT id,Assets,Quantity,ADate from tblAssetsEntry", con);
                DataTable dt = new DataTable();
                Adp.Fill(dt);
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtassets.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("ADMIN_ASSET_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "";
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@Assets", SqlDbType.VarChar).Value = txtassets.Text;
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = txtqty.Text;
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                //SqlCommand cmd = new SqlCommand("insert into tblAssetsEntry(ORGID,Assets,Quantity,ADate)values(@ORGID,@Assets,@Quantity,@ADate)", con);
                //cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                //cmd.Parameters.Add("@Assets", SqlDbType.VarChar).Value = txtassets.Text;
                //cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = txtqty.Text;
                //cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

                cmd.ExecuteNonQuery();
            }

            clearcontrol();
            string message1 = "alert('Successfully Saved.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            return;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/ADMIN/ASSET/AssetsEntry.aspx");
    }
    public void clearcontrol()
    {
        txtassets.Text = "";
        txtqty.Text = "";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtassets.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtqty.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd = new SqlCommand("ADMIN_ASSET_ENTRY", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
                cmd.Parameters.Add("@Assets", SqlDbType.VarChar).Value = txtassets.Text;
                cmd.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = txtqty.Text;
                cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");
                //SqlCommand cmd1 = new SqlCommand("UPDATE tblAssetsEntry SET Assets=@Assets,Quantity=@Quantity,ADate=@ADate WHERE id=@id", con);
                //cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
                //cmd1.Parameters.Add("@Assets", SqlDbType.VarChar).Value = txtassets.Text;
                //cmd1.Parameters.Add("@Quantity", SqlDbType.VarChar).Value = txtqty.Text;
                //cmd1.Parameters.Add("@ADate", SqlDbType.DateTime).Value = Convert.ToDateTime(txtdate.Text).ToString("yyyy-MM-dd");

                cmd.ExecuteNonQuery();
            }
            binddata();
            con.Close();
            string message1 = "alert('Successfully Updated.')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/ADMIN/asset/assetsentry.aspx");
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        binddata();
        Response.Redirect("~/ADMIN/asset/assetsentry.aspx");
    }
}