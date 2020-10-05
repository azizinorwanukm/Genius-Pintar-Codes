<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="tempahan_create.ascx.vb" Inherits="permatapintar.tempahan_create" %>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="fbform">
            <tr class="fbform_header">
                <td colspan="2">Tempahan
                </td>
            </tr>
            <tr>
                <td class="fbtd_left">Tahun:
                </td>
                <td>
                    <asp:Label ID="lblTahun" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>Nama Kemudahan:
                </td>
                <td>
                    <asp:Label ID="lblKemudahan" runat="server" Text=""></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="vertical-align: top;">Tarikh Tempahan:
                </td>
                <td>
                    <asp:TextBox ID="txtBookingDate" runat="server" Width="150px" MaxLength="250"></asp:TextBox>*&nbsp;
            <asp:ImageButton ID="btnDate" runat="server" ImageUrl="~/img/department-store-emoticon.png" AlternateText=".." Width="15" Height="15" />
                    <asp:Calendar ID="calStartDate" runat="server" Visible="false" BackColor="White"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top;">Masa:</td>
                <td>AM:<asp:CheckBox ID="Time07" Text="07:00-08:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time08" Text="08:00-09:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time09" Text="09:00-10:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time10" Text="10:00-11:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time11" Text="11:00-12:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time12" Text="12:00-01:00" runat="server" AutoPostBack="true" /><br />
                    PM:<asp:CheckBox ID="Time13" Text="01:00-02:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time14" Text="02:00-03:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time15" Text="03:00-04:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time16" Text="04:00-05:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time17" Text="05:00-06-00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time18" Text="06:00-07:00" runat="server" AutoPostBack="true" /><br />
                    PM:<asp:CheckBox ID="Time19" Text="07:00-08:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time20" Text="08:00-09:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time21" Text="09:00-10:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time22" Text="10:00-11:00" runat="server" AutoPostBack="true" /><asp:CheckBox ID="Time23" Text="11:00-12:00" runat="server" AutoPostBack="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="fbform_sap"></td>
            </tr>
            <tr>
                <td style="vertical-align: top;">Nama Pemohon:
                </td>
                <td>
                    <asp:TextBox ID="txtPemohon" runat="server" Width="450px" MaxLength="50"></asp:TextBox>*&nbsp;
                </td>
            </tr>
            <tr>
                <td>Tel#:</td>
                <td>
                    <asp:TextBox ID="txtContactNo" runat="server" Width="450px" MaxLength="50"></asp:TextBox>*&nbsp;</td>
            </tr>
            <tr style="vertical-align: top;">
                <td>Catatan:
                </td>
                <td>
                    <asp:TextBox ID="txtCatatan" runat="server" Width="450px" MaxLength="255"></asp:TextBox>*&nbsp;
                </td>
            </tr>
            <tr>
                <td>Status:</td>
                <td>
                    <asp:Label ID="lblStatusTempahan" runat="server" Text="POHON"></asp:Label></td>
            </tr>
            <tr>
                <td>Kod Tempahan:</td>
                <td>
                    <asp:Label ID="lblKodTempahan" runat="server" Text="" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td></td>
                <td class="fbform_sap">&nbsp;</td>
            </tr>
            <tr>
                <td class="column_width">&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnadd" runat="server" Text=" Tempah " CssClass="fbbutton" />
                    &nbsp;|&nbsp;<asp:LinkButton ID="lnkList" runat="server">Senarai Kemudahan</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="column_width"></td>
                <td>
                    <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
                </td>
            </tr>
        </table>

        <asp:Panel ID="pnlDisplay" runat="server" Visible="false">
            <asp:CheckBox ID="CheckBox07" Text="07:00" runat="server" />
            <asp:CheckBox ID="CheckBox08" Text="08:00" runat="server" />
            <asp:CheckBox ID="CheckBox09" Text="09:00" runat="server" />
            <asp:CheckBox ID="CheckBox10" Text="10:00" runat="server" />
            <asp:CheckBox ID="CheckBox11" Text="11:00" runat="server" />
            <asp:CheckBox ID="CheckBox12" Text="12:00" runat="server" />
            <br />
            <asp:CheckBox ID="CheckBox13" Text="01:00" runat="server" />
            <asp:CheckBox ID="CheckBox14" Text="02:00" runat="server" />
            <asp:CheckBox ID="CheckBox15" Text="03:00" runat="server" />
            <asp:CheckBox ID="CheckBox16" Text="04:00" runat="server" />
            <asp:CheckBox ID="CheckBox17" Text="05:00" runat="server" />
            <asp:CheckBox ID="CheckBox18" Text="06:00" runat="server" />
            <asp:CheckBox ID="CheckBox19" Text="07:00" runat="server" />
            <asp:CheckBox ID="CheckBox20" Text="08:00" runat="server" />
            <asp:CheckBox ID="CheckBox21" Text="09:00" runat="server" />
            <asp:CheckBox ID="CheckBox22" Text="10:00" runat="server" />
            <asp:CheckBox ID="CheckBox23" Text="11:00" runat="server" />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
