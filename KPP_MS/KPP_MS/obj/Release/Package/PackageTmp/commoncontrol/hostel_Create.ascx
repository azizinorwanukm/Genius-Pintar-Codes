<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="hostel_Create.ascx.vb" Inherits="KPP_MS.hostel_Create" %>

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

    function accordion(divID) {
      var x = document.getElementById(divID);
      if (x.className.indexOf("w3-show") == -1) {
        x.className += " w3-show";
      } else { 
        x.className = x.className.replace(" w3-show", "");
      }
    }
</script>

<style>
    .image-upload > input{
        display:none;
    }
     .ddl {
        border-radius: 25px;
    }
    .room-size{
        display:flex;
        align-items:center;
        background-color:#800000;
        color: black;
    }
    .w3-row{
      display: flex;
      flex-wrap: nowrap;
      overflow-x: auto;
    }
    .l3 {
      flex: 0 0 auto;
      padding:2px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Register New Hostel & Room Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Semester : </asp:Label>
            <asp:DropDownList ID="ddlSem" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Name : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Level : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Level" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Room Quantity : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtRoomQuantity" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
    </div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top:10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    
    <div class="messagealert" id="alert_container" style="text-align: center"></div> 
</div>

<div runat="server" id="roomDiv" visible="false" style="margin-top: 5px;">
    
    <div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Registered Hostel & Room Detail</p>
        <div class="row gridViewRespond w3-text-black" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">

            <div>
                <button onclick="accordion('hostel')" class="ddl w3-btn w3-block w3-left-align" type="button" style="background-color: #800000;">Hostel Detail</button>
                <div id="hostel" class="w3-container w3-hide w3-text-black w3-left-align w3-panel">
                    <div>
                        <asp:Label runat="server">Hostel Name: </asp:Label>
                        <asp:Label runat="server" ID="lblHostelName"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server">Hostel Level: </asp:Label>
                        <asp:Label runat="server" ID="lblHostelLevel"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server">Year: </asp:Label>
                        <asp:Label runat="server" ID="lblHostelYear"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server">Semester: </asp:Label>
                        <asp:Label runat="server" ID="lblHostelSem"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server">Rooms: </asp:Label>
                        <asp:Label runat="server" ID="lblHostelRoom"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server">Created On: </asp:Label>
                        <asp:Label runat="server" ID="lblCreatedDate"></asp:Label>
                    </div>
                    <div>
                        <asp:Label runat="server">Created By: </asp:Label>
                        <asp:Label runat="server" ID="lblCreatedBy"></asp:Label>
                    </div>
                </div>
            </div>
            <div>
                <button onclick="accordion('room')" class="ddl w3-btn w3-block w3-left-align" type="button" style="background-color: #800000;">Room Detail</button>
                <div id="room" class="w3-container w3-hide w3-text-black w3-panel">
                  <div style="overflow-y: scroll; overflow-x: hidden; height: 450px" class="table-responsive w3-text-black">
                    <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                        BackColor="#d9d9d9" DataKeyNames="room_ID" BorderStyle="None" GridLines="None"
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

                            <asp:TemplateField HeaderText="Room Name" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="room_Name" class="id1" runat="server" Text='<%# Eval("room_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Semester" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="room_Sem" class="id1" runat="server" Text='<%# Eval("RoomSem") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Room Capacity" ItemStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="room_Capacity" class="id1" runat="server" Text='<%# Eval("RoomCapacity") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Created At" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="createdDate" class="id1" runat="server" Text='<%# Eval("RoomDateCreated") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label align="center" runat="server" Class="id1">No room registered</asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                        <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                    </asp:GridView>
                </div>
                </div>
            </div>

        </div>
    </div>

</div>
