<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="course_Create.ascx.vb" Inherits="KPP_SYS.course_Create" %>

<p style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">New Course Registration</p>

<div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Name (English) : </asp:Label>
        <asp:TextBox CssClass="textbox" ID="subject_Name" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Name (Malay) : </asp:Label>
        <asp:TextBox CssClass="textbox" ID="subject_NameBM" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Code : </asp:Label>
        <asp:TextBox CssClass="textbox" ID="subject_code" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Year : </asp:Label>
        <asp:TextBox CssClass="textbox" ID="subject_year" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Type : </asp:Label>
        <asp:DropDownList ID="subject_type" runat="server" CssClass="btn btn-default font dll" style="border-radius: 25px;">
           <asp:ListItem Value="" Text="select course type..."></asp:ListItem>
        </asp:DropDownList>
    </div>
     <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Religion : </asp:Label>
        <asp:DropDownList ID="ddl_subjectreligions" runat="server" CssClass="btn btn-default font dll" style="border-radius: 25px;"> </asp:DropDownList>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Course Student Year : </asp:Label>
        <asp:DropDownList ID="subject_StudentYear" runat="server" CssClass="btn btn-default font dll" style="border-radius: 25px;">
           <asp:ListItem Value="" Text="select course student year..."></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
        <p></p>
        <asp:Label CssClass="Label" runat="server"> Sem : </asp:Label>
        <asp:DropDownList ID="subject_sem" runat="server" CssClass="btn btn-default font dll" style="border-radius: 25px;">
           <asp:ListItem Value="" Text="select sems..."></asp:ListItem>
        </asp:DropDownList>
    </div>
</div>

<div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
    <button id="btnCourseCreate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; display: inline" title="ADD Course"><i class="fa fa-save w3-large w3-text-white"></i></button>
    <button id="btnEditCourse" type="button" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;display:inline" onclick="edit_Course()" value="0" title="Edit Course"><i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
</div>