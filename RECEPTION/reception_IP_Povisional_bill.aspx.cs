using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class RECEPTION_reception_IP_Povisional_bill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr, dr1, dr2, dr3, dr4, dr5;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
    RECEPTION_reception_IP_Povisional_bill cr;
    string fr, to;
    string GET;
    DataMathods OBJ_METHOD = new DataMathods();

    public class ReportFactory
    {
        protected static Queue reportQueue = new Queue();

        protected static ReportClass CreateReport(Type reportClass)
        {
            object report = Activator.CreateInstance(reportClass);
            reportQueue.Enqueue(report);
            return (ReportClass)report;
        }

        public static ReportClass GetReport(Type reportClass)
        {

            //75 is my print job limit.
            if (reportQueue.Count > 75) ((ReportClass)reportQueue.Dequeue()).Dispose();
            return CreateReport(reportClass);
        }
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
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    public void binddata()
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
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        
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
        //string S = Session["PAYMENT"].ToString();
        string c_id = Session["i_id"].ToString();


        SqlParameter[] SQL_PARAMS = new SqlParameter[2];

        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "provisional_ip");
        SQL_PARAMS[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, c_id);


        DataSet DS2 = OBJ_METHOD.Get_DataSet("SP_PROVISIONAL_BILLIP", false, true, SQL_PARAMS);
        if (DS2.Tables[0].Rows.Count > 0)
        {
            DataTable dt1 = new DataTable();
            DS2.Tables[0].TableName = "STOCK";
            dt1 = DS2.Tables["STOCK"];
            int l = 0, m = 0;
            ds2.PROBILL.Rows.Clear();
            while (l < dt1.Rows.Count)
            {
                dtr = dt1.Rows[l];
                ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
                m++;
                l += 1;
            }
            cr = new RECEPTION_reception_IP_Povisional_bill();
            rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
            rodc.SetDataSource(dt1);
            CrystalReportViewer1.ReportSource = rodc;
            CrystalReportViewer1.DataBind();
        }
        //using (SqlCommand cmd1 = new SqlCommand("SP_PROVISIONAL_BILLIP", con))
        //{
        //    cmd1.CommandType = CommandType.StoredProcedure;
        //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "provisional_ip";
        //    cmd1.Parameters.Add("@c_id", SqlDbType.VarChar).Value = c_id;
        //    da = new SqlDataAdapter(cmd1);
        //    ds = new DataSet();
        //    DataTable dt2 = new DataTable();
        //    da.Fill(ds, "STOCK");
        //    dt2 = ds.Tables["STOCK"];
        //    int l = 0, m = 0;
        //    ds2.PROBILL.Rows.Clear();
        //    while (l < dt2.Rows.Count)
        //    {
        //        dtr = dt2.Rows[l];
        //        ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
        //        m++;
        //        l += 1;
        //    }
        //    cr = new RECEPTION_provisional_billip();
        //    rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
        //    rodc.SetDataSource(dt2);
        //    CrystalReportViewer1.ReportSource = rodc;
        //    CrystalReportViewer1.DataBind();
        //}

        if (!IsPostBack)
        {
            if (Session["get"] == "1")
            {
                SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

                SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "provisional_ip");
                SQL_PARAMS1[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, c_id);


                DataSet DS1 = OBJ_METHOD.Get_DataSet("SP_PROVISIONAL_BILLIP", false, true, SQL_PARAMS1);
                if (DS1.Tables[0].Rows.Count > 0)
                {
                    DataTable dt2 = new DataTable();
                    DS1.Tables[0].TableName = "STOCK";
                    dt2 = DS1.Tables["STOCK"];
                    int l = 0, m = 0;
                    ds2.PROBILL.Rows.Clear();
                    while (l < dt2.Rows.Count)
                    {
                        dtr = dt2.Rows[l];
                        ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
                        m++;
                        l += 1;
                    }
                    cr = new RECEPTION_reception_IP_Povisional_bill();
                    rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
                    rodc.SetDataSource(dt2);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                }
                btnPrint.Visible = true;
            }
            
        }
      
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new RECEPTION_reception_IP_Povisional_bill();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
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
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            cr.rodc.Close();
            cr.rodc.Clone();
            cr.rodc.Dispose();
            rodc = null;
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
        }
        catch (Exception ex)
        {
            string message = "alert('" + ex.Message + "')";
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
           
        }
    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        CloseReport();

        cr = new RECEPTION_reception_IP_Povisional_bill();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();

    }
}