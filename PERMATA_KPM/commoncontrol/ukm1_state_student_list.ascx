<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm1_state_student_list.ascx.vb"
    Inherits="permatapintar.ukm1_state_student_list" %>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Tahun Ujian:
        </td>
        <td>
            <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
        <td class="fbtd_left">Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>

        <td>PPD:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolPPD" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
        <td>Bandar:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolCity" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Status:
        </td>
        <td>
            <select name="selStatus" id="selStatus" style="width: 250px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="DONE">DONE</option>
                <option value="NEW">NEW</option>
            </select>&nbsp;
        </td>
        <td></td>
        <td>
            <asp:CheckBox ID="chkRuleAge" runat="server" Text="Pelajar berumur 8-15 tahun (8 tahun - beragama Islam sahaja)" Checked="true" />
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4"></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Keputusan Carian.
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
                    <asp:TemplateField HeaderText="Kod">
                        <ItemTemplate>
                            <asp:Label ID="SchoolCode" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="Studentfullname" runat="server" Text='<%# Bind("Studentfullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian Tamat">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Bind("ExamEnd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Laman Akhir">
                        <ItemTemplate>
                            <asp:Label ID="LastPage" runat="server" Text='<%# Bind("LastPage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="Status" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
   <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
