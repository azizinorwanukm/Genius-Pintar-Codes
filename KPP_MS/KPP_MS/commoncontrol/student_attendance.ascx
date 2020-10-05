<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_attendance.ascx.vb" Inherits="KPP_MS.student_attendance" %>

<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<link rel="stylesheet" href="/resources/demos/style.css">
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

<style>
    .ddl {
        border-radius: 25px;
    }

    .centerHeader {
        text-align: center;
    }

    .lblAbsent {
        font-size: 15px;
    }

    .lblAttend {
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
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(2000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=addRespondent.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">View Attendance</p>


    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 150px;"></asp:DropDownList>
            <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 150px;"></asp:DropDownList>
        </div>


        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:DropDownList ID="ddlStudent_Sem" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;" OnSelectedIndexChanged="ddlStudentSem_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlClass_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;" OnSelectedIndexChanged="ddlClassName_SelectedIndexChanged"></asp:DropDownList>
            <asp:DropDownList ID="ddlSubject_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 190px;" OnSelectedIndexChanged="ddlSubjectName_SelectedIndexChanged"></asp:DropDownList>
        </div>

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 100%; border-radius: 25px;" runat="server" Text="" placeholder="Search By Name / ID "></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
                <p></p>
                <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
            </div>
        </div>
    </div>

</div>
<br />


<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student Attendance</p>
    <br />

    <div style="overflow-y: scroll; overflow-x: scroll; height: 350px" class="table-responsive">
        <asp:GridView ID="viewRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="course_ID" BorderStyle="None" GridLines="None"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>

                <asp:TemplateField HeaderText="#" ItemStyle-Width="10" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student Name">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>' Width="200px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Class">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>' Width="50px"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="1" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday1" class="id1" runat="server" Text='<%# Eval("1") %>' ToolTip='<%# Eval("R1") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="2" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday2" class="id1" runat="server" Text='<%# Eval("2") %>' ToolTip='<%# Eval("R2") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="3" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday3" class="id1" runat="server" Text='<%# Eval("3") %>' ToolTip='<%# Eval("R3") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="4" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday4" class="id1" runat="server" Text='<%# Eval("4") %>' ToolTip='<%# Eval("R4") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="5" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday5" class="id1" runat="server" Text='<%# Eval("5") %>' ToolTip='<%# Eval("R5") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="6" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday6" class="id1" runat="server" Text='<%# Eval("6") %>' ToolTip='<%# Eval("R6") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="7" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday7" class="id1" runat="server" Text='<%# Eval("7") %>' ToolTip='<%# Eval("R7") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="8" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday8" class="id1" runat="server" Text='<%# Eval("8") %>' ToolTip='<%# Eval("R8") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="9" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday9" class="id1" runat="server" Text='<%# Eval("9") %>' ToolTip='<%# Eval("R9") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="10" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday10" class="id1" runat="server" Text='<%# Eval("10") %>' ToolTip='<%# Eval("R10") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="11" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday11" class="id1" runat="server" Text='<%# Eval("11") %>' ToolTip='<%# Eval("R11") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="12" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday12" class="id1" runat="server" Text='<%# Eval("12") %>' ToolTip='<%# Eval("R12") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="13" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday13" class="id1" runat="server" Text='<%# Eval("13") %>' ToolTip='<%# Eval("R13") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="14" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday14" class="id1" runat="server" Text='<%# Eval("14") %>' ToolTip='<%# Eval("R14") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="15" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday15" class="id1" runat="server" Text='<%# Eval("15") %>' ToolTip='<%# Eval("R15") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="16" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday16" class="id1" runat="server" Text='<%# Eval("16") %>' ToolTip='<%# Eval("R16") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="17" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday17" class="id1" runat="server" Text='<%# Eval("17") %>' ToolTip='<%# Eval("R17") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="18" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday18" class="id1" runat="server" Text='<%# Eval("18") %>' ToolTip='<%# Eval("R18") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="19" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday19" class="id1" runat="server" Text='<%# Eval("19") %>' ToolTip='<%# Eval("R19") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="20" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday20" class="id1" runat="server" Text='<%# Eval("20") %>' ToolTip='<%# Eval("R20") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="21" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday21" class="id1" runat="server" Text='<%# Eval("21") %>' ToolTip='<%# Eval("R21") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="22" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday22" class="id1" runat="server" Text='<%# Eval("22") %>' ToolTip='<%# Eval("R22") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="23" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday23" class="id1" runat="server" Text='<%# Eval("23") %>' ToolTip='<%# Eval("R23") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="24" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday24" class="id1" runat="server" Text='<%# Eval("24") %>' ToolTip='<%# Eval("R24") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="25" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday25" class="id1" runat="server" Text='<%# Eval("25") %>' ToolTip='<%# Eval("R25") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="26" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday26" class="id1" runat="server" Text='<%# Eval("26") %>' ToolTip='<%# Eval("R26") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="27" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday27" class="id1" runat="server" Text='<%# Eval("27") %>' ToolTip='<%# Eval("R27") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="28" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday28" class="id1" runat="server" Text='<%# Eval("28") %>' ToolTip='<%# Eval("R28") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="29" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday29" class="id1" runat="server" Text='<%# Eval("29") %>' ToolTip='<%# Eval("R29") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="30" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday30" class="id1" runat="server" Text='<%# Eval("30") %>' ToolTip='<%# Eval("R30") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="31" HeaderStyle-VerticalAlign="Middle" HeaderStyle-CssClass="centerHeader">
                    <ItemTemplate>
                        <asp:Label ID="lblday31" class="id1" runat="server" Text='<%# Eval("31") %>' ToolTip='<%# Eval("R31") %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

            </Columns>

            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">

    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Update Student Attendance</p>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px">
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px; font-weight: bold">
            <asp:Label CssClass="Label" runat="server"> Selected Year : </asp:Label>
            <asp:Label CssClass="Label" runat="server" ID="lblYear"> </asp:Label>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px; font-weight: bold">
            <asp:Label CssClass="Label" runat="server"> Selected Month : </asp:Label>
            <asp:Label CssClass="Label" runat="server" ID="lblMonth"> </asp:Label>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px; font-weight: bold">
            <asp:Label CssClass="Label" runat="server"> Selected Day : </asp:Label>
            <asp:DropDownList ID="ddlDay" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        </div>
    </div>


    <div style="overflow-y: scroll; overflow-x: hidden; height: 350px" class="table-responsive">
        <asp:GridView ID="addRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
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

                <asp:TemplateField HeaderText="#" ItemStyle-Width="10" HeaderStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Student Name">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %> ' Width="200px"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Class">
                    <ItemTemplate>
                        <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>' Width="50px"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Day">
                    <ItemTemplate>
                        <asp:Label ID="date_day" class="id1" runat="server" Text='<%# Eval("date_day") %>' Width="50px"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="status" class="id1" runat="server" Text='<%# Eval("status") %>' Width="100px"></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Remarks">
                    <ItemTemplate>
                        <asp:TextBox ID="attendance_Remarks" Width="250" class="id1" runat="server" Text=""></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                </asp:TemplateField>


            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <p></p>

    <div style="text-align: left; margin-top: 5px; margin-bottom: 10px">
        <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 150px;"></asp:DropDownList>
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 30px" title="Save">Save &#160;  <i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>
</div>

<br />

<div class="messagealert" id="alert_container" style="text-align: center"></div>




