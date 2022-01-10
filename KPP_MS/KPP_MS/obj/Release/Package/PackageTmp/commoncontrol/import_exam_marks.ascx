<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="import_exam_marks.ascx.vb" Inherits="KPP_MS.import_exam_marks" %>

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
        Menu &nbsp; : &nbsp; Examination &nbsp; / &nbsp; Import Examination &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnImportExaminationResult" runat="server" style="display: inline-block; font-size: 0.8vw">Import Examination Result </button>
        <button id="btnImportGPACGPA" runat="server" style="display: inline-block; font-size: 0.8vw">Import GPA & CGPA </button>
        <button id="btnImportKOKO" runat="server" style="display: inline-block; font-size: 0.8vw">Import Co-curricular</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ImportExaminationResult" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="image1" runat="server" Height="30" Width="30" ImageUrl="~/img/1 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnDownloadImportExaminationResult" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw" class="btn btn-warning">Download Excel File</button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnUpload" runat="server" Height="30" Width="30" ImageUrl="~/img/2 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:FileUpload ID="FileUploadImportExaminationResult" runat="server" class="btn btn-info" Style="display: inline-block; font-size: 0.8vw" />
            <asp:RegularExpressionValidator ID="regexValidatorImportExaminationResult" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FileUploadImportExaminationResult" Style="display: inline-block"></asp:RegularExpressionValidator>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnImport" runat="server" Height="30" Width="30" ImageUrl="~/img/3 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnUploadedImportExaminationResult" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Import Examination Result </button>
        </div>

        <br />
        <br />

        <div runat="server" id="ViewImportExaminationResult" style="overflow-y: scroll; height: 40vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="35%">
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
                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="class_name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exam Name" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="exam_name" class="id1" runat="server" Text='<%# Eval("exam_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="subject" class="id1" runat="server" Text='<%# Eval("subject_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Marks" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="marks" class="id1" runat="server" Text='<%# Eval("marks") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Grade" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="grade" class="id1" runat="server" Text='<%# Eval("grade") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ImportGPACGPA" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="image2" runat="server" Height="30" Width="30" ImageUrl="~/img/1 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnDownloadImportGPACGPA" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw" class="btn btn-warning">Download Excel File</button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="Image3" runat="server" Height="30" Width="30" ImageUrl="~/img/2 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:FileUpload ID="FileUploadImportGPACGPA" runat="server" class="btn btn-info" Style="display: inline-block; font-size: 0.8vw" />
            <asp:RegularExpressionValidator ID="regexValidatorImportGPACGPA" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FileUploadImportGPACGPA" Style="display: inline-block"></asp:RegularExpressionValidator>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnImportDataGPACGPA" runat="server" Height="30" Width="30" ImageUrl="~/img/3 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnUploadedImportGPACGPA" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Import GPA & CGPA </button>
        </div>

        <br />
        <br />

        <div runat="server" id="ViewImportGPACGPA" style="overflow-y: scroll; height: 40vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondentGPACGPA" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="std_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="35%">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IC" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="student_MyKad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student Level" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exam" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="exam_Name" class="id1" runat="server" Text='<%# Eval("exam_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
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
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ImportKOKO" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="Image5" runat="server" Height="30" Width="30" ImageUrl="~/img/2 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:FileUpload ID="FileUploadImportKOKO" runat="server" class="btn btn-info" Style="display: inline-block; font-size: 0.8vw" />
            <asp:RegularExpressionValidator ID="regexValidatorImportKOKO" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FileUploadImportKOKO" Style="display: inline-block"></asp:RegularExpressionValidator>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="Image6" runat="server" Height="30" Width="30" ImageUrl="~/img/3 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnUploadedImportKOKO" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Import Co-Curricular </button>
        </div>
    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
