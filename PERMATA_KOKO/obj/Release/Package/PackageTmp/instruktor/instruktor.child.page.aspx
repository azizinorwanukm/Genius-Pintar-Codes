<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="instruktor.child.page.aspx.vb" Inherits="permatapintar.instruktor_child_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Senarai Kelas</title>
    <script type="text/javascript">
        function GetRowValue(val) {
            // hardcoded value used to minimize the code.
            // ControlID can instead be passed as query string to the popup window
            window.opener.document.getElementById("ctl00_ContentPlaceHolder1_txtCity").value = val;
            //alert(val);
            window.close();
        }
    </script>
    <link href="~/css/koko_style.css" rel="stylesheet" type="text/css" />
    <link href="~/css/koko_table.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="fbform">
                <tr class="fbform_header">
                    <td colspan="2">Senarai Kelas
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                            CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="KelasID"
                            Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    <ItemStyle VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nama Kelas">
                                    <ItemTemplate>
                                        <asp:Label ID="Kelas" runat="server" Text='<%# Bind("Kelas")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pilih">
                                    <AlternatingItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" Text="Select" />
                                    </AlternatingItemTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnSelect" runat="server" Text="Select" />
                                    </ItemTemplate>
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
            </table>
            <div class="info" id="divMsg" runat="server">
                <asp:Label ID="lblMsg" runat="server" Text="System message..."></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
