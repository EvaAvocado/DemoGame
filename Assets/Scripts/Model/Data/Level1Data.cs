using System;

[Serializable]
public class Level1Data
{
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
}