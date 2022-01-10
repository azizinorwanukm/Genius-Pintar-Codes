<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="course_Create.ascx.vb" Inherits="KPP_MS.course_Create" %>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

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
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%;text-align:left; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<style>
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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 10px" class="w3-text-black">
        Menu &nbsp; : &nbsp; Student &nbsp; / &nbsp;
        <asp:HyperLink runat="server" ID="previousPage"> Course Management </asp:HyperLink> &nbsp;
        / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px;" class="w3-card-2">
    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 15px; border-bottom: 3px solid #567572FF;">
        <button id="btnCourseCreate" runat="server" style="top: 8px; display: inline-block;">Create Course</button>
        <button id="btnCourseTransfer" runat="server" style="top: 8px; display: inline-block;">Transfer Course</button>
    </div>

    <div style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; overflow-y: scroll; height: 68vh" id="CourseCreate" runat="server" class="sc4">

        <table class="w3-text-black" style="text-align: left; padding-left: 10px; border: hidden; margin-left: 10px">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Name (English) </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="subject_Name" Style="width: 600px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Name (Malay) </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="subject_NameBM" Style="width: 600px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Code </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="subject_code" Style="width: 200px"></asp:TextBox>
                </td>
                <td style="width: 50px"></td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Year </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlsubject_year" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Level </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="subject_StudentYear" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
                <td style="width: 50px"></td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Semester </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="subject_sem" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Credit Hour </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="subject_CreditHour" Style="width: 200px"></asp:TextBox>
                </td>
                <td style="width: 50px"></td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Religion </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddl_subjectreligions" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Type </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="subject_type" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Course Group </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlCourse_group" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>

        </table>

        <br />
        <br />

        <button id="btnUpdate" runat="server" class="btn btn-success" style="top: 8px; display: inline-block;">Add New Course</button>
    </div>

    <div style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; overflow-y: scroll; height: 68vh" id="CourseTransfer" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 10px; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddl_Year" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddl_Level" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 10px; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList ID="ddl_Sem" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Type : </asp:Label>
            <asp:DropDownList ID="ddl_type" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 47vh" class="table-responsive sc4">
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
                    <asp:TemplateField HeaderText="Course Name" ItemStyle-Width="400">
                        <ItemTemplate>
                            <asp:Label ID="subjectName" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Code" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subjectCode" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Course Type" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subjectType" class="id1" runat="server" Text='<%# Eval("subject_type") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Year" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subject_studentYear" class="id1" runat="server" Text='<%# Eval("subject_StudentYear") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Credit Hour" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subject_credithour" class="id1" runat="server" Text='<%# Eval("subject_CreditHour") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sem" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="subject_sem" class="id1" runat="server" Text='<%# Eval("subject_sem") %>'></asp:Label>
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

        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Transfer Year : </asp:Label>
            <asp:DropDownList ID="ddlyear_Transfer" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 20px; display: inline-block">
            <button id="btnTransferCourse" runat="server" class="btn btn-success" style="top: 8px; display: inline-block;">Transfer Course</button>
        </div>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
