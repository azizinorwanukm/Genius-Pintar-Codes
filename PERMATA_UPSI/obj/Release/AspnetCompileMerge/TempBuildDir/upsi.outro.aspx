<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/upsi.Master" CodeBehind="upsi.outro.aspx.vb" Inherits="permata_upsi.upsi_outro" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Page Content -->
    
        <div class="container">
            <div class="row">
                <br /><br /><br />
                <!-- /.col-lg-12 -->
                <div class="col-lg-12">
                    <div class="panel panel-info">
                        <div class="panel-heading" id="lblTitle" runat="server">
                           Ujian Tamat
                        </div>
                        <div class="panel-body">

                            <h4 id="outro" runat="server">Terima kasih kerana turut serta dalam ujian ini. Sila tekan butang Cetak untuk mencetak sijil penyertaan.</h4>
                            <asp:HyperLink ID="btnPrint" runat="server" class="btn btn-outline btn-primary" Text="Cetak" NavigateUrl="~/upsi.certificate.aspx" Target="_blank" Visible="true" />

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
