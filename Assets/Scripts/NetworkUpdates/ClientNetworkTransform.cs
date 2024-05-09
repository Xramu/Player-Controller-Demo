using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

public class ClientNetworkTransform : NetworkTransform
{
    [SerializeField] protected bool _isServerAuthoritative = false;

    protected override bool OnIsServerAuthoritative()
    {
        return _isServerAuthoritative;
    }
}
