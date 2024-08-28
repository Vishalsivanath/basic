using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace Grid
{
    public partial class Gridview : System.Web.UI.Page
    {
        string conn = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
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
            GridView1.EditIndex = -1;
            GridView();
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ddlCountry.SelectedValue);
            if (id > 0)
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
            GridView1.EditIndex = -1;
            GridView();
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

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string name = (row.FindControl("text2") as TextBox).Text;
            string gender = (row.FindControl("text3") as TextBox).Text;
            string role = (row.FindControl("text4") as TextBox).Text;
            DropDownList ddl1 = (row.FindControl("ddlcountry") as DropDownList);
            int country = Convert.ToInt32(ddl1.SelectedItem.Value);
            DropDownList ddl2 = (row.FindControl("ddlstate") as DropDownList);
            int state = Convert.ToInt32(ddl2.SelectedItem.Value);
            DropDownList ddl3 = (row.FindControl("ddlcity") as DropDownList);
            int city = Convert.ToInt32(ddl3.SelectedItem.Value);

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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            TextBox textId = (TextBox)row.FindControl("textId");
            TextBox text2 = (TextBox)row.FindControl("text2");
            TextBox text3 = (TextBox)row.FindControl("text3");
            TextBox text4 = (TextBox)row.FindControl("text4");
            DropDownList ddlcountry = (DropDownList)row.FindControl("ddlcountry");
            DropDownList ddlstate = (DropDownList)row.FindControl("ddlstate");
            DropDownList ddlcity = (DropDownList)row.FindControl("ddlcity");


            Label lbltextId = (Label)row.FindControl("lbltextId");
            Label lbltext2 = (Label)row.FindControl("lbltext2");
            Label lbltext3 = (Label)row.FindControl("lbltext3");
            Label lbltext4 = (Label)row.FindControl("lbltext4");
            Label lbltext5 = (Label)row.FindControl("lbltext5");
            Label lbltext6 = (Label)row.FindControl("lbltext6");
            Label lbltext7 = (Label)row.FindControl("lbltext7");

            textId.Visible = true;
            text2.Visible = true;
            text3.Visible = true;
            text4.Visible = true;
            ddlcountry.Visible = true;
            ddlstate.Visible = true;
            ddlcity.Visible = true;

            lbltextId.Visible = false;
            lbltext2.Visible = false;
            lbltext3.Visible = false;
            lbltext4.Visible = false;
            lbltext5.Visible = false;
            lbltext6.Visible = false;
            lbltext7.Visible = false;
            //GridView();

            SqlConnection sc = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand("GetAllCountries", sc);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            ddlcountry.DataSource = dr;
            ddlcountry.DataTextField = "Country";
            ddlcountry.DataValueField = "Countryid";
            ddlcountry.DataBind();
            ddlcountry.Items.Insert(0, new ListItem("Select Country", "0"));

        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = e.RowIndex - 1;
            GridView();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }

        protected void ddlcountry_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DropDownList ddlcountry = (DropDownList)sender;
            GridViewRow gvrow = (GridViewRow)ddlcountry.NamingContainer;

            DropDownList ddlgvcountry = (DropDownList)gvrow.FindControl("ddlcountry");
            DropDownList ddlgvstate = (DropDownList)gvrow.FindControl("ddlstate");
            DropDownList ddlgvcity = (DropDownList)gvrow.FindControl("ddlcity");

            ddlgvstate.Items.Clear();
            ddlgvcity.Items.Clear();

            if (ddlgvcountry != null)
            {
                int selectedCountryValue = Convert.ToInt32(ddlgvcountry.SelectedValue);
                
                SqlConnection sc = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand("GetAllStates", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", selectedCountryValue);
                sc.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlgvstate.DataSource = dr;
                ddlgvstate.DataTextField = "state";
                ddlgvstate.DataValueField = "StateId";
                ddlgvstate.DataBind();
                ddlgvstate.Items.Insert(0, new ListItem("--Select State--", "0"));
                ddlgvstate.Enabled = true;
                ddlgvstate.Visible = true;

            }
            else
            {
                ddlgvstate.Enabled = false;
                ddlgvstate.Items.Clear();
                ddlgvstate.Items.Insert(0, new ListItem("--Select State--", "0"));
                
            }

        }

        protected void ddlstate_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow gvrow = (GridViewRow)ddl.NamingContainer;
            DropDownList ddlgvstate = (DropDownList)gvrow.FindControl("ddlstate");
            DropDownList ddlgvcity = (DropDownList)gvrow.FindControl("ddlcity");
            if (ddlgvstate != null)
            {
                int selectedCountryValue = Convert.ToInt32(ddlgvstate.SelectedValue);
                SqlConnection sc = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand("GetAllCities", sc);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Stateid", selectedCountryValue);
                sc.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                ddlgvcity.DataSource = dr;
                ddlgvcity.DataTextField = "city";
                ddlgvcity.DataValueField = "Cityid";
                ddlgvcity.DataBind();
                ddlgvcity.Items.Insert(0, new ListItem("--Select City--", "0"));
                ddlgvcity.Enabled = true;
                ddlgvcity.Visible = true;
            }
            else
            {
                ddlgvcity.Enabled = false;
                ddlgvstate.Items.Clear();
                ddlgvstate.Items.Insert(0, new ListItem("--Select State--", "0"));

            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainPage.aspx");
        }
    }
}
