<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/main.Master" CodeBehind="esurvey.page11.aspx.vb" Inherits="PERMATA_EQTest.esurvey_page11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        <asp:Label ID="lblInstruction" runat="server" Text=""></asp:Label>
    </p>
    <table id="tablelegend">
        <tr>
            <th>1
            </th>
            <th>2
            </th>
            <th>3
            </th>
            <th>4
            </th>
            <th>5
            </th>
        </tr>
        <tr>

            <td>
                <asp:Label ID="lblLevel1" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel2" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel3" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel4" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblLevel5" runat="server" Text=""></asp:Label>
            </td>

        </tr>
    </table>
    &nbsp;
    <asp:Label ID="lblMsgTop" runat="server" Text="" ForeColor="Red"></asp:Label>
    <table id="mycustomtable">
        <tr>
            <th>#
            </th>
            <th>
                <asp:Label ID="lblDomain01" runat="server" Text=""></asp:Label>
            </th>
            <th>
                <asp:Label ID="lblAnswer" runat="server" Text=""></asp:Label>
            </th>
        </tr>
        <tr>
            <td>154
            </td>
            <td>
                <asp:Label ID="lblQ001" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ1" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr class="alt">
            <td>155
            </td>
            <td>
                <asp:Label ID="lblQ002" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ2" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>156
            </td>
            <td>
                <asp:Label ID="lblQ003" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ3" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr class="alt">
            <td>157
            </td>
            <td>
                <asp:Label ID="lblQ004" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ4" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>158
            </td>
            <td>
                <asp:Label ID="lblQ005" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ5" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr class="alt">
            <td>159
            </td>
            <td>
                <asp:Label ID="lblQ006" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ6" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>160
            </td>
            <td>
                <asp:Label ID="lblQ007" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ7" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr class="alt">
            <td>161
            </td>
            <td>
                <asp:Label ID="lblQ008" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQ8" runat="server" Width="350px" MaxLength="250"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>
                <asp:Label ID="lblNext" runat="server" Text=""></asp:Label>
            </td>
            <td>
                <asp:Button ID="btnPrev" runat="server" Text=" << Prev" CssClass="fbbutton" />
                &nbsp;<asp:Button ID="btnNext" runat="server" Text="Next >>" CssClass="fbbutton" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
