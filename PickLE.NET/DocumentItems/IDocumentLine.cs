using System;
using System.Collections.Generic;
using System.Text;

namespace PickLE {
	/// <summary>
	/// Abstract representation of a standardized document line item.
	/// </summary>
	interface IDocumentLine {
		/// <summary>
		/// Parses a line from a document.
		/// </summary>
		/// <param name="line">Line to be parsed.</param>
		void ParseLine(string line);

		/// <summary>
		/// Creates an item line for a PickLE document.
		/// </summary>
		/// <returns>Document-formatted line.</returns>
		string ToDocumentFormat();
	}
}
