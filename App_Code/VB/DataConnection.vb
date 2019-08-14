Imports Microsoft.VisualBasic
Imports System
Imports System.Data.SqlClient
Imports System.Configuration

Public Class DataConnection
    Public Shared Function open_connection(ByVal needToOpen As Boolean) As SqlConnection
        Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("abcd").ToString())
        If needToOpen = True Then
            con.Open()
        End If
        Return con
    End Function
    Public Shared Sub Close_connection(ByVal con As SqlConnection)
        If con IsNot Nothing Then
            If con.State = Data.ConnectionState.Open Then
                con.Close()
            End If
        End If
    End Sub
End Class


