<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="alumni_Student_Status.ascx.vb" Inherits="KPP_MS.alumni_Student_Status" %>

<style>
    .image-upload > input {
        display: none;
    }

    .ddl {
        border-radius: 25px;
    }
</style>



<div class="gridViewRespond1" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="pelajar_personalStatus" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="student_status()" value="0">Personal Status <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="personal_status">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Status : </asp:Label>
                <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="false" CssClass=" btn btn-default font ddl" Style="width: 100%;">
                    <asp:ListItem Text="Please Select"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Age Married : </asp:Label>
                <asp:TextBox ID="txtAgeMarried" runat="server" CssClass="textbox" Style="width: 100%; border-radius: 25px;">
                </asp:TextBox>
            </div>

            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Year Married :</asp:Label>
                <asp:TextBox ID="txtYearMarried" runat="server" CssClass="textbox" Style="width: 100%; border-radius: 25px;">
                </asp:TextBox>
            </div>
            <br />

        </div>
        <br />
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
            <button id="update" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white">Save</i></button>
        </div>
        <p></p>
    </div>
</div>
<br />
