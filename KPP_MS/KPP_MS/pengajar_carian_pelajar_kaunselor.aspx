<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengajar.Master" CodeBehind="pengajar_carian_pelajar_kaunselor.aspx.vb" Inherits="KPP_MS.pengajar_carian_pelajar_kaunselor" %>

<%@ Register Src="~/commoncontrol/lecturer_counselor_List_Table.ascx" TagPrefix="uc1" TagName="lecturer_counselor_List_Table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:lecturer_counselor_List_Table runat="server" id="lecturer_counselor_List_Table" />
</asp:Content>
