Imports System.Data.SqlClient
Imports System.Security.Cryptography

Public Class admin_create
    Inherits System.Web.UI.UserControl

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Dim strConn As String = ConfigurationManager.AppSettings("ConnectionString")
    Dim objConn As SqlConnection = New SqlConnection(strConn)
    Dim straction As String = ""
    Dim oDes As New Simple3Des("p@ssw0rd1")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
            End If

        Catch ex As Exception

        End Try

    End Sub


    Protected Sub btnCreate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCreate.Click
        '--insert into UserProfile
        If ValidatePage() = False Then
            Exit Sub
        End If

        ''--UPDATE UKM1 also
        strSQL = "INSERT INTO pcis_admin (Fullname,LoginID,Pwd,UserType,Active) VALUES ('" & oCommon.FixSingleQuotes(txtFullname.Text.ToUpper) & "','" & oCommon.FixSingleQuotes(txtLoginID.Text) & "','" & txtPwd.Text & "','" & oCommon.FixSingleQuotes(txtUserType.Text) & "','" & selIsActive.Value & "')"
        strRet = oCommon.ExecuteSQL(strSQL)
        If strRet = "0" Then
            lblMsg.Text = " Succesfully INSERT userprofile!"
        Else
            lblMsg.Text += " INSERT SchoolProfile fail:" & strRet
        End If

    End Sub

    Private Function ValidatePage() As Boolean
        If txtFullname.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Nama Pelajar"
            txtFullname.Focus()
            Return False
        End If

        If txtLoginID.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Login ID"
            txtLoginID.Focus()
            Return False
        End If

        strSQL = "SELECT * FROM UserProfile WHERE LoginID='" & txtLoginID.Text & "'"
        If oCommon.isExist(strSQL) = True Then
            lblMsg.Text = "Login ID sudah digunakan. Sila gunakan Login ID yang lain."
            Return False
        End If

        If txtPwd.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Password"
            txtPwd.Focus()
            Return False
        End If

        If txtUserType.Text.Length = 0 Then
            lblMsg.Text = "!Sila masukkan Jenis Pengguna"
            txtUserType.Focus()
            Return False
        End If


        Return True
    End Function

    Protected Sub lnkList_Click(sender As Object, e As EventArgs) Handles lnkList.Click
        Response.Redirect("admin.pcisadmin.list.aspx")

    End Sub

End Class

Public NotInheritable Class Simple3Des
    Private TripleDes As New TripleDESCryptoServiceProvider

    Sub New(ByVal key As String)
        ' Initialize the crypto provider.
        TripleDes.Key = TruncateHash(key, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)
    End Sub

    Private Function TruncateHash(
    ByVal key As String,
    ByVal length As Integer) As Byte()

        Dim sha1 As New SHA1CryptoServiceProvider

        ' Hash the key.
        Dim keyBytes() As Byte =
        System.Text.Encoding.Unicode.GetBytes(key)
        Dim hash() As Byte = sha1.ComputeHash(keyBytes)

        ' Truncate or pad the hash.
        ReDim Preserve hash(length - 1)
        Return hash
    End Function

    Public Function EncryptData(ByVal plaintext As String) As String

        ' Convert the plaintext string to a byte array.
        Dim plaintextBytes() As Byte =
            System.Text.Encoding.Unicode.GetBytes(plaintext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream.
        Dim encStream As New CryptoStream(ms,
            TripleDes.CreateEncryptor(),
            System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string.
        Return Convert.ToBase64String(ms.ToArray)
    End Function

    Public Function DecryptData(ByVal encryptedtext As String) As String

        ' Convert the encrypted text string to a byte array.
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream.
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream.
        Dim decStream As New CryptoStream(ms,
        TripleDes.CreateDecryptor(),
        System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plaintext stream to a string.
        Return System.Text.Encoding.Unicode.GetString(ms.ToArray)
    End Function

End Class