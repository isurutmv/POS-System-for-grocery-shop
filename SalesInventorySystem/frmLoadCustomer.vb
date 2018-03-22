
Imports System.Data.OleDb
Public Class frmLoadCustomer

    Private Sub LoadCustomer()
        Try
            sqL = "SELECT CustomerNo, CustName, Address FROM Customer Order By CustName"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2))
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub SearchCustomer()
        Try
            sqL = "SELECT CustomerNo, CustName, Address FROM Customer WHERE CustName LIKE '" & TextBox1.Text & "%' Order By CustName"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2))
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub


    Private Sub dgw_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgw.CellDoubleClick
        If frmCustomer.search = True Then
            frmCustomer.txtCustNo.Text = dgw.CurrentRow.Cells(0).Value
            Me.Close()
        End If

        If frmPOS.isSearchCust = True Then
            frmPOS.lblCustomerNo.Text = dgw.CurrentRow.Cells(0).Value
            frmPOS.lblCustomerName.Text = dgw.CurrentRow.Cells(1).Value
            frmPOS.isSearchCust = False
            Me.Close()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        SearchCustomer()
    End Sub

    Private Sub frmLoadCustomer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadCustomer()
    End Sub
End Class