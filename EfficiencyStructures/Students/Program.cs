using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EfficiencyStructures
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var students
				= new SortedDictionary<string, SortedSet<Student>>();
			var projectDir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			var file = new StreamReader (projectDir + Path.DirectorySeparatorChar + "students.txt");
			string line;
			while(null != (line = file.ReadLine()))
			{
				var record = line.Split ('|').Select(p => p.Trim()).ToList();
				if( !students.ContainsKey(record[2] )) {
					students[record[2]] = new SortedSet<Student>();
				}
				students [record [2]].Add(new Student (record[0], record[1], record[2]));
			}

			foreach (var course in students) {
				Console.Write ("{0}: ", course.Key);
				var i = 0;
				foreach (var student in course.Value) {
					Console.Write (student);
					if (i < course.Value.Count-1) {
						Console.Write (", ");
					}
					i++;
				}
				Console.WriteLine ();
			}
		}
	}
}
