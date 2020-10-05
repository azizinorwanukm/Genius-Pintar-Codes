<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="config_examStudentQuestionUpload.ascx.vb" Inherits="permatapintar.config_examStudentQuestionUpload" %>
<Table runat="server">
    <tr>
        <td>
            Go To Question
            </td>
        <td>
            <asp:TextBox ID="txtQuestNo" runat="server" Width="100px" MaxLength="5" ></asp:TextBox>
            <asp:Label ID="lblQuestTotal" runat="server"> </asp:Label>
            <asp:Button id="btnGo" runat="server" text="Go" CssClass="fbbutton" Width="90px"/>
        </td>
    </tr>
    <tr>
        <td></td>
    </tr>
    <tr>
        <td>Question <asp:Label ID="lblQuestNo" runat="server"> </asp:Label></td>
    </tr>
    <tr>
        <td>
            Question
        </td>
        <td>
            <asp:TextBox ID="txtQuest" runat="server" Width="100%" TextMode ="MultiLine" ></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            Upload Image
        </td>
        <td>
            <asp:FileUpload ID="imgUpload" Text="Upload" runat="server" CssClass="fbbutton" Width="100%" />
        </td>
    </tr>
    <tr>
        <td>Subject: </td>
        <td>
            <asp:DropDownList ID="ddlSuject" runat="server" Width="100%"></asp:DropDownList>
        </td>

    </tr>
    <tr>
        <td>Difficulty: </td>
        <td>
            <asp:DropDownList ID="ddlDifficulty" runat="server" Width="175px"></asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp
        </td>
    </tr>
    <tr>
        <td>
            Answer
        </td>
        <td>
            <asp:DropDownList ID="ddlAnswer" runat="server" Width="175px"></asp:DropDownList>
        </td>
    </tr>
</Table>
<p>
    <asp:Button id="btn_back" runat="server" text="Back" CssClass="fbbutton" Width="189px"/>
    <asp:Button id="btnUpdate" runat="server" text="Update" CssClass="fbbutton" Width="189px"/>
        </p>
<table border="1">
    <tr>
        <td>
            <asp:Image ID="imgControl" runat="server" />
        </td>
    </tr>
</table>