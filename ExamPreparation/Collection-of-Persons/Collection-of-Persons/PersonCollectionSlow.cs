using System;
using System.Collections.Generic;

public class PersonCollectionSlow : IPersonCollection
{
    List<Person> Persons = new List<Person>();

    public bool AddPerson(string email, string name, int age, string town)
    {
        if( null != FindPerson(email) )
        {
            return false;
        }

        Persons.Add(new Person(email, name, age, town));
        Persons.Sort();
        return true;
    }

    public int Count
    {
        get
        {
            return Persons.Count;
        }
    }

    public Person FindPerson(string email)
    {
        foreach( var person in Persons)
        {
            if( person.Email == email )
            {
                return person;
            }
        }
        return null;
    }

    public bool DeletePerson(string email)
    {
        var person = FindPerson(email);
        return Persons.Remove(person);
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        var result = new List<Person>();
        foreach (var person in Persons)
        {
            if (PersonCollection.GetDomain( person.Email ) == emailDomain)
            {
                result.Add(person);
            }
        }
        return result;
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var result = new List<Person>();
        foreach (var person in Persons)
        {
            if (person.Name == name && person.Town == town)
            {
                result.Add(person);
            }
        }
        return result;
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var result = new List<Person>();
        foreach (var person in Persons)
        {
            if( person.Age >= startAge && person.Age <= endAge )
            {
                result.Add(person);
            }
        }
        return result;

    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        var result = new List<Person>();
        foreach (var person in Persons)
        {
            if (person.Town == town && person.Age >= startAge && person.Age <= endAge)
            {
                result.Add(person);
            }
        }
        return result;
    }
}
