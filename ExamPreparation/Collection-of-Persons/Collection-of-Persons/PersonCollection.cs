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
        personsByEmail = new Dictionary<string, Person>();
        personsByDomain = new Dictionary<string, SortedSet<Person>>();
        personsByAge = new OrderedMultiDictionary<int, Person>(true);
        personsByTownAge = new Dictionary<string, OrderedMultiDictionary<int, Person>>();
        personsByNameTown = new Dictionary<string, SortedSet<Person>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (personsByEmail.ContainsKey(email))
        {
            return false;
        }
        var domain = GetDomain(email);
        if (!personsByDomain.ContainsKey(domain))
        {
            personsByDomain[domain] = new SortedSet<Person>();
        }
        if (!personsByTownAge.ContainsKey(town))
        {
            personsByTownAge[town] = new OrderedMultiDictionary<int, Person>(true);
        }
        var nameTown = name + '|' + town;
        if (!personsByNameTown.ContainsKey(nameTown))
        {
            personsByNameTown[nameTown] = new SortedSet<Person>();
        }

        var person = new Person(email, name, age, town);
        personsByEmail[email] = person;
        personsByAge[age].Add(person);
        personsByDomain[domain].Add(person);
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
        if(!personsByEmail.ContainsKey(email) )
        {
            return null;
        }
        return personsByEmail[email];
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
        if( !personsByDomain.ContainsKey(emailDomain))
        {
            return new List<Person>();
        }
        return personsByDomain[emailDomain];
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var nameTown = name + '|' + town;
        if (!personsByNameTown.ContainsKey(nameTown))
        {
            return new List<Person>();
        }
        return personsByNameTown[nameTown];
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var range = personsByAge.Range(startAge, true, endAge, true);
        foreach ( var items in range )
        {
            foreach (var item in items.Value)
            {
                yield return item;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if( !personsByTownAge.ContainsKey(town))
        {
            yield break;
        }
        var range = personsByTownAge[town].Range(startAge, true, endAge, true);
        foreach (var items in range)
        {
            foreach( var item in items.Value )
            {
                yield return item;
            }
        }
    }

    public static string GetDomain(string email)
    {
        return email.Substring(email.IndexOf('@') + 1);
    }
}
