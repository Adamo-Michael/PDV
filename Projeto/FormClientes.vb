Imports System.Data.SqlClient

Public Class FormClientes

    Private Sub FormClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CarregaRegistros()
    End Sub

    Private Sub CarregaRegistros()
        Try
            statusBar.Text = "Conectando, aguarde..."

            Using conn = New SqlConnection(strCon)
                conn.Open()

                Dim str = "select * from clientes"
                Using da = New SqlDataAdapter(str, conn)
                    Using dt = New DataTable()
                        da.Fill(dt)
                        dgvClientes.DataSource = dt
                    End Using
                End Using
                End Using
        Catch ex As Exception
            MsgBox($"Não foi possível conectar {vbNewLine} {vbNewLine} {ex.Message}", vbCritical)
        Finally
            statusBar.Text = ""
            Refresh()
        End Try
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Try
            statusBar.Text = "Conectando, aguarde..."

            Using conn = New SqlConnection(strCon)
                conn.Open()

                Dim str = $"select * from clientes where 0=0"

                If txtBuscaId.Text <> "" Then
                    str += $" and id={txtBuscaId.Text}"
                End If

                If txtBuscaNome.Text <> "" Then
                    str += $" and nome like '%{txtBuscaNome.Text}%'"
                End If

                Using da = New SqlDataAdapter(str, conn)
                    Using dt = New DataTable()
                        da.Fill(dt)
                        dgvClientes.DataSource = dt
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MsgBox($"Não foi possível conectar {vbNewLine} {vbNewLine} {ex.Message}", vbCritical)
        Finally
            statusBar.Text = ""
            Refresh()
        End Try
    End Sub

    Private Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click

        Dim id = dgvClientes.Rows(dgvClientes.CurrentCell.RowIndex).Cells(0).Value

        Dim frm As New FormClienteAdicionar(id)
        frm.ShowDialog()

        CarregaRegistros()
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click

        If MsgBox("Tem certeza que deseja excluir? ", vbYesNo + vbQuestion, "Aviso") = vbYes Then
            Dim id = dgvClientes.Rows(dgvClientes.CurrentCell.RowIndex).Cells(0).Value

            Try
                Using conn = New SqlConnection(strCon)
                    conn.Open()

                    Dim sql = $"delete from clientes where id=@id"

                    Using cmd = New SqlCommand(sql, conn)
                        cmd.Parameters.AddWithValue("@Id", id)
                        cmd.ExecuteNonQuery()

                        MsgBox("Cliente excluído com sucesso")
                    End Using
                End Using
            Catch ex As Exception
                MsgBox($"Não foi possível conectar {vbNewLine} {vbNewLine} {ex.Message}", vbCritical)

            End Try
            CarregaRegistros()
        End If

    End Sub

    Private Sub btnAdionar_Click(sender As Object, e As EventArgs) Handles btnAdionar.Click
        Dim frm As New FormClienteAdicionar(0)
        frm.ShowDialog()

        CarregaRegistros()
    End Sub

    Private Sub dgvClientes_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvClientes.CellDoubleClick
        'Dim id = dgvClientes.Rows(dgvClientes.CurrentCell.RowIndex).Cells(0).Value
        Dim id = dgvClientes.Rows(e.RowIndex).Cells(0).Value
        Dim frm As New FormClienteAdicionar(id)
        frm.ShowDialog()

        CarregaRegistros()
    End Sub
End Class