<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ubahmarkah.aspx.vb" Inherits="permatapintar.kpp_ubahmarkah" %>

<%@ Register Src="~/commoncontrol/admin.marktableupdate.ascx" TagPrefix="uc1" TagName="marktable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Markah Keseluruhan>Paparan Markah Keseluruhan"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:marktable runat="server" id="marktable" />
</asp:Content>
