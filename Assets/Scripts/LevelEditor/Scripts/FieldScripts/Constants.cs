using UnityEngine;

public static class Constants
{
    public const int MaxValueOfLifes = 5;
    public const int StartLivesCount = 6;
    public const int StartCoinsCount = 1000;
    public const int ExtraTimeBooster = 15;
    public const int ExtraMovesBooster = 5;

    public const int MaxWidthOfField = 12;
    public const int MaxHeightOfField = 12;

    public const int DefaultWidthOfField = 9;
    public const int DefaultHeightOfField = 9;
    public const int MinWidthOfField = 5;
    public const int MinHeightOfField = 5;
    public const int MaxCountOfColors = 6;
    public const int MinCountOfColors = 3;

    public const int MaxCountOfMoves = 50;
    public const int MinCountOfMoves = 5;
    public const int MaxCountOfSugarDrop = 20;
    public const int MinCountOfSugarDrop = 1;
    public const int MaxCountOfTime = 180;
    public const int MinCountOfTime = 15;

    public const int MaxCountTargetColor = 6;
    public const int MinCountTargetColor = 1;

    public const int MinFirstScore = 100;
    public const  int MinSecondScore = 105;
    public const int MinThirdScore = 150;
    public const int MaxCountOfOneTargetColor = 1000;
    public const int MinCountOfOneTargetColor = 1;
    public const float MaxCountPortion = 0.7f;
    public const int MinCountPortion = 0;

    public static readonly Color ColorA = new Color(1, 0.6f, 0.6f, 1);
    public static readonly Color ColorB = new Color(0.6f, 1, 0.6f, 1);
    public static readonly Color ColorC = new Color(0.6f, 0.8f, 1, 1);
    public static readonly Color ColorD = new Color(1, 1, 0.6f, 1);
    public static readonly Color ColorE = new Color(1, 0.6f, 1, 1);
    public static readonly Color ColorF = new Color(1, 0.8f, 0.6f, 1);
    public static readonly Color ColorRandom = Color.white;
    public static readonly Color ColorButton = Color.grey;
    public static readonly Color[] _colors = new Color[10];
    public static readonly Color ColorWalls = new Color(0, 1, 1, 1);

    static Constants()
    {
        _colors[0] = ColorRandom;
        _colors[1] = ColorA;
        _colors[2] = ColorB;
        _colors[3] = ColorC;
        _colors[4] = ColorD;
        _colors[5] = ColorE;
        _colors[6] = ColorF;
        _colors[9] = ColorButton;
    }

    public static Color GetColorByEnum(eColors color)
    {
        if (GetIdByEnum(color) == -1)
        {
            return _colors[0];
        }

        return _colors[GetIdByEnum(color)];
    }

    public static int GetIdByEnum(eColors color)
    {
        return (int)color;
    }

    public static eColors GetEnumById(int color)
    {
        return (eColors)color;
    }
}

public enum eColors
{
    ColorRandom = 0,
    ColorA = 1,
    ColorB = 2,
    ColorC = 3,
    ColorD = 4,
    ColorE = 5,
    ColorF = 6,
    ColorButton = 9
}

public enum eTypesOfBoosters
{
    None,
    ExtraMoves,
    ExtraTime,
    Spoon,
    MagicFinger,
    Bombs,
    Shaker,
    Hand,
    Rainbow,
    Ladybird
}
