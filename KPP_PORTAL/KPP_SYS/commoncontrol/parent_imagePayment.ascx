<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="parent_imagePayment.ascx.vb" Inherits="KPP_SYS.parent_imagePayment" %>

<style>
    .sc3::-webkit-scrollbar {
        height: 10px;
    }

    .sc3::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc3::-webkit-scrollbar-thumb {
        background-color: #929B9E;
        border-radius: 3px;
    }

    .sc4::-webkit-scrollbar {
        width: 10px;
    }

    .sc4::-webkit-scrollbar-track {
        background-color: transparent;
    }

    .sc4::-webkit-scrollbar-thumb {
        background-color: #929B9E;
    }
</style>

<script type="text/javascript">
    function ShowMessage(message, messagetype) {
        var cssclass;
        switch (messagetype) {
            case 'Success':
                cssclass = 'alert-success'
                break;
            case 'Error':
                cssclass = 'alert-danger'
                break;
            default:
                cssclass = 'alert-info'
        }
        $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; text-align:left -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        setTimeout(function () {
            $("#alert_div").fadeTo(5000, 500).slideUp(500, function () {
                $("#alert_div").remove();
            });
        }, 3000);
    }
</script>

<script type="text/javascript" lang="javascript">
    function CheckAllEmp(Checkbox) {
        var GridVwHeaderChckbox = document.getElementById("<%=datRespondentPayment.ClientID%>");
        for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
            GridVwHeaderChckbox.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
        }
    }
</script>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2; margin-bottom: 3vh" class="w3-card-2 font">
    <%--Breadcrum--%>
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh" class="w3-text-black">
        Menu &nbsp; : &nbsp; Payment &nbsp; / &nbsp; Invoice information 
    </div>
</div>

<div style="background-color: #F2F2F2; border: 5px solid #F2F2F2;" class="w3-card-2">
    <div style="padding-top: 1vh; padding-left: 1.1vw; padding-bottom: 1vh; border-bottom: 3px solid #567572FF;">
        <div class="w3-text-black font" style="text-align: left; padding-left: 1vw; display: inline-block">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Year : </asp:Label>
            <asp:DropDownList ID="ddlyear" runat="server" CssClass=" btn btn-default font" AutoPostBack="true"></asp:DropDownList>
        </div>
    </div>

    <div style="padding-top: 3vh; padding-left: 1vw; padding-bottom: 1vh; white-space: nowrap; height: 70vh" id="InvoiceLock" runat="server" class="sc4">

         <div style="overflow-y: scroll; height: 46vh" class="table-responsive sc4 font">
            <asp:GridView ID="datRespondentPayment" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
                BackColor="#FFFAFA" DataKeyNames="II_ID" BorderStyle="None" GridLines="None" ShowHeaderWhenEmpty="true"
                Width="97%" HeaderStyle-HorizontalAlign="Left">
                <RowStyle HorizontalAlign="Left" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        <ItemStyle VerticalAlign="Middle" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Invoice No" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="II_InvNo" class="id1" runat="server" Text='<%# Eval("II_InvNo") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Year" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="II_Year" class="id1" runat="server" Text='<%# Eval("II_Year") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Paid Times" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="II_PaidSetting" class="id1" runat="server" Text='<%# Eval("II_PaidSetting") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Amount" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="II_FullAmount" class="id1" runat="server" Text='<%# Eval("II_FullAmount") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Outstanding" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="II_Outstanding" class="id1" runat="server" Text='<%# Eval("II_Outstanding") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Published" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="II_Published" class="id1" runat="server" Text='<%# Eval("II_Published") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="Publish_Status" class="id1" runat="server" Text='<%# Eval("Publish_Status") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Status" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="II_Status" class="id1" runat="server" Text='<%# Eval("II_Status") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-Width="8%">
                        <ItemTemplate>
                            <asp:Label ID="Status" class="id1" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="View" ButtonType="image" ShowDeleteButton="true" DeleteImageUrl="~/img/plus image 2.png" ControlStyle-Width="22px" ControlStyle-Height="22px" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label runat="server" Class="id1 w3-text-black"><b> No Invoice Information Are Recorded </b> </asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#3C3232" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
                <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
            </asp:GridView>
        </div>

    </div>
</div>
<br />


