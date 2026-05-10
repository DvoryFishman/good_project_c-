using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using DO;
using DalApi; // או המקום שבו נמצא Config.cs
namespace Dal;

internal class ProductImplementation : DalApi.IProduct
{
    private readonly string filePath = @"..\xml\products.xml";

    // --- פונקציית עזר פנימית לטעינה (Deserialize) ---
    private List<Product> Load()
    {
        if (!File.Exists(filePath)) return new List<Product>();

        XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
        using (FileStream stream = new FileStream(filePath, FileMode.Open))
        {
            return (List<Product>)serializer.Deserialize(stream);
        }
    }

    // --- פונקציית עזר פנימית לשמירה (Serialize) ---
    private void Save(List<Product> list)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Product>));
        using (FileStream stream = new FileStream(filePath, FileMode.Create))
        {
            serializer.Serialize(stream, list);
        }
    }

    // 1. הוספה (Create)
    public int Create(Product item)
    {
        List<Product> products = Load();

        // קבלת מספר רץ מהקובץ data-config דרך ה-Config
        int nextId = Config.NextProductNum;

        // יצירת אובייקט חדש עם ה-ID שנוצר
        Product newProduct = item with { ProductId = nextId };

        products.Add(newProduct);
        Save(products);

        return nextId;
    }
    public Product? Read()
    {
        List<Product> products = Load();
        return products.FirstOrDefault();
    }
    // דוגמה עבור Product
    public Product? Read(int id)
    {
        return Read(p => p.ProductId == id);
    }

    // 2. שליפה (Read/Get)
    public Product Read(Func<Product, bool> filter)
    {
        List<Product> products = Load();
        return products.FirstOrDefault(filter)
               ?? throw new Exception("Product not found");
    }

    // 3. שליפת כל הרשימה (ReadAll)
    public List<Product> ReadAll(Func<Product, bool>? filter = null)
    {
        List<Product> products = Load();
        if (filter == null) return products;
        return products.Where(filter).ToList(); // הוספת .ToList() בסוף
    }

    // 4. עדכון (Update)
    public void Update(Product item)
    {
        List<Product> products = Load();

        int index = products.FindIndex(p => p.ProductId == item.ProductId);
        if (index == -1) throw new Exception("Product to update was not found");

        products[index] = item; // עדכון האיבר ברשימה הלוגית

        Save(products); // שמירה חזרה לקובץ
    }

    // 5. מחיקה (Delete)
    public void Delete(int id)
    {
        List<Product> products = Load();

        int removedCount = products.RemoveAll(p => p.ProductId == id);
        if (removedCount == 0) throw new Exception("Product to delete was not found");

        Save(products);
    }
}