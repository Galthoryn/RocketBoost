using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
[SerializeField] float delayTime = 2f;
[SerializeField] AudioClip crashSound;
[SerializeField] AudioClip sucessSound;
[SerializeField] ParticleSystem sparticle;
[SerializeField] ParticleSystem cparticle;
    
AudioSource b;
 bool isControllable = true;
 bool collidable = true;
 
    
    
    private void Start()
    {
       b= GetComponent<AudioSource>();
    }
    private void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown("l"))
        {
            nextLevel();
        }
        else if (Input.GetKeyDown("c"))
        {
            collidable = !collidable;
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable||!collidable)
        {
            return;
        }
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friend");
                break;
            case "Finish":
                
                SuccessCrashing();
                break;
            default:
               
                StartCrashing();
               
                break;
        }
    }

            void StartCrashing()
               {
                     isControllable = false;
                     b.Stop();
                     cparticle.Play();
                     b.PlayOneShot(crashSound);
                    GetComponent<Movement>().enabled = false;
                   
                    Invoke("ReloadScene", delayTime);
                }


            void SuccessCrashing()
                 {
                  isControllable = false;
                 b.Stop();
        sparticle.Play();
                  b.PlayOneShot(sucessSound);
                  Invoke("nextLevel", delayTime);
                 }
            void ReloadScene()
                 {
                    int currentScene = SceneManager.GetActiveScene().buildIndex;
                    SceneManager.LoadScene(currentScene);
                 }
            void nextLevel()
                 {
                    int currentScene = SceneManager.GetActiveScene().buildIndex;
                    int nextScene = currentScene + 1;
               
                    if(nextScene== SceneManager.sceneCountInBuildSettings)
                    {
                        nextScene = 0;
                    }
                    
                    SceneManager.LoadScene(nextScene);
                 }
 
        }
    

