
Imports System.Data.OleDb
Public Class frmLogin


    Private Sub Login()
        Try
            sqL = "SELECT * FROM Users WHERE Username = '" & txtUser.Text & "' AND pwd = '" & txtPwd.Text & "'"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.Read = True Then
                frmMain.lblEmployeeNo.Text = dr("StaffID")
                txtUser.Text = ""
                txtPwd.Text = ""
                Me.Close()
            Else
                MsgBox("Incorrect username or password!", MsgBoxStyle.Critical, "Login")
                txtPwd.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub


   
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Login()
    End Sub

    Private Sub txtUser_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUser.GotFocus
        AcceptButton = btnLogin
    End Sub


    Private Sub txtPwd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPwd.GotFocus
        AcceptButton = btnLogin
    End Sub

    Private Sub btncancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncancel.Click

        If MsgBox("Are you sure you want to close?", MsgBoxStyle.YesNo, "Close Window") = MsgBoxResult.Yes Then
            End
        End If
        txtUser.Focus()
    End Sub
End Class