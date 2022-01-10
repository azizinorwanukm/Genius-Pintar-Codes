<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_Create.ascx.vb" Inherits="KPP_MS.student_Create" %>

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
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


<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 10px" class="w3-text-black">
        Menu &nbsp; : &nbsp; Student &nbsp; / &nbsp; Register Student &nbsp; /  &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 20px;" class="w3-card-2">

    <div style="padding-top: 10px; padding-left: 15px; padding-bottom: 15px; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnRegisterStudent" runat="server" style="top: 8px; display: inline-block;">Register Student Information</button>
        <button id="btnImportStudent" runat="server" style="top: 8px; display: inline-block;">Import Student Information</button>
    </div>

    <div style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; overflow-y: scroll; white-space: nowrap; height: 68vh" id="RegisterStudent" runat="server" class="sc4">

        <div class="w3-text-black sc3" style="text-align: left; padding-left: 10px; border-bottom: 2px solid #929B9E; overflow-x: auto; white-space: nowrap;">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> <b>Student Information</b> </asp:Label>
        </div>
        <p></p>

        <table class="w3-text-black" style="text-align: left; padding-left: 10px; border: hidden; margin-left: 10px">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Name </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Name" Style="width: 500px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> NRIC / MYKAD </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Mykad" Style="width: 300px" placeholder="Example : xxxxxxxxxxx , Without '-'"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="student_ID" Style="width: 300px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email </asp:Label>
                </td>
                <td colspan="2">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Email" Style="width: 300px"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="student_FonNo" Style="width: 245px"></asp:TextBox>
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
                     <asp:DropDownList ID="ddlRace" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
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
                     <asp:DropDownList ID="ddlReligion" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Address </asp:Label>
                </td>
                <td colspan="7">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="student_Address" Style="width: 500px"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="std_txtCty" Style="width: 300px"></asp:TextBox>
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
                     <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
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
                    <asp:TextBox runat="server" ID="student_PostalCode" Style="width: 200px"></asp:TextBox>
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
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
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
                    <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
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
                    <asp:DropDownList ID="ddlSem" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>

        </table>

        <br />

        <div class="w3-text-black sc3" style="text-align: left; padding-left: 10px; border-bottom: 2px solid #929B9E; overflow-x: auto; white-space: nowrap;">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> <b>Guardian Information</b> </asp:Label>
        </div>
        <p></p>

        <table class="w3-text-black" style="text-align: left; padding-left: 10px; border: hidden; margin-left: 10px">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Guardian 1 Name </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent1_Name" Style="width: 400px"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent2_Name" Style="width: 400px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> NRIC / MYKAD </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent1_IC" Style="width: 300px"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> NRIC / MYKAD </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent2_IC" Style="width: 300px"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent1_MobileNo" Style="width: 300px"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent2_MobileNo" Style="width: 300px"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent1_Email" Style="width: 150px"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp 
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="Parent2_Email" Style="width: 150px"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent1_Work" Style="width: 300px"></asp:TextBox>
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
                    <asp:TextBox runat="server" ID="Parent2_Work" Style="width: 300px"></asp:TextBox>
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
                    <asp:DropDownList ID="ddlsalaryP1" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
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
                    <asp:DropDownList ID="ddlsalaryP2" runat="server" AutoPostBack="false" CssClass=" btn btn-default font"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />
        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 10px; display: inline-block">
            <button id="Btnsimpan" runat="server" class="btn btn-success" style="top: 8px; display: inline-block;">Add Student Information</button>
        </div>

    </div>

    <div style="padding-top: 20px; padding-left: 15px; padding-bottom: 10px; overflow-y: scroll; white-space: nowrap; height: 68vh" id="ImportStudent" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 10px;">
            <button id="BtnDownload" runat="server" style="top: 8px; display: inline-block;" class="btn btn-warning">Download Excel File</button>
            <asp:Label CssClass="Label" runat="server"> Please used this excel format </asp:Label>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 10px; padding-top: 10px">
            <asp:FileUpload ID="FlUploadcsv" runat="server" class="btn btn-info" Style="display: inline-block" />
            <asp:Label CssClass="Label" runat="server"> Upload excel file </asp:Label>
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv" Style="display: inline-block"></asp:RegularExpressionValidator>
        </div>

        <br />
        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 10px; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Import Type </asp:Label>
            &nbsp : &nbsp
            <asp:RadioButton ID="rbtnStudentOnly" Text="&nbsp; Student Only &nbsp;&nbsp;&nbsp; " runat="server" GroupName="importtype" />
            <asp:RadioButton ID="rbtnStudentAndCourse" Text="&nbsp; Student And Course" runat="server" GroupName="importtype" />
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 10px; padding-top: 10px">
            <button id="BtnUploadedStudentOnly" runat="server" class="btn btn-success" style="top: 8px; display: inline-block;">Import Student Information </button>
        </div>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>



