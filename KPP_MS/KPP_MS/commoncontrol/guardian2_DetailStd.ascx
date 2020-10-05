<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="guardian2_DetailStd.ascx.vb" Inherits="KPP_MS.guardian2_DetailStd" %>

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <button id="penjaga2_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%" onclick="guardian2_info()" value="0">Guardian 2 Information <i class="fa fa-fw fa fa-caret-down w3-left"></i></button>
    <div style="display: none;" id="guard2_info">
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Guardian Name : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_Name" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Guardian IC Number : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_IC" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Phone No : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_MobileNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Relationship : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="Parent_relationship" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Home Address : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_Address" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> City : </asp:Label>
                <!--<asp:TextBox CssClass="textbox" ID="parent_City" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>-->
                <asp:DropDownList ID="ddlparent_City" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl"  Style="width: 100%;" ></asp:DropDownList>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> State : </asp:Label>
                <!--<asp:TextBox CssClass="textbox" ID="parent_State" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>-->
                <asp:DropDownList ID="ddlparent_State" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl"  Style="width: 100%;" ></asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> PostCode : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_Postcode" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Occupation : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_work" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Salary (RM) : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_Salary" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Office Phone No : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_OfficeNo" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Office Email Address : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_Work_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Email Address  : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_Email" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server"> Office Address : </asp:Label>
                <asp:TextBox CssClass="textbox" ID="parent_WorkAddress" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px; text-align: left; padding-left: 23px">
            <button id="btnGuardianUpdate" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save"><i class="fa fa-save w3-large w3-text-white"></i></button>
        </div>
        <p></p>
    </div>
</div>
<br />
