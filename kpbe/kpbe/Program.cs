/*
 * Created by SharpDevelop.
 * User: Kamran
 * Date: 07/12/2010
 * Time: 13:09
 * 
 */
using System;
using System.IO;
using System.Collections.Generic;

using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.IO;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Utilities;

using NDesk.Options;

namespace org.xeustechnologies.crypto.kpbe
{
	class Program
	{
		// Some constants
		private const string SALT="This is a long constant phrase used as salt to create PBE key";
		private const int ITERATIONS=2000;
		
		public static void Main(string[] args)
		{
			string password=null;
			bool encrypt=false;
			string salt=SALT;
			string algorithm=null;
			string output=null;
			string type=null;
			string digest=null;
			int keySize=128;
			int iterations=ITERATIONS;

			bool showHelp=false;

			OptionSet p = new OptionSet () {
				{"a|algo=","Encryption algorithm (AES, RC4, RC2, DES)",v=>algorithm=v},
				{"p|password=","Encryption password",v=>password=v},
				{"k|keysize=","Key size",(int v)=>keySize=v},
				{"d|digest=","Digest algorithm (SHA1, SHA224, SHA256, SHA384, SHA512, MD2, MD4, MD5)", v=>digest=v},
				{"s|salt=","Salt phrase",v=>salt=v},
				{"i|interations=","Number of iterations",(int v)=>iterations=v},
				{"e|encrypt","Encrypt",v=>encrypt= v!= null},
				{"t|type=","Type (PKCS12, OPENSSL)",v=>type=v},
				{"o|output=","Output directory",v=>output=v},
				{"h|help","Help", v => showHelp = v != null}
			};

			List<string> files;
			try {
				files = p.Parse (args);

				if (showHelp) {
					ShowHelp (p);
					return;
				}

				if(files.Count==0 || output==null || password==null || algorithm==null){
					throw new OptionException();
				}
			}
			catch (OptionException e) {
				Console.WriteLine ("kpbe: Missing options");
				Console.WriteLine ("Try `kpbe --help' for more information.");
				return;
			}

			DirectoryInfo odir=Directory.CreateDirectory(output);
			Pbe pbe=new Pbe(algorithm.ToUpper(),digest, type, password.ToCharArray(),
			                Utils.ToByteArray(salt), iterations,keySize);
			
			BasePbeCipher pbeCipher=new Pkcs12PbeCipher(pbe);
			
			if(type!=null && type.ToUpper().Equals("OPENSSL")){
				pbeCipher=new OpenSSLPbeCipher(pbe);
			}
			
			foreach(string file in files){
				FileInfo fi=new FileInfo(file);
				
				if(!fi.Exists){
					Console.WriteLine ("kpbe: File "+file+" not found.");
					return;
				}
				
				Stream ins=new FileStream(file, FileMode.Open);
				Stream outs=new FileStream(odir.FullName+"/"+fi.Name, FileMode.Create);
				
				try{
					CipherStream cipherStream=new CipherStream(ins,pbeCipher.createCipher(encrypt), null);

					int ch;
					while ((ch = cipherStream.ReadByte()) >= 0)
					{
						outs.WriteByte((byte) ch);
					}
					
					cipherStream.Close();
				}catch(CryptoException e){
					Console.WriteLine("kpbe: Error: "+e.Message);
				}catch(Exception e){
					Console.WriteLine(e);
				}
				
				outs.Close();
			}
		}

		private static void ShowHelp (OptionSet p)
		{
			Console.WriteLine ("Usage: kpbe [OPTIONS]+ file(s)");
			Console.WriteLine ("An opensource PBE utility for files.");
			Console.WriteLine ("Based on encryption algorithms from bouncycastle.org");
			Console.WriteLine ("Written by: Kamran");
			Console.WriteLine ();
			Console.WriteLine ("Options:");
			p.WriteOptionDescriptions (Console.Out);
			Console.WriteLine ();
			Console.WriteLine ("Encryption example:");
			Console.WriteLine ("\tkpbe -e -a AES -k 128 -p mypassword -o outdir MySecretFile.doc");
			Console.WriteLine ("Decryption example:");
			Console.WriteLine ("\tkpbe -a AES -k 128 -p mypassword -o outdir MySecretFile.doc");
		}
	}
}