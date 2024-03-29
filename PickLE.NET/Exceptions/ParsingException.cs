﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PickLE.Exceptions {
	/// <summary>
	/// Provides detailed information about an error while parsing a document.
	/// </summary>
	public class ParsingException : FormatException {
		public ParsingException() : base() {
		}

		public ParsingException(string message) : base(message) {
		}

		public ParsingException(string message, Exception innerException)
			: base(message, innerException) {
		}

		/// <summary>
		/// Constructs a new parsing exception with a message and the line
		/// where the error occurred.
		/// </summary>
		/// <param name="message">Description of the issue.</param>
		/// <param name="line">Line that we tried to parse.</param>
		public ParsingException(string message, string line)
			: this(message + "\n\tat \"" + line + "\"") {
			#if !PocketPC
				Data.Add("MalformedLine", line);
			#endif  // !PocketPC
		}
	}
}
