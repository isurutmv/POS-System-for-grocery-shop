
Imports System.Data.OleDb
Public Class frmPrintAvailableStocks

    Private Sub LoadItems()
        Dim totalItems As Integer
        Dim y As Integer
        Try
            sqL = "SELECT IDescription, UnitPrice, StocksOnHand FROM ITEM Where StocksOnHand > 0 Order By IDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            dgw.Rows.Clear()
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2))
                totalItems += dr(2)
                y += 19

            Loop
            Me.Height = Me.Height + y
            Me.Panel1.Height = Me.Panel1.Height + y
            Me.dgw.Height = Me.dgw.Height + y

            Me.Panel4.Location = New Point(1, 132 + y)
            Me.Panel3.Location = New Point(0, 172 + y)
            lblTotalStocks.Text = totalItems
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub frmPrintAvailableStocks_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadItems()
        PrintDialog1.Document = Me.PrintDocument1

        Dim ButtonPressed As DialogResult = PrintDialog1.ShowDialog()
        If (ButtonPressed = DialogResult.OK) Then
            PrintDocument1.Print()
        End If
        Me.Close()
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.Panel1.Width, Me.Panel1.Height)

        Panel1.DrawToBitmap(bm, New Rectangle(0, 0, Me.Panel1.Width, Me.Panel1.Height))

        e.Graphics.DrawImage(bm, 0, 0)
        Dim aPS As New PageSetupDialog
        aPS.Document = PrintDocument1
    End Sub
End Class