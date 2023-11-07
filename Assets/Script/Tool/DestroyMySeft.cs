using System;
using UnityEngine;
using System.Collections;

namespace NongTrai
{
    public class DestroyMySeft : MonoBehaviour
    {
        [SerializeField] float TimeDestroy;
        [SerializeField] ParticleSystemRendererPro[] particleSystemRendererPros;

        [SerializeField] OrderPro[] orderPro;

        // -----------------------------------
        void Start()
        {
            int order = (int) (transform.position.y * (-100));
            for (int i = 0; i < particleSystemRendererPros.Length; i++)
            {
                for (int j = 0; j < particleSystemRendererPros[i].particleSystemRenderers.Length; j++)
                {
                    particleSystemRendererPros[i].particleSystemRenderers[j].sortingOrder =
                        order + particleSystemRendererPros[i].order;
                }
            }

            for (int i = 0; i < orderPro.Length; i++)
            {
                for (int j = 0; j < orderPro[i].SprRenderer.Length; j++)
                {
                    orderPro[i].SprRenderer[j].sortingOrder = order + orderPro[i].order;
                }
            }

            StartCoroutine(StartDestroy());
        }

        IEnumerator StartDestroy()
        {
            yield return new WaitForSeconds(TimeDestroy);
            Destroy(gameObject);
        }
    }

    [Serializable]
    public struct ParticleSystemRendererPro
    {
        public int order;
        public ParticleSystemRenderer[] particleSystemRenderers;
    }
}