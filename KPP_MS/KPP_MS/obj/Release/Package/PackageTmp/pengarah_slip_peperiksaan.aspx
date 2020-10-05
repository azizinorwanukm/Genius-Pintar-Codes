<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengarah.Master" CodeBehind="pengarah_slip_peperiksaan.aspx.vb" Inherits="KPP_MS.pengarah_slip_peperiksaan" %>

<%@ Register Src="~/commoncontrol/pengarah_examination_slip.ascx" TagPrefix="uc1" TagName="pengarah_examination_slip" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:pengarah_examination_slip runat="server" id="pengarah_examination_slip" />
</asp:Content>
