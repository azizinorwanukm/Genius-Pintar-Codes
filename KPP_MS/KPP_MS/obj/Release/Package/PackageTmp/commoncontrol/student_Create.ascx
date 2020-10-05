<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="student_Create.ascx.vb" Inherits="KPP_MS.student_Create" %>

<script type="text/javascript">
    function guardian1_info() {
        if (document.getElementById("penjaga1_info").value == 0) {
            document.getElementById("guard1_info").style.display = "block";
            document.getElementById("penjaga1_info").value = 1;
        }

        else if (document.getElementById("penjaga1_info").value == 1) {
            document.getElementById("guard1_info").style.display = "none";
            document.getElementById("penjaga1_info").value = 0;
        }
    }

    function guardian2_info() {
        if (document.getElementById("penjaga2_info").value == 0) {
            document.getElementById("guard2_info").style.display = "block";
            document.getElementById("penjaga2_info").value = 1;
        }

        else if (document.getElementById("penjaga2_info").value == 1) {
            document.getElementById("guard2_info").style.display = "none";
            document.getElementById("penjaga2_info").value = 0;
        }
    }
</script>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Student Detail</p>
    <div class="row gridViewRespond w3-text-black" style="display: inline-block; width: 100%; border-radius: 25px;text-align: left;padding-left: 23px">
        <asp:Label CssClass="Label " runat="server" style="background-color:#D4CECD;border-radius: 25px;"  > Note : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i>  is mandatory to fill </asp:Label>
    </div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px;">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Student Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="student_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Student IC Number : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="student_Mykad" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student ID : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="student_ID" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Email : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="student_Email" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Phone Number : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="student_FonNo" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Gender : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            &nbsp;
            <asp:RadioButton ID="rbtn_Male" Text="Male &nbsp;&nbsp; " runat="server" GroupName="gender" />
            <asp:RadioButton ID="rbtn_Female" Text="Female" runat="server" GroupName="gender" />
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Race : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:DropDownList ID="ddlRace" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Religion : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:DropDownList ID="ddlReligion" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Home Address : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="student_Address" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> City : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="std_txtCty" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">State : </asp:Label>
            <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%">Postcode : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="student_PostalCode" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Student Level : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Year : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i></asp:Label>
            <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
        </div>
    </div>
    <p></p>
    <p></p>
    <p></p>
    <p></p>
    <button id="penjaga1_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px; width: 100%; margin-top: 20px" onclick="guardian1_info()" value="0">Guardian 1 Detail </button>
    <div style="display: none;" id="guard1_info">
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Guardian Name : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Guardian IC Number :<i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i> </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_IC" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Relationship : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_Status" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Guardian Email : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_Email" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Phone Number : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_MobileNo" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Home Address : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_HomeAddress" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> City : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="P1_txtCity" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">State : </asp:Label>
                <asp:DropDownList ID="ddlStateP1" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Postcode: </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_Postalcode" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Occupation : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_Work" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Office Email : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_Work_Email" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Salary (RM) : </asp:Label>
                <asp:DropDownList ID="ddlsalaryP1" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <%--            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Office Number : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_OfficeNo" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Office Address : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent1_WorkAddress" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>--%>
        </div>
    </div>
    <p></p>
    <p></p>
    <p></p>
    <p></p>
    <button id="penjaga2_info" type="button" class="btn btn-info" style="background-color: #800000; display: inline-block; width: 100%; margin-top: 20px; border-radius: 25px; width: 100%" onclick="guardian2_info()" value="0">Guardian 2 Detail </button>
    <div style="display: none;" id="guard2_info">
        <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Guardian Name : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_Name" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Guardian IC Number :<i class="fa fa-certificate fa fa-fw w3-text-red w3-small"></i> </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_IC" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Relationship : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_Status" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Guardian Email : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_Email" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Phone Number : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_MobileNo" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Home Address : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_HomeAddress" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> City : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="P2_txtCity" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">State : </asp:Label>
                <asp:DropDownList ID="ddlStateP2" runat="server" AutoPostBack="false" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Postcode: </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_PostalCode" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Occupation : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_Work" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Office Email : </asp:Label>
                <asp:TextBox CssClass="textbox" class="form-control" ID="Parent2_Work_Email" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
            </div>
            <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
                <p></p>
                <asp:Label CssClass="Label" runat="server" Style="width: 20%">Salary (RM) : </asp:Label>
                <asp:DropDownList ID="ddlsalaryP2" runat="server" CssClass=" btn btn-default ddl" Style="width: 100%;"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; margin-top: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
</div>


