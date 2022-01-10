<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_list_table.ascx.vb" Inherits="KPP_MS.payment_list_table" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Payment Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px;text-align: left; padding-left: 23px">
        <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddllevel" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlInvGroup" runat="server" AutoPostBack="true" CssClass=" btn btn-default font ddl"></asp:DropDownList>
    </div>
<p></p>

<div style="display: inline-block;">
    <div id="editor"></div>
    <asp:Label ID="FEE_Lists" runat="server" Style="text-align: center"></asp:Label>
</div>

<p></p>
<div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px; text-align: left">
    <button id="Btnprint" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 23px" title="Print">Print &#160;<i class="fa fa-print w3-large w3-text-white"></i></button>
</div>
</div>
<br />
