#include "stdafx.h"
#include "commdlg.h"
#include "FlappyBird_WinApi.h"


#define MAX_LOADSTRING 100
#define ID_TIMER 1001
#define ID_NPCTIMER 1002
#define win_width 600
#define win_height 400
#define ball_diameter 16
#define obstacle_width 21
#define OBSTACLESCOUNT 4
#define obstacleYGap 80
#define obstacleXGap 172
#define initObstacleX 200

const int ScreenX = (GetSystemMetrics(SM_CXSCREEN) - win_width) / 2; //defines positions such that window will be in the middle
const int ScreenY = (GetSystemMetrics(SM_CYSCREEN) - win_height) / 2;
double ballYPos = 170;
double ballYSpeed = 1;
const double ballGravity = 1.02;

int score = 0;
int x = 0;

// Global Variables:
HINSTANCE hInst;
TCHAR szTitle[MAX_LOADSTRING];	
TCHAR szWindowClass[MAX_LOADSTRING];			
HWND mainWindow;
HWND ballWindow;
HWND obstacleWindows[OBSTACLESCOUNT*2];
TCHAR szFile[260];
HBITMAP hb;
OPENFILENAME op;
HMENU hPopupMenu;

//arrays for obstacles

int obstacleHeights[OBSTACLESCOUNT];
int obstacleXPos[OBSTACLESCOUNT * 2];
int obstacleYPos[OBSTACLESCOUNT * 2];

// Forward declarations of functions included in this code module:
ATOM				MyRegisterClass(HINSTANCE hInstance);
ATOM				MyRegisterClassball(HINSTANCE hInstance);
ATOM				MyRegisterClassObstacle(HINSTANCE hInstance);
BOOL				InitInstance(HINSTANCE, int);
LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);

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
	SetWindowLong(mainWindow, GWL_EXSTYLE,
		GetWindowLong(mainWindow, GWL_EXSTYLE) | WS_EX_LAYERED);
	SetLayeredWindowAttributes(mainWindow, 0, (255 * 80) / 100, LWA_ALPHA); //80% transparency

	// Show the mainWindow
	ShowWindow(mainWindow, nCmdShow);
	UpdateWindow(mainWindow);

	// Create a ball instance
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
	//make ball round
	SetWindowRgn(ballWindow, (HRGN)CreateRoundRectRgn(0, 0, ball_diameter, ball_diameter, ball_diameter, ball_diameter), TRUE);
	SetMenu(ballWindow, NULL);
	ShowWindow(ballWindow, nCmdShow);
	UpdateWindow(ballWindow);

	srand(time(NULL));


	for (int i = 0, j = 0; i < OBSTACLESCOUNT*2; i+=2, j++) {
		int obstacleHeight = rand() % (280);
		obstacleHeights[j] = obstacleHeight;
		obstacleXPos[i] = initObstacleX + obstacleXGap * j;
		obstacleXPos[i+1] = initObstacleX + obstacleXGap * j;
		obstacleYPos[i] = 0;
		obstacleWindows[i] = CreateWindow(L"obstacleClass", szTitle, WS_VISIBLE | WS_CHILD,
			obstacleXPos[i], 0, obstacle_width, obstacleHeight, mainWindow, NULL, GetModuleHandle(NULL), NULL);
		
		obstacleYPos[i + 1] = obstacleHeight + obstacleYGap;
		obstacleWindows[i+1] = CreateWindow(L"obstacleClass", szTitle, WS_VISIBLE | WS_CHILD,
			obstacleXPos[i+1], obstacleYPos[i + 1], obstacle_width, 1000, mainWindow, NULL, GetModuleHandle(NULL), NULL);	
	}

	hPopupMenu = CreatePopupMenu();
	InsertMenu(hPopupMenu, 0, MF_BYPOSITION | MF_STRING, IDM_COLOR, L"Choose Color\tAlt+C");
	InsertMenu(hPopupMenu, 1, MF_BYPOSITION | MF_STRING, IDM_NEWGAME, L"New Game\tAlt+N");
	InsertMenu(hPopupMenu, 2, MF_BYPOSITION | MF_STRING, IDM_BITMAP, L"Bitmap...\tAlt+B");
	InsertMenu(hPopupMenu, 3, MF_BYPOSITION | MF_STRING, IDM_TILES, L"Tiles...\tAlt+T");
	InsertMenu(hPopupMenu, 4, MF_BYPOSITION | MF_STRING, IDM_STRETCH, L"Stretched...\tAlt+S");
	EnableMenuItem(hPopupMenu, IDM_TILES, MF_GRAYED);
	EnableMenuItem(hPopupMenu, IDM_STRETCH, MF_GRAYED);


	UpdateWindow(mainWindow);
	return TRUE;
}

//handling mouse msges
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

void newGame() {
	ballYPos = 170;
	ballYSpeed = 1;
	score = 0;
	for (int i = 0; i < OBSTACLESCOUNT; i++) {
		obstacleHeights[i] = rand() % 280;
	}

	for (int i = 0; i < OBSTACLESCOUNT * 2; i += 2)
	{
		obstacleXPos[i] = initObstacleX + obstacleXGap * i / 2;
		obstacleXPos[i + 1] = initObstacleX + obstacleXGap * i / 2;
		obstacleYPos[i] = 0;
		obstacleYPos[i + 1] = obstacleHeights[i / 2] + obstacleYGap;
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
		SetTimer(mainWindow, 7, 10, NULL);
	break;
	case WM_COMMAND:
	{
		wmId = LOWORD(wParam);
		// Parse the menu selections:
		switch (wmId)
		{

		case IDM_COLOR:
		{
			CHOOSECOLOR newColor;                 // common dialog box structure 
			static COLORREF acrCustClr[16]; // array of custom colors 
			HWND hwnd = mainWindow;                      // owner window
			HBRUSH hbrush;                  // brush handle
			static DWORD rgbCurrent;        // initial color selection
											// Initialize CHOOSECOLOR 
			ZeroMemory(&newColor, sizeof(newColor));
			newColor.lStructSize = sizeof(newColor);
			newColor.hwndOwner = hwnd;
			newColor.lpCustColors = (LPDWORD)acrCustClr;
			newColor.rgbResult = rgbCurrent;
			newColor.Flags = CC_FULLOPEN | CC_RGBINIT;

			if (ChooseColor(&newColor) == TRUE)
			{
				hbrush = CreateSolidBrush(newColor.rgbResult);
				rgbCurrent = newColor.rgbResult;
				SetClassLongPtr(hwnd, -10, (LONG)hbrush);
				InvalidateRect(hwnd, NULL, TRUE);
				UpdateWindow(hwnd);
				SetClassLongPtr(ballWindow, -10, (LONG)(CreateSolidBrush(RGB(255, 0, 0))));
				InvalidateRect(ballWindow, NULL, TRUE);
				UpdateWindow(ballWindow);
			}
		}
		break;
		case IDM_BITMAP:
		{
			EnableMenuItem(hPopupMenu, IDM_TILES, MF_ENABLED);
			EnableMenuItem(hPopupMenu, IDM_STRETCH, MF_ENABLED);
			CheckMenuItem(hPopupMenu, IDM_STRETCH, MF_UNCHECKED);
			CheckMenuItem(hPopupMenu, IDM_TILES, MF_CHECKED);

			ZeroMemory(&op, sizeof(op));
			op.lStructSize = sizeof(op);
			op.hwndOwner = hWnd;
			op.lpstrFile = szFile;
			op.lpstrFile[0] = '\0';
			op.nMaxFile = sizeof(szFile);
			op.nFilterIndex = 1;
			op.lpstrFileTitle = NULL;
			op.nMaxFileTitle = 0;
			op.lpstrInitialDir = NULL;
			op.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;
			op.lpstrFilter = _T("Bitmaps\0*.BMP\0");

			if (GetOpenFileName(&op)) {
				hb = (HBITMAP)LoadImage(NULL, op.lpstrFile, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
				SetClassLongPtr(mainWindow, GCLP_HBRBACKGROUND, (LONG)CreatePatternBrush(hb));
				InvalidateRect(mainWindow, NULL, true);
				UpdateWindow(mainWindow);
				DeleteObject(hb);
				SetClassLongPtr(ballWindow, -10, (LONG)(CreateSolidBrush(RGB(255, 0, 0))));
				InvalidateRect(ballWindow, NULL, TRUE);
				UpdateWindow(ballWindow);
			}
		}
		break;
		case IDM_TILES:
		{
			CheckMenuItem(hPopupMenu, IDM_STRETCH, MF_UNCHECKED);
			CheckMenuItem(hPopupMenu, IDM_TILES, MF_CHECKED);
			hb = (HBITMAP)LoadImage(NULL, op.lpstrFile, IMAGE_BITMAP, 0, 0, LR_LOADFROMFILE);
			//Tile
			SetClassLongPtr(mainWindow, GCLP_HBRBACKGROUND, (LONG)CreatePatternBrush(hb));
			InvalidateRect(mainWindow, NULL, true);
			UpdateWindow(mainWindow);
			SetClassLongPtr(ballWindow, -10, (LONG)(CreateSolidBrush(RGB(255, 0, 0))));
			InvalidateRect(ballWindow, NULL, TRUE);
			UpdateWindow(ballWindow);
			DeleteObject(hb);
		}
		break;
		case IDM_STRETCH:
		{
			CheckMenuItem(hPopupMenu, IDM_STRETCH, MF_CHECKED);
			CheckMenuItem(hPopupMenu, IDM_TILES, MF_UNCHECKED);

			hb = (HBITMAP)LoadImage(NULL, op.lpstrFile, IMAGE_BITMAP, win_width, win_height, LR_LOADFROMFILE);
			SetClassLongPtr(mainWindow, GCLP_HBRBACKGROUND, (LONG)CreatePatternBrush(hb));
			InvalidateRect(mainWindow, NULL, true);
			UpdateWindow(mainWindow);
			DeleteObject(hb);
			SetClassLongPtr(ballWindow, -10, (LONG)(CreateSolidBrush(RGB(255, 0, 0))));
			InvalidateRect(ballWindow, NULL, TRUE);
			UpdateWindow(ballWindow);
		}
		break;
		case IDM_NEWGAME:
		{
			newGame();
		}
		break;
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

		hdc = BeginPaint(ballWindow, &ps);
		EndPaint(ballWindow, &ps);

		hdc = BeginPaint(hWnd, &ps);
		EndPaint(hWnd, &ps);
	}
	break;

	case WM_LBUTTONDOWN:
	{
		GetTextInfoForMouseMsg(wParam, lParam, (TCHAR*)_T("LBUTTONDOWN"), buf, bufSize);
		//jump 30 pixels up, and set the speed to initial
		ballYPos -= 30;
		ballYSpeed = 1;

	}
	case WM_LBUTTONUP:
	{
		GetTextInfoForMouseMsg(wParam, lParam, (TCHAR*)_T("LBUTTONUP"), buf, bufSize);
	}
	break;
	case WM_RBUTTONDOWN:
	{
		//point p points to current mouse coordinates
		POINT p;
		if (GetCursorPos(&p))
		{
			SetForegroundWindow(hWnd);
			TrackPopupMenu(hPopupMenu, TPM_BOTTOMALIGN | TPM_LEFTALIGN, p.x, p.y, 0, hWnd, NULL);
		}
	}
	case WM_TIMER:
	{
		RECT rc_ball, rc_upperObstacle, rc_lowerObstacle;
		GetWindowRect(ballWindow, &rc_ball);
		
		ballYSpeed *= ballGravity;
		ballYPos += ballYSpeed;
		MoveWindow(ballWindow, 100, ballYPos, 20, 20, TRUE);

		// ball out of playing zone
		if (ballYPos < 0 || ballYPos > 370)
		{
			newGame();
		}

		//obstacle disappears on the left
		for (int i = 0, n = 0; i < OBSTACLESCOUNT*2; i += 2, n++)
		{
			if (obstacleXPos[i] <= 0)
			{
				obstacleHeights[n] = rand() % 280;
				obstacleXPos[i] = 685;
				obstacleYPos[i] = 0;
				obstacleXPos[i + 1] = 685;
				obstacleYPos[i + 1] = obstacleHeights[n] + obstacleYGap;
			}

			GetWindowRect(obstacleWindows[i], &rc_upperObstacle);
			GetWindowRect(obstacleWindows[i + 1], &rc_lowerObstacle);

			//ball hits an obstacle
			if (rc_upperObstacle.right >= rc_ball.left && rc_upperObstacle.left <= rc_ball.right)
			{
				if (x != n && n < 5)
				{
					x = n;
					score++;
				}

				if (rc_ball.top <= rc_upperObstacle.bottom || rc_ball.bottom >= rc_lowerObstacle.top)
				{
					newGame();
				}
			}

			MoveWindow(obstacleWindows[i], obstacleXPos[i], obstacleYPos[i], obstacle_width, obstacleHeights[n], TRUE);
			obstacleXPos[i] = obstacleXPos[i] - 1;

			MoveWindow(obstacleWindows[i + 1], obstacleXPos[i + 1], obstacleYPos[i+1], obstacle_width, 1000, TRUE);//1000 makes it definitely higher than win_height
			obstacleXPos[i + 1] = obstacleXPos[i + 1] - 1;

		TCHAR s[256];
		_stprintf_s(s, 256, _T("Flappy Bird           Score:  %d"), score);
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

