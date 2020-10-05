<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_AddHostel.ascx.vb" Inherits="KPP_MS.student_AddHostel" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }

    function ShowMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            default:
                cssclass = 'alert-info'
        }
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Select Room:</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList runat="server" ID="ddlHostelYear" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlBlockName" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlBlockLevel" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlHostelSem" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;"></asp:DropDownList>
            <asp:DropDownList runat="server" ID="ddlRoomName" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;"></asp:DropDownList>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">List Student &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>

        <div runat="server" id="divRoomInfo" visible="false" class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <br />
            <div>
                <h5>
                    <asp:Label CssClass="Label" runat="server"> Room Name : </asp:Label><asp:Label CssClass="Label" runat="server" ID="lblRoomName"></asp:Label></h5>
            </div>
            <div>
                <h5>
                    <asp:Label CssClass="Label" runat="server"> Availability : </asp:Label><asp:Label CssClass="Label" runat="server" ID="lblRoomAvailability"></asp:Label></h5>
            </div>
        </div>
    </div>
    <p></p>
</div>
<br />

<div runat="server" class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; text-align: left; padding-left: 23px">
        <div class="col-md-6 w3-text-black" style="text-align: left;">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 85%; border-radius: 25px;" runat="server" Text="" placeholder="   By Name / ID / IC"></asp:TextBox>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <div class="col-md-6 w3-text-black" style="text-align: left;">
            <div style="text-align: left; padding-left: 23px">
                <asp:DropDownList runat="server" ID="dddlFilterTable" AutoPostBack="true" CssClass="btn btn-default ddl" Style="width: 190px;">
                    <asp:ListItem Text="Filter student..."></asp:ListItem>
                    <asp:ListItem Text="Registered" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Not Registered" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div>
            <button id="searchStudent" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>

    <div style="overflow-y: scroll; overflow-x: hidden; height: 350px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="STDID" BorderStyle="None" GridLines="None"
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
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("StudentName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Years" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("StudentLevelYear") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Levels" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("StudentLevelLevel") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Semesters" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="sem" class="id1" runat="server" Text='<%# Eval("SettingSem") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Block Name" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="block_Name" class="id1" runat="server" Text='<%# Eval("SettingBlockName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Block Floor" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="block_Level" class="id1" runat="server" Text='<%# Eval("SettingBlockLevel") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Room Name" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="room_Name" class="id1" runat="server" Text='<%# Eval("RoomName") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Relocate Room">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnEdit" CommandName="changeRoom" CommandArgument='<%# Eval("StudentRoomID") %>' runat="server" ImageUrl="~/img/edit-11-512.png" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <asp:Label align="center" runat="server" Class="id1">No Student Listed</asp:Label>
            </EmptyDataTemplate>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Assign Room &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
    <p></p>
    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</div>
