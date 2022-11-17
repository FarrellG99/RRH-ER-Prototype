using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowEffect : MonoBehaviour
{

    public Vector2 ShadowOffset;
    public Material ShadowMaterial;

    SpriteRenderer spriteRenderer;
    [SerializeField]GameObject gameObjectParent;
    GameObject shadowGameobject;
    
    private Vector3 lastCameraPosition;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //create a new gameobject to be used as drop shadow
        shadowGameobject = new GameObject("Shadow");
        shadowGameobject.transform.parent = gameObject.transform;

        //create a new SpriteRenderer for Shadow gameobject
        SpriteRenderer shadowSpriteRenderer = shadowGameobject.AddComponent<SpriteRenderer>();
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;

        //set the shadow gameobject's sprite to the original sprite
        shadowSpriteRenderer.sprite = sprite;
        shadowSpriteRenderer.drawMode = SpriteDrawMode.Tiled;   
        shadowSpriteRenderer.size = new Vector2(70f, 10.8f);
        Debug.Log(shadowSpriteRenderer.size);
        //set the shadow gameobject's material to the shadow material we created
        shadowSpriteRenderer.material = ShadowMaterial;

        //update the sorting layer of the shadow to always lie behind the sprite
        shadowSpriteRenderer.sortingLayerName = spriteRenderer.sortingLayerName;
        shadowSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
    }

    void LateUpdate()
    {
        //update the position and rotation of the sprite's shadow with moving sprite
        shadowGameobject.transform.localPosition = (Vector3)ShadowOffset;
        shadowGameobject.transform.localRotation = transform.localRotation;
    }
}
