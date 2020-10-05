<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_daftarPenyelidikanMentor.aspx.vb" Inherits="KPP_MS.admin_daftarPenyelidikanMentor" %>

<%@ Register Src="~/commoncontrol/Student_RegisterMentor_List.ascx" TagPrefix="uc1" TagName="Student_RegisterMentor_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Student_RegisterMentor_List runat="server" id="Student_RegisterMentor_List" />
</asp:Content>
