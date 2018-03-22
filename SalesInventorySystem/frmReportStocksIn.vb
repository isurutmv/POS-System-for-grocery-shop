

Imports System.Data.OleDb
Public Class frmReportStocksIn

    Private Sub LoadStocksIn()
        Dim totalQuantity As Integer
        Dim totalStocks As Integer
        Try
            sqL = "SELECT SIDate, IDescription, ItemQuantity, CurrentStocks FROM Item as I, StocksIn as S WHERE I.ItemNo = S.ItemNo AND SIDate >= #" & dtpFrom.Text & "# AND SIDate <=#" & dtpTo.Text & "# Order By SIDate, IDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            totalQuantity = 0
            totalStocks = 0
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2), dr(3))
                totalQuantity += dr(2)
                totalStocks += dr(2)
            Loop
            lblQuantity.Text = Format(totalQuantity, "#,##0")
            'lblTotal.Text = Format(totalStocks, "#,##0")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub frmReportStocksIn_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        LoadStocksIn()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmReportStocksInPrint.Show()
    End Sub
End Class