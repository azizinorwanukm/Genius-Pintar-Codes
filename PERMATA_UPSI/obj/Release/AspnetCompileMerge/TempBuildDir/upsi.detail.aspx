<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.detail.aspx.vb" Inherits="permata_upsi.upsi_detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <style>
        .form-horizontal .control-label{
               text-align:left !important; 
            }
    </style>
    <script type="text/javascript">
        $("#<%=txtMyKidNo.ClientID%>").keypress(function (e) {
           
            if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {             
                
                return false;
            }
        });
    </script>
    <div class="container">
        <div class="row">
            <br /><br /><br />
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-info">
                    <div class="panel-heading" id="intro_title" runat="server" ></div>
                    <div class="panel-body">
                                                
                        <p id="intro1" runat="server"></p>
                        <p id="intro2" runat="server"></p>
                        <p id="intro3" runat="server"></p>
                        
                        <br />
                        <div class="form-group">
                            <label runat="server" id="lblName" class="control-label">Full name *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label runat="server" id="lblMyKidNo" class="control-label">MyKid No. *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtMyKidNo" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>                            
                        </div>                        
                        <div class="form-group">
                            <label runat="server" id="lblMotherName" class="control-label">Mother's name *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtMotherName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label runat="server" id="lblFatherName" class="control-label">Father's name *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtFatherName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div> 
                        

                        <div id="lblalert" class="alert alert-danger" style="display: none" onclick="$(this).hide()"></div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnSubmit" runat="server" Text="Kemaskini >>" CssClass="btn btn-outline btn-primary center-block" />
                                <asp:Button ID="btnNext" runat="server" Text="Halaman Seterusnya >>" CssClass="btn btn-outline btn-primary center-block" />
                            </ContentTemplate>                        
                        </asp:UpdatePanel>
                    </div>
                    
                </div>
            </div>
            <!-- /.col-lg-4 -->
        </div>
        <!-- /.row -->
    </div>
</asp:Content>
