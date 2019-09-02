using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.PocketFighters
{
    public class GameManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public int rounds;
        public bool isFighting, onBattle;
        public float timeToBlink, elapsedTime;

        PocketIA foeIA;

        public Slider barSlider;
        public Text textRounds, UIVidasLeft, UIVidasRight;  
        public PocketFighterStats playerLeft, playerRight;
        public JokenPo leftHand, rightHand;
        public Player lastWinner;
        public int winnerCount, damage, slideCount, movesByRound;

        public GameObject screenTitle, screenBattle, screenGameOver;
        public GameObject gameOverLeft, gameOverRight;
        public GameObject imageLeft, imageRight;

       public AudioSource audio;
        [SerializeField]
        public List<AudioClip> dict;

        public float timeToStartBattle = 3, elapsedToStart;
        

        void Start()
        {            
        }

        public void Setup()
        {
            rounds = 1;
            foeIA = new PocketIA();
            //barRect = frontBar.GetComponent<RectTransform>();

            foeIA.GetJoKenPo();

            playerLeft.ClearStats();
            playerRight.ClearStats();

            rightHand = leftHand = JokenPo.None;
            lastWinner = Player.Nobody;
            damage = 1;
            movesByRound = 2;

            isFighting = false;

            screenTitle.SetActive(true);
            screenBattle.SetActive(false);
            screenGameOver.SetActive(false);

            gameOverLeft.SetActive(false);
            gameOverRight.SetActive(false);

            elapsedToStart = 0;
            onBattle = false;
        }

        void Awake()
        {
            Setup();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log("Oi");
            ReadyToFight();
            UI_Update();
            Fight();
        }

        public void ReadyToFight()
        {
            if (onBattle)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= timeToStartBattle)
                {
                    onBattle = false;
                    isFighting = true;
                    audio.PlayOneShot(dict[1]);
                }               
            }
        }

        public void UI_Update()
        {
            textRounds.text = "Round " + this.rounds;
            UIVidasLeft.text = "( " + playerLeft.vida + " / " + playerLeft.vidaMax + " )";
            UIVidasRight.text = "( " + playerRight.vida + " / " + playerRight.vidaMax + " )";            
        }

        public void Fight()
        {
            if (isFighting)
            {
                //                
                CheckInputs();
                IncreaseBar();
            }
            else {
                barSlider.value = barSlider.minValue;
            }
        }

        public void IncreaseBar()
        {
            //Debug.Log(barSlider.value);
            barSlider.value += Time.deltaTime;

            if (barSlider.value == barSlider.maxValue)
            {
                CheckHands();
                barSlider.value = barSlider.minValue;
                slideCount++;

                if (slideCount >= movesByRound)
                {
                    slideCount = 0;
                    this.rounds++;

                    if (rounds == 10)
                    {
                        isFighting = false;

                        if (playerLeft.vida > playerRight.vida)
                        {
                            ShowGameOver(Player.Right);
                        }
                        else if (playerRight.vida > playerLeft.vida)
                        {
                            ShowGameOver(Player.Right);
                        }
                        else
                        {
                            ShowGameOver(Player.Nobody);
                        }
                    }                    
                }
            }            
        }

        public void CheckInputs()
        {
            if (leftHand == JokenPo.None)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    leftHand = JokenPo.Rock;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    leftHand = JokenPo.Paper;
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    leftHand = JokenPo.Scissors;
                }
            }

            if (rightHand == JokenPo.None)
            {
                if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Keypad1)
                    || Input.GetKeyDown(KeyCode.Keypad4)
                    || Input.GetKeyDown(KeyCode.Keypad7))
                {
                    rightHand = JokenPo.Rock;
                }
                else if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.Keypad2)
                    || Input.GetKeyDown(KeyCode.Keypad5)
                    || Input.GetKeyDown(KeyCode.Keypad8))
                {
                    rightHand = JokenPo.Paper;
                }
                else if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Keypad3)
                    || Input.GetKeyDown(KeyCode.Keypad6)
                    || Input.GetKeyDown(KeyCode.Keypad9))
                {
                    rightHand = JokenPo.Scissors;
                }
            }
        }
        public void CheckHands()
        {
            Player tempP = PlayJokenPo(leftHand, rightHand);

            if (lastWinner == tempP && lastWinner != Player.Nobody)
            {
                audio.PlayOneShot(dict[1]);
                winnerCount++;
                damage += winnerCount;
            }
            else if (winnerCount > 0)
            {
                audio.PlayOneShot(dict[2]);
            }
            else
            {                
                winnerCount = 0;
            }

            if (tempP == Player.Left)
            {
                playerRight.vida -= damage;
            }
            else if (tempP == Player.Right)
            {
                playerLeft.vida -= damage;
            }
            else if (leftHand == rightHand && (leftHand != JokenPo.None || rightHand != JokenPo.None))
            {
                playerRight.vida -= damage;
                playerLeft.vida -= damage;
            }

            if (playerLeft.vida == 0)
                ShowGameOver(Player.Left);
            else if (playerRight.vida == 0)
                ShowGameOver(Player.Right);

            damage = 1;
            lastWinner = tempP;

            leftHand = rightHand = JokenPo.None;
        }

        public void ShowGameOver(Player loser)
        {
            // ativar tela.
            isFighting = false;

            audio.Stop();

            //audio.PlayOneShot(dict[5]);

            screenTitle.SetActive(false);
            screenBattle.SetActive(false);
            screenGameOver.SetActive(true);

            if (loser == Player.Left)
            {
                gameOverRight.SetActive(true);
            }
            else
            {
                gameOverLeft.SetActive(true);
            }
        }

        public void ShowBattle(string battleMode)
        {
            //AudioClip.
            onBattle = true;
            //audio.clip = dict[5];
            //audio.Play();

            if (battleMode == "1PVS2P")
            {
                screenTitle.SetActive(false);
                screenBattle.SetActive(true);
                screenGameOver.SetActive(false);
            }
        }

        public void GoToTitle()
        {
            screenTitle.SetActive(true);
            screenBattle.SetActive(false);
            screenGameOver.SetActive(false);

            Awake();
        }

        //
        public Player PlayJokenPo(JokenPo playerleft, JokenPo playerRight)
        {
            // comparando jogada
            Player winner;

            if (playerleft == playerRight )
            {
                winner = Player.Nobody;
            }
            else if (playerRight == JokenPo.None)
            {
                winner = Player.Left;
            }
            else if (playerleft == JokenPo.None && playerRight != JokenPo.None)
            {
                winner = Player.Right;
            }
            
            else if (playerleft == WhoIsJokenPoLoser(playerRight))
            {
                winner = Player.Right;
            }
            else
            {
                winner = Player.Left;
            }

            return winner;
        }

        public JokenPo WhoIsJokenPoLoser(JokenPo input)
        {
            JokenPo result;

            switch (input)
            {
                case JokenPo.Rock: result = JokenPo.Scissors; break;
                case JokenPo.Scissors: result = JokenPo.Paper; break;
                
                default: result = JokenPo.Rock;  break;
            }

            return result;
        }
    }
}
