using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float axisMov = 1f;
    Rigidbody rigidBody;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip engineThrust;
    AudioSource audioSource;
    [SerializeField] ParticleSystem thrustParticle;
    // Start is called before the first frame update
    void Start()
    {
        //Initalizing rigid body to get to component from the object
        rigidBody = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.L)) 
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            if(currentScene + 1 < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(currentScene + 1);
            }
        }
        ProcessInput(); 
        
    }
    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineThrust);
            }
            rigidBody.freezeRotation = true;
            thrustParticle.Play();
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            rigidBody.freezeRotation = false;
        }
        else
        {
           audioSource.Stop();
            thrustParticle.Stop();
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * axisMov * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * axisMov * Time.deltaTime);
            }
    }
}

