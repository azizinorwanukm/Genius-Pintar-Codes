<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_access.ascx.vb" Inherits="KPP_MS.admin_access" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="admin_access_list" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Admin Accessibility List</button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top:10px; margin-bottom:5px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddladmin" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: scroll; height: 300px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="Menu_Act_ID" BorderStyle="None" GridLines="None"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAllEmp(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Admin Name">
                    <ItemTemplate>
                        <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Menu">
                    <ItemTemplate>
                        <asp:Label ID="Menu" class="id1" runat="server" Text='<%# Eval("Menu") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sub Menu">
                    <ItemTemplate>
                        <asp:Label ID="Sub_Menu" class="id1" runat="server" Text='<%# Eval("Sub_Menu") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Add">
                    <ItemTemplate>
                        <asp:TextBox ID="Add_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Add_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:TextBox ID="Delete_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Delete_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:TextBox ID="Edit_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Edit_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Print">
                    <ItemTemplate>
                        <asp:TextBox ID="Print_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Print_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Save">
                    <ItemTemplate>
                        <asp:TextBox ID="Save_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Save_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Back">
                    <ItemTemplate>
                        <asp:TextBox ID="Back_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Back_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Import/Export">
                    <ItemTemplate>
                        <asp:TextBox ID="Import_Export_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Import_Export_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cgpa">
                    <ItemTemplate>
                        <asp:TextBox ID="Generate_Cgpa_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Generate_Cgpa_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <asp:TextBox ID="Upload_Image_Function" Width="100px" class="id1" runat="server" Text='<%# Eval("Upload_Image_Function") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <p></p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px;">
        <button id="btnSave" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save <i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="admin_access_update" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Admin Config</button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Admin Name : <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:Label CssClass="Label" ID="admin_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlMenu" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlSubMenu" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Add" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Add Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Delete" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Delete Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Edit" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Edit Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Print" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Print Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Save" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Save Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Back" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Back Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_ImportExport" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Import/Export Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Cgpa" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Cgpa Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:CheckBox ID="check_Image" runat="server" />
            <asp:Label CssClass="Label" runat="server"> Image Function <i class="fa fa-fw w3-text-red w3-small"> </i></asp:Label>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px">
        <button id="btnUpload" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save <i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>
</div>
