using System;
using System.Collections.Generic;

[Serializable]
public class WyResultGameScore{

    public int code;//(integer, optional): code: 返回代码，1表示OK，其它的都有对应问题 ,
    public List<GameScore> data;//(Array[GameScore], optional): data : code为1时返回结果集 ,
    public string message;//(string, optional): message : code!=1时的错误信息

}

[Serializable]
public class GameScore
{
    public int gameId;//(integer, optional): 游戏id 1=滚蛋吧 ,
    public int id;//(integer, optional): 分数id ,
    public string name;//(string, optional): 昵称 ,
    public string platform;//(string, optional): 游戏平台 ,
    public int score;//(integer, optional): 分数 ,
    public string userId;//(string, optional): userId
}