<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="logged.ascx.vb" Inherits="UKM3.logged2" %>

<table>
    <tr>
        <td>Nama Calon :</td>
        <td><asp:Label ID="lblUser" runat="server" Text=""></asp:Label></td>
    </tr>
    <tr>
        <td>MYKAD :</td>
        <td><asp:Label ID="lblMykad" runat="server" Text=""></asp:Label></td>
    </tr>
</table>
<br />
<h3>Guideline Ujian UKM3</h3><br />

Sebelum login: <br />
1. Ujian UKM3 mengandungi 80 soalan yang perlu dijawab dalam masa 2 JAM.<br />
2. Pelajar perlu masuk ke page UKM3 ukm3.permatapintar.edu.my/login.aspx<br />
3. Masukkan nombor mykad/mykid pelajar dan kata laluan<br />
4. Pelajar perlu tekan butang "Mula Ujian"<br />
5. Page soalan akan terpapar<br />
6. Pelajar boleh mula menjawab dalam masa 2 JAM<br /><br />


Panduan menjawab (selepas login): <br />
1. Pelajar perlu memilih jawapan A/B/C/D dengan menekan butang radio yang disediakan<br />
2. Jawapan yang dipilih akan dipaparkan dalam mesej di bawah pilihan jawapan<br />
3. Butang CONFIRM akan bertukar kepada Hijau<br />
3. Klik butang CONFIRM tersebut untuk terus ke soalan seterusnya.<br />
4. Pelajar tidak dibenarkan untuk undur semula ke soalan sebelumnya.<br /><br />

Page akan LOGOUT serta merta selepas pelajar menjawab soalan terakhir.<br /><br />
<div class="container">
    <asp:Button ID="btnStartExam" runat="server" Text="Mula Ujian" CssClass="logbtn" />
</div>

<style>
.container {
    padding: 16px;
    text-align: left;
    margin: 0 auto;
    width: 100%;
}

.logbtn {
    background-color: #4CAF50;
    color: white;
    padding: 14px 20px;
    margin: 8px 0;
    border: none;
    cursor: pointer;
    width: 500px;
}
</style>