<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master" CodeBehind="pengkomputer.view.complete.aspx.vb" Inherits="permatapintar.pengkomputer_view_complete" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="ukm2" TagPrefix="uc1" %>
<%@ Register Src="../commoncontrol/pengkomputeran.view.01.ascx" TagName="pengkomputeran"
    TagPrefix="uc2" %>
<%@ Register Src="../commoncontrol/pengkomputeran.view.02.ascx" TagName="pengkomputeran"
    TagPrefix="uc3" %>
<%@ Register Src="../commoncontrol/pengkomputeran.view.03.ascx" TagName="pengkomputeran"
    TagPrefix="uc4" %>
<%@ Register Src="../commoncontrol/pengkomputeran.view.04.ascx" TagName="pengkomputeran"
    TagPrefix="uc5" %>
    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td>
                Soalselidik Pembudayaan ICT (View only)
            </td>
        </tr>
    </table>
    <uc1:ukm2 ID="ukm21" runat="server" />
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <b>Mukasurat 1</b>  
    <uc2:pengkomputeran ID="pengkomputeran1" runat="server" />
    <b>Mukasurat 2</b>
    <uc3:pengkomputeran ID="pengkomputeran2" runat="server" />
    <b>Mukasurat 3</b>
    <uc4:pengkomputeran ID="pengkomputeran3" runat="server" />
    <b>Mukasurat 4</b>
    <uc5:pengkomputeran ID="pengkomputeran4" runat="server" />
</asp:Content>
