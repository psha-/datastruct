using System;
using System.Collections;

namespace EfficiencyStructures
{
	public class Student: IComparable
	{
		public string Course { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }

		public Student (string firstName, string lastName, string course)
		{
			FirstName = firstName;
			LastName = lastName;
			Course = course;
		}

		public int CompareTo( object other )
		{
			Student otherStudent = other as Student;
			var lastNameCmp = this.LastName.CompareTo (otherStudent.LastName);
			if (0 == lastNameCmp) {
				return this.FirstName.CompareTo (otherStudent.FirstName);
			}
			return lastNameCmp;
		}

		public override string ToString ()
		{
			return string.Format ("{0} {1}", FirstName, LastName);
		}
	}

}
