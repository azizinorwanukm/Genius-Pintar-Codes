<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="kpp_studentselection.ascx.vb" Inherits="UKM3.kpp_studentselection1" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Pemilihan Pelajar.
        </td>
    </tr>

    <tr>
        <td>UKM3 Session :
        </td>
        <td>
            <asp:DropDownList ID="ddlSession" runat="server" Width="200px" MaxLength="150" AutoPostBack="true"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>Kod Kelas :
        </td>
        <td>
            <asp:DropDownList ID="ddlKodKelas" runat="server" Width="200px" MaxLength="150"></asp:DropDownList>
        </td>
    </tr>

    <tr>
        <td>Jantina :
        </td>
        <td>
            <asp:DropDownList ID="ddlJantina" runat="server" Width="200px" MaxLength="150">
                <asp:ListItem Selected="True" Text="Semua" Value="2"></asp:ListItem>
                <asp:ListItem Text="Lelaki" Value="1"></asp:ListItem>
                <asp:ListItem Text="Perempuan" Value="0"></asp:ListItem>
            </asp:DropDownList>

        </td>
    </tr>

    <tr>
        <td>Nama / Mykad :
        </td>
        <td>
            <asp:TextBox ID="txtsearch" runat="server" Width="200px" MaxLength="200" placeholder="Search by Name/MYKAD"></asp:TextBox>
        </td>
    </tr>

    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
&nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
                Width="100%" PageSize="25" CssClass="gridview_footer">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                     <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="CheckUncheckAll" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="student_sex" runat="server" Text='<%# Bind("student_sex") %>'></asp:Label>
                        </ItemTemplate>
                         <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod Kelas">
                        <ItemTemplate>
                            <asp:Label ID="student_id" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Nama Instruktor">
                        <ItemTemplate>
                            <asp:Label ID="instruktor_id" runat="server" Text='<%# Bind("staff_name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Font-Underline="true" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" CssClass="cssPager" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                    HorizontalAlign="Left" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>
        </td>
    </tr>

    <tr>
        <td colspan="4">
            <asp:Button ID="btnSimpan" Text="Simpan" runat="server" CssClass="fbbutton" />
        </td>
    </tr>

    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
</table>

<br />

<asp:Label ID="lblDebug" runat="server" Text=""></asp:Label>
