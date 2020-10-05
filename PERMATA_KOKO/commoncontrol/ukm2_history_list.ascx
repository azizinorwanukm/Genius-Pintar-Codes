<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_history_list.ascx.vb"
    Inherits="permatapintar.ukm2_history_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Sejarah UKM2
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
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
                    <asp:TemplateField HeaderText="ExamStart">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExamEnd">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Bind("ExamEnd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LastPage">
                        <ItemTemplate>
                            <asp:Label ID="LastPage" runat="server" Text='<%# Bind("LastPage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VCI">
                        <ItemTemplate>
                            <asp:Label ID="VCI" runat="server" Text='<%# Bind("VCI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PRI">
                        <ItemTemplate>
                            <asp:Label ID="PRI" runat="server" Text='<%# Bind("PRI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="WMI">
                        <ItemTemplate>
                            <asp:Label ID="WMI" runat="server" Text='<%# Bind("WMI") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PSI">
                        <ItemTemplate>
                            <asp:Label ID="PSI" runat="server" Text='<%# Bind("PSI") %>'></asp:Label>
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
                    <asp:TemplateField HeaderText="Mental Age">
                        <ItemTemplate>
                            <asp:Label ID="Mental_Age_Year" runat="server" Text='<%# Bind("Mental_Age_Year", "{0:n0}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IQ">
                        <ItemTemplate>
                            <asp:Label ID="Student_IQ" runat="server" Text='<%# Bind("Student_IQ", "{0:n0}")%>'></asp:Label>
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
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
