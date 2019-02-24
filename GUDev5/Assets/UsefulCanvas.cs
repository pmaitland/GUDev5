using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsefulCanvas : MonoBehaviour
{
    public InfoPanel infoPanel;
    public SlimeCurtain curtain;
    
    static private UsefulCanvas instance;
    public static UsefulCanvas Instance
    {
        get { return instance; }
    }

    
    private void Awake(){
        if(instance == null){
            instance = this;
            return;
        }

        if(instance == this){
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}
