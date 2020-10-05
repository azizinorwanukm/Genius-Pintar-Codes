<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Disiplin_Letter_List.ascx.vb" Inherits="KPP_MS.Disiplin_Letter_List" %>


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

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Warning Letters List</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px;">
            <!-- Student Name -->
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Name : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlListStudentName" Style="width: 20%; font-weight: bold;"></asp:Label>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; ">
            <!-- Student Class -->
            <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Class : </asp:Label>
                <asp:Label CssClass="Label" runat="server" ID="wlClassName" Style="width: 20%; font-weight: bold;"></asp:Label>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px;">
            <div style="overflow-y: scroll; overflow-x: hidden; height: 380px" class="table-responsive">
                <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="9"
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
                                <asp:Label ID="Dicipline_Date" class="id1" runat="server" Text='<%# Eval("Dicipline_Date") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Case" ItemStyle-Width= "350">
                            <ItemTemplate>
                                <asp:Label ID="case_Name" class="id1" runat="server" Text='<%# Eval("case_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Label ID="counseling_status" class="id1" runat="server" Text='<%# Eval("kslr_status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Download">
                            <ItemTemplate>
                                <asp:ImageButton Width="22" Height="22" ID="btnPrint" CommandName="Print" CommandArgument='<%# Eval("disiplin_id") %>' runat="server" ImageUrl="~/img/file_icon.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View">
                            <ItemTemplate>
                                <asp:ImageButton Width="22" Height="22" ID="btnEdit" CommandName="Edit" runat="server" ImageUrl="~/img/edit-11-512.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this letter data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label align="center" runat="server" Class="id1">No warning letter available</asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                    <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <button id="BtnBack" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
        </div>

    </div>
</div>
