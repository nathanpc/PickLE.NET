using PickLE.Exceptions;
using PickLE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace PickLE {
	/// <summary>
	/// Representation of a document property that resides in the header.
	/// </summary>
	public class Property : IDocumentLine {
		private string _name;
		private string _value;

		protected static Regex regex = new Regex(
			@"(?<name>[A-Za-z0-9 \-]+):\s+(?<value>.+)", RegexOptions.Compiled);

		/// <summary>
		/// Creates an empty document property.
		/// </summary>
		public Property() {
		}

		/// <summary>
		/// Constructs a pre-populated document property object.
		/// </summary>
		/// <param name="name">Name of the property.</param>
		/// <param name="value">Value of the property.</param>
		public Property(string name, string value) {
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Constructs a property object from a document line.
		/// </summary>
		/// <param name="line">Document line to be parsed.</param>
		public Property(string line) {
			ParseLine(line);
		}

		/// <summary>
		/// Checks if a document line is a valid property line.
		/// </summary>
		/// <param name="line">Line to be checked.</param>
		/// <returns>Is this a property line that we can parse?</returns>
		public static bool IsPropertyLine(string line) {
			return StringUtils.StartsWithLetter(line) && line.Contains(": ");
		}

		/// <summary>
		/// Checks if we have reached the header section delimiter line.
		/// </summary>
		/// <param name="line">Line to be checked.</param>
		/// <returns>Have we reached the end of the header section?</returns>
		public static bool IsHeaderEndLine(string line) {
			return line == "---";
		}

		/// <summary>
		/// Parses a property line from a document.
		/// </summary>
		/// <param name="line">Line to be parsed.</param>
		public void ParseLine(string line) {
			// Parse the property line.
			Match match = regex.Match(line);

			// Check if the parsing was successful.
			if (!match.Success)
				throw new ParsingException("Couldn't parse the property line.", line);

			// Populate ourselves.
			Name = match.Groups["name"].Value;
			Value = match.Groups["value"].Value;
		}

		/// <summary>
		/// Creates a property line for a PickLE document.
		/// </summary>
		/// <returns>Document formatted property line.</returns>
		public string ToDocumentFormat() {
			return Name + ": " + Value;
		}

		/// <summary>
		/// Gets a prettier version of the document property that can be used
		/// for displaying it to the user.
		/// </summary>
		/// <returns>Prettified property name.</returns>
		public string GetPrettyName() {
			return Name.Replace('-', ' ');
		}

		/// <summary>
		/// Gets the string representation of a document property.
		/// </summary>
		/// <returns>The property as it is represented in the document.</returns>
		public override string ToString() {
			return ToDocumentFormat();
		}

		/// <summary>
		/// Property name.
		/// </summary>
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Property value.
		/// </summary>
		public string Value {
			get { return _value; }
			set { _value = value; }
		}
	}
}
