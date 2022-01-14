<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_kaunseling.aspx.vb" Inherits="KPP_SYS.pelajar_kaunseling" %>

<%@ Register Src="~/commoncontrol/student_counselling_list.ascx" TagPrefix="uc1" TagName="student_counselling_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_counselling_list runat="server" id="student_counselling_list" />
</asp:Content>
