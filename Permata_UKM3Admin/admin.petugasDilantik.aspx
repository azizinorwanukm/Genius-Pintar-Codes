<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.petugasDilantik.aspx.vb" Inherits="permatapintar.admin_petugasDilantik" %>

<%@ Register Src="~/commoncontrol/admin.petugas_Dilantik.ascx" TagPrefix="uc1" TagName="adminpetugas_Dilantik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:label id="label1" runat="server" CssClass="lblBreadcrum" Text ="Petugas>Senarai Petugas Dilantik"></asp:label>
            </td>
        </tr>
    </table>
    <uc1:adminpetugas_Dilantik runat="server" id="adminpetugas_Dilantik" />
</asp:Content>

