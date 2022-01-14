<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="course_Update.ascx.vb" Inherits="KPP_SYS.course_Update" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Course Information </p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Name (English) : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="subject_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Name (Malay) : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="subject_NameBM" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Code : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="subject_Code" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Year : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="subject_year" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Course Type : </asp:Label>
            <asp:DropDownList ID="ddlsubjectType" AutoPostBack="false" runat="server" CssClass="btn btn-default font dll" style="border-radius: 25px;"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Course Student Year : </asp:Label>
            <asp:DropDownList ID="ddlsubject_StudentYear" AutoPostBack="false" runat="server" CssClass="btn btn-default font dll" style="border-radius: 25px;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Sem : </asp:Label>
            <asp:DropDownList ID="ddlsubject_Sem" AutoPostBack="false" runat="server" CssClass="btn btn-default font dll" style="border-radius: 25px;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Credit Hour : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="subject_credithour" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> No. of Student : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txtStdNumber" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white" ></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    <p></p>
</div>
