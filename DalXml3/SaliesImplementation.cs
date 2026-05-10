using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using DO;

namespace Dal;

internal class SaliesImplementation : DalApi.ISalies
{
    private readonly string filePath = @"..\xml\salies.xml";

    // --- פונקציית עזר פנימית לטעינה (Deserialize) ---
    private List<Salies> Load()
    {
        if (!File.Exists(filePath)) return new List<Salies>();

        XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            return (List<Salies>)serializer.Deserialize(stream);
        }
    }

    // --- פונקציית עזר פנימית לשמירה (Serialize) ---
    private void Save(List<Salies> list)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Salies>));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, list);
        }
    }

    // 1. הוספה (Create)
    public int Create(Salies item)
    {
        List<Salies> salies = Load();

        // קבלת מספר רץ מהקובץ data-config דרך ה-Config
        int nextId = Config.NextSaliesNum;

        // יצירת אובייקט חדש עם ה-ID שנוצר
        Salies newSalies = item with { SaliesId = nextId };

        salies.Add(newSalies);
        Save(salies);

        return nextId;
    }

    // 2. שליפה (Read/Get)
    public Salies Read(Func<Salies, bool> filter)
    {
        List<Salies> salies = Load();
        return salies.FirstOrDefault(filter)
               ?? throw new Exception("Salies not found");
    }

    // 3. שליפת כל הרשימה (ReadAll)
    public IEnumerable<Salies> ReadAll(Func<Salies, bool>? filter = null)
    {
        List<Salies> salies = Load();
        if (filter == null) return salies;
        return salies.Where(filter);
    }

    // 4. עדכון (Update)
    public void Update(Salies item)
    {
        List<Salies> salies = Load();

        int index = salies.FindIndex(p => p.SaliesId == item.SaliesId);
        if (index == -1) throw new Exception("Salies to update was not found");

        salies[index] = item; // עדכון האיבר ברשימה הלוגית

        Save(salies); // שמירה חזרה לקובץ
    }

    // 5. מחיקה (Delete)
    public void Delete(int id)
    {
        List<Salies> salies = Load();

        int removedCount = salies.RemoveAll(p => p.SaliesId == id);
        if (removedCount == 0) throw new Exception("Salies to delete was not found");

        Save(salies);
    }
}