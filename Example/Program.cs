using System;
using System.Collections.Generic;
using System.Text;
using PickLE;

namespace PickLE.Example {
	class Program {
		static void Main(string[] args) {
			// Get the pick list file to parse.
			string filePath = @"D:\Projects\pickle\examples\example.pkl";
			if (args.Length >= 1) {
				filePath = args[0];
			}

			// Parse the document and list its contents.
			Document doc = new Document(filePath);
			foreach (Component component in doc.Components) {
				Console.WriteLine(component.ToDocumentString());
			}

			// Wait to close.
			Console.ReadKey();
		}
	}
}
