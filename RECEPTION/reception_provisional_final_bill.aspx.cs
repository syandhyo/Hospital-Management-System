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

public partial class RECEPTION_reception_provisional_final_bill : System.Web.UI.Page
{
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds, ds1;
    DataRow dtr;
    SqlDataReader dr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    ReportDocument rodc1 = new ReportDocument();
    RECEPTION_reception_provisional_final_bill cr;
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
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
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
        //string c_id = Session["i_id"].ToString();
        //da = new SqlDataAdapter("select A.BEDNO,A.VOUCHERNO,A.DATETIME,A.DESCRIPTION,A.CHARGES,A.VN ,A.CATEGORY,ISNULL( B.CREDIT,0)AS CREDIT,-+B.DEBIT AS DEBIT,C.ADDRESS,C.EMAIL,C.GSTNO,C.NAME,C.PHONE+','+C.PHONE2 AS PHONE,C.REGNO,D.NAME AS PNAME,D.AGE,D.PADDRESS,D.GENDER,D.UHID from PA_TRANS A,PA_MASTER B,ORG_TABLE C,ADMISSION_TABLE D WHERE A.VN=B.VN AND A.VN=D.VN AND A.VN='" + c_id + "'", con);
        //ds = new DataSet();
        //DataTable dt2 = new DataTable();
        //da.Fill(ds, "STOCK");
        //dt2 = ds.Tables["STOCK"];
        //int l = 0, m = 0;
        //ds2.PROBILL.Rows.Clear();
        //while (l < dt2.Rows.Count)
        //{
        //    dtr = dt2.Rows[l];
        //    ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
        //    m++;
        //    l += 1;
        //}
        //cr = new RECEPTION_reception_provisional_final_bill();
        //rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
        //rodc.SetDataSource(dt2);
        //CrystalReportViewer1.ReportSource = rodc;
        //CrystalReportViewer1.DataBind();

        if (IsPostBack)
        {
            if (Session["get"] == "1")
            {

                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "provisional_ip");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, txtspid.Text);


                DataSet DS = OBJ_METHOD.Get_DataSet("SP_PROVISIONAL_BILLIP", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    DS.Tables[0].TableName = "STOCK";
                    dt1 = DS.Tables["STOCK"];
                    int i = 0, k = 0;
                    ds2.PROBILL.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        //ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
                        k++;
                        i += 1;
                    }
                    cr = new RECEPTION_reception_provisional_final_bill();
                    rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                }


                //using (SqlCommand cmd1 = new SqlCommand("SP_PROVISIONAL_BILLIP", con))
                //{
                    //cmd1.CommandType = CommandType.StoredProcedure;
                    //cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "provisional_ip";
                    //cmd1.Parameters.Add("@c_id", SqlDbType.VarChar).Value = txtspid.Text;
                    //da = new SqlDataAdapter(cmd1);
                    //ds = new DataSet();
                    
                    //da.Fill(ds, "STOCK");
                    
                    

                //}
            }
            else
            {
                //using (SqlCommand cmd1 = new SqlCommand("SP_PROVISIONAL_BILLIP", con))
                //{
                SqlParameter[] SQL_PARAMS = new SqlParameter[2];

                SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "provisional_uhid");
                SQL_PARAMS[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, TXTUHID.Text);


                DataSet DS = OBJ_METHOD.Get_DataSet("SP_PROVISIONAL_BILLIP", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    DS.Tables[0].TableName = "STOCK";
                    dt1 = DS.Tables["STOCK"];
                    int i = 0, k = 0;
                    ds2.PROBILL.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
                        k++;
                        i += 1;
                    }
                    cr = new RECEPTION_reception_provisional_final_bill();
                    rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();

                    btnPrint.Visible = true;
                }
                    
            }

        }
        //con.Close();
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {

        CloseReport();
        //if (rodc != null)
        //{
        cr = new RECEPTION_reception_provisional_final_bill();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        //}
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        Session["get"] = "1";
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        //con.Open();
        try
        {

            //using (SqlCommand cmd1 = new SqlCommand("SP_PROVISIONAL_BILLIP", con))
            //{
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "provisional_only");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, txtspid.Text);


                DataSet DS = OBJ_METHOD.Get_DataSet("SP_PROVISIONAL_BILLIP", false, true, SQL_PARAMS);
                if (DS.Tables[0].Rows.Count > 0)
                {

                    ds = new DataSet();
                    DataTable dt1 = new DataTable();
                    DS.Tables[0].TableName = "STOCK";
                    dt1 = DS.Tables["STOCK"];

                    int i = 0, k = 0;
                    ds2.PROBILL.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
                        k++;
                        i += 1;
                    }
                    cr = new RECEPTION_reception_provisional_final_bill();
                    rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                }
                else
                {
                    string message = "alert(' No Record Found')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        //con.Close();
        btnPrint.Visible = true;

    }
    protected void btnUHID_Click(object sender, EventArgs e)
    {
        Session["get"] = "2";
        try
        {

            //using (SqlCommand cmd1 = new SqlCommand("SP_PROVISIONAL_BILLIP", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "provisional_uhid";
            //    cmd1.Parameters.Add("@c_id", SqlDbType.VarChar).Value = TXTUHID.Text;
            //    da = new SqlDataAdapter(cmd1);
               
            //    ds = new DataSet();
            //    DataTable dt1 = new DataTable();
            //    da.Fill(ds, "STOCK");
            //    dt1 = ds.Tables["STOCK"];
            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "provisional_uhid");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@c_id", SqlDbType.VarChar, 500, TXTUHID.Text);


            DataSet DS = OBJ_METHOD.Get_DataSet("SP_PROVISIONAL_BILLIP", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {

                ds = new DataSet();
                DataTable dt1 = new DataTable();
                DS.Tables[0].TableName = "STOCK";
                dt1 = DS.Tables["STOCK"];

                
                int i = 0, k = 0;
                ds2.PROBILL.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.PROBILL.Rows.Add(new string[] { dtr["BEDNO"].ToString(), dtr["VOUCHERNO"].ToString(), dtr["DATETIME"].ToString(), dtr["DESCRIPTION"].ToString(), dtr["CHARGES"].ToString(), dtr["VN"].ToString(), dtr["CATEGORY"].ToString(), dtr["CREDIT"].ToString(), dtr["DEBIT"].ToString(), dtr["ADDRESS"].ToString(), dtr["EMAIL"].ToString(), dtr["GSTNO"].ToString(), dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["REGNO"].ToString(), dtr["PNAME"].ToString(), dtr["AGE"].ToString(), dtr["PADDRESS"].ToString(), dtr["GENDER"].ToString(), dtr["UHID"].ToString() });
                    k++;
                    i += 1;
                }
                cr = new RECEPTION_reception_provisional_final_bill();
                rodc.Load(Server.MapPath("~/REPORTS/probill.rpt"));
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();
            }
            else
            {
                string message = "alert(' No Record Found')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
        
        btnPrint.Visible = true;

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
            //return;
            // Response.Write("<script LANGUAGE='JavaScript' >alert('connect printer settings')</script>");
        }
    }
    protected void CrystalReportViewer1_Unload(object sender, EventArgs e)
    {
        CloseReport();

        cr = new RECEPTION_reception_provisional_final_bill();
        cr.rodc.Close();
        cr.rodc.Clone();
        cr.rodc.Dispose();
        rodc = null;
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();

    }
}