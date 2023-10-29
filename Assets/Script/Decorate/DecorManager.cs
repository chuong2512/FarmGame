using System;
using UnityEngine;

namespace Script.Decorate
{
    public class DecorManager : MonoBehaviour
    {
        [SerializeField] private DecorateObject[] _decorateObjects;

        [SerializeField] private int _firstID = 0;
        
        private void OnValidate()
        {
            _decorateObjects = GetComponentsInChildren<DecorateObject>();

            for (int i = 0; i < _decorateObjects.Length; i++)
            {
                _decorateObjects[i].idSerial = i + _firstID;
            }
        }
    }
}