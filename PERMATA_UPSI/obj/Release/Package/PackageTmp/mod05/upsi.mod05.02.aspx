<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod05.02.aspx.vb" Inherits="permata_upsi.upsi_mod5_02" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <script src="../js/mod05.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {

            mod05_init($('#<%=btnNext.ClientID%>'), $('#<%=user_answer.ClientID%>'), $('#<%=lblInstruction.ClientID%>'), $('#<%=hfInstruction1.ClientID%>').val(), $('#<%=hfInstruction2.ClientID%>').val(), 1);
       
        });
    </script>
    <style>        
        img{border-style: solid;border-color: white;border-width: 2px !important;}
    </style>
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="MEMORI FOTOGRAFIK"></asp:Label>
                </h3>
                <p class="lead">
                    <asp:Label ID="lblInstruction" runat="server" Text="Sila tekan butang Mula dibawah."></asp:Label><asp:HiddenField ID="hfInstruction1" runat="server" /><asp:HiddenField ID="hfInstruction2" runat="server" />
                </p>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" OnClientClick="$(specialRow).css('visibility', 'visible');" />
                        <button id="timer_progress" runat="server" onclick="return false;" type="button" class="btn btn-danger pull-right">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <p></p>
        <div class="row">
            <!-- /.col-lg-12 -->
            <div class="col-lg-12">
                <div class="panel panel-primary">

                    <div class="panel-body">

                        <div class="col-sm-12">

                            <div class="row">
                                <div class="col-sm-12">

                                    <div class="row question" style="display:none;">
                                        <div class="col-sm-12">
                                            <asp:Image ID="ImageButton1" ImageUrl="~/images/mod05/mod05.02.01.png" runat="server" CssClass=" center-block"  />
                                        </div>

                                    </div>
                                    <div class="waiting row" style="display:none;"><div class="col-sm-12" style="text-align:center"><img src="/images/spinner.gif" width="50"  /></div></div>
                                    <div class="row answer" style="display:none;">
                                        <div class="col-sm-6">
                                            <asp:Image ID="ImageButton2" ImageUrl="~/images/mod05/mod05.02.01.png" runat="server" CssClass=" pull-right" style="cursor:pointer" data-choose="mod05.02.01" onclick="setImage(this);" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:Image ID="ImageButton3" ImageUrl="~/images/mod05/mod05.02.02.png" runat="server" CssClass=" pull-left"  style="cursor:pointer" data-choose="mod05.02.02" onclick="setImage(this);" />
                                        </div>

                                    </div>


                                </div>
                            </div>
                            <input type="hidden" id="user_answer" runat="server" />
                            <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" style="display:none" />
                        </div>

                    </div>
                    <!-- /panel-body -->

                </div>
            </div>
            <!-- /.col-lg-12 -->
        </div>
        <!-- /.row -->
    </div>
    <!-- /.container-fluid -->

    <!-- /#page-wrapper -->
</asp:Content>

