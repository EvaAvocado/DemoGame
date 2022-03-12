using System;

[Serializable]
public class Level1Data
{
    public ColorLevel stateColorLevel;
    public bool stateWhiteGem;
    public bool stateDoorStone;
    public bool stateGreyStone;

    public enum ColorLevel
    {
      Green,
      Pink,
      Blue,
      Red
    }
}