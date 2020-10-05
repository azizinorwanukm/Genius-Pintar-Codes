<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="payment_Email.ascx.vb" Inherits="KPP_MS.payment_Email" %>
<%@ Register Assembly="TextboxioControl" Namespace="TextboxioControl" TagPrefix="textboxio" %>

<style>
    .ddl {
        border-radius: 25px;
    }
</style>



<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; text-align: center; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; display: inline-block; width: 100%; border-radius: 25px">Create Email</p>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-top: 5px; margin-bottom: 5px; text-align: left; padding-left: 23px">
        <div class="col-md-10 w3-text-black" style="text-align: left;">
            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Subject : </asp:Label>
            <asp:TextBox CssClass="textbox" ID="txtsubject" Style="width: 80%; border-radius: 25px;" runat="server" Text=""></asp:TextBox>
        </div>
    </div>
    <div class="row" style="background-color: #f2f2f2; display: inline-block; width: 100%; border-radius: 25px; margin-bottom: 10px; text-align: left; padding-left: 23px">
        <div class="col-md-10 w3-text-black" style="text-align: left;">
            <%--            <asp:Label CssClass="Label" runat="server" Style="width: 100%"> Attachment : </asp:Label>--%>
            <asp:FileUpload ID="emailUpload" class="w3-text-black" runat="server" Style="width: 30%"></asp:FileUpload>
        </div>
    </div>
    <div style="padding-left: 23px">
        <textboxio:Textboxio ID="txtDescription" Style="width: 100%; height: 250px; padding-left: 33px;" runat="server"  ScriptSrc="textboxio/textboxio.js"
            Content="<p>Content...</p>" />
    </div>
    <%-- <form style="padding-left: 23px">
        <textarea id="txtDescription" style="width: 100%; height: 250px; padding-left: 33px; padding-right: 23px" runat="server"> Content... </textarea>
    </form>
    <script>textboxio.replace('textarea');</script>--%>



    <p></p>
    <p></p>
</div>
<br />

<div class="gridViewRespond" style="width: 100%; background-color: #f2f2f2; border-radius: 25px; border: 5px solid #8c8c8c;">
    <p style="background-color: #800000; text-align: center; display: inline-block; width: 100%; border-radius: 25px">Student List</p>
    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px">
        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlLevelnaming" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
        <asp:DropDownList ID="ddlClassnaming" runat="server" AutoPostBack="true" CssClass="btn btn-default ddl"></asp:DropDownList>
    </div>
    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 5px">
        <asp:Label CssClass="Label" runat="server"> Search : </asp:Label>
        <asp:TextBox CssClass="textbox" ID="txtstudent" Style="width: 40%; border-radius: 25px;" runat="server" Text="" placeholder="   Search By Name / ID / IC"></asp:TextBox>
        <button id="btnSearch" runat="server" class="btn btn-info" style="background-color: #005580; border-radius: 25px;" title="Search">Search &#160;<i class="fa fa-search w3-large w3-text-white"></i></button>
    </div>
    <br />
    <div style="overflow-y: scroll; overflow-x: hidden; height: 350px" class="table-responsive">
        <asp:GridView ID="datRespondent" runat="server" class="table w3-text-black " AutoGenerateColumns="False"
            BackColor="#d9d9d9" DataKeyNames="std_ID" BorderStyle="None" GridLines="None"
            Width="97%" HeaderStyle-HorizontalAlign="Left">
            <RowStyle HorizontalAlign="Left" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkboxSelectAll" Text="" runat="server" onclick="CheckAllEmp(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="#" ItemStyle-Width="10">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Name" ItemStyle-Width="330">
                    <ItemTemplate>
                        <asp:Label ID="student_Name" class="id1" runat="server" Text='<%# Eval("student_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student ID" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="student_ID" class="id1" runat="server" Text='<%# Eval("student_ID") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student IC" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="student_Mykad" class="id1" runat="server" Text='<%# Eval("student_Mykad") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Student Level" ItemStyle-Width="130">
                    <ItemTemplate>
                        <asp:Label ID="class_Level" class="id1" runat="server" Text='<%# Eval("class_Level") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Class" ItemStyle-Width="80">
                    <ItemTemplate>
                        <asp:Label ID="class_Name" class="id1" runat="server" Text='<%# Eval("class_Name") %>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#800000" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Left" />
            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
        </asp:GridView>
    </div>

    <div class="w3-text-black" style="margin-left: 23px; margin-top: 10px; margin-bottom: 10px">
        <button id="BtnSendEmail" runat="server" class="btn btn-info" style="background-color: #009900; border-radius: 25px;" title="Publish">Send Email &#160; <i class="fa fa-caret-square-o-up w3-large w3-text-white"></i></button>
        <button id="BtnBack" runat="server" class="btn btn-info" style="background-color: #ffdb4d; border-radius: 25px;" title="Print">Back &#160;<i class="fa fa-chevron-circle-left w3-large w3-text-white"></i></button>
    </div>

    <asp:Label ID="emailTest" style="color:black" runat="server"> </asp:Label>
    <asp:HiddenField ID="hiddenAccess" runat="server" />

</div>
