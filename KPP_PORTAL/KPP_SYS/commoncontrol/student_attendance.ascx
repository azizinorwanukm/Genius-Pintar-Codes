<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_attendance.ascx.vb" Inherits="KPP_SYS.student_attendance" %>

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
            <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID "></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
            <p></p>
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search"><i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:DropDownList ID="ddlStudent_Sem" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 190px;" OnSelectedIndexChanged="ddlStudentSem_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlClass_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 190px;" OnSelectedIndexChanged="ddlClassName_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlSubject_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 190px;" OnSelectedIndexChanged="ddlSubjectName_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <br />

    <div style="overflow-y: scroll; overflow-x: hidden; height: 420px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="course_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Course" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
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
                <asp:TemplateField HeaderText="Student Mykad" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Class" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlDay" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100px;"></asp:DropDownList>
        <asp:DropDownList ID="ddlAttendance" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 180px;"></asp:DropDownList>
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        <asp:Label ID="Label1" runat="server" class="w3-text-black"" Text="Label"></asp:Label>
    </div>
    <p></p>
</div>
