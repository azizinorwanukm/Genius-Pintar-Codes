<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.master" CodeBehind="public.kemudahan.list.aspx.vb" Inherits="permatapintar.public_kemudahan_list" %>

<%@ Register src="commoncontrol/kemudahan_list.ascx" tagname="kemudahan_list" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_bread">
            <td>Kemudahan>Tempahan>Senarai Kemudahan
            </td>
        </tr>
    </table>
    <uc1:kemudahan_list ID="kemudahan_list1" runat="server" />
&nbsp;
</asp:Content>
