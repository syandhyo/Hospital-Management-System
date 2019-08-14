Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Xml


Public Class ErrorLog
    Inherits System.Web.UI.Page

    Dim _path As String
    Dim _slNo As String
    Dim _msgTrace As String
    Dim Doc As XmlDocument
    Dim newAtt As XmlAttribute
    Dim err As XmlNode

    Public Property slNo() As String
        Get
            Return _slNo
        End Get
        Set(ByVal value As String)
            _slNo = value
        End Set
    End Property

    Public Property msgTrace() As String
        Get
            Return _msgTrace
        End Get
        Set(ByVal value As String)
            _msgTrace = value
        End Set
    End Property

    Public Sub New()
        _path = Context.Request.PhysicalApplicationPath() & "Error\ErrText.txt"
        If IO.File.Exists(_path) = False Then
            My.Computer.FileSystem.WriteAllText(_path, "", False)
            Doc = New XmlDocument()
            Dim dec As XmlDeclaration = Doc.CreateXmlDeclaration("1.0", _
                                     Nothing, Nothing)
            Doc.AppendChild(dec)

            Dim DocRoot As XmlElement = Doc.CreateElement("Errors")
            Doc.AppendChild(DocRoot)

            err = Doc.CreateElement("Error")
            newAtt = Doc.CreateAttribute("slNo")
            newAtt.Value = "0"
            err.Attributes.Append(newAtt)

            newAtt = Doc.CreateAttribute("msgTrace")
            newAtt.Value = "Empty"
            err.Attributes.Append(newAtt)

            DocRoot.AppendChild(err)

            Doc.Save(_path)
        End If
    End Sub

    Public Sub New(ByVal errNo As Integer)
        _path = Context.Request.PhysicalApplicationPath() & "Error\ErrText.txt"
        If IO.File.Exists(_path) = True Then
            Doc = New XmlDocument()
            Doc.LoadXml(File.ReadAllText(_path))
            err = Doc.ChildNodes(1).ChildNodes(errNo)
            msgTrace = err.Attributes(1).Value
        End If
    End Sub

    Shared Sub Write(ByVal ex As Exception)
        Try
            Dim er As New ErrorLog()
            er.Doc = New XmlDocument()
            Dim line = (Environment.NewLine + Environment.NewLine)
            Dim sw As StreamWriter = File.AppendText(er._path)
            sw.WriteLine(("-----------Exception Details on " + (" " _
                            + (DateTime.Now.ToString + "-----------------"))))
            sw.WriteLine("-------------------------------------------------------------------------------------")
            'sw.WriteLine(line)
            sw.WriteLine("Source : " & ex.Source)
            sw.WriteLine(ex.Message.ToString())
            sw.WriteLine("--------------------------------*End*------------------------------------------")
            sw.WriteLine(line)
            sw.Flush()
            sw.Close()

        Catch e As Exception
            Dim s As String = e.ToString()
        End Try
    End Sub

End Class


