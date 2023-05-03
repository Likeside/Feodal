using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TokenType { Operator, Parenthesis, Integer, Float, Variable }

[System.Serializable]
public class GRES_Token
{
    private string token;
    private float tokenF;

    public TokenType type;
    private bool shortPrint = true;

    public GRES_Token(string token, TokenType type)
    {
        this.type = type;
        this.token = token;

        if (this.type == TokenType.Float)
            float.TryParse(this.token, out tokenF);
    }

    public GRES_Token(GRES_Token t)
    {
        token = t.token;
        tokenF = t.tokenF;
        type = t.type;
    }

    public GRES_Token(char c)
    {
        token = c.ToString();

        if (c == '(' || c == ')')
            type = TokenType.Parenthesis;
        else
            type = TokenType.Operator;
    }

    public GRES_Token(float f)
    {
        tokenF = f;
        token = f.ToString();
        type = TokenType.Float;
    }

    public GRES_Token(int i)
    {
        token = i.ToString();
        type = TokenType.Integer;
    }

    public override string ToString()
    {
        if (shortPrint)
            return token;

        string r = "";

        if (type == TokenType.Float)
            r = tokenF + "F";
        else if (type == TokenType.Integer)
            r = token + "I";
        else if (type == TokenType.Operator)
            r = "op:" + token;
        else if (type == TokenType.Variable)
            r = "v:" + token;

        return r;
    }

    public bool IsEmpty()
    {
        return token.Length == 0;
    }

    public int Precedence()
    {
        if (token == "+" || token == "-") return 1;
        else if (token == "*" || token == "/") return 2;
        else if (token == "^") return 3;
        else if (token == "%") return 4;
        else return 99;
    }

    public bool IsOp()
    {
        return type == TokenType.Operator;
    }

    public bool IsNumber()
    {
        return type == TokenType.Integer || type == TokenType.Float;
    }

    public int TI()
    {
        if (type == TokenType.Integer)
        {
            int.TryParse(token, out int i);
            return i;
        }

        if (type == TokenType.Float)
            return Mathf.RoundToInt(tokenF);

        return 0;
    }

    public float TF()
    {
        if(type == TokenType.Float)
            return tokenF;
        else if (type == TokenType.Integer)
        {
            int.TryParse(token, out int i);
            return i;

        }

        return float.NaN;
    }

    public string TS()
    {
        return token;
    }
}