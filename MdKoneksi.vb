Imports System.Data.OleDb

Module MdKoneksi

    Public cmd As OleDbCommand
    Public da As OleDbDataAdapter
    Public dr As OleDbDataReader
    Public dt As DataTable
    Public ketemu As Boolean

    Public connection As New OleDbConnection(" provider=microsoft.ace.oledb.12.0; data source=dbtodolist.accdb ")

    'Mengkoneksikan Database Ms Access ke Visual Basic
    Sub connectDb()
        Try
            connection.Open()
            ' MsgBox("Koneksi Berhasil Dilakukan")
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox("Koneksi Gagal")
        End Try
    End Sub

    'Mengdikoneksikan Database Ms Access ke Visual Basic
    Sub disconnectDb()
        connection.Close()
    End Sub

End Module
