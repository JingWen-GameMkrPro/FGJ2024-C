using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public List<AudioSource> audioList = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var clip in audioList)
        {
            if (GameObject.Find("EventSystem").transform != null)
            {
                //clip.volume = GameObject.Find("EventSystem").transform.GetComponent<UIControllor>().slidVoice.value;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var clip in audioList)
        {
            if(GameObject.Find("EventSystem").transform !=null)
            {
                //clip.volume = GameObject.Find("EventSystem").transform.GetComponent<UIControllor>().slidVoice.value;
            }

        }
    }
}
