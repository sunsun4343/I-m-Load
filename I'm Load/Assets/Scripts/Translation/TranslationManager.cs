using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationManager : SingletonBehaviour<TranslationManager>
{
    public Action OnLoadedTranslation;

    private void Awake()
    {
        string filePath = Application.streamingAssetsPath + "/Translation/Korean/Translation.txt";
        Str.Load(filePath);

        if (OnLoadedTranslation != null)
        {
            OnLoadedTranslation.Invoke();
        }
    }



}
