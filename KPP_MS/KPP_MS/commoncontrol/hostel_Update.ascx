<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="hostel_Update.ascx.vb" Inherits="KPP_MS.hostel_Update" %>

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
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
        Menu &nbsp; : &nbsp; Hostel &nbsp; / &nbsp;  Hostel Management  &nbsp; / &nbsp; 
        <asp:HyperLink runat="server" ID="previousPage"> View Hostel </asp:HyperLink>
        &nbsp; / &nbsp;  
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

      <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnEditHostelInformation" runat="server" style="top: 8px; display: inline-block; font-size:0.8vw">Edit Hostel Information</button>
        <button id="btnEditRoomInformation" runat="server" style="top: 8px; display: inline-block; font-size:0.8vw">Edit Room Information</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="EditHostelInformation" runat="server">

        <table class="w3-text-black" style="text-align: left; padding-left: 1vh; border: hidden; margin-left: 1vw">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlHostel_Year" runat="server" CssClass=" btn btn-default font font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Semester </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlHostel_Semester" runat="server" CssClass=" btn btn-default font font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Campus </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlHostel_Campus" runat="server" CssClass=" btn btn-default font font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Block </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlHostel_Block" runat="server" CssClass=" btn btn-default font font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Block Level </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                     <asp:DropDownList ID="ddlHostel_BlockLevel" runat="server" CssClass=" btn btn-default font font"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Room Quantity </asp:Label>
                </td>
                <td colspan="4">
                    <p></p>
                    &nbsp : &nbsp
                    <asp:TextBox runat="server" ID="txtHostel_RoomQuantity" Style="width: 10vw; font-size:0.8vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>

        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top:1vh; display: inline-block">
            <button id="Btnsimpan" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size:0.8vw">Update Hostel</button>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="EditRoomInformation" runat="server">

        <div style="overflow-y: scroll; height: 62vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating"
                BackColor="#FFFAFA" DataKeyNames="room_ID" BorderStyle="None" GridLines="None"
                Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="year" runat="server" Text='<%# Bind("year") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Semester" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="room_Sem" runat="server" Text='<%# Bind("room_Sem") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Block" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="hostel_BlockNames" runat="server" Text='<%# Bind("hostel_BlockNames") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Floor" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="hostel_BlockLevels" runat="server" Text='<%# Bind("hostel_BlockLevels") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Room Name" ItemStyle-Width="150">
                        <ItemTemplate>
                            <asp:Label ID="room_Name" runat="server" Text='<%# Bind("room_Name") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:TextBox ID="txtroom_Name" Width="80px" class="id1" runat="server" Text='<%# Eval("room_Name") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Capacity" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="room_Capacity" runat="server" Text='<%# Bind("room_Capacity") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:TextBox ID="txtroom_Capacity" Width="80px" class="id1" runat="server" Text='<%# Eval("room_Capacity") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date Registered" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="created_date" runat="server" Text='<%# Bind("created_date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" UpdateImageUrl="~/img/correct image 2.png" CancelImageUrl="~/img/minus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Room Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>

