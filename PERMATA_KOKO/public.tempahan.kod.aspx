<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="public.tempahan.kod.aspx.vb" Inherits="permatapintar.public_tempahan_kod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kemudahan>Cetak atau Batal Tempahan
            </td>

        </tr>
    </table>
     <table class="fbform">
        <tr>
            <td style="width: 20%">Kod Tempahan:</td>
            <td>
                <asp:TextBox ID="txtKodTempahan" runat="server" Width="100px"></asp:TextBox> [<asp:Label ID="lblMsgTop" runat="server" Text="Jika anda terlupa Kod Tempahann sila hubungi Pihak Pengurusan." ForeColor="red"></asp:Label>]
                
            </td>
        </tr>
        <tr>
            <td colspan="2" class="fbform_sap"></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnView" runat="server" Text="Paparkan" CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
