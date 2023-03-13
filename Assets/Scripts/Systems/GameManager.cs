public static class GameManager
{
    /// <summary>
    /// Current player of this instance of game.
    /// </summary>
    public static Player player
    {
        // Default retrieval call
        get
        {
            if (_player == null)
            {
                StorageHandler storageHandler = new StorageHandler();
                _player = (Player)storageHandler.LoadData("player");
                if (_player == null)
                {
                    _player = new Player();
                }
            }
            return _player;
        }
        // Default defining call
        set
        {
            _player = value;
        }
    }
    private static Player _player;
    /// <summary>
    /// Save Player data to the file system
    /// </summary>
    private static void Save()
    {
        StorageHandler storageHandler = new StorageHandler();
        storageHandler.SaveData(player, "player");
    }
}