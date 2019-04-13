using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBuild : MonoBehaviour
{
    [SerializeField] GameObject ControlMenuObj;
    [SerializeField] Transform CameraTransform;
    [SerializeField] SpriteRenderer spriteRenderer;

    RectTransform ControlMenuRectTransform;

    Building db;
    int rotate;


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
        this.transform.position = position;

        //ControllMenu Position
        Vector3 controlPosition = position + new Vector3(0, spriteRenderer.size.y * 0.5f, 0);
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
