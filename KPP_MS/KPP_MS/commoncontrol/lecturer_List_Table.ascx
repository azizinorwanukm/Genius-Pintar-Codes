<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_List_Table.ascx.vb" Inherits="KPP_MS.lecturer_List_Table" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<script type="text/javascript">

    $(document).ready(function () {
        var accessMenu = document.getElementById('<%= hiddenAccess.ClientID %>').value;

        if (accessMenu == "PPE") {s
            document.getElementById('btnReg').style.display = "block";
        }
        else if (accessMenu == "SA") {
            document.getElementById('btnReg').style.display = "block";
        }
    });


</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Staff Data</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstaff_data" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 10px">
            <p></p>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Staff List</p>
    <br />

    <div style="overflow-y: scroll;overflow-x: hidden; height: 550px"  class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black" AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
            BackColor="#d9d9d9" DataKeyNames="stf_ID" BorderStyle="None" GridLines="None"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Image ID="staff_Photo" class="id1 w3-circle" runat="server" ImageUrl='<%# Eval("staff_Photo") %>' Width="50" Height="40"></asp:Image>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Staff Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Bind("staff_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Staff IC" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="staff_IC" class="id1" runat="server" Text='<%# Bind("staff_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone Number" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="staff_MobileNo" class="id1" runat="server" Text='<%# Bind("staff_MobileNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Staff Email" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="staff_Email" class="id1" runat="server" Text='<%# Bind("staff_Email") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
        <div id="btnReg" style="display: none">
            <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-bottom: 10px">
                <button id="btnRegNewStaff" runat="server" type="button" class="btn btn-info" style="background-color: #009900; border-radius: 25px;" title="Save"><i class="fa fa-plus w3-large w3-text-white"></i></button>
                <br />
            </div>
        </div>
    </div>
    <div class="messagealert" id="alert_container" style="text-align: center"></div>
    <asp:HiddenField ID="hiddenAccess" runat="server" />
</div>

