<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_update_course.ascx.vb" Inherits="KPP_MS.student_update_course" %>

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
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Student &nbsp; / &nbsp; View Information  &nbsp; /  &nbsp;
        <asp:HyperLink runat="server" ID="previousPage"> View Course </asp:HyperLink>
        &nbsp; / &nbsp; Add A Dropout Student
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddl_year" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Institutions : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddl_Campus" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Program : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddl_Program" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddl_level" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddl_sem" runat="server" AutoPostBack="True" style="font-size:0.8vw"></asp:DropDownList>
        </div>
    </div>

     <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ViewStudent" runat="server">

         <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="std_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
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
                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student IC" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_IC" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Sem" class="id1" runat="server" Text='<%# Eval("student_Sem") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Stream" class="id1" runat="server" Text='<%# Eval("student_Stream") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Institutions" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Campus" class="id1" runat="server" Text='<%# Eval("student_Campus") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />

         <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Course : </asp:Label>
            <asp:DropDownList ID="ddlCourseChoose" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw"></asp:DropDownList>
        </div>
         <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class : </asp:Label>
            <asp:DropDownList ID="ddlClassChose" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" style="font-size:0.8vw"></asp:DropDownList>
        </div>
         <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <button id="btnUpdateStudentCourse" runat="server" class="btn btn-success" style="top: 8px; display: inline-block; ; font-size:0.8vw">Add Student Course</button>
        </div>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>

