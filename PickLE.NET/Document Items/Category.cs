using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PickLE {
	/// <summary>
	/// A category in a pick list document.
	/// </summary>
	public class Category : IDocumentLine {
		private string _name;
		private List<Component> _components;

		protected static Regex regex = new Regex(
			@"^(?<name>[^:]+):[\s]*$", RegexOptions.Compiled);
		protected static Regex validLineRegex = new Regex(
			@"^[^\s][^:]+:[\s]*$", RegexOptions.Compiled);
		
		/// <summary>
		/// Creates a brand new blank category object.
		/// </summary>
		public Category() {
			Components = new List<Component>();
		}

		/// <summary>
		/// Creates a brand new category without any components.
		/// </summary>
		/// <param name="name">Name of the category.</param>
		public Category(string name) : base() {
			Name = name;
		}

		/// <summary>
		/// Creates a fully populated category.
		/// </summary>
		/// <param name="name">Category name.</param>
		/// <param name="components">Components belonging to this category.</param>
		public Category(string name, List<Component> components) {
			Name = name;
			Components = components;
		}

		/// <summary>
		/// Checks if a document line is a valid category label line.
		/// </summary>
		/// <param name="line">Line to be checked.</param>
		/// <returns>Is this line a category label line?</returns>
		public static bool IsLabelLine(string line) {
			return validLineRegex.IsMatch(line);
		}

		/// <summary>
		/// Parses a category label line from a document.
		/// </summary>
		/// <param name="line">Line to be parsed.</param>
		public void ParseLine(string line) {
			// Parse the property line.
			Match match = regex.Match(line);

			// Check if the parsing was successful.
			if (!match.Success)
				throw new Exception("Couldn't parse the category label line.");

			// Populate ourselves.
			Name = match.Groups["name"].Value;
			Components.Clear();
		}

		/// <summary>
		/// Creates a category label line for a PickLE document.
		/// </summary>
		/// <returns>Document formatted category label line.</returns>
		public string ToDocumentFormat() {
			return Name + ":";
		}

		/// <summary>
		/// Gets the string representation of a category label.
		/// </summary>
		/// <returns>The category label as it is represented in the document.</returns>
		public override string ToString() {
			return ToDocumentFormat();
		}

		/// <summary>
		/// Name of the category.
		/// </summary>
		public string Name {
			get { return _name; }
			set { _name = value; }
		}

		/// <summary>
		/// Components belonging to this category.
		/// </summary>
		public List<Component> Components {
			get { return _components; }
			set { _components = value; }
		}
	}
}
