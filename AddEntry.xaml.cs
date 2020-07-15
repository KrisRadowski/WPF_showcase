using System;
using System.Collections.Generic;
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
			if (((StackPanel)((RepeatButton)sender).Parent).Name.Equals("timeStackPanel")) {
				tb = timeTextBox;
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
			if (((StackPanel)((RepeatButton)sender).Parent).Name.Equals("timeStackPanel"))
			{
				tb = timeTextBox;
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
			tbText = dateTime.ToString("HH:mm");
		}
	}
}
