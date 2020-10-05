<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="exam_Transcript.ascx.vb" Inherits="KPP_MS.exam_Transcript" %>

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
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%;text-align:left; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>


<div class="gridViewRespond" id="Search_Test" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Information</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Student Name : </asp:Label>
            <asp:TextBox CssClass="txtstudent" ID="txtstudent" Style="width: 100%; border-radius: 25px;" runat="server" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px;">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlexam_Name" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlstudent_Year" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlclass" runat="server" AutoPostBack="false" CssClass="btn btn-default ddl"></asp:DropDownList>
        <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160; <i class="fa fa-search w3-large w3-text-white"></i></button>
    </div>
    <asp:Label ID="LBLttlpercentage" runat="server" class="w3-text-black" Text=""></asp:Label>
    <asp:Label ID="LBLpngs" runat="server" class="w3-text-black" Text=""></asp:Label>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="gridViewRespond" style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <br />

    <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" RowStyle-HorizontalAlign="Left"
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
                <asp:TemplateField HeaderText="#">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" ItemStyle-Width="300">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IC" ItemStyle-Width="100">
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
                <asp:TemplateField HeaderText="Class" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GPA" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="png" class="id1" runat="server" Text='<%# Eval("png") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CGPA" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="pngs" class="id1" runat="server" Text='<%# Eval("pngs") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="View" ButtonType="image" ShowDeleteButton="true" DeleteImageUrl="~/img/Eye_Care_Services-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>

    <div id="btn_geberate" class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px;">
        <p>Please generate gpa/cgpa before print slip examination :</p>
        <button id="BtnGenerate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Print">Generate GPA & CGPA &#160; <i class="fa fa-list-alt w3-large w3-text-white"></i></button>
    </div>

    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px">
        <p>Please choose the preferably language :</p>
        <asp:RadioButton ID="rbtn_Malay" Text="Malay &nbsp;&nbsp" runat="server" GroupName="printing_language" />
        <asp:RadioButton ID="rbtn_English" Text="English" runat="server" GroupName="printing_language" />
        <button id="BtnPrintkoko" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 23px" title="Print">Print &#160;<i class="fa fa-print w3-large w3-text-white"></i></button>
        <%--<button id="BtnITextsharpPrint" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 23px" title="Print">Print ITextsharp &#160;<i class="fa fa-print w3-large w3-text-white"></i></button>--%>
        <button id="BtnExport" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Print">Export &#160; <i class="fa fa-list-alt w3-large w3-text-white"></i></button>
    </div>

    <div class="w3-text-black" id="Test" style="display: none">
        <div id="testBM" style='display: block' runat="server">
            <div id='dataBM' style='display: block' runat="server">
                <p>TEST................</p>
                <p>Please Printout the reulst..</p>
            </div>
        </div>
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
