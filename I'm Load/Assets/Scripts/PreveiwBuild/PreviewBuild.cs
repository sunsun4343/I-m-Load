using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuild : MonoBehaviour
{
    [SerializeField] GamePlayData gamePlayData;
    [SerializeField] GameObject ControlMenuObj;
    [SerializeField] Transform CameraTransform;
    [SerializeField] SpriteRenderer spriteRenderer;

    RectTransform ControlMenuRectTransform;

    Building db;
    int rotate;
    Vector2Int buildPosition;


    private void Awake()
    {
        ControlMenuRectTransform = ControlMenuObj.GetComponent<RectTransform>();

    }

    void Start()
    {
        
    }

    void Update()
    {
        //Sprite Position
        Vector3 CamPos = CameraTransform.position;
        Vector3 position = Vector3.zero;
        position.x = Mathf.FloorToInt(CamPos.x);
        position.y = Mathf.FloorToInt(CamPos.y);
        Vector2Int size = db.size;
        if (rotate == 1 || rotate == 3)
            size = new Vector2Int(db.size.y, db.size.x);
        if (size.x % 2 == 1) position.x += 0.5f;
        if (size.y % 2 == 1) position.y += 0.5f;
        this.transform.position = position;

        Vector2Int positionInt = Vector2Int.zero;
        positionInt.x = (int)position.x;
        positionInt.y = (int)position.y;
        Vector2Int offsetSize = db.offsetSize;
        if (rotate == 1 || rotate == 3)
            offsetSize = new Vector2Int(offsetSize.y, offsetSize.x);
        buildPosition = positionInt - offsetSize;

        //ControllMenu Position
        Vector3 controlPosition = position;
        if (rotate == 0 || rotate == 2) controlPosition += new Vector3(0, db.size.y * 0.5f, 0);
        else if (rotate == 1 || rotate == 3) controlPosition += new Vector3(0, db.size.x * 0.5f, 0);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(controlPosition);
        ControlMenuRectTransform.anchoredPosition = screenPosition;
    }

    public void ActiveBuilding(Building DB)
    {
        this.db = DB;

        this.gameObject.SetActive(true);
        ControlMenuObj.SetActive(true);

        rotate = 0;
        spriteRenderer.sprite = DB.PreviewSprites[rotate];

    }

    public void Build()
    {
        gamePlayData.CreateBuilding(db, buildPosition, rotate);

        ControlMenuObj.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void Rotate()
    {
        rotate++;
        if (rotate >= 4) rotate = 0;
        spriteRenderer.sprite = db.PreviewSprites[rotate];
    }

    public void Cancel()
    {
        ControlMenuObj.SetActive(false);
        this.gameObject.SetActive(false);
    }

}
