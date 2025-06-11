using UnityEngine;

namespace _MyGame.Scripts.Core
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public Sprite[] cubes;
    }
}