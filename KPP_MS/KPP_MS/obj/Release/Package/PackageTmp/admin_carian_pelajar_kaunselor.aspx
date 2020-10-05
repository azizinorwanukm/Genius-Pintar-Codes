<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_carian_pelajar_kaunselor.aspx.vb" Inherits="KPP_MS.admin_carian_pelajar_kaunselor" %>

<%@ Register Src="~/commoncontrol/counselor_List_Table.ascx" TagPrefix="uc1" TagName="counselor_List_Table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_List_Table runat="server" id="counselor_List_Table" />
</asp:Content>
