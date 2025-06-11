using UnityEngine;

public interface ICubeFactory
{
    GameObject Create(Transform parent);

    GameObject Clone(GameObject source, Transform parent);
}