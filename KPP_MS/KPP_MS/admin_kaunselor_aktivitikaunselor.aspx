<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_aktivitikaunselor.aspx.vb" Inherits="KPP_MS.admin_kaunselor_aktivitikaunselor" %>

<%@ Register Src="~/commoncontrol/counselor_Activity.ascx" TagPrefix="uc1" TagName="counselor_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_Activity runat="server" id="counselor_Activity" />
</asp:Content>
