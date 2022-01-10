<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_aktivitikaunselor_view.aspx.vb" Inherits="KPP_MS.admin_kaunselor_aktivitikaunselor_view" %>

<%@ Register Src="~/commoncontrol/counselor_Activity_View.ascx" TagPrefix="uc1" TagName="counselor_Activity_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_Activity_View runat="server" id="counselor_Activity_View" />
</asp:Content>
