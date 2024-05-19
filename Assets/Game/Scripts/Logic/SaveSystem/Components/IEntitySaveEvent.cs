namespace SaveSystem.Components
{
    public interface IEntitySaveEvent<TData>
    {
        TData Data { get; set; }
        bool IsDone { get; set; }
    }
}