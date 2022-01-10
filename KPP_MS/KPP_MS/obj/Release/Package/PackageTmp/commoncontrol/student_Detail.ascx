<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_Detail.ascx.vb" Inherits="KPP_MS.student_Detail" %>
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
                <label for="ctl00_ContentPlaceHolder1_student_Detail_uploadPhoto">
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
                <asp:TextBox CssClass="textbox" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server" Enabled ="false" ></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student ID : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:TextBox CssClass="textbox" ID="student_ID" Style="width: 100%; border-radius: 25px;" runat="server" Enabled ="false" ></asp:TextBox>
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
                <asp:DropDownList ID="ddlCity" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
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
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Registered Year : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student Year : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Sem : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:DropDownList ID="ddlSem" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Reset Password : </asp:Label>
                <button style="border-radius: 25px; background-color: #ffd800;" type="button" class="btn btn-info" data-toggle="modal" data-target="#myModal"><i class="fa fa-exclamation-triangle" aria-hidden="true"></i></button>
                <div id="myModal" class="modal fade" role="dialog">
                    <div class="modal-dialog">
                        <!— Modal content-->
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title">Are you sure?</h4>
                        </div>
                        <div class="modal-body">
                            <p>Are you sure you want to reset this user's password?</p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button CssClass="btn btn-info" ID="resetButton" runat="server" Text="Yes" />
                            <button type="button" class="btn btn-primary" data-dismiss="modal">No</button>
                        </div>
                    </div>

                    </div>
                </div>
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
