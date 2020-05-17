public class Player : Character
{
    private void Awake()
    {
        InitializeControllerSet<PlayerControllerSet>();
    }
}