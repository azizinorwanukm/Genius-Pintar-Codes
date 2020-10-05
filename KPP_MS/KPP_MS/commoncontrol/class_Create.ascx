<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="class_Create.ascx.vb" Inherits="KPP_MS.class_Create" %>

<p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">New Class Registration</p>
<style>
    .ddl {
        border-radius: 25px;
    }
</style>
<div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px">
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Class Name : </asp:Label>
        <asp:TextBox CssClass="textbox" ID="class_Name" Style="width: 60%; border-radius: 25px;" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Class Year : </asp:Label>
        <asp:DropDownList Style="width: 190px; height: 29px;" CssClass="w3-text-black btn btn-default ddl" ID="ddlYear" runat="server"></asp:DropDownList>
    </div>
    <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Class Level : </asp:Label>
        <asp:DropDownList Style="width: 190px; height: 29px;" CssClass="w3-text-black btn btn-default ddl" ID="class_Level" runat="server"></asp:DropDownList>
    </div>
    <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Class Type : </asp:Label>
        <asp:DropDownList Style="width: 190px; height: 29px;" CssClass="w3-text-black btn btn-default ddl" ID="class_Type" runat="server" AutoPostBack="true"></asp:DropDownList>
    </div>
    <div></div>
    <div id="classExist" class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px;" runat="server" >
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Class Sem : </asp:Label>
        <asp:DropDownList Style="width: 190px; height: 29px;" CssClass="w3-text-black btn btn-default ddl" ID="class_Sem" runat="server"></asp:DropDownList>
    </div>
    <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px;">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Name : </asp:Label>
        <asp:DropDownList Style="width: 190px; height: 29px;" CssClass="w3-text-black btn btn-default ddl" ID="course_Name" runat="server"></asp:DropDownList>
    </div>
    <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Staff Name : </asp:Label>
        <asp:DropDownList Style="width: 190px;" CssClass="w3-text-black btn btn-default ddl" ID="staff_ID" runat="server"></asp:DropDownList>
    </div>
</div>
<div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
    <button id="btnClassCreate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
    <button id="btnEditClass" type="button" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px; display: inline" runat="server" title="Back">Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
</div>

