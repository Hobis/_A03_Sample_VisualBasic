Imports System.Runtime.InteropServices

'---------------------------------------------------------------------------------------------------------------------------
' # 본 스크립트는 닷넷 윈폼을 이용하여 윈도우창과 HTML/JS 또는 플래시와 연동는 샘플을 제공합니다
' @Name: VisualBasic.NET WinForm
' @Author: HobisJung
' @Date: 2014-07-26
'---------------------------------------------------------------------------------------------------------------------------
Public NotInheritable Class Form1

    ' :: 생성자
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.p_InitOnce()
    End Sub

    ' # 초기화 한번
    Private Sub p_InitOnce()
        Dim t_s As Size = Me.Size
        't_s.Width = 1170
        't_s.Height = 796
        t_s.Width = 1024
        t_s.Height = 768
        Me.ClientSize = t_s
        Me.MinimumSize = Me.Size
        Me.Text = "Main"

        Me.WebBrowser1.ObjectForScripting = Me
        Me.WebBrowser1.IsWebBrowserContextMenuEnabled = False
        Me.WebBrowser1.AllowWebBrowserDrop = False
        Me.WebBrowser1.WebBrowserShortcutsEnabled = False
        Me.WebBrowser1.ScrollBarsEnabled = False

        Dim t_name As String = Path.GetFileNameWithoutExtension(Application.ExecutablePath)
        Dim t_src As String = Path.Combine(Environment.CurrentDirectory, t_name & ".html")
        Me.WebBrowser1.Navigate(t_src)
    End Sub

    ' -
    Private _bFirst As Boolean = True
    ' ::
    Protected Overrides Sub SetVisibleCore(ByVal value As Boolean)
        If (Me._bFirst) Then
            MyBase.SetVisibleCore(False)
            Me._bFirst = False
        Else
            MyBase.SetVisibleCore(value)
        End If
    End Sub

    ' :: 폼 로드 핸들러
    Private Sub p_Form1_Load(ByVal sender As Object, ByVal ea As EventArgs) Handles MyBase.Load
        '
    End Sub

    ' :: 브라우저_컨트롤 Dom 로드 완료 핸들러
    Private Sub p_WebBrowser1_DocumentCompleted(ByVal sender As Object, ByVal wdcea As WebBrowserDocumentCompletedEventArgs) _
        Handles WebBrowser1.DocumentCompleted
        '
        RemoveHandler Me.WebBrowser1.DocumentCompleted, AddressOf Me.p_WebBrowser1_DocumentCompleted

        MyBase.SetVisibleCore(True)
    End Sub

    ' :: 웹브라우저 키다운 핸들러
    Private Sub p_WebBrowser1_PreviewKeyDown(ByVal sender As Object, ByVal pkdea As PreviewKeyDownEventArgs) _
        Handles WebBrowser1.PreviewKeyDown
        '
        If pkdea.KeyCode.Equals(Keys.Escape) Then
            Me.p_SetFullScreen(False)
        ElseIf pkdea.KeyCode.Equals(Keys.F5) Then
            'Me.WebBrowser1.Refresh()
            Me.p_Js_Call("p_reload", Nothing)
        End If
    End Sub

    Private Const WM_SYSCOMMAND As Integer = &H112
    Private Const SC_MAXIMIZE As Integer = &HF030
    ' :: WndProcess 루프함수
    Protected Overrides Sub WndProc(ByRef m As Message)
        If (m.Msg.Equals(WM_SYSCOMMAND)) Then
            If (m.WParam.ToInt32.Equals(SC_MAXIMIZE)) Then
                Me.p_FullScreen()
                Return
            End If

            'ElseIf (m.Msg.Equals(&H216) Or m.Msg.Equals(&H214)) Then
            '    Dim t_rect As _Rect = _
            '        CType(Marshal.PtrToStructure(m.LParam, GetType(_Rect)), _Rect)
            '    Dim t_w As Integer = t_rect.Right - t_rect.Left
            '    Dim t_h As Integer = t_rect.Bottom - t_rect.Top

            '    Dim t_z As Integer
            '    If (t_w > t_h) Then
            '        t_z = t_w
            '    Else
            '        t_z = t_h
            '    End If

            '    t_rect.Bottom = t_rect.Top + t_z
            '    t_rect.Right = t_rect.Left + t_z

            '    Marshal.StructureToPtr(t_rect, m.LParam, False)
            '    m.Result = CType(1, IntPtr)

            '    Return
        End If

        MyBase.WndProc(m)
    End Sub

    ' -
    <StructLayout(LayoutKind.Sequential)> _
    Private Structure _Rect
        Public Left As Integer
        Public Right As Integer
        Public Top As Integer
        Public Bottom As Integer
    End Structure


    '
    Private _tempSize As Size
    ' :: 풀스크린 설정
    Private Sub p_SetFullScreen(ByVal b As Boolean)
        If b Then
            If (Me.TopMost.Equals(False)) Then
                Me.TopMost = True
                Me._tempSize = Me.Size
                Me.FormBorderStyle = FormBorderStyle.None
                Me.WindowState = FormWindowState.Maximized
                'Me.Bounds = Screen.PrimaryScreen.Bounds
                Me.WebBrowser1.Focus()
            End If
        Else
            If (Me.TopMost.Equals(True)) Then
                Me.TopMost = False
                Me.WindowState = FormWindowState.Normal
                Me.FormBorderStyle = FormBorderStyle.Sizable
                Me.Size = Me._tempSize
                Me._tempSize = Nothing
            End If
        End If
    End Sub

    ' :: 풀스크린 토글
    Private Sub p_FullScreen()
        If Me.TopMost Then
            Me.p_SetFullScreen(False)
        Else
            Me.p_SetFullScreen(True)
        End If
    End Sub


    ' - 윈도우 초기화
    Private Const Win_Init As String = "Win_Init"

    ' - 윈도우 설정 타이틀
    Private Const Win_Set_Title As String = "Win_Set_Title"

    ' - 윈도우 설정 보이기/안보이기
    Private Const Win_Set_Visible As String = "Win_Set_Visible"

    ' - 윈도우 설정 최소사이즈
    Private Const Win_Set_MinSize As String = "Win_Set_MinSize"

    ' - 윈도우 설정 위치
    Private Const Win_Set_Location As String = "Win_Set_Location"

    ' - 윈도우 리사이즈 최대
    Private Const Win_Resize_Max As String = "Win_Resize_Max"

    ' - 윈도우 리사이즈 최소
    Private Const Win_Resize_Min As String = "Win_Resize_Min"

    ' - 윈도우 리사이즈 기본
    Private Const Win_Resize_Normal As String = "Win_Resize_Normal"

    ' - 윈도우 리사이즈 풀스크린
    Private Const Win_Resize_FullScreen As String = "Win_Resize_FullScreen"

    ' - 윈도우 리사이즈
    Private Const Win_Resize As String = "Win_Resize"

    ' - 윈도우 열기
    Private Const Win_Open As String = "Win_Open"


    ' :: Js 호출 보냄
    Private Sub p_Js_Call(ByVal funcName As String, ByVal args As Object())
        Try
            Me.WebBrowser1.Document.InvokeScript(funcName, args)
        Catch ex As Exception

        End Try
    End Sub

    ' :: Js 호출 받음 노멀
    Public Sub Js_CallBack_n(ByVal type As String)
        Me.Js_CallBack(type, Nothing)
    End Sub

    ' :: Js 호출 받음
    Public Sub Js_CallBack(ByVal type As String, ByVal args As Object())
        '
        Select Case type
            '
            Case Win_Init
                '

            Case Win_Set_Title
                Dim t_name As String = CType(args(0), String)
                Me.Text = t_name

            Case Win_Set_Visible
                Dim t_b As Boolean = CType(args(0), Boolean)
                Me.Visible = t_b

            Case Win_Set_MinSize
                Me.WindowState = FormWindowState.Normal
                Dim t_s As Size = Me.Size
                t_s.Width = CType(args(0), Integer)
                t_s.Height = CType(args(1), Integer)
                Me.MinimumSize = Me.DefaultMaximumSize
                Me.ClientSize = t_s
                Me.MinimumSize = Me.Size

            Case Win_Set_Location
                Me.WindowState = FormWindowState.Normal
                Dim t_p As Point = Me.Location
                t_p.X = CType(args(0), Integer)
                t_p.Y = CType(args(1), Integer)
                Me.Location = t_p
                '

            Case Win_Resize_Max
                Me.WindowState = FormWindowState.Maximized

            Case Win_Resize_Min
                Me.WindowState = FormWindowState.Minimized

            Case Win_Resize_Normal
                Me.WindowState = FormWindowState.Normal

            Case Win_Resize_FullScreen
                Dim t_b As Boolean = CType(args(0), Boolean)
                Me.p_SetFullScreen(t_b)

            Case Win_Resize
                Me.WindowState = FormWindowState.Normal
                Dim t_s As Size = Me.Size
                t_s.Width = CType(args(0), Integer)
                t_s.Height = CType(args(1), Integer)
                Me.Size = t_s
                '

            Case Win_Open
                Dim t_path As String = CType(args(0), String)
                Process.Start(t_path)

        End Select
    End Sub

End Class



