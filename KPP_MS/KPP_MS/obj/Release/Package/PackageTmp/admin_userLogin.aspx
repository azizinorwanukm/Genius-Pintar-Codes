<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/admin.Master" CodeBehind="admin_userLogin.aspx.vb" Inherits="KPP_MS.admin_userLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            var data = document.getElementById('<%=UserType_HF.ClientID %>').value;

            if (data == "Staff") {
                document.getElementById("table_Staff").style.display = "block";
                document.getElementById("table_Student").style.display = "none";
            }


            else if (data == "Student") {
                document.getElementById("table_Staff").style.display = "none";
                document.getElementById("table_Student").style.display = "block";
            }

        });

        function date_Click() {
            if (divcalendar.style.display == "none")
                divcalendar.style.display = "";
            else
                divcalendar.style.display = "none";
        }
    </script>

    <style>
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

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });

    </script>


    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
        <button id="user_list" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">User list <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>

        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px">
            <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl"></asp:DropDownList>
            <asp:DropDownList ID="ddlLogin" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl"></asp:DropDownList>            
        </div>
        <div class="row w3-text-black" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px">
            <asp:Label CssClass="Label" runat="server"> Date : <i class="fa fa-calendar w3-medium w3-text-black"></i></asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="StartDate" runat="server" />
            <asp:Label CssClass="Label" runat="server">      To : <i class="fa fa-calendar w3-medium w3-text-black"></i></asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="EndDate" runat="server" />
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
        <p></p>
        <div style="overflow-y: scroll; overflow-x: hidden; height: 450px; display: none" class="table-responsive" id="table_Staff">
            <asp:GridView ID="datRespondentStaff" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#d9d9d9" DataKeyNames="Trail_ID" BorderStyle="None" GridLines="None" Height="150" Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Login" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="Login_ID" class="id1" runat="server" Text='<%# Eval("Login_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name" ItemStyle-Width="250">
                        <ItemTemplate>
                            <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="Log_Date" class="id1" runat="server" Text='<%# Eval("Log_Date") %>'></asp:Label>
                        </ItemTemplate>                       
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Activity" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="Activity" class="id1" runat="server" Text='<%# Eval("Activity") %>'></asp:Label>
                        </ItemTemplate>                        
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>                    
                </Columns>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <div style="overflow-y: scroll; overflow-x: hidden; height: 450px; display: none" class="table-responsive" id="table_Student">
            <asp:GridView ID="datRespondentStudent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#d9d9d9" DataKeyNames="Trail_ID" BorderStyle="None" GridLines="None" Height="150" Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Login" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="Login_ID" class="id1" runat="server" Text='<%# Eval("Login_ID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name" ItemStyle-Width="250">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="Log_Date" class="id1" runat="server" Text='<%# Eval("Log_Date") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Activity" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="Activity" class="id1" runat="server" Text='<%# Eval("Activity") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

        <asp:HiddenField ID="UserType_HF" runat="server" />
    </div>
    <br />

</asp:Content>
