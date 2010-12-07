/*
 * Created by SharpDevelop.
 * User: Kamran
 * Date: 07/12/2010
 * Time: 13:30
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
	/// Description of Pkcs12Pbe.
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
		
		private IDigest getDigest(){
			if(pbe.Digest.Equals("SHA") || pbe.Digest.Equals("SHA1") || pbe.Digest.Equals("SHA-1")){
				return new Sha1Digest();
			}else if(pbe.Digest.Equals("SHA256")){
				return new Sha256Digest();
			}else if(pbe.Digest.Equals("SHA224")){
				return new Sha224Digest();
			}else if(pbe.Digest.Equals("SHA384")){
				return new Sha384Digest();
			}else if(pbe.Digest.Equals("SHA512")){
				return new Sha512Digest();
			}else if(pbe.Digest.Equals("MD2")){
				return new MD2Digest();
			}else if(pbe.Digest.Equals("MD4")){
				return new MD4Digest();
			}else if(pbe.Digest.Equals("MD5")){
				return new MD5Digest();
			}
			
			return new Sha1Digest();
		}
	}
}
