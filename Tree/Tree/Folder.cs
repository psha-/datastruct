using System;
using System.IO;
using System.Collections.Generic;

namespace TreeStruct
{
	public class Folder
	{
		public string Name { get; private set;}
		public File[] Files { get; private set;}
		public Folder[] Children { get; private set;}

		public static Dictionary<string, Folder> Folders = new Dictionary<string, Folder> ();

		public Folder (string name, File[] files, Folder[] children)
		{
			Name = name;
			Files = files;
			Children = children;
		}

		public static Folder TraverseFolder(DirectoryInfo di)
		{
			var fsDirs = di.GetDirectories ();
			Folder[] subFolders = new Folder[fsDirs.Length];
			for (var i=0; i< fsDirs.Length; i++) {
				subFolders[i] = TraverseFolder (fsDirs[i]);
			}
			var fsFiles = di.GetFiles ();
			File[] files = new File[fsFiles.Length];

			for (var i=0; i< fsFiles.Length; i++) {
				files[i] = new File(fsFiles[i].Name, fsFiles[i].Length);
			}
			var folder = new Folder(di.Name, files, subFolders);
			Folders [di.FullName] = folder;
			return folder;
		}

		public static long CalculateSize(Folder folder)
		{
			long size = 0;
			foreach (var file in folder.Files) {
				size += file.Size;
			}
			foreach (var sub in folder.Children) {
				size += CalculateSize (sub);
			}
			return size;
		}
	}
}

