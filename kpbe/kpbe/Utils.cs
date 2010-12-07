/*
 * Created by SharpDevelop.
 * User: Kamran
 * Date: 07/12/2010
 * Time: 13:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace org.xeustechnologies.crypto
{
	/// <summary>
	/// Description of Utils.
	/// </summary>
	public class Utils
	{
		public static byte[] ToByteArray(string str)
		{
			System.Text.UTF8Encoding  encoding=new System.Text.UTF8Encoding();
			return encoding.GetBytes(str);
		}
	}
}
