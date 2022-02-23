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
using System.Collections.ObjectModel;
using System.Diagnostics;

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

		// ListBox アイテム
		public ObservableCollection<String> ListBoxItems { get; set; } = new();

		// ステータスバーに表示するメッセージ
		private String _statusBarMessage = String.Empty;
		public String StatusBarMessage
		{
			get => _statusBarMessage;
			set => RaisePropertyChangedIfSet(ref _statusBarMessage, value);
		}

		// 動的に状態を制御するコンテキストメニューのヘッダー
		private String _menuItemHeaderDynamicState = String.Empty;
		public String MenuItemHeaderDynamicState
		{
			get => _menuItemHeaderDynamicState;
			set => RaisePropertyChangedIfSet(ref _menuItemHeaderDynamicState, value);
		}

		// 動的に状態を制御するコンテキストメニューの有効状態
		private Boolean _menuItemEnabledDynamicState;
		public Boolean MenuItemEnabledDynamicState
		{
			get => _menuItemEnabledDynamicState;
			private set => RaisePropertyChangedIfSet(ref _menuItemEnabledDynamicState, value);
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
			// ListBox アイテム
			ListBoxItems.Add("ListBox のアイテムのみ右クリックでオープンするコンテキストメニュー（アイテムではない部分ではオープンしない）");
		}

		// --------------------------------------------------------------------
		// 動的に状態を制御するコンテキストメニューの準備
		// --------------------------------------------------------------------
		public void PrepareDynamicStateContextMenu()
		{
			MenuItemHeaderDynamicState = "時刻 " + DateTime.Now.ToString("HH:mm:ss") + " に生成";
			MenuItemEnabledDynamicState = !MenuItemEnabledDynamicState;
		}
	}
}
