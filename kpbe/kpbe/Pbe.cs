/*
 * Created by SharpDevelop.
 * User: Kamran
 * Date: 07/12/2010
 * Time: 18:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace org.xeustechnologies.crypto
{
	public class Pbe
	{
		protected string algorithm;
		protected string baseAlgorithm;
		protected string digest;
		protected string type;
		protected char[] password;
		protected byte[] salt;
		protected int iterations;
		protected int keySize;
		protected int ivSize;
		
		public Pbe(){}
		
		public Pbe(string baseAlgorithm,
		           string digest,
		           string type,
		           char[] password,
		           byte[] salt,
		           int iterations,
		           int keySize)
		{
			this.baseAlgorithm=baseAlgorithm.ToUpper();
			this.digest=digest==null?"SHA":digest.ToUpper();
			this.type=type==null?"PKCS12":type.ToUpper();
			this.password=password;
			this.salt=salt;
			this.iterations=iterations;
			this.keySize=keySize;
			
			init();
		}
		
		private void init(){
			if(baseAlgorithm.Equals("AES")){
				if(type.ToUpper().Equals("OPENSSL")){
					algorithm="PBEWITHMD5AND"+keySize+"BITAES-CBC-OPENSSL";
				}else{
					algorithm="PBEWith"+digest.ToUpper()+"And"+keySize+"BitAES-CBC-BC";
				}
				ivSize=128;
			}else if(baseAlgorithm.Equals("RC4")){
				algorithm="PBEWITHSHAAND"+keySize+"BITRC4";
				ivSize=0;
			}else if(baseAlgorithm.Equals("RC2")){
				algorithm="PBEWITHSHAAND"+keySize+"BITRC2-CBC";
				ivSize=64;
			}else if(baseAlgorithm.Equals("DES")){
				algorithm="PBEWITHSHAAND"+(keySize==192?3:2)+"-KEYTRIPLEDES-CBC";
				baseAlgorithm="DESede";
				ivSize=64;
			}
		}

		public string Algorithm {
			get { return algorithm; }
			set { algorithm = value; }
		}
		
		public string BaseAlgorithm {
			get { return baseAlgorithm; }
			set { baseAlgorithm = value; }
		}
		
		public char[] Password {
			get { return password; }
			set { password = value; }
		}
		
		public byte[] Salt {
			get { return salt; }
			set { salt = value; }
		}
		
		public int Iterations {
			get { return iterations; }
			set { iterations = value; }
		}
		
		public int KeySize {
			get { return keySize; }
			set { keySize = value; }
		}
		
		public int IvSize {
			get { return ivSize; }
			set { ivSize = value; }
		}

		public string Digest {
			get { return digest; }
			set { digest = value; }
		}
	}
}
