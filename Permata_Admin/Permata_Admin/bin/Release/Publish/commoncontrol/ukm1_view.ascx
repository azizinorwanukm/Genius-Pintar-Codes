<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_view.ascx.vb" Inherits="permatapintar.ukm1_view" %>

<table class="fbform">
    <tr class="fbform_header">
        <td>Keputusan UKM1
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="UKM1ID"
                Width="100%" PageSize="10">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExamYear">
                        <ItemTemplate>
                            <asp:Label ID="ExamYear" runat="server" Text='<%# Bind("ExamYear") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian Mula">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian Tamat">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Bind("ExamEnd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Laman Akhir">
                        <ItemTemplate>
                            <asp:Label ID="LastPage" runat="server" Text='<%# Bind("LastPage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModA">
                        <ItemTemplate>
                            <asp:Label ID="ModA" runat="server" Text='<%# Bind("ModA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModB">
                        <ItemTemplate>
                            <asp:Label ID="ModB" runat="server" Text='<%# Bind("ModB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModC">
                        <ItemTemplate>
                            <asp:Label ID="ModC" runat="server" Text='<%# Bind("ModC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TotalScore">
                        <ItemTemplate>
                            <asp:Label ID="TotalScore" runat="server" Text='<%# Bind("TotalScore") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total%">
                        <ItemTemplate>
                            <asp:Label ID="TotalPercentage" runat="server" Text='<%# Bind("TotalPercentage","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <asp:Button ID="btnReset" runat="server" Text="Reset ExamStart" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export Detail" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnLayak" runat="server" Text="LAYAK UKM2" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnTidakLayak" runat="server" Text="TIDAK LAYAK UKM2" CssClass="fbbutton" />&nbsp;
            <asp:LinkButton ID="lnkViewProfile" runat="server">Maklumat Pelajar</asp:LinkButton>
        </td>
    </tr>

</table>
