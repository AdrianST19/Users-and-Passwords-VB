Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'path este calea unde ruleaza aplicatia
        Dim path As String = My.Application.Info.DirectoryPath

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(IO.Path.Combine(path, "data"))
            Dim infoFisier As System.IO.FileInfo
            infoFisier = My.Computer.FileSystem.GetFileInfo(foundFile)
            Dim lungimeNumeFisier As Integer
            lungimeNumeFisier = infoFisier.Name.Length
            'preferam sa avem in lista stringuri de forma grupa1, grupa2, in loc de grupa1.txt, grupa2.txt
            ComboBox1.Items.Add(infoFisier.Name.Substring(0, lungimeNumeFisier - 4))
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not String.IsNullOrEmpty(Trim(TextBox1.Text)) Then
            If Not String.IsNullOrEmpty(Trim(TextBox2.Text)) Then
                If ComboBox1.SelectedIndex < 0 Then
                    MsgBox("Selectati o grupa de acces")
                Else
                    Dim EsteAutentificat As Boolean

                    'path este calea unde ruleaza aplicatia
                    Dim path As String = My.Application.Info.DirectoryPath
                    'deschidem fisierul cu numarul grupei
                    Dim fisierGrupa As String = IO.Path.Combine(path, "data", ComboBox1.Text + ".txt")

                    Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(fisierGrupa)
                        MyReader.TextFieldType = FileIO.FieldType.Delimited
                        MyReader.SetDelimiters(",")
                        Dim currentRow As String()
                        While Not MyReader.EndOfData
                            Try
                                currentRow = MyReader.ReadFields()
                                If currentRow(0) = TextBox1.Text Then
                                    If currentRow(1) = TextBox2.Text Then
                                        Me.Hide()
                                        If ComboBox1.Text = "admin" Then
                                            Form2.Show()
                                        Else
                                            Form3.Show()
                                        End If
                                        EsteAutentificat = True
                                    End If
                                End If
                            Catch ex As Microsoft.VisualBasic.
                                        FileIO.MalformedLineException
                                MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                            End Try
                        End While
                    End Using

                    If Not EsteAutentificat Then
                        MsgBox("Utilizator sau Parola gresite.")
                    End If
                End If
            Else
                MsgBox("Introduceti o parola")
            End If
        Else
            MsgBox("Introduceti un utilizator")
        End If
    End Sub
End Class
