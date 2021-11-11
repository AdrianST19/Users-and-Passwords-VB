Public Class Form5
    'path este calea unde ruleaza aplicatia
    Dim path As String = My.Application.Info.DirectoryPath
    'deschidem fisierul cu numarul grupei
    Dim fisierLogs As String = IO.Path.Combine(path, "logs", "inchirieri.csv")

    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CreateMenuStrip(Me)

        Dim afisare As String = ""

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(fisierLogs)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    afisare = afisare & currentRow(1) & " a inchiriat " & currentRow(0) &
                        " la data " & currentRow(2) & Environment.NewLine

                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

        TextBox1.Text = afisare
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        Form3.Show()
    End Sub
End Class