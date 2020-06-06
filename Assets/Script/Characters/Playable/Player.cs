public class Player : Character
{
    private void Awake()
    {
        InitializeControllerPack<PlayerControllerPack>();
    }
}