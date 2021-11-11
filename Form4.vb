Imports System.IO

Public Class Form4

    Dim path As String = My.Application.Info.DirectoryPath
    'deschidem fisierul cu numarul grupei
    Dim fisierClienti As String = IO.Path.Combine(path, "client", "clienti.csv")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Trim(Me.TextBox1.Text) Is String.Empty Then
            MsgBox("Introduceti numele")
        Else
            If Trim(Me.TextBox2.Text) Is String.Empty Then
                MsgBox("Introduceti prenumele")
            Else
                If Trim(Me.TextBox3.Text) Is String.Empty Then
                    MsgBox("Introduceti adresa")
                Else
                    If Trim(Me.TextBox3.Text) Is String.Empty Then
                        MsgBox("Introduceti telefonul")
                    Else
                        Using sw As New IO.StreamWriter(fisierClienti, True)
                            Dim rand As String
                            rand = Me.TextBox5.Text & "," & Me.TextBox1.Text & "," &
                                Me.TextBox2.Text & "," & Me.TextBox3.Text & "," & Me.TextBox4.Text
                            sw.WriteLine(rand)
                            sw.Close()

                            Dim result As DialogResult = MessageBox.Show("Salvati clientul?",
                              "Salvati",
                              MessageBoxButtons.YesNo)

                            If result = DialogResult.Yes Then
                                Me.Hide()
                                Form3.Show()
                            End If
                        End Using
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Form3.Show()
    End Sub

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rand As String = File.ReadLines(fisierClienti).Last()
        Dim randArr() As String = rand.Split(",")
        TextBox5.Text = randArr(0) + 1
        TextBox5.Enabled = False
    End Sub
End Class