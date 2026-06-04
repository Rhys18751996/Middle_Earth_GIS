namespace Fantasy_World_GIS.GIS.Serialization
{
    public interface IGisSerializer<T>
    {
        string Serialize(T item);

        T Deserialize(string data);
    }
}