<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_RegClass.ascx.vb" Inherits="KPP_MS.lecturer_RegClass" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

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
    .sc3::-webkit-scrollbar {
        height: 8px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
        border-radius: 3px;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }
</style>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Staff &nbsp; / &nbsp; Course Placement &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewCourse" runat="server" style="display: inline-block; font-size:0.8vw">View Staff Course</button>
        <button id="btnRegisterCourse" runat="server" style="display: inline-block; font-size:0.8vw">Register Staff Course</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" class="sc4" id="ViewCourse" runat="server">
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlCourseYear" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Institutions : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlCourseCampus" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> program : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlCourseProgram" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlCourseLevel" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlCourseSem" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Course : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlCourse" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlClass" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 50vh" class="table-responsive sc4 font">
            <asp:GridView ID="ViewdatRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Staff Name" ItemStyle-Width="35%">
                        <ItemTemplate>
                            <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course" ItemStyle-Width="35%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="subject_level" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="subject_sem" class="id1" runat="server" Text='<%# Eval("subject_sem") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Staff Course Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" class="sc4" id="RegisterCourse" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlYear" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Institutions : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlFilterCampus" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Program : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlFilterProgram" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlFilterLevel" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlFilterSems" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 49vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="subject_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
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
                    <asp:TemplateField HeaderText="Subject Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Code" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Code" class="id1" runat="server" Text='<%# Eval("subject_Code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Type " ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_type" class="id1" runat="server" Text='<%# Eval("subject_type") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_StudentYear" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_sem" class="id1" runat="server" Text='<%# Eval("subject_sem") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit Hour" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_CreditHour" class="id1" runat="server" Text='<%# Eval("subject_CreditHour") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Course Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top:1vh; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Staff : </asp:Label>
            <asp:DropDownList ID="ddlstaffChoose" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" style="font-size:0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <button id="Btnsimpan" runat="server" class="btn btn-success" style="display: inline-block; font-size:0.8vw">Assign Course</button>
        </div>

    </div>



</div>
