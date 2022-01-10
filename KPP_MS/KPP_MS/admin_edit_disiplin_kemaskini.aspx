<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_edit_disiplin_kemaskini.aspx.vb" Inherits="KPP_MS.admin_edit_disiplin_kemaskini" %>

<%@ Register Src="~/commoncontrol/Disiplin_Update_Detail.ascx" TagPrefix="uc1" TagName="Disiplin_Update_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:Disiplin_Update_Detail runat="server" id="Disiplin_Update_Detail" />
</asp:Content>
