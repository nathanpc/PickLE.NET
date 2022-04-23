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
			set { this._value = value; }
		}

		/// <summary>
		/// Description of the component.
		/// </summary>
		public string Description {
			get { return _description; }
			set { this._description = value; }
		}

		/// <summary>
		/// Component package if applicable.
		/// </summary>
		public string Package {
			get { return _package; }
			set { this._package = value; }
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
