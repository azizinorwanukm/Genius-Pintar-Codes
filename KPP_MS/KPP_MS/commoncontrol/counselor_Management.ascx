<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="counselor_Management.ascx.vb" Inherits="KPP_MS.counselor_Management" %>

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

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh;" class="w3-card-2">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black font">
        Menu &nbsp; : &nbsp; Counselor &nbsp; / &nbsp; Counselor Management &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewManagement" runat="server" style="display: inline-block; font-size: 0.8vw">View Management</button>
        <button id="btnSelfDevelopmentManagement" runat="server" style="display: inline-block; font-size: 0.8vw">Self Development Management</button>
        <button id="btnPersonalityDevelopmentManagement" runat="server" style="display: inline-block; font-size: 0.8vw">Personality Development Management</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="ViewManagement" runat="server" class="sc4">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddl_Year" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Program : </asp:Label>
            <asp:DropDownList ID="ddl_Program" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Level : </asp:Label>
            <asp:DropDownList ID="ddl_Level" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating"
                BackColor="#FFFAFA" DataKeyNames="CM_ID" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="30">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="CM_Year" class="id1" runat="server" Text='<%# Eval("CM_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="CM_Program" class="id1" runat="server" Text='<%# Eval("CM_Program") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Level" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="CM_Level" class="id1" runat="server" Text='<%# Eval("CM_Level") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assessed Name" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="CM_Name" class="id1" runat="server" Text='<%# Eval("CM_Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCM_Name" Width="100%" class="id1" runat="server" Text='<%# Eval("CM_Name") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Percentage " ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="CM_Percentage" class="id1" runat="server" Text='<%# Eval("CM_Percentage") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:TextBox ID="txtCM_Percentage" Width="100%" class="id1" runat="server" Text='<%# Eval("CM_Percentage") %>' />
                        </EditItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Edit" ButtonType="image" ShowEditButton="true" EditImageUrl="~/img/plus image 2.png" UpdateImageUrl="~/img/correct image 2.png" CancelImageUrl="~/img/minus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Self Development / Personality Development Management Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="SelfDevelopmentManagement" runat="server" class="sc4">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSDM_Year" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Program </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSDM_Program" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSDM_Level" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Assessed Name  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSDM_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Percentage </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="txtSDMPercentage" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <button id="btnSDM_Update" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Add New Management</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="PersonalityDevelopmentManagement" runat="server" class="sc4">
        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlPDM_Year" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Program </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlPDM_Program" runat="server" AutoPostBack="false" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Level </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlPDM_Level" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Assessed Name  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:DropDownList ID="ddlPDM_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Percentage </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="txtPDMPercentage" Style="width: 10vw" CssClass="textboxcss"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <button id="btnPDM_Update" runat="server" class="btn btn-success" style="top: 1vw; margin-left: 1vw; display: inline-block; font-size: 0.8vw">Add New Management</button>
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
