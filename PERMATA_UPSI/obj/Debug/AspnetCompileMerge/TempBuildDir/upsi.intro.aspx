<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.intro.aspx.vb" Inherits="permata_upsi.upsi_intro" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
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
                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label runat="server" id="lblMyKidNo" class="control-label">MyKid No. *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtMyKidNo" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>                            
                        </div>                        
                        <div class="form-group">
                            <label runat="server" id="lblAddress" class="control-label">Contact address *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblPhone" class="control-label">Contact phone no. *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblEmail" class="control-label">Contact email *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblMotherName" class="control-label">Mother's name</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtMotherName" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label runat="server" id="lblMotherOccupation" class="control-label">Mother's occupation</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtMotherOccupation" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblFatherName" class="control-label">Father's name</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtFatherName" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblFatherOccupation" class="control-label">Father's occupation</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtFatherOccupation" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label runat="server" id="lblLearningCentreName" class="control-label">Name of learning centre/taska the child is currently attending *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtLearningCentreName" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblLearningCentreAddress" class="control-label">Address of the learning centre/taska *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtLearningCentreAddress" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblLearningCentreState" class="control-label">State of the learning centre/taska *</label>
                            <div>
                                <asp:DropDownList runat="server" ID="dlstLearningCentreState" DataValueField="stateid" DataTextField="statename" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label runat="server" id="lblLearningCentrePhoneNo" class="control-label">Phone No of the learning centre/taska *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtLearningCentrePhoneNo" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblAssistantName" class="control-label">Full name of the person assisting in taking the test *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtAssistantName" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 
                        <div class="form-group">
                            <label runat="server" id="lblAssistantPhoneNo" class="control-label">Contact no. of the person assisting in taking the test *</label>
                            <div>
                                <asp:TextBox runat="server" ID="txtAssistantPhoneNo" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div> 

                        <br />

                        <asp:CheckBox ID="chkSetuju" runat="server" Checked="false" />
                        <label runat="server" id="intro4" class="control-label">Saya telah membaca/dibacakan arahan di atas dan bersetuju dengan syarat-syarat tersebut.</label>

                        <%--<p id="intro4xx" runat="server"></p>--%>

                        <div id="lblalert" class="alert alert-danger" style="display: none" onclick="$(this).hide()"></div>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btnSubmit" runat="server" Text="Setuju >>" CssClass="btn btn-outline btn-primary center-block" />
                            </ContentTemplate>                        
                        </asp:UpdatePanel>
                    </div>
                    
                </div>
            </div>
            <!-- /.col-lg-4 -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.container-fluid -->

    <!-- /#page-wrapper -->
</asp:Content>
