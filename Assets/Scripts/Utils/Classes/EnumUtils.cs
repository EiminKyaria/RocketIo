namespace Com.Eimin.Personnal.Scripts.Utils.Classes
{
    /// <summary>
    /// state the game can take
    /// </summary>
    public enum GameState
    {
        init,
        mainMenu,
        fakeload,
        inGame,
        pauseMenu,
        interRound,
        gameWin,
        gameOver,
        restart
    }

    /// <summary>
    /// state of an object with life
    /// </summary>
    public enum TouchableState
    {
        normal,
        god
    }

    /// <summary>
    /// State of the state MAchine
    /// </summary>
    public enum ObjectState
    {
        Normal,
        Death,
        Void
    }

    public enum MobileType
    {
        Enemy,
        Player
    }

    public enum SaveKey
    {
        LevelNumber,
        StageNumber,
        FoeDifficulty
    }
}