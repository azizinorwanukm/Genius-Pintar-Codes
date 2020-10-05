<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_pelajar_kepastian_kokurikulum.aspx.vb" Inherits="KPP_MS.admin_pelajar_kepastian_kokurikulum" %>

<%@ Register Src="~/commoncontrol/student_CocurricularList.ascx" TagPrefix="uc1" TagName="student_CocurricularList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_CocurricularList runat="server" id="student_CocurricularList" />
</asp:Content>
