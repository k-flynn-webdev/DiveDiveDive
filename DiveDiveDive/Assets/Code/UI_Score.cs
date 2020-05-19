using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Score : MonoBehaviour, ISubscribeScore
{

    private Animator _scoreAnim;
    private Text _scoreText;
    private TextMeshProUGUI _scoreTextPro;

    void Awake()
    {
        _scoreAnim = GetComponent<Animator>();
        _scoreText = GetComponent<Text>();
        _scoreTextPro = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        ServiceLocator.Resolve<ScoreManager>().SubscribeScore(this);
    }

    public void ReactScore(ScoreObj score)
    {
        if (_scoreAnim != null)
        {
            _scoreAnim.SetTrigger("click");
        }

        if (_scoreText != null)
        {
            _scoreText.text = score._valueText;
        }

        if (_scoreTextPro != null)
        {
            _scoreTextPro.text = score._valueText;
        }
    }
}
