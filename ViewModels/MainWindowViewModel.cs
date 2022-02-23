// ============================================================================
// 
// メインウィンドウの ViewModel
// 
// ============================================================================

// ----------------------------------------------------------------------------
// 
// ----------------------------------------------------------------------------

using Livet;
using Livet.Commands;

using System;

namespace TestWpfContextMenu.ViewModels
{
	internal class MainWindowViewModel : ViewModel
	{
		// ====================================================================
		// public プロパティー
		// ====================================================================

		// --------------------------------------------------------------------
		// View 通信用のプロパティー
		// --------------------------------------------------------------------

		// ステータスバーに表示するメッセージ
		private String _statusBarMessage = String.Empty;
		public String StatusBarMessage
		{
			get => _statusBarMessage;
			set => RaisePropertyChangedIfSet(ref _statusBarMessage, value);
		}

		// --------------------------------------------------------------------
		// コマンド
		// --------------------------------------------------------------------

		#region メニューアイテムの制御
		private ListenerCommand<String>? _menuItemTestClickedCommand;

		public ListenerCommand<String> MenuItemTestClickedCommand
		{
			get
			{
				if (_menuItemTestClickedCommand == null)
				{
					_menuItemTestClickedCommand = new ListenerCommand<String>(MenuItemTestClicked);
				}
				return _menuItemTestClickedCommand;
			}
		}

		public void MenuItemTestClicked(String parameter)
		{
			StatusBarMessage = DateTime.Now.ToString("HH:mm:ss") + "　" + parameter;
		}
		#endregion

		// ====================================================================
		// public 関数
		// ====================================================================

		// --------------------------------------------------------------------
		// 初期化
		// --------------------------------------------------------------------
		public void Initialize()
		{
		}
	}
}
