<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="hostel_Create.ascx.vb" Inherits="KPP_SYS.hostel_Create" %>

<style>
    .image-upload > input{
        display:none;
    }
     .ddl {
        border-radius: 25px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Register New Hostel & Room Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Hostel Name : </asp:Label>
            <asp:DropDownList ID="ddlHostelName" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Name : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Name" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Block Level : </asp:Label>
            <asp:DropDownList ID="ddlBlock_Level" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Year : </asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Room Quantity : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="room_Quantity" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
    </div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top:10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;"><i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>


