using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;


    public class EndSequence : MonoBehaviour
    {
        [SerializeField] private List<Transform> multiplierBlocks = new List<Transform>();

        [SerializeField] private float yOffset;

        private void Awake()
        {
            SetupBlocks();
        }

        private void Start()
        {
            EventsManager.OnReachedEnd += SetupMultipliers;
        }

        private void OnDisable()
        {
            EventsManager.OnReachedEnd -= SetupMultipliers;

        }

        private void SetupBlocks()
        {
            StartCoroutine(nameof(SetUpBlockOffset));
        }

        private void SetupMultipliers()
        {
            StartCoroutine(nameof(Setup));
        }
        IEnumerator SetUpBlockOffset()
        {
            float yValue = 0;
            int blockIndex = 1;
            foreach (var block in multiplierBlocks)
            {
                block.DOLocalMoveZ(yValue, 0);
                block.GetComponentInChildren<TextMeshPro>().text = "x" + blockIndex;
                block.GetComponent<Multiplier>().SetMultiplier(blockIndex);
                blockIndex++;
                yValue += yOffset;
                yield return new WaitForSeconds(.25f);
            }
        }

        IEnumerator Setup()
        {
            foreach (var block in multiplierBlocks)
            {
                block.DOLocalMoveY(0, .1f);
                yield return new WaitForSeconds(.25f);
            }
        }
    }
