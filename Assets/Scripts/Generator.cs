using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

    public GameObject dots;
    public Image Dot;

    [Header("Balls")]
    public int minQuantity; 
    public int maxQuantity;
    public float minY;
    public float maxY;
    public float minX;
    public float maxX;
    public float DistFromCenter = 125f;
    public float DistFromPD = 125f;
    public float AngleDots;

    int _upSide;
    int _botSide;
    Image _img;
    float _distFromCenter;
    float _distFromPD;
    bool _isClose = false;
    float _scale;

    public void GenerateBalls(float degreesZ)
    {
        int upSide = Random.Range(minQuantity, maxQuantity);

        for (int i = 0; i < upSide; i++)
        {
            _img = Instantiate<Image>(Dot);
            _img.transform.SetParent(dots.transform);
            _img.transform.position = dots.transform.position + new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            _scale = Random.Range(0.5f, 1f);
            _img.transform.localScale = new Vector3(_scale, _scale, _scale);

            if (_img.transform.position.x < 0)
            {
                _distFromCenter = (-_img.transform.localPosition.x * -_img.transform.localPosition.x) + Mathf.Pow(_img.transform.position.y, 2);
            }
            else
            {
                _distFromCenter = Mathf.Pow(_img.transform.localPosition.x, 2) + Mathf.Pow(_img.transform.localPosition.y, 2);
            }

            for (int a = 0; a < dots.transform.childCount; a++)
            {
                _distFromPD = Vector3.Distance(_img.gameObject.transform.position, dots.transform.GetChild(a).position);
                if (_img.gameObject.transform.position == dots.transform.GetChild(a).position)
                {
                    continue;
                }
                if (_distFromPD < DistFromPD)
                {
                    Debug.Log("Dots is too close! " + _distFromPD);
                    _isClose = true;
                }
            }

            if (_distFromCenter > DistFromCenter || _isClose)
            {
                Debug.Log("Dot is destroyed! " + _distFromPD + " " + _distFromCenter);
                Destroy(_img.gameObject);
            }
            else
            {
                Debug.Log("Dot is created! " + _distFromPD + " " + _distFromCenter);
            }

            _isClose = false;
        }

        dots.transform.localEulerAngles = new Vector3(0, 0, degreesZ);
    }

    public void Clear()
    {
        for (int a = 0; a < dots.transform.childCount; a++)
        {
            Destroy(dots.transform.GetChild(a).gameObject);
        }
        dots.transform.localEulerAngles = new Vector3(0, 0, 0);
    }
           
}

