<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin/admin.Master"
    CodeBehind="addPembantuPelajar.aspx.vb" Inherits="permatapintar.addPembantuPelajar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbform">
        <tr>
            <td colspan="4">
                Tambah Pembantu Pelajar
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Butir-butir Akaun
            </td>
        </tr>
        <tr>
            <td>
                *Nama penuh:
            </td>
            <td>
                <asp:TextBox ID="txtfullname" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Kelas:
            </td>
            <td>
                <asp:DropDownList ID="txtclass" runat="server" Width="255px">
                </asp:DropDownList>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *Nombor IC:
            </td>
            <td>
                <asp:TextBox ID="txtIC" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *File name:
            </td>
            <td>
                <input id="txtFileupload" runat="server" type="file" style="width: 250px;" size="36" />
            </td>
        </tr>
        <tr>
            <td>
                *No. Telefon:
            </td>
            <td>
                <asp:TextBox ID="txtcontactno" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
            <td>
                *Email:
            </td>
            <td>
                <asp:TextBox ID="txtemail" runat="server" Width="250px" MaxLength="254"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *Alamat:
            </td>
            <td>
                <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Rows="5" Width="250px"
                    MaxLength="254"></asp:TextBox>
            </td>
            <td>
                *Kata Laluan:
            </td>
            <td>
                <asp:TextBox ID="txtpwd" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                *Nama Kursus:
            </td>
            <td>
                <asp:DropDownList ID="txtcourse" runat="server" Width="255px">
                </asp:DropDownList>
                <asp:Button ID="btnCode" runat="server" Text=" Kod Kursus " CssClass="fbbutton" />
            </td>
            <td>
                *Mengesahkan kata laluan:
            </td>
            <td>
                <asp:TextBox ID="vPwd" runat="server" Width="250px" MaxLength="254" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                *Kod Kursus:
            </td>
            <td>
                <asp:TextBox ID="txtcoursecode" runat="server" Width="250px" MaxLength="254" ReadOnly="true"
                    BackColor="LightGray"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td style="color: Red;">
                <asp:Label ID="lblScan" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr class="fbform_msg">
            <td>&nbsp;</td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Button ID="btnadd" runat="server" Text=" Tambah " CssClass="fbbutton" />
                &nbsp; &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="4">
                Senarai Pengajar ( Klik pilih untuk mengemaskini atau menghapus data )
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="loginid"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Email ID">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtloginid" runat="server" Text='<%# Bind("loginid") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblloginid" runat="server" Text='<%# Bind("loginid") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nombor IC">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtIC" runat="server" Text='<%# Bind("ICnumber") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblIC" runat="server" Text='<%# Bind("ICnumber") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Penuh">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtfullname" runat="server" Text='<%# Bind("fullname") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblfullname" runat="server" Text='<%# Bind("fullname") %>'></asp:Label>
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
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="" ForeColor="Black"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
