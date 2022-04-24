using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace PickLE {
	/// <summary>
	/// Pick list document parser.
	/// </summary>
	public class Parser {
		protected Document document;
		protected enum Phase {
			Empty = 0,
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
			Phase phase = Phase.Empty;
			string line;
			Component component = null;
			Regex regex = new Regex(
				@"\[(?<picked>.)\]\s+(?<quantity>\d+)\s+(?<name>[^\s]+)\s*(\((?<value>[^\)]+)\)\s*)?({(?<category>[^}]+)}\s*)?(""(?<description>[^\""]+)""\s*)?(\[(?<case>[^\]]+)\]\s*)?",
				RegexOptions.Compiled);

			// Go through lines.
			while ((line = sr.ReadLine()) != null) {
				switch (phase) {
				case Phase.Empty:
					if (line[0] == '[') {
						// Looks like we are about to parse a descriptor line.
						phase = Phase.Descriptor;
						component = new Component();
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
				component.Category = match.Groups["category"].Value;
				component.Description = match.Groups["description"].Value;
				component.Package = match.Groups["case"].Value;

				// Move to the next phase.
				phase = Phase.RefDes;
			}

			sr.Close();
		}
	}
}
