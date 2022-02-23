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
using System.Windows;
using System.Windows.Controls;

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
		private Boolean _isDynamicStateMenuItemEnabled;
		public Boolean IsDynamicStateMenuItemEnabled
		{
			get => _isDynamicStateMenuItemEnabled;
			private set => RaisePropertyChangedIfSet(ref _isDynamicStateMenuItemEnabled, value);
		}

		// 動的に状態を制御するコンテキストメニューの可視状態
		private Visibility _dynamicStateMenuItemVisibility = Visibility.Collapsed;
		public Visibility DynamicStateMenuItemVisibility
		{
			get => _dynamicStateMenuItemVisibility;
			set => RaisePropertyChangedIfSet(ref _dynamicStateMenuItemVisibility, value);
		}

		// 動的にアイテム数を制御するコンテキストメニューのアイテム
		public ObservableCollection<Control> DynamicMenuItems { get; set; } = new();

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
			SetStatusBarMessage(parameter);
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
		// 動的にアイテム数を制御するコンテキストメニューの準備
		// --------------------------------------------------------------------
		public void PrepareDynamicItemsContextMenu()
		{
			DynamicMenuItems.Clear();

			// _numDynamicItems 個生成
			_numDynamicItems++;
			if (_numDynamicItems > 5)
			{
				_numDynamicItems = 1;
			}
			for (Int32 i = 0; i < _numDynamicItems; i++)
			{
				AddMenuItem("動的生成アイテム " + (i + 1).ToString());
			}

			// セパレーター
			DynamicMenuItems.Add(new Separator());

			// 固定アイテム
			AddMenuItem("最下段の項目");
		}

		// --------------------------------------------------------------------
		// 動的に状態を制御するコンテキストメニューの準備
		// --------------------------------------------------------------------
		public void PrepareDynamicStateContextMenu()
		{
			MenuItemHeaderDynamicState = "時刻 " + DateTime.Now.ToString("HH:mm:ss") + " に生成";
			IsDynamicStateMenuItemEnabled = !IsDynamicStateMenuItemEnabled;
			DynamicStateMenuItemVisibility = DynamicStateMenuItemVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
		}

		// ====================================================================
		// private 変数
		// ====================================================================

		// 動的なアイテム数
		private Int32 _numDynamicItems = 1;

		// ====================================================================
		// private 関数
		// ====================================================================

		// --------------------------------------------------------------------
		// ButtonSelectOpEd のコンテキストメニューにアイテムを追加
		// --------------------------------------------------------------------
		private void AddMenuItem(String label)
		{
			MenuItem menuItem = new();
			menuItem.Header = label;
			menuItem.Click += DynamicMenuItem_Click;
			DynamicMenuItems.Add(menuItem);
		}

		// --------------------------------------------------------------------
		// イベントハンドラー
		// --------------------------------------------------------------------
		private void DynamicMenuItem_Click(Object sender, RoutedEventArgs routedEventArgs)
		{
			SetStatusBarMessage(((MenuItem)sender).Header.ToString());
		}

		// --------------------------------------------------------------------
		// ステータスバーにメッセージを表示
		// --------------------------------------------------------------------
		private void SetStatusBarMessage(String? message)
		{
			StatusBarMessage = DateTime.Now.ToString("HH:mm:ss") + "　" + message;
		}
	}
}
