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

public partial class STOREKEEPER_store_party_ledger_report : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    STOREKEEPER_store_party_ledger_report cr;
    string fr, to;
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

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
        lblorgid.Text = Session["ORGID"].ToString();

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

        if (!IsPostBack)
        {
            da = new SqlDataAdapter("select distinct NAME1 AS NAME FROM VENDER_MASTER_TABLE ", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dropvendor.DataSource = ds;
            dropvendor.DataTextField = "NAME";
            dropvendor.DataValueField = "NAME";
            dropvendor.DataBind();
            dropvendor.Items.Insert(0, "Please Select ");
        }

        
        con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new STOREKEEPER_store_party_ledger_report();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropvendor.SelectedIndex == 0)
            {
                string message = "alert('Please!! Select The Vendor..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txtfdate.Text == "")
            {
                string message = "alert('Please!! Select Both Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            else if (txttdate.Text == "")
            {
                string message = "alert('Please!! Select Both Dates..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            con.Open();
            fr = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
            to = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");
            //da1 = new SqlDataAdapter(" SELECT CONVERT(VARCHAR(10),A.[DATE],103) as DATE,B.NAME1,A.OPENING,A.CREDIT,A.DEBIT,A.CLOSING FROM VENDOR_TRANS_TABLE A, VENDER_MASTER_TABLE B WHERE A.DATE BETWEEN ' " + fr + "' AND '" + to + "' AND B.NAME1='" + dropvendor.SelectedItem.Text + "' ORDER BY A.[DATE] ASC ", con);

            using (SqlCommand COM = new SqlCommand("SP_Stock_PartyLedgerRPT", con))
            {
                COM.CommandType = CommandType.StoredProcedure;
                COM.Parameters.Add("@DBOperation", SqlDbType.VarChar).Value = "SELECT_DATE";
                COM.Parameters.Add("@fr", SqlDbType.Date).Value = fr;
                COM.Parameters.Add("@tr", SqlDbType.Date).Value = to;
                COM.Parameters.Add("@NAME", SqlDbType.VarChar).Value = dropvendor.SelectedItem.Text;
                SqlDataAdapter da1 = new SqlDataAdapter(COM);
                ds = new DataSet();
                da1.Fill(ds, "123");
                dt1 = ds.Tables["123"];
                int i = 0;
                ds2.STORE_STOCK_TRAN.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.STORE_STOCK_TRAN.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["NAME1"].ToString(), dtr["OPENING"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["CLOSING"].ToString() });
                    i += 1;
                }
                cr = new STOREKEEPER_store_party_ledger_report();
                rodc.Load(Server.MapPath("~/MATREPORT/partyledger.rpt"));
                TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
                fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
                TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
                todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
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
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
}