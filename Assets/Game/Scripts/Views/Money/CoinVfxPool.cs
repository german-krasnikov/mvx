using UnityEngine;
using Zenject;

namespace Game.Views.Planets
{
    public class CoinVFXPool : MonoMemoryPool<Vector2, Transform>
    {
        protected override void Reinitialize(Vector2 position, Transform coin)
        {
            coin.position = position;
            coin.gameObject.SetActive(true);
        }

        protected override void OnDespawned(Transform coin)
        {
            coin.gameObject.SetActive(false);
        }
    }
}