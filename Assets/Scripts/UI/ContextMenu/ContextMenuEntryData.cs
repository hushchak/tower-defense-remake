public class ContextMenuEntryData
{
    public string Name;
    public TowerAction Action;

    public ContextMenuEntryData(string name, TowerAction action)
    {
        Name = name;
        Action = action;
    }
}
