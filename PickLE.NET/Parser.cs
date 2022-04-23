using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PickLE {
	/// <summary>
	/// Pick list document parser.
	/// </summary>
	class Parser {
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
			string line;

			while ((line = sr.ReadLine()) != null) {

			}

			sr.Close();
		}
	}
}
