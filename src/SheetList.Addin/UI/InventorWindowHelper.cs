using System;
using System.Runtime.InteropServices;
using System.Windows;
using Inventor;
using Point = System.Windows.Point;

namespace SheetList.UI
{
	public static class InventorWindowHelper
	{
		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		public static Point GetInventorWindowPosition(Inventor.Application inventor)
		{
			IntPtr hwnd = new IntPtr(inventor.MainFrameHWND);
			if (GetWindowRect(hwnd, out RECT rect))
			{
				return new Point(rect.Left, rect.Top);
			}
			throw new InvalidOperationException("Unable to get Inventor window position.");
		}
	}
}