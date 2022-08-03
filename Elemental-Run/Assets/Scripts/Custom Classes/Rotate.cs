using System.Collections;
using UnityEngine;


    public class Rotate : MonoBehaviour
    {
        [SerializeField] private float rotateSpeed;

        [SerializeField] private Transform targetToRotate;

        private float _xRotateVal;
        private void Start()
        {
            StartCoroutine(nameof(StartRotating));
        }

        IEnumerator StartRotating()
        {
            while (true)
            {
                _xRotateVal += 10f;
                var transform1 = targetToRotate.transform;
                var rotation = transform1.localRotation;
                var normalRotation = Quaternion.Euler(0, _xRotateVal, 0);
                transform.localRotation =Quaternion.Lerp(rotation, normalRotation, Time.deltaTime * rotateSpeed);
                yield return new WaitForSeconds(.05f);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
