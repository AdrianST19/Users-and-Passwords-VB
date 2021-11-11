Imports System.IO
Public Class Form2
    Private Sub addBtn_Click(sender As Object, e As EventArgs) Handles addBtn.Click
        If Trim(Me.TextBox1.Text) IsNot String.Empty Then
            If Me.ListBox1.Items.Contains(Me.TextBox1.Text) Then
                MsgBox("Username trebuie sa fie unic")
            Else
                If Trim(Me.TextBox2.Text) Is String.Empty Then
                    MsgBox("Trebuie sa introduci o parola")
                Else
                    Me.ListBox1.Items.Add(Me.TextBox1.Text)
                    Me.TextBox1.Clear()
                    Me.ListBox2.Items.Add(Me.TextBox2.Text)
                    Me.TextBox2.Clear()
                End If
            End If
        Else
            MsgBox("Trebuie sa introduci un username")
        End If
    End Sub

    Private Sub delBtn_Click(sender As Object, e As EventArgs) Handles delBtn.Click
        ListBox1.Items.Remove(ListBox1.SelectedItem)
        ListBox2.Items.Remove(ListBox2.SelectedItem)
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If Me.ListBox1.SelectedIndex >= 0 Then
            Me.ListBox2.SelectedIndex = Me.ListBox1.SelectedIndex
            Me.editBtn.Enabled = True
            Me.delBtn.Enabled = True
        Else
            Me.editBtn.Enabled = False
            Me.delBtn.Enabled = False
        End If
    End Sub

    Private Sub editBtn_Click(sender As Object, e As EventArgs) Handles editBtn.Click

        If Me.ListBox1.SelectedIndex < 0 Then
            MsgBox("Nu ati selectat nimic")
        Else
            Dim val = InputBox("Edit this value", "Edit", ListBox1.Items.Item(ListBox1.SelectedIndex))
            If Trim(val) IsNot String.Empty Then
                If Me.ListBox1.Items.Contains(val) Then
                    MsgBox("Username exista deja")
                Else
                    ListBox1.Items.Item(ListBox1.SelectedIndex) = val
                End If
            End If
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        If Trim(Me.TextBox1.Text) IsNot String.Empty And Not Me.ListBox2.Items.Contains(Me.TextBox1.Text) Then
            Me.ListBox2.Items.Add(Me.TextBox1.Text)
            Me.TextBox1.Clear()
        Else
            MsgBox("Elementul exista deja sau nu ati introdus nimic")
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Me.ListBox2.SelectedIndex < 0 Then
            MsgBox("Nu ati selectat nimic")
        Else
            Dim val = InputBox("Edit this value", "Edit", ListBox2.Items.Item(ListBox2.SelectedIndex))
            If Trim(val) IsNot String.Empty Then
                ListBox2.Items.Item(ListBox2.SelectedIndex) = val
            End If
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        If Me.ListBox2.SelectedIndex < 0 Then
            MsgBox("Nu ati selectat nimic")
        Else
            Me.ListBox2.Items.Remove(Me.ListBox2.SelectedItem.ToString)
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If Me.ListBox2.SelectedIndex >= 0 Then
            Me.ListBox1.SelectedIndex = Me.ListBox2.SelectedIndex
            Me.delBtn.Enabled = True
            Me.Button2.Enabled = True
        Else
            Me.delBtn.Enabled = False
            Me.Button2.Enabled = False
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Trim(Me.TextBox5.Text) IsNot String.Empty Then
            For I1 = 0 To Me.ListBox1.Items.Count - 1
                If Me.ListBox1.Items.Item(I1).contains(Me.TextBox5.Text) Then
                    Me.ListBox1.SelectedIndex = I1
                    Me.ListBox2.SelectedIndex = Me.ListBox1.SelectedIndex
                End If
                If Me.ListBox2.Items.Item(I1).contains(Me.TextBox5.Text) Then
                    Me.ListBox2.SelectedIndex = I1
                    Me.ListBox1.SelectedIndex = Me.ListBox2.SelectedIndex
                End If
            Next
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If Trim(ComboBox1.Text) IsNot String.Empty Then
            Dim randuri(ListBox1.Items.Count - 1) As String
            For i = 0 To ListBox1.Items.Count - 1
                randuri(i) = ListBox1.Items(i) + "," + ListBox2.Items(i)
            Next i
            Using sw As New IO.StreamWriter(GlobalVariables.CaleFisierGrupa)
                For Each rand In randuri
                    sw.WriteLine(rand)
                Next
                sw.Close()
            End Using

            Label1.Text = "Ai salvat lista " + ComboBox1.Text
        Else
            MsgBox("Nu ai nicio lista selectata")
        End If
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Form1.Show()
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.incarcaListaFisiere()
        CreateMenuStrip(Me)
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Me.incarcaFisierInListe()
    End Sub

    Private Sub addGr_Click(sender As Object, e As EventArgs) Handles addGr.Click
        'path este calea unde ruleaza aplicatia
        Dim path As String = My.Application.Info.DirectoryPath
        Dim fisierNouGrupa As String = IO.Path.Combine(path, "data", ComboBox1.Text + ".txt")

        If System.IO.File.Exists(fisierNouGrupa) Then
            MsgBox("Lista exista deja.")
        Else

            GlobalVariables.CaleFisierGrupa = fisierNouGrupa

            ' Create or overwrite the file.
            Dim fs As FileStream = File.Create(fisierNouGrupa)
            fs.Close()
            Me.incarcaListaFisiere()
            Me.incarcaFisierInListe()
        End If

    End Sub

    Private Sub incarcaListaFisiere()
        'path este calea unde ruleaza aplicatia
        Dim path As String = My.Application.Info.DirectoryPath

        ComboBox1.Items.Clear()

        For Each foundFile As String In My.Computer.FileSystem.GetFiles(IO.Path.Combine(path, "data"))
            Dim infoFisier As System.IO.FileInfo
            infoFisier = My.Computer.FileSystem.GetFileInfo(foundFile)
            Dim lungimeNumeFisier As Integer
            lungimeNumeFisier = infoFisier.Name.Length
            'preferam sa avem in lista stringuri de forma grupa1, grupa2, in loc de grupa1.txt, grupa2.txt
            ComboBox1.Items.Add(infoFisier.Name.Substring(0, lungimeNumeFisier - 4))
        Next
    End Sub

    Private Sub incarcaFisierInListe()
        'path este calea unde ruleaza aplicatia
        Dim path As String = My.Application.Info.DirectoryPath
        'deschidem fisierul cu numarul grupei
        Dim fisierGrupa As String = IO.Path.Combine(path, "data", ComboBox1.Text + ".txt")
        GlobalVariables.CaleFisierGrupa = fisierGrupa

        Label1.Text = "Prelucrezi lista " + ComboBox1.Text

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()

        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(fisierGrupa)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    ListBox1.Items.Add(currentRow(0))
                    ListBox2.Items.Add(currentRow(1))
                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using
    End Sub

    Private Sub deleteGr_Click(sender As Object, e As EventArgs) Handles deleteGr.Click
        If (Trim(ComboBox1.Text) IsNot String.Empty) Then
            My.Computer.FileSystem.DeleteFile(GlobalVariables.CaleFisierGrupa)
            Label1.Text = "Ai sters lista " + ComboBox1.Text
            Me.incarcaListaFisiere()
            ListBox1.Items.Clear()
            ListBox2.Items.Clear()
            ComboBox1.Text = ""
            Me.incarcaListaFisiere()
            ComboBox1.SelectedIndex = -1
        End If
    End Sub

End Class

Public Class GlobalVariables
    Public Shared CaleFisierGrupa As String
End Class