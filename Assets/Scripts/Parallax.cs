using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float _length;
    private float _startPosition;
    public new GameObject camera;
    [SerializeField]private float parallaxEffect;

     void Start()
    {
        _startPosition = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

      void Update()
      {
          float temp = camera.transform.position.x * (1 - parallaxEffect);
          float distance = camera.transform.position.x * parallaxEffect;
          transform.position = new Vector3(_startPosition + distance, transform.position.y, transform.position.z);

          if (temp > _startPosition + _length)
              _startPosition += _length;
          else if (temp < _startPosition - _length)
              _startPosition -= _length;
      }
}
