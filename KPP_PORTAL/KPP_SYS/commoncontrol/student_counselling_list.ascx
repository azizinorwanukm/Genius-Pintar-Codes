<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_counselling_list.ascx.vb" Inherits="KPP_SYS.student_counselling_list" %>

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
        Menu &nbsp; : &nbsp Counselling  &nbsp / &nbsp Counselling Information &nbsp / &nbsp 
         <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>


<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewCounselor" runat="server" style="display: inline-block; font-size: 0.8vw">View Counseling </button>
        <button id="btnRegisterCounselor" runat="server" style="display: inline-block; font-size: 0.8vw">I Need Counseling </button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="ViewCounselling" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Type : </asp:Label>
            <asp:DropDownList ID="ddl_CounselorType_VC" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddl_Year_VC" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Session : </asp:Label>
            <asp:DropDownList ID="ddl_session_VC" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
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
                        <asp:TemplateField HeaderText="GPA" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="png" class="id1" runat="server" Text='<%# Eval("png") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="pngs" class="id1" runat="server" Text='<%# Eval("pngs") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Counsellor Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Session" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Session" class="id1" runat="server" Text='<%# Eval("CI_Session") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="5">
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
                        <asp:TemplateField HeaderText="Counsellor Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Session" ItemStyle-Width="7.5%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Session" class="id1" runat="server" Text='<%# Eval("CI_Session") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Counsellor Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Session" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CI_Session" class="id1" runat="server" Text='<%# Eval("CI_Session") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Counsellor Name" ItemStyle-Width="30%">
                            <ItemTemplate>
                                <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Session" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_Session" class="id1" runat="server" Text='<%# Eval("CINC_Session") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_Date" class="id1" runat="server" Text='<%# Eval("CINC_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_StartTime" class="id1" runat="server" Text='<%# Eval("CINC_StartTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_EndTime" class="id1" runat="server" Text='<%# Eval("CINC_EndTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_Status" class="id1" runat="server" Text='<%# Eval("CINC_Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                            <ItemTemplate>
                                <asp:Label ID="CINC_Status_Color" class="id1" runat="server" Text='<%# Eval("CINC_Status_Color") %>'></asp:Label>
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
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="INeedCounselling" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; color:red"><b> ** Please select and fill in the information below if you need a counselling session. </b></asp:Label>
            <br />
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; color:red"><b> ** Noted : Counselling session date will be inform after admin had review your application. </b></asp:Label>
        </div>

        <br /><br /><br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Counselor Name : </asp:Label>
            <asp:DropDownList ID="ddl_INC_CN" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br /><br />

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <%--adjust column width--%>
                <td style="width: 13vw">
                    <p></p>
                    <asp:CheckBox ID="CB_MT" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Motivation </asp:Label>
                </td>
                <td style="width: 9vw">
                    <p></p>
                    <asp:CheckBox ID="CB_PL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Plagiarism </asp:Label>
                </td>
                <td style="width: 17vw">
                    <p></p>
                    <asp:CheckBox ID="CB_CA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class Attendance </asp:Label>
                </td>
                <td style="width: 15vw">
                    <p></p>
                    <asp:CheckBox ID="CB_IA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Inattentive </asp:Label>
                </td>
                <td style="width: 13vw">
                    <p></p>
                    <asp:CheckBox ID="CB_AP" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Academic Performance </asp:Label>
                </td>
                <td style="width: 10vw">
                    <p></p>
                    <asp:CheckBox ID="CB_TR" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Tardy </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_TM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Time Management </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_CG" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Career Guide </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Vandelisme </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_ACWL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Apetite Change & Weight Loss </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Adjustment </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_DA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Disrespectful </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_CL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Cheating & Lying </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_IS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Impulsive </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_BY" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Bully </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AY" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Apathy </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Social Skill </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SH" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Sexual Harrasment </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_RWF" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Relationship With Friend </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Stealling </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_HS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Homesick </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_OC" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Over Confidence </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_HA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Hyperactive </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_EP" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Easy Panic </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_WD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Withdrawn </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AX" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Anxiety </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_RG" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Rage </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_DP" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Depress </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_FGW" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Feeling Guilty / Worthless </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Sadness </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_RL" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Restless </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_PN" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Paranoid </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_PH" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Personal Hygience </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SCD" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Suicidal Desires </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_MF" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Missing Functionality </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_PNM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Perfectionism </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_VA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Very Aggresive </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_IM" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Insomnia </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_ADSAI" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Addiction (Drug,Sex,Alcohol,Internet) </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_BC" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Behavioral Changes </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_DAS" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Drug Abuse </asp:Label>
                </td>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_SHA" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Self-harm Attitude </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:CheckBox ID="CB_AT" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Attachment </asp:Label>
                </td>
                <td colspan="5">
                    <p></p>
                    <asp:CheckBox ID="CB_Other" runat="server" />
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Others : </asp:Label>
                    <asp:TextBox runat="server" ID="txt_CBOther" Style="width: 38vw" CssClass="textboxcss" ></asp:TextBox>
                </td>
            </tr>
        </table>

         <br /><br />

        <button id="btn_register_INC" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw"> Register Counselling </button>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
