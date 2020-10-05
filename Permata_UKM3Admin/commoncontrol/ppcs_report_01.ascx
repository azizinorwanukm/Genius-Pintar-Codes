<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_report_01.ascx.vb" Inherits="permatapintar.ppcs_report_01" %>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td>Sessi PPCS:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
        <td></td><td></td>
        <%--<td class="fbtd_left">Tahun Ujian:
        </td>
        <td>
            <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
            &nbsp; &nbsp;
        </td>--%>
    </tr>

    <tr>
        <td>Agama:
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentReligion" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
        <td>Tahun Lahir:
        </td>
        <td>
            <asp:DropDownList ID="ddlDOB_Year" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Negeri:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
        <td>PPD:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolPPD" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;
            &nbsp;
        </td>
        <td>MYKAD#:
        </td>
        <td>
            <asp:TextBox ID="txtMYKAD" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;
        </td>
    </tr>
    <tr>
        <td>Nama Sekolah:</td>
        <td>
            <asp:TextBox ID="txtSchoolName" runat="server" Width="250px" MaxLength="150"></asp:TextBox></td>
        <td>Jenis Sekolah:</td>
        <td>
            <asp:DropDownList ID="ddlSchoolType" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td class="fbtd_left">AlumniID:</td>
        <td>
            <asp:TextBox ID="txtAlumniID" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
        </td>

        <td></td>
        <td>
            <asp:CheckBox ID="chkAlumni" runat="server" Text="Pelajar Alumni" />
        </td>
    </tr>
    <tr>
        <td>Status PPCS:</td>
        <td>
            <asp:DropDownList ID="ddlPPCSSstatus" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
            <asp:Label ID="lblInfo" runat="server" Text="" ForeColor="Red" Font-Italic="true"></asp:Label>
        </td>
        <td>Sort By:</td>
        <td>
            <select name="selSort" id="selSort" style="width: 250px;" runat="server">
                <option value="0" selected="selected">Nama Pelajar</option>
                <option value="1">Sessi PPCS</option>
            </select>

        </td>
    </tr>
</table>
&nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar.
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
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentFullname" runat="server" Text='<%# Bind("StudentFullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKAD">
                        <ItemTemplate>
                            <asp:Label ID="MYKAD" runat="server" Text='<%# Bind("MYKAD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AlumniID">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jantina">
                        <ItemTemplate>
                            <asp:Label ID="StudentGender" runat="server" Text='<%# Bind("StudentGender") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sessi PPCS">
                        <ItemTemplate>
                            <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="PPCSStatus" runat="server" Text='<%# Bind("PPCSStatus")%>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kursus">
                        <ItemTemplate>
                            <asp:Label ID="CourseNameBM" runat="server" Text='<%# Bind("CourseNameBM") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Papar" />
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
            <asp:Button ID="btnExport" runat="server" Text="Eksport" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
<asp:Label ID="lblSQLDebug" runat="server" Text=""></asp:Label>
