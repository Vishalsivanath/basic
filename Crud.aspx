<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crud.aspx.cs" Inherits="CrudOperation.Crud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <title></title>
    <style type="text/css">
        .Eu_DataTable {}
    </style>
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
            <asp:GridView ID="GridView1" runat ="server" AutoGenerateColumns="false" DataKeyNames="empid" OnRowEditing="Gridview1_RowEditing" OnRowCancelingEdit="Gridview1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" BackColor="NavajoWhite" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="ID" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate> 
                            <asp:Label ID="lbltextId" runat="server" Text='<%#Eval("empid")%>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="textId" runat="server" Text='<%#Eval("empid")%>'  ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="EmpName">
                        <ItemTemplate> 
                            <asp:Label ID="lbltext2" runat="server" Text='<%#Eval("empName")%>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="text2" runat="server"  Text='<%#Bind("empName") %>' ></asp:TextBox>
                        </EditItemTemplate>
                   </asp:TemplateField>

                    <asp:TemplateField HeaderText="Gender">
                        <ItemTemplate>
                            <asp:Label ID="lbltext3" runat="server" Text='<%#Eval("Gender")%>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Textbox ID="text3" runat="server" Text='<%#Bind("Gender") %>' ></asp:Textbox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="empRole">
                        <ItemTemplate>
                            <asp:Label ID="lbltext4" runat="server" Text='<%#Eval("empRole")%>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="text4" runat="server" Text='<%#Bind("empRole") %>' ></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Country" SortExpression="Country">
                        <ItemTemplate>
                            
                            <asp:Label ID="lbltext5" runat="server" Text='<%#Eval("country")%>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlcountry" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                        
                    </asp:TemplateField>
    
                    <asp:TemplateField HeaderText="State">
                        <ItemTemplate>
                            
                            <asp:Label ID="lbltext6" runat="server" Text='<%#Eval("state")%>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlstate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="City">
                        <ItemTemplate>
                            
                            <asp:Label ID="lbltext7" runat="server" Text='<%#Eval("city")%>' ></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlcity" runat="server"></asp:DropDownList>
                        </EditItemTemplate>
                        </asp:TemplateField>

                    <asp:CommandField ButtonType="Link" ShowEditButton="true" ShowDeleteButton="true"/>

                </Columns>     
            </asp:GridView>
        </div>
    </form>
</body>
</html>
