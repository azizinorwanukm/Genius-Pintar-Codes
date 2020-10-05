<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parent_studenthomepage.ascx.vb" Inherits="KPP_MS.parent_studenthomepage" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajar_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_info()" value="0">Student Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;font-size:14px" id="std_info">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black image-upload" style="text-align: center; padding-left: 23px">
                <label for="ctl00_ContentPlaceHolder1_parent_studenthomepage_uploadPhoto">
                    <asp:Image ID="student_Photo" runat="server" class="w3-circle blah" />
                </label>
            </div>
           <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Name : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> IC Number : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student ID : </asp:Label>
                <asp:Label CssClass="Label" ID="student_ID" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Gender : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Sex" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
             <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Race : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Race" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Religion : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Religion" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
             <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Email Address : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Phone No : </asp:Label>
                <asp:Label CssClass="Label" ID="student_FonNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Address : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> City : </asp:Label>
                <asp:Label CssClass="Label" ID="student_City" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> State : </asp:Label>
                <asp:Label CssClass="Label" ID="student_State" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> PostCode : </asp:Label>
                <asp:Label CssClass="Label" ID="student_PostCode" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Level : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Level" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Student Year : </asp:Label>
                <asp:Label CssClass="Label" ID="student_Year" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
        </div>
        <p></p>
    </div>
</div>
<br />
