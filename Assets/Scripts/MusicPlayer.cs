using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
void Awake() 
{
    int NumMusicPlayers = FindObjectsOfType<MusicPlayer>().Length;
    if (NumMusicPlayers>1) 
    {
        Destroy(gameObject);//destroy this new music player if one exists already 
    }  
    else
    {
        DontDestroyOnLoad(gameObject); // don't destroy music player on reload of level
    } 
}
}
