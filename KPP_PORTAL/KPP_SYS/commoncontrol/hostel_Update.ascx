<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="hostel_Update.ascx.vb" Inherits="KPP_SYS.hostel_Update" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Hostel Information<i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Hostel Name : </asp:Label>
            <asp:DropDownList ID="ddlHostelName" runat="server" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Name : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Name" runat="server" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Level : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Level" runat="server" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
           <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Room Capacity : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="room_Capacity" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Room Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="room_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    <p></p>
</div>
