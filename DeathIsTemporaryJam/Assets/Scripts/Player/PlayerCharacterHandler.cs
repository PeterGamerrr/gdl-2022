using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterHandler : MonoBehaviour
{
    [SerializeField] AudioSource footstep;


    void Update()
    {
        
    }


    public void PlayFootstepSound()
    {
        footstep.Play();
    }
    
}
