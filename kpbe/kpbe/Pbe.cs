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
	public class Pbe
	{	
		protected string baseAlgorithm;
		protected string mode;
		protected string padding;
		protected string digest;
		protected string type;
		protected char[] password;
		protected byte[] salt;
		protected int iterations;
		protected int keySize;
		protected int ivSize;
		
		public Pbe(){}
		
		public Pbe(string baseAlgorithm,
		           string mode,
		           string padding,
		           string digest,
		           char[] password,
		           byte[] salt,
		           int iterations,
		           int keySize)
		{
			this.baseAlgorithm=baseAlgorithm.ToUpper();
			this.mode=mode==null?Kpbe.Modes.CBC:mode.ToUpper();
			this.padding=(padding==null?Kpbe.Paddings.PKCS7:
			              padding.ToUpper().Equals(Kpbe.Paddings.NONE)?"No":padding)+"Padding";
			this.digest=digest==null?Kpbe.Digests.SHA1:digest.ToUpper();
			this.password=password;
			this.salt=salt;
			this.iterations=iterations;
			this.keySize=keySize;
			
			init();
		}
		
		private void init(){
			if(baseAlgorithm.Equals(Kpbe.Algorithms.AES)){
				ivSize=128;
			}else if(baseAlgorithm.Equals(Kpbe.Algorithms.RC4)){
				ivSize=0;
			}else if(baseAlgorithm.Equals(Kpbe.Algorithms.RC2)){
				ivSize=64;
			}else if(baseAlgorithm.Equals(Kpbe.Algorithms.DES)){
				baseAlgorithm="DESede";
				ivSize=64;
			}else if(baseAlgorithm.Equals(Kpbe.Algorithms.TWOFISH)){
				ivSize=128;
			}else if(baseAlgorithm.Equals(Kpbe.Algorithms.BLOWFISH)){
				ivSize=64;
			}else{
				throw new ArgumentException("Algorithm ["+baseAlgorithm+"] not supported.");
			}
			
			if(mode.Equals(Kpbe.Modes.CTR)){
				padding="NoPadding";
			}else if(!mode.Equals(Kpbe.Modes.NONE) && !mode.Equals(Kpbe.Modes.CBC)){
				throw new ArgumentException("Mode ["+mode+"] not supported");
			}
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
			set { digest = value.ToUpper(); }
		}
		
		public string Mode {
			get { return mode; }
			set { mode = value; }
		}
		
		public string Padding {
			get { return padding; }
			set { padding = value; }
		}
	}
}
