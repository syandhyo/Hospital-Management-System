using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Web.Services;
using System.IO;

public partial class LABORATORY_Optestresult : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2, da3, da4, da5, da6, da7;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    GridViewRow gr;
    string PAIDMAT;
    decimal amount = 0;

    [WebMethod]
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select ID from LABRES_TABLE";

        com = new SqlCommand(qry1, con);
        dr = null;

        dr = com.ExecuteReader();

        while (dr.Read())
        {
            num1 = dr["ID"].ToString();
        }
        num1 = string.Format("LT{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));
        TXTID.Text = num1;
        dr.Close();
        con.Close();
    }
    public void binddata()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand COM = new SqlCommand("SELECT FYEAR FROM FYEAR1_TABLE WHERE  ORGID='" + lblorgid.Text + "'", con);
        dr = COM.ExecuteReader();
        if (dr.Read())
        {
            //Label1.Text = dr["FYEAR"].ToString();
            lblfyear.Text = dr["FYEAR"].ToString();
        }
        dr.Close();
        SqlDataAdapter da1 = new SqlDataAdapter("SELECT A.ID as ID,A.DATE AS DATE,A.PNAME AS PID FROM LABRES_TABLE A  WHERE  A.ORGID='" + lblorgid.Text + "' AND A.PTYPE!='INPATIENT' ORDER BY A.ID DESC", con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        GridView2.SelectedIndex = 0;
        GridView2.DataSource = dt1;
        GridView2.DataKeyNames = new string[] { "ID" };
        GridView2.DataBind();

    }
    protected void Page_Load(object sender, EventArgs e)
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
        txtinvdate.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ID"), new DataColumn("INV"), new DataColumn("PRICE") });
            ViewState["ITEM"] = dt;
            this.BindGrid();
            binddata();
            //da1 = new SqlDataAdapter("select ID from LABRES_TABLE where ORGID='" + lblorgid.Text + "'", con);
            //DataTable ds1 = new DataTable();
            //da1.Fill(ds1);
            //droptesttype.DataSource = ds1;
            //droptesttype.DataTextField = "ID";
            //droptesttype.DataValueField = "ID";
            //droptesttype.DataBind();
            //droptesttype.Items.Insert(0, "-----Select-----");
        }
        con.Close();
    }
    protected void BindGrid()
    {
        try
        {
            GridView1.DataSource = (DataTable)ViewState["ITEM"];
            GridView1.DataBind();

        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void droptesttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            DataTable dt = (DataTable)ViewState["ITEM"];
            dt.Clear();
            SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT,D.VALUE AS VALUE,D.slno AS slno,D.INV AS INV,E.PRICE AS PRICEE,E.DISCAMT AS DISCAMT,E.PAIDAMT AS PAIDAMT,E.DUEAMT AS DUEAMT,E.ID AS ID  from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C,LABRESULT_TABLE D,LABRES_TABLE E  WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.INV=A.slno  AND E.ID=D.ID  AND E.ID='" + droptesttype.Text + "' AND A.ORGID='" + lblorgid.Text + "'", con);

            da.Fill(dt);
            hdn_Slno.Value = dt.Rows[0]["ID"].ToString();
            ViewState["ITEM"] = dt;
            this.BindGrid();
            lbltotalprice.Text = dt.Rows[0]["PRICEE"].ToString();
            lbldiscamt.Text = dt.Rows[0]["DISCAMT"].ToString();
            lblpaidamt.Text = dt.Rows[0]["PAIDAMT"].ToString();
            lblbalanceamt.Text = dt.Rows[0]["DUEAMT"].ToString();

            GridView1.DataSource = dt;
            // GridView1.DataKeyNames = new string[] { "ID" };
            GridView1.DataBind();

            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        try
        {
            if (txtpaidLastamt.Text == "")
            {

                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            if (Convert.ToDouble(lblbalanceamt.Text) > 0)
            {
                string message1 = "alert('Balance Amount Should be Zero')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
                return;
            }
            else
            {
                datatable_Resul_item();
                datatable_Amt_Updt();
                datatable_po_item_WIDALTEST();
                Session["LABID"] = droptesttype.Text;

                Response.Redirect("~/LABORATORY/OBill.aspx");

                string message = "alert('Data Create Sucessfully.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        con.Close();
    }

    public void datatable_Resul_item()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        foreach (GridViewRow row in GridView1.Rows)
        {

            var slno = row.FindControl("lblSlno") as Label;
            var value = row.FindControl("txt_value") as TextBox;
            var reff = row.FindControl("txtref") as TextBox;
            var unit = row.FindControl("txtunit") as TextBox;


            //var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
            SqlCommand test_cmd = new SqlCommand("UPDATE LABRESULT_TABLE SET REF=@REF,UNIT=@UNIT,VALUE=@VALUE WHERE slno=@slno", con);
            test_cmd.Parameters.Add("@slno", SqlDbType.Int).Value = slno.Text.ToString();
            // stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
            test_cmd.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
            //  stock_cmd.Parameters.Add("@PRICE", SqlDbType.VarChar).Value = price.Text.ToString();
            test_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = reff.Text.ToString();
            test_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text.ToString();
            test_cmd.ExecuteNonQuery();
           // string message = "alert('Data Create Sucessfully.')";
           // ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
       
        con.Close();
    }
    public void datatable_po_item_WIDALTEST()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        SqlCommand stDel_cmd = new SqlCommand("delete from  LAB_WIDALRESULT_TABLE where  ID='" + droptesttype .Text+ "'", con);

        stDel_cmd.ExecuteNonQuery();

        foreach (GridViewRow row in Gvwidaltest.Rows)
        {
            var inves = row.FindControl("lblinvw") as Label;
            var A140 = row.FindControl("txt140") as TextBox;
            var A160 = row.FindControl("txt160") as TextBox;
            var A180 = row.FindControl("txt180") as TextBox;
            var A320 = row.FindControl("txt320") as TextBox;
            var INV = Convert.ToInt32(Gvwidaltest.DataKeys[row.RowIndex].Values[0]);
            SqlCommand stock_cmd = new SqlCommand("INSERT INTO LAB_WIDALRESULT_TABLE (ID,INV,INVES,A140,A160,A180,A320) VALUES (@ID,@INV,@INVES,@A140,@A160,@A180,@A320)", con);
            stock_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = droptesttype.Text;
            stock_cmd.Parameters.Add("@INV", SqlDbType.VarChar).Value = INV.ToString();
            stock_cmd.Parameters.Add("@INVES", SqlDbType.VarChar).Value = inves.Text.ToString();
            stock_cmd.Parameters.Add("@A140", SqlDbType.VarChar).Value = A140.Text.ToString();
            stock_cmd.Parameters.Add("@A160", SqlDbType.VarChar).Value = A160.Text.ToString();
            stock_cmd.Parameters.Add("@A180", SqlDbType.VarChar).Value = A180.Text.ToString();
            stock_cmd.Parameters.Add("@A320", SqlDbType.VarChar).Value = A320.Text.ToString();
            stock_cmd.ExecuteNonQuery();

        }
        //}
        //catch (Exception ex)
        //{

        //}
        con.Close();
    }
    public void datatable_Resul_itemUDATE()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();

        foreach (GridViewRow row in GridView3.Rows)
        {

            var slno = row.FindControl("lblSlno") as Label;
            var value = row.FindControl("txt_value") as TextBox;
            var reff = row.FindControl("txtref") as TextBox;
            var unit = row.FindControl("txtunit") as TextBox;


            //var INV = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Values[0]);
            SqlCommand test_cmd = new SqlCommand("UPDATE LABRESULT_TABLE SET REF=@REF,UNIT=@UNIT,VALUE=@VALUE WHERE slno=@slno ", con);
            test_cmd.Parameters.Add("@slno", SqlDbType.Int).Value = slno.Text.ToString();
            
            test_cmd.Parameters.Add("@VALUE", SqlDbType.VarChar).Value = value.Text.ToString();
            test_cmd.Parameters.Add("@REF", SqlDbType.VarChar).Value = reff.Text.ToString();
            test_cmd.Parameters.Add("@UNIT", SqlDbType.VarChar).Value = unit.Text.ToString();
            test_cmd.ExecuteNonQuery();
            // string message = "alert('Data Create Sucessfully.')";
            // ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }

        con.Close();
    }
    public void datatable_Amt_Updt()
    {
        //try
        //{
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
       // var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();

        SqlCommand Amt_cmd = new SqlCommand("UPDATE LABRES_TABLE SET PRICE=@PRICE,DISCAMT=@DISCAMT,PAIDAMT=@PAIDAMT,DUEAMT=@DUEAMT WHERE ID=@ID", con);

        Amt_cmd.Parameters.Add("@ID", SqlDbType.VarChar).Value = droptesttype.Text;
        Amt_cmd.Parameters.Add("@PRICE", SqlDbType.Decimal).Value = lbltotalprice.Text;
        Amt_cmd.Parameters.Add("@DISCAMT", SqlDbType.Decimal).Value = lbldiscamt.Text;
        Amt_cmd.Parameters.Add("@PAIDAMT", SqlDbType.Decimal).Value = lblpaidamt.Text;
        Amt_cmd.Parameters.Add("@DUEAMT", SqlDbType.Decimal).Value = lblbalanceamt.Text;

        Amt_cmd.ExecuteNonQuery();
         //  string message = "alert('Data Create Sucessfully.')";
          // ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
      

        con.Close();
    }

    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            //if (droprtype.SelectedIndex == 0)
            //{
            SqlCommand com = new SqlCommand("select * from LABRES_TABLE where ID='" + txtname.Text + "' or PNAME='" + txtname.Text + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                droptesttype.Text = dr["ID"].ToString();
                txt_LName.Text = dr["PNAME"].ToString();
                dr.Close();
                //-------------------------------------------------------------
                DataTable dt = (DataTable)ViewState["ITEM"];
                dt.Clear();
                SqlDataAdapter da = new SqlDataAdapter("select A.slno AS ID ,A.NAME AS NAME,A.INV AS INV,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT,D.VALUE AS VALUE,D.slno AS slno,D.INV AS INV,E.PRICE AS PRICEE,E.DISCAMT AS DISCAMT,E.PAIDAMT AS PAIDAMT,E.DUEAMT AS DUEAMT,E.ID AS ID  from TEST_COMPONENT_TABLE A,TEST_CATEGORY_TABLE B,TEST_NAME_TABLE C,LABRESULT_TABLE D,LABRES_TABLE E  WHERE A.ID=C.ID AND C.CATEGORY=B.ID AND D.INV=A.slno  AND E.ID=D.ID  AND E.ID='" + txtname.Text + "'  AND A.ORGID='" + lblorgid.Text + "'", con);

                da.Fill(dt);
                hdn_Slno.Value = dt.Rows[0]["ID"].ToString();
                ViewState["ITEM"] = dt;
                this.BindGrid();
                lbltotalprice.Text = dt.Rows[0]["PRICEE"].ToString();
                lbldiscamt.Text = dt.Rows[0]["DISCAMT"].ToString();
                lblpaidamt.Text = dt.Rows[0]["PAIDAMT"].ToString();
                lblbalanceamt.Text = dt.Rows[0]["DUEAMT"].ToString();

                GridView1.DataSource = dt;
                // GridView1.DataKeyNames = new string[] { "ID" };
                GridView1.DataBind();
                //--------------------------------------------------------------
                SqlDataAdapter da1 = new SqlDataAdapter("select * from LAB_WIDALRESULT_TABLE where ID='" + txtname.Text + "'", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                Gvwidaltest.DataSource = dt1;
                Gvwidaltest.DataKeyNames = new string[] { "INV" };
                Gvwidaltest.DataBind();
            }


            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }

    protected void txtpaidLastamt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            //lblbalanceamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(txtpaidamt.Text) - Convert.ToDouble(txtdiscamt.Text)).ToString();
            //lblbalanceamt.Text = (Convert.ToDouble(lblbalanceamt.Text) - Convert.ToDouble(txtpaidLastamt.Text)).ToString();
            lblbalanceamt.Text = (Convert.ToDouble(lbltotalprice.Text) - Convert.ToDouble(lblpaidamt.Text) - Convert.ToDouble(lbldiscamt.Text) - Convert.ToDouble(txtpaidLastamt.Text)).ToString();
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        Session["LABID"] = gr.Cells[0].Text;
         Response.Redirect("~/LABORATORY/OBill.aspx");
        con.Close();
    }
    protected void GridView2_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {
            var slno = GridView2.DataKeys[e.NewSelectedIndex].Values["ID"].ToString();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlCommand com = new SqlCommand("select ID,PNAME  AS NAME,PRICE,PID,TESTTYPE,PTYPE,TESTINDEX,REFBY,DISCAMT,PAIDAMT,DUEAMT from LABRES_TABLE  where ID='" + slno + "'", con);
            dr = com.ExecuteReader();
            if (dr.Read())
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                TXTID.Text = slno.ToString();
                txtname.Text = dr["NAME"].ToString();
                LBPAIDAMT.Text = dr["PRICE"].ToString();
                lblpaidamt.Text = dr["PAIDAMT"].ToString();
                lblbalanceamt.Text = dr["DUEAMT"].ToString();
                lbldiscamt.Text = dr["DISCAMT"].ToString();
                lbltotalprice.Text = dr["PRICE"].ToString();
                droptesttype.Text = dr["ID"].ToString();

            }
            dr.Close();

            SqlDataAdapter da = new SqlDataAdapter("SELECT A.slno AS slno, A.INV AS ID,A.PRICE AS PRICE,A.REF AS RANGE, A.UNIT AS UNIT,B.INV AS INV,A.VALUE AS VALUE FROM LABRESULT_TABLE A,TEST_COMPONENT_TABLE B WHERE A.INV=B.slno AND A.ID='" + slno.ToString() + "'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = null;
            GridView1.DataBind();
            GridView3.SelectedIndex = 0;
            GridView3.DataSource = null;
            GridView3.DataSource = dt;
            GridView3.DataBind();
            btndelete.Visible = true;
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("SELECT A.ID as ID,A.DATE AS DATE,A.PNAME AS PID FROM LABRES_TABLE A  WHERE  A.ORGID='" + lblorgid.Text + "' AND A.PTYPE!='INPATIENT' ORDER BY A.ID DESC", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView2.SelectedIndex = 0;
            GridView2.DataSource = dt;
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataKeyNames = new string[] { "ID" };
            GridView2.DataBind();
            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        datatable_Resul_itemUDATE();
        datatable_Amt_Updt();
        Session["LABID"] = droptesttype.Text;

        Response.Redirect("~/LABORATORY/OBill.aspx");

        string message = "alert('Data Create Sucessfully.')";
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        con.Close();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LABORATORY/Optestresult.aspx");
    }
    protected void btndelete_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("delete from LABRESULT_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet d = new DataSet();
            da.Fill(d);
            SqlDataAdapter da1 = new SqlDataAdapter("delete from LABRES_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet d1 = new DataSet();
            da1.Fill(d1);
            SqlDataAdapter da2 = new SqlDataAdapter("delete from LAB_WIDALRESULT_TABLE where ID='" + TXTID.Text + "'", con);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2);


            con.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        Response.Redirect("~/LABORATORY/Optestresult.aspx");
    }
}