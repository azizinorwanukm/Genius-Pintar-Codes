<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="lecturer_Create.ascx.vb" Inherits="KPP_MS.lecturer_Create" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Register New Staff Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Staff Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Staff Identification Card : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_Mykad" Style="width: 100%; border-radius: 25px;" runat="server" Text="" Placeholder="Please enter data without '-' "></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server">Staff ID : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="staff_ID" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server">Gender : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:RadioButtonList CssClass="radio" ID="staff_Sex" runat="server" Style="width: 100%; border-radius: 25px;" RepeatDirection="Horizontal">
            </asp:RadioButtonList>
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
            <asp:TextBox CssClass="textbox " class="form-control" ID="txtCity" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
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
            <asp:DropDownList CssClass="btn btn-default ddl" ID="staff_P1_Position" Style="width: 100%; border-radius: 25px;" runat="server"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Position 2 : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="staff_P2_Position" Style="width: 100%; border-radius: 25px;" runat="server"></asp:DropDownList>
        </div>
         <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Position 3 : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="staff_P3_Position" Style="width: 100%; border-radius: 25px;" runat="server"></asp:DropDownList>
        </div>
    </div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
