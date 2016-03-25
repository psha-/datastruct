using System;

public class Person : IComparable<Person>
{
    public string Email { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Town { get; set; }

    public Person(string email, string name, int age, string town)
    {
        Email = email;
        Name = name;
        Age = age;
        Town = town;
    }

    public int CompareTo(Person other)
    {
        return Email.CompareTo(other.Email);
    }

    public override string ToString()
    {
        return string.Format("Email:{0}, Name:{1}, Age:{2}, Town:{3}", Email, Name, Age, Town);
    }
}
