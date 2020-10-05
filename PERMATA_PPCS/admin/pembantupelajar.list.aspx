<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="pembantupelajar.list.aspx.vb" Inherits="permatapintar.pembantupelajar_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="4">
                Senarai PEMBANTU PELAJAR (Sila pilih Pembantu Pelajar untuk ditentukan pelajar dibawahnya)
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Tahun:
                <select name="selUsersYear" id="selUsersYear" style="width: 100px;" runat="server">
                    <option value="2010">2010</option>
                    <option value="2011">2011</option>
                    <option value="2012">2012</option>
                    <option value="2013">2013</option>
                    <option value="2014">2014</option>
                    <option value="2015">2015</option>
                </select>&nbsp;<asp:Button ID="btnRefresh" runat="server" Text=" Refresh " CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="myGUID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama">
                            <ItemTemplate>
                                <asp:Label ID="lblFullname" runat="server" Text='<%# Bind("Fullname") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Login ID">
                            <ItemTemplate>
                                <asp:Label ID="lblLoginID" runat="server" Text='<%# Bind("LoginID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Password">
                            <ItemTemplate>
                                <asp:Label ID="lblPwd" runat="server" Text='<%# Bind("Pwd") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IC#">
                            <ItemTemplate>
                                <asp:Label ID="lblICNo" runat="server" Text='<%# Bind("ICNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel. #">
                            <ItemTemplate>
                                <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField SelectText="Pilih" ShowSelectButton="true" />
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
                Jumlah Rekod:<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="Black"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr style="text-align: left">
            <td>
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red" Style="text-align: left"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
