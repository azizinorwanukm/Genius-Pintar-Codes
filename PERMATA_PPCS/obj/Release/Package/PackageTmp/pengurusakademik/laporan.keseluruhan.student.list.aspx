<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pengurusakademik/main.Master" CodeBehind="laporan.keseluruhan.student.list.aspx.vb" Inherits="permatapintar.laporan_keseluruhan_student_list6" %>

<%@ Register Src="../commoncontrol/ppcs_class_view.ascx" TagName="ppcs_class_view"
    TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/ppcs_list_classid.ascx" TagName="ppcs_list_classid"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ppcs_class_view ID="ppcs_class_view1" runat="server" />
    &nbsp;<uc2:ppcs_list_classid ID="ppcs_list_classid1" runat="server" />
    &nbsp;
</asp:Content>
