<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ukm2_kelayakan_totalscore_list.ascx.vb"
    Inherits="permatapintar.ukm2_kelayakan_totalscore_list" %>

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
            &nbsp; &nbsp;
        </td>
        <td class="fbtd_left">Tahun Lahir:
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
        <td>Tarikh Tamat:
        </td>
        <td>
            <asp:TextBox ID="txtExamEnd" runat="server" Width="100px" MaxLength="25"></asp:TextBox>&nbsp;
            <asp:ImageButton ID="btnDate" runat="server" ImageUrl="~/icons/event.png" AlternateText=".." Width="15" Height="15" />
            <asp:Calendar ID="calExamEnd" runat="server" Visible="false" BackColor="White"></asp:Calendar>
        </td>
        <td>Agama:
        </td>
        <td>
            <asp:DropDownList ID="ddlStudentReligion" runat="server" AutoPostBack="true" Width="250px">
            </asp:DropDownList>
        </td>

    </tr>
    <tr>
        <td></td>
        <td>
            <asp:CheckBox ID="chkAlumni" runat="server" Text="Alumni PPCS" />
        </td>

        <td></td>
        <td>
            <asp:CheckBox ID="chkRuleAge" runat="server" Text="Pelajar berumur 8-15 tahun (8 tahun - beragama Islam sahaja)" Checked="true" />
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4">
            <asp:Label ID="lblInfo" runat="server" Text="Nota: Kosongkan Tarikh Tamat untuk carian semua rekod." ForeColor="Red" Font-Italic="true"></asp:Label>
        </td>
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
        <td>Senarai Pelajar. Urutan mengikut markah tertinggi. 
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
                    <asp:TemplateField HeaderText="Alumni ID">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("AlumniID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Tahun Lahir">
                        <ItemTemplate>
                            <asp:Label ID="DOB_Year" runat="server" Text='<%# Bind("DOB_Year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Agama">
                        <ItemTemplate>
                            <asp:Label ID="StudentReligion" runat="server" Text='<%# Bind("StudentReligion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Kod">
                        <ItemTemplate>
                            <asp:Label ID="SchoolCode" runat="server" Text='<%# Bind("SchoolCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian Mula">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Bind("ExamStart") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian Tamat">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Bind("ExamEnd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Soalan">
                        <ItemTemplate>
                            <asp:Label ID="QuestionYear" runat="server" Text='<%# Bind("QuestionYear") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModA">
                        <ItemTemplate>
                            <asp:Label ID="ModA" runat="server" Text='<%# Bind("ModA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModB">
                        <ItemTemplate>
                            <asp:Label ID="ModB" runat="server" Text='<%# Bind("ModB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ModC">
                        <ItemTemplate>
                            <asp:Label ID="ModC" runat="server" Text='<%# Bind("ModC") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total">
                        <ItemTemplate>
                            <asp:Label ID="TotalScore" runat="server" Text='<%# Bind("TotalScore") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="%">
                        <ItemTemplate>
                            <asp:Label ID="TotalPercentage" runat="server" Text='<%# Bind("TotalPercentage","{0:F2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Layak">
                        <ItemTemplate>
                            <asp:Label ID="IsLayak" runat="server" Text='<%# Bind("IsLayak") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" />
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
            <asp:Button ID="btnLayak" runat="server" Text="Layak UKM2" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnTidakLayak" runat="server" Text="Tidak Layak UKM2" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export" CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
