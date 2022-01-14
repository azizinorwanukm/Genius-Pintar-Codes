<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_kaunseling_detail.aspx.vb" Inherits="KPP_SYS.pelajar_kaunseling_detail" %>

<%@ Register Src="~/commoncontrol/student_counselling_detail.ascx" TagPrefix="uc1" TagName="student_counselling_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_counselling_detail runat="server" id="student_counselling_detail" />
</asp:Content>
