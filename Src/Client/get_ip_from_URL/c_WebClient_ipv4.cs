using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace get_ip_from_URL
{
	internal class c_HttpClient_ipv4
	{
		private readonly HttpClient	m_httpClient;

		internal c_HttpClient_ipv4()
		{
			var handler = new SocketsHttpHandler
			{
				ConnectCallback = async (context, cancellationToken) =>
				{
					IPAddress[] addresses = await Dns.GetHostAddressesAsync(context.DnsEndPoint.Host, cancellationToken);
					IPAddress? ipv4Address = Array.Find(addresses, ip => ip.AddressFamily == AddressFamily.InterNetwork);
					if(ipv4Address == null)
						throw new InvalidOperationException("No IPv4 address available");

					Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					await socket.ConnectAsync(new IPEndPoint(ipv4Address, context.DnsEndPoint.Port), cancellationToken);
					return new NetworkStream(socket, ownsSocket: true);
				}
			};

			m_httpClient = new(handler, disposeHandler: true);
		}

		internal async Task<string> GetStringAsync(string url, CancellationToken cancellationToken = default)
		{
			return await m_httpClient.GetStringAsync(url, cancellationToken);
		}
	}
}	// namespace get_ip_from_URL
