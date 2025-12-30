using JetBrains.Annotations;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    public GameObject smallfish;
    public GameObject mediumfish;
    public GameObject sharkfish;
    public SpringJoint fishingTension;
    public AudioSource reelSound;
    GameObject fish;
    public float timeFishing = 0;
    bool hasFish = false;
    public bool isReeling = false;
    public bool caughtFish = false;
    public bool maxLevel = false;
    int whichFish = 0;
    [SerializeField] private Animator reeling;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        reeling.SetBool("startReeling", false);
        reelSound.Stop();
    }
    // Update is called once per frame
    void Update()
    {
        if (isReeling)
        {
            fishingTension.spring += 1*Time.deltaTime;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("water") && !hasFish)
        {
            timeFishing += Time.deltaTime;
            if (timeFishing >= 3)
            {
                whichFish = UnityEngine.Random.Range(0, 10);
                if (whichFish < 7 && !maxLevel)
                {
                    fish = Instantiate(smallfish, transform);
                    fish.transform.localPosition = new Vector3(0.429f, 0.014f, 0);
                    fish.GetComponent<AudioSource>().Play();
                }
                else if (whichFish < 9 && !maxLevel)
                {
                    fish = Instantiate(mediumfish, transform);
                    fish.transform.localPosition = new Vector3(0.442f, 0, 0);
                    fish.GetComponent<AudioSource>().Play();
                }
                else
                {
                    fish = Instantiate(sharkfish, transform);
                    fish.transform.localPosition = new Vector3(1.115f, 0, 0);
                    fish.GetComponent<AudioSource>().Play();
                }
                timeFishing = 0;
                hasFish = true;
            }
        }
        if (other.CompareTag("hookOrigin") && isReeling)
        {
            isReeling = false;
            reeling.SetBool("startReeling", false);
            reelSound.Stop();
            caughtFish = true;
            Debug.Log("you caught a fish");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("water"))
        {
            if (hasFish)
            {
                reeling.SetBool("startReeling", true);
                reelSound.Play();
                hasFish = false;
                isReeling = true;
            }
            timeFishing = 0;
        }
    }
}