<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="hostel_Update.ascx.vb" Inherits="KPP_MS.hostel_Update" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
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

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" disabled class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Hostel Information<i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Name : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Name" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Level : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Level" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
           <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
           <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Semester : </asp:Label>
            <asp:DropDownList ID="ddlSem" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server">Number of Rooms : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="roomNumbers" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
    </div>
    <div id="newRoom" class="w3-modal">
      <div class="gridViewRespond w3-container w3-text-black w3-modal-content ddl" style="width: 50%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
         <button type="button" disabled class="btn btn-info" style="background-color: #800000; display: inline-block; width: 500%; border-radius: 25px; width: 100%">Edit Room</button>
         <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
             <div class="w3-container ddl">
                 <div>
                     <label for="txtRoomName">Room Name : </label>
                     <asp:TextBox runat="server" ID="txtRoomName" Text="" CssClass="w3-input"></asp:TextBox>
                 </div>
                 <div>
                     <label for="txtRoomCapacity">Room Capacity : </label>
                     <asp:TextBox runat="server" ID="txtRoomCapacity" Text="" CssClass="w3-input"></asp:TextBox>
                 </div>
                 <asp:Button runat="server" ID="btnUpdate" OnClick="AddNewRoom" CssClass="w3-button w3-green" Text="Save"  />
                 <span onclick="document.getElementById('newRoom').style.display='none';" class="w3-button w3-red">Close</span>
             </div>    
         </div>
      </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <%--<button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>--%>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
        <button id="BtnModal" type="button" onclick="document.getElementById('newRoom').style.display='block';" class="btn btn-info w3-right-align" style="border-radius: 25px;">New Room &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    <p></p>
</div>
<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" disabled class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Hostel Floor Information</button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <asp:Repeater runat="server" ID="floorInfo">
            
            <ItemTemplate>
                <asp:HiddenField runat="server" Value='<%# Eval("room_ID") %>' ID="roomID" />
                <div class="w3-card-4 w3-col m4 l3">
                    <header class="w3-container" style="background-color:#800000;">
                        <h5><%# Eval("room_Name") %></h5>
                    </header>

                    <div class="w3-container w3-left-align">
                      <p class="w3-text-black ddl">
                        Room Capacity: <%# Eval("room_Capacity") %>
                      </p>
                      <p class="w3-text-black ddl">
                        For : <%# Eval("year") %>, <%# Eval("RoomSem") %>
                      </p>
                      <p class="w3-text-black ddl">
                        Room Registered: <%# Eval("created_date") %>
                      </p>
                    </div>
                    <div>
                        <!-- The Modal -->
                        <div id='<%# Eval("room_ID") %>' class="w3-modal">
                          <div class="gridViewRespond w3-container w3-text-black w3-modal-content ddl" style="width: 50%; background-color: #f2f2f2; text-align: center; border-radius: 25px;">
                             <button type="button" disabled class="btn btn-info" style="background-color: #800000; display: inline-block; border-radius: 25px; width: 100%">Edit Room</button>
                             <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
                                 <div class="w3-container ddl">
                                     <div>
                                         <label for="txtRoomName">Room Name:</label>
                                         <asp:TextBox runat="server" ID="txtRoomName" Text='<%# Eval("room_Name") %>' CssClass="w3-input"></asp:TextBox>
                                     </div>
                                     <div>
                                         <label for="txtRoomSem"></label>
                                     </div>
                                     <div>
                                         <label for="txtRoomCapacity">Room Capacity:</label>
                                         <asp:TextBox runat="server" ID="txtRoomCapacity" Text='<%# Eval("room_Capacity") %>' CssClass="w3-input"></asp:TextBox>
                                     </div>
                                     <span onclick="document.getElementById('<%# Eval("room_ID") %>').style.display='none';" class="w3-button w3-red">Close</span>
                                     <asp:Button runat="server" ID="btnUpdate" OnClick="UpdateRoom" CssClass="w3-button w3-green" Text="Save"  />
                                 </div>    
                             </div>
                          </div>
                        </div>
                    </div>

                    <button type="button" onclick="document.getElementById('<%# Eval("room_ID") %>').style.display='block';" class="w3-button w3-green w3-text-black">Edit</button>
                    <asp:Button runat="server" ID="btnRemove" OnClick="DeleteRoom" CssClass="w3-button w3-red" Text="Remove" OnClientClick="javascript:return confirm('Are you sure to delete this room data ? ')" />
                    
                    <footer class="w3-container w3-blue"></footer>
                </div>
            </ItemTemplate>

        </asp:Repeater>
    </div>
</div>
<div class="messagealert" id="alert_container" style="text-align: center"></div>

<script type ="text/javascript">

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