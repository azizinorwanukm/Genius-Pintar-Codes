<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pegajar_koordinator_kemaskini_markah.aspx.vb" Inherits="KPP_MS.pegajar_koordinator_kemaskini_markah" %>

<%@ Register Src="~/commoncontrol/coordinator_student_marks.ascx" TagPrefix="uc1" TagName="coordinator_student_marks" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:coordinator_student_marks runat="server" id="coordinator_student_marks" />
</asp:Content>
