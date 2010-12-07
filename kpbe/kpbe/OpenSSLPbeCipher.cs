/*
 * Created by SharpDevelop.
 * User: Kamran
 * Date: 07/12/2010
 * Time: 16:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace org.xeustechnologies.crypto
{
	/// <summary>
	/// Description of OpenSSLPbe.
	/// </summary>
	public class OpenSSLPbeCipher: BasePbeCipher
	{
		public OpenSSLPbeCipher():base()
		{
		}
		
		public OpenSSLPbeCipher(Pbe pbe):base(pbe)
		{
		}
		
		public override IBufferedCipher createCipher(bool encrypt)
		{
			PbeParametersGenerator pGen = new OpenSslPbeParametersGenerator();
			pGen.Init(
				PbeParametersGenerator.Pkcs5PasswordToBytes(pbe.Password),
				pbe.Salt,
				pbe.Iterations);

			ParametersWithIV parameters = (ParametersWithIV)
				pGen.GenerateDerivedParameters(pbe.BaseAlgorithm, pbe.KeySize, pbe.IvSize);

			KeyParameter encKey = (KeyParameter) parameters.Parameters;

			IBufferedCipher c;
			if (pbe.BaseAlgorithm.Equals("RC4"))
			{
				c = CipherUtilities.GetCipher(pbe.BaseAlgorithm);

				c.Init(encrypt, encKey);
			}
			else
			{
				c = CipherUtilities.GetCipher(pbe.BaseAlgorithm + "/CBC/PKCS7Padding");

				c.Init(encrypt, parameters);
			}

			return c;
		}
	}
}
