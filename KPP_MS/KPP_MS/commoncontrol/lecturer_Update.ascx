<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_Update.ascx.vb" Inherits="KPP_MS.lecturer_Update" %>


<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Update Staff Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-6 w3-text-black image-upload" style="text-align: center; padding-left: 23px">
            <p>Click the image to upload</p>
            <label for="ctl00_ContentPlaceHolder1_lecturer_Update_uploadPhoto">
                <asp:Image ID="staff_Photo" runat="server" class="w3-circle blah" />
            </label>
            <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black" onchange="readURL(this)" />
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Staff Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Staff Identification Card : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_Mykad" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server">Staff ID : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_ID" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server">Gender : </asp:Label>
            <asp:TextBox CssClass="textbox " class="form-control" ID="staff_sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server">Email : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Phone Number : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_MobileNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Home Address : </asp:Label>
            <asp:TextBox CssClass="textbox " class="form-control" ID="staff_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">City : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_City" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">State : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="staff_State" Style="width: 100%; border-radius: 25px;" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Postcode : </asp:Label>
            <asp:TextBox CssClass="textbox " class="form-control" ID="staff_Posscode" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Position 1 : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="staff_Position_P1" Style="width: 100%; border-radius: 25px;" runat="server"></asp:DropDownList>
        </div>
         <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Position 2 : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="staff_Position_P2" Style="width: 100%; border-radius: 25px;" runat="server"></asp:DropDownList>
        </div>
         <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Position 3 : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="staff_Position_P3" Style="width: 100%; border-radius: 25px;" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
    </div>
</div>
