using PickLE.Exceptions;
using PickLE.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace PickLE {
	/// <summary>
	/// Abstration of a pick list document.
	/// </summary>
	public class Document {
		private List<Property> _properties;
		private List<Category> _categories;

		protected enum Section {
			Body = 0,
			Header
		};

		/// <summary>
		/// Constructs an empty pick list.
		/// </summary>
		public Document() {
			Properties = new List<Property>();
			Categories = new List<Category>();
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
			StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open));
			Section section = Section.Header;
			string line;

			// Go through lines.
			while ((line = sr.ReadLine()) != null) {
				switch (section) {
					case Section.Body:
						// Parsing the body of the pick list.
						if (StringUtils.IsEmptyOrWhitespace(line)) {
							// Just another empty line...
							continue;
						} else if (Category.IsLabelLine(line)) {
							// Got a category line.
							Categories.Add(new Category());
							Categories[Categories.Count - 1].ParseLine(line);

							continue;
						} else if (!Component.IsDescriptorLine(line)) {
							// We have no idea what this line actually is.
							throw new ParsingException("Unknown type of line in body of pick list.", line);
						}

						// Parsing a component.
						Component component = new Component(line);
						line = sr.ReadLine();
						if (!Component.IsRefDesLine(line))
							throw new ParsingException("A reference designator line must always follow a component descriptor line.", line);
						component.ParseRefDesLine(line);
						Categories[Categories.Count - 1].Components.Add(component);

						break;
					case Section.Header:
						// Parsing the header section.
						if (StringUtils.IsEmptyOrWhitespace(line)) {
							// Just another empty line...
							continue;
						} else if (Property.IsHeaderEndLine(line)) {
							// Looks like we are done parsing the header section.
							section = Section.Body;
							continue;
						} else if (!Property.IsPropertyLine(line)) {
							// We have an invalid line for this section of the document.
							throw new ParsingException("Not a valid property line in the header section.", line);
						}

						// Parse the property and append it to the properties list.
						Properties.Add(new Property(line));
						break;
					default:
						throw new Exception("Invalid parsing phase.");
				}
			}

			// Close the reader.
			#if WINDOWS_UWP
				sr.Dispose();
			#else
				sr.Close();
			#endif  // WINDOWS_UWP
        }

		/// <summary>
		/// List of properties in the header of a document.
		/// </summary>
		public List<Property> Properties {
			get { return _properties; }
			set { _properties = value; }
		}

		/// <summary>
		/// List of the available categories of components in the pick list.
		/// </summary>
		public List<Category> Categories {
			get { return _categories; }
			set { _categories = value; }
		}

		/// <summary>
		/// Components listed in this pick list.
		/// </summary>
		public List<Component> Components {
			get { return null; }
		}
	}
}
