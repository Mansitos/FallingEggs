using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [SerializeField] GameObject terrain;
    [SerializeField] GameObject sky;
    [SerializeField] GameObject chickensManager;

    [SerializeField] Texture terrainSprite;
    [SerializeField] Texture skySprite;
    [SerializeField] Texture chickenSprite;

    void Start()
    {
        terrain.GetComponent<Renderer>().material.SetTexture("_MainTex", terrainSprite);
        sky.GetComponent<Renderer>().material.SetTexture("_MainTex", skySprite);
        chickensManager.GetComponent<ChickensManager>().setChickensSprite(chickenSprite);
    }

    void Update()
    {
    }
}
