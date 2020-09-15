using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF_showcase
{
	/// <summary>
	/// Logika interakcji dla klasy AddEntry.xaml
	/// </summary>
	public partial class AddEntry : Window
	{
		public AddEntry()
		{
			InitializeComponent();
		}

		DateTime GetTimeFromTextBox(TextBox tb) {
			string time = tb.Text;
			DateTime dateTime = DateTime.ParseExact(time,"HH:mm",CultureInfo.InvariantCulture);
            return dateTime;
		}

        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
			TextBox tb = null;
			if (((StackPanel)((RepeatButton)sender).Parent).Name.Equals("entryStackPanel")) {
				tb = entryTextBox;
			} else if (((StackPanel)((RepeatButton)sender).Parent).Name.Equals("alarmStackPanel")) {
				tb = alarmTextBox;
			}
			DateTime dateTime = GetTimeFromTextBox(tb);
			if (dateTime.Hour == 23 && dateTime.Minute == 45)
			{

			}
			else dateTime = dateTime.AddMinutes(15);
			tb.Text = dateTime.ToString("HH:mm");
        }

		private void DownButton_Click(object sender, RoutedEventArgs e)
		{
			TextBox tb = null;
			if (((StackPanel)((RepeatButton)sender).Parent).Name.Equals("entryStackPanel"))
			{
				tb = entryTextBox;
			}
			else if (((StackPanel)((RepeatButton)sender).Parent).Name.Equals("alarmStackPanel"))
			{
				tb = alarmTextBox;
			}
			DateTime dateTime = GetTimeFromTextBox(tb);
			if (dateTime.Hour == 0 && dateTime.Minute == 0)
			{

			}
			else dateTime = dateTime.AddMinutes(-15);
			tb.Text = dateTime.ToString("HH:mm");
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				DateTime entryDateTime = (DateTime)entryDatePicker.SelectedDate;
				DateTime entryTime = GetTimeFromTextBox(entryTextBox);
				entryDateTime = entryDateTime.AddHours(entryTime.Hour).AddMinutes(entryTime.Minute);
				DateTime alarmDateTime = (DateTime)alarmDatePicker.SelectedDate;
				DateTime alarmTime = GetTimeFromTextBox(alarmTextBox);
				alarmDateTime = alarmDateTime.AddHours(alarmTime.Hour).AddMinutes(alarmTime.Minute);
				using (var db = new DBModel())
				{
					db.Entries.Add(
						new Entry
						{
							EntryDate = entryDateTime,
							EntryContent = entryContent.Text,
							Alarm = new Alarm
							{
								AlarmDate = alarmDateTime,
								Enabled = true
							}
						});
					db.SaveChanges();
				}
			}
			catch (InvalidOperationException)
			{
				MessageBox.Show("Nie wybrano daty :(");
			}
			catch (ProviderIncompatibleException)
			{
				MessageBox.Show("Nie można połączyć się z serwerem :(");
			}
			catch (Exception ex) {
				MessageBox.Show(ex.StackTrace);
			}
			//Close();
		}
	}
}
