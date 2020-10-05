<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master" CodeBehind="ukm1.intro.page02.aspx.vb" Inherits="permatapintar.ukm1_intro_page02" 
    title="" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table width="100%" border="0px" style="background-color: #eceff6;">
        <tr>
            <td class="fbsection_header">
                <asp:Label ID="ukm1_page001_001" runat="server" Text="ARAHAN"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="ukm1_page001_002" runat="server" Text="Pada bahagian ini anda dikehendaki menjawab sebanyak 60 soalan yang terbahagi kepada lima(5) set. Set A hingga E. Pada setiap set terdapat 12 soalan yang perlu dijawab.&lt;br/&gt;&lt;br/&gt;

Soalan pertama dalam setiap set adalah soalan percubaan sebagai panduan untuk menjawab soalan seterusnya.&lt;br/&gt;

Untuk menjawab setiap soalan anda perlu memilih rajah/gambar yang sesuai dengan menekan pilihan tersebut dan soalan seterusnya akan dipaparkan.&lt;br/&gt;&lt;br/&gt;
 
Perhatian: Selepas ini bantuan dari pihak lain tidak dibenarkan.&lt;br/&gt;&lt;br/&gt;
 
Selamat Berjaya." /></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="fbform_sap">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnNext" runat="server" Text="  Seterusnya >>  " CssClass="fbbutton" />
            </td>
        </tr>
    </table>

</asp:Content>
