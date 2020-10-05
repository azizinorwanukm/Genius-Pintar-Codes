<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_examStudentQuestionSetup.ascx.vb" Inherits="permatapintar.config_examStudentQuestionSetup1" %>
<style type="text/css">
    .auto-style1 {
        margin-left: 40px;
        width: 199px;
    }
    .auto-style2 {
        width: 199px;
    }
</style>
<table>
    <tr>
        <td class="auto-style1">
            <asp:Label runat="server" rowspan="10" Text ="Buat Soalan Exam "></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label runat="server" Text="Nama Ujian : " ID="Label1"></asp:Label></td>
        <td>
            <asp:TextBox ID="exam_Name" runat="server" Width="100%"></asp:TextBox>&nbsp
        </td>
    </tr>
    <!--<tr>
        <td class="auto-style2">
            <asp:Label runat="server" Text="Set Soalan : "></asp:Label>    
        </td>
        <td>
            <asp:TextBox ID="txt_quesSet" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label runat="server" Text="Jenis Soalan : "></asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="ddl_jenisSoalan" runat="server" Width="187px">
                <asp:ListItem Text="1"></asp:ListItem>
                <asp:ListItem Text="2"></asp:ListItem>
                <asp:ListItem Text="3"></asp:ListItem>
                <asp:ListItem Text="4"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>-->
    <tr>
        <td>
            <asp:label runat="server" >Tahun Soalan : </asp:label>
        </td>
        <td>
            <asp:DropDownList ID="ddlExamyear" runat ="server" Width="100%"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Label runat="server" Text="Jumlah Soalan : "></asp:Label>
        </td>
        <td>
            <asp:TextBox runat="server" ID="txt_jumlahSoalan" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Button runat="server" ID="btn_add" CssClass="fbbutton" Text="Tambah Set Soalan" Width="182px" />
            </td>
        <td>
            <asp:Button id="btn_back" runat="server" text="Back" CssClass="fbbutton" Width="189px"/>
        </td>
    </tr>
</table>