<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ppcs.dobyear.list.aspx.vb" Inherits="permatapintar.admin_ppcs_dobyear_list" %>

<%@ Register Src="commoncontrol/ppcs_dobyear_list.ascx" TagName="ppcs_dobyear_list" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_dobyear_list ID="ppcs_dobyear_list1" runat="server" />
    &nbsp;
</asp:Content>
