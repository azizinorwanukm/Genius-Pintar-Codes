﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master"
    CodeBehind="PPCS.Eval.Weekly.create.aspx.vb" Inherits="permatapintar.PPCS_Eval_Weekly_create" %>

<%@ Register Src="commoncontrol/PPCS_Eval_Weekly_create.ascx" TagName="PPCS_Eval_Weekly_create"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/studentprofile_header.ascx" TagName="studentprofile_header"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="Label2" runat="server" Text="Maklumat Pelajar>Paparan PPCS" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
    
    <uc2:studentprofile_header ID="studentprofile_header1" runat="server" />
    &nbsp;&nbsp;<uc1:PPCS_Eval_Weekly_create ID="PPCS_Eval_Weekly_create1" runat="server" />
</asp:Content>
