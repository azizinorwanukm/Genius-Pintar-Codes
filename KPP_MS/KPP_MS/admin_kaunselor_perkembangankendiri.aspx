<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_kaunselor_perkembangankendiri.aspx.vb" Inherits="KPP_MS.admin_kaunselor_perkembangankendiri" %>

<%@ Register Src="~/commoncontrol/counselor_Self_Development_Mark.ascx" TagPrefix="uc1" TagName="counselor_Self_Development_Mark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:counselor_Self_Development_Mark runat="server" id="counselor_Self_Development_Mark" />

</asp:Content>
