<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_view.ascx.vb" Inherits="KPP_MS.Disiplin_view" %>

<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>jQuery UI Datepicker - Default functionality</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="/resources/demos/style.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script>
        $(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd MM yy' }).val();
        });

    </script>
    <style type="text/css">
        .ddl {
            border - radius: 25px;
        }

        .CalendarCssClass {
            background - color: #990000;
            font-family: Century;
            Text-transform: lowercase;
            width: 750px;
            border: 1px solid Olive;
        }
    </style>
</head>

<!--header-->
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Search Student Data</p>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 10px; margin-top: 5px; margin-bottom: 5px; text-align: left;">
        <div class="w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlYear_selectedindexchange" CssClass="btn btn-default ddl" Width="200px"></asp:DropDownList>
            <asp:DropDownList ID="ddlLevelNaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlLevelNaming_selectedindexchange" CssClass="btn btn-default ddl" Width="200px"></asp:DropDownList>
            <asp:DropDownList ID="ddlClassnaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlClassnaming_SelectedIndexChanged" CssClass="btn btn-default ddl" Width="200px"></asp:DropDownList>
        </div>
    </div>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 10px; margin-top: px; margin-bottom: 5px; text-align: left;">
        <div class="w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlCasenaming" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCasenaming_SelectedIndexChanged" CssClass="btn btn-default ddl" Width="350px"></asp:DropDownList>
            <asp:DropDownList ID="ddlAction" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl" Width="200px"></asp:DropDownList>
            <asp:DropDownList ID="ddlCounselingStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCounselingStatus_SelectedIndexChanged" CssClass="btn btn-default ddl" Width="200px"></asp:DropDownList>
        </div>
    </div>

    <!--dropdown search -->
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 10px; margin-top: 5px; margin-bottom: 5px; text-align: left;">
        <div class="w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server">Search Date From : <i class="fa fa-calendar w3-medium w3-text-black"></i></asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="StartDate" runat="server" />
            <asp:Label CssClass="Label" runat="server">      To : <i class="fa fa-calendar w3-medium w3-text-black"></i></asp:Label>
            <asp:TextBox CssClass="ddl datepicker" ID="EndDate" runat="server" />
            <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>

<br />

<!--table untuk display data search -->
<div id="dic_table" style="display: block;" runat="server">
    <div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c; ">

        <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List With Case</p>
        <br />

        <!--direct search -->
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; margin-top: 5px; margin-bottom: 10px;">
            <div class="col-md-8 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server"> Search Student : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="txtstudent" Style="border-radius: 25px; width: 79%" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
            </div>
            <div class="col-md-4 w3-text-black" style="text-align: left;">
                <button id="btnFind" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
            </div>
        </div>

        <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="8"
                BackColor="#d9d9d9" DataKeyNames="disiplin_id" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true" onitemediting="OnItemEditing"
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

                    <asp:TemplateField HeaderText="Date" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="Dicipline_Date" class="id1" runat="server" Text='<%# Eval("Dicipline_Date") %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="300">
                        <ItemTemplate>
                            <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="90">
                        <ItemTemplate>
                            <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Case" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="case_Name" class="id1" runat="server" Text='<%# Eval("case_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Action" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="title" class="id1" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Counselling" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Eval("staff_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Counseling Status" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="counseling_status" class="id1" runat="server" Text='<%# Eval("kslr_status") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Warning Letter">
                        <ItemTemplate>
                            <asp:Button runat="server" CommandName="1" CommandArgument='<%# Eval("std_ID") %>' Text="View" class="btn btn-info" Style="background-color: #005580; border-radius: 25px;" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />

                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label align="center" runat="server" Class="id1">No students conduct any diciplinary action</asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>
</div>
<br />
