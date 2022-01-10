<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_List_Table.ascx.vb" Inherits="KPP_MS.lecturer_List_Table" %>

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

    .image-upload > input {
        display: none;
    }
</style>

<script>
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#blah').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#uploadPhoto").change(function () {
        readURL(this);
    });
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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Staff &nbsp; / &nbsp; Search Staff &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewStaff" runat="server" style="display: inline-block; font-size: 0.8vw">View Staff</button>
        <button id="btnRegisterStaff" runat="server" style="display: inline-block; font-size: 0.8vw">Register Staff</button>
        <button id="btnImportStaff" runat="server" style="display: inline-block; font-size: 0.8vw">Import Staff</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ViewStaff" runat="server">

        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block; font-size: 0.8vw">
            <asp:TextBox CssClass="textboxcss" ID="txtstaff_data" runat="server" Text="" placeholder="   Search By Name / ID / IC" Style="width: 500px"></asp:TextBox>
        </div>
        <div class="w3-text-black " style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <button id="btnSearch" runat="server" style="display: inline-block; font-size: 0.8vw" class="btn btn-success">Search Staff</button>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="stf_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Staff Name" ItemStyle-Width="40%">
                        <ItemTemplate>
                            <asp:Label ID="staff_Name" class="id1" runat="server" Text='<%# Bind("staff_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Staff MYKAD" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="staff_Mykad" class="id1" runat="server" Text='<%# Bind("staff_Mykad") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone Number" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="staff_MobileNo" class="id1" runat="server" Text='<%# Bind("staff_MobileNo") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="staff_Email" class="id1" runat="server" Text='<%# Bind("staff_Email") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Institutions" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="staff_Campus" class="id1" runat="server" Text='<%# Bind("staff_Campus") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Staff Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" class="sc4" id="RegisterStaff" runat="server">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">

            <tr>
                <td>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> UKM ID </asp:Label>
                </td>
                <td colspan="4">&nbsp : &nbsp
                    <asp:TextBox runat="server" ID="staff_ID" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>&nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp   &nbsp &nbsp  &nbsp &nbsp  &nbsp   &nbsp &nbsp  &nbsp &nbsp  &nbsp
                </td>
                <td rowspan="8" class="Label" style="text-align: center;">
                    <asp:Image ID="staff_Photo" runat="server" Width="200px" Height="200px" border="2px balck solid" />
                    <br />
                    <br />
                    <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black btn btn-default font" onchange="readURL(this)" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Full Name </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="staff_Name" Style="width: 38.6vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Mykad No </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="staff_Mykad" Style="width: 17vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:RadioButton ID="rbtn_Male" Text="&nbsp; Male &nbsp;&nbsp;&nbsp; " runat="server" GroupName="gender" />
                    <asp:RadioButton ID="rbtn_Female" Text="&nbsp; Female" runat="server" GroupName="gender" />
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="staff_Email" Style="width: 17vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="staff_MobileNo" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Home Address </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="staff_Address" Style="width: 38.6vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> City </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtCity" Style="width: 17vw" CssClass="textboxcss"></asp:TextBox>
                </td>
                <td>
                    <p></p>
                    &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp &nbsp  &nbsp
                </td>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Postcode </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="staff_Posscode" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="staff_State" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Institutions </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="staff_Campus" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position 1 </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="staff_P1_Position" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position 2 </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="staff_P2_Position" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Position 3 </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="staff_P3_Position" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 1vh; display: inline-block">
            <button id="Btnsimpan" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Add Staff Information</button>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" class="sc4" id="ImportStaff" runat="server">


        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="image1" runat="server" Height="30" Width="30" ImageUrl="~/img/1 number.png" Style="display: inline-block" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnDownload" runat="server" style="top: 8px; display: inline-block; font-size: 0.8vw" class="btn btn-warning">Download Excel File</button>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnUpload" runat="server" Height="30" Width="30" ImageUrl="~/img/2 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:FileUpload ID="FlUploadcsv" runat="server" class="btn btn-info" Style="display: inline-block; font-size: 0.8vw" />
            <asp:RegularExpressionValidator ID="regexValidator" runat="server" ErrorMessage="Only XLSX file are allowed" ValidationExpression="(.*\.([Xx][Ll][Ss][Xx])$)" ControlToValidate="FlUploadcsv" Style="display: inline-block"></asp:RegularExpressionValidator>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Image ID="BtnImport" runat="server" Height="30" Width="30" ImageUrl="~/img/3 number.png" Style="display: inline-block; font-size: 0.8vw" />
            &nbsp; &nbsp; &nbsp; &nbsp;
            <button id="BtnUploadedStaff" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Import Staff Information </button>
        </div>

    </div>

</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
