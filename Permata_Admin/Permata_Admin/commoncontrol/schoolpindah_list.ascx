<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="schoolpindah_list.ascx.vb" Inherits="permatapintar.schoolpindah_list" %>

<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Sekolah Untuk Dipindahkan.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="SchoolID"
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
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="SchoolState" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPD">
                        <ItemTemplate>
                            <asp:Label ID="SchoolPPD" runat="server" Text='<%# Bind("SchoolPPD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar">
                        <ItemTemplate>
                            <asp:Label ID="SchoolCity" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="SchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod">
                        <ItemTemplate>
                            <asp:Label ID="SchoolCode" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lokasi">
                        <ItemTemplate>
                            <asp:Label ID="SchoolLokasi" runat="server" Text='<%# Bind("SchoolLokasi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
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
            <asp:Button ID="btnCancel" runat="server" Text="Batal" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnPindah" runat="server" Text="Pindah Sekolah" CssClass="fbbutton" />&nbsp;
            <asp:CheckBox ID="chkDelete" runat="server" Text="Tanda IsDeleted=Y selepas pindah" />

        </td>
    </tr>
</table>
*Nota: Ujian UKM1 dan UKM2 tahun semasa akan dipindahkan ke sekolah baru. Tahun Ujian:<asp:Label ID="lblExamYear" runat="server" Text=""></asp:Label><br />
StudentSchool - UPDATED<br />
UKM1 - UPDATED<br />
UKM2 - UPDATED<br />
SchoolProfile - IsDeleted='Y' if checked<br />
