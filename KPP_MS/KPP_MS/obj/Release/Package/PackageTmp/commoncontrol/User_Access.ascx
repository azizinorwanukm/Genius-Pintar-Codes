<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="User_Access.ascx.vb" Inherits="KPP_MS.User_Access" %>

<script type="text/javascript">
    $(document).ready(function () {
        var data = document.getElementById('<%=UserType_HF.ClientID %>').value;

        if (data == "Staff") {
            document.getElementById("table_Staff").style.display = "block";
            document.getElementById("table_Student").style.display = "none";
        }

        else if (data == "Student") {
            document.getElementById("table_Staff").style.display = "none";
            document.getElementById("table_Student").style.display = "block";
        }

    });
</script>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="user_list" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">User list <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlAccess" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlPosition" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px">
        <p></p>
        <asp:Label CssClass="Label w3-text-black" runat="server"> Search User : <i class=" fa fa-fw w3-text-red w3-small"></i></asp:Label>
        <asp:TextBox CssClass="textbox w3-text-black" class="form-control" ID="txt_User" Style="width: 40%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search" >Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 400px; display: none" class="table-responsive" id="table_Staff">
        <asp:GridView ID="datRespondentStaff" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="login_ID" BorderStyle="None" GridLines="None" Height="150" OnRowCancelingEdit="StaffCancelEdit" OnRowUpdating="StaffUpdate"
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
                <asp:TemplateField HeaderText="User Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Login ID" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="staff_Login" class="id1" runat="server" Text='<%# Eval("staff_Login") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtstaff_Login" Width="180px" class="id1" runat="server" Text='<%# Eval("staff_Login") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Password" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="staff_Password" class="id1" runat="server" Text='<%# Eval("staff_Password") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtstaff_Password" Width="180px" class="id1" runat="server" Text='<%# Eval("staff_Password") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Access" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="staff_Status" class="id1" runat="server" Text='<%# Eval("staff_Status") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtstaff_Status" Width="60px" class="id1" runat="server" Text='<%# Eval("staff_Status") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" UpdateImageUrl="~/img/save.png" CancelImageUrl="~/img/cancel.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <div style="overflow-y: scroll; overflow-x: hidden; height: 450px; display: none" class="table-responsive" id="table_Student">
        <asp:GridView ID="datRespondentStudent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" Height="150" OnRowCancelingEdit="StudentCancelEdit" OnRowUpdating="StudentUpdate"
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
                <asp:TemplateField HeaderText="User Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Login ID" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Password" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Password" class="id1" runat="server" Text='<%# Eval("student_Password") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtstudent_Password" Width="140px" class="id1" runat="server" Text='<%# Eval("student_Password") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Access" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="student_Status" class="id1" runat="server" Text='<%# Eval("student_Status") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtstudent_Status" Width="60px" class="id1" runat="server" Text='<%# Eval("student_Status") %>' />
                    </EditItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" UpdateImageUrl="~/img/save.png" CancelImageUrl="~/img/cancel.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <asp:HiddenField ID="UserType_HF" runat="server" />
</div>
<br />
