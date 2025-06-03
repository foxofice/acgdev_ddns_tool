using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace get_ip_from_URL
{
	internal class c_WebClient_ipv4 : WebClient
	{
		protected override WebRequest GetWebRequest(Uri address)
		{
			HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
			request.ServicePoint.BindIPEndPointDelegate = new BindIPEndPoint(BindIPEndPointCallback);
			return request;
		}

		private IPEndPoint BindIPEndPointCallback(ServicePoint servicePoint, IPEndPoint remoteEndPoint, int retryCount)
		{
			if(remoteEndPoint.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				// 强制使用 IPv4
				return new IPEndPoint(IPAddress.Any, 0);
			}
			else
			{
				throw new InvalidOperationException("No IPv4 address available");
			}
		}
	};
}	// namespace get_ip_from_URL
