<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="alumni_student_EditEduback.ascx.vb" Inherits="KPP_MS.alumni_student_EditEduback" %>

<style>
    .ddl {
        border-radius: 25px;
    }
    .auto-style1 {
        margin-left: 40px;
    }
</style>

<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Insert / Update Current Education Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Qualification Type : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="ddl_qualificationType" Style="width: 100%; border-radius: 25px;" runat="server" ></asp:DropDownList>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Institute / University Name : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_insUniName" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Location : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_location" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Country : <i class="fa fa-certificate fa fa-fw w3-text-red w3-small"> </i></asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="ddl_Country" Style="width: 100%; border-radius: 25px;" runat="server" ></asp:DropDownList>
       </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Course : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_course" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Field : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_field" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Year Graduate : </asp:Label>
            <asp:TextBox CssClass="textbox " class="form-control" ID="txt_yearGrad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Age On Graduation : </asp:Label>
            <asp:TextBox CssClass="textbox " class="form-control" ID="txt_ageGrad" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Result (Cumulative CGPA) : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_result" Style="width: 100%; border-radius: 25px;" runat="server"></asp:TextBox>
        </div>
        <div class="col-md-3 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server" Style="width: 20%"> Scholarship : </asp:Label>
            <asp:DropDownList CssClass="btn btn-default ddl" ID="ddl_scholarship" Style="width: 100%; border-radius: 25px;" runat="server" ></asp:DropDownList>
        </div>
        <div></div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    </div>
