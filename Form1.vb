Imports System.Data.OleDb
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        showData()
    End Sub

    'Untuk menampilkan data dari database
    Sub showData()
        connectDb()

        'Query untuk mengambil data dari database
        da = New OleDbDataAdapter("SELECT * from tmhs", connection)
        dt = New DataTable
        da.Fill(dt)
        dgToDoList.DataSource = dt

        disconnectDb()
    End Sub

    Sub ClearTextBox()
        'Clear TextBox
        tbKegiatan.Clear()
        tbTimeline.Clear()
        tbKeterangan.Clear()
        tbKegiatan.Focus()
    End Sub

    'Untuk Menyimpan data ke Database
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        If String.IsNullOrEmpty(tbKegiatan.Text) Or String.IsNullOrEmpty(tbTimeline.Text) Or String.IsNullOrEmpty(tbKeterangan.Text) Then
            MsgBox("Data harus Diisi Terlebih dahulu!")
        Else
            If ketemu = False Then
                'Simpan Data Baru'

                connectDb()
                'Query untuk memasukan data ke Database
                cmd = New OleDbCommand("INSERT INTO tmhs (kegiatan, timeline, keterangan) VALUES ('" & tbKegiatan.Text & "','" & tbTimeline.Text & "','" & tbKeterangan.Text & "')", connection)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Disimpan")
                disconnectDb()

                ClearTextBox()
            Else
                'Edit Data'
                connectDb()
                'Query untuk edit data di Database
                cmd = New OleDbCommand("UPDATE tmhs set timeline = '" & tbTimeline.Text & "',keterangan = '" & tbKeterangan.Text & "' WHERE kegiatan = '" & tbKegiatan.Text & "' ", connection)
                cmd.ExecuteNonQuery()
                MsgBox("Data Berhasil Diubah")
                disconnectDb()

                ClearTextBox()

            End If
        End If

        'Refrest Data
        showData()
    End Sub


    Private Sub tbKegiatan_KeyPress(sender As Object, e As KeyPressEventArgs) Handles tbKegiatan.KeyPress
        If e.KeyChar = Chr(13) Then
            connectDb()
            cmd = New OleDbCommand("SELECT * FROM tmhs WHERE kegiatan= '" & tbKegiatan.Text & "' ", connection)
            dr = cmd.ExecuteReader
            If dr.Read Then
                tbTimeline.Text = dr.Item(1)
                tbKeterangan.Text = dr.Item(2)
                ketemu = True
            Else
                MsgBox("Maaf Data Tidak Ditemukan")
                ketemu = False
            End If
            disconnectDb()

        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        'Menjalankan Function Clear Text Box
        ClearTextBox()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        'Menjalankan perintah untuk mendelete kegiatan
        Dim selected = MessageBox.Show("Apakah Yakin akan Hapus Kegiatan ini?", "Perhatian!", MessageBoxButtons.YesNo)
        If selected = vbYes Then
            connectDb()
            cmd = New OleDbCommand("DELETE FROM tmhs WHERE kegiatan= '" & tbKegiatan.Text & "' ", connection)
            cmd.ExecuteNonQuery()
            MsgBox("Kegiatan Berhasil Dihapus")
            disconnectDb()

            'Refrest Data
            showData()
            ClearTextBox()

        End If


    End Sub


    Private Sub btn_createBy_Click(sender As Object, e As EventArgs) Handles btn_createBy.Click
        'List Pembuat applikasi todo list
        Dim createdByList() As String = {"Rahmat Sugiarto", "Agis Nuryanto"}

        'Melakukan perulangan untuk Memunculkan Message nama pembuat
        For i As Integer = 0 To createdByList.Length - 1
            MsgBox("Created By : " & createdByList.GetValue(i))
        Next
    End Sub

End Class
