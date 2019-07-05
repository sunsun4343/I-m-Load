using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 해당 컨포넌트가 설정된 게임오브젝트의 Active가 false일때
/// 정상적으로 동작하지 않으므로, RequireAwakeManager에 등록 필요함.
/// 관련 관리를 위한 그룹핑
/// </summary>
public class RequireAwakeBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        RequireAwakeManager.Instance.AddGameObject(this.gameObject);
    }
}


