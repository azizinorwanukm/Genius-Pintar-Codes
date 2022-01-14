<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_list_table.ascx.vb" Inherits="KPP_SYS.payment_list_table" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%"> Payment Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>

    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-2 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddlyear" runat="server" AutoPostBack="true"  CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-2 w3-text-black" style="text-align: left">
            <asp:DropDownList ID="ddllevel" runat="server" AutoPostBack="true"  CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>

    <div style=" display:inline-block;">
    <asp:Label ID="FEE_Lists" runat="server" style="text-align:center"></asp:Label>
    </div>

    <p></p>
    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px; text-align:left">
        <button id="Btnprint" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; margin-left: 23px" title="Print" ><i class="fa fa-print w3-large w3-text-white"></i></button>
    </div>
</div>
<br />
