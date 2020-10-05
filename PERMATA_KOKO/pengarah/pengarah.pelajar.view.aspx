<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah/pengarah.Master" CodeBehind="pengarah.pelajar.view.aspx.vb" Inherits="permatapintar.pengarah_pelajar_view" %>

<%@ Register Src="../commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ukm1_history_list.ascx" TagName="ukm1_history_list" TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/ppcs_history_list.ascx" TagName="ppcs_history_list" TagPrefix="uc4" %>
<%@ Register src="../commoncontrol/ukm2_history_list.ascx" tagname="ukm2_history_list" tagprefix="uc5" %>
<%@ Register src="../commoncontrol/koko_history_list.ascx" tagname="koko_history_list" tagprefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Paparan Maklumat Pelajar
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc2:ukm1_history_list ID="ukm1_history_list1" runat="server" />
    &nbsp;<uc5:ukm2_history_list ID="ukm2_history_list1" runat="server" />
    &nbsp;<uc4:ppcs_history_list ID="ppcs_history_list1" runat="server" />
    &nbsp;<uc6:koko_history_list ID="koko_history_list1" runat="server" />
</asp:Content>
