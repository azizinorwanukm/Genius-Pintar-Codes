<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/penguruspejabat/main.Master"
    CodeBehind="ppcs.alumni.studentprofile.aspx.vb" Inherits="permatapintar.ppcs_alumni_studentprofile1" %>

<%@ Register Src="../commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ukm1_history_list.ascx" TagName="ukm1_history_list"
    TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/ukm2_history_list.ascx" TagName="ukm2_history_list"
    TagPrefix="uc3" %>
<%@ Register Src="../commoncontrol/ppcs_history_list.ascx" TagName="ppcs_history_list"
    TagPrefix="uc4" %>
<%@ Register Src="../commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc6" %>
<%@ Register Src="../commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc7" %>
<%@ Register Src="../commoncontrol/PPCS_Eval_Daily_list.ascx" TagName="PPCS_Eval_Daily_list"
    TagPrefix="uc5" %>
<%@ Register Src="../commoncontrol/PPCS_Eval_Weekly_list.ascx" TagName="PPCS_Eval_Weekly_list"
    TagPrefix="uc8" %>
<%@ Register Src="../commoncontrol/PPCS_Eval_End_list.ascx" TagName="PPCS_Eval_End_list"
    TagPrefix="uc9" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Profil Alumni PPCS" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc7:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;<uc6:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;<uc2:ukm1_history_list ID="ukm1_history_list1" runat="server" />
    &nbsp;
    <uc3:ukm2_history_list ID="ukm2_history_list1" runat="server" />
    &nbsp;<uc4:ppcs_history_list ID="ppcs_history_list1" runat="server" />
    &nbsp;
    <uc5:PPCS_Eval_Daily_list ID="PPCS_Eval_Daily_list1" runat="server" />
    &nbsp;<uc8:PPCS_Eval_Weekly_list ID="PPCS_Eval_Weekly_list1" runat="server" />
    &nbsp;<uc9:ppcs_eval_end_list id="PPCS_Eval_End_list1" runat="server" />
    &nbsp;&nbsp;
</asp:Content>
