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
		/// Gets the list of components that belong to the specified category.
		/// </summary>
		/// <param name="category">Category to be used as a filter.</param>
		/// <returns>Filtered list of components.</returns>
		public List<Component> GetComponentsByCategory(string category) {
			List<Component> components = new List<Component>();

			// Go through components filtering them.
			foreach (Component component in Components) {
				if (component.Category == category)
					components.Add(component);
			}

			return components;
		}

		/// <summary>
		/// Gets the list of components that come in the specified package.
		/// </summary>
		/// <param name="package">Package to be used as a filter.</param>
		/// <returns>Filtered list of components.</returns>
		public List<Component> GetComponentsByPackage(string package) {
			List<Component> components = new List<Component>();

			// Go through components filtering them.
			foreach (Component component in Components) {
				if (component.Package == package)
					components.Add(component);
			}

			return components;
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

		/// <summary>
		/// Generates a list of the available categories of components in the list.
		/// </summary>
		public List<string> Categories {
			get {
				List<string> categories = new List<string>();
				foreach (Component component in Components) {
					if (!categories.Contains(component.Category))
						categories.Add(component.Category);
				}
				return categories;
			}
		}

		/// <summary>
		/// Generates a list of the available packages of components in the list.
		/// </summary>
		public List<string> Packages {
			get {
				List<string> packages = new List<string>();
				foreach (Component component in Components) {
					if (!packages.Contains(component.Package))
						packages.Add(component.Package);
				}
				return packages;
			}
		}
	}
}
