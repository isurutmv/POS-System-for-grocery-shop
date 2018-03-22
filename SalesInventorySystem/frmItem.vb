
Imports System.Data.OleDb
Public Class frmItem

    Dim adding As Boolean
    Dim updating As Boolean
    Public search As Boolean
    Dim getStocksOnHand As Integer

    Private Sub GetQuantity()
        Try
            sqL = "SELECT StocksOnhand FROM Item WHERE ItemNo = " & txtItemNo.Text & ""
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.Read = True Then
                getStocksOnHand = dr(0)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub AddStocksInUpdating()
        If txtItemNo.Text = "" Then
            MsgBox("Please select item to add stocks.", MsgBoxStyle.Critical, "Select Item")
            Exit Sub
        End If
        Dim strStocksIn As String
        strStocksIn = InputBox("Enter number of items : ", "Stocks In")
        txtQuantity.Text = Val(txtQuantity.Text) + Val(strStocksIn)
        Try
            sqL = "INSERT INTO StocksIn(ItemNo, ItemQuantity, SIDate, CurrentStocks) VALUES('" & txtItemNo.Text & "', '" & strStocksIn & "', '" & Format(Date.Now, "Short Date") & "', " & txtQuantity.Text & ")"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            Dim i As Integer

            i = cmd.ExecuteNonQuery()
            If i > 0 Then
                MsgBox("Stocks added successfully", MsgBoxStyle.Information, "Stocks In")
                Updateproduct()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub AddStocksInAdding()
        Try
            sqL = "INSERT INTO StocksIn(ItemNo, ItemQuantity, SIDate) VALUES('" & txtItemNo.Text & "', '" & txtQuantity.Text & "', '" & Format(Date.Now, "Short Date") & "')"
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

    Private Sub GetItemNo()
        Try
            sqL = "SELECT ItemNo From Item Order By ItemNo desc"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.Read = True Then
                txtItemNo.Text = dr(0) + 1
            Else
                txtItemNo.Text = 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub AddItem()
        txtUnitPrice.Text = txtUnitPrice.Text.Replace(",", "")
        Try
            sqL = "Insert Into Item(ItemCode, iDescription, iSize, StocksOnHand, UnitPrice, ReproduceLevel) VALUES('" & txtItemCode.Text & "', '" & txtDescription.Text & "', '" & txtSize.Text & "', '" & txtQuantity.Text & "', '" & txtUnitPrice.Text & "', '" & txtReproduceLevel.Text & "')"
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            Dim i As Integer
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Item Added", MsgBoxStyle.Information, "Add Item")
            Else
                MsgBox("Failed to add item", MsgBoxStyle.Critical, "Add Item")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub UpdateItem()
        txtUnitPrice.Text = txtUnitPrice.Text.Replace(",", "")
        Try
            sqL = "UPDATE Item SET ItemCode = '" & txtItemCode.Text & "', iDescription = '" & txtDescription.Text & "', iSize ='" & txtSize.Text & "', StocksOnHand = '" & txtQuantity.Text & "', UnitPrice = '" & txtUnitPrice.Text & "', ReproduceLevel = '" & txtReproduceLevel.Text & "' WHERE ItemNo = " & txtItemNo.Text & ""
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            Dim i As Integer
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Item Updated", MsgBoxStyle.Information, "Update Item")
            Else
                MsgBox("Failed to update item", MsgBoxStyle.Information, "Update Item")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub

    Private Sub GetItemRecord()
        Try
            sqL = "SELECT ItemCode, iDescription, iSize, StocksOnHand, UnitPrice, ReproduceLevel FROm Item Where ItemNo = " & txtItemNo.Text & ""
            ConnDB()
            cmd = New OleDbCommand(sqL, conn)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.Read = True Then
                txtItemCode.Text = dr(0)
                txtDescription.Text = dr(1)
                txtSize.Text = dr(2)
                txtQuantity.Text = dr(3)
                txtUnitPrice.Text = dr(4)
                txtReproduceLevel.Text = dr(5)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            conn.Close()
        End Try
    End Sub
   
    Private Sub ClearFields()
        txtItemNo.Text = ""
        txtItemCode.Text = ""
        txtDescription.Text = ""
        txtSize.Text = ""
        txtQuantity.Text = ""
        txtUnitPrice.Text = ""
        txtReproduceLevel.Text = ""
    End Sub

    Private Sub EnabledText()
        txtItemNo.Enabled = True
        txtItemCode.Enabled = True
        txtDescription.Enabled = True
        txtSize.Enabled = True
        txtQuantity.Enabled = True
        txtUnitPrice.Enabled = True
        txtReproduceLevel.Enabled = True
    End Sub

    Private Sub DisabledText()
        txtItemNo.Enabled = False
        txtItemCode.Enabled = False
        txtDescription.Enabled = False
        txtSize.Enabled = False
        txtQuantity.Enabled = False
        txtUnitPrice.Enabled = False
        txtReproduceLevel.Enabled = False

    End Sub

    Private Sub EnabledButton()
        btnAdd.Enabled = True
        btnUpdate.Enabled = True
        btnSearch.Enabled = True
        btnClose.Enabled = True

        btnSave.Enabled = False
        btnCancel.Enabled = False
    End Sub

    Private Sub DisabledButton()
        btnAdd.Enabled = False
        btnUpdate.Enabled = False
        btnSearch.Enabled = False
        btnClose.Enabled = False

        btnSave.Enabled = True
        btnCancel.Enabled = True
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        adding = True
        updating = False

        EnabledText()
        DisabledButton()
        ClearFields()
        GetItemNo()
        txtItemCode.Focus()
        txtItemNo.Enabled = False
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        adding = False
        updating = True
        EnabledText()
        DisabledButton()
        txtItemCode.Focus()
        txtItemNo.Enabled = False
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        search = True
        frmLoadItem.ShowDialog()
    End Sub

    Private Sub txtItemNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtItemNo.TextChanged
        If search = True Then
            GetItemRecord()
            search = False
        End If
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub Updateproduct()
        GetQuantity()
        UpdateItem()
        DisabledText()
        EnabledButton()
        ClearFields()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If adding = True Then
            AddItem()
            AddStocksInAdding()
            DisabledText()
            EnabledButton()
            ClearFields()
        Else
            Updateproduct()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DisabledText()
        EnabledButton()
        ClearFields()
    End Sub

    Private Sub frmItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        EnabledButton()
        DisabledText()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        AddStocksInUpdating()
    End Sub
End Class