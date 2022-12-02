using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource punch_1;
    public AudioSource coin_1;
    public AudioSource grapple_1;
    public AudioSource elimination_1;
    public AudioSource click_1;
    public AudioSource laser_1;

    public void PlayPunchSFX()
    {
        punch_1.Play();
    }

    public void PlayCoinSFX()
    {
        coin_1.Play();
    }

    public void PlayGrappleSFX()
    {
        grapple_1.Play();
    }

    public void PlayEliminationSFX()
    {
        elimination_1.Play();
    }

    public void PlayClickSFX()
    {
        click_1.Play();
    }

    public void PlayLaserSFX()
    {
        laser_1.Play();
    }
}
