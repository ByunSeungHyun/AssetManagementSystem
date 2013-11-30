using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
//using System.Web;
//using System.Web.Security;

using System.Security.Principal;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection;
using System.DirectoryServices.Protocols;
using System.Runtime.InteropServices;

namespace Intra.User
{
	public class LdapAuthentication : IDisposable
	{
		TimeSpan _passwordAge = TimeSpan.MinValue;
		DirectoryEntry en = null;

		private string _path;
		private string _filterAttribute;
		private string _maxpwdage;

		public void Dispose()
		{
			_path = null;
			_filterAttribute = null;
			_maxpwdage = null;
			en = null;
		}

		public LdapAuthentication(string path)
		{
			_path = path;
			_maxpwdage = "";
		}

		public bool IsAuthenticated(string domain, string username, string pwd)
		{
			string domainAndUsername = domain + @"\" + username;

			if (!string.IsNullOrEmpty(_path)) // _path를 입력받아 class를 생성한 경우에만 동작한다.
			{
				DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

				try
				{
					this.en = entry;
					//Bind to the native AdsObject to force authentication.
					object obj = entry.NativeObject;

					DirectorySearcher search = new DirectorySearcher(entry);

					search.Filter = "(&(objectClass=user)(objectCategory=person)(sAMAccountName=" + username + "))";
					//search.Filter = "(SAMAccountName=" + username + ")";
					//search.PropertiesToLoad.Add("cn");

					SearchResult result = search.FindOne();

					if (null == result)
					{
						return false;
					}


					DirectoryEntry entry2 = new DirectoryEntry(result.Path, username, pwd);

					TimeSpan maxPwdAge = TimeSpan.MinValue;

					if (entry.Properties.Contains("maxPwdAge"))
					{
						try
						{
							DateTime pwdLastSet = DateTime.FromFileTime((long)result.Properties["pwdLastSet"][0]);
							if (pwdLastSet.Subtract(PasswordAge).CompareTo(DateTime.Now) > 0)
							{
								TimeSpan t = pwdLastSet.Subtract(PasswordAge).Subtract(DateTime.Now);
								_maxpwdage = string.Format("{0}", DateTime.Now.Add(t));
							}


						}
						catch (Exception) { _maxpwdage = (string)result.Properties["cn"][0]; } // "error1"; }
					}
					else
					{
						_maxpwdage = "없음";
					}

					//DirectoryEntry de4 = result.GetDirectoryEntry();
					//_maxpwdage = de4.Properties["maxpwdage"].ToString();
					//Update the new path to the user in the directory.
					_path = result.Path;
					_filterAttribute = (string)result.Properties["cn"][0];

				}
				catch (Exception ex)
				{
					//throw new Exception("Error authenticating user. " + ex.Message);
					return false;
				}
			}
			else
			{
				return false;
			}
			return true;
		}

		public TimeSpan PasswordAge
		{
			get
			{
				if (_passwordAge == TimeSpan.MinValue)
				{
					long ldate = LongFromLargeInteger(en.Properties["maxPwdAge"][0]);
					_passwordAge = TimeSpan.FromTicks(ldate);
				}

				return _passwordAge;
			}
		}

		private long LongFromLargeInteger(object largeInteger)
		{
			System.Type type = largeInteger.GetType();
			int highPart = (int)type.InvokeMember("HighPart", BindingFlags.GetProperty, null, largeInteger, null);
			int lowPart = (int)type.InvokeMember("LowPart", BindingFlags.GetProperty, null, largeInteger, null);

			return (long)highPart << 32 | (uint)lowPart;
		}
	}
}
