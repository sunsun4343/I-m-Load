using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Str
{
    static char splitWord = '=';
    static Dictionary<string, string> Dic = new Dictionary<string, string>();

    public static void Load(string filePath)
    {
        Dic.Clear();

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 0; i < lines.Length; i++)
        {
            string[] split = lines[i].Split(splitWord);
            
            if(split.Length > 1)
            {
                string key = split[0];
                string text = split[1];

                if (Dic.ContainsKey(key) == false)
                {
                    Dic.Add(key, text);
                }
            }
        }
    }


    public static string Get(string key)
    {
        string result = "";
        Dic.TryGetValue(key, out result);
        return result;
    }


}
