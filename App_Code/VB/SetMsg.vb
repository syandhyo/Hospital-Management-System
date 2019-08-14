Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Xml
Imports System.Diagnostics


Public Class UploadMsg
    Inherits System.Web.UI.Page

    Public Shared _msg As String = String.Empty

    Public Sub New()
    End Sub

    Public Shared ReadOnly Property APPEND_PATH() As String
        Get
            Dim _path As String = System.Configuration.ConfigurationManager.AppSettings("APPEND_PATH").ToString()
            _path = _path.TrimEnd("\", "/") & "\"
            Return _path
        End Get
    End Property

    Public Shared ReadOnly Property REPLACE_PATH() As String
        Get
            Dim _path As String = System.Configuration.ConfigurationManager.AppSettings("REPLACE_PATH").ToString()
            _path = _path.TrimEnd("\", "/") & "\"
            Return _path
        End Get
    End Property

    Public Shared ReadOnly Property SYSTEM_VERSION() As String
        Get
            Return "V2.0.1.3"
        End Get
    End Property

    Public Shared Sub showMsg(ByVal page As Page, ByVal msg As String)
        Dim rNum As New Random
        If page.Master IsNot Nothing Then
            CType(page.Master.FindControl("hdnMsg1"), HtmlControls.HtmlInputHidden).Value = msg
            CType(page.Master.FindControl("hdnMsg2"), HtmlControls.HtmlInputHidden).Value = msg
        Else
            CType(page.FindControl("hdnMsg1"), HtmlControls.HtmlInputHidden).Value = msg
            CType(page.FindControl("hdnMsg2"), HtmlControls.HtmlInputHidden).Value = msg
        End If
        ScriptManager.RegisterStartupScript(page, page.GetType(), rNum.Next().ToString(), "showModalAlert();", True)
    End Sub
End Class



