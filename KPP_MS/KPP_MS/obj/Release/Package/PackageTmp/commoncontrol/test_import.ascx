<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="test_import.ascx.vb" Inherits="KPP_MS.test_import" %>

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
    .ddl {
        border-radius: 25px;
    }
</style>

<div id="importClass" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c; margin-top: 10px">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Import Student GPA & CGPA</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">

        <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-top: 10px; margin-bottom: 10px">
            <p>Please used this format before import :</p>
            <button id="BtnDownload" runat="server" class="btn btn-info" style="background-color: #087c35; border-radius: 25px;" title="Print">Excel Format &#160; <i class="fa fa-file-excel-o w3-large w3-text-white"></i></button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px">
            <asp:Label CssClass="Label" runat="server"> Select Excel File to Import:</asp:Label>
            <asp:FileUpload ID="FlUploadcsv" runat="server" class="btn btn-info  ddl" />
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv"></asp:RegularExpressionValidator>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 23px; margin-bottom: 10px">
            <button id="BtnUploaded" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Import File">Import &#160; <i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            <div class="info" id="divMsg" runat="server"></div>
        </div>
    </div>
</div>
<br />
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; border-radius: 25px; border: 5px solid #8c8c8c; text-align: center;">
    <p class="gridViewRespond" style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Export STduent GPA & CGPA</p>
    <br />

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; padding-left: 23px; text-align: left;">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass="btn btn-default font ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlexam_Name" runat="server" AutoPostBack="true" CssClass="btn btn-default font ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddllevel" runat="server" AutoPostBack="true" CssClass="btn btn-default font ddl"></asp:DropDownList>
    </div>

    <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" RowStyle-HorizontalAlign="Left"
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
                <asp:TemplateField HeaderText="Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IC" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_MyKad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Student Level" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="student_Level" class="id1" runat="server" Text='<%# Eval("student_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>              
                <asp:TemplateField HeaderText="Exam" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="exam_Name" class="id1" runat="server" Text='<%# Eval("exam_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="year" class="id1" runat="server" Text='<%# Eval("year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GPA" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="png" class="id1" runat="server" Text='<%# Eval("png") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Label ID="pngs" class="id1" runat="server" Text='<%# Eval("pngs") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
             
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>

    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px; text-align: left">
        <button id="BtnExport" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Export">Export <i class="fa fa-list-alt w3-large w3-text-white"></i></button>
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
