<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_petugas_list_all.ascx.vb"
    Inherits="permatapintar.pusatujian_petugas_list_all" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Senarai Petugas
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Jenis Petugas:
        </td>
        <td>
            <select name="selUserType" id="selUserType" style="width: 200px;" runat="server">
                <option value="ALL">ALL</option>
                <option value="PEMANTAU JPN">PEMANTAU JPN</option>
                <option value="PEMANTAU KPM">PEMANTAU KPM</option>
                <option value="PEMANTAU PPD">PEMANTAU PPD</option>
                <option value="PEMANTAU UKM">PEMANTAU UKM</option>
                <option value="PENGAWAS">PENGAWAS</option>
                <option value="JURUTEKNIK">JURUTEKNIK</option>
            </select>&nbsp;
        </td>
        <td class="fbtd_left">Nama Petugas:
        </td>
        <td>
            <asp:TextBox ID="txtFullname" runat="server" Width="200px" MaxLength="250"></asp:TextBox>&nbsp;
        </td>
    </tr>
     <tr>
        <td class="fbform_sap" colspan="4"></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Petugas Pusat Ujian
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="PetugasID"
                Width="100%" PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Petugas">
                        <ItemTemplate>
                            <asp:Label ID="Fullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD#">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. Tel#">
                        <ItemTemplate>
                            <asp:Label ID="ContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="Email" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Account#">
                        <ItemTemplate>
                            <asp:Label ID="AccountNo" runat="server" Text='<%# Bind("AccountNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bank Name">
                        <ItemTemplate>
                            <asp:Label ID="BankName" runat="server" Text='<%# Bind("BankName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jenis Petugas">
                        <ItemTemplate>
                            <asp:Label ID="UserType" runat="server" Text='<%# Bind("UserType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="Pilih" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
