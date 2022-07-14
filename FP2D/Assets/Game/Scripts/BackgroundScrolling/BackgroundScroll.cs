using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] Vector2 ParallaxEffect;
    private Transform cameraTranform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;

    private void Start()
    {
        cameraTranform = Camera.main.transform;
        lastCameraPosition = cameraTranform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    }

    private void Update()
    {
        ScrollBackground();
    }

    public void ScrollBackground()
    {
        Vector3 deltaMovement = cameraTranform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * ParallaxEffect.x, deltaMovement.y * ParallaxEffect.y);
        lastCameraPosition = cameraTranform.position;

        if(Mathf.Abs(cameraTranform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            float offsetPositionX = (cameraTranform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTranform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
