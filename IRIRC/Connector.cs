using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ChatSharp;

namespace IRIRC
{
	class Connector
	{
		private IrcClient _client;
		private TaskCompletionSource<int> _connectTcs;

		public async Task Start(string host, string username, string password, string channelName)
		{
			await Connect(host, username, password);

			_client.Channels.Join(channelName.ToLower());

			_client.ChannelMessageRecieved += OnChannelMessageRecieved;
		}
		
		private Task Connect(string host, string username, string password)
		{
			_client = new IrcClient(host, new IrcUser(username.ToLower(), username, password));
			_client.ConnectionComplete += OnConnectionComplete;
			_connectTcs = new TaskCompletionSource<int>();
			
			_client.ConnectAsync();

			return _connectTcs.Task;
		}

		void OnConnectionComplete(object sender, EventArgs e)
		{
			_connectTcs.SetResult(0);
		}

		void OnChannelMessageRecieved(object sender, ChatSharp.Events.PrivateMessageEventArgs e)
		{
			MessageBox.Show("<" + e.PrivateMessage.User.Nick + "> " + e.PrivateMessage.Message);
		}
	}
}
