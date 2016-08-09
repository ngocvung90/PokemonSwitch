using System;
using Xamarin.Forms;
using System.IO;
using System.Threading.Tasks;
using PokemonSwitch.Droid;

[assembly: Dependency (typeof (SaveAndLoad_Android))]

namespace PokemonSwitch.Droid
{
	public class SaveAndLoad_Android : ISaveAndLoad
	{
		#region ISaveAndLoad implementation

		public void SaveText (string filename, string text)
		{
			var path = CreatePathToFile (filename);
			using (StreamWriter sw = File.CreateText (path))
				 sw.Write(text);
		}

		public string LoadText (string filename)
		{
			var path = CreatePathToFile (filename);
			using (StreamReader sr = File.OpenText(path))
				return  sr.ReadToEnd();
		}

		public bool FileExists (string filename)
		{
			return File.Exists (CreatePathToFile (filename));
		}

		#endregion

		string CreatePathToFile (string filename)
		{
            var docsPath = "/sdcard/Android";//Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(docsPath, filename);
		}
	}
}