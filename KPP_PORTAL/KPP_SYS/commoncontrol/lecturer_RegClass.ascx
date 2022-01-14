<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_RegClass.ascx.vb" Inherits="KPP_SYS.lecturer_RegClass" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>
<style>
    .select-inline {
        display: inline-block;
        border-radius: 25px;
    }

    .ddl {
        border-radius: 25px;
    }
</style>
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Filter List</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="searchTextBox" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Subject Name / Subject Code"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
            <p></p>
            <asp:LinkButton Style="background-color: #005580; border-radius: 25px; display: inline-block;" runat="server" ID="searchBtn" Text="<i class='fa fa-search w3-large w3-text-white'></i>" OnClick="searchBtn_Click" CssClass="btn btn-info" title="Search" />
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:DropDownList ID="ddlFilterSems" runat="server" AutoPostBack="true" onselectedindexchanged="ddlFilterSems_SelectedIndexChanged" CssClass=" btn btn-default font ddl" Style="width: 190px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlFilterType" runat="server" AutoPostBack="true" onselectedindexchanged="ddlFilterType_SelectedIndexChanged" CssClass=" btn btn-default font ddl" Style="width: 190px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlFilterStdntYear" runat="server" AutoPostBack="true" onselectedindexchanged="ddlFilterStdntYear_SelectedIndexChanged" CssClass=" btn btn-default font ddl" Style="width: 190px;"></asp:DropDownList>
        </div>
    </div>
</div>
<br />
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Subject List</p>
    <br />

    <div style="overflow-y: scroll;overflow-x: hidden; height: 450px"  class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
            BackColor="#d9d9d9" DataKeyNames="subject_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Subject Name(BI)" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject Code" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_Code" class="id1" runat="server" Text='<%# Eval("subject_Code") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Level" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_StudentYear" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Subject Type " ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_type" class="id1" runat="server" Text='<%# Eval("subject_type") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semester" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_sem" class="id1" runat="server" Text='<%# Eval("subject_sem") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Credit Hour" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="subject_CreditHour" class="id1" runat="server" Text='<%# Eval("subject_CreditHour") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
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
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlclassChoose" runat="server" class="btn btn-default font" Style="background-color: #f2f2f2; display: inline-block; border-radius: 25px; text-align: left; padding-left: 23px"></asp:DropDownList>
        <asp:DropDownList ID="ddlstaffChoose" runat="server" class="btn btn-default font" Style="background-color: #f2f2f2; display: inline-block; border-radius: 25px; text-align: left; padding-left: 23px"></asp:DropDownList>
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="display: inherit; background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>

    </div>
</div>
