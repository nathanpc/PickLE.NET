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
			Empty = 0,
			Property,
			Descriptor,
			RefDes
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
			Phase phase = Phase.Property;
			string line;
			//string currentCategory = "Unknown";
			//Component component = null;
			/*Regex regex = new Regex(
				@"\[(?<picked>.)\]\s+(?<quantity>\d+)\s+(?<name>[^\s]+)\s*(\((?<value>[^\)]+)\)\s*)?(""(?<description>[^\""]+)""\s*)?(\[(?<case>[^\]]+)\]\s*)?",
				RegexOptions.Compiled);
			*/

			// Go through lines.
			while ((line = sr.ReadLine()) != null) {
				switch (phase) {
					case Phase.Empty:
						// Parsing the body of the pick list.
						if (StringUtils.IsEmptyOrWhitespace(line)) {
							// Just another empty line...
							continue;
						} else if (Category.IsLabelLine(line)) {
							// Got a category line.
							document.Categories.Add(new Category());
							document.Categories[document.Categories.Count - 1].ParseLine(line);

							continue;
						} else {
							//throw new ParsingException("Unknown type of line in body of pick list.", line);
						}
						break;
					case Phase.Property:
						// Parsing the header section.
						if (StringUtils.IsEmptyOrWhitespace(line)) {
							// Just another empty line...
							continue;
						} else if (Property.IsHeaderEndLine(line)) {
							// Looks like we are done parsing the header section.
							phase = Phase.Empty;
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
				/*
				switch (phase) {
				case Phase.Empty:
					if (line[0] == '[') {
						// Looks like we are about to parse a descriptor line.
						phase = Phase.Descriptor;
						component = new Component();
					} else if (line[line.Length - 1] == ':') {
						// Got a category line.
						currentCategory = line.Substring(0, line.Length - 1);
						continue;
					} else if (line.Length == 0) {
						// Just another empty line...
						continue;
					}
					break;
				case Phase.RefDes:
					// Looks like we've finished parsing this component.
					if (line.Length == 0) {
						document.Components.Add(component);
						component = null;
						phase = Phase.Empty;
						continue;
					}

					// Parse the reference designators.
					component.RefDes.AddRange(line.Split(new char[] { ' ' },
						StringSplitOptions.RemoveEmptyEntries));
					continue;
				}

				// Parse the descriptor line.
				Match match = regex.Match(line);
				component.Picked = match.Groups["picked"].Value != " ";
				component.Name = match.Groups["name"].Value;
				component.Value = match.Groups["value"].Value;
				component.Category = currentCategory;
				component.Description = match.Groups["description"].Value;
				component.Package = match.Groups["case"].Value;

				// Move to the next phase.
				phase = Phase.RefDes;
				*/
			}

			// Close the reader.
			sr.Close();
		}
	}
}
