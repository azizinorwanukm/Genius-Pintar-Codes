<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_coco_list_table.ascx.vb" Inherits="KPP_SYS.student_coco_list_table" %>

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
    .messagealert {
        width: 40%;
        position: fixed;
        bottom: 25px;
        right: 0px;
        z-index: 100000;
        padding: 0;
        font-size: 15px;
    }

    .sc3::-webkit-scrollbar {
        height: 8px;
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

<script>
    function readURL(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#student_Photo').attr('src', e.target.result);
            }
            reader.readAsDataURL(input.files[0]);
        }
    }
    $("#uploadPhoto").change(function () {
        readURL(this);
    });
</script>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp Co-Curricular &nbsp / &nbsp  Co-Curricular Information &nbsp / &nbsp
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">

    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF; overflow-x: auto; white-space: nowrap;" class="sc3">
        <button id="btnViewCocu" runat="server" style="display: inline-block; font-size: 0.8vw">View Co-Curriculum Information</button>
        <button id="btnRegisterCocu" runat="server" style="display: inline-block; font-size: 0.8vw">Register Co-Curriculum Information</button>
        <button id="btnViewAchievement" runat="server" style="display: inline-block; font-size: 0.8vw">View Achievement Information</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; overflow-y: scroll; white-space: nowrap; height: 70vh" id="ViewCocurricular" runat="server" class="sc4">

        <div class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent_Cocurricular" runat="server" class="table w3-text-black " AutoGenerateColumns="false"
                BackColor="#FFFAFA" DataKeyNames="std_ID" BorderStyle="None" GridLines="None" Width="97%"
                HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="Tahun" runat="server" Text='<%# Bind("Tahun") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class" ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:Label ID="Kelasa" runat="server" Text='<%# Bind("Kelas") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uniform" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="Uniform" runat="server" Text='<%# Bind("Uniform") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Club" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="Persatuan" runat="server" Text='<%# Bind("Persatuan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sport" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="Sukan" runat="server" Text='<%# Bind("Sukan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Sport Houses" ItemStyle-Width="22.5%">
                        <ItemTemplate>
                            <asp:Label ID="RumahSukan" runat="server" Text='<%# Bind("RumahSukan") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Cocurricular Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; height: 70vh" id="RegisterCocurricular" runat="server">

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border: hidden; margin-left: 1vw">
            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlYear_Kokurrikulum" runat="server" AutoPostBack="true" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Class </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlClass_Kokurrikulum" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" Enabled="false"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Uniform </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlUniform_Kokurrikulum" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp; <asp:Label CssClass="Label" runat="server"> Total Student : </asp:Label><asp:Label ID="lblCountUniform" runat="server" CssClass="Label"></asp:Label>/<asp:Label ID="lblMaxUniform" runat="server" CssClass="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Club </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlClub_Kokurrikulum" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw" ></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp; <asp:Label CssClass="Label" runat="server"> Total Student : </asp:Label><asp:Label ID="lblCountClub" runat="server" CssClass="Label"></asp:Label>/<asp:Label ID="lblMaxClub" runat="server" CssClass="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Sport </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSport_Kokurrikulum" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp; <asp:Label CssClass="Label" runat="server"> Total Student : </asp:Label><asp:Label ID="lblCountSport" runat="server" CssClass="Label"></asp:Label>/<asp:Label ID="lblMaxSport" runat="server" CssClass="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Sport Houses </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                    <asp:DropDownList ID="ddlSportHouses_Kokurrikulum" runat="server" CssClass=" btn btn-default font" Style="font-size: 0.8vw"></asp:DropDownList>
                </td>
                <td>
                    <p></p>
                    &nbsp; <asp:Label CssClass="Label" runat="server"> Total Student : </asp:Label><asp:Label ID="lblCountSportHouses" runat="server" CssClass="Label"></asp:Label>/<asp:Label ID="lblMaxSportHouses" runat="server" CssClass="Label"></asp:Label>
                </td>
            </tr>

            <tr id="HideForAPP" runat="server">
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Swimming </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox runat="server" ID="txtSwimming_Kokurrikulum" Style="width: 10vw" CssClass="textboxcss" Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>

        <br />

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh; display: inline-block">
            <button id="btnRegister_Koko" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Register Information</button>
        </div>

    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; height: 70vh" id="ViewAchievement" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw;">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlYear_Achievement" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" Style="font-size: 0.8vw"></asp:DropDownList>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Participation And Achievement : </asp:Label>
            <asp:TextBox ID="txtarea_achievement" runat="server" TextMode="MultiLine" Style="font-size: 0.8vw; overflow-y: scroll; white-space: nowrap; height: 45vh; width: 150vh" placeholder="1. International Badminton Competition" class="sc4"></asp:TextBox>
        </div>

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; padding-top: 2vh; display: inline-block">
            <button id="btnUpdateAchievement" runat="server" class="btn btn-success" style="top: 1vh; display: inline-block; font-size: 0.8vw">Update Achievement Information</button>
        </div>
    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
