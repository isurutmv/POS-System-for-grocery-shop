
Imports System.Data.OleDb
Public Class frmPrintReceipt
    Dim getTotal As Double
    Dim getQuantity As Double

    Private Sub LoadCustomer()
        Try
            sqL = "SELECT CustName FROM Customer As C, POS as P WHERE C.CustomerNo = P.CustomerNo AND P.InvoiceNo = '" & frmPOS.lblInvoiceNo.Text & "'"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read = True Then
                lblCustomer.Text = "Customer  :  " & dr("CustName")
            Else
                MsgBox("Customer not found")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub GetVatNonVat()
        Try
            sqL = "Select VatAmount, NonVatAmount FROM POS WHERE InvoiceNo = '" & frmPOS.lblInvoiceNo.Text & "'"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.Read = True Then
                lblVat.Text = Format(dr("VatAmount"), "#,##0.00")
                lblSubtotal.Text = Format(dr("NonVatAmount"), "#,##0.00")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub


    Private Sub getCashChange()
        Try
            sqL = "SELECT * FROM PAYMENT WHERE INvoiceNo = '" & frmPOS.lblInvoiceNo.Text & "'"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read = True Then
                lblCash.Text = Format(dr("Cash"), "#,##0.00")
                lblChange.Text = Format(dr("Change"), "#,##0.00")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub getEmployee()
        Try
            sqL = "SELECT S.StaffID, LastName, FirstName FROM STaff as S, POS as P WHERE S.StaffID = P.StaffID AND InvoiceNo = '" & frmPOS.lblInvoiceNo.Text & "'"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If dr.Read = True Then
                lblEmpName.Text = dr("FirstName") & " " & dr("LastName")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub



    Private Sub LoadReceipt()
        Dim x As Integer = 0
        Dim Y As Integer = 0
        Try
            sqL = "SELECT IDescription, itemprice, [Quantity], POSDATE, (itemPrice * [Quantity]) as TotalPrice FROM [Item] as I, POSDetail as PD, POS as P WHERE I.ItemNo = PD.itemNo And PD.InvoiceNO = P.InvoiceNo AND P.InvoiceNo = '" & frmPOS.lblInvoiceNo.Text & "' ORDER By iDescription"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            dgw.Rows.Clear()
            getTotal = 0.0
            getQuantity = 0.0
            Do While dr.Read = True
                dgw.Rows.Add(dr("Quantity"), dr("iDescription"), dr("Itemprice"), dr("TotalPrice"))
                getTotal += dr("TotalPrice")
                getQuantity += dr(2)
                dgw.Height += 19
                x += 19
            Loop
            Y = x - 30
            dgw.Height = dgw.Height - 20
            lblTotal.Text = Format(getTotal, "#,##0.00")
            lblVat.Location = New Point(204, 212 + Y)
            vat.Location = New Point(4, 212 + Y)
            subTotal.Location = New Point(3, 227 + Y)
            lblSubtotal.Location = New Point(203, 227 + Y)
            TotalAmount.Location = New Point(3, 245 + Y)
            lblTotal.Location = New Point(203, 245 + Y)
            Cash.Location = New Point(3, 262 + Y)
            lblCash.Location = New Point(203, 262 + Y)
            change.Location = New Point(3, 280 + Y)
            lblChange.Location = New Point(203, 280 + Y)
            lblLine.Location = New Point(4, 299 + Y)
            lblOR.Location = New Point(46, 315 + Y)
            lblThank.Location = New Point(62, 331 + Y)
            Panel1.Height = Panel1.Height + Y
            Me.Height = Me.Height + Y
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub
    Private Sub PrintDocument1_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Dim bm As New Bitmap(Me.Panel1.Width, Me.Panel1.Height)

        Panel1.DrawToBitmap(bm, New Rectangle(0, 0, Me.Panel1.Width, Me.Panel1.Height))

        e.Graphics.DrawImage(bm, 0, 0)
        Dim aPS As New PageSetupDialog
        aPS.Document = PrintDocument1
    End Sub


    Private Sub frmPrintReceipt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label7.Text = Format(Date.Now, "Short Date")
        Label9.Text = Format(Date.Now, "Long Time")
        lblInvoice.Text = frmPOS.lblInvoiceNo.Text
        LoadCustomer()
        GetVatNonVat()
        getCashChange()
        getEmployee()
        LoadReceipt()
        PrintDocument1.Print()
        frmPOS.txtSearch.Focus()
        Me.Close()
    End Sub

    Private Sub dgw_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgw.CellContentClick

    End Sub
End Class