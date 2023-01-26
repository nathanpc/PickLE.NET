using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using PickLE.Utilities;
using PickLE.Exceptions;

namespace PickLE {
	/// <summary>
	/// Pick list document parser.
	/// </summary>
	public class Parser {
		protected Document document;
		protected enum Phase {
			Body = 0,
			Header
		};

		/// <summary>
		/// Initializes the parser with an associated document object.
		/// </summary>
		/// <param name="doc">Associated document to be filled with the parsed
		/// data.</param>
		public Parser(Document doc) {
			this.document = doc;
		}

		/// <summary>
		/// Parses a file and populates the associated document.
		/// </summary>
		/// <param name="path">Path to a pick list file.</param>
		public void ParseFile(string path) {
			StreamReader sr = new StreamReader(path);
			Phase phase = Phase.Header;
			string line;

			// Go through lines.
			while ((line = sr.ReadLine()) != null) {
				switch (phase) {
					case Phase.Body:
						// Parsing the body of the pick list.
						if (StringUtils.IsEmptyOrWhitespace(line)) {
							// Just another empty line...
							continue;
						} else if (Category.IsLabelLine(line)) {
							// Got a category line.
							document.Categories.Add(new Category());
							document.Categories[document.Categories.Count - 1].ParseLine(line);

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
						document.Categories[document.Categories.Count - 1].Components.Add(component);

						break;
					case Phase.Header:
						// Parsing the header section.
						if (StringUtils.IsEmptyOrWhitespace(line)) {
							// Just another empty line...
							continue;
						} else if (Property.IsHeaderEndLine(line)) {
							// Looks like we are done parsing the header section.
							phase = Phase.Body;
							continue;
						} else if (!Property.IsPropertyLine(line)) {
							// We have an invalid line for this section of the document.
							throw new ParsingException("Not a valid property line in the header section.", line);
						}

						// Parse the property and append it to the properties list.
						document.Properties.Add(new Property(line));
						break;
					default:
						throw new Exception("Invalid parsing phase.");
				}
			}

			// Close the reader.
			sr.Close();
		}
	}
}
