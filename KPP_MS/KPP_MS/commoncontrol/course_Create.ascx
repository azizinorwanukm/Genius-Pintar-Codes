<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="course_Create.ascx.vb" Inherits="KPP_MS.course_Create" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajarCreate_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_create()" value="0">Create Course <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="create_info">

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <asp:Label CssClass="Label" runat="server"> Course Name (English) : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="subject_Name" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
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
                <asp:Label CssClass="Label" runat="server"> Course Credit Hour : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="subject_CreditHour" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
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
            <div class="col-md-7 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Course Group : </asp:Label>
                <asp:DropDownList ID="ddlCourse_group" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;"></asp:DropDownList>
            </div>
            <div class="col-md-5 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Course Year : </asp:Label>
                <asp:DropDownList ID="ddlsubject_year" runat="server" CssClass="btn btn-default dll" Style="border-radius: 25px;"></asp:DropDownList>
            </div>
        </div>

        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; text-align: left; padding-left: 23px; margin-bottom:10px">
            <button id="btnCourseCreate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px; display: inline" title="ADD Course">Save &#160; <i class="fa fa-save w3-large w3-text-white"></i></button>
            <button id="btnEditCourse" type="button" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px; display: inline" onclick="edit_Course()" value="0" title="Back">Back &#160; <i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
        </div>

    </div>
</div>

