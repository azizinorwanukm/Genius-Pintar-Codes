<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="ukm1.intro.page01.aspx.vb" Inherits="permatapintar.ukm1_intro_page01"
    Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblintro000" runat="server" Text="PERAKUAN"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <p>
                    <asp:Label ID="lblintro001" runat="server" Text=""></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblintro002" runat="server" Text=""></asp:Label>
                </p>

                <p>
                    <asp:Label ID="lblintro003" runat="server" Text="" Font-Bold="true"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="lblSijil" runat="server" Text=""></asp:Label>
                </p>
            </td>
        </tr>
        <tr>
            <td>
                <div class="warning" id="div1" runat="server">
                    <asp:Label ID="lblintro004" runat="server" Text=""></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNo" runat="server" Text="  << Tidak Setuju  " CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnYes" runat="server" Text="  Setuju >> " CssClass="fbbutton" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lbldebug" runat="server" Text="" CssClass="labelMsg"></asp:Label>
</asp:Content>
