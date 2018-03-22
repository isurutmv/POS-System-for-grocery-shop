
Imports System.Data.OleDb
Public Class frmLoadItem

    Private Sub LoadItem()
        Try
            sqL = "SELECT ItemNo, itemCode, iDescription, StocksOnHand FROM Item Order By iDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2), dr(3))
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub Search()
        Try
            sqL = "SELECT ItemNo, itemCode, iDescription, StocksOnHand FROM Item WHERE iDescription LIKE '" & TextBox1.Text & "%' Order By iDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2), dr(3))
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub frmLoadItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadItem()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Search()
    End Sub

    Private Sub dgw_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgw.CellDoubleClick
        If frmItem.search = True Then
            frmItem.txtItemNo.Text = dgw.CurrentRow.Cells(0).Value
            frmItem.search = False
            Me.Close()
        End If

        If frmPOS.posSearch = True Then
            frmPOS.txtSearch.Text = dgw.CurrentRow.Cells(0).Value
            frmPOS.posSearch = False
            Me.Close()
        End If
     
    End Sub
End Class