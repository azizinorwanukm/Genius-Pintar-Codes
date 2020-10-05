<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_peperiksaan_kemaskini_markah.aspx.vb" Inherits="KPP_MS.admin_peperiksaan_kemaskini_markah" %>
<%@ Register Src="~/commoncontrol/student_marks.ascx" TagPrefix="uc1" TagName="student_marks" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_marks runat="server" ID="student_marks" />
</asp:Content>
