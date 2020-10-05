<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="instruktor.forgot.aspx.vb" Inherits="permatapintar.instruktor_forgot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Lupa Kata Laluan
            </td>
        </tr>
        <tr>
           <td class="fbtd_left">Tahun Kemasukan
            </td>
            <td>
                :<asp:DropDownList ID="ddlTahun" runat="server" AutoPostBack="false" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 80px;">
                <asp:Label ID="Label1" runat="server" Text="Login ID"></asp:Label>
            </td>
            <td>:<asp:TextBox ID="txtLoginID" runat="server" Text="" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;*<asp:Label ID="lbl15_instruction" runat="server" Text="[Email address]"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Hantar" CssClass="fbbutton" />
            </td>
        </tr>
    </table>
    <div class="info" id="divMsg" runat="server">
        <asp:Label ID="lblMsg" runat="server" Text="Medan bertanda * adalah wajib diisi."></asp:Label>
    </div>
</asp:Content>
