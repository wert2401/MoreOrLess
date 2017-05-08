using UnityEngine;
using UnityEngine.UI;

public class Controll : MonoBehaviour {

    public Image ProgressBar;
    public float time;
    public GameObject dotsUp;
    public GameObject dotsDown;
    public Animator anim;
    public Button UpButton;
    public Button DownButton;
    public Button MainButton;
    public Generator genUp;
    public Generator genDown;
    public Text count;
    public Text score;
    public Text current;

    public float _time;
    int _count = 0;
    bool _IsPlaying;
    int _score;

    private void Start()
    {
        SavingAndLoading.save.Load();
        _score = SavingAndLoading.save.score;

        _time = time;
        count.text = _count.ToString();
        current.text = _count.ToString();
        score.text = _score.ToString();
    }

    void Update () {
        if (_IsPlaying)
        {
            _time -= Time.deltaTime;
            _time = Mathf.Clamp(_time, 0f, time);
            ProgressBar.fillAmount = _time / time;
        }
        if (_time == 0)
        {
            _time = time;
            Fail();
        }
	}

    public void Up()
    {
        if (dotsUp.transform.childCount >= dotsDown.transform.childCount)
        {
            Win();
        }
        else
        {
            Fail();
        }
    }

    public void Down()
    {
        if (dotsDown.transform.childCount >= dotsUp.transform.childCount)
        {
            Win();
        }
        else
        {
            Fail();
        }
    }

    public void Fail()
    {
        Debug.Log("You lose!");
        UpButton.interactable = false;
        DownButton.interactable = false;

        Clear();

        anim.SetTrigger("Reset");
        MainButton.gameObject.SetActive(true);
        _IsPlaying = false;
        _time = time;

        if (_score < _count)
        {
            _score = _count;
        }

        score.text = _score.ToString();
        current.text = _count.ToString();
        _count = 0;
        count.text = _count.ToString();

        SavingAndLoading.save.score = _score;
        SavingAndLoading.save.Save();
    }

    public void Win()
    {
        _count++;
        count.text = _count.ToString();
        if (_count < 16)
        {
            _time = time - (0.5f * _count);
        }
        else
        {
            _time = 2f;
        }


        Clear();
        Generate();
    }

    public void FirstTouch()
    {
        UpButton.interactable = true;
        DownButton.interactable = true;

        anim.SetTrigger("FirstTouch");
        MainButton.gameObject.SetActive(false);
        _IsPlaying = true;
        Generate();
    }

    void Generate()
    {
        genDown.GenerateBalls(genDown.AngleDots);
        genUp.GenerateBalls(genUp.AngleDots);
    }

    void Clear()
    {
        genDown.Clear();
        genUp.Clear();
    }

}
