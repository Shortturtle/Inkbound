using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "PlayerSoundContainer")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerSoundContainerClass : ScriptableObject
{
    public AudioClip FootstepSounds;
    public AudioClip JumpSounds;
    public AudioClip LandingSounds;
    public AudioClip PickUpSounds;
    public AudioClip PutDownSounds;
}