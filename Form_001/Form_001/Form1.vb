'
' 본 스크립트는 닷넷 윈폼을 이용하여 윈도우창과 HTML/JS 또는 플래시와 연동는 샘플을 제공합니다
' @Name: VisualBasic.NET WinForm
' @Author: HobisJung
' @Date: 2014-06-25
'
'
Public NotInheritable Class Form1

    ' # 생성자
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    ' # 폼 로드 핸들러
    Private Sub p_Form1_Load(ByVal sender As Object, ByVal ea As EventArgs) Handles MyBase.Load
        Me.p_InitOnce()
    End Sub

    ' # 브라우저_컨트롤 Dom 로드 완료 핸들러
    Private Sub p_WebBrowser1_DocumentCompleted(ByVal sender As Object, ByVal wdcea As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        '
    End Sub

    ' # 풀스크립 설정
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



    ' - 윈도우 초기화
    Private Const Win_Init As String = "Win_Init"

    ' - 윈도우 타이틀 변경
    Private Const Win_Set_Title As String = "Win_Set_Title"

    ' - 윈도우 보이기/안보이기
    Private Const Win_Visible As String = "Win_Visible"

    ' - 윈도우 리사이즈 최대
    Private Const Win_Resize_Max As String = "Win_Resize_Max"

    ' - 윈도우 리사이즈 최소
    Private Const Win_Resize_Min As String = "Win_Resize_Min"

    ' - 윈도우 리사이즈 기본
    Private Const Win_Resize_Normal As String = "Win_Resize_Normal"

    ' - 윈도우 리사이즈
    Private Const Win_Resize As String = "Win_Resize"

    ' - 윈도우 위치 변경
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
        Me.Js_CallBack(type, Nothing)
    End Sub

    ' :: Js 호출
    Public Sub Js_CallBack(ByVal type As String, ByVal args As Object())
        Select Case type
            Case Win_Init

            Case Win_Set_Title
                Dim t_name As String = CType(args(0), String)
                Me.Text = t_name

            Case Win_Visible
                Dim t_b As Boolean = CType(args(0), Boolean)
                Me.Visible = t_b

            Case Win_Resize_Max
                Me.WindowState = FormWindowState.Maximized

            Case Win_Resize_Min
                Me.WindowState = FormWindowState.Minimized

            Case Win_Resize_Normal
                Me.WindowState = FormWindowState.Normal

            Case Win_Resize
                Me.WindowState = FormWindowState.Normal

                Dim t_s As Size = Me.Size
                t_s.Width = CType(args(0), Integer)
                t_s.Height = CType(args(1), Integer)
                Me.Size = t_s

            Case Win_Location
                Me.WindowState = FormWindowState.Normal

                Dim t_p As Point = Me.Location
                t_p.X = CType(args(0), Integer)
                t_p.Y = CType(args(1), Integer)
                Me.Location = t_p

        End Select
    End Sub






    ' - 메인이름
    Private Const _name As String = "HobisWin"

    ' - 버전
    Private Const _ver As String = "Ver 1.0"

    ' - 타이틀
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
        'Me.WebBrowser1.Navigate("http://purecss.io/forms/")
    End Sub

End Class

