<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="pusatujian_student_select.ascx.vb"
    Inherits="permatapintar.pusatujian_student_select" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }

    function calSelect(myTextbox) {
        myCal.select(document.getElementById('<%= txtTarikhUjian.ClientID %>'), 'PERMATApintar', 'yyyy-MM-dd');
    }

</script>

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
        <td class="fbtd_left">Negeri Sekolah:
        </td>
        <td>
            <asp:DropDownList ID="ddlSchoolState" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>Negeri Pelajar:
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentState" runat="server" AutoPostBack="false" Width="250px">
            </asp:DropDownList>
        </td>
        <td>PPD Sekolah:</td>
        <td>
            <asp:DropDownList ID="ddlSchoolPPD" runat="server" AutoPostBack="false" Width="250px"></asp:DropDownList>
        </td>

    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnSearch" runat="server" Text="Cari" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
&nbsp;<asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar Layak UKM2
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
                            <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAll(this);" />
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
                    <asp:TemplateField HeaderText="Negeri Sek.">
                        <ItemTemplate>
                            <asp:Label ID="SchoolState" runat="server" Text='<%# Bind("SchoolState")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PPD Sek.">
                        <ItemTemplate>
                            <asp:Label ID="SchoolPPD" runat="server" Text='<%# Bind("SchoolPPD")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar Sek.">
                        <ItemTemplate>
                            <asp:Label ID="Schoolcity" runat="server" Text='<%# Bind("Schoolcity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Sekolah">
                        <ItemTemplate>
                            <asp:Label ID="SchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                        </ItemTemplate>
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
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Bandar Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentCity" runat="server" Text='<%# Bind("StudentCity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Negeri Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="StudentState" runat="server" Text='<%# Bind("StudentState") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField SelectText="[Pilih]" ShowSelectButton="True" HeaderText="Pilih" />
                    
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
</table>
<table class="fbform">
    <tr class="fbform_header">
        <td colspan="4">Tetapan
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">Waktu Ujian:</td>
        <td>
            <asp:DropDownList ID="ddlSessiUKM2" runat="server" Width="250px">
            </asp:DropDownList></td>
        <td class="fbtd_left">Tarikh Ujian:
        </td>
        <td>
            <script type="text/javascript" id="myjscal">
                var myCal = new CalendarPopup("calDiv");
                myCal.showNavigationDropdowns();
            </script>
            <asp:TextBox ID="txtTarikhUjian" runat="server" Width="150px" MaxLength="250"></asp:TextBox>
            <a href="#" onclick="calSelect(this)" title="calSelect(this)" name="PERMATApintar" id="PERMATApintar">
                <img src="img/department-store-emoticon.png" alt="X" width="15" height="15" onclick="calSelect(this)" title="calSelect(this)" />
            </a>
        </td>
    </tr>

    <tr>
        <td>Fungsi:</td>
        <td>
            <asp:DropDownList ID="ddlMenudesc" runat="server" Width="250px"></asp:DropDownList>
        </td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Button ID="btnExecute" runat="server" Text="Execute" CssClass="fbbutton" /></td>
    </tr>
</table>
*PusatCode IS NULL. Pusat Ujian belum ditetapkan.<br />
<asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label>
<div id="calDiv" style="position: absolute; visibility: hidden; background-color: white;"></div>
