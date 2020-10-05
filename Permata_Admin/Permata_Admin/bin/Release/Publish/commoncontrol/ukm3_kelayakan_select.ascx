<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm3_kelayakan_select.ascx.vb" Inherits="permatapintar.ukm3_kelayakan_select" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Carian.
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Sesi PPCS:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSDate" runat="server" Width="250px">
            </asp:DropDownList>
        </td>
        <td class="fbtd_left">Tahun Ujian:
        </td>
        <td>
            <asp:DropDownList ID="ddlExamYear" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
            &nbsp; &nbsp;
        </td>
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
        <td></td>
        <td></td>
        <td></td>
        <td>
            <asp:CheckBox ID="chkAlumni" runat="server" Text="Alumni PPCS" />
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <div id="dateField" class="overlay" style="display: none;">
                <asp:Calendar ID="calExamEnd" runat="server"></asp:Calendar>
            </div>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />&nbsp;
        </td>
        <td style="text-align: right;">Sort By:
            <select name="selSort" id="selSort" style="width: 200px;" runat="server">
                <option value="0" selected="selected">Nama Pelajar</option>
                <option value="1">UKM2%</option>
            </select>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
<br />
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar PPCS.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="StudentID"
                Width="100%" PageSize="25">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" Text="ALL" runat="server" onclick="CheckAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
                        </ItemTemplate>
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
                    <asp:TemplateField HeaderText="UKM1 %">
                        <ItemTemplate>
                            <asp:Label ID="UKM1TotalPercentage" runat="server" Text='<%# Bind("UKM1TotalPercentage", "{0:n2}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UKM2 %">
                        <ItemTemplate>
                            <asp:Label ID="UKM2TotalPercentage" runat="server" Text='<%# Bind("UKM2TotalPercentage", "{0:n2}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mental Age">
                        <ItemTemplate>
                            <asp:Label ID="Mental_Age_Year" runat="server" Text='<%# Bind("Mental_Age_Year", "{0:n2}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IQ">
                        <ItemTemplate>
                            <asp:Label ID="Student_IQ" runat="server" Text='<%# Bind("Student_IQ", "{0:n2}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UKM2Lang">
                        <ItemTemplate>
                            <asp:Label ID="UKM2SelectedLang" runat="server" Text='<%# Bind("UKM2SelectedLang", "{0:n2}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="PPCSDate">
                        <ItemTemplate>
                            <asp:Label ID="PPCSDate" runat="server" Text='<%# Bind("PPCSDate")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Layak">
                        <ItemTemplate>
                            <asp:Label ID="lblLayak" runat="server" Text=''></asp:Label>
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
    <tr>
        <td>Nota: PPCSStatus=LAYAK. StatusTawaran =TERIMA
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="EXPORT" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnLayak" runat="server" Text="LAYAK" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnTidakLayak" runat="server" Text="TIDAK LAYAK" CssClass="fbbutton" />
        </td>
    </tr>
</table>


