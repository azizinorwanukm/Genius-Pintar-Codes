<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="exam_mark.ascx.vb" Inherits="araken.pcisadmin.exam_mark" %>
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
        <td class="fbtd_left">Status:
        </td>
        <td>
            <select name="selStatus" id="selStatus" style="width: 250px;" runat="server">
                <option value="ALL">ALL</option>
                <option value="1" selected="selected">DONE</option>
                <option value="0">IN PROGRESS</option>
            </select>
        </td>
    </tr>
    <tr>
        <td>Nama Pelajar:
        </td>
        <td>
            <asp:TextBox ID="txtfullname" runat="server" Width="250px" MaxLength="150"></asp:TextBox>&nbsp;
        </td>
        <td>MYKID#:
        </td>
        <td>
            <asp:TextBox ID="txticno" runat="server" Width="250px" MaxLength="150"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbform_sap" colspan="4"></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnSearch" runat="server" Text="Cari " CssClass="fbbutton" />
        </td>
        <td>Susunan:</td>
        <td>
            <select name="selSort" id="selSort" style="width: 150px;" runat="server">
                <option value="test_start" selected="selected">Ujian Mula</option>
                <option value="fullname">Nama Pelajar</option>
                <option value="total">Jumlah</option>
            </select>&nbsp;
            <select name="selDir" id="selDir" style="width: 100px;" runat="server">
                <option value="ASC">Menaik</option>
                <option value="DESC" selected="selected">Menurun</option>
            </select>
        </td>
    </tr>
</table>
&nbsp;
<table class="fbform">
    <tr class="fbform_header">
        <td>Senarai Pelajar.
        </td>
    </tr>
    <tr>
        <td>
            <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="id"
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
                    <asp:TemplateField HeaderText="Ujian Mula">
                        <ItemTemplate>
                            <asp:Label ID="ExamStart" runat="server" Text='<%# Eval("test_start", "{0:dd/MM/yyyy hh:mm:tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Ujian Tamat">
                        <ItemTemplate>
                            <asp:Label ID="ExamEnd" runat="server" Text='<%# Eval("test_end", "{0:dd/MM/yyyy hh:mm:tt}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nama Pelajar">
                        <ItemTemplate>
                            <asp:Label ID="Studentfullname" runat="server" Text='<%# Bind("fullname") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MYKID#">
                        <ItemTemplate>
                            <asp:Label ID="MYKID" runat="server" Text='<%# Bind("icno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="PAPN">
                        <ItemTemplate>
                            <asp:Label ID="AlumniID" runat="server" Text='<%# Bind("learningcentrename") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD1">
                        <ItemTemplate>
                            <asp:Label ID="mod1" runat="server" Text='<%# Bind("mod1") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD2">
                        <ItemTemplate>
                            <asp:Label ID="mod2" runat="server" Text='<%# Bind("mod2") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD3">
                        <ItemTemplate>
                            <asp:Label ID="mod3" runat="server" Text='<%# Bind("mod3") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD4">
                        <ItemTemplate>
                            <asp:Label ID="mod4" runat="server" Text='<%# Bind("mod4") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD5">
                        <ItemTemplate>
                            <asp:Label ID="mod5" runat="server" Text='<%# Bind("mod5") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD6">
                        <ItemTemplate>
                            <asp:Label ID="mod6" runat="server" Text='<%# Bind("mod6") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD7">
                        <ItemTemplate>
                            <asp:Label ID="mod7" runat="server" Text='<%# Bind("mod7") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD8">
                        <ItemTemplate>
                            <asp:Label ID="mod8" runat="server" Text='<%# Bind("mod8") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD9">
                        <ItemTemplate>
                            <asp:Label ID="mod9" runat="server" Text='<%# Bind("mod9") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MOD10">
                        <ItemTemplate>
                            <asp:Label ID="mod10" runat="server" Text='<%# Bind("mod10") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Jumlah">
                        <ItemTemplate>
                            <asp:Label ID="total" runat="server" Text='<%# Bind("total") %>'></asp:Label>
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
    <tr>
        <td>
            <asp:Button ID="btnExport" runat="server" Text="Export Summary" CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExportDetail" runat="server" Text="Export Detail" CssClass="fbbutton" />
        </td>
    </tr>
</table>
<p>
    Mod 1 -  block assembly (5)<br />
    Mod 2 - information (1)<br />
    Mod 3 - abstract reasoning (2)<br />
    Mod 4 - symbol search (3)<br />
    Mod 5 - photographic memory (4)<br />

    * (#) sequence module in exam
</p>
