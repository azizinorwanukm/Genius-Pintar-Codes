<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentschool_select.ascx.vb" Inherits="permatapintar.studentschool_select" %>
<table class="fbform">
        <tr class="fbform_header">
            <td colspan="4">Carian Sekolah
            </td>
        </tr>
        <tr>

            <td class="fbtd_left">Negeri:</td>
            <td>
                <asp:DropDownList ID="ddlSchoolState" AutoPostBack="true" runat="server" Width="250px">
                </asp:DropDownList></td>
            <td class="fbtd_left">Bandar:</td>
            <td>
                <asp:DropDownList ID="ddlSchoolCity" runat="server" Width="250px">
                </asp:DropDownList></td>

        </tr>
        <tr>
            <td>Kod Sekolah:</td>
            <td>
                <asp:TextBox ID="txtSchoolCode" runat="server" Width="250px" MaxLength="250"></asp:TextBox></td>
            <td>Nama Sekolah:</td>
            <td>
                <asp:TextBox ID="txtSchoolName" runat="server" Width="250px" MaxLength="250"></asp:TextBox></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td>
                <asp:CheckBox ID="chkXXX" runat="server" Text="Kod Sekolah XXX" Checked="false" />
            </td>
        </tr>
        <tr>
            <td class="fbform_sap" colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" /></td>
        </tr>
    </table>
    &nbsp;<asp:Label ID="lblMsgTop" runat="server" Text=""></asp:Label>
    <table class="fbform">
        <tr class="fbform_header">
            <td>Senarai Sekolah
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="SchoolID"
                    Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Sekolah">
                            <ItemTemplate>
                                <asp:Label ID="lblKodSekolah" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Sekolah">
                            <ItemTemplate>
                                <asp:Label ID="lblNamaSekolah" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bandar">
                            <ItemTemplate>
                                <asp:Label ID="lblBandar" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negeri">
                            <ItemTemplate>
                                <asp:Label ID="txtNegeri" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Left" />
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