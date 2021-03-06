﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.IO;
using System.Resources;

namespace WPF_showcase
{
	/// <summary>
	/// Logika interakcji dla klasy MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private DBModel _model = new DBModel();
		CollectionViewSource entryViewSource;
		public MainWindow()
		{
			InitializeComponent();
			NotifyIcon notifyIcon = new NotifyIcon();
			using (ResXResourceSet resourceSet = new ResXResourceSet(@"..\..\Properties\Resources.resx")) {
				notifyIcon.Icon = (System.Drawing.Icon)resourceSet.GetObject("Icon",true);
			}
			notifyIcon.Visible = true;
			System.Windows.Forms.ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
			System.Windows.Forms.MenuItem exitItem = new System.Windows.Forms.MenuItem();
			exitItem.Index = 0;
			exitItem.Text = "Wyjdź";
            exitItem.Click += ContextMenuExit;
			contextMenu.MenuItems.Add(exitItem);
			notifyIcon.ContextMenu = contextMenu;
		}

        private void ContextMenuExit(object sender, EventArgs e)
        {
           ((App)System.Windows.Application.Current).Shutdown();
           //System.Windows.Forms.Application.Exit();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
		{

            entryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("entryViewSource")));
			// Załaduj dane poprzez ustawienie właściwości CollectionViewSource.Source:
			// entryViewSource.Źródło = [ogólne źródło danych]
			_model.Entries.Load();
			entryViewSource.Source = _model.Entries.Local.Where(x=>(x.EntryDate>calendar.DisplayDate
			&& x.EntryDate < calendar.DisplayDate.AddDays(1)));
		}

		private void AddEntryClick(object sender, RoutedEventArgs e)
		{
			AddEntry addEntry = new AddEntry();
			addEntry.Owner = this;
			addEntry.Show();
		}

		private void DeleteEntryClick(object sender, RoutedEventArgs e)
		{
			var result = System.Windows.MessageBox.Show("Czy jesteś pewien?","WPF Showcase",MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes) {
				Entry entryToDelete = (Entry)Entries.SelectedItem;
				_model.Entries.Remove(entryToDelete);
				_model.SaveChanges();
				entryViewSource.Source = _model.Entries.Local.Where(x => (x.EntryDate > calendar.SelectedDate
				&& x.EntryDate < calendar.SelectedDate.Value.AddDays(1)));
			};
		}

        private void LoadData(object sender, SelectionChangedEventArgs e)
        {
			entryViewSource.Source = null;
			entryViewSource.Source = _model.Entries.Local.Where(x => (x.EntryDate > calendar.SelectedDate
			&& x.EntryDate < calendar.SelectedDate.Value.AddDays(1)));
		}

        private void RowClick(object sender, MouseButtonEventArgs e)
        {
			deleteButton.IsEnabled = true;
        }
    }
}
