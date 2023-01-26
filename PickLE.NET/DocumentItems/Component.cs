using PickLE.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace PickLE {
	/// <summary>
	/// Representation of an electronic component in a pick list.
	/// </summary>
	public class Component : IDocumentLine {
		private bool _picked;
		private string _name;
		private string _value;
		private string _description;
		private string _package;
		private List<string> _refdes;

		protected static Regex regex = new Regex(
			@"\[(?<picked>.)\]\s+(?<quantity>\d+)\s+(?<name>[^\s]+)\s*(\((?<value>[^\)]+)\)\s*)?(""(?<description>[^\""]+)""\s*)?(\[(?<case>[^\]]+)\]\s*)?",
			RegexOptions.Compiled);
		protected static Regex validRefDesRegex = new Regex(
			@"^[A-Z][ A-Z0-9]+$", RegexOptions.Compiled);

		/// <summary>
		/// Constructs an empty component object.
		/// </summary>
		public Component() {
			// Required attributes.
			Picked = false;
			Name = "";
			RefDes = new List<string>();

			// Optional attributes.
			Value = null;
			Description = null;
			Package = null;
		}

		/// <summary>
		/// Constructs a partially populated component object from a descriptor
		/// line.
		/// </summary>
		/// <param name="line">Descriptor line to be parsed.</param>
		public Component(string line)
			: this() {
			ParseLine(line);
		}

		/// <summary>
		/// Checks if a document line is a valid component description line.
		/// </summary>
		/// <param name="line">Line to be checked.</param>
		/// <returns>Is this line a component description?</returns>
		public static bool IsDescriptorLine(string line) {
			return (line != null) && line.StartsWith("[");
		}

		/// <summary>
		/// Checks if a document line is a valid reference designator line.
		/// </summary>
		/// <param name="line">Line to be checked.</param>
		/// <returns>Is this line a reference designator line?</returns>
		public static bool IsRefDesLine(string line) {
			return (line != null) && validRefDesRegex.IsMatch(line);
		}

		/// <summary>
		/// Parses the component descriptor line from a document.
		/// </summary>
		/// <param name="line">Line to be parsed.</param>
		public void ParseLine(string line) {
			Match match = regex.Match(line);
			Picked = match.Groups["picked"].Value != " ";
			Name = match.Groups["name"].Value;
			Value = match.Groups["value"].Value;
			Description = match.Groups["description"].Value;
			Package = match.Groups["case"].Value;
		}

		/// <summary>
		/// Parses the reference designator line from a document.
		/// </summary>
		/// <param name="line">Line to be parsed.</param>
		public void ParseRefDesLine(string line) {
			RefDes.Clear();
			#if PocketPC
				RefDes = new List<string>(line.Split(' '));
			#else
				RefDes = new List<string>(line.Split(
					new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
			#endif  // PocketPC
		}

		/// <summary>
		/// Gets a document-formatted string of the component descriptor line.
		/// </summary>
		/// <returns>Document formatted component descriptor lines.</returns>
		public string GetDescriptorLine() {
			StringBuilder str = new StringBuilder();

			// Build up the descriptor line.
			str.Append("[" + ((Picked) ? "X" : " ") + "]");
			str.Append("\t" + Quantity);
			str.Append("\t" + Name);
			if (Value != null)
				str.Append("\t(" + Value + ")");
			if (Description != null)
				str.Append("\t\"" + Description + "\"");
			if (Package != null)
				str.Append("\t[" + Package + "]");

			return str.ToString();
		}

		/// <summary>
		/// Gets a document-formatted string of the reference designator line.
		/// </summary>
		/// <returns>Document formatted reference designator line.</returns>
		public string GetRefDesLine() {
			return String.Join(" ", RefDes.ToArray());
		}

		/// <summary>
		/// Creates a full component definition for a PickLE document.
		/// </summary>
		/// <returns>Document formatted component definition lines.</returns>
		public string ToDocumentFormat() {
			StringBuilder str = new StringBuilder();

			#if PocketPC
				str.Append(GetDescriptorLine());
				str.Append("\r\n");
			#else
				str.AppendLine(GetDescriptorLine());
			#endif  // PocketPC
			str.Append(GetRefDesLine());

			return str.ToString();
		}

		/// <summary>
		/// Gets the string representation of the component.
		/// </summary>
		/// <returns>The full component as it is represented in the document.</returns>
		public override string ToString() {
			return ToDocumentFormat();
		}

		/// <summary>
		/// Has the component already been picked?
		/// </summary>
		public bool Picked {
			get { return _picked; }
			set { this._picked = value; }
		}

		/// <summary>
		/// Name or manufacturer part number of the component.
		/// </summary>
		public string Name {
			get { return _name; }
			set { this._name = value; }
		}

		/// <summary>
		/// Value of a component if applicable.
		/// </summary>
		public string Value {
			get { return _value; }
			set {
				this._value = value;
				if (value != null) {
					if (value.Length == 0)
						this._value = null;
				}
			}
		}

		/// <summary>
		/// Description of the component.
		/// </summary>
		public string Description {
			get { return _description; }
			set {
				this._description = value;
				if (value != null) {
					if (value.Length == 0)
						this._description = null;
				}
			}
		}

		/// <summary>
		/// Component package if applicable.
		/// </summary>
		public string Package {
			get { return _package; }
			set {
				this._package = value;
				if (value != null) {
					if (value.Length == 0)
						this._package = null;
				}
			}
		}

		/// <summary>
		/// List of reference designators.
		/// </summary>
		public List<string> RefDes {
			get { return _refdes; }
			set { this._refdes = value; }
		}

		/// <summary>
		/// Quantity of items to be picked.
		/// </summary>
		public int Quantity {
			get { return RefDes.Count; }
		}
	}
}
