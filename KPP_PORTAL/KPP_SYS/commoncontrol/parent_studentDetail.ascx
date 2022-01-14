<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parent_studentDetail.ascx.vb" Inherits="KPP_SYS.parent_studentDetail" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajar_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_info()" value="0">Student Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="std_info">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black image-upload" style="text-align: center; padding-left: 23px">
                <p>Click the image to upload</p>
                <label for="ctl00_ContentPlaceHolder1_parent_studentDetail_uploadPhoto">
                    <asp:Image ID="student_Photo" runat="server" class="w3-circle blah" />
                </label>
                <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black" onchange="readURL(this)" />
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student IC Number : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student ID : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_ID" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student Email Address : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Phone No : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_FonNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Gender : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_Sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Race : </asp:Label>
                <asp:DropDownList ID="ddlRace" runat="server" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Religion : </asp:Label>
                <asp:DropDownList ID="ddlReligion" runat="server" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Address : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> City : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="std_txtCity" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> State : </asp:Label>
                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> PostCode : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_PostCode" Style="width: 100%; border-radius: 25px" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Year : </asp:Label>
                 <asp:TextBox CssClass="textbox" ID="txtYear" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student Level : </asp:Label>
                 <asp:TextBox CssClass="textbox" ID="txtStudent_Level" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Sem : </asp:Label>
                 <asp:TextBox CssClass="textbox" ID="txtStudent_Sem" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
            <button id="btnStudentUpdate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
            <asp:Label ID="Label1" CssClass="w3-text-black" runat="server"></asp:Label>
        </div>
        <p></p>
    </div>
</div>
<br />
