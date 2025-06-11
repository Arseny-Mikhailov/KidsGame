using I2.Loc;

using UnityEngine;

namespace _MyGame.Scripts.Localization
{
    [CreateAssetMenu(fileName = nameof(LocalizationConfig), menuName = "Configs/" + nameof(LocalizationConfig))]
    public class LocalizationConfig : ScriptableObject
    {
        [field: SerializeField] public LocalizedString CubeDroppedToHole { get; private set; }
        [field: SerializeField] public LocalizedString CubeReturned { get; private set; }
        [field: SerializeField] public LocalizedString CubePlaced { get; private set; }
        [field: SerializeField] public LocalizedString CubePlacementBlocked { get; private set; }
        [field: SerializeField] public LocalizedString CubeThrownToHole { get; private set; }
    }
}