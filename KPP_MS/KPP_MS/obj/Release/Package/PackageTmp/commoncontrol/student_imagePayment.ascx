<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_imagePayment.ascx.vb" Inherits="KPP_MS.student_imagePayment" %>

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

    $(document).ready(function () {
        var accessMenu = document.getElementById('<%= HiddenField1.ClientID %>').value;

        if (accessMenu == "1") {
            document.getElementById("VIEW").style.display = "block";

            $(function () {
                $("#VIEW").dialog({
                    modal: true,
                    buttons: {
                        Ok: function () {
                            $(this).dialog("close");
                        }
                    }
                });
            });

        }
    });    
</script>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajar_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Upload Receipt <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-3 w3-text-black image-upload" style="text-align: center; padding-left: 23px">
            <p style="font: 4px">Click the image below to upload </p>
            <label for="ctl00_ContentPlaceHolder1_student_imagePayment_uploadPhoto">
                <asp:Image ID="student_Photo" runat="server" class="w3-circle blah" Width="100" Height="71" />
            </label>
            <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black" onchange="readURL(this)" />
        </div>
        <div class="col-md-2 w3-text-black image-upload" style="text-align: left; padding-left: 23px">
            <button id="btnUpdate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-top: 70px" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
    </div>
</div>
<br />
<div style="display: none; background-color: #810000; color: #E6E6E6; width: 400px; text-align: center" id="VIEW" title="Receipt Image">
    <asp:Image ID="image1" class="id1" runat="server" ImageUrl='<%# Eval("photo") %>' Width="400" Height="285"></asp:Image>
</div>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="bayaran_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" value="0">Transaction Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-2 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <div style="overflow-y: scroll; overflow-x: hidden; height: 250px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="ID" BorderStyle="None" GridLines="None" Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Receipt" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Image ID="photo" class="id1 w3-circle" runat="server" ImageUrl='<%# Eval("photo") %>' Width="50" Height="40"></asp:Image>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date" ItemStyle-Width="150">
                    <ItemTemplate>
                        <asp:Label ID="date" class="id1" runat="server" Text='<%# Eval("date") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Log" ItemStyle-Width="400">
                    <ItemTemplate>
                        <asp:Label ID="log_user" class="id1" runat="server" Text='<%# Eval("log_user") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:CommandField HeaderText="View" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/Eye_Care_Services-512.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
        </asp:GridView>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</div>
<br />
