<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="upsi.certificate.aspx.vb" Inherits="permata_upsi.upsi_certificate" Debug="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PCIS</title>
    <style type="text/css">

        body{
            margin:0pt;
        }

        .auto-style4 {
            text-decoration: underline;
            color: #0000FF;
        }
               
        .labelstyle{
            font-family: Arial, Helvetica, sans-serif; 
            font-style: normal;
            font-size:12pt;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center">

            <table style="text-align:center;">
                <tr>
                    <td colspan="3">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/cert1-header.png" Width="600pt" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="border-color: #000000; border-style: solid none none none; border-width: thin; width:600pt">&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>                
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle" style="font-size: 36pt"><strong>Sijil Penyertaan</strong></td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle">Dengan ini disahkan bahawa</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td> 
                </tr>               
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle" style="font-weight: bold" >
                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                    </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle" style="font-weight: bold">
                        <asp:Label ID="lblMyKid" runat="server" Text=""></asp:Label>
                    </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle" style="font-weight: bold">
                        <asp:Label ID="lblTaska" runat="server" Text=""></asp:Label>
                    </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle" style="font-weight: bold">
                        <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                    </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>               
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle">telah menamatkan</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>                    
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td style="font-weight: bold" class="labelstyle">Ujian PERMATA Children Intelligence Scale (PCIS)&nbsp;<asp:Label ID="lblPCISYear" runat="server" Text=""></asp:Label> 
                    </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td style="font-weight:bold" class="labelstyle">Peringkat Kebangsaan</td>
                    <td >&nbsp;</td>
                </tr>               
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle">
                        <asp:Label ID="lblStart" runat="server" Text=""></asp:Label>
                    </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle">
                        <asp:Label ID="lblEnd" runat="server" Text=""></asp:Label>
                    </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle"><strong>Tahniah dan Terima Kasih</strong></td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td style="font-weight:bold;" class="labelstyle">Pusat Penyelidikan Perkembangan Kanak-Kanak Negara </td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td style="font-weight:bold;" class="labelstyle">National Child Development Research Centre (NCDRC)</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle">Universiti Pendidikan Sultan Idris</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle">35900 Tanjung Malim, Perak, Malaysia</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td class="labelstyle">Tel: +6015-48117129 Fax: +6015-4811 7294</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td class="labelstyle" >E-Mail: <span class="auto-style4">ncdrc@upsi.edu.my</span></td>
                    <td >&nbsp;</td>
                </tr>

                <tr>
                    <td >&nbsp;</td>
                    <td >&nbsp;</td>
                </tr>
                <tr>
                    <td >&nbsp;</td>
                    <td style="border-color: #000000; border-style: solid none none none; border-width: thin; font-size: small"><em style="line-height: 40px">(Sijil ini janaan komputer dan tidak perlu tandatangan)</em></td>
                    <td >&nbsp;</td>
                </tr>
            </table>

        </div>

    </form>
</body>
</html>
