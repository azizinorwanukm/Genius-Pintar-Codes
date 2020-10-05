<%@ Page Language="vb" AutoEventWireup="true" CodeBehind="upsi.report01.aspx.vb" Inherits="permata_upsi.upsi_report01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
 "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PCIS</title>

    <!-- The styles -->
    <link href="/css/bootstrap-cerulean.min.css" rel="stylesheet" />
    <link href="/css/charisma-app.css" rel="stylesheet" />
    <%--<link href="/bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />--%>

    <style type="text/css">
        .auto-style1 {
            font-size: small;
        }

        .auto-style2 {
            color: #FFFFFF;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


        <div>
            <table class="nav-justified">
                <tr>
                    <td height="50px" style="background-color: #333333; width: 100%">
                        <strong><span class="auto-style2">&nbsp;&nbsp; PERMATA Children Intelligence Scale</span> </strong>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>

        <div class="box-content">
            <table >
                <thead>
                    <tr>
                        <td height="50px" width="300px">Laporan Keputusan Bedasarkan Module :
             
                        </td>
                        <td width="300px">

                            <asp:DropDownList ID="ddlmodule" runat="server"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlmodule_SelectedIndexChanged" Width="300px">
                                <asp:ListItem Selected="true" Value="" Text="- Please Select Module -" />
                                <asp:ListItem Text="Block Assembly" Value="1" />
                                <asp:ListItem Text="Information" Value="2"  />
                                <asp:ListItem Text="Abstract Reasoning" Value="3" />
                                <asp:ListItem Text="Symbol Search" Value="4" />
                                <asp:ListItem Text="Photographic Memory" Value="5" />                                
                                <asp:ListItem Text="Likeness" Value="6" />
                                <asp:ListItem Text="Picture Concepts" Value="7" />
                                <asp:ListItem Text="Identification" Value="8" />
                                <asp:ListItem Text="Pet Locations" Value="9" />
                                <asp:ListItem Text="Picture Assembly" Value="10" />
                            </asp:DropDownList>

                        </td>
                        <td align="center" width="250px">

                            <asp:Button ID="Button2" runat="server" Text="Refresh" OnClick="Button2_Click" Width="120px" />
                            <asp:Button ID="Button1" runat="server" Text="Reset Answer" OnClick="Button1_Click" Width="120px" />
                        </td>
                    </tr>
                </thead>

            </table>

            <div></div>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button2"
                        EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlModule"
                        EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>

                    <div style="margin-left: 30px; margin-right: 30px; float: initial;">


                        <div class="alert alert-info" style="height: 220px">
                            <table class="table table-bordered table-striped table-condensed table-hover" id="Table2" runat="server" style="width: 850px;" align="center">
                                <thead>
                                    <tr>
                                        <td>
                                            <strong>
                                                <asp:Label ID="Label2" runat="server" Font-Size="Large" Style="font-size: small; text-align: center; text-decoration: underline; color: #000000;" />
                                                <asp:Label ID="lblExam" runat="server" Font-Size="Large" Style="font-size: small; display: none; text-align: center; text-decoration: underline; color: #000000;" />
                                            </strong>
                                        </td>
                                    </tr>
                                </thead>
                            </table>

                            <table class="table table-bordered table-striped table-condensed table-hover" id="Table1" runat="server" style="width: 850px;" align="center">
                                <thead>
                                    <tr>
                                        <td style="width: 120px" class="auto-style1">Name</td>
                                        <td style="width: 5px">:</td>
                                        <td style="width: 200px">
                                            <label id="lblname" runat="server">
                                            </label>
                                        </td>
                                        <td style="width: 30px"><span class="auto-style1">Total Question </span>&nbsp;</td>
                                        <td style="width: 3px">:</td>
                                        <td style="width: 70px">
                                            <label id="lblQuestion" runat="server">
                                            </label>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td style="width: 120px" class="auto-style1">MyKid No.</td>
                                        <td style="width: 5px">:</td>
                                        <td style="width: 200px">
                                            <label id="lblmykid" runat="server">
                                            </label>
                                        </td>
                                        <td style="width: 30px"><span class="auto-style1">Total Answered </span></td>
                                        <td style="width: 3px">:</td>
                                        <td style="width: 70px">
                                            <label id="lblAnswered" runat="server">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px" class="auto-style1">Person Assist</td>
                                        <td style="width: 5px">:</td>
                                        <td style="width: 200px">
                                            <label id="lblassistant" runat="server">
                                            </label>
                                        </td>
                                        <td style="width: 30px"><span class="auto-style1">Total Mark</span></td>
                                        <td style="width: 3px">:</td>
                                        <td style="width: 70px">
                                            <label id="lblmark" runat="server">
                                            </label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 120px" class="auto-style1">Contact No. of Person Assist</td>
                                        <td style="width: 5px">:</td>
                                        <td style="width: 200px">
                                            <label id="lblcontact" runat="server">
                                            </label>
                                        </td>
                                        
                                    </tr>

                                </thead>

                            </table>
                        </div>
                    </div>
                    <div>
                        <table class="table table-bordered table-striped table-condensed table-hover" id="tbllaporan4" runat="server" style="width: 850px;" align="center">
                            <tr>
                                <th style="width: 120px">Question No.</th>
                                <th style="width: 180px">Date And Time</th>
                                <th style="width: 600px">Respondent Answered</th>
                                <th style="width: 60px">Mark</th>
                            </tr>
                        </table>
                    </div>

                </ContentTemplate>

            </asp:UpdatePanel>


        </div>

    </form>
</body>
</html>
