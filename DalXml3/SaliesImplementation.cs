using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using DO;
using DalApi;
using Dal;

namespace Dal;

internal class SaliesImplementation : DalApi.ISalies
{
    private readonly string filePath = @"..\xml\salies.xml";

    private List<Salies> Load()
    {
        if (!File.Exists(filePath)) return new List<Salies>();
        XmlSerializer serializer = new XmlSerializer(typeof(List<Salies>));
        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            return (List<Salies>)serializer.Deserialize(stream)!;
        }
    }

    private void Save(List<Salies> list)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Salies>));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, list);
        }
    }

    public int Create(Salies item)
    {
        List<Salies> salies = Load();
        int nextId = Config.NextSaleNum;
        Salies newSalies = item with { Id = nextId };
        salies.Add(newSalies);
        Save(salies);
        return nextId;
    }

    // parameterless Read per ICrud<T>
    public Salies? Read()
    {
        List<Salies> salies = Load();
        var found = salies.FirstOrDefault();
        return found ?? throw new DO.IdNotFoundException("Salies not found");
    }

    // Read with nullable filter per ICrud<T>
    public Salies? Read(Func<Salies, bool>? filter)
    {
        List<Salies> salies = Load();
        var found = filter == null ? salies.FirstOrDefault() : salies.FirstOrDefault(filter);
        return found ?? throw new DO.IdNotFoundException("Salies not found");
    }

    public List<Salies> ReadAll(Func<Salies, bool>? filter = null)
    {
        List<Salies> salies = Load();
        if (filter == null) return salies;
        return salies.Where(filter).ToList();
    }

    public void Update(Salies item)
    {
        List<Salies> salies = Load();
        int index = salies.FindIndex(p => p.Id == item.Id);
        if (index == -1) throw new DO.IdNotFoundException("Salies to update was not found");
        salies[index] = item;
        Save(salies);
    }

    public void Delete(int id)
    {
        List<Salies> salies = Load();
        int removedCount = salies.RemoveAll(p => p.Id == id);
        if (removedCount == 0) throw new DO.IdNotFoundException("Salies to delete was not found");
        Save(salies);
    }
}