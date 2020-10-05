<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="coordinator_Update.ascx.vb" Inherits="KPP_MS.coordinator_Update" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Register Coordinator</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px;">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlstaff" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; text-align: left; padding-left: 23px; margin-top: 10px;">
        <asp:DropDownList ID="ddlcourse" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlsubject" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddllevel" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Backs &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>
