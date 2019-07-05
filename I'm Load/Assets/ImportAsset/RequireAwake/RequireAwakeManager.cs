using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 씬 시작시 Awake가 호출 되기 위해 
/// GameObject의 Active가 False 상태인 오브젝트를 
/// 시작 시 Active 전환
/// </summary>
public class RequireAwakeManager : SingletonBehaviour<RequireAwakeManager>
{
    [SerializeField] List<GameObject> gameObjects = new List<GameObject>();

    private void Awake()
    {
        List<GameObject> FalseTargets = new List<GameObject>();

        foreach (var item in gameObjects)
        {
            if (item == null) continue;
                
            if (item.activeSelf == false)
            {
                item.SetActive(true);
                FalseTargets.Add(item);
            }
        }

        foreach (var item in FalseTargets)
        {
            item.SetActive(false);
        }

    }

    public void AddGameObject(GameObject gameObject)
    {
        foreach (var item in gameObjects)
        {
            if (item == null) continue;
            if (item.Equals(gameObject))
            {
                return;
            }
        }

        gameObjects.Add(gameObject);
    }

    /// <summary>
    /// 목록에서 DelegateBehaviour를 상속받지 않은 객체를 제거합니다.
    /// </summary>
    [ContextMenu("Refresh")]
    public void Refresh()
    {
        for (int i = gameObjects.Count - 1; i >= 0; i--)
        {
            if (gameObjects[i] == null)
            {
                gameObjects.RemoveAt(i);
            }
            else
            {
                var component = gameObjects[i].GetComponent<RequireAwakeBehaviour>();
                if (component == null)
                {
                    gameObjects.RemoveAt(i);
                }
            }
        }
    }

    [ContextMenu("AutoTrace")]
    public void AutoTrace()
    {
        gameObjects.Clear();

        var components = Resources.FindObjectsOfTypeAll<RequireAwakeBehaviour>();
        foreach (var item in components)
        {
            Transform parent = item.transform.parent;
            while (parent != null)
            {
                if(parent.gameObject.activeSelf == false)
                {
                    AddGameObject(parent.gameObject);
                }

                parent = parent.transform.parent;
            }
                                
            AddGameObject(item.gameObject);
        }
    }

}

