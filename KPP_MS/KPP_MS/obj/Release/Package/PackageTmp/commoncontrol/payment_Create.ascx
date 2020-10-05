<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_Create.ascx.vb" Inherits="KPP_MS.payment_Create" %>

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
    <button id="payment_information" type="button" runat="server" autopostback="true" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Register New Payment <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>

    <div id="payment_baru" runat="server">

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Year: <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 60%;"></asp:DropDownList>
            </div>

            <div runat="server" class="col-md-4 w3-text-black" style="text-align: left; padding-left: 10px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Fee Type : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 60%;"></asp:DropDownList>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Invoice Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Inv_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div id="Invoice_Gender" runat="server" class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Gender : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlGender" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Price (RM) : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Std_Price" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Placeholder="  Price Per Unit"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Remark : <i class="fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Inv_Remark" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
        </div>
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
            <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
<br />
<div class="messagealert" id="alert_container" style="text-align: center"></div>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Fee Item List </p>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
        <div class="col-md-3 w3-text-black" style="text-align: left">
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Year: </asp:Label>
            <asp:DropDownList ID="ddlYear_List" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 60%;"></asp:DropDownList>
        </div>
        <div class="col-md-4 w3-text-black" style="text-align: left">
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Fee Type : </asp:Label>
            <asp:DropDownList ID="ddlType_List" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 60%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 375px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="FIM_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Year" ItemStyle-Width="70">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Year" class="id1" runat="server" Text='<%# Eval("FIM_Year") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description" ItemStyle-Width="500">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Item" class="id1" runat="server" Text='<%# Eval("FIM_Item") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Quantity" class="id1" runat="server" Text='<%# Eval("FIM_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price (RM)" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Price" class="id1" runat="server" Text='<%# Eval("FIM_Price") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark" ItemStyle-Width="250">
                    <ItemTemplate>
                        <asp:Label ID="FIM_Remark" class="id1" runat="server" Text='<%# Eval("FIM_Remark") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
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
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <button id="Add_Student" runat="server" type="button" class="btn btn-info" style="background-color: #009900; border-radius: 25px;">Assign Student &#160;<i class="fa fa-plus-circle w3-large w3-text-white"></i></button>
    </div>

    <asp:HiddenField ID="hiddenAccess" runat="server" />
</div>

