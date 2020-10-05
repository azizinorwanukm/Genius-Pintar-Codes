<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/ukm2.main.Master" CodeBehind="ukm2.08.14.aspx.vb" Inherits="permatapintar.ukm2_08_14" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript">
        function Img_Onclick(strImg) {
            //alert("test");

            //img 1
            if (strImg == "1") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st01.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st01.value = "0"
                    aspnetForm.img01.className = "img08Normal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st01.value = "1"
                    aspnetForm.img01.className = "img08Select";
                }
            }
            //img 2
            if (strImg == "2") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st02.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st02.value = "0"
                    aspnetForm.img02.className = "img08Normal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st02.value = "1"
                    aspnetForm.img02.className = "img08Select";
                }
            }
            //img 3
            if (strImg == "3") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st03.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st03.value = "0"
                    aspnetForm.img03.className = "img08Normal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st03.value = "1"
                    aspnetForm.img03.className = "img08Select";
                }
            }
            //img 4
            if (strImg == "4") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st04.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st04.value = "0"
                    aspnetForm.img04.className = "img08Normal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st04.value = "1"
                    aspnetForm.img04.className = "img08Select";
                }
            }

            //img 5
            if (strImg == "5") {
                if (aspnetForm.ctl00$ContentPlaceHolder1$st05.value == "1") {
                    aspnetForm.ctl00$ContentPlaceHolder1$st05.value = "0"
                    aspnetForm.img05.className = "img08Normal";
                }
                else {
                    aspnetForm.ctl00$ContentPlaceHolder1$st05.value = "1"
                    aspnetForm.img05.className = "img08Select";
                }
            }
        }
    </script>

    <input id="st01" name="st01" type="hidden" value="0" runat="server" />
    <input id="st02" name="st02" type="hidden" value="0" runat="server" />
    <input id="st03" name="st03" type="hidden" value="0" runat="server" />
    <input id="st04" name="st04" type="hidden" value="0" runat="server" />
    <input id="st05" name="st05" type="hidden" value="0" runat="server" />

    <%--<a href="#">
        <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
            height="35" border="0" class="logo" /></a>--%>
    <h1>Araken I-PROFILE</h1>

    <h2>
        <asp:Label ID="ukm2_0800_header" runat="server" Text=""></asp:Label>[14/38]</h2>
    <asp:Label ID="lbl08_instruction" runat="server" Text="Sila <b>klik pada objek atau rajah di bawah yang sepadan untuk ruangan kosong yang bertanda '?'</b>."></asp:Label>
    <table class="mytablemain">
        <tr>
            <td colspan="5">
                <table class="mytableleft">
                    <tr>
                        <td class="mytableleft_td">
                            <img src="images/08.14.04.jpg" alt="answer1" />
                        </td>
                        <td class="mytableleft_td">
                            <img src="images/08.14.01.jpg" alt="answer1" />
                        </td>
                    </tr>
                    <tr>
                        <td class="mytableleft_td">
                            <img src="images/08.14.03.jpg" alt="answer1" />
                        </td>
                        <td class="mytableleft_td">
                            <img src="images/blank_100.gif" alt="answer1" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="ukm2_0800_01" runat="server" Text="Pilihan jawapan"></asp:Label></td>
        </tr>

        <tr>
            <td class="mytablemain_td">
                <img src="images/08.14.01.jpg" alt="answer1" class="img08Normal" id="img01" name="img01"
                    onclick='Javascript:Img_Onclick("1");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/08.14.02.jpg" alt="answer2" class="img08Normal" id="img02" name="img02"
                    onclick='Javascript:Img_Onclick("2");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/08.14.03.jpg" alt="answer3" class="img08Normal" id="img03" name="img03"
                    onclick='Javascript:Img_Onclick("3");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/08.14.04.jpg" alt="answer4" class="img08Normal" id="img04" name="img04"
                    onclick='Javascript:Img_Onclick("4");' />
            </td>
            <td class="mytablemain_td">
                <img src="images/08.14.05.jpg" alt="answer5" class="img08Normal" id="img05" name="img05"
                    onclick='Javascript:Img_Onclick("5");' />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Button ID="btnLangkau" runat="server" Text="Langkau Modul" CssClass="mybutton" /><img src="images/white-space.png" width="400px" alt="" />
                <asp:Button ID="btnNext" runat="server" Text="Seterusnya >>" CssClass="mybutton" />
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
