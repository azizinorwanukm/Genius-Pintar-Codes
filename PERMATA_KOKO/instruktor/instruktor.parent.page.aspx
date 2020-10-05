<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/instruktor/instruktor.Master" CodeBehind="instruktor.parent.page.aspx.vb" Inherits="permatapintar.instruktor_parent_page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenPopup(pageURL, title, w, h) {
            var left = (screen.width - w) / 2;
            var top = (screen.height - h) / 4;  // for 25% - devide by 4  |  for 33% - devide by 3
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Kemaskini Maklumat Instruktor
            </td>
        </tr>

        <tr>
            <td class="fbtd_left">Nama Penuh:
            </td>
            <td>
                <asp:textbox id="txtFullname" runat="server" width="400px" maxlength="250"></asp:textbox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>MYKAD#:
            </td>
            <td>
                <asp:textbox id="txtMYKAD" runat="server" width="400px" maxlength="254"></asp:textbox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>No. Telefon:
            </td>
            <td>
                <asp:textbox id="txtContactNo" runat="server" width="400px" maxlength="254"></asp:textbox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>Email:
            </td>
            <td>
                <asp:textbox id="txtEmail" runat="server" width="400px" maxlength="254"></asp:textbox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Alamat:
            </td>
            <td>
                <asp:textbox id="txtAddress1" runat="server" width="400px" maxlength="254"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;"></td>
            <td>
                <asp:textbox id="txtAddress2" runat="server" width="400px" maxlength="254"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Poskod:
            </td>
            <td>
                <asp:textbox id="txtPostcode" runat="server" width="400px" maxlength="254"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Bandar:
            </td>
            <td>
                <asp:textbox id="txtCity" runat="server" width="400px" maxlength="254"></asp:textbox>
                <asp:button id="btnCity" runat="server" text="..." />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Negeri:
            </td>
            <td>
                <asp:dropdownlist id="ddlState" autopostback="false" runat="server" width="400px">
            </asp:dropdownlist>
            </td>
        </tr>
        <tr class="fbform_header">
            <td colspan="2">Maklumat Login
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Login ID:
            </td>
            <td>
                <asp:textbox id="txtLoginID" runat="server" width="400px" maxlength="254" readonly="true" cssclass="fbreadonly"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Kata Laluan:
            </td>
            <td>
                <asp:textbox id="txtPwd" runat="server" width="400px" maxlength="254"></asp:textbox>
            </td>
        </tr>
        <tr class="fbform_header">
            <td colspan="2">Maklumat Kewangan
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Nama Bank:
            </td>
            <td>
                <asp:textbox id="txtBankName" runat="server" width="400px" maxlength="254"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Akaun#:
            </td>
            <td>
                <asp:textbox id="txtAcctNo" runat="server" width="400px" maxlength="254"></asp:textbox>
            </td>
        </tr>
        <tr class="fbform_header">
            <td colspan="2">Maklumat Kokurikulum
            </td>
        </tr>
        <tr>
            <td>Tahun:</td>
            <td>
                <asp:textbox id="txtTahun" runat="server" width="400px" maxlength="254" readonly="true" cssclass="fbreadonly"></asp:textbox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Kelas:
            </td>
            <td>
                <asp:dropdownlist id="ddlKelas" autopostback="false" runat="server" width="400px" enabled="false" cssclass="fbreadonly">
            </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Badan Beruniform:
            </td>
            <td>
                <asp:dropdownlist id="ddlUniform" autopostback="false" runat="server" width="400px" enabled="false" cssclass="fbreadonly">
            </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Kelab & Persatuan:
            </td>
            <td>
                <asp:dropdownlist id="ddlPersatuan" autopostback="false" runat="server" width="400px" enabled="false" cssclass="fbreadonly">
            </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Sukan & Permainan:
            </td>
            <td>
                <asp:dropdownlist id="ddlSukan" autopostback="false" runat="server" width="400px" enabled="false" cssclass="fbreadonly">
            </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">Rumah Sukan:
            </td>
            <td>
                <asp:dropdownlist id="ddlRumahsukan" autopostback="false" runat="server" width="400px" enabled="false" cssclass="fbreadonly">
            </asp:dropdownlist>
            </td>
        </tr>
        <tr>
            <td></td>
            <td class="fbform_sap">&nbsp;
            </td>
        </tr>
        <tr>
            <td class="column_width">&nbsp;
            </td>
            <td>
                <asp:button id="btnUpdate" runat="server" text=" Kemaskini " cssclass="fbbutton" />
                &nbsp;<asp:linkbutton id="lnkList" runat="server" visible="false">|Senarai Instruktor</asp:linkbutton>
            </td>
        </tr>
        <tr>
            <td class="column_width"></td>
            <td>
                <asp:label id="lblMsg" runat="server" text="" forecolor="red"></asp:label>
            </td>
        </tr>
    </table>
    <asp:label id="lblMYKADOrg" runat="server" text="" forecolor="red" visible="false"></asp:label>
</asp:Content>
