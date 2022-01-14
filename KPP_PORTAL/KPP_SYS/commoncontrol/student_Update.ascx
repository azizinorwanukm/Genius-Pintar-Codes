<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_Update.ascx.vb" Inherits="KPP_SYS.student_Update" %>
<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Student Data</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Student Name : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtstudent_data" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search"><i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddl_Level" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevel_Changed"></asp:DropDownList>
        <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddl_Sem" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSem_Changed"></asp:DropDownList>
        <asp:DropDownList Style="width: 190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddl_Year" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_Changed"></asp:DropDownList>
    </div>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <br />
    <div style="overflow-y: scroll;overflow-x: hidden; height: 450px"  class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
            BackColor="#d9d9d9" DataKeyNames="std_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
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
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student MyKad" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Level" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Sem" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Sem" class="id1" runat="server" Text='<%# Eval("student_Sem") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Year" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
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

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlstudentSem" runat="server" AutoPostBack="false" class=" btn btn-default font " Style="width: 190px; border-radius: 25px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlstudentLevel" runat="server" AutoPostBack="false" class=" btn btn-default font " Style="width: 190px; border-radius: 25px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="false" class=" btn btn-default font " Style="width: 190px; border-radius: 25px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlclass" runat="server" AutoPostBack="false" class=" btn btn-default font " Style="width: 190px; border-radius: 25px;"></asp:DropDownList>
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>
    <br />
</div>
