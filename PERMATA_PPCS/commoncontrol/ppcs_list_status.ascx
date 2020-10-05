<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ppcs_list_status.ascx.vb" Inherits="permatapintar.ppcs_list_status1" %>

<script type="text/javascript" lang="javascript">
    function CheckAll(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[19].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

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
        <td>Status Tawaran:
        </td>
        <td>
            <select name="selStatusTawaran" id="selStatusTawaran" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="TERIMA">TERIMA</option>
                <option value="TOLAK">TOLAK</option>
            </select>&nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
        </td>
        <td>
            
        </td>
        <td>Status PPCS:
        </td>
        <td>
            <asp:DropDownList ID="ddlPPCSStatusSearch" runat="server" AutoPostBack="false" Width="200px">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="fbtd_left">
        </td>
        <td>
            
        </td>
        <td>Hadir PPCS:
        </td>
        <td>
            <select name="selHadirPPCS" id="selHadirPPCS" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="Y">Y</option>
                <option value="N">N</option>
            </select>&nbsp;
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
        <td>Scan:
        </td>
        <td>
            <select name="selScanSearch" id="selScanSearch" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="Y">Y</option>
                <option value="N">N</option>
            </select>
        </td>
        <td>Pos:
        </td>
        <td>
            <select name="selPosSearch" id="selPosSearch" style="width: 200px;" runat="server">
                <option value="ALL" selected="selected">ALL</option>
                <option value="Y">Y</option>
                <option value="N">N</option>
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
            <asp:TextBox ID="txtStudentFullname" runat="server" Width="200px" MaxLength="250"></asp:TextBox>
        </td>
    </tr>
    
    <tr>
        <td></td>
        <td class="fbform_sap" colspan="3">&nbsp;
        </td>
    </tr>
    <tr>
        <td></td>
        <td colspan="3">
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
            <%--<div style="width: 80%; overflow: auto">--%>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="StudentID"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left" RowStyle-Wrap="true">
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
                        <asp:TemplateField HeaderText="Nama Sekolah">
                            <ItemTemplate>
                                <asp:Label ID="SchoolName" runat="server" Text='<%# Bind("SchoolName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bandar">
                            <ItemTemplate>
                                <asp:Label ID="SchoolCity" runat="server" Text='<%# Bind("SchoolCity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Negeri">
                            <ItemTemplate>
                                <asp:Label ID="SchoolState" runat="server" Text='<%# Bind("SchoolState") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agama">
                            <ItemTemplate>
                                <asp:Label ID="StudentReligion" runat="server" Text='<%# Bind("StudentReligion") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kursus">
                            <ItemTemplate>
                                <asp:Label ID="CourseCode" runat="server" Text='<%# Bind("CourseCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kelas">
                            <ItemTemplate>
                                <asp:Label ID="ClassCode" runat="server" Text='<%# Bind("ClassCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Tel# Bapa">
                            <ItemTemplate>
                                <asp:Label ID="FamilyContactNo" runat="server" Text='<%# Bind("FamilyContactNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tel# Ibu">
                            <ItemTemplate>
                                <asp:Label ID="FamilyContactNoIbu" runat="server" Text='<%# Bind("FamilyContactNoIbu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Status PPCS">
                            <ItemTemplate>
                                <asp:Label ID="PPCSStatus" runat="server" Text='<%# Bind("PPCSStatus") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status Tawaran">
                            <ItemTemplate>
                                <asp:Label ID="StatusTawaran" runat="server" Text='<%# Bind("StatusTawaran") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hadir PPCS">
                            <ItemTemplate>
                                <asp:Label ID="isHadir" runat="server" Text='<%# Bind("IsHadir") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Scan">
                            <ItemTemplate>
                                <asp:Label ID="IsScan" runat="server" Text='<%# Bind("IsScan") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pos">
                            <ItemTemplate>
                                <asp:Label ID="IsPos" runat="server" Text='<%# Bind("IsPos") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Catatan">
                            <ItemTemplate>
                                <div style="overflow: hidden; text-overflow: ellipsis; white-space: nowrap; width: 100px">
                                    <asp:Label ID="lblCatatan" runat="server" Text='<%# Bind("Catatan") %>' ToolTip='<%#Bind("Catatan")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Edit" SortExpression="">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="ShowPopup" CommandArgument='<%#Eval("PPCSID") %>'>Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAll" Text="ALL" runat="server" onclick="CheckAll(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
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
            <%--</div>--%>
        </td>
    </tr>
    <tr class="fbform_msg">
        <td>
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>

            <select name="selStatus" id="selStatus" style="width: 100px;" runat="server">
                <option value="ScanY">Scan Y</option>
                <option value="ScanN">Scan N</option>
                <option value="PosY">Pos Y</option>
                <option value="PosN">Pos N</option>
            </select>&nbsp;
            <asp:Button ID="btnUpdate" runat="server" Text="Kemaskini " CssClass="fbbutton" />&nbsp;
            <asp:Button ID="btnExport" runat="server" Text="Export " CssClass="fbbutton" />

            <asp:Button ID="btnScan" runat="server" Text="Scan " CssClass="fbbutton" Visible="false" />&nbsp;
            <select name="selScan" id="selScan" style="width: 50px;" runat="server" visible="false">
                <option value="Y">Y</option>
                <option value="N" selected="selected">N</option>
            </select>

            <asp:Button ID="btnPos" runat="server" Text="Pos " CssClass="fbbutton" Visible="false" />&nbsp;
            <select name="selPos" id="selPos" style="width: 50px;" runat="server" visible="false">
                <option value="Y">Y</option>
                <option value="N" selected="selected">N</option>
            </select>
            &nbsp;

        </td>
    </tr>

</table>
Nota: PPCSStatus=LAYAK
<asp:Label ID="lblStudentID" runat="server" Text=""></asp:Label>

<style type="text/css">
    #mask {
        position: fixed;
        left: 0px;
        top: 0px;
        z-index: 4;
        opacity: 0.4;
        -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
        filter: alpha(opacity=40); /* second!*/
        background-color: gray;
        display: none;
        width: 100%;
        height: 100%;
    }
</style>

<script src="../js/jquery-2.0.3.min.js" type="text/javascript"></script>
<script type="text/javascript" lang="javascript">
    function ShowPopup() {
        $('#mask').show();
        $('#<%=pnlpopup.ClientID %>').show();
    }

    function HidePopup() {
        $('#mask').hide();
        $('#<%=pnlpopup.ClientID %>').hide();
    }

    $(".btnClose").live('click', function () {
        HidePopup();
    });
</script>
<div id="mask">
</div>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="215px" Width="400px" Style="z-index: 111; background-color: White; position: absolute; left: 40%; top: 20%; border: outset 2px gray; padding: 5px 5px 10px 5px; display: none">

    <table class="fbform">
        <tr class="fbform_header">
            <td colspan="2">Catatan PPCS </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 45%; text-align: center;">
                <asp:Label ID="LabelValidate" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="fbtd_left">MYKAD:
            </td>
            <td>
                <asp:Label ID="lblMYKAD" runat="server"></asp:Label>
            </td>
        </tr>

        <tr>
            <td>Nama Penuh:
            </td>
            <td>
                <asp:Label ID="lblStudentFullname" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Catatan:
            </td>
            <td>
                <asp:TextBox TextMode="MultiLine" Rows="5" Width="90%" ID="txtCatatan" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="fbform_sap" colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnPPCSUpdate" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="fbbutton" />
                <input type="button" class="fbbutton" value="Cancel" onclick="HidePopup();" />
            </td>
        </tr>
    </table>
</asp:Panel>
