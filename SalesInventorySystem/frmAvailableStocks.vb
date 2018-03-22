

Imports System.Data.OleDb
Public Class frmAvailableStocks

    Private Sub LoadItems()
        Dim totalItems As Integer
        Try
            sqL = "SELECT IDescription, UnitPrice, StocksOnHand FROM ITEM Where StocksOnHand > 0 Order By IDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            dgw.Rows.Clear()
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2))
                totalItems += dr(2)
            Loop
            lblTotalStocks.Text = totalItems
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub frmAvailableStocks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadItems()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        frmPrintAvailableStocks.Show()
    End Sub
End Class