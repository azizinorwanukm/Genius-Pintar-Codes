<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="addCourse.aspx.vb" Inherits="permatapintar.addCourse" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td>Pengurusan Am>Pengurusan Kursus
            </td>
        </tr>
    </table>
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Tambah Kursus Baru
            </td>
        </tr>
        <tr>
            <td class="fbtd_left">Sessi PPCS:
            </td>
            <td>
                <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Kod Kursus:
            </td>
            <td>
                <asp:TextBox ID="txtCourseCode" runat="server" Width="200px" MaxLength="50"></asp:TextBox>&nbsp;*
            </td>
        </tr>
        <tr>
            <td>Nama Kursus (BM):
            </td>
            <td>
                <asp:TextBox ID="txtCourseNameBM" runat="server" Width="350px" MaxLength="150"></asp:TextBox>&nbsp;*
            </td>
        </tr>
        <tr>
            <td>Nama Kursus (BI):
            </td>
            <td>
                <asp:TextBox ID="txtCourseNameBI" runat="server" Width="350px" MaxLength="150"></asp:TextBox>&nbsp;*
            </td>
        </tr>
        <tr>
            <td>Jenis :
            </td>
            <td>
                <select name="selCourseType" id="selCourseType" runat="server" style="width: 200px;">
                    <option value="PRIMARY">PRIMARY</option>
                    <option value="SECONDARY">SECONDARY</option>
                </select>&nbsp;*
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
            <td>&nbsp;
            </td>
            <td>
                <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
                &nbsp;
                <asp:Button ID="btnRefresh" runat="server" Text=" Refresh " CssClass="fbbutton" />
            </td>
        </tr>

    </table>
    <br />
    <table class="fbform">
        <tr class="fbform_header">
            <td>Senarai Kursus
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="CourseID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Kursus">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseCode" runat="server" Text='<%# Bind("CourseCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Kursus (BM)">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseNameBM" runat="server" Text='<%# Bind("CourseNameBM") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Kursus (BI)">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseNameBI" runat="server" Text='<%# Bind("CourseNameBI") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Jenis">
                            <ItemTemplate>
                                <asp:Label ID="CourseType" runat="server" Text='<%# Bind("CourseType") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sessi PPCS">
                            <ItemTemplate>
                                <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate") %>'></asp:Label>
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
        <tr class="fbform_msg">
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</asp:Content>
