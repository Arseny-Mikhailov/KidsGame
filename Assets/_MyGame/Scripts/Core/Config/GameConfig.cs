using UnityEngine;

namespace _MyGame.Scripts.Core
{
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Configs/" + nameof(GameConfig))]
    public class GameConfig : ScriptableObject
    {
        public Sprite[] cubes;
    }
}