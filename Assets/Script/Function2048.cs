using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Function2048  {
    private static Function2048 function;
    /// <summary>
    /// 排列的起始位置
    /// </summary>
    private int iStartPosX=-90,iStartPosY=90;
    /// <summary>
    /// 精灵的宽度
    /// </summary>
    private int iSpriteWidth=60;
    /// <summary>
    /// 方格的二维数组
    /// </summary>
    private SquareSprite[,] squareSpite = new SquareSprite[4, 4];
    /// <summary>
    /// 游戏状态
    /// </summary>
    public bool isStopGame = false;
    /// <summary>
    /// 分数
    /// </summary>
    public int iSocre = 0;

    public static Function2048 getInstance()
    {
        if (function == null)
        {
            function = new Function2048();
        }
        return function;
    }

    /// <summary>
    /// 绘制背景
    /// </summary>
    /// <param name="goPrefab">预设</param>
    /// <param name="parent">父物体</param>
    public void DrawBackGround(GameObject goPrefab,Transform parent)
    {
        for (int i = 0; i < squareSpite.GetLength(0); i++)
        {
            for (int j = 0; j < squareSpite.GetLength(1); j++)
            {
                GameObject go = GameObject.Instantiate(goPrefab) as GameObject;
                go.transform.parent = parent;
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = new Vector3(iStartPosX+iSpriteWidth*i,iStartPosY-iSpriteWidth*j);

                squareSpite[i, j] = new SquareSprite();
                squareSpite[i, j].sprite = go.GetComponent<Image>();
                squareSpite[i, j].text = go.transform.FindChild("Text").GetComponent<Text>();
                squareSpite[i,j].grid= new Vector2(i,j);
                squareSpite[i, j].num = 0;
            }
        }
        isStopGame = false;
       
    }
    /// <summary>
    /// 初始化游戏
    /// </summary>
    public void InitGame()
    {
        iSocre = 0;
        isStopGame = false;
        for (int i = 0; i < squareSpite.GetLength(0); i++)
        {
            for (int j = 0; j < squareSpite.GetLength(1); j++)
            {
                squareSpite[i, j].num=0;
            }
        }
        //创建初始数据
        for (int i = 0; i < 2; i++)
        {
            CreateNumber();
        }
    }
    /// <summary>
    /// 创建数字
    /// </summary>
    private void CreateNumber()
    {
        int iHaveNumber=0;
        bool isCreateNumber=true;
        for (int i = 0; i < squareSpite.GetLength(0); i++)
        {
            for (int j = 0; j < squareSpite.GetLength(1); j++)
            {
                if(squareSpite[i,j].num!=0)
                    iHaveNumber++;
            }
        }
        if (iHaveNumber < 16)
        {
            while (isCreateNumber)
            {
                int x = UnityEngine.Random.Range(0, squareSpite.GetLength(0));
                int y = UnityEngine.Random.Range(0, squareSpite.GetLength(1));
                if (squareSpite[x, y].num == 0)
                {
                    Debug.Log(iHaveNumber);
                    squareSpite[x, y].num = UnityEngine.Random.Range(0, 10) < 7 ? 2 : 4;
                    isCreateNumber = false;
                    break;
                }
            }
        }
        else
        {
            isStopGame = true;
        }
    }
    public void MoveSquare(string dir)
    {
        switch (dir)
        {
            case "up":
                for (int i = 0; i < squareSpite.GetLength(0); i++)
                {
                    for (int j = 0; j < squareSpite.GetLength(1); j++)
                    {
                        //如果方格里面的值为0
                        if (squareSpite[i, j].num == 0)
                        {
                            //找到[i,j]下面的所有数值
                            for (int k = j + 1; k < squareSpite.GetLength(1); k++)
                            {
                                if (squareSpite[i, k].num != 0)
                                {
                                    squareSpite[i, j].num = squareSpite[i, k].num;
                                    squareSpite[i, k].num = 0;
                                    j--;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int k = j + 1; k < squareSpite.GetLength(1); k++)
                            {
                                if (squareSpite[i, k].num == squareSpite[i, j].num)
                                {
                                    squareSpite[i, j].num += squareSpite[i, k].num;
                                    squareSpite[i, k].num = 0;
                                    j--;
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
            case "down":
                for (int i = 0; i < squareSpite.GetLength(0); i++)
                {
                    for (int j = squareSpite.GetLength(1)-1; j >=0; j--)
                    {
                        //如果方格里面的值为0
                        if (squareSpite[i, j].num == 0)
                        {
                            //找到[i,j]下面的所有数值
                            for (int k = j - 1; k >=0; k--)
                            {
                                if (squareSpite[i, k].num != 0)
                                {
                                    squareSpite[i, j].num = squareSpite[i, k].num;
                                    squareSpite[i, k].num = 0;
                                    j++;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int k = j - 1; k >=0; k--)
                            {
                                if (squareSpite[i, k].num == squareSpite[i, j].num)
                                {
                                    squareSpite[i, j].num += squareSpite[i, k].num;
                                    squareSpite[i, k].num = 0;
                                    j++;
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
            case "left":
                for (int j = 0; j < squareSpite.GetLength(1); j++)
                {
                    for (int i = 0; i < squareSpite.GetLength(0); i++)
                    {
                        //如果方格里面的值为0
                        if (squareSpite[i, j].num == 0)
                        {
                            //找到[i,j]下面的所有数值
                            for (int k = i+1; k <squareSpite.GetLength(0); k++)
                            {
                                if (squareSpite[k, j].num != 0)
                                {
                                    squareSpite[i, j].num = squareSpite[k, j].num;
                                    squareSpite[k, j].num = 0;
                                    i--;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int k = i + 1; k < squareSpite.GetLength(0); k++)
                            {
                                if (squareSpite[k,j].num == squareSpite[i, j].num)
                                {
                                    squareSpite[i, j].num += squareSpite[k,j].num;
                                    squareSpite[k,j].num = 0;
                                    i--;
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
            case "right":
                for (int j = 0; j < squareSpite.GetLength(1); j++)
                {
                    for (int i = squareSpite.GetLength(0)-1; i >=0; i--)
                    {
                        //如果方格里面的值为0
                        if (squareSpite[i, j].num == 0)
                        {
                            //找到[i,j]下面的所有数值
                            for (int k = i - 1; k >=0; k--)
                            {
                                if (squareSpite[k, j].num != 0)
                                {
                                    squareSpite[i, j].num = squareSpite[k, j].num;
                                    squareSpite[k, j].num = 0;
                                    i++;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (squareSpite[k, j].num == squareSpite[i, j].num)
                                {
                                    squareSpite[i, j].num += squareSpite[k, j].num;
                                    squareSpite[k, j].num = 0;
                                    i++;
                                    break;
                                }
                            }
                        }
                    }
                }
                break;
        }
    }

    /// <summary>
    /// 处理更新游戏
    /// </summary>
    public void DealUpdateGame()
    {
        if (isStopGame)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
            MoveSquare("up");
            CreateNumber();
            AddSocre();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            MoveSquare("down");
            CreateNumber();
            AddSocre();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.A))
        {
            MoveSquare("left");
            CreateNumber();
            AddSocre();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.D))
        {
            MoveSquare("right");
            CreateNumber();
            AddSocre();
        }

        for (int i = 0; i < squareSpite.GetLength(0); i++)
        {
            for (int j = 0; j < squareSpite.GetLength(1); j++)
            {
                squareSpite[i, j].text.text = squareSpite[i, j].num + "";
                squareSpite[i, j].sprite.color = GetSquareColor(squareSpite[i, j].num);
            }
        }
    }
    /// <summary>
    /// 计算总分
    /// </summary>
    private void AddSocre()
    {
        iSocre = 0;
        for (int i = 0; i < squareSpite.GetLength(0); i++)
        {
            for (int j = 0; j < squareSpite.GetLength(1); j++)
            {
                squareSpite[i, j].text.text = squareSpite[i, j].num + "";
                squareSpite[i, j].sprite.color = GetSquareColor(squareSpite[i, j].num);
                iSocre += squareSpite[i, j].num;
            }
        }
    }
    /// <summary>
    /// 获取到颜色
    /// </summary>
    /// <param name="num">标签</param>
    /// <returns></returns>
    private Color GetSquareColor(int num)
    {
        Color color = Color.white;
        switch (num)
        {
            case 0:
               break;
            case 2:
               color.r = 240;
               color.g = 227;
               color.b = 0;
               break;
            case 4:
                color.r = 237;
                color.g = 0;
                color.b = 200;
                break;
            case 8:
                color = Color.blue;
                break;
            case 16:
                color.r = 245;
                color.g = 149;
                color.b = 0;
                break;
            case 32:
                color.r = 0;
                color.g = 124;
                color.b = 95;
                break;
            case 64:
                color = Color.gray;
                break;
            case 128:
                color.r = 245;
                color.g = 204;
                color.b = 0;
                break;
            case 256:
                color = Color.green;
                break;
            case 512:
                color.r = 0;
                color.g = 204;
                color.b = 97;
                break;
            case 1024: 
                color.r = 237;
                color.g = 0;
                color.b = 97;
                break;
            case 2048:
                color = Color.red;
                break;
            case 4096:
                color.r = 0;
                color.g = 137;
                color.b = 243;
                break;
            case 8192:
                color.r = 0;
                color.g = 213;
                color.b = 201;
                break;
            case 16384:
                color.r = 237;
                color.g = 204;
                color.b = 0;
                break;
            default:
                color = Color.white;
                break;
        }
        return color;
       
    }

}

class SquareSprite
{
    public Vector2 grid;
    public int num;
    public Text text;
    public Image sprite;
}

