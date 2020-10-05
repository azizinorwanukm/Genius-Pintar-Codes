<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kemaskini_markah_koko.aspx.vb" Inherits="KPP_MS.admin_kemaskini_markah_koko" %>

<%@ Register Src="~/commoncontrol/Student_marks_koko.ascx" TagPrefix="uc1" TagName="Student_marks_koko" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Student_marks_koko runat="server" ID="Student_marks_koko" />
</asp:Content>
