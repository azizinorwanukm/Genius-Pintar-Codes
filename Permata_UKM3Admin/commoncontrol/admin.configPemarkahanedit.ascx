<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin.configPemarkahanedit.ascx.vb" Inherits="permatapintar.admin_configPemarkahanedit" %>


<table>
    <tr>
    <td colspan ="6"><label>Kemasukan Format Permarkahan</label></td>
    </tr>
    <tr>
        <td>
            STEM
        </td>
        <td> :</td>
        <td><asp:TextBox ID="txtMarkStem" runat ="server" text="0"></asp:TextBox></td>
        <td></td>
        <td>UKM2</td>
        <td>:</td>
        <td><asp:TextBox runat ="server" id="txtUKM2" text="0"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            EQ
        </td>
        <td> :</td>
        <td><asp:TextBox ID="txtMarkEQ" runat ="server" text="0"></asp:TextBox></td>
         <td></td>
        <td>Post Test</td>
        <td>:</td>
        <td><asp:TextBox runat ="server" id="txtPostTest" text="0"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Instruktor KPP
        </td>
        <td> :</td>
        <td><asp:TextBox ID="txtMarkKPP" runat ="server" text="0"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Instruktor PPCS
        </td>
        <td> :</td>
        <td><asp:TextBox ID="txtMarkPPCS" runat ="server" text="0"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            Instruktor RA PPCS
        </td>
        <td> :</td>
        <td><asp:TextBox ID="txtMarkRAPPCS" runat ="server" text="0"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label runat="server" ID="lblmsgSubmit"></asp:Label>
        </td>
    </tr>
    <tr>
        <td><asp:Button ID ="btnSubmitMark" runat ="server" Text="Submit" /> </td>
        <td><asp:Button ID="btnBack" runat="server" Text="Kembali" /></td>
    </tr>
</table>