/*
 * Created by SharpDevelop.
 * User: Kamran
 * Date: 07/12/2010
 * Time: 13:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Org.BouncyCastle.Crypto;
namespace org.xeustechnologies.crypto
{
	/// <summary>
	/// Description of BasePbe.
	/// </summary>
	public abstract class BasePbeCipher
	{
		protected Pbe pbe;
		
		public BasePbeCipher(){}
		
		public BasePbeCipher(Pbe pbe)
		{
			this.pbe=pbe;
		}
		
		public abstract IBufferedCipher createCipher(bool encrypt);

		public Pbe Pbe {
			get { return pbe; }
			set { pbe = value; }
		}
	}
}
