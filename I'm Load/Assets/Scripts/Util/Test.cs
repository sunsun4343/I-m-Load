using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public long count;

    [ContextMenu("Test")]
    public void TestFunction()
    {
        string str = Util.CountToString(count, 5);

        Debug.Log(str);
    }

}
