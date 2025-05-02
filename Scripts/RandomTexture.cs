using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTexture : MonoBehaviour
{
    [SerializeField] private Texture2DArray textureArray;
    void Awake()
    {
        Texture2D texture = new Texture2D(textureArray.width, textureArray.height, textureArray.format, false);
        Graphics.CopyTexture(textureArray, Random.Range(0, textureArray.depth), 0, texture, 0, 0);
        GetComponent<Renderer>().material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
