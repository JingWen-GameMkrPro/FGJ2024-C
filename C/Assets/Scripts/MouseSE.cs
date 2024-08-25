using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseSE : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public List<AudioClip> mouseSEList = new List<AudioClip>();
    public AudioSource mouseSE;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseSE.clip = mouseSEList[0];
        mouseSE.Play();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseSE.clip = mouseSEList[1];
        mouseSE.Play();
    }
}
