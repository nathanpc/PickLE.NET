using System;
using System.Collections.Generic;
using System.Text;

namespace PickLE {
	/// <summary>
	/// Abstration of a pick list document.
	/// </summary>
	class Document {
		private List<Component> _components;

		/// <summary>
		/// Constructs an empty pick list.
		/// </summary>
		public Document() {
			Components = new List<Component>();
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
