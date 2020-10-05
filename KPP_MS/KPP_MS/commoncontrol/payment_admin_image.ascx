<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_admin_image.ascx.vb" Inherits="KPP_MS.payment_admin_image" %>

<link rel="stylesheet" href="js/jquery-ui.css" />
<script type="text/javascript" src="js/jquery-1.10.2.js"></script>
<script type="text/javascript" src="js/jquery-ui.js"></script>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }

    .modalBackground {
        background-color: Gray;
        filter: alpha(opacity=80);
        opacity: 0.8;
        z-index: 10000;
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

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student Information</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Name : </asp:Label>
            <asp:Label CssClass="Label" ID="student_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> IC Number : </asp:Label>
            <asp:Label CssClass="Label" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student ID : </asp:Label>
            <asp:Label CssClass="Label" ID="student_ID" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender : </asp:Label>
            <asp:Label CssClass="Label" ID="student_Sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
    </div>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Invoice Information</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 250px" class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="II_ID" BorderStyle="None" GridLines="None" Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoice No" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="II_InvNo" class="id1" runat="server" Text='<%# Eval("II_InvNo") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" ItemStyle-Width="350">
                    <ItemTemplate>
                        <asp:Label ID="II_Date" class="id1" runat="server" Text='<%# Eval("II_Date") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-Width="200">
                    <ItemTemplate>
                        <asp:Label ID="II_Status" class="id1" runat="server" Text='<%# Eval("II_Status") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="Detail" ButtonType="image" ShowDeleteButton="true" DeleteImageUrl="~/img/Eye_Care_Services-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <p></p>
         <button id="BtnBack" runat="server"  class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px; display: inline" >Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</div>
<br />

<div id="Invoice_Detail" runat="server" class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Invoice Detail</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:DropDownList ID="ddlInvoice_Type" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        </div>
        <div class="col-md-2 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
        </div>
        <div class="col-md-2 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
        </div>
        <div class="col-md-4 w3-text-black ddl" style="text-align: left; padding-left: 23px; margin-top:10px; background-color:plum">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Outstanding Price : (RM) </asp:Label>
            <asp:Label CssClass="Label" ID="ttl_InvPrice" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
        </div>
        <div class="col-md-1 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
        </div>

    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 280px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="IT_ID" BorderStyle="None" GridLines="None"
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
                <asp:TemplateField HeaderText="Item" ItemStyle-Width="450">
                    <ItemTemplate>
                        <asp:Label ID="ID_Item" class="id1" runat="server" Text='<%# Eval("FIM_Item") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:TextBox ID="IT_Quantity" class="id1" runat="server" Width="50" Text='<%# Eval("IT_Quantity") %>'></asp:TextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price (RM)" ItemStyle-Width="125">
                    <ItemTemplate>
                        <asp:Label ID="IT_Price" class="id1" runat="server" Text='<%# Eval("IT_Price") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="IT_Status" class="id1" runat="server" Text='<%# Eval("IT_Status") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <button id="Btnkemaskini" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
<br />
