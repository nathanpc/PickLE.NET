using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PickLE.Utilities {
	/// <summary>
	/// A bunch of string utilities to be used throughout the project.
	/// </summary>
	public static class StringUtils {
		private static Regex whitespaceRegex = new Regex(@"^[\s]+$", RegexOptions.Compiled);

		/// <summary>
		/// Checks if a string starts with a letter character.
		/// </summary>
		/// <param name="str">String to be checked.</param>
		/// <returns>Does this string start with a letter?</returns>
		public static bool StartsWithLetter(string str) {
			return !String.IsNullOrEmpty(str) && Char.IsLetter(str[0]);
		}

		/// <summary>
		/// Checks if a string is empty or just contains whitespace.
		/// </summary>
		/// <param name="str">String to be checked.</param>
		/// <returns>Is this string empty or just contains whitespace?</returns>
		public static bool IsEmptyOrWhitespace(string str) {
			return (str != null) && ((str.Length == 0) || whitespaceRegex.IsMatch(str));
		}
	}
}
