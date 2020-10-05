<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="studentprofile_ukm3_nopelajar_update.ascx.vb"
    Inherits="permatapintar.studentprofile_ukm3_nopelajar_update" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sessi PPCS:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
        </td>
        <td>Program Pendidikan:
        </td>
        <td>
            <select name="selProgram" id="selProgram" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="ASAS 1">ASAS 1</option>
                <option value="TAHAP 1">TAHAP 1</option>
            </select>
        </td>

    </tr>
    <tr>
        <td>Jantina:
        </td>
        <td>
            <select name="selStudentGender" id="selStudentGender" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="LELAKI">LELAKI</option>
                <option value="PEREMPUAN">PEREMPUAN</option>
            </select>
        </td>
        <td>Tahun Lahir:
        </td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Layak Kolej PERMATApintar:
        </td>
        <td>
            <select name="selLayakPPMT" id="selLayakPPMT" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="Y">LAYAK</option>
                <option value="N">TIDAK LAYAK</option>
            </select>
        </td>
        <td>Status Tawaran:
        </td>
        <td>
            <select name="selStatusTawaran" id="selStatusTawaran" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="TERIMA">TERIMA</option>
                <option value="TOLAK">TOLAK</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="200px" MaxLength="50"></asp:TextBox>
        </td>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnLoad" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">Senarai Pelajar
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="StudentID"
                Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Penuh">
                        <ItemTemplate>
                            <asp:Label ID="lblStudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ID#">
                        <ItemTemplate>
                            <asp:Label ID="lblMYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AlumniID">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DOB Year">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="StudentGender" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course">
                        <ItemTemplate>
                            <asp:Label ID="CourseCode" runat="server" Text='<%# Bind("CourseCode")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class">
                        <ItemTemplate>
                            <asp:Label ID="ClassCode" runat="server" Text='<%# Bind("ClassCode")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Layak">
                        <ItemTemplate>
                            <asp:Label ID="PPMT" runat="server" Text='<%# Bind("PPMT")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program">
                        <ItemTemplate>
                            <asp:Label ID="lblProgram" runat="server" Text='<%# Bind("Program") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No Pelajar">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNoPelajar" runat="server" Width="60px" MaxLength="10" Text='<%# Bind("NoPelajar") %>'></asp:TextBox>
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
    <tr>
        <td class="fbform_sap">&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini" CssClass="fbbutton" />&nbsp;<asp:Button
                ID="btnExport" runat="server" Text="Export " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<div class="info" id="divMsg" runat="server">
    <asp:Label ID="lblMsg" runat="server" Text="Jumlah Pelajar#:"></asp:Label>
    <asp:Label ID="lblMsgFunc" runat="server" Text=""></asp:Label>
</div>
