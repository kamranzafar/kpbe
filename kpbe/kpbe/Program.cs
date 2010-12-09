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
using System.IO;
using System.Collections.Generic;
using System.Reflection;

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
		private const string DEFAULT_SALT="This is a long constant phrase used as salt to create PBE key";
		private const int DEFAULT_ITERATIONS=100;
		private const int DEFAULT_KEY_SIZE=128;
		
		public static void Main(string[] args)
		{
			string password=null;
			bool encrypt=false;
			string salt=DEFAULT_SALT;
			string algorithm=null;
			string mode=null;
			string padding=null;
			string output=null;
			string type=null;
			string digest=null;
			int keySize=DEFAULT_KEY_SIZE;
			int iterations=DEFAULT_ITERATIONS;

			bool showHelp=false;

			OptionSet p = new OptionSet () {
				{"a|algo=","Encryption algorithm (AES, RC4, RC2, DES, BLOWFISH, TWOFISH)",v=>algorithm=v},
				{"m|mode=","Block cipher mode (NONE, CBC, CTR, CFB, OFB)",v=>mode=v},
				{"b|padding=","Block padding (NONE, PKCS7, ISO10126d2, ISO7816d4, X932, ZEROBYTE)",v=>padding=v},
				{"p|password=","Encryption password",v=>password=v},
				{"k|keysize=","Key size",(int v)=>keySize=v},
				{"d|digest=","Digest algorithm (SHA1, SHA224, SHA256, SHA384, SHA512, MD2, MD4, MD5)", v=>digest=v},
				{"s|salt=","Salt phrase",v=>salt=v},
				{"i|iterations=","Number of iterations",(int v)=>iterations=v},
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

			BasePbeCipher pbeCipher=null;
			try{
				Pbe pbe=new Pbe(algorithm.ToUpper(), mode, padding, digest, password.ToCharArray(),
				                Utils.ToByteArray(salt), iterations,keySize);
				
				pbeCipher=new Pkcs12PbeCipher(pbe);
				if(type!=null && type.ToUpper().Equals(Kpbe.Types.OPENSSL)){
					pbeCipher=new OpenSSLPbeCipher(pbe);
				}
			}catch(Exception e){
				Console.WriteLine("kpbe: "+e.Message);
				return;
			}
			
			foreach(string file in files){
				FileInfo fi=new FileInfo(file);
				
				if(!fi.Exists){
					Console.WriteLine ("kpbe: File "+file+" not found.");
					return;
				}
				
				try{
					DirectoryInfo odir=Directory.CreateDirectory(output);
					
					Stream ins=new FileStream(file, FileMode.Open);
					Stream outs=new FileStream(odir.FullName+"/"+fi.Name, FileMode.Create);

					CipherStream cipherStream=new CipherStream(ins,pbeCipher.createCipher(encrypt), null);

					int ch;
					while ((ch = cipherStream.ReadByte()) >= 0)
					{
						outs.WriteByte((byte) ch);
					}
					
					cipherStream.Close();
					outs.Close();
				}catch(CryptoException e){
					Console.WriteLine("kpbe: "+e.Message);
				}catch(ArgumentException e){
					Console.WriteLine("kpbe: "+e.Message);
				}catch(Exception e){
					Console.WriteLine(e);
				}
			}
		}

		private static void ShowHelp (OptionSet p)
		{
			Version v=Assembly.GetExecutingAssembly().GetName().Version;
			string version=v.Major+"."+v.Minor+"."+v.Build;
			
			Console.WriteLine ("kpbe v-"+version+" opensource PBE utility for files.");
			Console.WriteLine ("Based on encryption algorithms from bouncycastle.org");
			Console.WriteLine ("Copyright 2010 Kamran");
			Console.WriteLine ();
			Console.WriteLine ("Usage: kpbe [OPTIONS]+ file(s)");
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