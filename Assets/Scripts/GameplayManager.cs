using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    public bool IsPlaying { get; set; }

    // Falling into wolf trap
    public bool IsFallIntoTheTrap { get; set; }

    public static GameplayManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        IsPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayStart()
    {
        IsPlaying = true;
    }
}
