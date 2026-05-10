namespace BL.BlApi;

public interface IBl<T>
{
    T Create(T item);
    T? Read(Func<T, bool> filter);
    List<T> ReadAll(Func<T, bool>? filter = null);
    void Update(T item);
    void Delete(int id);
}

public interface IBlManager
{
    IProduct Product { get; }
    ICustomer Customer { get; }
    ISalies Salies { get; }
}