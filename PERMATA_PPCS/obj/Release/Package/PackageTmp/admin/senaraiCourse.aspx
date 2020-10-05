<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="senaraiCourse.aspx.vb"
    Inherits="permatapintar.senaraiCourse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Program Perkhemahan Cuti Sekolah</title>
    <meta content="FREE Marketplace" name="iMarketplace" />
    <meta content="Education, Pelajaran, UKM, Online Test, EQ, IQ, Test," name="Keywords" />
    <meta content="Global" name="Distribution" />
    <meta content="jjamain@yahoo.com" name="Author" />
    <meta content="index,follow" name="Robots" />
    <link href="~/css/portal.default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function clickclear(thisfield, defaulttext) {
            if (thisfield.value == defaulttext) {
                thisfield.value = "";
            }
        }

        function clickrecall(thisfield, defaulttext) {
            if (thisfield.value == "") {
                thisfield.value = defaulttext;
            }
        }

        //slide show
        var slideimages = new Array()
        var slidelinks = new Array()
        function slideshowimages() {
            for (i = 0; i < slideshowimages.arguments.length; i++) {
                slideimages[i] = new Image()
                slideimages[i].src = slideshowimages.arguments[i]
            }
        }

        function slideshowlinks() {
            for (i = 0; i < slideshowlinks.arguments.length; i++)
                slidelinks[i] = slideshowlinks.arguments[i]
        }

        function gotoshow() {

            /*if (!window.winslide||winslide.closed)
            winslide=window.open(slidelinks[whichlink])
            else
            winslide.location=slidelinks[whichlink]
            winslide.focus()
            */

            document.location.href = slidelinks[whichlink];
        }
    </script>

</head>
<body class="fbbody">
    <form id="form1" runat="server">
    <table class="fbform">
        <tr>
            <td>
                Senarai Kursus
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="datRespondent" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                    CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="25" DataKeyNames="courseCode"
                    Width="100%" Style="text-align: left;" HeaderStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="#">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            <ItemStyle VerticalAlign="Middle" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Nama Kursus">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtcourse" runat="server" Text='<%# Bind("courseName") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblcourse" runat="server" Text='<%# Bind("courseName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Kod Kursus">
                            <EditItemTemplate>
                                <asp:TextBox Width="350px" ID="txtcourseCode" runat="server" Text='<%# Bind("courseCode") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblcourseCode" runat="server" Text='<%# Bind("courseCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" CssClass="cssPager" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" VerticalAlign="Middle"
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                Jumlah Rekod:<asp:Label ID="lblTotal" runat="server" Text="" ForeColor="Black"></asp:Label>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
