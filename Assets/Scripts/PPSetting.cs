using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PPSetting : MonoBehaviour
{

    public static PPSetting Instance;

    private PostProcessVolume ppVolume;
    private Bloom bloomEffect;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ppVolume = GetComponent<PostProcessVolume>();
        ppVolume.profile.TryGetSettings(out bloomEffect);
    }

    public void ActivateBloomEffect(bool on)
    {
        if (on) bloomEffect.active = true;
        else bloomEffect.active = false;
    }
}
