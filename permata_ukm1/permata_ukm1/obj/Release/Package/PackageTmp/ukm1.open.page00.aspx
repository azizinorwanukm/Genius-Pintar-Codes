<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="ukm1.open.page00.aspx.vb" Inherits="permatapintar.ukm1_open_page00" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbbluebox" width="100%" border="0px">
        <tr>
            <td>
                <asp:Label ID="lblOpen" runat="server" Text="*Sila terangkan maksud bagi setiap perkataan di bawah. Jika anda tidak tahu jawabannya, masukkan 'x'"
                    CssClass="fbinstruction"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblQ1" runat="server" Text="" CssClass="lblQuestion"></asp:Label>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQ1"
                    ErrorMessage="*"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtQ1" runat="server" Width="600px" MaxLength="500" TextMode="MultiLine"
                    Rows="3" CssClass="txtOpen"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="fbbutton" />&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
