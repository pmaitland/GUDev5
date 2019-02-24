using UnityEngine;
using System.Collections;
using System.Collections.Generic;

    public class SoundManager : MonoBehaviour 
    {
        private const int numChannel = 4;
        private List<AudioSource> audioSources;                    
        public static SoundManager instance = null;    

        // public float lowPitchRange = .95f;
        // public float highPitchRange = 1.05f;
        
        
        void Awake ()
        {
            if (instance == null)
            {
                instance = this;
                audioSources = new List<AudioSource>(GetComponentsInChildren<AudioSource>());
                while(audioSources.Count < numChannel){
                    audioSources.Add(gameObject.AddComponent(typeof(AudioSource)) as AudioSource);
                }
            }
            else if (instance != this)
                Destroy (gameObject);

            DontDestroyOnLoad (gameObject);
        }
        public void PlayFX(AudioClip clip)
        {
            AudioSource player = GetFreePlayer();
            player.loop = false;
            player.clip = clip;
            player.Play ();
        }
        public void PlayBGM(AudioClip clip){
            AudioSource player = GetFreePlayer();
            player.loop = true;
            player.clip = clip;
            player.Play ();
        }
        public void StopAllBGM(){
            foreach(AudioSource player in audioSources){
                if(player.isPlaying && player.loop)
                    player.Stop();
            }
        }
        private AudioSource GetFreePlayer(){
            foreach(AudioSource player in audioSources){
                if(player.isPlaying == false)
                return player;
            }
            return null;
        }
    }