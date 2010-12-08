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

namespace org.xeustechnologies.crypto.kpbe
{
	/// <summary>
	/// PKCS12 based encryption
	/// </summary>
	public class Pkcs12PbeCipher : BasePbeCipher
	{
		public Pkcs12PbeCipher():base(){}
		
		public Pkcs12PbeCipher(Pbe pbe):base(pbe)
		{
		}
		
		public override IBufferedCipher createCipher(bool encrypt){
			IDigest digest=getDigest();
			
			PbeParametersGenerator pGen = new Pkcs12ParametersGenerator(digest);
			pGen.Init(
				PbeParametersGenerator.Pkcs12PasswordToBytes(pbe.Password),
				pbe.Salt,
				pbe.Iterations);

			ParametersWithIV parameters = (ParametersWithIV)
				pGen.GenerateDerivedParameters(pbe.BaseAlgorithm, pbe.KeySize, pbe.IvSize);

			KeyParameter encKey = (KeyParameter) parameters.Parameters;

			IBufferedCipher c;
			if (pbe.BaseAlgorithm.Equals(Kpbe.Algorithms.RC4))
			{
				c = CipherUtilities.GetCipher(pbe.BaseAlgorithm);
				
				c.Init(encrypt, encKey);
			}
			else
			{
				c = CipherUtilities.GetCipher(pbe.BaseAlgorithm + "/"+pbe.Mode+"/"+pbe.Padding);
				
				c.Init(encrypt, parameters);
			}
			
			return c;
		}
		
		private IDigest getDigest(){
			if(pbe.Digest.Equals(Kpbe.Digests.SHA1)){
				return new Sha1Digest();
			}else if(pbe.Digest.Equals(Kpbe.Digests.SHA224)){
				return new Sha224Digest();
			}else if(pbe.Digest.Equals(Kpbe.Digests.SHA256)){
				return new Sha256Digest();
			}else if(pbe.Digest.Equals(Kpbe.Digests.SHA384)){
				return new Sha384Digest();
			}else if(pbe.Digest.Equals(Kpbe.Digests.SHA512)){
				return new Sha512Digest();
			}else if(pbe.Digest.Equals(Kpbe.Digests.MD2)){
				return new MD2Digest();
			}else if(pbe.Digest.Equals(Kpbe.Digests.MD4)){
				return new MD4Digest();
			}else if(pbe.Digest.Equals(Kpbe.Digests.MD5)){
				return new MD5Digest();
			}
			
			return new Sha1Digest();
		}
	}
}
