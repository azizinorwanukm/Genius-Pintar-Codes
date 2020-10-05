<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_session_edit.ascx.vb" Inherits="permatapintar.config_sessiont_edit" %>

<table class="fbform">
    <col width="150px" />
    <tr>
        <td>Nama Session : </td>
        <td> <asp:TextBox ID="txt_namaSession" runat="server" ></asp:TextBox></td>
    </tr>
    <tr>
        <td >Tahun : </td>
        <td> <asp:Label ID="lblYear" Text="" runat="server" /></td>
    </tr>
    <tr>
        <td>
            Ujian :
        </td>
        <td><asp:DropDownList ID="ddlExams" runat="server" ></asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            PPCS Date :
        </td>
        <td><asp:DropDownList ID="ddlPpcs" runat="server" ></asp:DropDownList></td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
    </tr>
    <tr>
        <td>
            Sesi Terkini?
        </td>
        <td>
            <asp:RadioButtonList ID="sesiTerkini" runat="server" EnableViewState="False" RepeatDirection="Vertical" RepeatLayout="Flow" >
                <asp:ListItem Value="0" Text="Tidak"></asp:ListItem>
                <asp:ListItem Value="1" Text="Ya"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <div>*Hanya satu sesi sahaja boleh ditetapkan sebagai sesi terkini.</div>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button runat="server" Text="Kemaskini" id="btn_updatesession" CssClass="fbbutton" class="btn btn-primary active"/>
        </td>
        <td>
            <asp:Button runat="server" Text="Kembali" id="btn_back" CssClass="fbbutton" class="btn btn-primary active"/>
        </td>
    </tr>
</table>
<style>
div{
    color: red;
}

</style>