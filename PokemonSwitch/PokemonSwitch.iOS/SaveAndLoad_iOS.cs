using System;
using Xamarin.Forms;
using WorkingWithFiles.iOS;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using System.Linq;
using PokemonSwitch;

[assembly: Dependency (typeof(SaveAndLoad_iOS))]

namespace WorkingWithFiles.iOS
{
	public class SaveAndLoad_iOS : ISaveAndLoad
	{
		public static string DocumentsPath {
			get {
				var documentsDirUrl = NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User).Last ();
				return documentsDirUrl.Path;
			}
		}

		#region ISaveAndLoad implementation

		public void SaveText (string filename, string text)
		{
			string path = CreatePathToFile (filename);
			using (StreamWriter sw = File.CreateText(path))
				 sw.Write(text);
		}

		public string LoadText (string filename)
		{
			string path = CreatePathToFile (filename);
			using (StreamReader sr = File.OpenText(path))
				return  sr.ReadToEnd();
		}

		public bool FileExists (string filename)
		{
			return File.Exists (CreatePathToFile (filename));
		}

		#endregion

		static string CreatePathToFile(string fileName)
		{
			return Path.Combine (DocumentsPath, fileName);
		}
	}
}