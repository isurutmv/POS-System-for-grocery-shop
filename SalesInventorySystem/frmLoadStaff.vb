
Imports System.Data.OleDb
Public Class frmLoadStaff

    Private Sub LoadStaff()
        Try
            sqL = "SELECT StaffID, Lastname & ', ' & Firstname & ' ' & mi as StaffName, [Position] FROM STAFF Order by Lastname"
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

    Private Sub SearchStaff()
        Try
            sqL = "SELECT StaffID, Lastname & ', ' & Firstname & ' ' & mi as StaffName, [Position] FROM STAFF WHERE Lastname LIKE '" & TextBox1.Text & "%' Order by Lastname"
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

    Private Sub frmLoadStaff_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        LoadStaff()
    End Sub

    Private Sub dgw_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgw.CellDoubleClick
        If frmStaff.search = True Then
            frmStaff.txtStaffID.Text = dgw.CurrentRow.Cells(0).Value
            Me.Close()
        End If
        If frmUsers.uSearch = True Then
            frmUsers.txtStaffID.Text = dgw.CurrentRow.Cells(0).Value
            frmUsers.uSearch = False
            Me.Close()
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        SearchStaff()
    End Sub
End Class