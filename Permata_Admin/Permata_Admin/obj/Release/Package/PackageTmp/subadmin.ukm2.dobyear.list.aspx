<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/subadmin.Master" CodeBehind="subadmin.ukm2.dobyear.list.aspx.vb" Inherits="permatapintar.subadmin_ukm2_dobyear_list" %>
<%@ Register src="commoncontrol/ukm2_dobyear_list.ascx" tagname="ukm2_dobyear_list" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm2_dobyear_list ID="ukm2_dobyear_list1" runat="server" />
</asp:Content>
