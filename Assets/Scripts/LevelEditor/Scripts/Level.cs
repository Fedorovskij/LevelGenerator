using UnityEngine;
using System.Collections.Generic;


public class Level : MonoBehaviour
{
    public static Dictionary<int, LevelProfile> all = new Dictionary<int, LevelProfile>();

    public LevelProfile profile;

    void Awake()
    {

        if (!Application.isEditor)
        {
            Destroy(gameObject); 
        }

        for (int i = 0; i < 50; i++)
        {
            profile = new LevelProfile();

            profile.level = (i + 1);
          
            all.Add(profile.level, XmlSerializator.OpenLevel((i + 1).ToString()));
        }

    }

 
    public static void LoadLevel(int key)
    {
        if (!all.ContainsKey(key))
        {
            return;
        }

        LevelProfile.SetLevelProfile(all[key]);
    }

    public static void LoadLevelInEditor(int key)
    {
        if (!all.ContainsKey(key))
        {
            return;
        }

        LevelProfile.SetLevelProfile(all[key]);
    }

    private void OnDestroy()
    {
        all.Clear();
    }
}

// Класс информации об уровне
[System.Serializable]
public class LevelProfile
{
    public static LevelProfile LevelProfileNow { get; private set; }

    public static int[] ColorMask;

    public static void SetLevelProfile(LevelProfile profile)
    {
        LevelProfileNow = profile;

        int[] colorMask = { 0, 1, 2, 3, 4, 5 };
        int j;
        for (int i = 5; i > 0; i--)
        {
            j = Random.Range(0, i);
            colorMask[j] = colorMask[j] + colorMask[i];
            colorMask[i] = colorMask[j] - colorMask[i];
            colorMask[j] = colorMask[j] - colorMask[i];
        }

        ColorMask = colorMask;
    }

    public string nameOfLevel;

    // current level
    const int maxSize = 12;
 
    // maximal playing field size
    
    public int levelID = 0;
    // Level ID
    public int level = 0;
    // Level number
    // field size±
    public int width = 9;
    public int height = 9;
    public int chipCount = 6;
    // count of chip colors
    public int targetColorCount = 0;
    // Count of target color in Color mode
    public int targetSugarDropsCount = 0;
    // Count of sugar chips in SugaDrop mode
    public int firstStarScore = 100;
    // number of score points needed to get a first stars
    public int secondStarScore = 200;
    // number of score points needed to get a second stars
    public int thirdStarScore = 300;
    // number of score points needed to get a third stars
    public float buttonPortion = 0f;


    public FieldTarget target = FieldTarget.None;
    // Playing rules
    // Target score in Score mode = firstStarScore;
    // Count of jellies in Jelly mode colculate automaticly via jellyData array;
    // Count of blocks in Blocks mode colculate automaticly via blockData array;
    // Count of remaining chips in Color mode takes from "countOfEachTargetCount" array, where value is count, index is color ID ;

    public Limitation limitation = Limitation.Moves;
    // Session duration in time limitation mode = duration value (sec);
    // Count of moves in moves limimtation mode = moveCount value (sec);
    public int moveCount = 30;
    // Count of moves in TargetScore and JellyCrush
    public int duration = 100;

    public int[] countOfEachTargetCount = new int[6];
    // Array of counts of each color matches. Color ID is index.
    public void SetTargetCount(int index, int target)
    {
        Debug.Log("target " + target);
        countOfEachTargetCount[index] = target;
    }

    public int GetTargetCount(int index)
    {
        return countOfEachTargetCount[index];
    }

    public LevelProfile()
    {
        for (int x = 0; x < maxSize; x++)
            for (int y = 0; y < maxSize; y++)
                SetSlot(x, y, true);
        for (int x = 0; x < maxSize; x++)
            SetGenerator(x, 0, true);
        /*for (int x = 0; x < maxSize; x++)
			SetSugarDrop(x, maxSize - 1, true);*/
    }

    // Slot
    public bool[] slot = new bool[maxSize * maxSize];

    public bool GetSlot(int x, int y)
    {
        return slot[y * maxSize + x];
    }

    public void SetSlot(int x, int y, bool v)
    {
        slot[y * maxSize + x] = v;
    }

    // Gravity
    public int[] gravity = new int[maxSize * maxSize];

    public int GetGravity(int x, int y)
    {
        return gravity[y * maxSize + x];
    }

    public void SetGravity(int x, int y, int v)
    {
        gravity[y * maxSize + x] = v;
    }

    // Generators
    public bool[] generator = new bool[maxSize * maxSize];

    public bool GetGenerator(int x, int y)
    {
        return generator[y * maxSize + x];
    }

    public void SetGenerator(int x, int y, bool v)
    {
        generator[y * maxSize + x] = v;
    }

    // Teleports
    public int[] teleport = new int[maxSize * maxSize];

    public int GetTeleport(int x, int y)
    {
        return teleport[y * maxSize + x];
    }

    public void SetTeleport(int x, int y, int v)
    {
        teleport[y * maxSize + x] = v;
    }

    // Sugar Drop slots
    public bool[] sugarDrop = new bool[maxSize * maxSize];

    public bool GetSugarDrop(int x, int y)
    {
        return sugarDrop[y * maxSize + x];
    }

    public void SetSugarDrop(int x, int y, bool v)
    {
        sugarDrop[y * maxSize + x] = v;
    }

    // Chip
    public int[] chip = new int[maxSize * maxSize];

    public int GetChip(int x, int y)
    {
        return chip[y * maxSize + x];
    }

    public int GetColorWithMask(int index)
    {
        return countOfEachTargetCount[ColorMask[index]];
    }


    public void SetChip(int x, int y, int v)
    {
        chip[y * maxSize + x] = v;
    }

    // Jelly
    public int[] jelly = new int[maxSize * maxSize];

    public int GetJelly(int x, int y)
    {
        return jelly[y * maxSize + x];
    }

    public int GetCountOfAllJelly()
    {
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (GetSlot(x, y) && GetJelly(x, y) != 0)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void SetJelly(int x, int y, int v)
    {
        jelly[y * maxSize + x] = v;
    }


    // Block
    public int[] block = new int[maxSize * maxSize];

    public int GetBlock(int x, int y)
    {
        return block[y * maxSize + x];
    }

    public int GetCountOfAllBlocks()
    {
        int count = 0;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (GetSlot(x, y) && GetBlock(x, y) != 0 && GetBlock(x, y) < 4)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public void SetBlock(int x, int y, int v)
    {
        block[y * maxSize + x] = v;
    }

    // Powerup
    public int[] powerup = new int[maxSize * maxSize];

    public int GetPowerup(int x, int y)
    {
        return powerup[y * maxSize + x];
    }

    public void SetPowerup(int x, int y, int v)
    {
        powerup[y * maxSize + x] = v;
    }

    // Wall
    public bool[] wallV = new bool[maxSize * maxSize];
    public bool[] wallH = new bool[maxSize * maxSize];

    public bool GetWallV(int x, int y)
    {
        return wallV[y * maxSize + x];
    }

    public bool GetWallH(int x, int y)
    {
        return wallH[y * maxSize + x];
    }

    public void SetWallV(int x, int y, bool v)
    {
        wallV[y * maxSize + x] = v;
    }

    public void SetWallH(int x, int y, bool v)
    {
        wallH[y * maxSize + x] = v;
    }

    public LevelProfile GetClone()
    {
        LevelProfile clone = new LevelProfile();
        clone.level = level;

        clone.width = width;
        clone.height = height;
        clone.chipCount = chipCount;
        clone.targetSugarDropsCount = targetSugarDropsCount;
        clone.countOfEachTargetCount = countOfEachTargetCount;
        clone.targetColorCount = targetColorCount;

        clone.firstStarScore = firstStarScore;
        clone.secondStarScore = secondStarScore;
        clone.thirdStarScore = thirdStarScore;

        clone.target = target;
        clone.limitation = limitation;

        clone.duration = duration;
        clone.moveCount = moveCount;

        clone.slot = slot;
        clone.gravity = gravity;
        clone.generator = generator;
        clone.teleport = teleport;
        clone.sugarDrop = sugarDrop;
        clone.chip = chip;
        clone.jelly = jelly;
        clone.block = block;
        clone.powerup = powerup;
        clone.wallV = wallV;
        clone.wallH = wallH;

        clone.buttonPortion = buttonPortion;

        return clone;
    }
}

public enum FieldTarget
{
    None = 0,
    Jelly = 1,
    Block = 2,
    Color = 3,
    SugarDrop = 4
}

public enum Limitation
{
    Moves,
    Time
}