<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="homeroom_ViewAttendance.ascx.vb" Inherits="KPP_MS.homeroom_ViewAttendance" %>

<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<link rel="stylesheet" href="/resources/demos/style.css">
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>

<style>
    .centerHeader {
        text-align: center;
    }

    .lblAbsent {
        font-size: 15px;
    }

    .lblAttend {
        font-size: 15px;
    }

    .lblOthers {
        font-size: 15px;
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
        Menu &nbsp; : &nbsp Homeroom &nbsp / &nbsp Attendance Report
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF;">

        <div class="w3-text-black" style="text-align: left; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Month : </asp:Label>
            <asp:DropDownList ID="ddlMonth" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Program : </asp:Label>
            <asp:DropDownList ID="ddlProgram" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddlStudent_Level" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block;">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList ID="ddlStudent_Sem" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block;">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class : </asp:Label>
            <asp:DropDownList ID="ddlSubject_Name" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block;">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class : </asp:Label>
            <asp:DropDownList ID="ddlClass_Name" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 49vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="course_ID" BorderStyle="None" GridLines="None"
                Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="#" ItemStyle-Width="2%" HeaderStyle-CssClass="centerHeader">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="60%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="5%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="1" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday1" class="id1" runat="server" Text='<%# Eval("1") %>' ToolTip='<%# Eval("R1") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="2" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday2" class="id1" runat="server" Text='<%# Eval("2") %>' ToolTip='<%# Eval("R2") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="3" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblday3" class="id1" runat="server" Text='<%# Eval("3") %>' ToolTip='<%# Eval("R3") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="4" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday4" class="id1" runat="server" Text='<%# Eval("4") %>' ToolTip='<%# Eval("R4") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="5" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday5" class="id1" runat="server" Text='<%# Eval("5") %>' ToolTip='<%# Eval("R5") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="6" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday6" class="id1" runat="server" Text='<%# Eval("6") %>' ToolTip='<%# Eval("R6") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="7" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday7" class="id1" runat="server" Text='<%# Eval("7") %>' ToolTip='<%# Eval("R7") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="8" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday8" class="id1" runat="server" Text='<%# Eval("8") %>' ToolTip='<%# Eval("R8") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="9" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday9" class="id1" runat="server" Text='<%# Eval("9") %>' ToolTip='<%# Eval("R9") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="10" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblday10" class="id1" runat="server" Text='<%# Eval("10") %>' ToolTip='<%# Eval("R10") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="11" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday11" class="id1" runat="server" Text='<%# Eval("11") %>' ToolTip='<%# Eval("R11") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="12" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblday12" class="id1" runat="server" Text='<%# Eval("12") %>' ToolTip='<%# Eval("R12") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="13" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday13" class="id1" runat="server" Text='<%# Eval("13") %>' ToolTip='<%# Eval("R13") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="14" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblday14" class="id1" runat="server" Text='<%# Eval("14") %>' ToolTip='<%# Eval("R14") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="15" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday15" class="id1" runat="server" Text='<%# Eval("15") %>' ToolTip='<%# Eval("R15") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="16" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday16" class="id1" runat="server" Text='<%# Eval("16") %>' ToolTip='<%# Eval("R16") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="17" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday17" class="id1" runat="server" Text='<%# Eval("17") %>' ToolTip='<%# Eval("R17") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="18" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday18" class="id1" runat="server" Text='<%# Eval("18") %>' ToolTip='<%# Eval("R18") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="19" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday19" class="id1" runat="server" Text='<%# Eval("19") %>' ToolTip='<%# Eval("R19") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="20" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday20" class="id1" runat="server" Text='<%# Eval("20") %>' ToolTip='<%# Eval("R20") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="21" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="3%">
                        <ItemTemplate>
                            <asp:Label ID="lblday21" class="id1" runat="server" Text='<%# Eval("21") %>' ToolTip='<%# Eval("R21") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="22" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday22" class="id1" runat="server" Text='<%# Eval("22") %>' ToolTip='<%# Eval("R22") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="23" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday23" class="id1" runat="server" Text='<%# Eval("23") %>' ToolTip='<%# Eval("R23") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="24" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday24" class="id1" runat="server" Text='<%# Eval("24") %>' ToolTip='<%# Eval("R24") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="25" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday25" class="id1" runat="server" Text='<%# Eval("25") %>' ToolTip='<%# Eval("R25") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="26" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday26" class="id1" runat="server" Text='<%# Eval("26") %>' ToolTip='<%# Eval("R26") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="27" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday27" class="id1" runat="server" Text='<%# Eval("27") %>' ToolTip='<%# Eval("R27") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="28" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday28" class="id1" runat="server" Text='<%# Eval("28") %>' ToolTip='<%# Eval("R28") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="29" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday29" class="id1" runat="server" Text='<%# Eval("29") %>' ToolTip='<%# Eval("R29") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="30" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday30" class="id1" runat="server" Text='<%# Eval("30") %>' ToolTip='<%# Eval("R30") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="31" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="lblday31" class="id1" runat="server" Text='<%# Eval("31") %>' ToolTip='<%# Eval("R31") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Attendance Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Notes : </asp:Label>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; background-color: green"> &nbsp; &nbsp; &nbsp; </asp:Label>
            &nbsp;
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Attend , </asp:Label>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; background-color: red"> &nbsp; &nbsp; &nbsp; </asp:Label>
            &nbsp;
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Absent , </asp:Label>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; background-color: yellow"> &nbsp; &nbsp; &nbsp; </asp:Label>
            &nbsp;
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Others </asp:Label>
        </div>

    </div>
</div>

