#include "stdafx.h"
#include "commdlg.h"
#include "FlappyBird_WinApi.h"


#define MAX_LOADSTRING 100
#define ID_TIMER 1001
#define ID_NPCTIMER 1002
#define win_width 600
#define win_height 400
#define ball_diameter 16
#define obstacle_width 20
#define ID_CLOSE 2000
#define ID_EXIT 2001
#define IDM_COLOR 2002
#define OBSTACLESCOUNT 4
#define obstacleYGap 60
#define obstacleXGap 120
#define initObstacleX 200

const int ScreenX = (GetSystemMetrics(SM_CXSCREEN) - win_width) / 2; //defines positions such that window will be in the middle
const int ScreenY = (GetSystemMetrics(SM_CYSCREEN) - win_height) / 2;
int ballY = 0;
int ballYSpeed = 4;


int score = 0;
int x = 0;

// Global Variables:
HINSTANCE hInst;								// current instance
TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
HWND mainWindow;
HWND ballWindow;
HWND obstacleWindows[OBSTACLESCOUNT*2];
int obstacleHeights[OBSTACLESCOUNT];
int obstacleXPos[OBSTACLESCOUNT*2];

// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
ATOM				MyRegisterClassball(HINSTANCE hInstance);
ATOM				MyRegisterClassObstacle(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
//LRESULT CALLBACK	ballProc(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(_In_ HINSTANCE hInstance,
	_In_opt_ HINSTANCE hPrevInstance,
	_In_ LPTSTR    lpCmdLine,
	_In_ int       nCmdShow)
{
	UNREFERENCED_PARAMETER(hPrevInstance);
	UNREFERENCED_PARAMETER(lpCmdLine);

	// TODO: Place code here.
	MSG msg;
	HACCEL hAccelTable;

	// Initialize global strings
	LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
	LoadString(hInstance, IDC_FLAPPYBIRDWINAPI, szWindowClass, MAX_LOADSTRING);
	MyRegisterClass(hInstance);
	MyRegisterClassball(hInstance);
	MyRegisterClassObstacle(hInstance);
	// Perform application initialization:
	if (!InitInstance(hInstance, nCmdShow))
	{
		return FALSE;
	}

	hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_FLAPPYBIRDWINAPI));

	// Main message loop:
	while (GetMessage(&msg, NULL, 0, 0))
	{
		if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
	}

	return (int)msg.wParam;
}

//
//  FUNCTION: MyRegisterClass()
//
//  PURPOSE: Registers the window class.
//
ATOM MyRegisterClass(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FLAPPYBIRDWINAPI));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(CreateSolidBrush(RGB(0, 255, 255)));
	wcex.lpszMenuName = MAKEINTRESOURCE(IDC_FLAPPYBIRDWINAPI);
	wcex.lpszClassName = szWindowClass;
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

ATOM MyRegisterClassball(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FLAPPYBIRDWINAPI));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(CreateSolidBrush(RGB(255, 0, 0)));
	wcex.lpszMenuName = MAKEINTRESOURCE(IDC_FLAPPYBIRDWINAPI);
	wcex.lpszClassName = L"ballclass";
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

ATOM MyRegisterClassObstacle(HINSTANCE hInstance)
{
	WNDCLASSEX wcex;

	wcex.cbSize = sizeof(WNDCLASSEX);

	wcex.style = CS_HREDRAW | CS_VREDRAW;
	wcex.lpfnWndProc = WndProc;
	wcex.cbClsExtra = 0;
	wcex.cbWndExtra = 0;
	wcex.hInstance = hInstance;
	wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_FLAPPYBIRDWINAPI));
	wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
	wcex.hbrBackground = (HBRUSH)(CreateSolidBrush(RGB(0, 0, 255)));
	wcex.lpszMenuName = MAKEINTRESOURCE(IDC_FLAPPYBIRDWINAPI);
	wcex.lpszClassName = L"obstacleClass";
	wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

	return RegisterClassEx(&wcex);
}

BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
{
	hInst = hInstance; // Store instance handle in our global variable

	mainWindow = CreateWindow(
		szWindowClass,
		L"Flappy Bird",
		WS_OVERLAPPED | WS_MINIMIZEBOX | WS_SYSMENU,
		ScreenX,
		ScreenY,
		win_width,
		win_height,
		NULL,
		NULL,
		hInstance,
		NULL
	);
	if (!mainWindow)
	{
		return FALSE;
	}

	SetMenu(mainWindow, NULL);
	// Set WS_EX_LAYERED on this window 
	SetWindowLong(mainWindow, GWL_EXSTYLE,
		GetWindowLong(mainWindow, GWL_EXSTYLE) | WS_EX_LAYERED);
	SetLayeredWindowAttributes(mainWindow, 0, (255 * 80) / 100, LWA_ALPHA);

	// Show the window
	ShowWindow(mainWindow, nCmdShow);
	UpdateWindow(mainWindow);


	static int x = win_width / 2;
	static int y = win_height / 2;

	//create a ball instance
	ballWindow = CreateWindow(
		L"ballclass",
		szTitle,
		WS_VISIBLE | WS_CHILD,
		80,
		160,
		ball_diameter,
		ball_diameter,
		mainWindow,
		NULL,
		GetModuleHandle(NULL),
		NULL
	);
	//make window round
	SetWindowRgn(ballWindow, (HRGN)CreateRoundRectRgn(0, 0, ball_diameter, ball_diameter, ball_diameter, ball_diameter), TRUE);
	//SetWindowLong(ballWindow, GWL_STYLE, 0);
	SetMenu(ballWindow, NULL);
	ShowWindow(ballWindow, nCmdShow);
	UpdateWindow(ballWindow);

	int j = 0;
	srand(time(NULL));


	for (int i = 0, j = 0; i < OBSTACLESCOUNT*2; i+=2, j++) {
		int obstacleHeight = rand() % (320);
		obstacleHeights[j] = obstacleHeight;
		obstacleXPos[j] = initObstacleX + obstacleXGap * j;
		obstacleWindows[i] = CreateWindow(L"obstacleClass", szTitle, WS_VISIBLE | WS_CHILD,
			obstacleXPos[j], 0, obstacle_width, obstacleHeight, mainWindow, NULL, GetModuleHandle(NULL), NULL);
		obstacleWindows[i+1] = CreateWindow(L"obstacleClass", szTitle, WS_VISIBLE | WS_CHILD,
			obstacleXPos[j], obstacleHeight + obstacleYGap, obstacle_width, 1000, mainWindow, NULL, GetModuleHandle(NULL), NULL);

		ShowWindow(obstacleWindows[i], nCmdShow);
		UpdateWindow(obstacleWindows[i]);

		ShowWindow(obstacleWindows[i+1], nCmdShow);
		UpdateWindow(obstacleWindows[i+1]);
	}

	return TRUE;
}

void GetTextInfoForMouseMsg(WPARAM wParam, LPARAM lParam, TCHAR *msgName,
	TCHAR *buf, int bufSize)
{
	short x = (short)LOWORD(lParam);
	short y = (short)HIWORD(lParam);
	_stprintf_s(buf, bufSize, _T("%s x: %d, y: %d, vk:"), msgName, x, y);
	if ((wParam & MK_LBUTTON) != 0) {
		_tcscat_s(buf, bufSize, _T(" LEFT"));
	}
	if ((wParam & MK_MBUTTON) != 0) {
		_tcscat_s(buf, bufSize, _T(" MIDDLE"));
	}
	if ((wParam & MK_RBUTTON) != 0) {
		_tcscat_s(buf, bufSize, _T(" RIGHT"));
	}
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{

	const int bufSize = 256;
	TCHAR buf[bufSize];


	int wmId;

	HBRUSH brush;


	switch (message)
	{
	case WM_CREATE:
		SetTimer(mainWindow, 7, 50, NULL);
		break;
	case WM_COMMAND:
	{
		wmId = LOWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{
		case IDM_EXIT:
			DestroyWindow(hWnd);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
	}
	break;
		
	case WM_PAINT:
	{

		PAINTSTRUCT ps;
		HDC hdc;
		//for (int i = 0; i < OBSTACLESCOUNT; i++)
		//{
		//	hdc = BeginPaint(obstacleLowerWindows[i], &ps);
		//	EndPaint(obstacleLowerWindows[i], &ps);
		//}

		//for (int i = 0; i < OBSTACLESCOUNT*2; i++)
		//{
		//	//hdc = BeginPaint(obstacleUpperWindows[i], &ps);
		//	//EndPaint(obstacleUpperWindows[i], &ps);
		//}

		hdc = BeginPaint(ballWindow, &ps);
		EndPaint(ballWindow, &ps);

		hdc = BeginPaint(hWnd, &ps);
		EndPaint(hWnd, &ps);
	}
	break;

	case WM_LBUTTONDOWN:
	{
		GetTextInfoForMouseMsg(wParam, lParam, (TCHAR*)_T("LBUTTONDOWN"), buf, bufSize);
		//ballYSpeed *= -1;
		ballY -= 35;

	}
	case WM_LBUTTONUP:
	{
		GetTextInfoForMouseMsg(wParam, lParam, (TCHAR*)_T("LBUTTONUP"), buf, bufSize);
	}
	break;
	case WM_TIMER:
	{
		RECT rc_ball, rc_paddleTop, rc_paddleBottom;
		GetWindowRect(ballWindow, &rc_ball);

		ballY += ballYSpeed;
		MoveWindow(ballWindow, 100, ballY, 20, 20, TRUE);

		//when a ball is out of a client area
		//if (ballY < 0 || ballY > 340)
		//{
		//	ballY = 170;
		//	for (int i = 0, n = 0; i < N / 2; i += 2, n++)
		//	{
		//		heights[n] = rand() % 130;
		//		paddleX[i] = initialX[i];
		//		paddleY[i] = 0;
		//		paddleX[i + 1] = initialX[i + 1];
		//		paddleY[i + 1] = heights[n] + 80;
		//		score = 0;
		//	}
		//}

		//when a paddle is out of a client area
		for (int i = 0, n = 0; i < OBSTACLESCOUNT*2; i += 2, n++)
		{
			//if (paddleX[i] <= 0)
			//{
			//	heights[n] = rand() % 130;
			//	paddleX[i] = 685;
			//	paddleY[i] = 0;
			//	paddleX[i + 1] = 685;
			//	paddleY[i + 1] = heights[n] + 80;
			//}

			//GetWindowRect(paddle[i], &rc_paddleTop);
			//GetWindowRect(paddle[i + 1], &rc_paddleBottom);

			////when the ball hits a paddle
			//if (rc_paddleTop.right >= rc_ball.left && rc_paddleTop.left <= rc_ball.right)
			//{
			//	if (x != n && n < 5)
			//	{
			//		x = n;

			//		score++;
			//	}

			//	if (rc_ball.top <= rc_paddleTop.bottom || rc_ball.bottom >= rc_paddleBottom.top)
			//	{
			//		ballY = 170;
			//		for (int i = 0, n = 0; i < N / 2; i += 2, n++)
			//		{
			//			heights[n] = rand() % 130;
			//			paddleX[i] = initialX[i];
			//			paddleY[i] = 0;
			//			paddleX[i + 1] = initialX[i + 1];
			//			paddleY[i + 1] = heights[n] + 80;
			//			score = 0;
			//		}
			//	}
			//}

			MoveWindow(obstacleWindows[i], obstacleXPos[i] - 50, 0, obstacle_width, obstacleHeights[n], TRUE);
			obstacleXPos[i] = obstacleXPos[i] - 5;

			MoveWindow(obstacleWindows[i + 1], obstacleXPos[i + 1] - 50, obstacleHeights[n] + obstacleYGap, obstacle_width, 1000, TRUE);
			obstacleXPos[i + 1] = obstacleXPos[i + 1] - 5;

		TCHAR s[256];
		_stprintf_s(s, 256, _T("Flappy Bird     Score:  %d"), score);
		SetWindowText(mainWindow, s);
	}




	}
	break;
	case WM_DESTROY:
		KillTimer(hWnd, ID_TIMER);
		PostQuitMessage(0);
		break;
	default:
		return DefWindowProc(hWnd, message, wParam, lParam);
	}
	return 0;
}