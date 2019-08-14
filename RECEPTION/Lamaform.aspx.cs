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
public partial class RECEPTION_Lamaform : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from LAMA_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LM{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        txtid.Text = num1;

        dr.Close();
        con.Close();
    }
    
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

            }
            con.Close();
            txtdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
        using (SqlCommand cmd = new SqlCommand("RECP_LAMA_SELECT", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LAMA_PAGE";
            cmd.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,IPNO,PNAME,BEDNO FROM LAMA_TABLE ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
        using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_BED";
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO AS NAME FROM BED_TABLE", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            //dropbedno.SelectedIndex = 0;
            dropbedno.DataSource = dt1;
            dropbedno.DataTextField = "NAME";
            dropbedno.DataBind();
            dropbedno.Items.Insert(0, "Please Select");
        }

        con.Close();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropbedno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Bed No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtgname.Text == "")
            {
                string message = "alert('* Please Enter Guardian Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_IPNO";
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblipno.Text;
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                //SqlCommand com = new SqlCommand("select * from LAMA_TABLE where IPNO='" + lblipno.Text + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    string message = "alert('*Lama details for this patient already exist.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                dr.Close();
            }
            auto();
            using (SqlCommand cm = new SqlCommand("RECP_LAMA_INSER_UPDATE", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "INSERT";
                //SqlCommand cm = new SqlCommand("INSERT INTO LAMA_TABLE(ID,IPNO,PNAME,BEDNO,DATE,GNAME) VALUES(@ID,@IPNO,@PNAME,@BEDNO,@DATE,@GNAME)", con);
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblipno.Text;
                cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = lblpname.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
                cm.Parameters.Add("@GNAME", SqlDbType.VarChar).Value = txtgname.Text;
                cm.ExecuteNonQuery();
            }


            binddata();
            Session["LAID"] = txtid.Text;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }


        Response.Redirect("~/RECEPTION/lama_reciept.aspx");
    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LAMA_ID";
                cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
                cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
                cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
                //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                //SqlCommand com = new SqlCommand("select * from LAMA_TABLE where ID='" + slno + "'", con);
                dr = cmd1.ExecuteReader();
                if (dr.Read())
                {
                    btncreate.Visible = false;
                    btnupdate.Visible = true;
                    txtid.Text = dr["ID"].ToString();
                    lblipno.Text = dr["IPNO"].ToString();
                    lblpname.Text = dr["PNAME"].ToString();
                    dropbedno.SelectedItem.Text = dr["BEDNO"].ToString();
                    txtgname.Text = dr["GNAME"].ToString();

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
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DROP";
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
            //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //SqlCommand COM = new SqlCommand("SELECT A.VN,A.PNAME,B.FAMILY FROM BED_TABLE A,ADMISSION_TABLE B WHERE  A.BEDNO='" + dropbedno.Text + "' AND B.VN=A.VN", con);
            dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
                lblipno.Text = dr["VN"].ToString();
                lblpname.Text = dr["PNAME"].ToString();
                txtgname.Text = dr["FAMILY"].ToString();
            }
            else
            {
                string message = "alert('No Record Found..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            dr.Close();
        }
        con.Close();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[5].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
        }
    }
    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string slno = GridView1.DataKeys[e.RowIndex].Values["ID"].ToString();
        using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DELETE";
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = slno;
            cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
            cmd1.ExecuteReader();
            //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da = new SqlDataAdapter("delete from LAMA_TABLE where ID='" + slno + "'", con);
            //ds = new DataSet();
            //da.Fill(ds);
        }
        binddata();
        con.Close();
        Response.Redirect("~/RECEPTION/Lamaform.aspx");
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        using (SqlCommand cmd1 = new SqlCommand("RECP_LAMA_SELECT", con))
        {
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_LAMA_PAGE";
            cmd1.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@ID", SqlDbType.VarChar).Value = "NULL";
            cmd1.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = "NULL";
            SqlDataAdapter da = new SqlDataAdapter(cmd1);
            //SqlDataAdapter da = new SqlDataAdapter("SELECT ID,DATE,IPNO,PNAME,BEDNO FROM LAMA_TABLE ORDER BY ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.SelectedIndex = 0;
            GridView1.DataSource = dt;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();
        }
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropbedno.SelectedValue == "0")
            {
                string message = "alert('* Please Select Bed No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtgname.Text == "")
            {
                string message = "alert('* Please Enter Guardian Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            using (SqlCommand cm = new SqlCommand("RECP_LAMA_INSER_UPDATE", con))
            {
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
                //SqlCommand cm = new SqlCommand("UPDATE LAMA_TABLE SET IPNO=@IPNO,PNAME=@PNAME,BEDNO=@BEDNO,GNAME=@GNAME WHERE ID=@ID", con);
                cm.Parameters.Add("@DATE", SqlDbType.DateTime).Value = txtdate.Text;
                cm.Parameters.Add("@ID", SqlDbType.VarChar).Value = txtid.Text;
                cm.Parameters.Add("@IPNO", SqlDbType.VarChar).Value = lblipno.Text;
                cm.Parameters.Add("@PNAME", SqlDbType.VarChar).Value = lblpname.Text;
                cm.Parameters.Add("@BEDNO", SqlDbType.VarChar).Value = dropbedno.Text;
                cm.Parameters.Add("@GNAME", SqlDbType.VarChar).Value = txtgname.Text;
                cm.ExecuteNonQuery();
            }
            binddata();
            Session["LAID"] = txtid.Text;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/RECEPTION/lama_reciept.aspx");      
    }
    protected void Button3_Click(object sender, EventArgs e)
    {       
        binddata();
        Response.Redirect("~/RECEPTION/Lamaform.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["LAID"] = gr.Cells[0].Text;
        Response.Redirect("~/RECEPTION/lama_reciept.aspx");
    }
}