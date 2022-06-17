using System;

[Serializable]
public class LevelData
{
    public DifficultyLevel stateDifficultyLevel;
    public ColorLevel stateColorLevel;
    public bool stateWhiteGem;
    public bool stateDoorStone;
    public bool stateEndLevel;
    public bool stateChest;

    public enum ColorLevel
    {
      Green,
      Pink,
      Blue,
      Red
    }
    
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
}