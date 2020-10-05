<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/default.Master"
    CodeBehind="admin.import.aspx.vb" Inherits="permatapintar.admin_import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">
                Admin Import
            </td>
        </tr>
        <tr>
            <td>
                Upload file (Column separator:
                <asp:Label ID="lblSap" runat="server" Text="" CssClass="labelMsg"></asp:Label>):
                <input id="myFile" style="width: 400px; height: 22px" type="file" size="51" name="myFile"
                    runat="server" class="fbbutton" />&nbsp;
                <asp:Button ID="btnImport" runat="server" Text="Import" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td>
                [TokenID],[RespFullname],[RespFilename]<br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="AppMsg" runat="server" ForeColor="red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtRespondents" runat="server" TextMode="MultiLine" Width="700px"
                    Rows="25" Wrap="False" Height="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtSQL" runat="server" Text="" Width="550px"></asp:TextBox>&nbsp;
                <asp:Button ID="btnLoad" runat="server" Text="Load " CssClass="fbbutton" Height="23px"
                    Width="60px" />
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%" border="0">
                    <tr>
                        <td class="fbform_header">
                            Senarai Pelajar :
                            <asp:Label ID="lblCourse" runat="server" Text="" ForeColor="white"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="Tokenid"
                                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nama Penuh">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRespFullname" runat="server" Text='<%# Bind("RespFullname") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTokenid" runat="server" Text='<%# Bind("Tokenid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Filename">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRespFilename" runat="server" Text='<%# Bind("RespFilename") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
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
                            &nbsp;<asp:Label ID="lblPageNo" runat="server" Text="" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnExport" runat="server" Text=" Export to CSV " CssClass="fbbutton"
                                Height="23px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
