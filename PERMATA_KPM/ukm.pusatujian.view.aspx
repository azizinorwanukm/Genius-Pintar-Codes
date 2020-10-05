<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master"
    CodeBehind="ukm.pusatujian.view.aspx.vb" Inherits="permatapintar.ukm_pusatujian_view1" %>

<%@ Register Src="commoncontrol/pusatujian_view.ascx" TagName="pusatujian_view" TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/pusatujian_student_list_view.ascx" TagName="pusatujian_student_list_view" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Pusat Ujian UKM2>Senarai Pusat Ujian>Paparan Maklumat Pusat Ujian" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:pusatujian_view ID="pusatujian_view1" runat="server" />
    &nbsp;<uc2:pusatujian_student_list_view ID="pusatujian_student_list_view1" runat="server" />
</asp:Content>
