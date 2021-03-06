﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Waher.Content;
using Waher.Content.Xml;
using Waher.Security;

namespace Waher.Networking.XMPP.PEP
{
	/// <summary>
	/// User Avatar data event, as defined in XEP-0084:
	/// https://xmpp.org/extensions/xep-0084.html
	/// </summary>
	public class UserAvatarData : PersonalEvent
	{
		/// <summary>
		/// urn:xmpp:avatar:data
		/// </summary>
		public const string AvatarDataNamespace = "urn:xmpp:avatar:data";

		private byte[] data = null;
		private string sha1Hash = null;

		/// <summary>
		/// User Avatar data event, as defined in XEP-0084:
		/// https://xmpp.org/extensions/xep-0084.html
		/// </summary>
		public UserAvatarData()
		{
		}

		/// <summary>
		/// Local name of the event element.
		/// </summary>
		public override string LocalName => "data";

		/// <summary>
		/// Namespace of the event element.
		/// </summary>
		public override string Namespace => AvatarDataNamespace;

		/// <summary>
		/// XML representation of the event.
		/// </summary>
		public override string PayloadXml
		{
			get
			{
				StringBuilder Xml = new StringBuilder();

				Xml.Append("<data xmlns='");
				Xml.Append(this.Namespace);
				Xml.Append("'>");
				Xml.Append(Convert.ToBase64String(this.data));
				Xml.Append("</data>");

				return Xml.ToString();
			}
		}

		/// <summary>
		/// Optional Item ID of event. If null, a new will automatically be generated by the server.
		/// </summary>
		public override string ItemId
		{
			get
			{
				if (this.sha1Hash == null)
					this.sha1Hash = Hashes.ComputeSHA1HashString(this.data);

				return this.sha1Hash;
			}
		}

		/// <summary>
		/// Parses a personal event from its XML representation
		/// </summary>
		/// <param name="E">XML representation of personal event.</param>
		/// <returns>Personal event object.</returns>
		public override IPersonalEvent Parse(XmlElement E)
		{
			UserAvatarData Result = new UserAvatarData()
			{
				data = Convert.FromBase64String(E.InnerText)
			};

			return Result;
		}

		/// <summary>
		/// Binary representation of avatar
		/// </summary>
		public byte[] Data
		{
			get => this.data;
			set
			{
				this.data = value;
				this.sha1Hash = null;
			}
		}

	}
}
