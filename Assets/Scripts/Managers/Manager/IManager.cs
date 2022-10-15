namespace Game.General.Managers
{
    public interface IManager
    {
        private static IManager instance;
        public static IManager Instance => instance;
        
    }
}