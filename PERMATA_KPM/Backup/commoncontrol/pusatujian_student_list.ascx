<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_student_list.ascx.vb"
    Inherits="permatapintar.pusatujian_student_list" %>
<table class="fbform" width="100%">
    <tr class="fbform_header">
        <td colspan="2">
            Carian.
        </td>
    </tr>
    <tr>
        <td>
            Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" Width="250px" MaxLength="250" runat="server"></asp:TextBox>&nbsp;
            MYKAD#:<asp:TextBox ID="txtMYKAD" Width="150px" MaxLength="50" runat="server"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnLoad" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Pelajar Layak UKM2
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
                Width="100%" PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="SchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar">
                        <ItemTemplate>
                            <asp:Label ID="Schoolcity" runat="server" Text='<%# Bind("Schoolcity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TarikhUjian">
                        <ItemTemplate>
                            <asp:Label ID="TarikhUjian" runat="server" Text='<%# Bind("TarikhUjian") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sessi">
                        <ItemTemplate>
                            <asp:Label ID="SessiUKM2" runat="server" Text='<%# Bind("SessiUKM2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No. Tel">
                        <ItemTemplate>
                            <asp:Label ID="FamilyContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Bapa">
                        <ItemTemplate>
                            <asp:Label ID="FatherFullname" runat="server" Text='<%# Bind("FatherFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[PILIH]" ShowSelectButton="True" HeaderText="PILIH" />
                </Columns>
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
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
        <td>
            <asp:Panel ID="pnlBottom" runat="server">
                <asp:Button ID="btnUnAssign" runat="server" Text="Un-assign Pusat" CssClass="fbbutton" />&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;
                Tarikh Ujian:<asp:TextBox ID="txtTarikhUjian" Width="70px" runat="server"></asp:TextBox>&nbsp;
                <asp:ImageButton ID="btnCal" runat="server" ImageUrl="../icons/event.png" />(YYYY-MM-DD)&nbsp;
                Sessi:<select name="selSessiUKM2" id="selSessiUKM2" style="width: 100px;" runat="server">
                    <option value="PAGI">PAGI</option>
                    <option value="TENGAHARI">TENGAHARI</option>
                    <option value="PETANG">PETANG</option>
                </select>
                &nbsp;<asp:Button ID="btnExport" runat="server" Text="Export List" CssClass="fbbutton" />
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td>
            <table width="100%" border="0px">
                <tr>
                    <td style="width: 40%;">
                    </td>
                    <td style="vertical-align: top;">
                        <asp:Calendar ID="myCal" runat="server" Visible="false"></asp:Calendar>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label><br />
    <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label>
</div>
