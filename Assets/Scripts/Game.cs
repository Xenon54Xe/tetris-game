using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// à faire:

public class Game : MonoBehaviour
{
    public GameObject scoreSign;
    private GameObject firstCase;
    private Vector3 firstCasePos;

    // Touches pour actions
    private KeyCode rotateRight;
    private KeyCode rotateLeft;
    private KeyCode moveRight;
    private KeyCode moveLeft;
    private KeyCode moveDown;

    private bool canMoveDownWithKey;

    public int maxTimeSamePiece = 3;
    private int time;
    private int nbChosed = -1;

    public float timeBeforeDoThing = 1.5f;

    private Vector3 spawnCasePos;

    private GameObject spawnCaseNextPiece;
    private Vector3 spawnCaseNextPiecePos;

    public float gameSpeed = 1;

    public int nbCasesWidth = 8;
    public int nbCasesHeight = 12;

    public float spaceBetweenCases = 2;

    Vector2[] choiceForme = { new Vector2(0, 0) };
    List<Vector2[]> choiceList;
    bool canRotate = true;
    int reversed = 1;

    private Color caseColor = Color.red;
    private Vector3 actualCase;
    private bool canMoveBottom = true;
    private float score = 0;
    int randInt;

    private bool localCanRotate;
    private bool localCanReverse;
    private Vector2[] localChoiceForme;
    private int localReversed;
    private Color localCaseColor;
    private int localRandInt;
    private List<Vector2[]> localChoiceList;

    private bool nextCanRotate;
    private bool nextCanReverse;
    private Vector2[] nextChoiceForme;
    private int nextReversed;
    private Color nextCaseColor;
    private int nextRandInt;
    private List<Vector2[]> nextChoiceList;

    public bool runGame;

    IEnumerator gameTime;

    // Trouver un moyen de stocker les données sur les figures mobiles
    // Pièce point (6)
    private List<Vector2[]> pointPieceList = new List<Vector2[]>();
    private Vector2[] pointPiece = { new Vector2(0, 0) };
    // Pièce en forme de L (0)
    private List<Vector2[]> LPieceList = new List<Vector2[]>();
    private Vector2[] LPieceUp = { new Vector2(0, 0), new Vector2(0, -1), new Vector2(1, -1), new Vector2(0, 1) };
    private Vector2[] LPieceRight = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(-1, -1) };
    private Vector2[] LPieceDown = { new Vector2(0, 0), new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 1) };
    private Vector2[] LPieceLeft = { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(1, 1) };
    // Pièce en forme de Z (1)
    private List<Vector2[]> ZPieceList = new List<Vector2[]>();
    private Vector2[] ZPieceUP = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(-1, 1), new Vector2(0, 1) };
    private Vector2[] ZPieceRight = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(-1, 0), new Vector2(-1, -1) };
    // Pièce en forme de ligne4 (2)
    private List<Vector2[]> Line4PieceList = new List<Vector2[]>();
    private Vector2[] Line4PieceUP = { new Vector2(0, 1), new Vector2(0, -1), new Vector2(0, -2), new Vector2(0, 0) };
    private Vector2[] Line4PieceRight = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(-2, 0) };
    // Pièce en forme de ligne3 (7)
    private List<Vector2[]> Line3PieceList = new List<Vector2[]>();
    private Vector2[] Line3PieceUP = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, -1) };
    private Vector2[] Line3PieceRight = { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(1, 0) };
    // Pièce en forme de ligne5 (8)
    private List<Vector2[]> Line5PieceList = new List<Vector2[]>();
    private Vector2[] Line5PieceUP = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2), new Vector2(0, -1), new Vector2(0, -2) };
    private Vector2[] Line5PieceRight = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(2, 0), new Vector2(-1, 0), new Vector2(-2, 0) };
    // Pièce en forme de carré (3)
    private List<Vector2[]> SquarePieceList = new List<Vector2[]>();
    private Vector2[] SquarePiece = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0), new Vector2(1, 1) };
    // Pièce en forme de coin (4)
    private List<Vector2[]> CoinPieceList = new List<Vector2[]>();
    private Vector2[] CoinPieceUP = { new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(0, -1) };
    private Vector2[] CoinPieceRight = { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(-1, -1) };
    private Vector2[] CoinPieceDown = { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(0, -1) };
    private Vector2[] CoinPieceLeft = { new Vector2(0, 0), new Vector2(-1, -1), new Vector2(0, -1) };
    // Pièce en forme de T (5)
    private List<Vector2[]> TPieceList = new List<Vector2[]>();
    private Vector2[] TPieceUP = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(-1, -1), new Vector2(1, -1) };
    private Vector2[] TPieceRight = { new Vector2(0, 0), new Vector2(1, 0), new Vector2(-1, 0), new Vector2(-1, 1), new Vector2(-1, -1) };
    private Vector2[] TPieceDown = { new Vector2(0, 0), new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, 1), new Vector2(1, 1) };
    private Vector2[] TPieceLeft = { new Vector2(0, 0), new Vector2(-1, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(1, -1) };

    // Liste des fréquences d'apparition des pièces (facile)
    private int[] frequencyListFacile = { 6, 6, 0, 0, 0, 1, 1, 1, 7, 7, 7, 2, 2, 8, 8, 4, 4, 4, 3, 3 };
    // normal
    private int[] frequencyListNormal = { 0, 0, 0, 1, 1, 1, 2, 2, 7, 7, 7, 8, 3, 3, 3, 4, 4, 5, 6, 6 };
    // difficile
    private int[] frequencyListDifficile = { 0, 0, 0, 1, 1, 1, 2, 2, 7, 8, 8, 3, 3, 3, 4, 4, 5, 5, 5, 6 };


    private void Awake()
    {
        GameManager.OnActionTouchesChanged += UpdateTouches;
        GameManager.OnGameStateChanged += ClearGame;
    }

    private void Start()
    {
        // Stocker la forme des pièces
        // Pièce en forme de point
        pointPieceList.Add(pointPiece);
        // Pièce en forme de L
        LPieceList.Add(LPieceUp);
        LPieceList.Add(LPieceRight);
        LPieceList.Add(LPieceDown);
        LPieceList.Add(LPieceLeft);
        // Pièce en forme de Z
        ZPieceList.Add(ZPieceUP);
        ZPieceList.Add(ZPieceRight);
        // Pièce en forme de ligne4
        Line4PieceList.Add(Line4PieceUP);
        Line4PieceList.Add(Line4PieceRight);
        // Pièce en forme de ligne3
        Line3PieceList.Add(Line3PieceUP);
        Line3PieceList.Add(Line3PieceRight);
        // Pièce en forme de ligne5
        Line5PieceList.Add(Line5PieceUP);
        Line5PieceList.Add(Line5PieceRight);
        // Pièce en forme de carré
        SquarePieceList.Add(SquarePiece);
        // Pièce en forme de coin
        CoinPieceList.Add(CoinPieceUP);
        CoinPieceList.Add(CoinPieceRight);
        CoinPieceList.Add(CoinPieceDown);
        CoinPieceList.Add(CoinPieceLeft);
        // Pièce en forme de T
        TPieceList.Add(TPieceUP);
        TPieceList.Add(TPieceRight);
        TPieceList.Add(TPieceDown);
        TPieceList.Add(TPieceLeft);

        // Définir les cases importantes du jeu
        firstCase = GameObject.Find("FirstCase");
        firstCasePos = firstCase.transform.position;

        spawnCasePos = firstCasePos + new Vector3(nbCasesWidth * spaceBetweenCases / 2, 0, (nbCasesHeight - 1) * spaceBetweenCases);

        actualCase = spawnCasePos;

        spawnCaseNextPiece = GameObject.Find("SpawnCaseNextPiece");

        // choisir tout ce qui est en rapport avec les pièces
        DefAttributs();
        choiceForme = localChoiceForme;
        randInt = localRandInt;
        canRotate = localCanRotate;
        reversed = localReversed;
        caseColor = localCaseColor;
        choiceList = localChoiceList;
        DefAttributs();
        nextChoiceForme = localChoiceForme;
        nextRandInt = localRandInt;
        nextCanRotate = localCanRotate;
        nextCanReverse = localCanReverse;
        nextReversed = localReversed;
        nextCaseColor = localCaseColor;
        nextChoiceList = localChoiceList;

        spawnCaseNextPiecePos = spawnCaseNextPiece.transform.position;
    }

    private void Update()
    {
        runGame = GameManager.instance.GameState == GameStates.RunGame ? runGame = true : runGame = false;

        if (runGame)
        {
            scoreSign.GetComponent<TextMeshPro>().text = score.ToString();// trouver le moyen de convertir le score en string
            OutBorder(choiceForme, actualCase);

            if (canMoveBottom && TestCanMove(choiceForme, actualCase, new Vector2(0, -spaceBetweenCases)))
            {
                float timeToWait = 1.5f;
                if(GameManager.instance.difficulty == 3)
                {
                    float dividend = (score + 10000) / 10000;
                    GameManager.instance.ChangeGameSpeed(dividend);
                    timeToWait = 1.5f / dividend;
                }
                gameTime = GameTime(timeToWait);
                StartCoroutine(gameTime);
                ClearColor(choiceForme, actualCase);
                actualCase.z = actualCase.z - spaceBetweenCases;
                canMoveDownWithKey = true;
            }
            else if (canMoveBottom)
            {
                foreach (Vector2 positions in choiceForme)
                {
                    if (Physics.Raycast(actualCase + new Vector3(positions.x * reversed, -1, positions.y) * spaceBetweenCases, Vector3.up, out RaycastHit hit, 2 * spaceBetweenCases))
                    {
                        if (hit.transform.tag == "Case")
                        {
                            hit.transform.tag = "Occupied";
                        }
                    }
                }

                int nbLinesFull = 0;
                int addScore = 0;
                for (int i = 0; i < nbCasesHeight; i++)
                {
                    int add = 0;
                    RaycastHit[] hits = Physics.RaycastAll(firstCasePos + new Vector3(-1, 0, i) * spaceBetweenCases, Vector3.right, 2 * spaceBetweenCases * nbCasesWidth);
                    foreach (RaycastHit hit in hits)
                    {
                        if (hit.transform.tag == "Occupied")
                        {
                            add++;
                        }
                    }
                    if (add == nbCasesWidth)
                    {
                        nbLinesFull++;
                        foreach (RaycastHit hit in hits)
                        {
                            if (hit.transform.tag == "Occupied")
                            {
                                hit.transform.tag = "Case";
                                hit.transform.GetComponent<Renderer>().material.color = Color.black;
                            }
                        }

                        for (int iLine = i; iLine < nbCasesHeight - 1; iLine++)
                        {
                            for (int iColumn = 0; iColumn < nbCasesWidth; iColumn++)
                            {
                                if (Physics.Raycast(firstCasePos + new Vector3(iColumn, -1, iLine + 1) * spaceBetweenCases, Vector3.up, out RaycastHit hitCopy, 2 * spaceBetweenCases))
                                {
                                    if (hitCopy.transform.tag == "Occupied" || hitCopy.transform.tag == "Case")
                                    {
                                        Color caseColor = hitCopy.transform.GetComponent<Renderer>().material.color;
                                        string caseTag = hitCopy.transform.tag;
                                        if (Physics.Raycast(firstCasePos + new Vector3(iColumn, -1, iLine) * spaceBetweenCases, Vector3.up, out RaycastHit hitPaste, 2 * spaceBetweenCases))
                                        {
                                            if (hitPaste.transform.tag == "Occupied" || hitPaste.transform.tag == "Case")
                                            {
                                                hitPaste.transform.GetComponent<Renderer>().material.color = caseColor;
                                                hitPaste.transform.tag = caseTag;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        i--;
                        addScore = nbLinesFull == 1 ? addScore = 40 : nbLinesFull == 2 ? addScore = 100 : nbLinesFull == 3 ? addScore = 250 : nbLinesFull == 4 ? addScore = 600 : addScore = 1500;
                        Vector3 posTargeted = firstCasePos + new Vector3(-1, 0, nbCasesHeight - 1) * spaceBetweenCases;
                        RaycastHit[] hitsTwo = Physics.RaycastAll(posTargeted, Vector3.right, 2 * spaceBetweenCases * nbCasesWidth);
                        foreach(RaycastHit hit in hitsTwo)
                        {
                            if(hit.transform.tag == "Occupied")
                            {
                                hit.transform.tag = "Case";
                                hit.transform.GetComponent<Renderer>().material.color = Color.black;
                            }
                        }
                    }
                }
                addScore = addScore + 10;
                score = score + addScore * gameSpeed;

                actualCase = spawnCasePos;

                foreach (Vector2 vectors in nextChoiceForme)
                {
                    if (Physics.Raycast(spawnCaseNextPiecePos + new Vector3(vectors.x * nextReversed, -1, vectors.y) * spaceBetweenCases, Vector3.up, out RaycastHit hit, 2 * spaceBetweenCases))
                    {
                        if (hit.transform.tag == "NextPiece")
                        {
                            hit.transform.GetComponent<Renderer>().material.color = Color.white;
                        }
                    }
                }

                //ClearColor(choiceForme, actualCase);
                choiceForme = nextChoiceForme;
                randInt = nextRandInt;
                canRotate = nextCanRotate;
                reversed = nextReversed;
                caseColor = nextCaseColor;
                choiceList = nextChoiceList;
                DefAttributs();
                nextChoiceForme = localChoiceForme;
                nextRandInt = localRandInt;
                nextCanRotate = localCanRotate;
                nextCanReverse = localCanReverse;
                nextReversed = localReversed;
                nextCaseColor = localCaseColor;
                nextChoiceList = localChoiceList;

                if (TestCanRotate(nextChoiceForme, actualCase) is false)
                {
                    GameManager.instance.SetScore(score - 10f * gameSpeed);
                    GameManager.instance.ChangeGameState(GameStates.Lose);
                }
            }

            if (reversed == 1 && canRotate)
            {
                if (Input.GetKeyDown(rotateRight) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    int newRandInt = randInt + 1 < choiceList.Count ? newRandInt = randInt + 1 : newRandInt = 0;
                    Vector2[] newChoiceForme = choiceList[newRandInt];
                    if (TestCanRotate(newChoiceForme, actualCase))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase + new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x + spaceBetweenCases;
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase - new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x - spaceBetweenCases;
                    }
                }
                if (Input.GetKeyDown(rotateLeft) || Input.GetMouseButtonDown(0))
                {
                    int newRandInt = randInt - 1 > -1 ? newRandInt = randInt - 1 : newRandInt = choiceList.Count - 1;
                    Vector2[] newChoiceForme = choiceList[newRandInt];
                    if (TestCanRotate(newChoiceForme, actualCase))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase - new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x - spaceBetweenCases;
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase + new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x + spaceBetweenCases;
                    }
                }
            }
            else if(canRotate)
            {
                if (Input.GetKeyDown(rotateLeft) || Input.GetMouseButtonDown(0))
                {
                    int newRandInt = randInt + 1 < choiceList.Count ? newRandInt = randInt + 1 : newRandInt = 0;
                    Vector2[] newChoiceForme = choiceList[newRandInt];
                    if (TestCanRotate(newChoiceForme, actualCase))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase - new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x - spaceBetweenCases;
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase + new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x + spaceBetweenCases;
                    }
                }
                if (Input.GetKeyDown(rotateRight) || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    int newRandInt = randInt - 1 > -1 ? newRandInt = randInt - 1 : newRandInt = choiceList.Count - 1;
                    Vector2[] newChoiceForme = choiceList[newRandInt];
                    if (TestCanRotate(newChoiceForme, actualCase))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase + new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x + spaceBetweenCases;
                    }
                    else if (TestCanRotate(newChoiceForme, actualCase - new Vector3(1, 0, 0) * spaceBetweenCases))
                    {
                        ClearColor(choiceForme, actualCase);
                        randInt = newRandInt;
                        choiceForme = choiceList[randInt];
                        actualCase.x = actualCase.x - spaceBetweenCases;
                    }
                }
            }
            if(TestCanMove(choiceForme, actualCase, new Vector2(spaceBetweenCases, 0)))
            {
                if (Input.GetKeyDown(moveRight) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    ClearColor(choiceForme, actualCase);
                    actualCase.x = actualCase.x + spaceBetweenCases;
                }
            }
            
            if(TestCanMove(choiceForme, actualCase, new Vector2(-spaceBetweenCases, 0)))
            {
                if (Input.GetKeyDown(moveLeft) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    ClearColor(choiceForme, actualCase);
                    actualCase.x = actualCase.x - spaceBetweenCases;
                }
            }
            if (Input.GetKeyDown(moveDown) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (canMoveDownWithKey)
                {
                    ClearColor(choiceForme, actualCase);
                    while (TestCanMove(choiceForme, actualCase, new Vector2(0, -spaceBetweenCases)))
                    {
                        actualCase.z = actualCase.z - spaceBetweenCases;
                    }

                    StopCoroutine(gameTime);
                    canMoveBottom = true;
                    gameTime = GameTime(0.25f);
                    StartCoroutine(gameTime);
                    canMoveDownWithKey = false;
                }
            }
            /* pour la piece de 5 cases
            if (Input.GetKeyDown(rotateRight) || Input.GetKeyDown(rotateLeft))
            {
                if (choiceList == Line5PieceList && actualCase.z == 11)
                {
                    ClearColor(choiceForme, actualCase);
                    actualCase.z = actualCase.z - spaceBetweenCases;
                }
            }*/

            foreach (Vector2 positions in choiceForme)
            {
                Debug.DrawRay(actualCase + new Vector3(positions.x * reversed, -1, positions.y) * spaceBetweenCases, Vector2.up * 10, Color.red);
                if (Physics.Raycast(actualCase + new Vector3(positions.x * reversed, -1, positions.y) * spaceBetweenCases, Vector3.up, out RaycastHit hit, 2 * spaceBetweenCases))
                {
                    if (hit.transform.tag == "Case")
                    {
                        hit.transform.GetComponent<Renderer>().material.color = caseColor;
                        /*if (setOccupied)
                        {
                            setOccupied = false;
                            hit.transform.tag = "Occupied";
                        }*/
                    }
                }
            }

            foreach (Vector2 vectors in nextChoiceForme)
            {
                if (Physics.Raycast(spawnCaseNextPiecePos + new Vector3(vectors.x * nextReversed, -1, vectors.y) * spaceBetweenCases, Vector3.up, out RaycastHit hit, 2 * spaceBetweenCases))
                {
                    if (hit.transform.tag == "NextPiece")
                    {
                        hit.transform.GetComponent<Renderer>().material.color = nextCaseColor;
                    }
                }
            }
        }

        
    }
    public void ClearGame(GameStates newState)
    {
        if(newState == GameStates.RunGame)
        {
            score = 0;

            for (int i = 0; i < nbCasesWidth; i++)
            {
                for (int ib = 0; ib < nbCasesHeight; ib++)
                {
                    if (Physics.Raycast(firstCasePos + new Vector3(i, -1, ib) * spaceBetweenCases, Vector3.up, out RaycastHit hit, 2 * spaceBetweenCases))
                    {
                        if (hit.transform.tag == "Occupied")
                        {
                            hit.transform.GetComponent<Renderer>().material.color = Color.black;
                            hit.transform.tag = "Case";// vérifier que ça marche et l'appeler avant le commencement du jeu
                        }
                    }
                }
            }
        }
    }

    private bool TestCanRotate(Vector2[] choiceNextForme, Vector3 actualPosition)
    {
        foreach (Vector2 vectors in choiceNextForme)
        {
            if (Physics.Raycast(actualPosition + new Vector3(vectors.x * reversed, -1, vectors.y) * spaceBetweenCases, Vector3.up, out RaycastHit hit, 2 * spaceBetweenCases))
            {
                if (hit.transform.tag == "Occupied")
                {
                    return false;
                }
            }

            if (actualPosition.x + vectors.x * reversed * spaceBetweenCases < firstCasePos.x)
            {
                return false;
            }
            else if (actualPosition.x + vectors.x * reversed * spaceBetweenCases > firstCasePos.x + spaceBetweenCases * (nbCasesWidth - 1))
            {
                return false;
            }
            else if (actualPosition.z + vectors.y * reversed * spaceBetweenCases < firstCasePos.z)
            {
                return false;
            }
        }

        return true;
    }

    private void OutBorder(Vector2[] choiceForme, Vector3 actualPosition)
    {
        foreach (Vector2 vectors in choiceForme)
        {
            if (actualPosition.x + vectors.x * reversed * spaceBetweenCases < firstCasePos.x)
            {
                actualCase.x = actualCase.x + spaceBetweenCases;

            }
            else if (actualPosition.x + vectors.x * reversed * spaceBetweenCases > firstCasePos.x + spaceBetweenCases * (nbCasesWidth - 1))
            {
                actualCase.x = actualCase.x - spaceBetweenCases;

            }
            else if (actualPosition.z + vectors.y * spaceBetweenCases < firstCasePos.z)
            {
                actualCase.z = actualCase.z + spaceBetweenCases;
            }
        }
    }

    private bool TestCanMove(Vector2[] choiceForme, Vector3 actualPosition, Vector2 wantedMove)
    {
        foreach (Vector2 vectors in choiceForme)
        {
            if (actualPosition.x + spaceBetweenCases * (vectors.x * reversed) + wantedMove.x < firstCasePos.x || actualPosition.x + spaceBetweenCases * (vectors.x * reversed) + wantedMove.x > firstCasePos.x + spaceBetweenCases * (nbCasesWidth - 1))
            {
                return false;

            }
            else if (actualPosition.z + spaceBetweenCases * vectors.y + wantedMove.y < firstCasePos.z)
            {
                return false;
            }
            else if (Physics.Raycast(actualPosition + new Vector3(vectors.x * reversed, -1, vectors.y) * spaceBetweenCases + new Vector3(wantedMove.x, 0, wantedMove.y), Vector3.up, out RaycastHit hit, 2 * spaceBetweenCases))
            {
                if (hit.transform.tag == "Occupied")
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void ClearColor(Vector2[] choiceForme, Vector3 actualPosition)
    {
        foreach (Vector2 positions in choiceForme)
        {
            if (Physics.Raycast(actualPosition + new Vector3(positions.x * reversed, -1, positions.y) * spaceBetweenCases, Vector2.up, out RaycastHit hit, 2 * spaceBetweenCases))
            {
                if (hit.transform.tag == "Case")
                {
                    hit.transform.GetComponent<Renderer>().material.color = Color.black;
                }
            }
        }
    }

    IEnumerator GameTime(float timeToWait = 0)
    {
        canMoveBottom = false;
        int diff = GameManager.instance.difficulty;
        gameSpeed = diff == 0 || diff == 3 ? gameSpeed = 1 : diff == 1 ? gameSpeed = 1.5f : gameSpeed = 2;
        yield return new WaitForSeconds(timeToWait / gameSpeed);
        canMoveBottom = true;
    }

    void DefAttributs()
    {
        int diff = GameManager.instance.difficulty;
        int[] frequencyList = diff == 0 || diff == 3 ? frequencyList = frequencyListFacile : diff == 1 ? frequencyList = frequencyListNormal : frequencyList = frequencyListDifficile;

        int choiceInt = Random.Range(0, 20);
        if (nbChosed == choiceInt)
        {
            time++;
            if(time >= maxTimeSamePiece)
            {
                while(choiceInt == nbChosed)
                {
                    choiceInt = Random.Range(0, 20);
                }
                time = 0;
            }
        }
        else
        {
            time = 0;
        }
        choiceInt = frequencyList[choiceInt];
        nbChosed = choiceInt;
        if (choiceInt == 0)
        {
            localChoiceList = LPieceList;
            localCanRotate = true;
            localCanReverse = true;
        }
        else if (choiceInt == 1)
        {
            localChoiceList = ZPieceList;
            localCanRotate = true;
            localCanReverse = true;
        }
        else if (choiceInt == 2)
        {
            localChoiceList = Line4PieceList;
            localCanRotate = true;
            localCanReverse = false;
        }
        else if (choiceInt == 3)
        {
            localChoiceList = SquarePieceList;
            localCanRotate = false;
            localCanReverse = false;
        }
        else if (choiceInt == 4)
        {
            localChoiceList = CoinPieceList;
            localCanRotate = true;
            localCanReverse = false;
        }
        else if (choiceInt == 5)
        {
            localChoiceList = TPieceList;
            localCanRotate = true;
            localCanReverse = false;
        }
        else if(choiceInt == 6)
        {
            localChoiceList = pointPieceList;
            localCanRotate = false;
            localCanReverse = false;
        }
        else if(choiceInt == 7)
        {
            localChoiceList = Line3PieceList;
            localCanRotate = true;
            localCanReverse = false;
        }
        else
        {
            localChoiceList = Line5PieceList;
            localCanRotate = true;
            localCanReverse = false;
        }

        localRandInt = Random.Range(0, localChoiceList.Count);
        if(localChoiceList == Line5PieceList)
        {
            localRandInt = 1;
        }
        localChoiceForme = localChoiceList[localRandInt];
        localReversed = 1;
        if (localCanReverse)
        {
            localReversed = Random.Range(0, 2) == 1 ? localReversed = 1 : localReversed = -1;
        }

        float x = Random.Range(0, 36);
        float sColor = 1f;
        float vColor = 0.8f;
        localCaseColor = Color.HSVToRGB(x / 36, sColor, vColor);
        //localCaseColor = x == 0 ? localCaseColor = Color.HSVToRGB(0, sColor, vColor) : x == 1 ? localCaseColor = Color.HSVToRGB(50, sColor, vColor) : x == 2 ? localCaseColor = Color.HSVToRGB(100, sColor, vColor) : x == 3 ? localCaseColor = Color.HSVToRGB(150, sColor, vColor) : x == 4 ? localCaseColor = Color.HSVToRGB(200, sColor, vColor) : localCaseColor = Color.HSVToRGB(250, sColor, vColor);
    }

    private void UpdateTouches(KeyCode[] keyCodeArray)
    {
        rotateRight = keyCodeArray[0];
        rotateLeft = keyCodeArray[1];
        moveRight = keyCodeArray[2];
        moveLeft = keyCodeArray[3];
        moveDown = keyCodeArray[4];
    }
}