Imports System.Web
Imports System.Web.SessionState
Imports System.Data.OleDb

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Private Shared intTotalNumberOfUsers As Integer = 0
    Private Shared intCurrentNumberOfUsers As Integer = 0

    Dim oCommon As New Commonfunction
    Dim strSQL As String = ""
    Dim strRet As String = ""

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Get the Total Number of Users from the database.
            strSQL = "SELECT NumberOfHits FROM master_counter WHERE CounterType = 'UserCounter'"
            intTotalNumberOfUsers = oCommon.getFieldValueInt(strSQL)

        Catch ex As Exception
            Response.Write("Err:" & ex.Message)
        End Try

    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        Try
            ' Increase the two counters.
            intTotalNumberOfUsers += 1
            intCurrentNumberOfUsers += 1

            strSQL = "UPDATE master_counter SET NumberOfHits = " & intTotalNumberOfUsers & " WHERE CounterType = 'UserCounter'"
            strRet = oCommon.ExecuteSQL(strSQL)

        Catch ex As Exception
            Response.Write("Err:" & ex.Message)
        End Try

    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        ' Note: The Session_End event is raised only when the sessionstate 
        ' mode is set to InProc in the Web.config file. 
        ' If session mode is set to StateServer or SQLServer, 
        ' the event is not raised.
        intCurrentNumberOfUsers -= 1

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

    Public Shared ReadOnly Property TotalNumberOfUsers() As Integer
        Get
            Return intTotalNumberOfUsers
        End Get
    End Property

    Public Shared ReadOnly Property CurrentNumberOfUsers() As Integer
        Get
            Return intCurrentNumberOfUsers
        End Get
    End Property

End Class