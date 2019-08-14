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

public partial class ADMIN_admin_staffentry : System.Web.UI.Page
{
    string num1;
    Object num;
    string path = "";
    SqlConnection con;
    SqlDataAdapter da, da1;
    DataSet ds = new DataSet();
    SqlCommand com, cmd;
    SqlDataReader dr, dr1, dr2;
    DataTable dt;
    DataRow dtr;
    int i, no, no1, sl, F;
    int flag = 0;
    string id, id1, ph, val1, val2, p, q, des1, name, k, PIN;
    //public string id_hist;
    //public double amt = 0, qty = 0, amt1 = 0, t_amt = 0, qt = 0, c_rs = 0, avg = 0;
    decimal a, b;
    DateTime DT;
    //double totalamt1, totamt, totalgstamt, dis, dism;
    GridViewRow gr;
    DataMathods OBJ_METHOD = new DataMathods();
    public void auto()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["abcd"].ToString());
        con.Open();
        string qry1 = "select max(empid) from tblStaff";

        com = new SqlCommand(qry1, con);
        //dr = null;

        // dr = com.ExecuteReader();

        // while (dr.Read())
        // {
        num = com.ExecuteScalar(); //dr["EMPID"].ToString();
        num1 = num.ToString();
        //}
        num1 = string.Format("EM{0}", (Convert.ToUInt32(num1.Substring(2)) + 1).ToString("D4"));

        txtid.Text = num1;

        // dr.Close();
        con.Close();
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
            Session["SortedView"] = null;
            binddata();

            SqlParameter[] SQL_PARAMS = new SqlParameter[1];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFFTYPE");
            DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                dropstaff.DataSource = DS.Tables[0];
                dropstaff.DataTextField = "Stype";
                dropstaff.DataValueField = "id";
                dropstaff.DataBind();
                //dropstaff.Items.Insert(0, );
                dropstaff.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                dropdept.DataSource = DS.Tables[0];
                dropdept.DataTextField = "DeptName";
                dropdept.DataValueField = "id";
                dropdept.DataBind();
                //dropdept.Items.Insert(0, "Please Select");
                dropdept.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DESIG");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                dropdesg.DataSource = DS.Tables[0];
                dropdesg.DataTextField = "DesgName";
                dropdesg.DataValueField = "id";
                dropdesg.DataBind();
                //dropdesg.Items.Insert(0, "Please Select");
                dropdesg.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
            }

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFF");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                dropreporting.DataSource = DS.Tables[0];
                dropreporting.DataTextField = "Sname";
                dropreporting.DataValueField = "id";
                dropreporting.DataBind();
                //dropreporting.Items.Insert(0, "Please Select");
                dropreporting.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
            }
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

            

            SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "GRID_PAGE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Session["Branch"]);
            DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
            if (DS.Tables[0].Rows.Count > 0)
            {
                DataTable Dt = new DataTable();
                GridView1.DataSource = DS;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string message = "";


            string slno = GridView1.DataKeys[e.RowIndex].Values["id"].ToString();


            SqlParameter[] SQL_PARAMS = new SqlParameter[2];

            SQL_PARAMS[0] = OBJ_METHOD.createParams("@ID", SqlDbType.Int, 0, slno);
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "DELETE_UPDATE");

            OBJ_METHOD.ExecuteProceedure("sp_InsertStaff", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);
            //write it in ap_insert staff

            if (OBJ_METHOD._RESULT > 0)
            {
                OBJ_METHOD.commitOrRollbackTran("commit");
                binddata();

            }
            message = "alert('" + OBJ_METHOD._objOut + "')";

            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // reference the Delete LinkButton
            LinkButton db = (LinkButton)e.Row.Cells[7].Controls[0];

            db.OnClientClick = "return confirm('Are you sure want to delete this info ?');";
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            DataSet ds = OBJ_METHOD.Get_DataSet("Select id,Sname,TempAddress,Email,Contact,Bloodgroup,Status from tblStaff", false, false);


            GridView1.DataSource = ds;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataKeyNames = new string[] { "id" };
            GridView1.DataBind();
            if (Session["SortedView"] != null)
            {
                GridView1.DataSource = Session["SortedView"];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }

    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        var slno = GridView1.DataKeys[e.NewSelectedIndex].Values["id"].ToString();
        try
        {
            DataSet Ds = OBJ_METHOD.Get_DataSet("select * from tblStaff where id='" + slno + "' ", false, false);
            
            if (Ds.Tables[0].Rows.Count>0)
            {
                btncreate.Visible = false;
                btnupdate.Visible = true;
                txtid.Text = slno.ToString();
                lblempid.Text =Ds.Tables[0].Rows[0]["EMPID"].ToString();
                txtname.Text = Ds.Tables[0].Rows[0]["Sname"].ToString();
                dropdesg.SelectedValue = Ds.Tables[0].Rows[0]["DesgId"].ToString();
                txtdoj.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DOJ"]).ToString("dd-MM-yyyy");
                txtdob.Text = Convert.ToDateTime(Ds.Tables[0].Rows[0]["DOB"]).ToString("dd-MM-yyyy");
                dropstaff.SelectedValue = Ds.Tables[0].Rows[0]["Stafftypeid"].ToString();
                txtexp.Text = Ds.Tables[0].Rows[0]["Exp"].ToString();
                txtspl.Text = Ds.Tables[0].Rows[0]["Specilisation"].ToString();
                txtemail.Text = Ds.Tables[0].Rows[0]["Email"].ToString();
                txtadhar.Text = Ds.Tables[0].Rows[0]["Adharcard"].ToString();
                txttadd.Text = Ds.Tables[0].Rows[0]["TempAddress"].ToString();
                txtpadd.Text = Ds.Tables[0].Rows[0]["PermAddress"].ToString();
                if (txttadd.Text == txtpadd.Text)
                {
                    CheckBox1.Checked = true;
                }
                else 
                {
                    CheckBox1.Checked = false;
                }
                txtcontact.Text = Ds.Tables[0].Rows[0]["Contact"].ToString();
                dropblood.Text = Ds.Tables[0].Rows[0]["Bloodgroup"].ToString();
                dropgen.Text = Ds.Tables[0].Rows[0]["Gender"].ToString();
                dropdept.SelectedValue = Ds.Tables[0].Rows[0]["Deptid"].ToString();
                txtpf.Text = Ds.Tables[0].Rows[0]["PFNo"].ToString();
                txtesi.Text = Ds.Tables[0].Rows[0]["ESINo"].ToString();
                //txtpf.Text = dr["PFNo"].ToString();
                dropreporting.SelectedValue = Ds.Tables[0].Rows[0]["reportingtoid"].ToString();

                txtbasic.Text = Ds.Tables[0].Rows[0]["Basic"].ToString();
                txthra.Text = Ds.Tables[0].Rows[0]["HRA"].ToString();
                txtcon.Text = Ds.Tables[0].Rows[0]["Con"].ToString();
                txtmed.Text = Ds.Tables[0].Rows[0]["Med"].ToString();
                txtfee.Text = Ds.Tables[0].Rows[0]["FEE"].ToString();
                txtfollowup.Text = Ds.Tables[0].Rows[0]["FOLLOWUP"].ToString();
                    

                //SqlParameter[] SQL_PARAMS = new SqlParameter[1];
  
                //SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DEPT");
                //DataSet DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
                //        if (DS.Tables[0].Rows.Count > 0)
                //        {
                //dropdept.DataSource = DS.Tables[0];
                //dropdept.DataTextField = "DeptName";
                //dropdept.DataValueField = "id";
                //dropdept.DataBind();
                ////dropdept.Items.Insert(0, "Please Select");
                //dropdept.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                //dropdept.SelectedValue = Session["id"].ToString();
                //}


                // SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFFTYPE");
                // DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
                //if (DS.Tables[0].Rows.Count > 0)
                //{
                //    dropstaff.DataSource = DS.Tables[0];
                //    dropstaff.DataTextField = "Stype";
                //    dropstaff.DataValueField = "id";
                //    dropstaff.DataBind();
                //    //dropstaff.Items.Insert(0, "Please Select");
                //    dropstaff.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                //    dropstaff.SelectedValue = Session["staffid"].ToString();
                //}

                //        SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_DESIG");
                //DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
                //if (DS.Tables[0].Rows.Count > 0)
                //{
                //    dropdesg.DataSource = DS.Tables[0];
                //    dropdesg.DataTextField = "DesgName";
                //    dropdesg.DataValueField = "id";
                //    dropdesg.DataBind();
                //    //dropdesg.Items.Insert(0, "Please Select");
                //    dropdesg.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                //    dropdesg.SelectedValue = Session["degid"].ToString();
                //}
                //       SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "SELECT_STAFF");
                //DS = OBJ_METHOD.Get_DataSet("ADMIN_STAFF_ENTRY", false, true, SQL_PARAMS);
                //if (DS.Tables[0].Rows.Count > 0)
                //{
                //    dropreporting.DataSource = DS.Tables[0];
                //    dropreporting.DataTextField = "Sname";
                //    dropreporting.DataValueField = "id";
                //    dropreporting.DataBind();
                //    //dropreporting.Items.Insert(0, "Please Select");
                //    dropreporting.Items.Insert(0, new ListItem("Please Select", "0")); //updated code
                //    dropreporting.SelectedValue = Session["reportid"].ToString();
                //} 
                //   dropdesg_SelectedIndexChanged(sender, e); 

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
        
        string message1 = "";
        try
        {
            // Validation
            if (txtname.Text == "")
            {
                string message = "alert('Please!!Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txttadd.Text == "")
            {
                string message = "alert('Please!!Enter The Present address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txttadd.Focus();
                return;
            }
            else if (dropdesg.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Designation..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdesg.Focus();
                return;
            }
            else if (txtpadd.Text == "")
            {
                string message = "alert('Please!!Enter The Permanent Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpadd.Focus();
                return;
            }
            else if (txtdoj.Text == "")
            {
                string message = "alert('Please!!Enter The Date Of Joining..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdoj.Focus();
                return;
            }
            else if (dropblood.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Blood Group..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropblood.Focus();
                return;
            }
            else if (txtdob.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdob.Focus();
                return;
            }
            else if (dropgen.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropgen.Focus();
                return;
            }
            else if (dropstaff.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The JobType..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropstaff.Focus();
                return;
            }
            else if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Department..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;
            }
            else if (txtexp.Text == "")
            {
                string message = "alert('Please!!Enter The Experience..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtexp.Focus();
                return;
            }

            else if (txtadhar.Text == "")
            {
                string message = "alert('Please!!Enter The Adhar Card')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtadhar.Focus();
                return;
            }
            OBJ_METHOD = new DataMathods();
            DataSet Ds = OBJ_METHOD.Get_DataSet("select Adharcard from tblStaff where Adharcard='" + txtadhar.Text + "'", false, false);


            if (Ds.Tables[0].Rows.Count > 0)
            {
                string message = "alert('* Adharcard " + txtadhar.Text + " Already Exist.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtadhar.Focus();
                return;
            }
            else if (txtcontact.Text == "")
            {
                string message = "alert('Please!!Enter the Emergency Contact..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcontact.Focus();
                return;
            }

            else if (txtbasic.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbasic.Focus();
                return;
            }
            else if (txthra.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txthra.Focus();
                return;
            }
            else if (txtcon.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcon.Focus();
                return;
            }
            else if (txtmed.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmed.Focus();
                return;
            }
            if (dropdesg.SelectedItem.Text == "Doctor" && txtfee.Text == "")
            {
                string message = "alert('Please!!Enter The Fee..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfee.Focus();
                return;
            }
            else if (txtfollowup.Text == "")
            {
                txtfollowup.Text = "0";
            }
            else if (txtfee.Text == "")
            {
                txtfee.Text = "0";
            }


            auto();

            /////

            ///File Upload to Folder
            if (FileUpload1.HasFile)
            {

                Guid id = Guid.NewGuid();
                path = "~/Admin/staff/EMPPhoto" + id + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath(path));
            }
            else
            {
                path = "~/Admin/staff/EMPPhoto/dimage.png";
            }

            lblempid.Text = num1;


            SqlParameter[] SQL_PARAMS = new SqlParameter[32];

            //date in split format 
            string[] strdoj = txtdoj.Text.Split('-');

            DateTime doj = new DateTime(Convert.ToInt32(strdoj[2]), Convert.ToInt32(strdoj[1]), Convert.ToInt32(strdoj[0]));

            string[] strdob = txtdob.Text.Split('-');

            DateTime dob = new DateTime(Convert.ToInt32(strdob[2]), Convert.ToInt32(strdob[1]), Convert.ToInt32(strdob[0]));

             OBJ_METHOD = new DataMathods();
            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "INSERT");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@EMPID", SqlDbType.VarChar, 500, txtid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DesgId", SqlDbType.Int, 0, Convert.ToInt32(dropdesg.SelectedValue));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DOJ", SqlDbType.Date, 0, doj);//

            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DOB", SqlDbType.Date, 0, dob);//
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Stafftypeid", SqlDbType.Int, 0, Convert.ToInt32(dropstaff.SelectedValue));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Exp", SqlDbType.VarChar, 500, txtexp.Text);

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@Specilisation", SqlDbType.VarChar, 500, txtspl.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@Email", SqlDbType.VarChar, 500, txtemail.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@Adharcard", SqlDbType.VarChar, 500, txtadhar.Text);

            SQL_PARAMS[12] = OBJ_METHOD.createParams("@TempAddress", SqlDbType.VarChar, 500, txttadd.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@PermAddress", SqlDbType.VarChar, 500, txtpadd.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@Contact", SqlDbType.VarChar, 500, txtcontact.Text);

            SQL_PARAMS[15] = OBJ_METHOD.createParams("@Bloodgroup", SqlDbType.VarChar, 500, dropblood.SelectedValue);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@Gender", SqlDbType.VarChar, 50, dropgen.SelectedValue);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@Deptid", SqlDbType.Int, 0,Convert.ToInt32( dropdept.SelectedValue));

            SQL_PARAMS[18] = OBJ_METHOD.createParams("@PFNo", SqlDbType.VarChar, 500, txtpf.Text);
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@ESINo", SqlDbType.VarChar, 500, txtesi.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@Photo", SqlDbType.VarChar, 500, path);

            SQL_PARAMS[21] = OBJ_METHOD.createParams("@DateStamp", SqlDbType.DateTime, 0, DateTime.Now);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@Status", SqlDbType.VarChar, 50, "Active");
            SQL_PARAMS[23] = OBJ_METHOD.createParams("@reportingtoid", SqlDbType.Int, 0,Convert.ToInt32(dropreporting.SelectedValue));

            SQL_PARAMS[24] = OBJ_METHOD.createParams("@Basic", SqlDbType.Decimal, 0, Convert.ToDecimal(txtbasic.Text));
            SQL_PARAMS[25] = OBJ_METHOD.createParams("@hra", SqlDbType.Decimal, 0,Convert.ToDecimal( txthra.Text));
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@con", SqlDbType.Decimal, 0, Convert.ToDecimal(txtcon.Text));

            SQL_PARAMS[27] = OBJ_METHOD.createParams("@med", SqlDbType.Decimal, 0,Convert.ToDecimal( txtmed.Text));
            SQL_PARAMS[28] = OBJ_METHOD.createParams("@FOLLOWUP", SqlDbType.Int, 0,Convert.ToInt32(txtfollowup.Text));
            SQL_PARAMS[29] = OBJ_METHOD.createParams("@FEE", SqlDbType.Decimal, 500,Convert.ToDecimal(txtfee.Text));

            SQL_PARAMS[30] = OBJ_METHOD.createParams("@Branch_FY", SqlDbType.Int, 0, Convert.ToInt32(Session["BRANCH_FYR"]));
            SQL_PARAMS[31] = OBJ_METHOD.createParams("@Branch_ID", SqlDbType.Int, 0, Convert.ToInt32(Session["Branch"]));


            OBJ_METHOD.ExecuteProceedure("sp_InsertStaff", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();

            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
            }
            binddata();
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
        }
        catch (Exception ex)
        {
            message1 = ex.Message.ToString();

        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
        }
    }

    public void clearcontrol()
    {
        txtname.Text = "";
        txtdoj.Text = "";
        txtdob.Text = "";
        txtexp.Text = "";
        txtspl.Text = "";
        txtemail.Text = "";
        txtadhar.Text = "";
        txttadd.Text = "";
        txtpadd.Text = "";
        txtcontact.Text = "";
        txtpf.Text = "";
        txtesi.Text = "";
        txtbasic.Text = "0";
        txthra.Text = "0";
        txtcon.Text = "0";
        txtmed.Text = "0";
        dropblood.SelectedIndex = 0;
        dropdept.SelectedIndex = 0;
        dropdesg.SelectedIndex = 0;
        dropreporting.SelectedIndex = 0;
        dropgen.SelectedIndex = 0;
        dropstaff.SelectedIndex = 0;
        txtfollowup.Text = "0";
        txtfee.Text = "0";
        CheckBox1.Checked = false;
        btncreate.Visible = true;
        btnupdate.Visible = false;
        btndelete.Visible = false;
    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        string message1 = "";
        try
        {
            if (txtname.Text == "")
            {
                string message = "alert('Please!!Enter The Name..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtname.Focus();
                return;
            }
            else if (txttadd.Text == "")
            {
                string message = "alert('Please!!Enter The Present address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txttadd.Focus();
                return;
            }
            else if (dropdesg.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Designation..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdesg.Focus();
                return;
            }
            else if (txtpadd.Text == "")
            {
                string message = "alert('Please!!Enter The Permanent Address..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtpadd.Focus();
                return;
            }
            else if (txtdoj.Text == "")
            {
                string message = "alert('Please!!Enter The Date Of Joining..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdoj.Focus();
                return;
            }
            else if (dropblood.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Blood Group..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropblood.Focus();
                return;
            }
            else if (txtdob.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtdob.Focus();
                return;
            }
            else if (dropgen.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Gender..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropgen.Focus();
                return;
            }
            else if (dropstaff.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The JobType..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropstaff.Focus();
                return;
            }
            else if (dropdept.SelectedIndex == 0)
            {
                string message = "alert('Please!!Select The Department..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                dropdept.Focus();
                return;
            }
            else if (txtexp.Text == "")
            {
                string message = "alert('Please!!Enter The Experience..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtexp.Focus();
                return;
            }
            //else if (txtemail.Text == "")
            //{
            //    string message = "alert('* Fields are mandatory.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

            //    return;
            //}
            else if (txtadhar.Text == "")
            {
                string message = "alert('Please!!Enter The Adhar Card')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtadhar.Focus();
                return;
            }
            
            else if (txtcontact.Text == "")
            {
                string message = "alert('Please!!Enter the Emergency Contact..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcontact.Focus();
                return;
            }

            else if (txtbasic.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtbasic.Focus();
                return;
            }
            else if (txthra.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txthra.Focus();
                return;
            }
            else if (txtcon.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtcon.Focus();
                return;
            }
            else if (txtmed.Text == "")
            {
                string message = "alert('* Fields are mandatory.')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtmed.Focus();
                return;
            }
            if (dropdesg.SelectedItem.Text == "Doctor" && txtfee.Text == "")
            {
                string message = "alert('Please!!Enter The Fee..')";
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
                txtfee.Focus();
                return;
            }
            else if (txtfollowup.Text == "")
            {
                txtfollowup.Text = "0";
            }
            else if (txtfee.Text == "")
            {
                txtfee.Text = "0";
            }




            ///File Upload to Folder
            if (FileUpload1.HasFile)
            {

                Guid id = Guid.NewGuid();
                path = "~/Admin/staff/EMPPhoto" + id + FileUpload1.FileName;
                FileUpload1.SaveAs(Server.MapPath(path));
            }


            SqlParameter[] SQL_PARAMS = new SqlParameter[30];

            string[] strdoj = txtdoj.Text.Split('-');

            DateTime doj = new DateTime(Convert.ToInt32(strdoj[2]), Convert.ToInt32(strdoj[1]), Convert.ToInt32(strdoj[0]));

            string[] strdob = txtdob.Text.Split('-');

            DateTime dob = new DateTime(Convert.ToInt32(strdob[2]), Convert.ToInt32(strdob[1]), Convert.ToInt32(strdob[0]));


            SQL_PARAMS[0] = OBJ_METHOD.createParams("@DBOpration", SqlDbType.VarChar, 500, "UPDATE");
            SQL_PARAMS[1] = OBJ_METHOD.createParams("@EMPID", SqlDbType.VarChar, 500, lblempid.Text);
            SQL_PARAMS[2] = OBJ_METHOD.createParams("@ORGID", SqlDbType.VarChar, 500, lblorgid.Text);

            SQL_PARAMS[3] = OBJ_METHOD.createParams("@Sname", SqlDbType.VarChar, 500, txtname.Text);
            SQL_PARAMS[4] = OBJ_METHOD.createParams("@DesgId", SqlDbType.Int, 0, Convert.ToInt32(dropdesg.SelectedValue));
            SQL_PARAMS[5] = OBJ_METHOD.createParams("@DOJ", SqlDbType.Date, 0, doj);//

            SQL_PARAMS[6] = OBJ_METHOD.createParams("@DOB", SqlDbType.Date, 0, dob);//
            SQL_PARAMS[7] = OBJ_METHOD.createParams("@Stafftypeid", SqlDbType.Int, 0, Convert.ToInt32(dropstaff.SelectedValue));
            SQL_PARAMS[8] = OBJ_METHOD.createParams("@Exp", SqlDbType.VarChar, 500, txtexp.Text);

            SQL_PARAMS[9] = OBJ_METHOD.createParams("@Specilisation", SqlDbType.VarChar, 500, txtspl.Text);
            SQL_PARAMS[10] = OBJ_METHOD.createParams("@Email", SqlDbType.VarChar, 500, txtemail.Text);
            SQL_PARAMS[11] = OBJ_METHOD.createParams("@Adharcard", SqlDbType.VarChar, 500, txtadhar.Text);

            SQL_PARAMS[12] = OBJ_METHOD.createParams("@TempAddress", SqlDbType.VarChar, 500, txttadd.Text);
            SQL_PARAMS[13] = OBJ_METHOD.createParams("@PermAddress", SqlDbType.VarChar, 500, txtpadd.Text);
            SQL_PARAMS[14] = OBJ_METHOD.createParams("@Contact", SqlDbType.VarChar, 500, txtcontact.Text);

            SQL_PARAMS[15] = OBJ_METHOD.createParams("@Bloodgroup", SqlDbType.VarChar, 500, dropblood.SelectedValue);
            SQL_PARAMS[16] = OBJ_METHOD.createParams("@Gender", SqlDbType.VarChar, 50, dropgen.SelectedValue);
            SQL_PARAMS[17] = OBJ_METHOD.createParams("@Deptid", SqlDbType.Int, 0, dropdept.SelectedValue);

            SQL_PARAMS[18] = OBJ_METHOD.createParams("@PFNo", SqlDbType.VarChar, 500, txtpf.Text);
            SQL_PARAMS[19] = OBJ_METHOD.createParams("@ESINo", SqlDbType.VarChar, 500, txtesi.Text);
            SQL_PARAMS[20] = OBJ_METHOD.createParams("@Photo", SqlDbType.VarChar, 500, path);

            SQL_PARAMS[21] = OBJ_METHOD.createParams("@DateStamp", SqlDbType.DateTime, 0, DateTime.Now);
            SQL_PARAMS[22] = OBJ_METHOD.createParams("@Status", SqlDbType.VarChar, 50, "Active");
            SQL_PARAMS[23] = OBJ_METHOD.createParams("@reportingtoid", SqlDbType.Int, 0, dropreporting.SelectedValue);

            SQL_PARAMS[24] = OBJ_METHOD.createParams("@Basic", SqlDbType.Decimal, 0, txtbasic.Text);
            SQL_PARAMS[25] = OBJ_METHOD.createParams("@hra", SqlDbType.Decimal, 0, txthra.Text);
            SQL_PARAMS[26] = OBJ_METHOD.createParams("@con", SqlDbType.Decimal, 0, txtcon.Text);

            SQL_PARAMS[27] = OBJ_METHOD.createParams("@med", SqlDbType.Decimal, 0, txtmed.Text);
            SQL_PARAMS[28] = OBJ_METHOD.createParams("@FOLLOWUP", SqlDbType.Int, 0, txtfollowup.Text);
            SQL_PARAMS[29] = OBJ_METHOD.createParams("@FEE", SqlDbType.Decimal, 500, txtfee.Text);




            OBJ_METHOD.ExecuteProceedure("sp_InsertStaff", "", "@msg", SqlDbType.VarChar, SQL_PARAMS, true);

            if (OBJ_METHOD._RESULT > 0)
            {

                OBJ_METHOD.commitOrRollbackTran("commit");
                clearcontrol();
                btncreate.Visible = true;
                btnupdate.Visible = false;

            }
            else
            {
                OBJ_METHOD.commitOrRollbackTran("rollback");
            }



            binddata();
            message1 = "alert('" + OBJ_METHOD._objOut + "')";
            //using (SqlCommand cmd = new SqlCommand("sp_InsertStaff", con))
            //{
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.Add("@DBOpration", SqlDbType.VarChar).Value = "UPDATE";
            //    cmd.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = lblempid.Text;
            //    cmd.Parameters.Add("@ORGID", SqlDbType.VarChar).Value = lblorgid.Text;
            //    cmd.Parameters.Add("@Sname", SqlDbType.VarChar).Value = txtname.Text;

            //    cmd.Parameters.Add("@DesgId", SqlDbType.Int).Value = dropdesg.SelectedValue;
            //    cmd.Parameters.Add("@DOJ", SqlDbType.Date).Value = Convert.ToDateTime(txtdoj.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@DOB", SqlDbType.Date).Value = Convert.ToDateTime(txtdob.Text).ToString("yyyy-MM-dd");
            //    cmd.Parameters.Add("@Stafftypeid", SqlDbType.Int).Value = Convert.ToInt32(dropstaff.SelectedValue);

            //    cmd.Parameters.Add("@Exp", SqlDbType.VarChar).Value = txtexp.Text;
            //    cmd.Parameters.Add("@Specilisation", SqlDbType.VarChar).Value = txtspl.Text;
            //    cmd.Parameters.Add("@Email", SqlDbType.VarChar).Value = txtemail.Text;
            //    cmd.Parameters.Add("@Adharcard", SqlDbType.VarChar).Value = txtadhar.Text;

            //    cmd.Parameters.Add("@TempAddress", SqlDbType.VarChar).Value = txttadd.Text;
            //    cmd.Parameters.Add("@PermAddress", SqlDbType.VarChar).Value = txtpadd.Text;
            //    cmd.Parameters.Add("@Contact", SqlDbType.VarChar).Value = txtcontact.Text;
            //    cmd.Parameters.Add("@Bloodgroup", SqlDbType.VarChar).Value = dropblood.Text;

            //    cmd.Parameters.Add("@Gender", SqlDbType.VarChar).Value = dropgen.Text;
            //    cmd.Parameters.Add("@Deptid", SqlDbType.Int).Value = Convert.ToInt32(dropdept.SelectedValue);
            //    cmd.Parameters.Add("@PFNo", SqlDbType.VarChar).Value = txtpf.Text;
            //    cmd.Parameters.Add("@ESINo", SqlDbType.VarChar).Value = txtesi.Text;

            //    cmd.Parameters.Add("@Photo", SqlDbType.VarChar).Value = path;
            //    cmd.Parameters.Add("@DateStamp", SqlDbType.DateTime).Value = DateTime.Now.ToString();
            //    cmd.Parameters.Add("@Status", SqlDbType.VarChar).Value = "Active";
            //    cmd.Parameters.Add("@reportingtoid", SqlDbType.BigInt).Value = dropreporting.SelectedValue.ToString();

            //    cmd.Parameters.Add("@Basic", SqlDbType.Decimal).Value = txtbasic.Text;
            //    cmd.Parameters.Add("@hra", SqlDbType.Decimal).Value = txthra.Text;
            //    cmd.Parameters.Add("@con", SqlDbType.Decimal).Value = txtcon.Text;
            //    cmd.Parameters.Add("@med", SqlDbType.Decimal).Value = txtmed.Text;

            //    cmd.Parameters.Add("@FOLLOWUP", SqlDbType.Int).Value = txtfollowup.Text;
            //    cmd.Parameters.Add("@FEE", SqlDbType.Decimal).Value = txtfee.Text;
            //    cmd.ExecuteNonQuery();

            //    string message = "alert('Updated Sucessfully.')";
            //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
            //    clearcontrol();
            //    binddata();
            //    return;
            //}
        }
        catch (Exception ex)
        {

            message1 = ex.ToString();
        }
        finally
        {
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message1, true);
            //Response.Redirect("~/ADMIN/admin_staffentry.aspx");
        }
        

    }
    protected void Btndelete_Click(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
       // Response.Redirect("~/ADMIN/admin_staffentry.aspx");
        clearcontrol();
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox1.Checked == true)
            {
                txtpadd.Text = txttadd.Text;
            }
            else
            {
                txtpadd.Text = "NA";
            }
        }
        catch (Exception ex)
        {
            

        }
    }
    protected void dropdesg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (dropdesg.SelectedItem.Text == "Doctor" || dropdesg.SelectedItem.Text == "DOCTOR")
            {
                txtfollowup.Enabled = true;
                txtfee.Enabled = true;
            }
            else
            {
                txtfollowup.Enabled = false;
                txtfee.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            

        }
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

        
        DataSet ds = OBJ_METHOD.Get_DataSet("Select id,Sname,TempAddress,Email,Contact,Bloodgroup,Status from tblStaff", false, false);

        dt = ds.Tables[0];
        return dt;

    }
}