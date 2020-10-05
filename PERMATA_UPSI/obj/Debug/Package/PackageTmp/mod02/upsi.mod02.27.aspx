<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod02.27.aspx.vb" Inherits="permata_upsi.upsi_mod02_27" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script type="text/javascript">
        $(document).ready(function () {

            var imgStack = [];

            setImage = function (e) {
               
                if (imgStack.indexOf($(e).attr('answer')) < 0){
                    imgStack.push($(e).attr('answer'));
                    
                    $(e).next('span').html(imgStack.length);
                    
                    if(imgStack.length == 4)
                    {
                        $('#<%=user_answer.ClientID%>').val(imgStack.slice(0));
                        $('#<%=btnNext.ClientID%>').click();
                    }

                }
            }

        });
    </script>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="MAKLUMAT"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Tekan pada gambar yang paling awal berlaku hingga yang terakhir."></asp:Label>
                </p>

            </div>
        </div>
        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">

                        <div class="row">
                            <input type="hidden" id="user_answer" runat="server" />
                            <div class="col-md-12">
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-6" style="text-align: center">
                                        <asp:ImageButton ID="ImageButton1" ImageUrl="~/images/mod02/mod02.27.01.gif" runat="server" answer="mod02.27.01" OnClientClick="setImage(this);return false;" /><span class="label label-primary"></span>
                                    </div>
                                    <div class="col-md-6" style="text-align: center">
                                        <asp:ImageButton ID="ImageButton2" ImageUrl="~/images/mod02/mod02.27.02.gif" runat="server" answer="mod02.27.02" OnClientClick="setImage(this);return false;" /><span class="label label-primary"></span>
                                    </div>

                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-6" style="text-align: center">
                                        <asp:ImageButton ID="ImageButton3" ImageUrl="~/images/mod02/mod02.27.03.gif" runat="server" answer="mod02.27.03" OnClientClick="setImage(this);return false;" /><span class="label label-primary"></span>
                                    </div>
                                    <div class="col-md-6" style="text-align: center;" >
                                        <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/mod02/mod02.27.04.gif" runat="server" answer="mod02.27.04" OnClientClick="setImage(this);return false;" /><span class="label label-primary"></span>
                                    </div>

                                </div>
                                <br />
                                <br />
                            </div>
                            
                        </div>

                        <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" style="display:none" />
                    </div>
                    <!-- /panel-body -->

                </div>
            </div>
            <!-- /.col-lg-12 -->

        </div>
        <!-- /.row -->
    </div>
    <!-- /.container-fluid -->

</asp:Content>
