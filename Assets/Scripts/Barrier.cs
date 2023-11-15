using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Barrier : MonoBehaviour
{
    [SerializeField] GameObject _sign;
    [SerializeField] GameObject[] _based;
    [SerializeField]  AnimationCurve _curve;
    [SerializeField] private float destroyDuration = 0.8f;

    public async void DeleteSign() {
        await DestroyAnimation();
        Destroy(gameObject);
        Destroy(this);
    }

    private async Task DestroyAnimation()
    {
        await Task.Delay(UnityEngine.Random.Range(200, 500));
        float elapsedTime = 0;

        while (elapsedTime< destroyDuration)
        {
            float controlValue = elapsedTime / destroyDuration;
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, _curve.Evaluate(controlValue), currentPos.z);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
        
         
    }
}
