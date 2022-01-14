<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_guardian_one.ascx.vb" Inherits="KPP_SYS.student_guardian_one" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="penjaga1_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="guardian1_info()" value="0">Guardian 1 Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;font-size:14px" id="guard1_info">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Name : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> IC Number : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_IC" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Phone No : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_MobileNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Relationship : </asp:Label>
                <asp:Label CssClass="Label" ID="Parent_relationship" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Home Address : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> City : </asp:Label>
                <asp:Label ID="parent_City" runat="server" AutoPostBack="false" CssClass="Label"  Style="width: 100%;" ></asp:Label>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> State : </asp:Label>
                <asp:Label ID="parent_State" runat="server" AutoPostBack="false" CssClass="Label"  Style="width: 100%;" ></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> PostCode : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_Postcode" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Occupation : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_work" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Salary (RM) : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_Salary" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Office Phone No : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_OfficeNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Office Email Address : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_Work_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Email Address  : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Office Address : </asp:Label>
                <asp:Label CssClass="Label" ID="parent_WorkAddress" Style="width: 100%; border-radius: 25px;" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</div>
<br />