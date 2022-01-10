<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_List_Table.ascx.vb" Inherits="KPP_MS.student_List_Table" %>

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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Student &nbsp; / &nbsp; Search Student &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewStudent" runat="server" style="display: inline-block; font-size: 0.8vw">View Student</button>
        <button id="btnRegisterStudent" runat="server" style="display: inline-block; font-size: 0.8vw">Register Student</button>
        <button id="btnImportStudent" runat="server" style="display: inline-block; font-size: 0.8vw">Import Student</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ViewStudent" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlYear" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Institutions : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddl_Campus" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Program : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlStreaming" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlLevelnaming" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Class : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlClassnaming" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="std_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
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
                    <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student IC" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="student_IC" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class " ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="12%">
                        <ItemTemplate>
                            <asp:Label ID="student_Sem" class="id1" runat="server" Text='<%# Eval("student_Sem") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vw; overflow-y: scroll; white-space: nowrap; height: 70vh" id="RegisterStudent" runat="server" class="sc4">

        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; width: 98%; border-bottom: 2px solid #929B9E;">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> <b>STUDENT INFORMATION</b> </asp:Label>
        </div>
        <p></p>

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vh; border: hidden; margin-left: 1vw">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Full Name </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Name" Style="width: 41.28vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> MyKad No </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Mykad" Style="width: 20vw" placeholder="Example : xxxxxxxxxxx , Without '-'" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student ID </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_ID" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Email" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                </td>
                <td colspan="3">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_FonNo" Style="width: 14.3vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:RadioButton ID="rbtn_Male" Text="&nbsp; Male &nbsp;&nbsp;&nbsp; " runat="server" GroupName="gender" />
                    <asp:RadioButton ID="rbtn_Female" Text="&nbsp; Female" runat="server" GroupName="gender" />
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Race </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlRace" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Religion </asp:Label>
                </td>
                <td colspan="3">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlReligion" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Home Address </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Address" Style="width: 41.28vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> City </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="std_txtCty" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Zip Code </asp:Label>
                </td>
                <td colspan="3">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_PostalCode" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State Of Birth </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlStateOfBirth" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Program </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlStream" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Institutions </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlCampus" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Year </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlYearNaming" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Semester </asp:Label>
                </td>
                <td colspan="3">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSem" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

        </table>

        <br />

        <div class="w3-text-black sc3 font" style="text-align: left; padding-left: 1vw; width: 98%; border-bottom: 2px solid #929B9E; overflow-x: auto; white-space: nowrap;">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> <b>GUARDIAN INFORMATION</b> </asp:Label>
        </div>
        <p></p>

        <table class="w3-text-black font" style="text-align: left; padding-left: 10px; border: hidden; margin-left: 1vw">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Guardian 1 Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent1_Name" Style="width: 28vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent2_Name" Style="width: 28vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> MyKad No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent1_IC" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> MyKad No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent2_IC" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent1_MobileNo" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent2_MobileNo" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent2_Email" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent1_Work" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent2_Work" Style="width: 20vw" CssClass="textboxcss"></asp:TextBox>
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
                    <asp:DropDownList ID="ddlsalaryP1" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
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
                    <asp:DropDownList ID="ddlsalaryP2" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <button id="Btnsimpan" runat="server" class="btn btn-success" style="top: 8px; display: inline-block; font-size: 0.8vw">Add Student Information</button>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1.1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ImportStudent" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="image1" runat="server" Height="30" Width="30" ImageUrl="~/img/1 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnDownload" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw" class="btn btn-warning">Download Excel File</button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnUpload" runat="server" Height="30" Width="30" ImageUrl="~/img/2 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:FileUpload ID="FlUploadcsv" runat="server" class="btn btn-info" Style="display: inline-block; font-size: 0.8vw" />
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv" Style="display: inline-block"></asp:RegularExpressionValidator>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnImport" runat="server" Height="30" Width="30" ImageUrl="~/img/3 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnUploadedStudentOnly" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Import Student Information </button>
        </div>


        <%--<br /><br /><br /><br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="btnUpload_Alumni" runat="server" Height="30" Width="30" ImageUrl="~/img/2 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:FileUpload ID="FlUploadcsv_Alumni" runat="server" class="btn btn-info" Style="display: inline-block; font-size: 0.8vw" />
            <asp:RegularExpressionValidator ID="regexValidator_Alumni" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv_Alumni" Style="display: inline-block"></asp:RegularExpressionValidator>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnImport_Alumni" runat="server" Height="30" Width="30" ImageUrl="~/img/3 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnUploadedStudentOnly_Alumni" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Import Alumni Information </button>
        </div>--%>
    </div>
</div>

