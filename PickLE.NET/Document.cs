using System;
using System.Collections.Generic;
using System.Text;

namespace PickLE {
	/// <summary>
	/// Abstration of a pick list document.
	/// </summary>
	public class Document {
		private List<Component> _components;

		/// <summary>
		/// Constructs an empty pick list.
		/// </summary>
		public Document() {
			Components = new List<Component>();
		}

		/// <summary>
		/// Constructs and populates a document object from a pick list file.
		/// </summary>
		/// <param name="path">Pick list file path.</param>
		public Document(string path) : this() {
			ParseFile(path);
		}

		/// <summary>
		/// Parses a pick list file and populates this object with its data.
		/// </summary>
		/// <param name="path">Pick list file path.</param>
		public void ParseFile(string path) {
			Parser parser = new Parser(this);
			parser.ParseFile(path);
		}

		/// <summary>
		/// Components listed in this pick list.
		/// </summary>
		public List<Component> Components {
			get { return _components; }
			set { this._components = value; }
		}
	}
}
