<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.04.13.aspx.vb" Inherits="permatapintar.ukm2_04_13" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        function Img_Onclick(strImg) {
            //img 1
            if (strImg == "1") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st01.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st01.value = "0"
                    aspnetForm.img01.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st01.value = "1"
                    aspnetForm.img01.className = "imgSelect";
                }
            }
            //img 2
            if (strImg == "2") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st02.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st02.value = "0"
                    aspnetForm.img02.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st02.value = "1"
                    aspnetForm.img02.className = "imgSelect";
                }
            }
            //img 3
            if (strImg == "3") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st03.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st03.value = "0"
                    aspnetForm.img03.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st03.value = "1"
                    aspnetForm.img03.className = "imgSelect";
                }
            }
            //img 4
            if (strImg == "4") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st04.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st04.value = "0"
                    aspnetForm.img04.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st04.value = "1"
                    aspnetForm.img04.className = "imgSelect";
                }
            }
            //img 5
            if (strImg == "5") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st05.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st05.value = "0"
                    aspnetForm.img05.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st05.value = "1"
                    aspnetForm.img05.className = "imgSelect";
                }
            }
            //img 6
            if (strImg == "6") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st06.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st06.value = "0"
                    aspnetForm.img06.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st06.value = "1"
                    aspnetForm.img06.className = "imgSelect";
                }
            }
            //img 7
            if (strImg == "7") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st07.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st07.value = "0"
                    aspnetForm.img07.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st07.value = "1"
                    aspnetForm.img07.className = "imgSelect";
                }
            }
            //img 8
            if (strImg == "8") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st08.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st08.value = "0"
                    aspnetForm.img08.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st08.value = "1"
                    aspnetForm.img08.className = "imgSelect";
                }
            }
            //img 9
            if (strImg == "9") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st09.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st09.value = "0"
                    aspnetForm.img09.className = "imgNormal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st09.value = "1"
                    aspnetForm.img09.className = "imgSelect";
                }
            }

        }
    </script>


    <input id="st01" name="st01" type="hidden" value="0" runat="server" />
    <input id="st02" name="st02" type="hidden" value="0" runat="server" />
    <input id="st03" name="st03" type="hidden" value="0" runat="server" />
    <input id="st04" name="st04" type="hidden" value="0" runat="server" />
    <input id="st05" name="st05" type="hidden" value="0" runat="server" />
    <input id="st06" name="st06" type="hidden" value="0" runat="server" />
    <input id="st07" name="st07" type="hidden" value="0" runat="server" />
    <input id="st08" name="st08" type="hidden" value="0" runat="server" />
    <input id="st09" name="st09" type="hidden" value="0" runat="server" />
    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_0400_header" runat="server" Text=""></asp:Label>
        [13/28]</h2>
    <asp:Label ID="lbl04_instruction" runat="server" Text="Lihat gambar-gambar berikut. <b>Sila klik atau pilih satu gambar dari setiap baris</b> yang mewakili kategori yang sama."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td class="mytablemain_td">
                <img src="images/04.13.01.jpg" alt="topi" class="imgNormal" id="img01" name="img01" onclick='Javascript:Img_Onclick("1");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/04.13.02.jpg" alt="gunting" class="imgNormal" id="img02" name="img02" onclick='Javascript:Img_Onclick("2");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/04.13.03.jpg" alt="kereta" class="imgNormal" id="img03" name="img03" onclick='Javascript:Img_Onclick("3");' />
            </td>
        </tr>
        <tr>
            <td class="mytablemain_td">
                <img src="images/04.13.04.jpg" alt="pintu" class="imgNormal" id="img04" name="img04" onclick='Javascript:Img_Onclick("4");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/04.13.05.jpg" alt="payung" class="imgNormal" id="img05" name="img05" onclick='Javascript:Img_Onclick("5");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/04.13.06.jpg" alt="kaktus" class="imgNormal" id="img06" name="img06" onclick='Javascript:Img_Onclick("6");' />
            </td>
        </tr>
        <tr>
            <td class="mytablemain_td">
                <img src="images/04.13.07.jpg" alt="beg" class="imgNormal" id="img07" name="img07" onclick='Javascript:Img_Onclick("7");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/04.13.08.jpg" alt="peti besi" class="imgNormal" id="img08" name="img08" onclick='Javascript:Img_Onclick("8");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/04.13.09.jpg" alt="bunga" class="imgNormal" id="img09" name="img09" onclick='Javascript:Img_Onclick("9");' />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" /><img src="images/white-space.png" width="400px" alt="" />
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
            </td>
        </tr>

        <tr>
            <td colspan="3">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
