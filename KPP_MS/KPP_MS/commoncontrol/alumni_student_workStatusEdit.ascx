<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="alumni_student_workStatusEdit.ascx.vb" Inherits="KPP_MS.alumni_student_workStatusEdit" %>

<style>
    .ddl {
        border-radius: 25px;
    }
    .auto-style1 {
        margin-left: 40px;
    }
</style>

<style type="text/css">

.ui-datepicker { 
    font-size:8pt
    }
</style>
<link type="text/css" href="css/ui-lightness/jquery-ui-1.8.19.custom.css" rel="stylesheet" />
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<link rel="stylesheet" href="/resources/demos/style.css">
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>

<script type ="text/javascript" >
                    $( function() {
                         $( "#txt_dateFrom" ).datepicker();
                         } );
                </script>
                                                                                                


<div style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p class="w3-text-white gridViewRespond" style="background-color: #800000; text-align: center; width: 100%; border-radius: 25px">Insert / Update Current Education Detail</p>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 20px">
        
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Employer Name : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_empName" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <div class="col-md-6 w3-text-black" style="text-align: left; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> Position : </asp:Label>
            <asp:TextBox CssClass="textbox" class="form-control" ID="txt_position" Style="width: 100%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
        <br />
        <div>
            <asp:Label runat ="server"> Date :</asp:Label>
        <div class="col-md-6 w3-text-black" style="text-align: center ; padding-left: 23px">
            <p></p>
            <asp:Label CssClass="Label" runat="server"> From : </asp:Label>
            <asp:TextBox  CssClass="textbox" class="form-control" ID="txt_dateFrom" Style="width: 15%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>            
       
            <asp:Label CssClass="Label" runat="server"> To : </asp:Label>
            <asp:TextBox  CssClass="textbox" class="form-control" ID="txt_dateTo" Style="width: 15%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>            
       </div>
            </div>
        <div></div>
    <div class="row gridViewRespond" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 10px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <button id="Btnsimpan" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Save">Save &#160;<i class="fa fa-save w3-large w3-text-white"></i></button>
        <button id="Btnback" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Back">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>
    </div>
    </div>
