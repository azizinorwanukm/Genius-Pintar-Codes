<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="scholarship_create.ascx.vb" Inherits="KPP_MS.scholarship_create" %>


<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
<script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>

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

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Scholarship Registration</p>

    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Scholarship Name : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="scholarship_name" Style="width: 60%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 10px">
            <asp:Label CssClass="Label" runat="server"> Scholarship Sponsor : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="scholarship_sponsor" Style="width: 60%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
    </div>

    <div class="row gridViewRespond w3-text-black" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align:left;padding-left: 23px">
        <asp:Label CssClass="Label" runat="server"> Scholarship Type : </asp:Label>
        <asp:DropDownList Style="width: 190px; height: 29px;" CssClass="w3-text-black btn btn-default ddl" ID="scholarship_type" runat="server"></asp:DropDownList>
    </div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom:10px; text-align: left; padding-left: 23px">
        <button id="btn_create" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="btn_back" type="button" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px; display: inline" runat="server" title="Back">Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>

<br />

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px;margin-bottom:5px">List Scholarship</p>

    <div class="row gridViewRespond w3-text-black" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align:left;padding-left: 23px;margin-bottom:5px">
        <asp:Label CssClass="Label" runat="server"> Scholarship Status : </asp:Label>
        <asp:DropDownList Style="width: 190px; height: 29px;" AutoPostBack="true" CssClass="w3-text-black btn btn-default ddl" ID="ddlStatus" runat="server"></asp:DropDownList>
    </div>

    <div style="overflow-y: scroll; overflow-x: hidden; height: 310px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="scholarship_id" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Scholarship Name" ItemStyle-Width="250">
                    <ItemTemplate>
                        <asp:Label ID="scholarship_name" class="id1" runat="server" Text='<%# Eval("scholarship_name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Scholarship Sponsor" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="scholarship_sponsar" class="id1" runat="server" Text='<%# Eval("scholarship_sponsar") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Type" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="scholarship_type" class="id1" runat="server" Text='<%# Eval("scholarship_type") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="scholarship_status" class="id1" runat="server" Text='<%# Eval("scholarship_status") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/edit-11-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:ImageButton Width="22" Height="22" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/trash.png" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>

