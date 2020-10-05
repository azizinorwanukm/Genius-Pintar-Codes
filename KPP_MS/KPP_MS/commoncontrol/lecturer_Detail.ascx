<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_Detail.ascx.vb" Inherits="KPP_MS.lecturer_Detail" %>
<style>
    .image-upload > input {
        display: none;
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
                <asp:TextBox CssClass="textbox " ID="txtCity" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> State : </asp:Label>
                <asp:DropDownList ID="staff_State" Style="width: 100%; border-radius: 25px;" runat="server" CssClass="btn btn-default ddl">
                </asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> PostalCode : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="staff_Posscode" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Position 1 : </asp:Label>
                <asp:DropDownList ID="staff_Position_P1" Style="width: 100%; border-radius: 25px;" runat="server" CssClass="btn btn-default ddl">
                </asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Position 2 : </asp:Label>
                <asp:DropDownList ID="staff_Position_P2" Style="width: 100%; border-radius: 25px;" runat="server" CssClass="btn btn-default ddl">
                </asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Position 3 : </asp:Label>
                <asp:DropDownList ID="staff_Position_P3" Style="width: 100%; border-radius: 25px;" runat="server" CssClass="btn btn-default ddl">
                </asp:DropDownList>
            </div>
        </div>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
            <button id="btnLecturerUpdate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
        <p></p>
    </div>
</div>
<br />
