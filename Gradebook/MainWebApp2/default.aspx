<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="MainWebApp2._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Countries<br />
        </div>
        <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
            <asp:ListItem>USA</asp:ListItem>
            <asp:ListItem>Mexico</asp:ListItem>
            <asp:ListItem>Canada</asp:ListItem>

        </asp:DropDownList>
        <p>
            <asp:Button ID="SubmitBtn" runat="server" OnClick="Button1_Click" Text="Submit" />
        </p>
        <asp:Label ID="OutputLabel" runat="server" Text="[OutputLabel]"></asp:Label>
    </form>
</body>
</html>
