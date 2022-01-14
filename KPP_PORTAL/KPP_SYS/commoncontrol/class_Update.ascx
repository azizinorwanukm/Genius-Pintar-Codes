<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="class_Update.ascx.vb" Inherits="KPP_SYS.class_Update" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Class Information<i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Class Name : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="class_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Class Year : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="class_year" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Class Level : </asp:Label>
            <asp:DropDownList style="width:190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddlclass_Level" runat="server" AutoPostBack="true" ></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Staff Name : </asp:Label>
            <asp:DropDownList style="width:190px; border-radius: 25px;" CssClass="w3-text-black btn btn-default font ddl" ID="ddlstaff_ID" runat="server" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> No. of Student : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtstd_number" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    <p></p>
</div>
