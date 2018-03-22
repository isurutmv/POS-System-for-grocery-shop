
Imports System.Data.OleDb
Public Class frmReportStocksInPrint
    Dim fromMonth As String
    Dim toMonth As String
    Dim days As Integer
    Dim totalAmount As Double
    Dim y As Integer

    Private Sub LoadStocksIn()
        Dim totalQuantity As Integer
        Dim totalStocks As Integer
        Try
            sqL = "SELECT SIDate, IDescription, ItemQuantity, CurrentStocks FROM Item as I, StocksIn as S WHERE I.ItemNo = S.ItemNo AND SIDate >= #" & frmReportStocksIn.dtpFrom.Text & "# AND SIDate <=#" & frmReportStocksIn.dtpTo.Text & "# Order By SIDate, IDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            totalQuantity = 0
            totalStocks = 0
            y = 0
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2), dr(3))
                totalQuantity += dr(2)
                y += 19

            Loop
            lblStocksin.Text = totalQuantity
            Me.Height = Me.Height + y
            Me.Panel2.Height = Me.Panel2.Height + y
            Panel4.Location = New Point(8, 227 + y)
            Panel1.Location = New Point(7, 185 + y)
            dgw.Height = dgw.Height + y
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub



    Private Sub frmReportStocksInPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblDate.Text = Date.Now.ToString("MM/dd/yyyy")
        Dim d1 As Date = Format(frmReportStocksIn.dtpFrom.Value, "Short Date")
        Dim d2 As Date = Format(frmReportStocksIn.dtpTo.Value, "Short Date")

        fromMonth = frmReportStocksIn.dtpFrom.Value.ToString("MMMM")
        toMonth = frmReportStocksIn.dtpTo.Value.ToString("MMMM")

        days = DateDiff(DateInterval.Day, d1, d2)
        If days <= 7 Then
            lblreport.Text = "Weekly Inventory"
        Else
            lblreport.Text = "Monthly Inventory"
        End If

        If fromMonth = "January" And toMonth = "March" Then
            lblreport.Text = "First Quarter of the Year Inventory"
        ElseIf fromMonth = "April" And toMonth = "June" Then
            lblreport.Text = "Second Quarter of the Year Inventory"
        ElseIf fromMonth = "July" And toMonth = "September" Then
            lblreport.Text = "Third Quarter of the Year Inventory"
        ElseIf fromMonth = "October" And toMonth = "December" Then
            lblreport.Text = "Fourth Quarter of the Year Inventory"
        ElseIf fromMonth = "January" And toMonth = "December" Then
            lblreport.Text = "Inventory for the Year of " & frmReportStocksIn.dtpFrom.Value.ToString("yyyy")

        End If

        LoadStocksIn()
        PrintDialog1.Document = Me.PrintDocument1

        Dim ButtonPressed As DialogResult = PrintDialog1.ShowDialog()
        If (ButtonPressed = DialogResult.OK) Then
            PrintDocument1.Print()
        End If
        Me.Close()
    End Sub

    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.Panel2.Width, Me.Panel2.Height)

        Panel2.DrawToBitmap(bm, New Rectangle(0, 0, Me.Panel2.Width, Me.Panel2.Height))

        e.Graphics.DrawImage(bm, 0, 0)
        Dim aPS As New PageSetupDialog
        aPS.Document = PrintDocument1
    End Sub
End Class