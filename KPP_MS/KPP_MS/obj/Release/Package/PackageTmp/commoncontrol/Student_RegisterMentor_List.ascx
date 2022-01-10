<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="Student_RegisterMentor_List.ascx.vb" Inherits="KPP_MS.Student_RegisterMentor_List" %>

<style>
    .sc3::-webkit-scrollbar {
        height: 5px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
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
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Research &nbsp; / &nbsp;
        <asp:Label runat="server" ID="txtbreadcrum1" Style="text-align: left;"></asp:Label>
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF;">
        <button id="btnViewStudentMentor" runat="server" style="top: 8px; display: inline-block; font-size:0.8vw">View Student Mentor</button>
        <button id="btnRegisterStudentMentor" runat="server" style="top: 8px; display: inline-block; font-size:0.8vw">Register Student Mentor</button>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="RegisterStudentMentor" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddl_year" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"  style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Group Name : </asp:Label>
            <asp:DropDownList ID="ddl_group" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <table class="w3-text-black font" style="text-align: left; padding-left: 1vw; border:hidden; margin-left:1.2vw">

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Mentor  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox CssClass="textboxcss" runat="server" ID="txtmentor" Style="width: 30vw; font-size:0.8vw"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Faculty  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox CssClass="textboxcss"  runat="server" ID="txtmentor_faculty" Style="width: 30vw; font-size:0.8vw"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Co-Mentor  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox CssClass="textboxcss" runat="server" ID="txtcomentor" Style="width: 30vw; font-size:0.8vw"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <p></p>
                    <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Faculty  </asp:Label>
                </td>
                <td>
                    <p></p>
                    &nbsp : &nbsp
                   <asp:TextBox  CssClass="textboxcss" runat="server" ID="txtcomentor_faculty" Style="width: 30vw; font-size:0.8vw"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <p></p>
                    <button id="btnAdd" runat="server" class="btn btn-success" style="top: 8px; display: inline-block; font-size:0.8vw">Add Student Mentor</button>
                </td>
            </tr>

        </table>
    </div>

   <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="ViewStudentMentor" runat="server">

        <div class="w3-text-black" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlViewYear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw"></asp:DropDownList>
        </div>
        <div class="w3-text-black" style="text-align: left; padding-left: 1.2vw; display: inline-block">
            <asp:Label CssClass="Label font" runat="server" Style="width: 100%"> Group Name : </asp:Label>
            <asp:DropDownList ID="ddlViewGroup" runat="server" CssClass=" btn btn-default font" AutoPostBack="true" style="font-size:0.8vw"></asp:DropDownList>
        </div>

        <br />
        <br />

        <div style="overflow-y: scroll; height: 56vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black gridViewRespond" AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="ri_id" Width="97%" HeaderStyle-HorizontalAlign="Left" BorderStyle="None" GridLines="None">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mentor" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="ri_mentor" class="id1" runat="server" Text='<%# Eval("ri_mentor") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Faculty" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="ri_mentorfaculty" class="id1" runat="server" Text='<%# Eval("ri_mentorfaculty") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Co-Mentor" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="ri_comentor" class="id1" runat="server" Text='<%# Eval("ri_comentor") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Faculty" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="ri_comentorfaculty" class="id1" runat="server" Text='<%# Eval("ri_comentorfaculty") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton Width="25" Height="25" ID="btnDelete" CommandName="Delete" OnClientClick="javascript:return confirm('Are you sure to delete this data ? ')" runat="server" ImageUrl="~/img/x image 2.png" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Student Mentor Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>
</div>

<div class="messagealert" id="alert_container" style="text-align: center"></div>
