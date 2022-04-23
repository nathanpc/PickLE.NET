using System;
using System.Collections.Generic;
using System.Text;

namespace PickLE {
	/// <summary>
	/// Representation of an electronic component in a pick list.
	/// </summary>
	public class Component {
		private bool _picked;
		private string _name;
		private string _value;
		private string _description;
		private string _package;
		private string _category;
		private List<string> _refdes;

		/// <summary>
		/// Constructs an empty component object.
		/// </summary>
		public Component() {
			// Required attributes.
			Picked = false;
			Name = "";
			Category = "";
			RefDes = new List<string>();

			// Optional attributes.
			Value = null;
			Description = null;
			Package = null;
		}

		/// <summary>
		/// Gets a string that represents how this component is defined in a
		/// pick list file.
		/// </summary>
		/// <returns>String that can be used inside a pick list file.</returns>
		public string ToDocumentString() {
			StringBuilder str = new StringBuilder();

			// Build up the descriptor line.
			str.Append("[" + ((Picked) ? "X" : " ") + "]");
			str.Append("\t" + Quantity);
			str.Append("\t" + Name);
			if (Value != null)
				str.Append("\t(" + Value + ")");
			str.Append("\t{" + Category + "}");
			if (Description != null)
				str.Append("\t\"" + Description + "\"");
			if (Package != null)
				str.Append("\t[" + Package + "]");

			// Append the reference designator line.
			str.AppendLine();
			str.AppendLine(string.Join(" ", RefDes.ToArray()));

			return str.ToString();
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
		/// Category of the component.
		/// </summary>
		public string Category {
			get { return _category; }
			set { this._category = value; }
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
			get { return _refdes.Count; }
		}

		/// <summary>
		/// String representation of this object.
		/// </summary>
		/// <returns>Component name and quantity.</returns>
		public override string ToString() {
			return Quantity + "x " + Name;
		}
	}
}
