<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Gridview.aspx.cs" Inherits="Grid.Gridview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div>
            <asp:Table runat="server">
                <asp:TableRow>
                    <asp:TableCell>Name:</asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtName" runat="server" ></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                
                <asp:TableRow>
                    <asp:TableCell>Gender:</asp:TableCell>
                    <asp:TableCell>
                        <asp:RadioButton ID="RdMale" runat="server" GroupName="Gender" Text="Male" />
                        <asp:RadioButton ID="RdFemale" runat="server" GroupName="Gender" Text="FeMale" />
                        <asp:RadioButton ID="RdOthers" runat="server" GroupName="Gender" Text="Others" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>Role:</asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlROle" runat="server">
                            <asp:ListItem>--select--</asp:ListItem>
                            <asp:ListItem>Dev</asp:ListItem>
                            <asp:ListItem>Testing</asp:ListItem>
                            <asp:ListItem>Build</asp:ListItem>
                            <asp:ListItem>Sales</asp:ListItem>
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell>Country :</asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" >
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                           
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell>State</asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell>City</asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="ddlCity" runat="server">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            
                        </asp:DropDownList>
                    </asp:TableCell>
                </asp:TableRow>



                <asp:TableRow>
                    <asp:TableCell>Sumbit</asp:TableCell>
                    <asp:TableCell>
                        <asp:Button ID="btnSumbit"  runat="server" Text="Sumbit" OnClick="btn_Sumbit_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>

        <div>
            <asp:GridView ID="GridView1" runat ="server" AutoGenerateColumns="false" DataKeyNames="empid" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" OnRowDataBound="GridView1_RowDataBound" >
                <Columns>
                    <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate> 
                            <asp:Label ID="lbltextId" runat="server" Text='<%#Eval("empid")%>' ></asp:Label>
                            <asp:TextBox ID="textId" runat="server"  Text='<%#Eval("empid")%>' Visible="false" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="EmpName">
                        <ItemTemplate>
                            <asp:Label ID="lbltext2" runat="server" Text='<%#Eval("empName")%>' ></asp:Label>
                            <asp:TextBox ID="text2" runat="server"  Text='<%#Eval("empName") %>' Visible="false" ></asp:TextBox>
                        </ItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <asp:Label ID="lbltext3" runat="server" Text='<%#Eval("Gender")%>' ></asp:Label>
                            <asp:Textbox ID="text3" runat="server"  Text='<%#Eval("Gender") %>' Visible="false" ></asp:Textbox>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="empRole">
                        <ItemTemplate>
                            <asp:Label ID="lbltext4" runat="server" Text='<%#Eval("empRole")%>' ></asp:Label>
                            <asp:TextBox ID="text4" runat="server"  Text='<%#Eval("empRole") %>' Visible="false" ></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Country">
                        <ItemTemplate>
                            <asp:Label ID="lbltext5" runat="server" Text='<%#Eval("country")%>' ></asp:Label>
                            <asp:DropDownList ID="ddlcountry" runat="server" Visible="false"  AutoPostBack="true" OnSelectedIndexChanged="ddlcountry_SelectedIndexChanged1"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
    
                    <asp:TemplateField HeaderText="State">
                        <ItemTemplate>
                            <asp:Label ID="lbltext6" runat="server" Text='<%#Eval("state")%>' ></asp:Label>
                            <asp:DropDownList ID="ddlstate" runat="server" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged1"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="City">
                        <ItemTemplate>
                            <asp:Label ID="lbltext7" runat="server" Text='<%#Eval("city")%>' ></asp:Label>
                            <asp:DropDownList ID="ddlcity" runat="server" Visible="false" ></asp:DropDownList>
                        </ItemTemplate>
                        </asp:TemplateField>
                   
                   <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"/>
                </Columns>     

            </asp:GridView>
            <asp:Button runat="server" ID="btnadd" Text="ADD" OnClick="btnadd_Click"/>
        </div>
    </form>
</body>
</html>
