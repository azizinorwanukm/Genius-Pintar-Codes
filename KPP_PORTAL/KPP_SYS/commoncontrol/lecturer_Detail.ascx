<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_Detail.ascx.vb" Inherits="KPP_SYS.lecturer_Detail" %>
<style>
    .image-upload > input{
        display:none;
    }
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pengajar_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="lecturer_info()" value="0">Staff Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="lecturer_info">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black image-upload" style="text-align: center; padding-left: 23px">
                <p>Click the image to upload</p>
                <label for="ctl00_ContentPlaceHolder1_lecturer_Detail_uploadPhoto">
                    <asp:Image ID="staff_Photo" runat="server" class="w3-circle blah" />
                </label>                
                <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black" onchange="readURL(this)" />
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Staff Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:TextBox CssClass="textbox " ID="staff_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Staff Identification Card : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:TextBox CssClass="textbox" ID="staff_MyKad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Staff ID : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:TextBox CssClass="textbox" ID="staff_ID" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Staff Email Address : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="staff_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Gender : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="staff_Sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Phone No : </asp:Label>
                <asp:TextBox CssClass="textbox " ID="staff_MobileNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Address : </asp:Label>
                <asp:TextBox CssClass="textbox " ID="staff_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> City : </asp:Label>
                <asp:DropDownList ID="staff_City" Style="width: 100%; border-radius: 25px;" runat="server" CssClass="btn btn-default font ddl">
                </asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> State : </asp:Label>
                <asp:DropDownList ID="staff_State" Style="width: 100%; border-radius: 25px;" runat="server" CssClass="btn btn-default font ddl">
                </asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> PostalCode : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="staff_Posscode" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Position : </asp:Label>
                <asp:DropDownList ID="staff_Position" Style="width: 100%; border-radius: 25px;" runat="server" CssClass="btn btn-default font ddl">
                </asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Reset Password : </asp:Label>
                <button style="border-radius: 25px; background-color: #ffd800;" type="button" class="btn btn-info" data-toggle="modal" data-target="#myModal"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i></button>
                <div id="myModal" class="modal fade" role="dialog">
                  <div class="modal-dialog">

                    <!-- Modal content-->
                    <div class="modal-content">
                      <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Are you sure?</h4>
                      </div>
                      <div class="modal-body">
                        <p>Are you sure you want to reset this user's password?</p>
                      </div>
                      <div class="modal-footer">
                          <asp:Button CssClass="btn btn-info" ID="resetButton" runat="server" Text="Yes" />
                        <button type="button" class="btn btn-primary" data-dismiss="modal">No</button>
                      </div>
                    </div>

                  </div>
                </div>
            </div>
        </div>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
            <button id="btnLecturerUpdate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
        <p></p>
    </div>
</div>
<br />
