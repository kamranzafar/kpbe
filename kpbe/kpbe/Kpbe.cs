/*
 * Created by SharpDevelop.
 * User: Kamran
 * Date: 08/12/2010
 * Time: 20:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
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
