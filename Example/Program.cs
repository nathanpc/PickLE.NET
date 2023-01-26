using System;
using System.Collections.Generic;
using System.Text;
using PickLE;

namespace PickLE.Example {
	class Program {
		static void Main(string[] args) {
			// Get the pick list file to parse.
			string filePath = @"..\..\..\example.pkl";
			if (args.Length >= 1) {
				filePath = args[0];
			}

			// Parse the document and list its contents.
			Document doc = new Document(filePath);
			foreach (string category in doc.Categories) {
				Console.WriteLine(category + ":");

				foreach (Component component in doc.GetComponentsByCategory(category)) {
					Console.Write("    [" + ((component.Picked) ? "X" : " ") + "]");
					Console.Write(" " + component.Quantity + "x " + component.Name);
					if (component.Value != null)
						Console.Write(" (" + component.Value + ")");
					if (component.Description != null)
						Console.Write(" \"" + component.Description + "\"");
					if (component.Package != null)
						Console.Write(" [" + component.Package + "]");

					Console.WriteLine();
					Console.WriteLine("        " + string.Join(" ", component.RefDes.ToArray()));
				}

				Console.WriteLine();
			}

			// Wait to close.
			Console.ReadKey();
		}
	}
}
