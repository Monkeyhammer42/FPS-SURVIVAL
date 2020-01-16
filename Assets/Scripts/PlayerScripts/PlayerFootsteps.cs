using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

   [SerializeField]
    private AudioClip[]footstep_Clip;

    private CharacterController character_Controller;
    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_distance;


    void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();
        character_Controller = GetComponent<CharacterController>();
    }



    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound ()
    {
        if (!character_Controller.isGrounded)
            return;
    }







}

