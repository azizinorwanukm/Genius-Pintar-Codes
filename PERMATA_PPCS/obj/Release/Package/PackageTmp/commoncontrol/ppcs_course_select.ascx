<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_course_select.ascx.vb"
    Inherits="permatapintar.ppcs_course_select" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Kursus|<asp:Label ID="lblUserType" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sessi PPCS:
            
            &nbsp;
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList></td>
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
        <td>Senarai Kursus.
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
                    <asp:TemplateField HeaderText="#Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="PelajarCount" runat="server" Text='<%# Bind("PelajarCount") %>'></asp:Label>
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
    <tr>
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="text-align: left;">
            <asp:Button ID="btnExport" runat="server" Text="Export " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
    <tr>
        <td>Nota: PPCSStatus=LAYAK
        </td>
    </tr>
</table>
<asp:Label ID="lblGUID" runat="server" Text=""></asp:Label>
