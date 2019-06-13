<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tenant.aspx.cs" Inherits="SE.Tenant" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
<table border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            Tenant:
        </td>
        <td>
            <asp:TextBox ID="txtTenant" runat="server" Text="" />
        </td>
    </tr>
    <tr>
        <td>
            Int. Cat.:
        </td>
        <td>
            <asp:TextBox ID="txtIntCat" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            uid</td>
        <td>
            <asp:TextBox ID="txtUserID" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            password</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
        </td>
    </tr>
    <tr>
        <td>
            Default Flag</td>
        <td>
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>No</asp:ListItem>
                <asp:ListItem>Yes</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
            <asp:Button ID="btnSubmit" OnClick="Submit" Text="Submit" runat="server" />
        </td>
    </tr>
</table>
<hr />
<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" HeaderStyle-BackColor="#3AC0F2"
    HeaderStyle-ForeColor="White" RowStyle-BackColor="#A1DCF2" OnRowDataBound = "OnRowDataBound">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Id" />
        <asp:BoundField DataField="Tenant" HeaderText="Tenant" />
        <asp:BoundField DataField="IntCat" HeaderText="IntCat" />
        <asp:BoundField DataField="uid" HeaderText="uid" />
        <asp:BoundField DataField="pwd" HeaderText="Encrypted Password" />
        <asp:BoundField DataField="dfi" HeaderText="dfi" />

        
    </Columns>
</asp:GridView>

        </div>
    </form>
</body>
</html>
