<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="admin.studentprofile.view.aspx.vb" Inherits="permatapintar.admin_studentprofile_view" %>

<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/StudentUni_list.ascx" TagName="StudentUni_list" TagPrefix="uc2" %>
<%--<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc4" %>
<%@ Register Src="commoncontrol/studentuni_view.ascx" TagName="studentuni_view" TagPrefix="uc2" %>--%>
<%@ Register Src="commoncontrol/ukm1_history_list.ascx" TagName="ukm1_history_list"
    TagPrefix="uc5" %>
<%@ Register Src="commoncontrol/ukm2_history_list.ascx" TagName="ukm2_history_list"
    TagPrefix="uc6" %>
<%@ Register Src="commoncontrol/ppcs_history_list.ascx" TagName="ppcs_history_list"
    TagPrefix="uc7" %>

<%@ Register Src="commoncontrol/studentschool_list.ascx" TagName="studentschool_list" TagPrefix="uc8" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    <%-- &nbsp;<uc4:studentschool_view ID="studentschool_view1" runat="server" />--%>
    &nbsp;<uc8:studentschool_list ID="studentschool_list1" runat="server" />
    <%--&nbsp;<uc2:studentuni_view ID="studentuni_view1" runat="server" />--%>
    &nbsp;<uc2:StudentUni_list ID="StudentUni_list1" runat="server" />
    &nbsp;<uc5:ukm1_history_list ID="ukm1_history_list1" runat="server" />
    &nbsp;<uc6:ukm2_history_list ID="ukm2_history_list1" runat="server" />
    &nbsp;<uc7:ppcs_history_list ID="ppcs_history_list1" runat="server" />
    &nbsp;
    
   <table class="fbform">
       <tr class="fbform_header">
           <td colspan="4">Carian.
           </td>
       </tr>
       <tr>
           <td class="fbtd_left">Tahun Ujian:
           </td>
           <td>
               <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="250px">
               </asp:DropDownList>
           </td>
           <td>
               <asp:DropDownList ID="ddlMenudesc" runat="server" Width="250px"></asp:DropDownList>
           </td>
           <td>
               <asp:Button ID="btnExecute" runat="server" Text="Execute" CssClass="fbbutton" />&nbsp;</td>
       </tr>
       <tr>
           <td>Sessi PPCS:
           </td>
           <td>
               <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="250px">
               </asp:DropDownList>
           </td>
           <td>
               <asp:DropDownList ID="ddlPPCSStatus" runat="server" AutoPostBack="false" Width="250px">
               </asp:DropDownList>
           </td>
           <td>
               <asp:Button ID="btnPPCS" runat="server" Text="Execute" CssClass="fbbutton" />
           </td>
       </tr>
       <tr>
           <td colspan="4">UKM1 Reset ExamStart:ExamStart=NULL,ExamEnd=NULL,Status='NEW',LastPage=NULL<br />
               UKM1 Delete:DELETE UKM1<br />
               UKM1 Export:UKM1 and StudentProfile Exported<br />
               UKM2 Reset ExamStart:ExamStart=NULL,ExamEnd=NULL,Status='NEW',LastPage=NULL,IsHadir='Y'<br />
               UKM2 Delete:DELETE UKM2->INSERT NEW UKM2<br />
               UKM2 Layak:INSERT NEW UKM2<br />
               UKM2 Tidak Layak:DELETE UKM2<br />
               UKM2 Hadir:IsHadir='Y'<br />
               UKM2 Tidak Hadir:IsHadir='N'<br />
               UKM2 Logout:IsHadir='Y',IsLogin='N'<br />
               Delete Profile:DELETE StudentProfile,StudentPhoto,ParentProfile,StudentSchool,UKM1,UKM2<br />
           </td>
       </tr>
       <tr class="fbform_msg">
           <td colspan="4">
               <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
       </tr>
   </table>
    <br />
    <br />
    <table class="fbform">
        <tr>
            <td>
                <select name="flag" id="flaging" style="width: 200px;" runat="server"></select>
                <asp:Button ID="Simpanflag" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
            </td>
                
        </tr>
    </table>

</asp:Content>
