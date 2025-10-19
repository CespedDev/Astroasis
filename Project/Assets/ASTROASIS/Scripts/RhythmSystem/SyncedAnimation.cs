using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class SyncedAnimation : MonoBehaviour
{
    public Toggle toggle;

    void Update()
    {

        toggle.isOn = !toggle.isOn;
    }
}
