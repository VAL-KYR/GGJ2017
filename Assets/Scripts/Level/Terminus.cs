﻿using UnityEngine;
using System.Collections;

public class Terminus : MonoBehaviour {
    public float Health = 100f;
    public Animator animDude;
    public GameObject particles;
    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (Health >= 80f)
            animDude.SetInteger("Mood", 0);
        else if(Health >= 60f)
            animDude.SetInteger("Mood", 1);
        else if (Health >= 40f)
            animDude.SetInteger("Mood", 2);
        else if (Health >= 20f)
            animDude.SetInteger("Mood", 3);
        else
            animDude.SetInteger("Mood", 4);

        if (Health <= 0f)
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
	void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Enemy"))
        {
           
            StartCoroutine(DelayDestroy(c.gameObject));
        }
    }

    IEnumerator DelayDestroy(GameObject g)
    {
        yield return new WaitForSeconds(0.5f);
        if (g != null)
        {
            if (audioS.isPlaying)
                audioS.Stop();
            audioS.Play();
            Instantiate(particles, g.transform.position, Quaternion.Euler(new Vector3(90, 0, 0)));
            Health -= g.GetComponent<AIBase>().SoundDamage;
            Destroy(g);
        }
    }
}
