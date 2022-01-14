<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_AddHostel.ascx.vb" Inherits="KPP_SYS.student_AddHostel" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Student Data</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Student : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
            <p></p>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search"><i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>   
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:DropDownList ID="ddl_year" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl"  style="width:190px;" onselectedindexchanged="ddlYear_SelectedIndexChanged" ></asp:DropDownList>
            <asp:DropDownList ID="ddl_level" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" style="width:190px;" onselectedindexchanged="ddlLevel_SelectedIndexChanged" ></asp:DropDownList>
            <asp:DropDownList ID="ddl_sem" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" style="width:190px;" onselectedindexchanged="ddlSem_SelectedIndexChanged" DataTextField="-Select Foundation/Level first-" ></asp:DropDownList>
        </div>
    </div>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <br />

    <div style="overflow-y: scroll;overflow-x: hidden; height: 350px"  class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" 
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Hostel" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="hostel_Name" class="id1" runat="server" Text='<%# Eval("hostel_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Block" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="block_Name" class="id1" runat="server" Text='<%# Eval("block_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Floor" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="block_Level" class="id1" runat="server" Text='<%# Eval("block_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Room" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="room_Name" class="id1" runat="server" Text='<%# Eval("room_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlHostelNameChoose" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlBlockNameChoose" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlBlockLevelChoose" runat="server" AutoPostBack="true"  onselectedindexchanged="ddlBlockLevelChoose_SelectedIndexChanged" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlRoomNameChoose" runat="server" AutoPostBack="true"  onselectedindexchanged="ddlRoomNameChoose_SelectedIndexChanged" CssClass=" btn btn-default font ddl" style="width:190px;"></asp:DropDownList>
        <asp:Label CssClass="Label w3-text-black" ID="count_student" runat="server" Text =" " ></asp:Label>
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: center; padding-left: 23px">
        <asp:Label CssClass="Label" ID="nodatamessage" runat="server" ><i class="w3-text-black" > No data found </i> </asp:Label>
        <asp:Label CssClass="Label w3-text-black" ID="debugQuery" runat="server" Text=""></asp:Label>
    </div>
    <p></p>
</div>