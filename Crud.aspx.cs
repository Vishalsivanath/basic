using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CrudOperation
{
    public partial class Crud : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView();
                BindCountries();
            }
        }
        protected void btn_Sumbit_Click(object sender, EventArgs e)
        {
            string gender;
            if (RdFemale.Checked)
            {
                gender = "Female";
            }
            else if (RdMale.Checked)
            {
                gender = "Male";
            }
            else
            {
                gender = "others";
            }
            try
            {
                SqlConnection sc = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand("AddEmploye", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empName", txtName.Text);
                cmd.Parameters.AddWithValue("@Gender", gender);
                cmd.Parameters.AddWithValue("@empRole", ddlROle.SelectedValue);
                cmd.Parameters.AddWithValue("@cityid", Convert.ToInt32(ddlCity.SelectedValue));
                cmd.Parameters.AddWithValue("@stateid", Convert.ToInt32(ddlState.SelectedValue));
                cmd.Parameters.AddWithValue("@countryid", Convert.ToInt32(ddlCountry.SelectedValue));

                sc.Open();
                cmd.ExecuteNonQuery();
                sc.Close();
                GridView();

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            
        }
        protected void GridView()
        {
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("All_employes", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            DataSet dt = new DataSet();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        protected void Gridview1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            
            GridView();
        }

        protected void Gridview1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = e.RowIndex - 1;
            GridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string name = (row.FindControl("text2") as TextBox).Text;
            string gender = (row.FindControl("text3") as TextBox).Text;
            string role = (row.FindControl("text4") as TextBox).Text;
            DropDownList ddl1 = (row.FindControl("ddlcountry") as DropDownList);
            int country = ddl1.SelectedIndex+100;
            DropDownList ddl2 = (row.FindControl("ddlstate") as DropDownList);
            int state = ddl2.SelectedIndex+1000;
            DropDownList ddl3 = (row.FindControl("ddlcity") as DropDownList);
            int city = ddl3.SelectedIndex+100;

            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("UpdateEmploye", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sc.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", id);
            cmd.Parameters.AddWithValue("@empName", name);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@empRole", role);
            cmd.Parameters.AddWithValue("@countryid", country);
            cmd.Parameters.AddWithValue("@stateid", state);
            cmd.Parameters.AddWithValue("@cityid", city);
            cmd.ExecuteNonQuery();
            sc.Close();
            GridView1.EditIndex = -1;
            GridView();
        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values[0]);
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("employeDelete", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@empid", id);
            sc.Open();
            cmd.ExecuteNonQuery();
            sc.Close();
            GridView();
        }

        protected void BindCountries()
        {
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("GetAllCountries", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ddlCountry.DataSource = dr;
            ddlCountry.DataTextField = "Country";
            ddlCountry.DataValueField = "Countryid";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("Select Country", "0"));
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlCountry.SelectedValue);
            if(id>0)
            {
                BindStates(id);
            }
            else
            {
                ddlState.Items.Clear();
                ddlState.Items.Add(new ListItem("Select State", "0"));
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem("Select City", "0"));
            }
        }
        protected void BindStates(int id)
        {
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("GetAllStates", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ddlState.DataSource = dr;
            ddlState.DataTextField = "state";
            ddlState.DataValueField = "StateId";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select State--", "0"));
        }

     

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlState.SelectedValue);
            if (id > 0)
            {
                BindCities(id);
            }
            else
            {
                ddlCity.Items.Clear();
                ddlCity.Items.Add(new ListItem("Select City", "0"));
            }

        }

        protected void BindCities(int id)
        {
            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("GetAllCities", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Stateid", id);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ddlCity.DataSource = dr;
            ddlCity.DataTextField = "city";
            ddlCity.DataValueField = "Cityid";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--Select City--", "0"));
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState.HasFlag(DataControlRowState.Edit))

            {



                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddl = (DropDownList)e.Row.FindControl("ddlcountry");
                    if (ddl != null)
                    {
                        SqlConnection sc = new SqlConnection(conn);
                        SqlCommand cmd = new SqlCommand("GetAllCountries", sc);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sc.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ddl.DataSource = dt;
                        ddl.DataTextField = "country";
                        ddl.DataBind();
                        ddl.Items.Insert(0, new ListItem("--Select Country--", "0"));
                    }
                    DropDownList ddl1 = (DropDownList)e.Row.FindControl("ddlstate");
                    if (ddl1 != null)
                    {
                        SqlConnection sc = new SqlConnection(conn);
                        SqlCommand cmd = new SqlCommand("States", sc);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sc.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ddl1.DataSource = dt;
                        ddl1.DataTextField = "state";

                        ddl1.DataBind();
                        ddl1.Items.Insert(0, new ListItem("--Select State--", "0"));
                    }
                    DropDownList ddl2 = (DropDownList)e.Row.FindControl("ddlcity");
                    if (ddl2 != null)
                    {
                        SqlConnection sc = new SqlConnection(conn);
                        SqlCommand cmd = new SqlCommand("Cities", sc);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        sc.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ddl2.DataSource = dt;
                        ddl2.DataTextField = "city";

                        ddl2.DataBind();
                        ddl2.Items.Insert(0, new ListItem("--Select city--", "0"));
                    }
                }
            }
        }
    }
}

