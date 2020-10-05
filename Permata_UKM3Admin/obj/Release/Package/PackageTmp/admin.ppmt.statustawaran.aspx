<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.ppmt.statustawaran.aspx.vb" Inherits="permatapintar.admin_ppmt_statustawaran" %>

<%@ Register Src="~/commoncontrol/ukm3_statustawaran_list.ascx" TagPrefix="uc1" TagName="ukm3_statustawaran_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ukm3_statustawaran_list runat="server" id="ukm3_statustawaran_list" />
</asp:Content>
