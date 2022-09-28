using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour
{
    public int level;
    LevelAtamaPaneli levelAtamaPaneli;
    void Start()
    {
        levelAtamaPaneli = FindObjectOfType<LevelAtamaPaneli>();
    }
    public void Level()
    {
        levelAtamaPaneli.chosenLevel = level;
    }
    
}
