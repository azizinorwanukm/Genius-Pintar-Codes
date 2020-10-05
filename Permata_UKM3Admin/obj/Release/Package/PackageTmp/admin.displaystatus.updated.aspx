<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin.displaystatus.updated.aspx.vb" Inherits="permatapintar.admin_displaystatus_updated" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Display Status.
            </td>
        </tr>
        <tr>
            <td class="fbtd_left">Tahun Ujian UKM2:
            </td>
            <td>
                <asp:DropDownList ID="ddlExamYearUKM2" runat="server" Width="200px">
                </asp:DropDownList>&nbsp;*Kelayakan ke UKM2
            </td>
        </tr>
        <tr>
            <td>Display Status UKM2:</td>
            <td>
                <select name="selDisplayStatusUKM2" id="selDisplayStatusUKM2" style="width: 200px;" runat="server">
                    <option value="" selected="selected">--Pilih--</option>
                    <option value="Y">Y</option>
                    <option value="N">N</option>
                </select>
            </td>
        </tr>
        <tr>
            <td>Sessi PPCS:
            </td>
            <td>
                <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
                </asp:DropDownList>&nbsp;*Kelayakan ke PPCS
            </td>
        </tr>
        <tr>
            <td>Display Status PPCS:</td>
            <td>
                <select name="selDisplayStatusPPCS" id="selDisplayStatusPPCS" style="width: 200px;" runat="server">
                    <option value="" selected="selected">--Pilih--</option>
                    <option value="Y">Y</option>
                    <option value="N">N</option>
                </select>

            </td>
        </tr>
        <tr>
            <td class="fbtd_left">Tahun Ujian UKM3:
            </td>
            <td>
                <asp:DropDownList ID="ddlExamYearUKM3" runat="server" Width="200px">
                </asp:DropDownList>&nbsp;*Kelayakan ke Kolej PERMATApintar
            </td>
        </tr>
        <tr>
            <td>Display Status UKM3:</td>
            <td>
                <select name="selDisplayStatusUKM3" id="selDisplayStatusUKM3" style="width: 200px;" runat="server">
                    <option value="" selected="selected">--Pilih--</option>
                    <option value="Y">Y</option>
                    <option value="N">N</option>
                </select>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap" colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />&nbsp;*Kemaskini akan dilakukan jika DisplayStatus dipilih.
            </td>
        </tr>

    </table>
</asp:Content>
