using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class XmlSerializator
{
    private static string _levelPath = Application.streamingAssetsPath + "/Levels/";

    private static FileStream fg = null;
 
    private static XmlSerializer formatter = new XmlSerializer(typeof(LevelProfile));

    public static LevelProfile OpenLevel(string nameLevel)
    {
        LevelProfile _level = null;

        string levelText;

        if (_levelPath.Contains("://"))
        {
            WWW www = new WWW(_levelPath + nameLevel + ".xml");

            while (!www.isDone)
            {
            }

            levelText = www.text;
        }

        // --- Для РС IOS и тд грузим так ---//
        else
        {
            #if UNITY_IOS && !UNITY_EDITOR
                _levelPath = Application.persistentDataPath + "/Levels/";
            #endif

            levelText = File.ReadAllText(_levelPath + nameLevel + ".xml");
        }

        XmlSerializer serializer = new XmlSerializer(typeof(LevelProfile));
 
        _level = serializer.Deserialize(new StringReader(levelText)) as LevelProfile;

        return _level;
    }

    public static void SaveLevel(LevelProfile prof)
    {
        Debug.Log(_levelPath + prof.nameOfLevel);

        #if UNITY_IOS && !UNITY_EDITOR
             _levelPath = Application.persistentDataPath + "/Levels/";
        #endif

        using (FileStream fg = new FileStream(string.Format(_levelPath + prof.nameOfLevel + ".xml"), FileMode.Create))
        {           
            formatter.Serialize(fg, prof);
        }
    }

}
