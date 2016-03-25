using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> personsByEmail;
    private Dictionary<string, SortedSet<Person>> personsByDomain;
    private OrderedMultiDictionary<int, Person> personsByAge;
    private Dictionary<string, OrderedMultiDictionary<int, Person>> personsByTownAge;
    private Dictionary<string, SortedSet<Person>> personsByNameTown;

    public PersonCollection()
    {
        personsByEmail = new Dictionary<string, Person>(1000);
        personsByDomain = new Dictionary<string, SortedSet<Person>>(1000);
        personsByAge = new OrderedMultiDictionary<int, Person>(true);
        personsByTownAge = new Dictionary<string, OrderedMultiDictionary<int, Person>>(1000);
        personsByNameTown = new Dictionary<string, SortedSet<Person>>(1000);
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (personsByEmail.ContainsKey(email))
        {
            return false;
        }
        if (!personsByTownAge.ContainsKey(town))
        {
            personsByTownAge[town] = new OrderedMultiDictionary<int, Person>(true);
        }

        var person = new Person(email, name, age, town);
        personsByEmail[email] = person;
        personsByAge[age].Add(person);
        personsByDomain[GetDomain(email)].Add(person);
        personsByTownAge[town][age].Add(person);
        personsByNameTown[name+'|'+town].Add(person);

        return true;
    }

    public int Count
    {
        get
        {
            return personsByEmail.Count;
        }
    }

    public Person FindPerson(string email)
    {
        try {
            return personsByEmail[email];
        } catch(KeyNotFoundException)
        {
            return null;
        }
    }

    public bool DeletePerson(string email)
    {
        if (personsByEmail.ContainsKey(email))
        {
            return false;
        }
        var person = personsByEmail[email];

        personsByEmail.Remove(email);
        personsByAge.Remove(person.Age);
        personsByDomain[GetDomain(person.Email)].Remove(person);
        personsByTownAge[person.Town][person.Age].Remove(person);
        personsByNameTown[person.Name+'|'+person.Town].Remove(person);

        if (0 == personsByAge[person.Age].Count)
        {
            personsByAge.Remove(person.Age);
        }
        if (0 == personsByDomain[GetDomain(person.Email)].Count)
        {
            personsByDomain.Remove(GetDomain(person.Email));
        }
        if (0 == personsByTownAge[person.Town][person.Age].Count)
        {
            personsByTownAge[person.Town].Remove(person.Age);
        }
        if (0 == personsByTownAge[person.Town].Count)
        {
            personsByTownAge.Remove(person.Town);
        }
        if (0 == personsByNameTown[person.Name].Count)
        {
            personsByNameTown.Remove(person.Name);
        }
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return personsByDomain[emailDomain];
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        try {
            return personsByNameTown[name+'|'+town];
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return (IEnumerable<Person>) personsByAge.Range(startAge, true, endAge, true);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        return (IEnumerable<Person>) personsByTownAge[town].Range(startAge, true, endAge, true);
    }

    public static string GetDomain(string email)
    {
        return email.Substring(email.IndexOf('@') + 1);
    }
}
