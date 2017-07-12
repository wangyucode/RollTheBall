using System;
using System.Collections.Generic;

[Serializable]
public class WyResultRanking
{

    public int code;//(integer, optional): code: 返回代码，1表示OK，其它的都有对应问题 ,
    public int data;//(Array[GameScore], optional): data : code为1时返回结果集 ,
    public string message;//(string, optional): message : code!=1时的错误信息

}