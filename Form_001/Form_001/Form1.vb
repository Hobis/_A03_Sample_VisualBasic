Public NotInheritable Class Form1

    '
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    '
    Private Sub p_Form1_Load(ByVal sender As Object, ByVal ea As EventArgs) Handles MyBase.Load
        Me.p_InitOnce()
    End Sub


    '
    Private Sub p_WebBrowser1_DocumentCompleted(ByVal sender As Object, ByVal wdcea As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

    End Sub

    '
    Private Sub p_SetFullScreen(ByVal b As Boolean)
        If b Then
            Me.TopMost = True
            Me.FormBorderStyle = FormBorderStyle.None
            Me.WindowState = FormWindowState.Maximized
            'Me.Bounds = Screen.PrimaryScreen.Bounds
        Else
            Me.WindowState = FormWindowState.Normal
            Me.FormBorderStyle = FormBorderStyle.Sizable
        End If
    End Sub



    Private Const Js_Init As String = "Js_Init"
    Private Const Win_Rename As String = "Win_Rename"
    Private Const Win_Visible As String = "Win_Visible"
    Private Const Win_Resize_Max As String = "Win_Resize_Max"
    Private Const Win_Resize_Min As String = "Win_Resize_Min"
    Private Const Win_Resize_Normal As String = "Win_Resize_Normal"
    Private Const Win_Resize As String = "Win_Resize"
    Private Const Win_Location As String = "Win_Location"


    ' :: Js 함수 호출
    Private Sub p_Js_Call(ByVal funcName As String, ByVal args As Object())
        Try
            Me.WebBrowser1.Document.InvokeScript(funcName, args)
        Catch ex As Exception

        End Try
    End Sub

    ' :: Js 호출 노멀
    Public Sub Js_CallBack_n(ByVal type As String)
        'Me.Js_CallBack(type, null)
    End Sub

    ' :: Js 호출
    Public Sub Js_CallBack(ByVal type As String, ByVal args As Object())
        Select Case type
            Case Js_Init

            Case Win_Rename
                Dim t_name As String = CType(args(0), String)
                Me.Text = t_name

            Case Win_Visible

            Case Win_Resize_Max

            Case Win_Resize_Min

            Case Win_Resize_Normal

            Case Win_Resize

            Case Win_Location
        End Select
    End Sub






    '
    Private Const _name As String = "MainWin"

    '
    Private Const _ver As String = "Ver 1.0"

    '
    Private Const _title As String = _name & " " & _ver


    ' 초기화 한번
    Private Sub p_InitOnce()
        'Me.AutoSizeMode = AutoSizeMode.GrowAndShrink
        'Me.MaximizeBox = False
        'Me.StartPosition = FormStartPosition.Manual
        'Me.Location = New Point(10, 10)
        'Me.Width = 600
        'Me.Height = 700
        'Me.p_SetFullScreen(True)
        Dim t_s As Size = New Size()
        t_s.Width = 800
        t_s.Height = 600
        Me.ClientSize = t_s
        Me.MinimumSize = Me.Size
        Me.Text = _title

        Me.WebBrowser1.ObjectForScripting = Me
        Me.WebBrowser1.IsWebBrowserContextMenuEnabled = False
        Me.WebBrowser1.AllowWebBrowserDrop = False
        Me.WebBrowser1.WebBrowserShortcutsEnabled = False
        Me.WebBrowser1.ScrollBarsEnabled = False

        Dim t_src As String = Path.Combine(Environment.CurrentDirectory, "Main.html")
        Me.WebBrowser1.Navigate(t_src)
    End Sub


End Class
