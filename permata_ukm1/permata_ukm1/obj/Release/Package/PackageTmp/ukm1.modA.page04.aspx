<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/ukm1.main.Master"
    CodeBehind="ukm1.modA.page04.aspx.vb" Inherits="permatapintar.ukm1_modA_page04"
    Title="" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="fbbluebox" width="100%" border="0px">
        <tr>
            <td>
                <asp:Label ID="lblQuestion" runat="server" Text="" CssClass="fbinstruction"></asp:Label>
                <asp:Label ID="lblQuestionNo" runat="server" Text="" CssClass="fbinstruction"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblModuleA" runat="server" Text="PILIHAN GAMBAR" CssClass="fbinstruction"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top;">
                <asp:Image ID="Image0" runat="server" AlternateText="images" BorderStyle="Solid"
                    BorderWidth="0px" BorderColor="Black" />
            </td>
            <td style="vertical-align: top;">
                <table class="fbgreybox" width="100%" border="0px">
                    <tr>
                        <td>
                            A
                        </td>
                        <td>
                            B
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a id="A1" href="ukm1.modA.page05.aspx?ans=1&lang=<%=Request.QueryString("lang") %>&studentid=<%=Request.QueryString("studentid") %>">
                                <asp:Image ID="Image1" runat="server" AlternateText="images" BorderStyle="Solid"
                                    BorderWidth="0px" BorderColor="Black" /></a>
                        </td>
                        <td>
                            <a id="A2" href="ukm1.modA.page05.aspx?ans=2&lang=<%=Request.QueryString("lang") %>&studentid=<%=Request.QueryString("studentid") %>">
                                <asp:Image ID="Image2" runat="server" AlternateText="images" BorderStyle="Solid"
                                    BorderWidth="0px" BorderColor="Black" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            C
                        </td>
                        <td>
                            D
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a id="A3" href="ukm1.modA.page05.aspx?ans=3&lang=<%=Request.QueryString("lang") %>&studentid=<%=Request.QueryString("studentid") %>">
                                <asp:Image ID="Image3" runat="server" AlternateText="images" BorderStyle="Solid"
                                    BorderWidth="0px" BorderColor="Black" /></a>
                        </td>
                        <td>
                            <a id="A4" href="ukm1.modA.page05.aspx?ans=4&lang=<%=Request.QueryString("lang") %>&studentid=<%=Request.QueryString("studentid") %>">
                                <asp:Image ID="Image4" runat="server" AlternateText="images" BorderStyle="Solid"
                                    BorderWidth="0px" BorderColor="Black" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            E
                        </td>
                        <td>
                            F
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a id="A5" href="ukm1.modA.page05.aspx?ans=5&lang=<%=Request.QueryString("lang") %>&studentid=<%=Request.QueryString("studentid") %>">
                                <asp:Image ID="Image5" runat="server" AlternateText="images" BorderStyle="Solid"
                                    BorderWidth="0px" BorderColor="Black" /></a>
                        </td>
                        <td>
                            <a id="A6" href="ukm1.modA.page05.aspx?ans=6&lang=<%=Request.QueryString("lang") %>&studentid=<%=Request.QueryString("studentid") %>">
                                <asp:Image ID="Image6" runat="server" AlternateText="images" BorderStyle="Solid"
                                    BorderWidth="0px" BorderColor="Black" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblInstructionModuleA" runat="server" Text="Sila klik gambar yang sesuai untuk melengkapkan gambar disebelah kiri."
                                CssClass="fbinstruction"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMsg" runat="server" Text="" CssClass="labelMsg"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
