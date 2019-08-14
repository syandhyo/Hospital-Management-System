Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Web.UI.WebControls
Imports System.Web.UI
Imports System.Data
Imports System

Imports System.IO
Imports System.Diagnostics




Public Class DataMathods
    'Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Public com As SqlCommand
    Dim da As SqlDataAdapter
    Dim dt As DataTable = Nothing
    Dim ds As DataSet = Nothing
    Public _page As Page
    Public _iD As Integer
    Public _objOut As Object
    Public _RESULT As Integer = 0

    Public Function SelectData(ByVal sqlString As String) As DataTable
        Try
            With con
                con = DataConnection.open_connection(False)
                da = New SqlDataAdapter(sqlString, con)
                dt = New DataTable
                da.Fill(dt)
            End With
        Catch ex As Exception
            dt = Nothing
            UploadMsg.showMsg(_page, ex.Message)
        Finally
            If con.State = ConnectionState.Open Then
                DataConnection.Close_connection(con)
            End If
        End Try
        Return dt
    End Function

    Public Function Get_DataSet(ByVal sqlString As String) As DataSet
        Try
            With con
                con = DataConnection.open_connection(False)
                da = New SqlDataAdapter(sqlString, con)
                ds = New DataSet
                da.Fill(ds)
            End With
        Catch ex As Exception
            ds = Nothing
            UploadMsg.showMsg(_page, ex.Message)
            Throw ex
        Finally
            If con.State = ConnectionState.Open Then
                DataConnection.Close_connection(con)
            End If
        End Try
        Return ds
    End Function

    Public Function Get_DataSet(ByVal sqlString As String, ByVal needTranforNextCall As Boolean, ByVal isSP As Boolean, Optional ByVal objParms() As SqlParameter = Nothing) As DataSet
        Dim trn As SqlTransaction = Nothing
        If com IsNot Nothing Then
            If com.Transaction IsNot Nothing Then
                trn = com.Transaction
            End If
        End If
        Try
            With con
                If con IsNot Nothing Then
                    If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                        con = DataConnection.open_connection(True)
                    End If
                Else
                    con = DataConnection.open_connection(True)
                End If
                If trn Is Nothing Then
                    trn = con.BeginTransaction(IsolationLevel.ReadCommitted)
                End If

                If com Is Nothing Then
                    If isSP = True Then
                        com = New SqlCommand(sqlString, con, trn)
                        com.CommandType = CommandType.StoredProcedure
                        If objParms Is Nothing Then
                        Else
                            com.Parameters.AddRange(objParms)
                        End If
                    Else
                        com = New SqlCommand(sqlString, con, trn)
                        com.CommandType = CommandType.Text
                    End If
                    Else
                        com.Parameters.Clear()
                        If isSP = True Then
                            com.CommandText = sqlString
                            com.CommandType = CommandType.StoredProcedure
                            com.Connection = con
                            com.Transaction = trn
                            If objParms Is Nothing Then
                            Else
                                com.Parameters.AddRange(objParms)
                            End If


                        Else
                            com.CommandText = sqlString
                            com.CommandType = CommandType.Text
                            com.Connection = con
                            com.Transaction = trn
                        End If
                    End If

                da = New SqlDataAdapter(com)
                ds = New DataSet
                da.Fill(ds)
            End With
        Catch ex As Exception
            ds = Nothing
            Throw ex
        Finally
            If needTranforNextCall = True Then
            Else
                trn.Commit()
                trn = Nothing
                If con.State = ConnectionState.Open Then
                    DataConnection.Close_connection(con)
                End If
            End If
        End Try
        Return ds
    End Function

    Public Sub ExecuteProceedure(ByVal ProceedureName As String, ByVal successStr As String, ByVal outPutParam As String, ByVal outPutParamDbType As SqlDbType, ByVal objParms() As SqlParameter)
        _objOut = Nothing
        Try
            With con
                con = DataConnection.open_connection(True)
                com = New SqlCommand(ProceedureName, con)
                com.CommandType = CommandType.StoredProcedure
                com.Parameters.Clear()
                com.Parameters.AddRange(objParms)
                If outPutParam <> "" Then
                    com.Parameters.Add(outPutParam, outPutParamDbType)
                    com.Parameters(outPutParam).Direction = ParameterDirection.Output
                    If outPutParamDbType = SqlDbType.VarChar Then
                        com.Parameters(outPutParam).Size = 2000
                    End If
                    _RESULT = com.ExecuteNonQuery()
                    _objOut = com.Parameters(outPutParam).Value
                Else
                    _RESULT = com.ExecuteNonQuery()
                End If
                If successStr <> "" Then
                    Throw New Exception(successStr)
                End If
            End With
        Catch ex As Exception
            If ex.Message <> successStr Then
                ErrorLog.Write(ex)
                Throw ex
            Else
                UploadMsg.showMsg(_page, successStr)
            End If
        Finally
            If con.State = ConnectionState.Open Then
                DataConnection.Close_connection(con)
            End If
        End Try
    End Sub

    Public Sub ExecuteProceedure(ByVal ProceedureName As String, ByVal successStr As String, ByVal outPutParam As String, ByVal outPutParamDbType As SqlDbType, ByVal objParms() As SqlParameter, ByVal needTranforNextCall As Boolean)
        _objOut = Nothing
        Dim trn As SqlTransaction = Nothing
        If com IsNot Nothing Then
            If com.Transaction IsNot Nothing Then
                trn = com.Transaction
            End If
        End If

        Try
            With con
                If con IsNot Nothing Then
                    If con.State = ConnectionState.Closed Or con.State = ConnectionState.Broken Then
                        con = DataConnection.open_connection(True)
                    End If
                Else
                    con = DataConnection.open_connection(True)
                End If
                If trn Is Nothing Then
                    trn = con.BeginTransaction(IsolationLevel.ReadCommitted)
                End If
                If com Is Nothing Then
                    com = New SqlCommand(ProceedureName, con, trn)
                    com.CommandType = CommandType.StoredProcedure
                Else
                    com.CommandText = ProceedureName
                    com.Parameters.Clear()
                End If

                com.Parameters.AddRange(objParms)

                If outPutParam <> "" Then
                    com.Parameters.Add(outPutParam, outPutParamDbType)
                    com.Parameters(outPutParam).Direction = ParameterDirection.Output
                    If outPutParamDbType = SqlDbType.VarChar Then
                        com.Parameters(outPutParam).Size = 2000
                    End If
                    _RESULT = com.ExecuteNonQuery()
                    _objOut = com.Parameters(outPutParam).Value
                Else
                    _RESULT = com.ExecuteNonQuery()
                End If
                If successStr <> "" Then
                    Throw New Exception(successStr)
                End If
            End With
        Catch ex As Exception
            If ex.Message <> successStr Then
                ErrorLog.Write(ex)
                Throw ex
            Else
                UploadMsg.showMsg(_page, successStr)
            End If
        Finally
            If needTranforNextCall = True Then
            Else
                trn.Commit()
                trn = Nothing
                If con.State = ConnectionState.Open Then
                    DataConnection.Close_connection(con)
                End If
            End If
        End Try
    End Sub

    Public Sub commitOrRollbackTran(ByVal action As String)
        If com IsNot Nothing Then
            If com.Transaction IsNot Nothing Then
                If action = "commit" Then
                    com.Transaction.Commit()
                Else
                    com.Transaction.Rollback()
                End If
            End If
        End If
    End Sub

    Public Sub cleanAll()
        If com IsNot Nothing Then
            com.Dispose()
            com = Nothing
        End If
        If con IsNot Nothing Then
            If con.State = ConnectionState.Open Then
                DataConnection.Close_connection(con)
            End If
            con.Dispose()
            con = Nothing
        End If
    End Sub

  
    Public Sub Show_Msg(ByVal msg As String)
        Dim Messege As String = String.Empty
        For i As Integer = 1 To msg.Length
            Dim s As String = Mid(msg, i, 1)
            If s <> "'" Then
                Messege += s
            Else
                Messege += "''"
            End If
        Next

    End Sub

    Function Set_strFormat(ByVal str As String) As String
        Dim Fstr As String = String.Empty
        For i As Integer = 1 To str.Length
            Dim s0 As String = Mid(str, i, 1)
            Dim s As String = Mid(str, i, 1)
            If s <> "'" Then
                If i > 1 Then
                    If s = " " And Mid(str, i - 1, 1) = " " Then
                        Fstr += ""
                    Else
                        Fstr += s
                    End If
                Else
                    Fstr += s
                End If
            Else
                Fstr += "''"
            End If
        Next
        Return Fstr
    End Function

    

    Public Function createParams(ByVal paramName As String, ByVal paramDbType As SqlDbType, ByVal paramSize As Integer, ByVal paramValue As Object) As SqlParameter
        Dim tempParam As SqlParameter
        If paramSize > 0 Then
            tempParam = New SqlParameter(paramName, paramDbType, paramSize)
            tempParam.Value = paramValue
        Else
            tempParam = New SqlParameter(paramName, paramDbType)
            tempParam.Value = paramValue
        End If
        Return tempParam
    End Function

    Private Function GetDBType(ByVal theType As System.Type) As SqlDbType
        Dim p1 As SqlClient.SqlParameter
        Dim tc As System.ComponentModel.TypeConverter
        p1 = New SqlClient.SqlParameter()
        tc = System.ComponentModel.TypeDescriptor.GetConverter(p1.DbType)
        If tc.CanConvertFrom(theType) Then
            p1.DbType = tc.ConvertFrom(theType.Name)
        Else
            'Try brute force
            Try
                p1.DbType = tc.ConvertFrom(theType.Name)
            Catch ex As Exception
                'Do Nothing
            End Try
        End If
        Return p1.SqlDbType
    End Function

End Class