
Imports System.Data.OleDb
Public Class frmPayment



    Private Sub AddPayment()
        txtCash.Text = txtCash.Text.Replace(",", "")
        Try
            sqL = "INSERT INTO PAYMENT(InvoiceNo,[Cash],[Change]) VALUES('" & frmPOS.lblInvoiceNo.Text & "','" & txtCash.Text & "', '" & txtChange.Text & "')"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub txtCash_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCash.KeyPress
        If e.KeyChar = ControlChars.Cr Then

            If txtCash.Text = "" Then
                MsgBox("Please Enter Amount! ", MsgBoxStyle.Information, "Payment")
                txtCash.Focus()
                Exit Sub
            End If

            If Val(txtCash.Text) < Val(txtTA.Text) Then
                MsgBox("The Amount is Lower than the Cost", MsgBoxStyle.Exclamation, "Payment")
                txtCash.SelectAll()
                txtCash.Focus()
                Exit Sub
            End If
           
            frmPOS.AddPOS()
            AddPayment()
            MsgBox("printing receipt...", MsgBoxStyle.Information, "Receipt")
            frmPrintReceipt.Show()
            frmPOS.NewTransaction()
            frmPOS.txtSearch.Focus()
            Me.Close()
        End If
    End Sub

    Private Sub txtCash_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCash.TextChanged
        txtChange.Text = Format(Val(txtCash.Text.Replace(",", "")) - Val(txtTA.Text.Replace(",", "")), "0.00")
    End Sub

    Private Sub frmPayment_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtTA.Text = frmPOS.lblTotalCost.Text
    End Sub

   
End Class