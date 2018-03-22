
Imports System.Data.OleDb
Public Class frmReportSoldItemsPrint

    Dim fromMonth As String
    Dim toMonth As String
    Dim days As Integer
    Dim totalAmount As Double
    Dim y As Integer

    Private Sub LoadReportSoldItems()

        Try
            sqL = "SELECT IDescription, UnitPrice, SUM([Quantity]) as ItemQuantity, POSDate, (UnitPrice * SUM([Quantity])) As TotalAmount FROM Item as I, POS as P, POSDetail as PD WHERE I.ItemNo = PD.ItemNo AND PD.InvoiceNo=P.InvoiceNo AND POSDate >= #" & frmReportSoldItems.dtpFrom.Text & "# AND POSDate <=#" & frmReportSoldItems.dtpTo.Text & "# Group By IDescription, UnitPrice, [Quantity], POSDate ORder By IDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            Do While dr.Read = True
                dgw.Rows.Add(dr(0), dr(1), dr(2), dr(4))
                y += 19
                totalAmount += dr(4)
            Loop
            lblAmount.Text = Format(totalAmount, "#,##0.00")
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


    Private Sub frmReportSoldItemsPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblDate.Text = Date.Now.ToString("MM/dd/yyyy")

        Dim d1 As Date = Format(frmReportSoldItems.dtpFrom.Value, "Short Date")
        Dim d2 As Date = Format(frmReportSoldItems.dtpTo.Value, "Short Date")

        fromMonth = frmReportSoldItems.dtpFrom.Value.ToString("MMMM")
        toMonth = frmReportSoldItems.dtpTo.Value.ToString("MMMM")

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
            lblreport.Text = "Inventory for the Year of " & frmReportSoldItems.dtpFrom.Value.ToString("yyyy")

        End If

        LoadReportSoldItems()
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