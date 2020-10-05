<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm.Master" CodeBehind="ukm.ukm2.view.aspx.vb" Inherits="permatapintar.ukm_ukm2_view" %>


<%@ Register Src="commoncontrol/studentprofile_view.ascx" TagName="studentprofile_view"
    TagPrefix="uc1" %>
<%@ Register Src="commoncontrol/parentprofile_view.ascx" TagName="parentprofile_view"
    TagPrefix="uc3" %>
<%@ Register Src="commoncontrol/studentschool_view.ascx" TagName="studentschool_view"
    TagPrefix="uc4" %>

<%@ Register src="commoncontrol/ukm2_view.ascx" tagname="ukm2_view" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <table class="fbform">
        <tr class="fbform_header">
            <td>
                <asp:Label ID="lblBreadcrum" runat="server" Text="Carian>Carian Pelajar UKM2>Ujian UKM2" CssClass="lblBreadcrum"></asp:Label>
            </td>
        </tr>
    </table>
     <uc1:studentprofile_view ID="studentprofile_view1" runat="server" />
    &nbsp;<uc4:studentschool_view ID="studentschool_view1" runat="server" />
    &nbsp;<uc3:parentprofile_view ID="parentprofile_view1" runat="server" />
    &nbsp;<uc2:ukm2_view ID="ukm2_view1" runat="server" />
</asp:Content>
