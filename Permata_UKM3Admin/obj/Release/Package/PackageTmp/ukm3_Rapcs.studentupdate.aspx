<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_Rapcs.studentupdate.aspx.vb" Inherits="permatapintar.ukm3_Rapcs_studentupdate" %>

<%@ Register Src="~/commoncontrol/student_updateRapcs.ascx" TagPrefix="uc1" TagName="student_updateRapcs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:student_updateRapcs runat="server" id="student_updateRapcs" />
</asp:Content>
