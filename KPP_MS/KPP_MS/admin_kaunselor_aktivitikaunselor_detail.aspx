<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_aktivitikaunselor_detail.aspx.vb" Inherits="KPP_MS.admin_kaunselor_aktivitikaunselor_detail" %>

<%@ Register Src="~/commoncontrol/counselor_Activity_Detail.ascx" TagPrefix="uc1" TagName="counselor_Activity_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:counselor_Activity_Detail runat="server" id="counselor_Activity_Detail" />
</asp:Content>
