using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //ƒV[ƒ“‚ª•Ï‚í‚Á‚Ä‚à”jŠü‚³‚ê‚È‚¢‚æ‚¤‚É‚·‚é
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        
    }
}
