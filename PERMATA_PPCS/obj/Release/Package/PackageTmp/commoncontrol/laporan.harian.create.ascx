<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="laporan.harian.create.ascx.vb"
    Inherits="permatapintar.laporan_harian_create1" %>
<table class="fbform">
    <tr class="fbform_header">
        <td class="fbsection_sap" colspan="3">
            Tarikh:[<asp:Label ID="lblSelectedDate" runat="server" Text=""></asp:Label>]&nbsp;
            <asp:Label ID="lblMsgTop" runat="server" Text="System message..." ForeColor="Red"></asp:Label>&nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Calendar ID="calToday" runat="server"></asp:Calendar>
        </td>
    </tr>
    <tr class="fbsection_bold">
        <td style="width: 2%;">
            #
        </td>
        <td style="width: 50%;">
            <asp:Label ID="lblSoalan" runat="server" Text="Soalan"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:Label ID="lblJawapan" runat="server" Text="Pilihan Jawapan"></asp:Label>
        </td>
    </tr>
    <tr class="fbsection_bold">
        <td style="width: 2%;">
            &nbsp;
        </td>
        <td style="width: 50%;">
            &nbsp;
        </td>
        <td style="width: 50%;">
            <img src="../img/keamatan01.png" alt="1.TIADA --------------------------------------->10.SENTIASA" />
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo01" runat="server" Text="01."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q1" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q001Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo02" runat="server" Text="02."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ002" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q2" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q002Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo03" runat="server" Text="03."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ003" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q3" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q003Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo04" runat="server" Text="04."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ004" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q4" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q004Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo05" runat="server" Text="05."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ005" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q5" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q005Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo06" runat="server" Text="06."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ006" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q6" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q006Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo07" runat="server" Text="07."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ007" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q7" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q007Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo08" runat="server" Text="08."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ008" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q8" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q008Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo09" runat="server" Text="09."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ009" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q9" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q009Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            &nbsp;
        </td>
        <td colspan='2' class="fbsection_sap">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="fbsection_article">
            <asp:Label ID="lblNo10" runat="server" Text="10."></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:Label ID="lblQ010" runat="server" Text=""></asp:Label>
        </td>
        <td class="fbsection_article">
            <asp:RadioButtonList ID="Q10" runat="server" EnableViewState="False" RepeatDirection="Horizontal"
                RepeatLayout="Flow">
                <asp:ListItem Value="1"></asp:ListItem>
                <asp:ListItem Value="2"></asp:ListItem>
                <asp:ListItem Value="3"></asp:ListItem>
                <asp:ListItem Value="4"></asp:ListItem>
                <asp:ListItem Value="5"></asp:ListItem>
                <asp:ListItem Value="6"></asp:ListItem>
                <asp:ListItem Value="7"></asp:ListItem>
                <asp:ListItem Value="8"></asp:ListItem>
                <asp:ListItem Value="9"></asp:ListItem>
                <asp:ListItem Value="10"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
        <td>
            &nbsp
        </td>
        <td>
            Catatan (500 aksara sahaja):<br />
            <asp:TextBox ID="Q010Remarks" runat="server" TextMode="MultiLine" Rows="3" Width="400px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="fbsection_sap" colspan="3">
            <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:Button ID="btnUpdate" runat="server" Text=" Kemaskini " CssClass="fbbutton" />&nbsp;
        </td>
    </tr>
</table>
