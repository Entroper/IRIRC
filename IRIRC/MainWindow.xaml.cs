using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IRIRC
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Connector _connector = new Connector();

		public MainWindow()
		{
			InitializeComponent();
		}

		private void sameAsUsernameCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			channelTextBox.Text = "#" + usernameTextBox.Text;
			channelTextBox.IsEnabled = false;
		}

		private void sameAsUsernameCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			channelTextBox.IsEnabled = true;
		}

		private void usernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (sameAsUsernameCheckBox.IsChecked.HasValue && sameAsUsernameCheckBox.IsChecked.Value)
				channelTextBox.Text = "#" + usernameTextBox.Text;
		}

		private async void connectButton_Click(object sender, RoutedEventArgs e)
		{
			await _connector.Start(ircServerTextBox.Text, usernameTextBox.Text, passwordBox.Password, channelTextBox.Text);
		}
	}
}
