using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
namespace Utility
{
	public class Http
	{
		private const string LineFeed = Constants.vbCr + Constants.vbLf;
		private CookieCollection Cookies;
		public mUploadData UploadData = new mUploadData();
		public struct mUploadData
		{
			private byte[] uContents;
			private string uFileName;
			private string uFieldName;
			public byte[] Contents {
				get { return uContents; }
				set { uContents = value; }
			}
			public string FileName {
				get { return uFileName; }
				set { uFileName = value; }
			}
			public string FieldName {
				get { return uFieldName; }
				set { uFieldName = value; }
			}
		}

		public event DownloadBeginEventHandler DownloadBegin;
		public delegate void DownloadBeginEventHandler(int Length);
		public event DownloadProgressEventHandler DownloadProgress;
		public delegate void DownloadProgressEventHandler(int Current);

		private string _Proxy = string.Empty;
		public string Proxy {
			get { return _Proxy; }
			set { _Proxy = value; }
		}
		private int _TimeOut = 15000;
		public int TimeOut {
			get { return _TimeOut; }
			set { _TimeOut = value; }
		}
		private string _UserAgent = "iTunes/9.0.3 (Macintosh; U; Intel Mac OS X 10_6_2; en-ca)";
		public string Useragent {
			get { return _UserAgent; }
			set { _UserAgent = value; }
		}
		private string _Referer = string.Empty;
		public string Referer {
			get { return _Referer; }
			set { _Referer = value; }
		}

		public Http()
		{
			System.Net.ServicePointManager.DefaultConnectionLimit = 150;
			System.Net.ServicePointManager.Expect100Continue = false;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
			Cookies = new CookieCollection();
		}

		public object GetResponse(string Uri, string PostData = "", List<DictionaryEntry> Headers = null)
		{
			try {
				HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Uri);
				var _with1 = Request;
				_with1.Method = "GET";
				_with1.UserAgent = Useragent;
				_with1.AllowAutoRedirect = false;
				_with1.Timeout = TimeOut;
				if (!string.IsNullOrEmpty(Referer))
					_with1.Referer = Referer;

				AddCookies(ref ref Request);

				if ((Headers != null)) {
					foreach (DictionaryEntry de in Headers) {
						try {
							_with1.Headers.Add(de.Key, de.Value);
						} catch (Exception ex) {
						}
					}
				}

				if (!string.IsNullOrEmpty(PostData)) {
					_with1.Method = "POST";
					_with1.ContentType = "application/x-www-form-urlencoded";
					byte[] byteArray = Encoding.GetEncoding(1252).GetBytes(PostData);
					_with1.ContentLength = byteArray.Length;
					Stream dataStream = _with1.GetRequestStream();
					dataStream.Write(byteArray, 0, byteArray.Length);
					dataStream.Close();
				}

				HttpWebResponse Response = (HttpWebResponse)_with1.GetResponse();
				SaveCookies(ref ref Response.Cookies, false);

				Referer = string.Empty;
				return Response;
			} catch (WebException we) {
				return we;
			} catch (Exception ex) {
				return ex;
			}
		}
		public object GetResponse(string Uri, List<DictionaryEntry> formFields, params mUploadData[] objects)
		{
			string Boundary = Guid.NewGuid().ToString().Replace("-", "");
			try {
				HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(new Uri(Uri));
				var _with2 = Request;
				_with2.UserAgent = Useragent;
				_with2.AllowAutoRedirect = false;
				_with2.Timeout = TimeOut;
				if (!string.IsNullOrEmpty(Referer))
					_with2.Referer = Referer;
				AddCookies(ref ref Request);

				_with2.Method = "POST";
				_with2.ContentType = "multipart/form-data; boundary=" + Boundary;

				MemoryStream PostData = new MemoryStream();

				StreamWriter Writer = new StreamWriter(PostData);
				var _with3 = Writer;
				if (formFields != null) {
					foreach (DictionaryEntry de in formFields) {
						_with3.Write(("--" + Boundary) + LineFeed);
						_with3.Write("Content-Disposition: form-data; name=\"{0}\"{1}{1}{2}{1}", de.Key, LineFeed, de.Value);
					}
				}
				if ((objects != null)) {
					foreach (mUploadData us in objects) {
						_with3.Write(("--" + Boundary) + LineFeed);
						_with3.Write("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", us.FieldName, us.FileName, LineFeed);
						_with3.Write(("Content-Type: application/octet-stream" + LineFeed) + LineFeed);
						_with3.Flush();
						if ((us.Contents != null))
							PostData.Write(us.Contents, 0, us.Contents.Length);
						_with3.Write(LineFeed);
					}
				}
				_with3.Write("--{0}--{1}", Boundary, LineFeed);
				_with3.Flush();

				_with2.ContentLength = PostData.Length;
				using (Stream s = _with2.GetRequestStream()) {
					PostData.WriteTo(s);
				}
				PostData.Close();

				HttpWebResponse Response = (HttpWebResponse)_with2.GetResponse();
				SaveCookies(ref ref Response.Cookies, false);

				Referer = string.Empty;
				return Response;
			} catch (WebException we) {
				return we;
			} catch (Exception ex) {
				return ex;
			}
		}

		public object GetFile(string Uri, string PostData, string Path, List<DictionaryEntry> Headers = null)
		{
			object functionReturnValue = null;
			try {
				HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(Uri);
				var _with4 = Request;

				_with4.Method = "GET";
				_with4.UserAgent = Useragent;
				_with4.AllowAutoRedirect = false;
				_with4.Timeout = TimeOut;
				if (!string.IsNullOrEmpty(Referer))
					_with4.Referer = Referer;

				AddCookies(ref ref Request);

				if ((Headers != null)) {
					foreach (DictionaryEntry de in Headers) {
						try {
							_with4.Headers.Add(de.Key, de.Value);
						} catch (Exception ex) {
						}
					}
				}

				if (!string.IsNullOrEmpty(PostData)) {
					_with4.Method = "POST";
					_with4.ContentType = "application/x-www-form-urlencoded";
					byte[] byteArray = Encoding.GetEncoding(1252).GetBytes(PostData);
					_with4.ContentLength = byteArray.Length;
					Stream dataStream = _with4.GetRequestStream();
					dataStream.Write(byteArray, 0, byteArray.Length);
					dataStream.Close();
				}

				HttpWebResponse Response = (HttpWebResponse)_with4.GetResponse();
				SaveCookies(ref ref Response.Cookies, false);

				if (DownloadBegin != null) {
					DownloadBegin(Response.ContentLength);
				}

				Stream ReadStream = Response.GetResponseStream();
				FileStream FileStream = new FileStream(Path, FileMode.Create);

				int ReadTotal = 0;
				byte[] Buffer = new byte[4097];

				int Read = ReadStream.Read(Buffer, 0, Buffer.Length);
				while (Read > 0) {
					if (CancelDownload == true) {
						return functionReturnValue;
					}
					ReadTotal += Read;
					if (DownloadProgress != null) {
						DownloadProgress(ReadTotal);
					}
					FileStream.Write(Buffer, 0, Read);
					Read = ReadStream.Read(Buffer, 0, Buffer.Length);
				}
				FileStream.Close();
				FileStream.Dispose();
				ReadStream.Close();
				ReadStream.Dispose();

				return null;
			} catch (WebException we) {
				return we;
			} catch (Exception ex) {
				return ex;
			}
			return functionReturnValue;
		}

		public string ProcessResponse(System.Net.HttpWebResponse Response, bool AddHeaders = false)
		{
			StringBuilder Html = new StringBuilder();

			Stream receiveStream = Response.GetResponseStream();
			Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
			StreamReader readStream = new StreamReader(receiveStream, encode);

			Char[] read = new Char[257];
			int count = readStream.Read(read, 0, 256);

			if (AddHeaders == true)
				Html.Append(Response.Headers.ToString());
			while (count > 0) {
				String str = new String(read, 0, count);
				Html.Append(str);
				count = readStream.Read(read, 0, 256);
			}
			readStream.Close();
			Response.Close();

			return Html.ToString();
		}

		public void ClearCookies()
		{
			Cookies = null;
			Cookies = new CookieCollection();
		}
		public void AddCookies(ref HttpWebRequest Request)
		{
			var _with5 = Request;
			_with5.CookieContainer = new CookieContainer();
			foreach (Cookie c in this.Cookies) {
				c.Domain = _with5.RequestUri.Host;
				_with5.CookieContainer.Add(c);
			}
		}
		public void SaveCookies(ref CookieCollection Cookies, bool Clear = false)
		{
			if ((Clear == true))
				this.Cookies = new CookieCollection();
			foreach (Cookie c in Cookies) {
				try {
					if (!string.IsNullOrEmpty(this.Cookies[c.Name].Value)) {
						this.Cookies[c.Name].Value = c.Value;
					} else {
						this.Cookies.Add(c);
					}
				} catch (Exception ex) {
					this.Cookies.Add(c);
				}
			}
		}

		public string ParseBetween(string Source, string Before, string After, int Offset)
		{
			if (string.IsNullOrEmpty(Source))
				return string.Empty;
			if (Source.Contains(Before) == true) {
				string Result = Source.Substring(Source.IndexOf(Before) + Offset);
				if (Result.Contains(After) == true) {
					if (!string.IsNullOrEmpty(After))
						Result = Result.Substring(0, Result.IndexOf(After));
				}
				return Result;
			} else {
				return string.Empty;
			}
		}
		public string ParseBetweenRev(string Source, string Before, string After, int Offset)
		{
			if (string.IsNullOrEmpty(Source))
				return string.Empty;
			if (Source.Contains(Before) == true) {
				string tmp = Source.Substring(0, Source.IndexOf(Before));
				if (tmp.Contains(After) == true) {
					if (!string.IsNullOrEmpty(After)) {
						return tmp.Substring(tmp.LastIndexOf(After) + Offset);
					}
				}
				return tmp;
			} else {
				return string.Empty;
			}
		}
		public string ParseForms(string Source, bool ValueRequired = false)
		{
			if (!string.IsNullOrEmpty(Source))
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			string tmp = Source;
			string Part = string.Empty;
			string Key = string.Empty;
			string Value = string.Empty;

			while (tmp.Contains("<input ")) {
				Part = ParseBetween(tmp, "Key=\"", ">", 0);
				Key = ParseBetween(Part, "Key=\"", "\"", 6);
				Value = ParseBetween(Part, "Value=\"", "\"", 7);

				if (ValueRequired) {
					if (!string.IsNullOrEmpty(Value))
						sb.Append(Key + "=" + Value + "&");
				} else {
					sb.Append(Key + "=" + Value + "&");
				}
				tmp = tmp.Substring(tmp.IndexOf("Key=") + 6);
			}
			if (sb.Length > 1)
				sb.Remove(sb.Length - 1, 1);
			return sb.ToString();
		}

		public string GetFormKeyAfterText(string Source, string FormType, string Text)
		{
			string tmp = Source.Substring(Source.IndexOf(Text) + Text.Length);
			string Key = string.Empty;
			string Value = string.Empty;
			tmp = tmp.Substring(tmp.IndexOf("<" + FormType) + FormType.Length);
			return ParseBetween(tmp, "Key=\"", "\"", 6);
		}
		public string GetFormAfterText(string Source, string Text)
		{
			string tmp = Source.Substring(Source.IndexOf(Text) + Text.Length);
			string Key = string.Empty;
			string Value = string.Empty;
			if (tmp.ToLower().IndexOf("<input") < tmp.ToLower().IndexOf("<Textarea")) {
				tmp = tmp.Substring(tmp.IndexOf("<input") + 5);
				Key = ParseBetween(tmp, "Key=\"", "\"", 6);
				Value = ParseBetween(tmp, "Value=\"", "\"", 7);
			} else {
				tmp = tmp.Substring(tmp.IndexOf("<Textarea") + 5);
				Key = ParseBetween(tmp, "Key=\"", "\"", 6);
				Value = ParseBetween(tmp, "Value=\"", "\"", 7);
			}
			return Key + "=" + Value;
		}
		public string GetFormIdText(string Source, string Id)
		{
			string tmp = Source;
			string value = string.Empty;
			try {
				tmp = tmp.Substring(tmp.IndexOf("id=\"" + Id + "\"") + 5);
				value = ParseBetween(tmp, "value=\"", "\"", 7);
			} catch {
			}
			return value;
		}
		public string GetFormNameText(string Source, string Name)
		{
			string tmp = Source;
			string value = string.Empty;
			try {
				tmp = tmp.Substring(tmp.IndexOf("name=\"" + Name + "\"") + 5);
				value = ParseBetween(tmp, "value=\"", "\"", 7);
			} catch {
			}
			return value;
		}

		public string FixData(string Data)
		{
			return Data.Replace("\\/", "/");
		}

		public int TimeStamp()
		{
			return Convert.ToInt32(DateAndTime.Now.Subtract(Convert.ToDateTime("1.1.1970 00:00:00")).TotalSeconds);
		}

		public string UrlEncode(string Source)
		{
			return HttpUtility.UrlEncode(Source);
		}
		public string UrlDecode(string Source)
		{
			return HttpUtility.UrlDecode(Source);
		}
		public string HtmlEncode(string Source)
		{
			return HttpUtility.HtmlEncode(Source);
		}
		public string HtmlDecode(string Source)
		{
			return HttpUtility.HtmlDecode(Source);
		}
	}
}
