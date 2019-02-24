using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
	public GameObject soundManager;
	public AudioClip backgroundMusic;

    void Start()
    {
		soundManager.GetComponent<SoundManager>().PlayBGM(backgroundMusic);
    }
}
