<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_dob.aspx.vb" Inherits="permatapintar.admin_dob" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="fbform">
        <tr class="fbform_header">
            <td>
                Lain-lain><a href="settings.aspx" rel="nofollow" target="_self">Sistem Konfigurasi V2</a>>Senarai Tahun Lahir
            </td>
        </tr>
    </table>
    <br />
    <asp:Button ID="btnDisplayAdd" runat="server" Text="Tambah Tahun Lahir" CssClass="fbbutton" />&nbsp;
    <asp:TextBox ID="txtYear" runat="server" Width="200px" MaxLength="20" placeholder="contoh, 2010" Visible="false"></asp:TextBox>&nbsp;
    <asp:Button ID="btnAddYear" runat="server" Text="Tambah Tahun Lahir" CssClass="fbbutton" Visible="false" CausesValidation="true" />&nbsp;
    <asp:Button ID="btnCancelAdd" runat="server" Text="Batal" CssClass="fbbutton" Visible="false" />&nbsp;
    <br />
    <asp:RangeValidator ID="val" runat="server" ErrorMessage="Sila tulis tahun yang sah." SetFocusOnError="true" ControlToValidate="txtYear" Display="Dynamic" ForeColor="red" Type="Integer" MinimumValue="1900" MaximumValue="2100">
    </asp:RangeValidator>

    <br />
    <asp:Label ID="lblMsg" runat="server" Text="System message: " ForeColor="Red" Font-Bold="true" />&nbsp;
    <br /><br />
    <div>
        <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            CellPadding="2" ForeColor="#333333" GridLines="Both" DataKeyNames="dobyearid" RowDataBound="datRespondent_RowDataBound"
            PageSize="80" CssClass="gridview_footer">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Tahun Lahir" >
                    <ItemTemplate>
                        <asp:Label ID="lbl_DobYear" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField ItemStyle-Width="50" HeaderText="Papar Tahun Lahir">
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlDob" runat="server"></asp:DropDownList>
                        <asp:Label ID="lblDisplay" runat="server" Text='<%# Bind("display") %>' Visible="false" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
            
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    
    </div>
    
    <br />
    <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
    <asp:Button ID="btnBack" runat="server" Text="Kembali" CssClass="fbbutton" />
    <br /><br />
    <asp:Button ID="btnDelYear" runat="server" Text="Buang Tahun Lahir" CssClass="fbbutton" />&nbsp;

</asp:Content>
