<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.mod06.18.aspx.vb" Inherits="permata_upsi.upsi_mod06_18" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <br />
                <br />
                <h3>
                    <asp:Label ID="lblTitle" runat="server" Text="PERSAMAAN"></asp:Label>
                </h3>

                <p style="margin-bottom: 5px" id="lblInstruction" runat="server" class="lead">Isikan jawapan dalam ruang yang disediakan.</p>

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Timer ID="Timer1" runat="server" Interval="1000" Enabled="false"></asp:Timer>
                        <asp:Button ID="btnStart" runat="server" class=" btn btn-default invisible" Text="Mula" />
                        <button id="timer_progress" runat="server" onclick="return false;" type="button" class="btn btn-danger pull-right invisible">Time Remaining <span id="time_left" runat="server" class="badge "></span></button>
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

                        <br />
                        
                        <div class="col-md-12">

                            <div class="row">
                                <div class="col-md-12">

                                    <div style="margin:0 auto;text-align:center"><asp:Label ID="lblQuestion" runat="server" Text="KUNING dan HIJAU, kedua-duanya adalah "></asp:Label><asp:TextBox ID="txtAnswer" runat="server" Width="250"></asp:TextBox></div>
                                    
                                </div>
                            </div>

                        </div>
                        
                        <br />
                        <br />                        
                        <br />

                        <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="center-block btn btn-outline btn-primary" />

                    </div>
                    <!-- /panel-body -->

                </div>
            </div>
            <!-- /.col-lg-12 -->

        </div>
        <!-- /.row -->
    </div>


</asp:Content>

