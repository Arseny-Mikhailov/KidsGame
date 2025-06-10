using System.Collections.Generic;
using _MyGame.Scripts.Features.Cube;

namespace _MyGame.Scripts.Features.Tower
{
    public class TowerState
    {
        private readonly List<CubeView> _tower = new();

        public int Count => _tower.Count;
        public IReadOnlyList<CubeView> All => _tower;
        public int GetIndexOf(CubeView cube) => _tower.IndexOf(cube);
        public bool Contains(CubeView cube) => _tower.Contains(cube);
        public bool Remove(CubeView cube) => _tower.Remove(cube);
        public void Add(CubeView cube) => _tower.Add(cube);
    }
}