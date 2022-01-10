<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_koko_attendanceStudent_Detail.ascx.vb" Inherits="KPP_MS.lectuer_koko_attendanceStudent_Detail" %>

<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<link rel="stylesheet" href="/resources/demos/style.css">
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

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
            $('.datepicker').datepicker({ dateFormat: 'DD dd-mm-yy' }).val();
        });
    </script>
</head>

<style type="text/css">
    .ddl {
        border-radius: 25px;
    }

    .CalendarCssClass {
        background-color: #990000;
        font-family: Century;
        text-transform: lowercase;
        width: 750px;
        border: 1px solid Olive;
    }
</style>

<script type="text/javascript">

    function date_Click() {
        if (divcalendar.style.display == "none")
            divcalendar.style.display = "";
        else
            divcalendar.style.display = "none";
    }
</script>

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

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=addRespondent.ClientID%>");
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
        Menu &nbsp; : &nbsp; Co-Curricular &nbsp; / &nbsp; Student Attendance
         &nbsp; / &nbsp; 
        <asp:HyperLink runat="server" ID="previousPage"> View Event Attendance </asp:HyperLink>
        &nbsp; / &nbsp; View Event Attendance Detail
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 76vh" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 10px; border: hidden; margin-left: 10px">
            <tr>
                <td>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year </asp:Label>
                </td>
                <td>&nbsp : &nbsp
                </td>
                <td>
                    <asp:DropDownList CssClass="btn btn-default font" ID="ddlKoko_YearRegister" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>&nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp   &nbsp &nbsp 
                </td>
                <td>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Date </asp:Label>
                </td>
                <td>&nbsp : &nbsp
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="datepicker textboxcss" ID="txtKoko_DateRegister" Style="width: 15vw"></asp:TextBox>
                </td>
                <td>&nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp   &nbsp &nbsp 
                </td>
                <td>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Co-Curricular </asp:Label>
                </td>
                <td>&nbsp : &nbsp
                </td>
                <td>
                    <asp:DropDownList CssClass="btn btn-default font" ID="ddlKoko_Cocurricular" runat="server" AutoPostBack="True" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Title </asp:Label>
                </td>
                <td>&nbsp : &nbsp
                </td>
                <td colspan="9">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtKoko_TitleRegister" Style="width: 40vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Agenda </asp:Label>
                </td>
                <td>&nbsp : &nbsp
                </td>
                <td colspan="9">
                    <p></p>
                    <asp:TextBox runat="server" ID="txtKoko_AgendaRegister" TextMode="Multiline" class="form-control textboxcss" Height="90" Style="width: 40vw"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <div style="overflow-y: scroll; height: 42vh" class="table-responsive sc4 font">
            <asp:GridView ID="addRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="StudentID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
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

                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="45%">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %> '></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student IC" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="Kelas" class="id1" runat="server" Text='<%# Eval("Kelas") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Status" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="status" class="id1" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="statusColor" class="id1" runat="server" Text='<%# Eval("statusColor") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                </Columns>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <button id="btnUpdate" runat="server" class="btn btn-success" style="top: 8px; display: inline-block; font-size: 0.8vw">Update Attendance</button>
        </div>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>