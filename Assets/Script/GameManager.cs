using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _playerLife = 100;
    private int MAXHEALTH = 100;
    [SerializeField] private int _playerLife_OutOfFive = 5;
    [SerializeField] private TextMeshProUGUI _lifeText;  
    [SerializeField] GameObject _gameOverPanel;
    [SerializeField] FloatingHealthBar _healthbar;
    public static GameManager instance;

    public int GetLife()
    {
        return _playerLife_OutOfFive;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Assigner l'instance si elle est null
        if (instance == null)
        {
            instance = this;
        }
        _gameOverPanel.SetActive(false);
        _healthbar = GetComponentInChildren<FloatingHealthBar>();
        _healthbar.UpdateHealthBar(_playerLife,MAXHEALTH);
        _lifeText.text = _playerLife_OutOfFive.ToString();
    }

    public void ReduceLife(int amount)
    {
        _playerLife -= amount;
        _playerLife_OutOfFive--;
        _lifeText.text = _playerLife_OutOfFive.ToString();
        _healthbar.UpdateHealthBar(_playerLife,MAXHEALTH);
        if (_playerLife_OutOfFive <= 0)
        {
        
            _gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }


}