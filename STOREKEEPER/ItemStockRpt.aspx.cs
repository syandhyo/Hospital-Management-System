using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using System.Drawing.Printing;


public partial class STOREKEEPER_ItemStockRpt : System.Web.UI.Page
{
    SqlDataReader dr;
    
    //----------------------------
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    STOREKEEPER_ItemStockRpt cr;
    string fr, to;

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
        lblorgid.Text = Session["ORGID"].ToString();
        if(!IsPostBack)
        {
            binddata();
        }
        if (IsPostBack)
        {
            try
            {
                // fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
                // to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
               // da1 = new SqlDataAdapter("select MATERIAL_NAME,UNIT,QTY from MATERIAL_STOCK_TABLE WHERE MATERIAL_NAME='" + dropItem.SelectedItem.Text + "' ORDER BY [MATERIAL_NAME] ASC ", con);
                // where A.DATE BETWEEN '2017-01-01' AND '2018-01-31' AND A.ITEMNAME ='CHAWANPRAS' AND A.ITEMNAME=B.NAME  order by CONVERT(datetime, [DATE] ) ASC 
                using (SqlCommand cmd1 = new SqlCommand("ITEM_STOCK_RPT", con))
                {
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ITEM";
                    cmd1.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = dropItem.SelectedItem.Text;
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    ds = new DataSet();
                    da1.Fill(ds, "123");
                    dt1 = ds.Tables["123"];
                    int i = 0;
                    ds2.STORE_STOCK.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        // ds2.STORE_STOCK.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["NAME"].ToString(), dtr["OPENING"].ToString(), dtr["PURCHES"].ToString(), dtr["RETN"].ToString(), dtr["ISSUE"].ToString(), dtr["REF"].ToString(), dtr["CLOSING"].ToString() });
                        ds2.STORE_STOCK.Rows.Add(new string[] { dtr["MATERIAL_NAME"].ToString(), dtr["UNIT"].ToString(), dtr["QTY"].ToString() });
                        i += 1;
                    }
                    cr = new STOREKEEPER_ItemStockRpt();
                    rodc.Load(Server.MapPath("~/MATREPORT/Itemstock.rpt"));
                    //  TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                    //fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                    //TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                    //todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }
        }
        
        con.Close();
        

    }
    protected void CloseReport()
    {
        try
        {
            if (rodc != null)
            {
                Sections objSections = rodc.ReportDefinition.Sections;
                foreach (Section objSection in objSections)
                {
                    ReportObjects objReports = objSection.ReportObjects;
                    foreach (ReportObject rptObj in objReports)
                    {
                        if (rptObj.Kind.Equals(CrystalDecisions.Shared.ReportObjectKind.SubreportObject))
                        {
                            SubreportObject subreportObject = (SubreportObject)rptObj;
                            ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                            subReportDocument.Close();
                        }
                    }
                }
                rodc.Close();
                rodc.Dispose();
                cr.rodc.Close();
                cr.rodc.Clone();
                cr.rodc.Dispose();
                rodc = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            if (CrystalReportViewer1 != null)
            {
                CrystalReportViewer1.ReportSource = null;
                CrystalReportViewer1.Dispose();
                cr.rodc.Close();
                cr.rodc.Clone();
                cr.rodc.Dispose();
                rodc = null;
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new STOREKEEPER_ItemStockRpt();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
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

        da = new SqlDataAdapter("select ID,MATERIAL_NAME from MATERIAL_STOCK_TABLE  ", con);
        DataTable ds = new DataTable();
        da.Fill(ds);
        dropItem.DataSource = ds;
        dropItem.DataTextField = "MATERIAL_NAME";
        dropItem.DataValueField = "ID";
        dropItem.DataBind();
        dropItem.Items.Insert(0, "Please Select");
        con.Close();
    }
    protected void btncreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropItem.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The DropDown First..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            // fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            // to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
           // da1 = new SqlDataAdapter("select MATERIAL_NAME,UNIT,QTY from MATERIAL_STOCK_TABLE WHERE MATERIAL_NAME='" + dropItem.SelectedItem.Text + "' ORDER BY [MATERIAL_NAME] ASC ", con);
            using (SqlCommand cmd1 = new SqlCommand("ITEM_STOCK_RPT", con))
            {
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_ITEM";
                cmd1.Parameters.Add("@MATERIAL_NAME", SqlDbType.VarChar).Value = dropItem.SelectedItem.Text;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.STORE_STOCK.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.STORE_STOCK.Rows.Add(new string[] { dtr["MATERIAL_NAME"].ToString(), dtr["UNIT"].ToString(), dtr["QTY"].ToString() });
                    i += 1;
                }
                cr = new STOREKEEPER_ItemStockRpt();
                rodc.Load(Server.MapPath("~/MATREPORT/Itemstock.rpt"));
                //TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                // fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                //TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                //todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();

                con.Close();
                btnPrint.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
                return printer;
        }
        return string.Empty;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {

            rodc.PrintOptions.PrinterName = GetDefaultPrinter();
            rodc.PrintToPrinter(1, false, 0, 0);
            //rodc1.PrintOptions.PrinterName = GetDefaultPrinter();
            // rodc1.PrintToPrinter(1, false, 0, 0);
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //return;
            // Response.Write("<script LANGUAGE='JavaScript' >alert('connect printer settings')</script>");
        }
    }
    protected void btnShowAll_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            // fr = Convert.ToDateTime(TextBox1.Text).ToString("yyyy-MM-dd");
            // to = Convert.ToDateTime(TextBox2.Text).ToString("yyyy-MM-dd");
            da1 = new SqlDataAdapter("select MATERIAL_NAME,UNIT,QTY from MATERIAL_STOCK_TABLE  ORDER BY [MATERIAL_NAME] ASC ", con);
            ds = new DataSet();
            da1.Fill(ds, "123");
            dt1 = ds.Tables["123"];
            int i = 0;
            ds2.STORE_STOCK.Rows.Clear();
            while (i < dt1.Rows.Count)
            {
                dtr = dt1.Rows[i];
                ds2.STORE_STOCK.Rows.Add(new string[] { dtr["MATERIAL_NAME"].ToString(), dtr["UNIT"].ToString(), dtr["QTY"].ToString() });
                i += 1;
            }
            cr = new STOREKEEPER_ItemStockRpt();
            rodc.Load(Server.MapPath("~/MATREPORT/Itemstock.rpt"));
            //TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
            // fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
            //TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
            //todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();

            con.Close();
            btnPrint.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
}