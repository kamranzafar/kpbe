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
	/// <summary>
	/// Utility class
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
