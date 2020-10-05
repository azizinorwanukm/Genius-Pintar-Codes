<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/popup.master" CodeBehind="public.ukm1.dobyear.summary.aspx.vb" Inherits="permatapintar.public_ukm1_dobyear_summary" %>

<%@ Register Src="commoncontrol/ukm1_dobyear_summary.ascx" TagName="ukm1_dobyear_summary" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    &nbsp;<uc1:ukm1_dobyear_summary ID="ukm1_dobyear_summary1" runat="server" />
</asp:Content>
