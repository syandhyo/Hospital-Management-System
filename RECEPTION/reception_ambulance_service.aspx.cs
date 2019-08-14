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

public partial class RECEPTION_reception_ambulance_service : System.Web.UI.Page
{
    SqlDataReader dr;
    // SqlConnection con;
    DataTable Dt;
    DataMathods OBJ_METHOD = new DataMathods();

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
            lblorgid.Text = Session["ORGID"].ToString();
            lbluid.Text = Session["UID"].ToString();

            if (!IsPostBack)
            {
                binddata();

            }

            //DateTime d = Convert.ToDateTime(DateTime.Now.ToString("dd-MM-yyyy HH:mm"));
            //TimeZone zone = TimeZone.CurrentTimeZone;
            //TimeSpan local = zone.GetUtcOffset(d);


            //txtAdate.Text = local.ToString();
            txtAdate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // reference the Delete LinkButton
                LinkButton db = (LinkButton)e.Row.Cells[9].Controls[0];

                db.OnClientClick = "return confirm('Are you sure want to delete this patient info ?');";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        GridView gv = new GridView();
        Control objc = (Control)sender;
        GridViewRow gvr = (GridViewRow)objc.Parent.Parent;
        int slno = Convert.ToInt32(GridView1.DataKeys[gvr.RowIndex].Values[0]);
        //GridViewRow gr = ((sender as LinkButton).NamingContainer as GridViewRow);
        //Session["id"] = gr.Cells[0].Text.Trim();
        Session["id"] = slno;
        Response.Redirect("reception_ambulance_bill.aspx");


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


            SqlParameter[] SQL_PARAMS1 = new SqlParameter[1];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_AMBULANCE");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_AMBULANCE", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.DataBind();
            }

            SqlParameter[] SQL_PARAMS2 = new SqlParameter[1];

            SQL_PARAMS2[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_AMBULANCE");

            DataSet DS2 = OBJ_METHOD.Get_DataSet("RECP_AMBULANCE", false, true, SQL_PARAMS2);
            if (DS2.Tables[0].Rows.Count > 0)
            {
                dropambulanceno.DataSource = DS2;
                dropambulanceno.DataTextField = "AmbulanceNo";
                dropambulanceno.DataValueField = "AmbulanceNo";
                dropambulanceno.DataBind();
                dropambulanceno.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            SqlParameter[] SQL_PARAMS3 = new SqlParameter[1];

            SQL_PARAMS3[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DRIVER");

            DataSet DS3 = OBJ_METHOD.Get_DataSet("RECP_AMBULANCE", false, true, SQL_PARAMS3);
            if (DS3.Tables[0].Rows.Count > 0)
            {
                dropdriver.DataSource = DS3;
                dropdriver.DataTextField = "Sname";
                dropdriver.DataValueField = "id";
                dropdriver.DataBind();
                dropdriver.Items.Insert(0, new ListItem("Please Select", "0"));
            }

            #region OLDCODE
            //using (SqlCommand cmd = new SqlCommand("RECP_AMBULANCE", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SHOW_AMBULANCE";
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
            //    cmd.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
            //    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
            //    cmd.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
            //    cmd.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
            //    SqlDataAdapter Adp = new SqlDataAdapter(cmd);
            //    //SqlDataAdapter Adp = new SqlDataAdapter("select id,ADate,PatientName,AttendentName,ContactNo,[From],[To],ApproxKm,Fee from tblAmbulance order by id desc", con);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(Dt);
            //    GridView1.DataSource = Dt;
            //    GridView1.DataBind();
            //}

            //using (SqlCommand cm = new SqlCommand("RECP_AMBULANCE", con))
            //{
            //    cm.CommandType = CommandType.StoredProcedure;
            //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_AMBULANCE";
            //    cm.Parameters.Add("@id", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = "NULL";
            //    cm.Parameters.Add("@Fee", SqlDbType.VarChar).Value = "NULL";
            //    SqlDataAdapter Adp = new SqlDataAdapter(cm);
            //    DataTable Dt = new DataTable();
            //    Adp.Fill(Dt);
            //    dropambulanceno.DataSource = Dt;
            //    dropambulanceno.DataTextField = "AmbulanceNo";
            //    dropambulanceno.DataValueField = "AmbulanceNo";
            //    dropambulanceno.DataBind();
            //    dropambulanceno.Items.Insert(0, "Please Select");
            //}

            //using (SqlCommand cmd = new SqlCommand("RECP_AMBULANCE", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "SELECT_DRIVER";
            //    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
            //    cmd.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
            //    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
            //    cmd.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
            //    cmd.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
            //    SqlDataAdapter Adp1 = new SqlDataAdapter(cmd);
            //    DataTable Dt1 = new DataTable();
            //    Adp1.Fill(Dt1);
            //    dropdriver.DataSource = Dt1;
            //    dropdriver.DataTextField = "Sname";
            //    dropdriver.DataValueField = "id";
            //    dropdriver.DataBind();
            //    dropdriver.Items.Insert(0, "Please Select");
            //}
            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: '{0}'", ex);
        }

    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        //using (SqlCommand cm = new SqlCommand("RECP_AMBULANCE", con))
        //{
        //    cm.CommandType = CommandType.StoredProcedure;
        //    cm.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "delete";
        //    cm.Parameters.Add("@id", SqlDbType.VarChar).Value = slno;
        //    cm.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
        //    cm.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
        //    cm.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
        //    cm.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
        //    cm.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
        //    //SqlDataAdapter Adp = new SqlDataAdapter(cm);
        //    //SqlCommand cm = new SqlCommand("delete from tblAmbulance where id='" + slno + "'", con);
        //    cm.ExecuteNonQuery();
        //}

        //binddata();

        string message = string.Empty;
        try
        {
            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();

            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@id", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "delete");

            OBJ_METHOD.ExecuteProceedure("sp_Ambulance", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
            }
            message = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {

            OBJ_METHOD.commitOrRollbackTran("rollback");
            message = "alert('Due to some issues, Data not Deleted.')";
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

        }

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[1];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_AMBULANCE");

            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_AMBULANCE", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = DS1;
                GridView1.PageIndex = e.NewPageIndex;
                GridView1.DataKeyNames = new string[] { "id" };
                GridView1.DataBind();
            }


        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        try
        {

            var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
                        
            SqlParameter[] SQL_PARAMS1 = new SqlParameter[2];

            SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SLECTED_EVENT");
            SQL_PARAMS1[1] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, slno);

            //DataSet DS1 = OBJ_METHOD.Get_DataSet("SELECT id,ADate,PatientName,Driver,AmbulanceNo, AttendentName,ContactNo,ApproxKm,Fee,from,To from tblAmbulance where id='"+slno+"'", false, false);
            DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_AMBULANCE", false, true, SQL_PARAMS1);
            if (DS1.Tables[0].Rows.Count > 0)
            {
                btnSubmit.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = DS1.Tables[0].Rows[0]["ID"].ToString();
                txtAdate.Text = DS1.Tables[0].Rows[0]["ADate"].ToString();
                dropdriver.SelectedValue = DS1.Tables[0].Rows[0]["Driver"].ToString();
                dropambulanceno.SelectedItem.Text = DS1.Tables[0].Rows[0]["AmbulanceNo"].ToString();
                txtpatient.Text = DS1.Tables[0].Rows[0]["PatientName"].ToString();
                txtattend.Text = DS1.Tables[0].Rows[0]["AttendentName"].ToString();
                txtcont.Text = DS1.Tables[0].Rows[0]["ContactNo"].ToString();
                txtfrom.Text = DS1.Tables[0].Rows[0]["fromdistance"].ToString();
                txtto.Text = DS1.Tables[0].Rows[0]["todistance"].ToString();
                txtkm.Text = DS1.Tables[0].Rows[0]["ApproxKm"].ToString();
                txtfee.Text = DS1.Tables[0].Rows[0]["Fee"].ToString();
            }

        }
        catch (Exception ex)
        {
            string message = ex.ToString();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropambulanceno.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Ambulance No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtpatient.Text == "")
            {
                string message = "alert('* Please Enter Patient Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (dropdriver.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Driver.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtcont.Text == "")
            {
                string message = "alert('* Please Enter Contact No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtfrom.Text == "")
            {
                string message = "alert('* Please!! Enter The From Address.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtto.Text == "")
            {
                string message = "alert('* PLease!! Enter To Address')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            else if (txtkm.Text == "" || txtkm.Text == "0")
            {
                string message = "alert('* Please!! Enter Approximate Km.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }
            //else if (txtkm.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            else if (txtfee.Text == "" || txtfee.Text == "0")
            {
                string message = "alert('* Please!! Enter The Fees.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                return;
            }



            //using (SqlCommand cmd = new SqlCommand("sp_Ambulance", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@ADate", SqlDbType.DateTime).Value = txtAdate.Text;
            //    cmd.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
            //    cmd.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
            //    cmd.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
            //    cmd.Parameters.Add("@From", SqlDbType.VarChar).Value = txtfrom.Text;
            //    cmd.Parameters.Add("@To", SqlDbType.VarChar).Value = txtto.Text;
            //    cmd.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
            //    cmd.Parameters.Add("@Fee", SqlDbType.Decimal).Value = Convert.ToDecimal(txtfee.Text);
            //    cmd.Parameters.Add("@AmbulanceNo", SqlDbType.VarChar).Value = dropambulanceno.Text;
            //    cmd.Parameters.Add("@Driver", SqlDbType.VarChar).Value = dropdriver.SelectedValue.ToString();
            //    cmd.Parameters.Add("@UserId", SqlDbType.VarChar).Value = lbluid.Text;

            //    cmd.ExecuteNonQuery();
            //}
            //using (SqlCommand cmd1 = new SqlCommand("RECP_AMBULANCE", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "report";
            //    cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = "";
            //    cmd1.Parameters.Add("@Fee", SqlDbType.VarChar).Value = "";
            //    dr = cmd1.ExecuteReader();
            //    if (dr.Read())
            //    {
            //        txtid.Text = dr["id"].ToString();
            //    }
            //}

            //clearcontrol();

            //binddata();

            //string message1 = "alert('*Successfully Saved.')";
            //ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //Session["idAmb"] = txtid.Text;
            DataMathods OBJ_METHOD = new DataMathods();

            string[] strexp = txtAdate.Text.Split('-');

            DateTime mfd = new DateTime(Convert.ToInt32(strexp[2]), Convert.ToInt32(strexp[1]), Convert.ToInt32(strexp[0]));


            SqlParameter[] SQL_PARAMS = new SqlParameter[15];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@ADate", SqlDbType.DateTime, 0, mfd);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PatientName", SqlDbType.VarChar, 500, txtpatient.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@AttendentName", SqlDbType.VarChar, 500, txtattend.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ContactNo", SqlDbType.VarChar, 500, txtcont.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@From", SqlDbType.VarChar, 500, txtfrom.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@To", SqlDbType.VarChar, 500, txtto.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@ApproxKm", SqlDbType.VarChar, 500, txtkm.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Fee", SqlDbType.VarChar, 500, Convert.ToDecimal(txtfee.Text));
            SQL_PARAMS[9] = OBJ_METHOD.createParams("@AmbulanceNo", SqlDbType.VarChar, 500, dropambulanceno.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@Driver", SqlDbType.VarChar, 500, dropdriver.SelectedValue.ToString());
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@UserId", SqlDbType.VarChar, 500, lbluid.Text);
            SQL_PARAMS[12] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Session["BRANCH_FYR"].ToString());
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"].ToString());
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");

            OBJ_METHOD.ExecuteProceedure("sp_Ambulance", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
               // txtid.Text = dr["id"].ToString();
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                Session["idAmb"] = txtid.Text;

            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
                message1 = "alert('Due to some issues, Data not saved.')";
            }
        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

    }

    public void clearcontrol()
    {
       // txtAdate.Text = "";
        txtpatient.Text = "";
        txtattend.Text = "";
        txtcont.Text = "";
        txtfrom.Text = "";
        txtto.Text = "";
        txtkm.Text = "";
        txtfee.Text = "";
        btnSubmit.Visible = true;
        btnupdate.Visible = false;
        dropambulanceno.SelectedIndex = 0;
        dropdriver.SelectedIndex = 0;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string message1 = string.Empty;
        try
        {
            if (dropambulanceno.Text == "")
            {
                string message = "alert('* Please Select Ambulance No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropambulanceno.Focus();
                return;
            }
            else if (txtpatient.Text == "")
            {
                string message = "alert('* Please Enter Patient Name.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpatient.Focus();
                return;
            }
            //else if (txtattend.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            else if (dropdriver.SelectedIndex == 0)
            {
                string message = "alert('* Please Select Driver.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdriver.Focus();
                return;
            }
            else if (txtcont.Text == "")
            {
                string message = "alert('* Please Enter Contact No.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcont.Focus();
                return;
            }
            else if (txtfrom.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfrom.Focus();
                return;
            }
            else if (txtto.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtto.Focus();
                return;
            }
            //else if (txtkm.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            else if (txtfee.Text == "")
            {
                string message = "alert('* Please Enter Fee.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfee.Focus();
                return;
            }

            //using (SqlCommand cmd1 = new SqlCommand("RECP_AMBULANCE", con))
            //{
            //    cmd1.CommandType = CommandType.StoredProcedure;
            //    cmd1.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    //SqlCommand cmd1 = new SqlCommand("UPDATE tblAmbulance SET PatientName=@PatientName,AttendentName=@AttendentName,ContactNo=@ContactNo,ApproxKm=@ApproxKm,Fee=@Fee WHERE id=@id", con);
            //    cmd1.Parameters.Add("@id", SqlDbType.VarChar).Value = txtid.Text;
            //    cmd1.Parameters.Add("@PatientName", SqlDbType.VarChar).Value = txtpatient.Text;
            //    cmd1.Parameters.Add("@AttendentName", SqlDbType.VarChar).Value = txtattend.Text;
            //    cmd1.Parameters.Add("@ContactNo", SqlDbType.VarChar).Value = txtcont.Text;
            //    //cmd1.Parameters.Add("@From", SqlDbType.VarChar).Value = txtfrom.Text;
            //    //cmd1.Parameters.Add("@[To]", SqlDbType.VarChar).Value = txtto.Text;
            //    cmd1.Parameters.Add("@ApproxKm", SqlDbType.VarChar).Value = txtkm.Text;
            //    cmd1.Parameters.Add("@Fee", SqlDbType.VarChar).Value = txtfee.Text;
            //    //cmd1.Parameters.Add("@AmbulanceNo", SqlDbType.VarChar).Value = dropambulanceno.Text;
            //    //cmd1.Parameters.Add("@Driver", SqlDbType.VarChar).Value = dropdriver.SelectedValue.ToString();
            //    cmd1.ExecuteNonQuery();
            //    binddata();
            //}

            SqlParameter[] SQL_PARAMS = new SqlParameter[9];
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@id", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@PatientName", SqlDbType.VarChar, 500, txtpatient.Text);
            SQL_PARAMS[3] = OBJ_METHOD.createParams("@AttendentName", SqlDbType.VarChar, 500, txtattend.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@ContactNo", SqlDbType.VarChar, 500, txtcont.Text);
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@ApproxKm", SqlDbType.Decimal, 0, txtkm.Text);
            SQL_PARAMS[6] = OBJ_METHOD.createParams("@Fee", SqlDbType.Decimal, 0, txtfee.Text);
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@From", SqlDbType.VarChar, 100, txtfrom.Text);
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@To", SqlDbType.VarChar, 100, txtto.Text);

            OBJ_METHOD.ExecuteProceedure("sp_Ambulance", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();
                message1 = "alert('" + OBJ_METHOD._objOut + "')";
                btnupdate.Visible = false;
                btnSubmit.Visible = true;
            }

        }
        catch (Exception ex)
        {
            OBJ_METHOD.commitOrRollbackTran("rollback");
            message1 = "alert('Due to some issues, Data not Updated.')";
        }
        finally
        {
            clearcontrol();
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //binddata();
        clearcontrol();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortingDirection = string.Empty;
        if (direction == SortDirection.Ascending)
        {
            direction = SortDirection.Descending;
            sortingDirection = "Desc";
        }
        else
        {
            direction = SortDirection.Ascending;
            sortingDirection = "Asc";
        }
        DataView sortedView = new DataView(getdata());
        sortedView.Sort = e.SortExpression + " " + sortingDirection;
        Session["SortedView"] = sortedView;
        GridView1.DataSource = sortedView;
        GridView1.DataBind();
    }
    public SortDirection direction
    {
        get
        {
            if (ViewState["directionState"] == null)
            {
                ViewState["directionState"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["directionState"];
        }
        set
        {
            ViewState["directionState"] = value;
        }
    }
    public DataTable getdata()
    {
        DataTable dt = new DataTable();

        //DataSet Ds = OBJ_METHOD.Get_DataSet("SELECT ID as ID,NAME,CATEGORY,QTY FROM ITEM_TABLE   where Branch_ID='" + Session["Branch"].ToString() + "' order by ID desc", false, false);

        SqlParameter[] SQL_PARAMS1 = new SqlParameter[1];

        SQL_PARAMS1[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SHOW_AMBULANCE");

        DataSet DS1 = OBJ_METHOD.Get_DataSet("RECP_AMBULANCE", false, true, SQL_PARAMS1);
        if (DS1.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = DS1;
            GridView1.DataBind();
        }
        dt = DS1.Tables[0];

        return dt;

    }
}