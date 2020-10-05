<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/saringan/nocheck.Master"
    CodeBehind="ukm2.end.aspx.vb" Inherits="permatapintar.ukm2_end" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
       if(window.history.forward(1) != null)
           window.history.forward(1);
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="tableend" border="0">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 30px;">
                &nbsp;
            </td>
            <td>
                <%--<a href="#">
                    <img src="images/logo.png" title="Araken I-PROFILE" alt="Araken I-PROFILE" width="400"
                        height="35" border="0" class="logo" /></a>--%>
                <h1>Araken I-PROFILE</h1>

            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <img src="images/permatapintar.jpg" alt="permatapintar" />
            </td>
            <td style="vertical-align: top;">
                <h2>
                    <asp:Label ID="ukm2_end_header" runat="server" Text=""></asp:Label></h2>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_instruction" runat="server" Text="Terima kasih kerana telah berjaya menjawab soalan yang telah disediakan."></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_01" runat="server" Text="TEL#"></asp:Label>
                :<asp:Label ID="lblMYKAD" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_02" runat="server" Text="Nama"></asp:Label>
                :<asp:Label ID="lblStudentFullname" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_03" runat="server" Text="Darjah/Tingkatan"></asp:Label>
                :<asp:Label ID="lblStudentForm" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_06" runat="server" Text="Nama Sekolah"></asp:Label>
                :<asp:Label ID="lblSchoolName" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_07" runat="server" Text="Bandar"></asp:Label>
                :<asp:Label ID="lblSchoolCity" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_08" runat="server" Text="Negeri"></asp:Label>
                :<asp:Label ID="lblSchoolState" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblend_09" runat="server" Text="Tarikh dan masa ujian tamat"></asp:Label>
                :<asp:Label ID="lblExamEnd" runat="server" Text="" Font-Bold="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr> 
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Label ID="lblLoadStart" runat="server" Text="0"></asp:Label>

</asp:Content>
