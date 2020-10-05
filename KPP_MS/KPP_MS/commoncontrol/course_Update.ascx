<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="course_Update.ascx.vb" Inherits="KPP_MS.course_Update" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%">Course Information </p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <asp:Label CssClass="Label" runat="server"> Course Name (English) : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="subject_Name" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">   
            <asp:Label CssClass="Label" runat="server"> Course Name (Malay) : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="subject_NameBM" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-12 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Code : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="subject_code" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Type : </asp:Label>
            <asp:DropDownList ID="subject_type" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;">
                <asp:ListItem Value="" Text="select course type..."></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Religion : </asp:Label>
            <asp:DropDownList ID="ddl_subjectreligions" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Student Year : </asp:Label>
            <asp:DropDownList ID="subject_StudentYear" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;">
                <asp:ListItem Value="" Text="select course student year..."></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Sem : </asp:Label>
            <asp:DropDownList ID="subject_sem" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;">
                <asp:ListItem Value="" Text="select sems..."></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Credit Hour : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="subject_credithour" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
        </div>
         <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
             <p></p>
            <asp:Label CssClass="Label" runat="server"> No. of Student : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtStdNumber" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Year : </asp:Label>
            <asp:DropDownList ID="ddlsubject_year" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;"></asp:DropDownList>
        </div>
        <div class="col-md-10 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course Group : </asp:Label>
            <asp:DropDownList ID="ddlCourse_group" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;"></asp:DropDownList>
        </div>
                
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    <p></p>
</div>
