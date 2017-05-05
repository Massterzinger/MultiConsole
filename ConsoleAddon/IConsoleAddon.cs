namespace ConsoleAddon
{
    public delegate void ConsoleFunc();

    public interface IConsoleAddon
    {
        string AddonName { get; }
        void Execute();
    }
}
