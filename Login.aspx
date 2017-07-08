<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        
        <p>Login</p>
      
  
        <p>Email:<asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>

        </p>
        <p>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Please enter email" ForeColor="Red"></asp:RequiredFieldValidator>

        </p>
        

        <p>
            Password:<asp:TextBox ID="TextBoxPass" runat="server"></asp:TextBox>

        </p>
        <p>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPass" ErrorMessage="Please enter password" ForeColor="Red"></asp:RequiredFieldValidator>

        </p>
        

        <asp:Button ID="ButtonSubmit" runat="server" Text="Login" OnClick="ButtonSubmit_Click" />
        

    <br/>
        
        <asp:Label ID="lbl_Error" runat="server" Text=" "></asp:Label><br/>
        
        

        <asp:Label ID="LabelWelcome" runat="server" ></asp:Label>

    </div>
    </form>
</body>
</html>
