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

public partial class ACCOUNTS_account_party_payment_report : System.Web.UI.Page
{
    DataMathods OBJ_METHOD = new DataMathods();
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    DataSet2 ds2 = new DataSet2();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ACCOUNTS_account_party_payment_report cr;
    SqlDataReader dr;
    string fr, to;
    SqlConnection con;

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
    protected void Page_Load(object sender, EventArgs e)
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
        lblorgid.Text = Session["ORGID"].ToString();

        if (!IsPostBack)
        {
            databind();
        }

        if (IsPostBack)
        {

            #region oldcode
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            ////fr = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
            ////to = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");

            //using (SqlCommand cm = new SqlCommand("SP_PartyPaymnt_RPT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cm.Parameters.Add("@NAME1", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
            //    SqlDataAdapter da1 = new SqlDataAdapter(cm);
            //    // da1 = new SqlDataAdapter(" SELECT CONVERT(VARCHAR(10),A.[DATE],103) as DATE,B.NAME1,A.VOUCHER_NO,A.MODE_OF_PAYMENT,A.PNO,A.PRICE FROM PARTY_PAYMENT_TBL A, VENDER_MASTER_TABLE B WHERE B.NAME1='" + dropvendor.SelectedValue + "' ORDER BY A.[DATE] DESC", con);


            //    ds = new DataSet();
            //    da1.Fill(ds, "123");
            //    dt1 = ds.Tables["123"];
            //    int i = 0;
            //    ds2.PARTY_PAYMENT.Rows.Clear();
            //    while (i < dt1.Rows.Count)
            //    {
            //        dtr = dt1.Rows[i];
            //        ds2.PARTY_PAYMENT.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["NAME1"].ToString(), dtr["VOUCHER_NO"].ToString(), dtr["MODE_OF_PAYMENT"].ToString(), dtr["PNO"].ToString(), dtr["PRICE"].ToString() });
            //        i += 1;
            //    }
            //    cr = new ACCOUNTS_account_party_payment_report();
            //    rodc.Load(Server.MapPath("~/MATREPORT/partypayment.rpt"));
            //    //TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
            //    //fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
            //    //TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
            //    //todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
            //    rodc.SetDataSource(dt1);
            //    CrystalReportViewer1.ReportSource = rodc;
            //    CrystalReportViewer1.DataBind();
            //}
            //con.Close();
            #endregion

            #region newcode
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME1", SqlDbType.VarChar, 500, dropvendor.SelectedValue);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_PartyPaymnt_RPT", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                DS.Tables[0].TableName = "123";
                dt1 = DS.Tables["123"];
                int i = 0;
                ds2.PARTY_PAYMENT.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PARTY_PAYMENT.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["NAME1"].ToString(), dtr["VOUCHER_NO"].ToString(), dtr["MODE_OF_PAYMENT"].ToString(), dtr["PNO"].ToString(), dtr["PRICE"].ToString() });
                    i += 1;
                }

                cr = new ACCOUNTS_account_party_payment_report();
                rodc.Load(Server.MapPath("~/MATREPORT/partypayment.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
            #endregion

        }
       
    }

    public void databind()
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


            DataSet Dt = OBJ_METHOD.Get_DataSet("select distinct NAME1 AS NAME FROM VENDER_MASTER_TABLE ", false, false);
            if (Dt.Tables[0].Rows.Count > 0)
            {
                dropvendor.DataSource = Dt;
                dropvendor.DataTextField = "NAME";
                dropvendor.DataValueField = "NAME";
                dropvendor.DataBind();
                dropvendor.Items.Insert(0, new ListItem("Please Select", "0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }

        
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new ACCOUNTS_account_party_payment_report();
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
            #region old code
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
            //con.Open();
            ////fr = Convert.ToDateTime(txtfdate.Text).ToString("yyyy-MM-dd");
            ////to = Convert.ToDateTime(txttdate.Text).ToString("yyyy-MM-dd");

            //if (dropvendor.SelectedIndex == 0)
            //{
            //    string message = "alert('* Please Select Vendor.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    dropvendor.Focus();
            //    return;
            //}
            //using (SqlCommand cm = new SqlCommand("SP_PartyPaymnt_RPT", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "DISPLAY";
            //    cm.Parameters.Add("@NAME1", SqlDbType.VarChar).Value = dropvendor.SelectedValue;
            //    SqlDataAdapter da1 = new SqlDataAdapter(cm);
            //    //da1 = new SqlDataAdapter("SELECT CONVERT(VARCHAR(10),A.[DATE],103) as DATE,B.NAME1,A.VOUCHER_NO,A.MODE_OF_PAYMENT,A.PNO,A.PRICE FROM PARTY_PAYMENT_TBL A, VENDER_MASTER_TABLE B WHERE B.NAME1='" + dropvendor.SelectedValue + "' ORDER BY A.[DATE] DESC", con);

            //    ds = new DataSet();
            //    da1.Fill(ds, "123");
            //    dt1 = ds.Tables["123"];
            //    int i = 0;
            //    ds2.PARTY_PAYMENT.Rows.Clear();
            //    while (i < dt1.Rows.Count)
            //    {
            //        dtr = dt1.Rows[i];
            //        ds2.PARTY_PAYMENT.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["NAME1"].ToString(), dtr["VOUCHER_NO"].ToString(), dtr["MODE_OF_PAYMENT"].ToString(), dtr["PNO"].ToString(), dtr["PRICE"].ToString() });
            //        i += 1;
            //    }
            //    cr = new ACCOUNTS_account_party_payment_report();
            //    rodc.Load(Server.MapPath("~/MATREPORT/partypayment.rpt"));
            //    //TextObject fromdate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtfrom"];
            //    //fromdate.Text = Convert.ToDateTime(fr).ToString("dd-MM-yyyy");
            //    //TextObject todate = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtto"];
            //    //todate.Text = Convert.ToDateTime(to).ToString("dd-MM-yyyy");
            //    rodc.SetDataSource(dt1);
            //    CrystalReportViewer1.ReportSource = rodc;
            //    CrystalReportViewer1.DataBind();
            //}
            //con.Close();
            //btnPrint.Visible = true;
            #endregion

            #region new code
            //---------------------------new code---------------------------------------------------------------
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DISPLAY");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@NAME1", SqlDbType.VarChar, 500, dropvendor.SelectedValue);

            DataSet DS = OBJ_METHOD.Get_DataSet("SP_PartyPaymnt_RPT", false, true, SQL_PARAMS);

            if (DS.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                DS.Tables[0].TableName = "123";
                dt1 = DS.Tables["123"];
                int i = 0;
                ds2.PARTY_PAYMENT.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PARTY_PAYMENT.Rows.Add(new string[] { dtr["DATE"].ToString(), dtr["NAME1"].ToString(), dtr["VOUCHER_NO"].ToString(), dtr["MODE_OF_PAYMENT"].ToString(), dtr["PNO"].ToString(), dtr["PRICE"].ToString() });
                    i += 1;
                }

                cr = new ACCOUNTS_account_party_payment_report();
                rodc.Load(Server.MapPath("~/MATREPORT/partypayment.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
            btnPrint.Visible = true;
            #endregion

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
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