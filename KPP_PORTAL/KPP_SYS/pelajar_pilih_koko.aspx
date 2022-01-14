<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_pilih_koko.aspx.vb" Inherits="KPP_SYS.pelajar_pilih_koko" %>

<%@ Register Src="~/commoncontrol/student_coco_list_table.ascx" TagPrefix="uc1" TagName="student_coco_list_table" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:student_coco_list_table runat="server" id="student_coco_list_table" />
</asp:Content>
