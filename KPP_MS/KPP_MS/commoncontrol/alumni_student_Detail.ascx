<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="alumni_student_Detail.ascx.vb" Inherits="KPP_MS.alumni_student_Detail" %>
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
                
                <label for="ctl00_ContentPlaceHolder1_student_Detail_uploadPhoto">
                    <asp:Image ID="student_Photo" runat="server" class="w3-circle blah" />
                </label>
                <asp:FileUpload ID="uploadPhoto" runat="server" class="w3-text-black" onchange="readURL(this)" />
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:Label CssClass="textbox" ID="student_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student IC Number : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
                <asp:Label CssClass="textbox" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server" Enabled ="false" ></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student ID : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:Label CssClass="textbox" ID="student_ID" Style="width: 100%; border-radius: 25px;" runat="server" Enabled ="false" ></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Student Email Address : </asp:Label>
                <asp:Label CssClass="textbox" ID="student_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
             <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Phone No : </asp:Label>
                <asp:Label CssClass="textbox" ID="student_FonNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Gender : </asp:Label>
                <asp:Label CssClass="textbox" ID="student_Sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Race : </asp:Label>
                <asp:label ID="ddlRace" runat="server"  CssClass="Label"  Style="width: 20%"></asp:label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Religion : </asp:Label>
                <asp:label ID="ddlReligion" runat="server"  CssClass="Label"  Style="width: 20%"></asp:label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Address : </asp:Label>
                <asp:Label CssClass="textbox" ID="student_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> City : </asp:Label>               
                <asp:label runat="server" CssClass="Label"  Style="width: 20%" ID="ddlcity"></asp:label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> State : </asp:Label>
                <asp:label runat="server" CssClass="Label"  Style="width: 20%" ID="ddlstate"></asp:label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> PostCode : </asp:Label>
                <asp:Label CssClass="textbox" ID="student_PostCode" Style="width: 100%; border-radius: 25px" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Registered Year : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
                <asp:Label CssClass="Label" runat ="server" ID="ddlyear" Style="width: 20%" ></asp:Label>
            </div>
            
        </div>
        <p></p>
    </div>
</div>
<br />