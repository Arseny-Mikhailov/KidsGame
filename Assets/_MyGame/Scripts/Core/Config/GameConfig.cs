using UnityEngine;

namespace Core.Config
{
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Configs/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        public Sprite[] cubes;
    }
}