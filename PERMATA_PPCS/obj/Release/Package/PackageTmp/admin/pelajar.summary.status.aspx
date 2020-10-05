<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="pelajar.summary.status.aspx.vb" Inherits="permatapintar.pelajar_summary_status" %>

<%@ Register Src="../commoncontrol/studentprofile_header.ascx" TagName="student"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td>
                Laporan Penaksiran Akademik
            </td>
        </tr>
    </table>
    <uc1:student ID="student1" runat="server" />
    <table class="fbform">
        <tr>
            <td>
                Laporan Harian
                <asp:Label ID="lblCourse" runat="server" Text="" ForeColor="white"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ppcsevalid"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tarikh">
                            <ItemTemplate>
                                <asp:Label ID="lblTokenid" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q001">
                            <ItemTemplate>
                                <asp:Label ID="lblQ001" runat="server" Text='<%# Bind("Q001") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q002">
                            <ItemTemplate>
                                <asp:Label ID="lblQ002" runat="server" Text='<%# Bind("Q002") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q003">
                            <ItemTemplate>
                                <asp:Label ID="lblQ003" runat="server" Text='<%# Bind("Q003") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q004">
                            <ItemTemplate>
                                <asp:Label ID="lblQ004" runat="server" Text='<%# Bind("Q004") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q005">
                            <ItemTemplate>
                                <asp:Label ID="lblQ005" runat="server" Text='<%# Bind("Q005") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q006">
                            <ItemTemplate>
                                <asp:Label ID="lblQ006" runat="server" Text='<%# Bind("Q006") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q007">
                            <ItemTemplate>
                                <asp:Label ID="lblQ007" runat="server" Text='<%# Bind("Q007") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q008">
                            <ItemTemplate>
                                <asp:Label ID="lblQ008" runat="server" Text='<%# Bind("Q008") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q009">
                            <ItemTemplate>
                                <asp:Label ID="lblQ009" runat="server" Text='<%# Bind("Q009") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q010">
                            <ItemTemplate>
                                <asp:Label ID="lblQ010" runat="server" Text='<%# Bind("Q010") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TotalMark">
                            <ItemTemplate>
                                <asp:Label ID="lblTotalMark" runat="server" Text='<%# Bind("TotalMark") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField SelectText="Pilih" ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr>
            <td>
                Laporan Mingguan
                <asp:Label ID="Label1" runat="server" Text="" ForeColor="white"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datMingguan" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="5" DataKeyNames="ppcsevalweekid"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lableIndexMingguan" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tarikh">
                            <ItemTemplate>
                                <asp:Label ID="lblTokenid" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Minggu 01">
                            <ItemTemplate>
                                <asp:Label ID="lblQ001Remarks" runat="server" Text='<%# Bind("Q001Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Minggu 02">
                            <ItemTemplate>
                                <asp:Label ID="lblQ002Remarks" runat="server" Text='<%# Bind("Q002Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Minggu 03">
                            <ItemTemplate>
                                <asp:Label ID="lblQ003Remarks" runat="server" Text='<%# Bind("Q003Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr>
            <td>
                Laporan Akhir
                <asp:Label ID="Label2" runat="server" Text="" ForeColor="white"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datEnd" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="5" DataKeyNames="ppcsevalendid"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lableIndexEnd" runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tarikh">
                            <ItemTemplate>
                                <asp:Label ID="lblTokenid" runat="server" Text='<%# Bind("DateCreated") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Q001">
                            <ItemTemplate>
                                <asp:Label ID="lblQ001" runat="server" Text='<%# Bind("Q001") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ringkasan">
                            <ItemTemplate>
                                <asp:Label ID="lblQ001Remarks" runat="server" Text='<%# Bind("Q001Remarks") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
