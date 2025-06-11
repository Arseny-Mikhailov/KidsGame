using Core.Interfaces;
using Features.Cube;
using UnityEngine;

namespace Factory
{
    public class CubeFactory : ICubeFactory
    {
        readonly CubeView _prefab;

        public CubeFactory(CubeView prefab)
        { 
            _prefab = prefab;
        }

        public GameObject Create(Transform parent)
        {
            return Object.Instantiate(_prefab.gameObject, parent, worldPositionStays: true);
        }
    
        public GameObject Clone(GameObject source, Transform parent)
        {
            return Object.Instantiate(source, parent, worldPositionStays: true);
        }
    }
}