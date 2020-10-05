<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_stat_list.ascx.vb"
    Inherits="permatapintar.ppcs_stat_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td>
            Senarai Pelajar PPCS
        </td>
    </tr>
    <tr>
        <td>
            Tahun Ujian:
            <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="120px">
            </asp:DropDownList>
            &nbsp; Sessi PPCS:
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="200px">
            </asp:DropDownList>
            &nbsp; Jenis Sekolah:
            <asp:DropDownList ID="ddlSchoolType" runat="server" Width="250px">
            </asp:DropDownList>
            &nbsp; Lokasi:
            <select name="selSchoolLokasi" id="selSchoolLokasi" style="width: 130px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="B">BANDAR</option>
                <option value="LB">LUAR BANDAR</option>
            </select>
            &nbsp;
            <asp:Button ID="btnLoad" runat="server" Text="Load " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="2">
            Status Pelajar
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
                    <asp:TemplateField HeaderText="Nama Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="lblSchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar">
                        <ItemTemplate>
                            <asp:Label ID="lblSchoolCity" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negeri">
                        <ItemTemplate>
                            <asp:Label ID="lblSchoolState" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
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
                    <asp:TemplateField HeaderText="Jenis Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="lblSchoolType" runat="server" Text='<%# Bind("SchoolType") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lokasi">
                        <ItemTemplate>
                            <asp:Label ID="lblSchoolLokasi" runat="server" Text='<%# Bind("SchoolLokasi") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
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
    <asp:Label ID="lblMsg" runat="server" Text="Jumlah Pelajar#:"></asp:Label></div>
