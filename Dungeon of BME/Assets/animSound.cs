using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animSound : MonoBehaviour
{
    public AudioSource soundEffect;
    public AudioSource soundEffect2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AnimationWalkSound()
    {
        soundEffect.Play();
    }

    void AnimationSwordSound()
    {
        soundEffect2.Play();
    }
}
