Imports MySql.Data.MySqlClient

Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand

    Private Sub ButtonConnect1_Click(sender As Object, e As EventArgs) Handles ButtonConnect1.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=root; database=crud_demo_db; "
        Try
            conn.Open()
            MessageBox.Show("Connected")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub ButtonCreate_Click(sender As Object, e As EventArgs) Handles ButtonCreate.Click
        Dim query As String = "INSERT INTO `crud_demo_db`.`students_tbl` (`name`, `age`, `email`) VALUES (@name, @age, @email);"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db; ")
                conn.Open()
                Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", CInt(TextBoxAge.Text))
                    cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                    cmd.ExecuteNonQuery()
                    MessageBox.Show("Record insert succesful!")
                    TextBoxName.Clear()
                    TextBoxAge.Clear()
                    TextBoxEmail.Clear()
                End Using
            End Using

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub ButtonRead_Click(sender As Object, e As EventArgs) Handles ButtonRead.Click
        Dim query As String = "SELECT name, age, email FROM crud_demo_db.students_tbl;"
        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database=crud_demo_db; ")
                Dim adapter As New MySqlDataAdapter(query, conn)
                Dim Table As New DataTable()
                adapter.Fill(Table)
                DataGridView1.DataSource = Table
                DataGridView1.Columns("id").Visible = False
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentDoubleClick
        If e.RowIndex >= 0 Then
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            TextBoxName.Text = selectedRow.Cells("name").Value.ToString
            TextBoxName.Text = selectedRow.Cells("age").Value.ToString
            TextBoxName.Text = selectedRow.Cells("email").Value.ToString

            TextBoxHiddenId.Text = selectedRow.Cells("id").Value.ToString
        End If

    End Sub
End Class
