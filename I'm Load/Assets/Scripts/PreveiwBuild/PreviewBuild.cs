using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewBuild : MonoBehaviour
{
    [SerializeField] GamePlayData gamePlayData;
    [SerializeField] GameObject ControlMenuObj;
    [SerializeField] Transform CameraTransform;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer GuideSpriteRenderer;
    [SerializeField] Color BuildAbleColor;
    [SerializeField] Color BuildUnableColor;
    [SerializeField] Button Button_Build;

    RectTransform ControlMenuRectTransform;

    BuildingDB db;
    int rotate;
    Vector2Int size;
    Vector2Int buildPosition;
    bool IsBuildAble;

    private void Awake()
    {
        ControlMenuRectTransform = ControlMenuObj.GetComponent<RectTransform>();

    }

    void Start()
    {
        
    }

    void Update()
    {
        //Calc Sprite Position
        Vector3 CamPos = CameraTransform.position;
        Vector3 position = Vector3.zero;
        position.x = Mathf.FloorToInt(CamPos.x);
        position.y = Mathf.FloorToInt(CamPos.y);
        if (size.x % 2 == 1) position.x += 0.5f;
        if (size.y % 2 == 1) position.y += 0.5f;
        this.transform.position = position;

        //Calc BuildPosition
        Vector2Int positionInt = Vector2Int.zero;
        positionInt.x = (int)position.x;
        positionInt.y = (int)position.y;
        Vector2Int offsetSize = db.offsetSize;
        if (rotate == 1 || rotate == 3)
            offsetSize = new Vector2Int(offsetSize.y, offsetSize.x);
        buildPosition = positionInt - offsetSize;

        //Calc Build Able
        IsBuildAble = true;
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if (gamePlayData.map_Able_Build[buildPosition.x + x, buildPosition.y + y] == false)
                {
                    IsBuildAble = false;
                    break;
                }
            }
            if (IsBuildAble == false) break;
        }
        if (IsBuildAble)
        {
            GuideSpriteRenderer.color = BuildAbleColor;
            Button_Build.interactable = true;
        }
        else
        {
            GuideSpriteRenderer.color = BuildUnableColor;
            Button_Build.interactable = false;
        }

        //Calc ControllMenu Position
        Vector3 controlPosition = position + new Vector3(0, size.y * 0.5f, 0);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(controlPosition);
        ControlMenuRectTransform.anchoredPosition = screenPosition;
    }

    public void ActiveBuilding(BuildingDB DB)
    {
        this.db = DB;

        this.gameObject.SetActive(true);
        ControlMenuObj.SetActive(true);

        rotate = 0;
        size = db.size;
        spriteRenderer.sprite = DB.PreviewSprites[rotate];
        GuideSpriteRenderer.size = size;
    }

    public void Build()
    {
        if (IsBuildAble == false) return;

        gamePlayData.CreateBuilding(db, buildPosition, rotate);

        ControlMenuObj.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void Rotate()
    {
        rotate++;
        if (rotate >= 4) rotate = 0;
        spriteRenderer.sprite = db.PreviewSprites[rotate];

        size = db.size;
        if (rotate == 1 || rotate == 3)
            size = new Vector2Int(db.size.y, db.size.x);
        GuideSpriteRenderer.size = size;
    }

    public void Cancel()
    {
        ControlMenuObj.SetActive(false);
        this.gameObject.SetActive(false);
    }

}
