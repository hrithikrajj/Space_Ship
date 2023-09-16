using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashed;
    [SerializeField] AudioClip finish;
    [SerializeField] private float delay= 1f;

    [SerializeField] ParticleSystem crashedParticle;
    [SerializeField] ParticleSystem finishParticle;

    bool collisionPossible = true;
    AudioSource audioSource;
    bool isTransitioning = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.C))
        {
           collisionPossible = !collisionPossible;
        } 
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) { return; }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                next();
                break;
            default:
                if (collisionPossible)
                {
                    crash();
                }
                break;
        } 
    }
    void crash()
    {
        crashedParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashed);
        GetComponent<Movement>().enabled = false;
        Invoke("reLoadlevel", delay);
        isTransitioning = false;
    }
    void reLoadlevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void next()
    {
        finishParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(finish);
        GetComponent<Movement>().enabled = false;
        Invoke("loadNextLevel", delay);
        isTransitioning = false;
    }
    void loadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentSceneIndex + 1; 
        if(nextScene < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }
}
