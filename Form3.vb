Imports System.IO
Public Class Form3
    'path este calea unde ruleaza aplicatia
    Dim path As String = My.Application.Info.DirectoryPath
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Adaugam meniul din Module1
        CreateMenuStrip(Me)
        Label1.Hide()
        Label2.Hide()
        Label3.Hide()
        Label4.Hide()

        LoadCat()

        Dim fisierProduse As String = IO.Path.Combine(path, "client", "clienti.csv")
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(fisierProduse)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    ComboBox2.Items.Add(currentRow(2) & " " & currentRow(1))

                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using

    End Sub

    Private Sub LoadCat()

        ComboBox1.Items.Add("All")

        'Adaugam fiecare timp de produs in combobox
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(IO.Path.Combine(path, "produse"))
            Dim infoFisier As System.IO.FileInfo
            infoFisier = My.Computer.FileSystem.GetFileInfo(foundFile)
            Dim lungimeNumeFisier As Integer
            lungimeNumeFisier = infoFisier.Name.Length
            'preferam sa avem in lista stringuri de forma filme, muzica
            Dim numeCategorie As String = infoFisier.Name.Substring(0, lungimeNumeFisier - 4)

            ComboBox1.Items.Add(numeCategorie)
            LoadProdFromCSV(numeCategorie)

        Next

        ComboBox1.SelectedItem = "All"
    End Sub

    Private Sub LoadProdFromCSV(numeFisier As String)
        Dim fisierProduse As String = IO.Path.Combine(path, "produse", numeFisier + ".csv")
        Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(fisierProduse)
            MyReader.TextFieldType = FileIO.FieldType.Delimited
            MyReader.SetDelimiters(",")
            Dim currentRow As String()
            While Not MyReader.EndOfData
                Try
                    currentRow = MyReader.ReadFields()
                    ListaProd.Items.Add(currentRow(0))

                Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ListaProd.Items.Clear()
        Dim categorieSelectata As String = ComboBox1.Text
        If String.Compare(categorieSelectata, "All") = 0 Then
            For Each catItems In ComboBox1.Items
                If String.Compare(catItems, "All") <> 0 Then
                    LoadProdFromCSV(catItems)
                End If
            Next
        Else
            LoadProdFromCSV(categorieSelectata)
        End If

    End Sub

    Private Sub ListaProd_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListaProd.SelectedIndexChanged
        IncarcaDetalii(ListaProd.SelectedItem.ToString)
        If ListaProd.SelectedIndex < 0 Then
            Label1.Hide()
            Label2.Hide()
            Label3.Hide()
            Label4.Hide()
        Else
            Label1.Show()
            Label2.Show()
            Label3.Show()
            Label4.Show()
        End If
    End Sub

    Private Sub IncarcaDetalii(nume As String)
        For Each foundFile As String In My.Computer.FileSystem.GetFiles(IO.Path.Combine(path, "produse"))
            Dim infoFisier As System.IO.FileInfo
            infoFisier = My.Computer.FileSystem.GetFileInfo(foundFile)

            Dim caleFisier As String = IO.Path.Combine(path, "produse", infoFisier.Name)
            Dim lungimeNumeFisier As Integer
            lungimeNumeFisier = infoFisier.Name.Length
            'preferam sa avem in lista stringuri de forma filme, muzica
            Dim numeCategorie As String = infoFisier.Name.Substring(0, lungimeNumeFisier - 4)

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(caleFisier)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        If String.Compare(currentRow(0), nume) = 0 Then
                            Label4.Text = "Categorie: " & numeCategorie
                            Label1.Text = "Nume: " + currentRow(0)
                            Label2.Text = "Gen: " + currentRow(1)
                            Label3.Text = "Anul: " + currentRow(2)
                        End If
                    Catch ex As Microsoft.VisualBasic.
                            FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                    End Try
                End While
            End Using

        Next

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListaProd.SelectedIndex < 0 Then
            MsgBox("Nu este niciun produs selectat")
        Else
            If ListaProd.SelectedIndex < 0 Then
                MsgBox("Nu este niciun client selectat")
            Else
                Dim path As String = My.Application.Info.DirectoryPath
                'deschidem fisierul cu numarul grupei
                Dim fisierActiuni As String = IO.Path.Combine(path, "logs", "inchirieri.csv")

                Dim result As DialogResult = MessageBox.Show("Clientul doreste sa inchirieze?",
                      "Inchiriati",
                      MessageBoxButtons.YesNo)

                If result = DialogResult.Yes Then
                    Using sw As New IO.StreamWriter(fisierActiuni, True)
                        Dim rand As String
                        rand = Me.ListaProd.SelectedItem.ToString & "," & Me.ComboBox2.Text & "," & DateTime.Now.ToString()
                        sw.WriteLine(rand)
                        sw.Close()
                    End Using
                    MsgBox("Salvat")
                    Me.ComboBox2.SelectedIndex = -1
                End If
            End If
        End If
    End Sub
End Class