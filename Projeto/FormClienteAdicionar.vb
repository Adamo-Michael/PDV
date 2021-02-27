Imports System.Data.SqlClient

Public Class FormClienteAdicionar
    Private v As Integer

    Public Sub New(v As Integer)
        InitializeComponent()
        Me.v = v

        If Me.v > 0 Then
            getCliente(v)
        End If
    End Sub

    Private Sub getCliente(id As Integer)
        Try
            Using conn = New SqlConnection(strCon)
                conn.Open()

                Dim sql = $"select * from clientes where id=@id"

                Using cmd = New SqlCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Id", id)

                    Using dr = cmd.ExecuteReader

                        If dr.HasRows Then
                            If dr.Read Then
                                txtNome.Text = dr("Nome")
                                txtEndereco.Text = dr("Endereco")
                            End If
                        End If

                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox($"Não foi possível acessar o registro {vbNewLine} {vbNewLine} {ex.Message}", vbCritical)

        End Try
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        If Validacoes() Then
            SalvarCliente(Me.v)

            Close()
        End If

    End Sub

    Private Function Validacoes() As Boolean
        If txtNome.Text = "" Then
            MsgBox("Necessário informar nome")
            txtNome.Focus()
            Return False

        ElseIf txtEndereco.Text = "" Then
            MsgBox("Necessário informar endereço")
            txtEndereco.Focus()
            Return False
        End If


        Return True
    End Function

    Private Sub SalvarCliente(id As Integer)
        Try
            Using conn = New SqlConnection(strCon)
                conn.Open()

                If id = 0 Then
                    Dim sql = $"insert into clientes (nome, endereco) values (@Nome, @Endereco)"

                    Using cmd = New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Nome", txtNome.Text.Trim)
                        cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text.Trim)
                        cmd.ExecuteNonQuery()

                        MsgBox("Cliente cadastrado com sucesso")
                    End Using
                ElseIf id > 0 Then
                    Dim sql = $"update clientes set nome=@Nome, endereco=@Endereco where id=@id"

                    Using cmd = New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Id", id)
                        cmd.Parameters.AddWithValue("@Nome", txtNome.Text.Trim)
                        cmd.Parameters.AddWithValue("@Endereco", txtEndereco.Text.Trim)
                        cmd.ExecuteNonQuery()

                        MsgBox("Cliente alterado com sucesso")
                    End Using
                End If
            End Using
        Catch ex As Exception
            MsgBox($"Não foi possível conectar {vbNewLine} {vbNewLine} {ex.Message}", vbCritical)

        End Try
    End Sub

    Private Sub FormClienteAdicionar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtNome.Focus()
    End Sub
End Class