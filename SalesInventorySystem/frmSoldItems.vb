
Imports System.Data.OleDb
Public Class frmSoldItems

    Private Sub LoadSoldItems()
        Dim totalAmount As Double
        Dim totalQuantity As Integer

        If chkDaily.CheckState = CheckState.Checked Then
            sqL = "SELECT IDescription, UnitPrice, SUM([Quantity]) as ItemQuantity, POSDate, (UnitPrice * SUM([Quantity])) As TotalAmount FROM Item as I, POS as P, POSDetail as PD WHERE I.ItemNo = PD.ItemNo AND PD.InvoiceNo=P.InvoiceNo AND DAY(POSDate) =" & Format(DateTimePicker1.Value, "dd") & " Group By IDescription, UnitPrice, [Quantity], POSDate ORder By IDescription"
        End If
        If chkMonthly.CheckState = CheckState.Checked Then
            sqL = "SELECT IDescription, UnitPrice, SUM([Quantity]) as ItemQuantity, POSDate, (UnitPrice * SUM([Quantity])) As TotalAmount FROM Item as I, POS as P, POSDetail as PD WHERE I.ItemNo = PD.ItemNo AND PD.InvoiceNo=P.InvoiceNo AND MONTH(POSDate) =" & Format(DateTimePicker1.Value, "MM") & " Group By IDescription, UnitPrice, [Quantity], POSDate ORder By IDescription"
        End If

        Try
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            totalAmount = 0
            totalQuantity = 0
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2), dr(4))
                totalQuantity += dr(2)
                totalAmount += dr(4)
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

    Private Sub frmSoldItems_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblQuantity.Text = ""
        lblTotal.Text = ""
        dgw.Rows.Clear()
    End Sub

    Private Sub chkDaily_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDaily.CheckedChanged
        If chkDaily.CheckState = CheckState.Checked Then
            chkMonthly.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub chkMonthly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMonthly.CheckedChanged
        If chkMonthly.CheckState = CheckState.Checked Then
            chkDaily.CheckState = CheckState.Unchecked
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If chkDaily.CheckState = CheckState.Unchecked And chkMonthly.CheckState = CheckState.Unchecked Then
            MsgBox("Please select either daily or monthly", MsgBoxStyle.Critical, "Select")
            Exit Sub
        End If
        LoadSoldItems()
    End Sub
End Class