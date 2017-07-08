<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table border="1">
            <header>Registration</header>
            <tr>
                <td>Email</td>
                <td>
                    <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>

                    <br />
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="Please enter Email" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
            </tr>
            
            <tr>
                <td>
                    
                    Password
                </td>
                <td>
                    <asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxPassword" ErrorMessage="Please enter password" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                </td>
                

            </tr>
            <tr>
                <td>Confirm password</td>
                
                <td>
                    <asp:TextBox ID="TextBoxPassCfm" runat="server"></asp:TextBox>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxPassCfm" ErrorMessage="Please confirm password" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="TextBoxPassword" ControlToValidate="TextBoxPassCfm" ErrorMessage="Password do not match" ForeColor="Red"></asp:CompareValidator>
                </td>

            </tr>
            <tr>
                <td>Name of Member</td>
                <td>
                    <asp:TextBox ID="TextBoxNameOfMember" runat="server"></asp:TextBox>

                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxNameOfMember" ErrorMessage="Please enter name" ForeColor="Red"></asp:RequiredFieldValidator>

                </td>
            </tr>
            

        </table>
        <br/>
        
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" />

    </div>
    </form>
</body>
</html>
