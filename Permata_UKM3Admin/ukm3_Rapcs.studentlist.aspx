<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_Rapcs.studentlist.aspx.vb" Inherits="permatapintar.ukm3_Rapcs_studentlist" %>

<%@ Register Src="~/commoncontrol/student_searchRapcs.ascx" TagPrefix="uc1" TagName="student_searchRapcs" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:student_searchRapcs runat="server" id="student_searchRapcs" />

</asp:Content>
