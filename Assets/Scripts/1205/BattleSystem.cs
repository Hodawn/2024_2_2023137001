using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance { get; private set; }
    public Character[] players = new Character[3];
    public Character[] enemies = new Character[3];

    public Button attackBtn;
    public TextMeshProUGUI turnText;
    public GameObject damageTextPrefab;
    public Canvas uiCanvas;

    Queue<Character> turnQueue = new Queue<Character>();
    Character curretChar;
    bool selectingTarget;

    void Awake()=> Instance = this;
    public Character GetCurrentChar() => curretChar;
    void OnAttackClick()=>selectingTarget = true;

     void Start()
    {
        var orderedChars = players.Concat(enemies).OrderByDescending(c => c.speed);

        foreach(var c in orderedChars)
        {
            turnQueue.Enqueue(c);

            attackBtn.onClick.AddListener(OnAttackClick);
            NextTurn();
        }
    }
     void Update()
    {
        if(selectingTarget&& Input.GetMouseButtonDown(0))
        {
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Character target = hit.collider.GetComponent<Character>();
                if (target != null)
                {
                    curretChar.Attack(target);
                    ShowDamageText(target.transform.position, "20");
                    selectingTarget = false;
                    NextTurn();
                }
               
            }
        }
    }


    void NextTurn()
    {
        curretChar=turnQueue.Dequeue();
        turnQueue.Enqueue(curretChar);
        turnText.text = turnText.text = $"{curretChar.name}¿« ≈œ (Speed: {curretChar.speed})";

        if (curretChar.isPlayer)
        {
            attackBtn.gameObject.SetActive(true);
        }
        else
        {
            attackBtn.gameObject.SetActive(false);
            Invoke("EnemyAttack", 1f);
        }
    }

    void ShowDamageText(Vector3 position, string damage)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        GameObject damageObj = Instantiate(damageTextPrefab, screenPos, Quaternion.identity, uiCanvas.transform);
        damageObj.GetComponent<TextMeshProUGUI>().text = damage;
        Destroy(damageObj, 1f);
    }
    void EnemyAttack()
    {
        var aliveTargets = players.Where(p=>p.gameObject.activeSelf).ToArray();
        if(aliveTargets.Length ==0 ) return;

        var target = aliveTargets[Random.Range(0, aliveTargets.Length)];
        curretChar.Attack(target);
        ShowDamageText(target.transform.position, "20");
        NextTurn();
    }
}
