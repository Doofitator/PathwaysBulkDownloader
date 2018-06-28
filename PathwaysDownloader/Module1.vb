Imports System.Net
Imports System.Reflection

Module Module1

    Sub Main()
        Console.WriteLine("+------------------------------------------------------------------+")
        Console.WriteLine("| Downloading all Math Pathways modules to the below directory. If |")
        Console.WriteLine("|           files already exist, they will be overwritten.         |")
        Console.WriteLine("+------------------------------------------------------------------+")
        Dim Path As String = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)
        Path = New Uri(Path).LocalPath
        Path = Path + "\pathways_modules"
        System.IO.Directory.CreateDirectory(Path)
        Console.WriteLine(Path)
        downloadModules("NA", Path)
        downloadModules("SP", Path)
        downloadModules("MG", Path)
    End Sub
    Sub downloadModules(Optional Section As String = "NA", Optional Path As String = Nothing)
        Dim i As Int32 = 0
        Dim x As Int32 = 0
        While i <= 500 'For i = 300
            Dim client As New WebClient()
            While x <= 30
                Dim sheetUrl As String = "https://mpcontent.blob.core.windows.net/worksheets/ACM" + Section + i.ToString("D3") + "-" + x.ToString("D2") + "-W.pdf"
                Dim localUrl As String = Path + "\ACM" + Section + i.ToString("D3") + "-" + x.ToString("D2") + "-W.pdf"
                Dim exc As Exception = Nothing
                Try
                    client.DownloadFile(sheetUrl, localUrl)
                Catch ex As Exception
                    'Console.WriteLine("Downloading " + sheetUrl + " to " + localUrl + " errored: 404")
                    exc = ex
                Finally
                    If exc Is Nothing Then
                        Console.WriteLine("Download of " + sheetUrl + " to " + localUrl + " success.")
                    End If
                End Try
                x = x + 1
            End While
            i = i + 1
            x = 0
        End While
    End Sub
End Module
