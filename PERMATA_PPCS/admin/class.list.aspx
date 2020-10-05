<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="class.list.aspx.vb" Inherits="permatapintar.class_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Pengurusan Am>Menentukan
                <asp:Label ID="lblUserType" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Pilih Kelas
            </td>
        </tr>
        <tr>
            <td class="fbtd_left">Sessi PPCS:</td>
            <td>
                <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
        <td>&nbsp;
        </td>
        <td class="fbaside_sap">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
        </td>
    </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="btnRefresh" runat="server" Text=" Refresh " CssClass="fbbutton" /></td>
        </tr>
    </table>
    <br />
    <table class="fbform">
        <tr class="fbform_header">
            <td>Senarai Kelas
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="ClassID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kursus">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseNameBM" runat="server" Text='<%# Bind("CourseNameBM") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Kelas">
                            <ItemTemplate>
                                <asp:Label ID="lblClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Kelas">
                            <ItemTemplate>
                                <asp:Label ID="lblClassNameBM" runat="server" Text='<%# Bind("ClassNameBM") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pengajar">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaPengajar" runat="server" Text='<%# Bind("NamaPengajar") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pembantu Pengajar">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaPembantuPengajar" runat="server" Text='<%# Bind("NamaPembantuPengajar") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%--                        <asp:TemplateField HeaderText="Pengurus Pelajar">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaPengurusPelajar" runat="server" Text='<%# Bind("NamaPengurusPelajar") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        --%>
                        <asp:TemplateField HeaderText="Pembantu Pelajar">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaPembantuPelajar" runat="server" Text='<%# Bind("NamaPembantuPelajar") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField SelectText="Pilih " ShowSelectButton="True" />
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
        <tr class="fbform_msg">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</asp:Content>
