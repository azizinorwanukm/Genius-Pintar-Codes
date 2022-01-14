<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parent_studenthomepage.ascx.vb" Inherits="KPP_SYS.parent_studenthomepage" %>

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

<script>
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#student_Photo').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#uploadPhoto").change(function () {
        readURL(this);
    });
</script>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp Home &nbsp / &nbsp 
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px;" class="w3-card-2">

    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnStudentInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Student Information</button>
        <button id="btnParentInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Family Information</button>
        <button id="btnCourseInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Course Information</button>
        <button id="btnCocurInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Co-Curriculum Information</button>
        <button id="btnExamInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Examination Information</button>
        <button id="btnHostelInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Hostel Information</button>
        <button id="btnDiscInfo" runat="server" style="display: inline-block; font-size: 0.8vw">Discipline Information</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="StudentInformation" runat="server" class="sc4">
        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student ID </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtstudentID" Style="width: 5vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
                <td>&nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp   &nbsp &nbsp  &nbsp &nbsp 
                </td>
                <td rowspan="8" class="Label" style="text-align: center;">
                    <asp:Image ID="student_Photo" runat="server" Width="200px" Height="200px" border="2px balck solid" />
                    <br />
                    <br />
                    <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black btn btn-default font" onchange="readURL(this)" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Name </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtstudentName" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Mykad No </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:TextBox runat="server" ID="txtstudentMykad" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Race </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlRace" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Religion </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlReligion" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="rbtn_Male" Text="&nbsp; Male &nbsp;&nbsp;&nbsp; " runat="server" GroupName="gender" />
                    <asp:RadioButton ID="rbtn_Female" Text="&nbsp; Female" runat="server" GroupName="gender" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="txtstudentEmail" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtstudentPhone" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Home Address </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="txtstudentAddress" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> City </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="txtstudentCity" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Postcode </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                     <asp:TextBox runat="server" ID="txtstudentPostcode" Style="width: 8vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State Of Birth </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlStateOfBirth" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Program </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtStream" Style="width: 15vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Country Of Birth </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlCountryOfBirth" runat="server" AutoPostBack="True" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Institutions </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtCampus" Style="width: 20vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>

        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Student Year &nbsp : &nbsp </asp:Label>
            <asp:TextBox runat="server" ID="txtstudentYear" Style="width: 10vw" Enabled="false" CssClass="textboxcss"></asp:TextBox>
        </div>
        <div class="w3-text-black font" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Student Level &nbsp : &nbsp </asp:Label>
            <asp:TextBox runat="server" ID="txtstudentLevel" Style="width: 10vw" Enabled="false" CssClass="textboxcss"></asp:TextBox>
        </div>
        <div class="w3-text-black font" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <p></p>
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Student Sem &nbsp : &nbsp </asp:Label>
            <asp:TextBox runat="server" ID="txtstudentSem" Style="width: 10vw" Enabled="false" CssClass="textboxcss"></asp:TextBox>
        </div>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <p></p>
            <button id="btnUpdateStudentInfo" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Information</button>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="ParentInformation" runat="server" class="sc4">
        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Guardian 1 Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtGuardianNameOne" Style="width: 28vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Guardian 2 Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtGuardianNameTwo" Style="width: 28vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Mykad / Passport No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtMykadNumberOne" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Mykad / Passport No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtMykadNumberTwo" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtPhoneNoOne" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtPhoneNoTwo" Style="width: 20vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent1_Email" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent2_Email" Style="width: 20vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Relationship </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtRelationshipOne" Style="width: 20vw" CssClass="textboxcss" placeholder="Father / Mother / Uncle / Grandmother "></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Relationship </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtRelationshipTwo" Style="width: 20vw" CssClass="textboxcss" placeholder="Father / Mother / Uncle / Grandmother " Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Occupation </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtJobOne" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Occupation </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtJobTwo" Style="width: 20vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Salary (RM) </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSalaryOne" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Salary (RM) </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlSalaryTwo" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw" Enabled="false"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Status </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="Parent1_RadioStatusAlive" Text="&nbsp; Alive &nbsp;&nbsp;&nbsp; " runat="server" GroupName="P1_Status" />
                    <asp:RadioButton ID="Parent1_RadioStatusPassAway" Text="&nbsp; Passed Away" runat="server" GroupName="P1_Status" />
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Status </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="Parent2_RadioStatusAlive" Text="&nbsp; Alive &nbsp;&nbsp;&nbsp; " runat="server" GroupName="P2_Status" />
                    <asp:RadioButton ID="Parent2_RadioStatusPassAway" Text="&nbsp; Passed Away" runat="server" GroupName="P2_Status" />
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <p></p>
            <button id="btnUpdateParentInformation" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Information</button>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="CourseInformation" runat="server" class="sc4">

        <div class="w3-text-black " style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester : </asp:Label>
            <asp:DropDownList ID="ddlSemesster" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="course_ID" BorderStyle="None" GridLines="None"
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
                    <asp:TemplateField HeaderText="Subject Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Code" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class Name" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("class_Level") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="subject_sem" class="id1" runat="server" Text='<%# Eval("subject_sem") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="yar" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
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

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="CocurricularInformation" runat="server" class="sc4">

        <div class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent_Cocurricular" runat="server" class="table w3-text-black " AutoGenerateColumns="false"
                BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" Width="97%"
                HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="Kelasa" runat="server" Text='<%# Bind("Kelas") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uniform" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="Uniform" runat="server" Text='<%# Bind("Uniform") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Club" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan" runat="server" Text='<%# Bind("Persatuan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sport" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="Sukan" runat="server" Text='<%# Bind("Sukan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sport Houses" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="RumahSukan" runat="server" Text='<%# Bind("RumahSukan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Cocurricular Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vw; white-space: nowrap; height: 70vh" id="ExaminationInformation" runat="server" class="sc4">
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYearExam" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Examination : </asp:Label>
            <asp:DropDownList ID="ddlExamination" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; padding-right: 1.5vw; display: inline-block; float: right">
            <button id="btnPrintMalay" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Print Slip (BM) </button>
            <button id="btnPrintEnglish" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Print Slip (BI) </button>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 36vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent_Examination" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="ID" BorderStyle="None" GridLines="None"
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
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("exam_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exam Name" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("Exam_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Name" ItemStyle-Width="45%">
                        <ItemTemplate>
                            <asp:Label ID="subject_Name" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject Code" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="subject_code" class="id1" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="grade" class="id1" runat="server" Text='<%# Eval("grade") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grades" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="grades" class="id1" runat="server" Text='<%# Eval("gpa") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Examination Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 2vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Academic GPA </asp:Label>
                </td>
                <td>
                    <p></p>
                    :</td>
                <td>
                    <p></p>
                </td>
                <td>

                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_academic_point" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Cocurriculum </asp:Label>
                </td>
                <td>
                    <p></p>
                    :</td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_cocurricular_grade" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_cocurricular_point" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Portfolio </asp:Label>
                </td>
                <td>
                    <p></p>
                    :</td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_portfolio_grade" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_portfolio_point" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Research </asp:Label>
                </td>
                <td>
                    <p></p>
                    :</td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_research_grade" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_research_point" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr id="row_sd" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Self Development </asp:Label>
                </td>
                <td>
                    <p></p>
                    :</td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_sd_grade" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_sd_point" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr id="row_pd" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Personality Development </asp:Label>
                </td>
                <td>
                    <p></p>
                    :</td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_pd_point" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
                <td>
                    <p></p>
                    <div class="w3-text-black" style="text-align: center; display: inline-block">
                        <asp:Label CssClass="Label font" ID="txt_pd_grade" runat="server" Style="width: 100%; padding-left: 1vw; text-align: center"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%; padding-left: 1vw;"> GPA </asp:Label>
            :
            <asp:Label CssClass="Label font" ID="txt_pd_gpa" runat="server" Style="width: 100%;"></asp:Label>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> CGPA </asp:Label>
            :
            <asp:Label CssClass="Label font" ID="txt_pd_cgpa" runat="server" Style="width: 100%;"></asp:Label>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="HostelInformation" runat="server" class="sc4">

        <div class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent_Hostel" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true"
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
                    <asp:TemplateField HeaderText="Campus" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="CampusNames" class="id1" runat="server" Text='<%# Eval("hostel_CampusNames") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="block_Name" class="id1" runat="server" Text='<%# Eval("hostel_BlockNames") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Floor" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="block_Level" class="id1" runat="server" Text='<%# Eval("hostel_BlockLevels") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="hostel_Sem" class="id1" runat="server" Text='<%# Eval("hostel_Sem") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Room No" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="room_Name" class="id1" runat="server" Text='<%# Eval("room_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Hostel Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="DisciplineInformation" runat="server" class="sc4">

        <div class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent_Discipline" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="disiplin_id" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true"
                Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>

                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Dicipline_Date" class="id1" runat="server" Text='<%# Eval("Dicipline_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="60%">
                        <ItemTemplate>
                            <asp:Label ID="Detail_Case" class="id1" runat="server" Text='<%# Eval("Detail_Case") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="Dicipline_Action" class="id1" runat="server" Text='<%# Eval("Dicipline_Action") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1"> <b> No Diciplinary Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>


    <div class="messagealert" id="alert_container" style="text-align: center"></div>

</div>
