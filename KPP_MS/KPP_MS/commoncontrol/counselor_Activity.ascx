<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="counselor_Activity.ascx.vb" Inherits="KPP_MS.counselor_Activity" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="textboxio/textboxio.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });
    </script>
</head>

<style type="text/css">
    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
    }
</style>

<script type="text/javascript">
    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
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

    .centerHeader {
        text-align: center;
    }
</style>

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

<script type="text/javascript" lang="javascript">
    function CheckAllEmpSC(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=SCRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<script type="text/javascript" lang="javascript">
    function CheckAllEmpSCDI(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=SCDIRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>


<script type="text/javascript" lang="javascript">
    function CheckAllEmpSCSE(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=SCSERespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<script type="text/javascript" lang="javascript">
    function CheckAllEmpSCINC(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=SCINCRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>


<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh;" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
        Menu &nbsp; : &nbsp; Counselor  &nbsp; / &nbsp; Counselling Activity &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnStudentCounselor" runat="server" style="display: inline-block; font-size: 0.8vw">Student Counselor </button>
        <button id="btnViewCounselorReport" runat="server" style="display: inline-block; font-size: 0.8vw">View Counselor Report </button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="StudentCounselor" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddl_Year" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Type : </asp:Label>
            <asp:DropDownList ID="ddl_type" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddl_Level" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div id="Table_SC" runat="server">
            <div style="overflow-y: scroll; height: 40vh" class="table-responsive sc4 font">
                <asp:GridView ID="SCRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAllSC" Text="" runat="server" onclick="CheckAllEmpSC(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectSC" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
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
                        <asp:TemplateField HeaderText="Level" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assessment" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="exam_Name" class="id1" runat="server" Text='<%# Eval("exam_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="student_Stream" class="id1" runat="server" Text='<%# Eval("student_Stream") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GPA" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="png" class="id1" runat="server" Text='<%# Eval("png") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="pngs" class="id1" runat="server" Text='<%# Eval("pngs") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Counselor Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>

            <br />

            <div id="assign_StatusCGPA" runat="server">
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh;">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Name : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorName" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Date : </asp:Label>
                    <asp:TextBox runat="server" ID="txtDate" Style="width: 15vw" CssClass="textboxcss datepicker font"></asp:TextBox>
                </div>

                <div class="w3-text-black" style="text-align: left; padding-left: 3vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Session : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorSessionCGPA" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-top: 2vh;">
                <button id="btn_RegisterCounselorCGPA" runat="server" class="btn btn-success" style="top: 1vh; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Assign Counselor </button>
            </div>
        </div>

        <div id="Table_SCDI" runat="server">
            <div style="overflow-y: scroll; height: 40vh" class="table-responsive sc4 font">
                <asp:GridView ID="SCDIRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="disiplin_id" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAllSCDI" Text="" runat="server" onclick="CheckAllEmpSCDI(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectSCDI" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
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
                        <asp:TemplateField HeaderText="Level" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("class_Level") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Demerit Point" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="case_MeritDemerit_Point" class="id1" runat="server" Text='<%# Eval("case_MeritDemerit_Point") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="case_Category" class="id1" runat="server" Text='<%# Eval("case_Category") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="12%">
                            <ItemTemplate>
                                <asp:Label ID="Dicipline_Date" class="id1" runat="server" Text='<%# Eval("Dicipline_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Discipline Issues Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>

            <br />

            <div id="assign_StatusDI" runat="server">
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh;">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Name : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorName_SCDI" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Date : </asp:Label>
                    <asp:TextBox runat="server" ID="txtDate_SCDI" Style="width: 15vw" CssClass="textboxcss datepicker font"></asp:TextBox>
                </div>
                <div class="w3-text-black" style="text-align: left; padding-left: 3vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Session : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorSessionDI" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-top: 2vh;">
                <button id="btn_RegisterCounselorDiscipline" runat="server" class="btn btn-success" style="top: 1vh; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Assign Counselor </button>
            </div>
        </div>

        <div id="Table_SCSE" runat="server">
            <div style="overflow-y: scroll; height: 40vh" class="table-responsive sc4 font">
                <asp:GridView ID="SCSERespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="std_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAllSCSE" Text="" runat="server" onclick="CheckAllEmpSCSE(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectSCSE" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
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
                        <asp:TemplateField HeaderText="Level" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="case_Category" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Discipline Issues Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>

            <br />

            <div id="assign_StatusSE" runat="server">
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh;">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Name : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorName_SCSE" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Date : </asp:Label>
                    <asp:TextBox runat="server" ID="txtDate_SCSE" Style="width: 15vw" CssClass="textboxcss datepicker font"></asp:TextBox>
                </div>
                <div class="w3-text-black" style="text-align: left; padding-left: 3vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Session : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorSessionSE" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-top: 2vh;">
                <button id="btn_RegisterCounselorSocialEmotional" runat="server" class="btn btn-success" style="top: 1vh; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Assign Counselor </button>
            </div>
        </div>

        <div id="Table_SCINC" runat="server">
            <div style="overflow-y: scroll; height: 40vh" class="table-responsive sc4 font">
                <asp:GridView ID="SCINCRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="CINC_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkboxSelectAllSCINC" Text="" runat="server" onclick="CheckAllEmpSCINC(this);" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelectSCINC" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="35%">
                            <ItemTemplate>
                                <asp:Label ID="Student_Name" class="id1" runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="8%">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested Counsellor" ItemStyle-Width="35%">
                            <ItemTemplate>
                                <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_Status" class="id1" runat="server" Text='<%# Eval("CINC_Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="Status_Color" class="id1" runat="server" Text='<%# Eval("Status_Color") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton Width="25px" Height="25px" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Counselor Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>

            <br />

            <div id="assign_StatusINC" runat="server">
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh;">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Name : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorName_SCINC" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
                <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Date : </asp:Label>
                    <asp:TextBox runat="server" ID="txtDate_SCINC" Style="width: 15vw" CssClass="textboxcss datepicker font"></asp:TextBox>
                </div>
                <div class="w3-text-black" style="text-align: left; padding-left: 3vw; padding-top: 2vh; display: inline-block">
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Session : </asp:Label>
                    <asp:DropDownList ID="ddl_CounselorSessionINC" runat="server" CssClass=" btn btn-default font" AutoPostBack="false" Style="font-size: 0.8vw"></asp:DropDownList>
                </div>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-top: 2vh;">
                <button id="btn_RegisterCounselorINeedCounselling" runat="server" class="btn btn-success" style="top: 1vh; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Assign Counselor </button>
            </div>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="ViewCounselorReport" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddl_Year_VR" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Type : </asp:Label>
            <asp:DropDownList ID="ddl_CounselorType_VR" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Month : </asp:Label>
            <asp:DropDownList ID="ddl_Month_VR" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddl_Level_VR" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Session : </asp:Label>
            <asp:DropDownList ID="ddl_session_VR" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div id="Table_VCR_CGPA" runat="server">
            <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="VCRCGPA_Respondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="CI_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("class_Level") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="5.5%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assessment" ItemStyle-Width="9.5%">
                            <ItemTemplate>
                                <asp:Label ID="exam_Name" class="id1" runat="server" Text='<%# Eval("exam_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="pngs" class="id1" runat="server" Text='<%# Eval("pngs") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Date" class="id1" runat="server" Text='<%# Eval("CI_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_StartTime" class="id1" runat="server" Text='<%# Eval("CI_StartTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_EndTime" class="id1" runat="server" Text='<%# Eval("CI_EndTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Status" class="id1" runat="server" Text='<%# Eval("CI_Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Status_Color" class="id1" runat="server" Text='<%# Eval("CI_Status_Color") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Counselor Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>

        <div id="Table_VCR_DI" runat="server">
            <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="VCRDI_Respondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="CI_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("class_Level") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="5.5%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="case_Category" class="id1" runat="server" Text='<%# Eval("case_Category") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Demerit Point" ItemStyle-Width="9.5%">
                            <ItemTemplate>
                                <asp:Label ID="case_MeritDemerit_Point" class="id1" runat="server" Text='<%# Eval("case_MeritDemerit_Point") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Date" class="id1" runat="server" Text='<%# Eval("CI_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_StartTime" class="id1" runat="server" Text='<%# Eval("CI_StartTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_EndTime" class="id1" runat="server" Text='<%# Eval("CI_EndTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Status" class="id1" runat="server" Text='<%# Eval("CI_Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Status_Color" class="id1" runat="server" Text='<%# Eval("CI_Status_Color") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Counselor Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>

        <div id="Table_VCR_SE" runat="server">
            <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="VCRSE_Respondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="CI_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Level" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("class_Level") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Date" class="id1" runat="server" Text='<%# Eval("CI_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_StartTime" class="id1" runat="server" Text='<%# Eval("CI_StartTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_EndTime" class="id1" runat="server" Text='<%# Eval("CI_EndTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Status" class="id1" runat="server" Text='<%# Eval("CI_Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Status_Color" class="id1" runat="server" Text='<%# Eval("CI_Status_Color") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Counselor Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>

        <div id="Table_VCR_INC" runat="server">
            <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
                <asp:GridView ID="VCRINC_Respondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                    BackColor="#FFFAFA" DataKeyNames="CINC_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                         <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="Student_Name" class="id1" runat="server" Text='<%# Eval("Student_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested Counsellor" ItemStyle-Width="20%">
                            <ItemTemplate>
                                <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Date" class="id1" runat="server" Text='<%# Eval("CINC_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_StartTime" class="id1" runat="server" Text='<%# Eval("CINC_StartTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_EndTime" class="id1" runat="server" Text='<%# Eval("CINC_EndTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_Status" class="id1" runat="server" Text='<%# Eval("CINC_Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_Status_Color" class="id1" runat="server" Text='<%# Eval("CINC_Status_Color") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Counselor Information Are Recorded </b> </asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>

    </div>
</div>


<div class="messagealert" id="alert_container" style="text-align: center"></div>
