<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="counselor_Self_Development_Mark.ascx.vb" Inherits="KPP_MS.counselor_Self_Development_Mark" %>

<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

<script type="text/javascript">
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
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Student Data</p>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlLevelnaming" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlClassnaming" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlExamnaming" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left">
        <div class="col-md-4 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 13px">
            <p></p>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>
    <p></p>
</div>
<br />

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="admin_access_list" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Student List</button>

    <p></p>
    <div style="overflow-y: scroll; overflow-x: scroll; height: 300px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="sd_id" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Student Name">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Width="150px" Font-Size="Smaller" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student IC">
                    <ItemTemplate>
                        <asp:Label ID="txtstudent_Mykad" class="id1" runat="server" Width="50px" Font-Size="Smaller" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class">
                    <ItemTemplate>
                        <asp:Label ID="txtclass_Name" class="id1" runat="server" Width="50px" Font-Size="Smaller" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Leadership">
                    <ItemTemplate>
                        <asp:TextBox ID="txtleadership_mark" Width="70px" Font-Size="Smaller" class="id1" runat="server" Text='<%# Eval("leadership_mark") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Community Service">
                    <ItemTemplate>
                        <asp:TextBox ID="txtcommunityservice_mark" Width="100px" Font-Size="Smaller" class="id1" runat="server" Text='<%# Eval("communityservice_mark") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reflection">
                    <ItemTemplate>
                        <asp:TextBox ID="txtreflection_mark" Width="70px" Font-Size="Smaller" class="id1" runat="server" Text='<%# Eval("reflection_mark") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Assignment">
                    <ItemTemplate>
                        <asp:TextBox ID="txtassignment_mark" Width="70px" Font-Size="Smaller" class="id1" runat="server" Text='<%# Eval("assignment_mark") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Appearance">
                    <ItemTemplate>
                        <asp:TextBox ID="txtappearance_mark" Width="70px" Font-Size="Smaller" class="id1" runat="server" Text='<%# Eval("appearance_mark") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Room Tidiness">
                    <ItemTemplate>
                        <asp:TextBox ID="txtroomtidiness_mark" Width="100px" Font-Size="Smaller" class="id1" runat="server" Text='<%# Eval("roomtidiness_mark") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Atitude">
                    <ItemTemplate>
                        <asp:TextBox ID="txtattitude_mark" Width="70px" Font-Size="Smaller" class="id1" runat="server" Text='<%# Eval("attitude_mark") %>' Enabled="true" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Demerit">
                    <ItemTemplate>
                        <asp:Label ID="txtsd_total_demerit" Width="100px" cFont-Size="Smaller" lass="id1" runat="server" Text='<%# Eval("sd_total_demerit") %>' Enabled="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Attitude">
                    <ItemTemplate>
                        <asp:Label ID="txtsd_total_attitude" Width="70px" class="id1" runat="server" Text='<%# Eval("sd_total_attitude") %>' Enabled="true"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total Mark">
                    <ItemTemplate>
                        <asp:Label ID="txtsd_total" Width="70px" class="id1" runat="server" Text='<%# Eval("sd_total") %>' Enabled="true"></asp:Label>
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
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back">Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>

    <div class="messagealert" id="alert_container" style="text-align: center"></div>
</div>
