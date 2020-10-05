<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/pelajar.Master" CodeBehind="pelajar_login_berjaya.aspx.vb" Inherits="WebApplication1.pelajar_login_berjaya" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">  
    <img class="permata" src="img/ukm_permatapintar.jpg" ><br /><br />

    <div>
        <img class="mySlides" src="img/GIBS.jpg">
        <img class="mySlides" src="img/libaran.jpg">
        <img class="mySlides" src="img/YayasanSarawak.jpg">
    </div>

    <!-- Slide Show-->
    <script>
        var myIndex = 0;
        carousel();

        function carousel() {
            var i;
            var x = document.getElementsByClassName("mySlides");
            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }
            myIndex++;
            if (myIndex > x.length) { myIndex = 1 }
            x[myIndex - 1].style.display = "block";
            setTimeout(carousel, 5000); // Change image every 2 seconds
        }
    </script>
</asp:Content>
