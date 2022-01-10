<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="admin_alumni.ascx.vb" Inherits="KPP_MS.admin_alumni1" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
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

<script type="text/javascript" lang="javascript">
    function CheckAIEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondentAI.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
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
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
        height: 8px;
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
        Menu &nbsp; : &nbsp; Counselor &nbsp; / &nbsp; Alumni &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF;">
        <button id="btnAlumniInformation" runat="server" style="display: inline-block; font-size: 0.8vw">Alumni Information</button>
        <button id="btnAlumniEducation" runat="server" style="display: inline-block; font-size: 0.8vw">Educational Information</button>
        <button id="btnAlumniCompany" runat="server" style="display: inline-block; font-size: 0.8vw">Professional Information</button>
    </div>

    <%--ALUMNI INFORMATION--%>
    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="AlumniInfo" runat="server">
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Graduate Year : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlAI_Year" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Campus : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlAI_Campus" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Program : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlAI_Program" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Batch : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlAI_Batch" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block; font-size: 0.8vw">
            <asp:TextBox CssClass="textboxcss" ID="txtAlumniName" runat="server" Text="" placeholder="   Search By Name / ID / IC" Style="width: 30vw"></asp:TextBox>
        </div>
        <div class="w3-text-black " style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <button id="btnSearch" runat="server" style="display: inline-block; font-size: 0.8vw" class="btn btn-success">Search Alumni</button>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 43vh;" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondentAI" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="std_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAI" Text="" runat="server" onclick="CheckAIEmp(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelectAI" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alumni Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alumni Batch" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="student_Batch" class="id1" runat="server" Text='<%# Eval("student_Batch") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Stream" class="id1" runat="server" Text='<%# Eval("student_Stream") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Start_Year" class="id1" runat="server" Text='<%# Eval("Start_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Graduate Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Graduate_Year" class="id1" runat="server" Text='<%# Eval("Graduate_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="View" ButtonType="image" ShowDeleteButton="true" DeleteImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Alumni Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />
        <br />

        <div id="AlumniDate_Setting" runat="server">
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Alumni Date Setting : </asp:Label>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                <asp:TextBox runat="server" ID="txtDateStart" Style="width: 10vw" CssClass="textboxcss datepicker font"></asp:TextBox>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> To </asp:Label>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                <asp:TextBox runat="server" ID="txtDateEnd" Style="width: 10vw" CssClass="textboxcss datepicker font"></asp:TextBox>
            </div>
            <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                <button id="btnDateSetting_Alumni" runat="server" class="btn btn-primary" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Alumni Setting </button>
            </div>
             <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
                 <button id="BtnPrintPoster" runat="server" class="btn btn-warning" style="top: 1vh; display: inline-block; font-size: 0.8vw">Print Alumni Poster </button>
            </div>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="EducationalInfo" runat="server">
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Graduate Year : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlEI_Year" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Campus : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlEI_Campus" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Program : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlEI_Program" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Batch : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default font" ID="ddlEI_Batch" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 43vh;" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondentEI" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
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
                    <asp:TemplateField HeaderText="Alumni Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alumni ID" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Alumni Batch" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="student_Batch" class="id1" runat="server" Text='<%# Eval("student_Batch") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_Stream" class="id1" runat="server" Text='<%# Eval("student_Stream") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Start_Year" class="id1" runat="server" Text='<%# Eval("Start_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Graduate Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Graduate_Year" class="id1" runat="server" Text='<%# Eval("Graduate_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="View" ButtonType="image" ShowDeleteButton="true" DeleteImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Alumni Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="ProfessionalInfo" runat="server">
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
