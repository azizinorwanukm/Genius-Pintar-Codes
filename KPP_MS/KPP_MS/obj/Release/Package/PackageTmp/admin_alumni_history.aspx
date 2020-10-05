<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_alumni_history.aspx.vb" Inherits="KPP_MS.admin_alumni_history" %>

<%@ Register Src="~/commoncontrol/àlumni_History.ascx" TagPrefix="uc1" TagName="àlumni_History" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:àlumni_History runat="server" id="àlumni_History" />
</asp:Content>
