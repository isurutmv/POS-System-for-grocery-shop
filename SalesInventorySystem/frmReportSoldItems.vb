
Imports System.Data.OleDb
Public Class frmReportSoldItems

    Private Sub LoadSoldItemsReport()
        Dim totalAmount As Double
        Dim totalQuantity As Integer
        Try
            sqL = "SELECT IDescription, UnitPrice, SUM([Quantity]) as ItemQuantity, POSDate, (UnitPrice * SUM([Quantity])) As TotalAmount FROM Item as I, POS as P, POSDetail as PD WHERE I.ItemNo = PD.ItemNo AND PD.InvoiceNo=P.InvoiceNo AND POSDate >= #" & dtpFrom.Text & "# AND POSDate <=#" & dtpTo.Text & "# Group By IDescription, UnitPrice, [Quantity], POSDate ORder By IDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()

            totalAmount = 0
            totalQuantity = 0
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2), dr(4))
                totalAmount += dr(4)
                totalQuantity += dr(2)
            Loop

            lblQuantity.Text = totalQuantity
            lblTotal.Text = Format(totalAmount, "#,##0.00")
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        LoadSoldItemsReport()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmReportSoldItemsPrint.Show()
    End Sub

    Private Sub frmReportSoldItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblQuantity.Text = ""
        lblTotal.Text = ""
        dgw.Rows.Clear()
    End Sub
End Class