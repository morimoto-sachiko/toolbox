Option Explicit

Public Sub ExportTableToCsv()
    
    Dim ws As Worksheet
    Dim tbl As ListObject
    Dim filePath As String
    
    '===== 設定 =====
    Set ws = ThisWorkbook.Worksheets("Sheet1")
    Set tbl = ws.ListObjects("Table1")
    
    filePath = Environ$("USERPROFILE") & "\Documents\table_export.csv"
    
    '===== 実行 =====
    Call WriteTableCsv(tbl, filePath)
    
    MsgBox "CSV Export Complete: " & filePath
    
End Sub


Private Sub WriteTableCsv(ByVal table As ListObject, ByVal filePath As String)

    Dim fso As Object
    Dim ts As Object
    
    Dim r As Long
    Dim c As Long
    
    Dim line As String
    Dim value As String
    
    Set fso = CreateObject("Scripting.FileSystemObject")
    Set ts = fso.CreateTextFile(filePath, True, False) 'ASCII
    
    '========================
    ' Header
    '========================
    
    line = ""
    
    For c = 1 To table.ListColumns.Count
        
        If c > 1 Then line = line & ","
        
        line = line & EscapeCsv(table.ListColumns(c).Name)
        
    Next
    
    ts.WriteLine line
    
    
    '========================
    ' Rows
    '========================
    
    For r = 1 To table.DataBodyRange.Rows.Count
        
        line = ""
        
        For c = 1 To table.ListColumns.Count
            
            If c > 1 Then line = line & ","
            
            value = table.DataBodyRange.Cells(r, c).Value
            
            line = line & EscapeCsv(value)
            
        Next
        
        ts.WriteLine line
        
    Next
    
    
    ts.Close
    
End Sub


Private Function EscapeCsv(ByVal text As String) As String
    
    text = Replace(text, """", """""")
    
    If InStr(text, ",") > 0 Or _
       InStr(text, """") > 0 Or _
       InStr(text, vbLf) > 0 Then
        
        text = """" & text & """"
        
    End If
    
    EscapeCsv = text
    
End Function