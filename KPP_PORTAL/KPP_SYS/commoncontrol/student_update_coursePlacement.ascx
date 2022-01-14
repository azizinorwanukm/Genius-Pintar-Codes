<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_update_coursePlacement.ascx.vb" Inherits="KPP_SYS.student_update_coursePlacement" %>

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
    .messagealert {
        width: 40%;
        position: fixed;
        bottom: 25px;
        right: 0px;
        z-index: 100000;
        padding: 0;
        font-size: 15px;
    }

    .sc3::-webkit-scrollbar {
        height: 10px;
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
        Menu &nbsp; : &nbsp Class &nbsp / &nbsp Course Placement
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewClassCourse" runat="server" style="display: inline-block; font-size: 0.8vw">View Class & Course</button>
        <button id="btnRegisterClassCourse" runat="server" style="display: inline-block; font-size: 0.8vw">Register Class & Course</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="ViewClassCourse" runat="server" class="sc4">

        <div class="w3-text-black " style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlViewYear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddlViewLevel" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList ID="ddlViewSemester" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="course_ID" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true"
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
                    <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Level" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_type" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit Hour" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_CreditHour" class="id1" runat="server" Text='<%# Eval("subject_CreditHour") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class Name" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Course Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="RegisterClassCourse" runat="server" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw; width:25vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlLevel" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw; width:25vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Semester </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlSemester" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw; width:25vw"></asp:DropDownList>
                </td>
            </tr>
             <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Type</asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlCourseType" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw; width:25vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlCourse" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw; width:25vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Class Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlClass" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw; width:25vw"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <p></p>
            <button id="btnRegisterCourse" runat="server" class="btn btn-success" style="top: 8px; display: inline-block; font-size:0.8vw">Register New Course</button>
        </div>
    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
