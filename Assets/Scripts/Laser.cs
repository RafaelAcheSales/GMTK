using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Damager damager;
    SpriteRenderer spriteRenderer;
    bool isFiring;
    public AudioSource audioSource;
    public float onTime = 1f;
    public float offTime = 1f;

    void Start()
    {
        damager = GetComponent<Damager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        isFiring = false;
        StartCoroutine(LaserCoroutine(true));
    }

    IEnumerator LaserCoroutine(bool fire) {
        if (fire) {
            audioSource.Play();
            damager.EnableDamage();
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(onTime);
            yield return StartCoroutine(LaserCoroutine(false));
        } else {
            audioSource.Stop();
            damager.DisableDamage();
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(offTime);
            yield return StartCoroutine(LaserCoroutine(true));
            
        }
    }

}
