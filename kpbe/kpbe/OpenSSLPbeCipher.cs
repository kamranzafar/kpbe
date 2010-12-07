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

using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace org.xeustechnologies.crypto
{
	/// <summary>
	/// Open SSL based encryption
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
