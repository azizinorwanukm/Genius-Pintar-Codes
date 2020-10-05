<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="ukm3_ppcs.studentupdate.aspx.vb" Inherits="permatapintar.ukm3_ppcs_studentupdate" %>

<%@ Register Src="~/commoncontrol/student_UpdatePpcs.ascx" TagPrefix="uc1" TagName="student_UpdatePpcs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_UpdatePpcs runat="server" id="student_UpdatePpcs" />
</asp:Content>
