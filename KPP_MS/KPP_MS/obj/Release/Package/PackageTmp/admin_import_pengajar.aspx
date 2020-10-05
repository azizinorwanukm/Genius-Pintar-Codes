<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_import_pengajar.aspx.vb" Inherits="KPP_MS.admin_import_pengajar" %>

<%@ Register Src="~/commoncontrol/import_staff.ascx" TagPrefix="uc1" TagName="import_staff" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:import_staff runat="server" id="import_staff" />
</asp:Content>
