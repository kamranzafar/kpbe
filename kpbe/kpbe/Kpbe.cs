/*
 * kpbe is a password based file encryption utility
 * Copyright (C) 2010 Kamran
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * 
 */
using System;

namespace org.xeustechnologies.crypto.kpbe
{
	/// <summary>
	/// Description of Kpbe.
	/// </summary>
	public class Kpbe
	{
		public static class Types{
			public static string OPENSSL="OPENSSL";
			public static string PKCS12="PKCS12";
		}	
		
		public static class Modes{
			public static string NONE="NONE";
			public static string CBC="CBC";
			public static string CTR="CTR";
			public static string CFB="CFB";
			public static string OFB="OFB";
		}
		
		public static class Paddings{
			public static string NONE="NONE";
			public static string PKCS7="PKCS7";
			public static string ISO10126d2="ISO10126d2";
			public static string ISO7816d4="ISO7816d4";
			public static string X932="X932";
			public static string ZEROBYTE="ZeroByte";
		}
		
		public static class Digests{
			public static string MD2="MD2";
			public static string MD4="MD4";
			public static string MD5="MD5";
			public static string SHA1="SHA1";
			public static string SHA224="SHA224";
			public static string SHA256="SHA256";
			public static string SHA384="SHA384";
			public static string SHA512="SHA512";
		}
		
		public static class Algorithms{
			public static string AES="AES";
			public static string DES="DES";
			public static string RC2="RC2";
			public static string RC4="RC4";
			public static string TWOFISH="TWOFISH";
			public static string BLOWFISH="BLOWFISH";
		}
	}
}
