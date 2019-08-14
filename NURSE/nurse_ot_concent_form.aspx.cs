using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

public partial class NURSE_nurse_ot_concent_form : System.Web.UI.Page
{
    string num1 = "SJ000";
    SqlConnection con;
    SqlDataAdapter da, da1, da2;
    DataSet ds = new DataSet();
    SqlCommand com, cmd, cmd1, cmd2, cmd3;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    DataSet1 ds2 = new DataSet1();
    DataTable dt1 = new DataTable();
    ReportDocument rodc = new ReportDocument();
    NURSE_nurse_ot_concent_form cr;
    string fr, to;
    DataMathods OBJ_METHOD = new DataMathods();
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
        try
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
            lbluid.Text = Session["UID"].ToString();
            lblorgid.Text = Session["ORGID"].ToString();

            if (!IsPostBack)
            {
                binddata();
                Session["w"] = "False";
            }
            if (!IsPostBack && Session["w"] == "True")
            {
                if (dropbedno.SelectedIndex == 0)
                {
                    string message = "Please Select BED Number.')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                    return;
                }
                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from ORG_TABLE", false, false);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    Ds.Tables[0].TableName = "123";
                    dt1 = Ds.Tables["123"];
                    int i = 0;
                    ds2.ORG.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.ORG.Rows.Add(new string[] { dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["ADDRESS"].ToString(), dtr["REGNO"].ToString(), dtr["EMAIL"].ToString(), dtr["PHONE2"].ToString() });
                        i += 1;
                    }

                    cr = new NURSE_nurse_ot_concent_form();
                    rodc.Load(Server.MapPath("~/REPORTS/otconcentreport.rpt"));
                    TextObject bedno = (TextObject)rodc.ReportDefinition.Sections["Section3"].ReportObjects["txtbedno"];
                    bedno.Text = lblbedno.Text;
                    TextObject name = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtname"];
                    name.Text = lblname.Text;
                    TextObject ipno = (TextObject)rodc.ReportDefinition.Sections["Section2"].ReportObjects["txtipno"];
                    ipno.Text = lblipno.Text;
                    TextObject ward = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtward"];
                    ward.Text = lblward.Text;
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();
                    //da1 = new SqlDataAdapter("select * from ORG_TABLE", con);
                    //ds = new DataSet();
                    //da1.Fill(ds, "123");
                    //dt1 = ds.Tables["123"];
                    //int i = 0;
                    //ds2.ORG.Rows.Clear();
                    //while (i < dt1.Rows.Count)
                    //{
                    //    dtr = dt1.Rows[i];
                    //    ds2.ORG.Rows.Add(new string[] { dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["ADDRESS"].ToString(), dtr["REGNO"].ToString(), dtr["EMAIL"].ToString(), dtr["PHONE2"].ToString() });
                    //    i += 1;
                    //}

                    //cr = new NURSE_nurse_ot_concent_form();
                    //rodc.Load(Server.MapPath("~/REPORTS/otconcentreport.rpt"));
                    //TextObject bedno = (TextObject)rodc.ReportDefinition.Sections["Section3"].ReportObjects["txtbedno"];
                    //bedno.Text = lblbedno.Text;
                    //TextObject name = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtname"];
                    //name.Text = lblname.Text;
                    //TextObject ipno = (TextObject)rodc.ReportDefinition.Sections["Section2"].ReportObjects["txtipno"];
                    //ipno.Text = lblipno.Text;
                    //TextObject ward = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtward"];
                    //ward.Text = lblward.Text;
                    //rodc.SetDataSource(dt1);
                    //CrystalReportViewer1.ReportSource = rodc;
                    //CrystalReportViewer1.DataBind();

                    
                    btnPrint.Visible = true;
                }

            }
            TXTDATE.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm");
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
        cr = new NURSE_nurse_ot_concent_form();
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
        try
        {
            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            DataSet DS = OBJ_METHOD.Get_DataSet("FINANCIAL_YEAR", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                lblfyear.Text = DS.Tables[0].Rows[0]["FYEAR"].ToString();
            }

            DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT DISTINCT BEDNO FROM BED_TABLE WHERE Branch_ID = " + Session["Branch"] + "", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                //SqlDataAdapter da1 = new SqlDataAdapter("SELECT DISTINCT BEDNO FROM BED_TABLE", con);
                //DataTable dt1 = new DataTable();
                //da1.Fill(dt1);
                ////dropbedno.SelectedIndex = 0;
                dropbedno.DataSource = Ds;
                dropbedno.DataTextField = "BEDNO";
                dropbedno.DataBind();
                dropbedno.Items.Insert(0,new ListItem("Please Select","0"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);

        }
    }
    protected void dropbedno_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet Ds1 = OBJ_METHOD.Get_DataSet("SELECT VN,PNAME,BEDNO,WARD FROM BED_TABLE A WHERE  BEDNO='" + dropbedno.Text + "'", false, false);

            if (Ds1.Tables[0].Rows.Count > 0)
            {
                lblipno.Text = Ds1.Tables[0].Rows[0]["VN"].ToString();
                lblname.Text = Ds1.Tables[0].Rows[0]["PNAME"].ToString();
                lblbedno.Text = Ds1.Tables[0].Rows[0]["BEDNO"].ToString();
                lblward.Text = Ds1.Tables[0].Rows[0]["WARD"].ToString();
            }
            //SqlCommand COM = new SqlCommand("SELECT VN,PNAME,BEDNO,WARD FROM BED_TABLE A WHERE  BEDNO='" + dropbedno.Text + "'", con);
            //dr = COM.ExecuteReader();
            //if (dr.Read())
            //{
            //    lblipno.Text = dr["VN"].ToString();
            //    lblname.Text = dr["PNAME"].ToString();
            //    lblbedno.Text = dr["BEDNO"].ToString();
            //    lblward.Text = dr["WARD"].ToString();
            //}
            //dr.Close();
            if (Session["w"] == "True")
            {
                DataSet Ds = OBJ_METHOD.Get_DataSet("select * from ORG_TABLE", false, false);

                if (Ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    Ds.Tables[0].TableName = "123";
                    //da1 = new SqlDataAdapter("select * from ORG_TABLE ", con);
                    //ds = new DataSet();
                    //da1.Fill(ds, "123");
                    dt1 = Ds.Tables["123"];
                    int i = 0;
                    ds2.ORG.Rows.Clear();
                    while (i < dt1.Rows.Count)
                    {
                        dtr = dt1.Rows[i];
                        ds2.ORG.Rows.Add(new string[] { dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["ADDRESS"].ToString(), dtr["REGNO"].ToString(), dtr["EMAIL"].ToString(), dtr["PHONE2"].ToString() });
                        i += 1;
                    }

                    cr = new NURSE_nurse_ot_concent_form();
                    rodc.Load(Server.MapPath("~/REPORTS/otconcentreport.rpt"));
                    TextObject bedno = (TextObject)rodc.ReportDefinition.Sections["Section3"].ReportObjects["txtbedno"];
                    bedno.Text = lblbedno.Text;
                    TextObject name = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtname"];
                    name.Text = lblname.Text;
                    TextObject ipno = (TextObject)rodc.ReportDefinition.Sections["Section2"].ReportObjects["txtipno"];
                    ipno.Text = lblipno.Text;
                    TextObject ward = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtward"];
                    ward.Text = lblward.Text;
                    rodc.SetDataSource(dt1);
                    CrystalReportViewer1.ReportSource = rodc;
                    CrystalReportViewer1.DataBind();


                    btnPrint.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        try
        {
            if (dropbedno.SelectedIndex == 0)
            {
                string message = "alert('Please Select BED. Number')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                return;
            }
            Session["w"] = "True";
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from ORG_TABLE ", false, false);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt1 = new DataTable();
                Ds.Tables[0].TableName = "123";
                //da1 = new SqlDataAdapter("select * from ORG_TABLE ", con);
                //ds = new DataSet();
                //da1.Fill(ds, "123");
                dt1 = Ds.Tables["123"];
                int i = 0;
                ds2.ORG.Rows.Clear();
                while (i < dt1.Rows.Count)
                {
                    dtr = dt1.Rows[i];
                    ds2.ORG.Rows.Add(new string[] { dtr["NAME"].ToString(), dtr["PHONE"].ToString(), dtr["ADDRESS"].ToString(), dtr["REGNO"].ToString(), dtr["EMAIL"].ToString(), dtr["PHONE2"].ToString() });
                    i += 1;
                }

                cr = new NURSE_nurse_ot_concent_form();
                rodc.Load(Server.MapPath("~/REPORTS/otconcentreport.rpt"));
                TextObject bedno = (TextObject)rodc.ReportDefinition.Sections["Section3"].ReportObjects["txtbedno"];
                bedno.Text = lblbedno.Text;
                TextObject name = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtname"];
                name.Text = lblname.Text;
                TextObject ipno = (TextObject)rodc.ReportDefinition.Sections["Section2"].ReportObjects["txtipno"];
                ipno.Text = lblipno.Text;
                TextObject ward = (TextObject)rodc.ReportDefinition.Sections["Section1"].ReportObjects["txtward"];
                ward.Text = lblward.Text;
                rodc.SetDataSource(dt1);
                CrystalReportViewer1.ReportSource = rodc;
                CrystalReportViewer1.DataBind();

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
}