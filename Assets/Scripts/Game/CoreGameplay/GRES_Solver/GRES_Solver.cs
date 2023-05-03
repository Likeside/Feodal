using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GRES_Solver
{
    public string infix = "";

    private List<GRES_Token> tokens;
    private List<GRES_Token> postfix;

    private bool debugMode = false;
    public bool printPostfix = false;

    public float variable;

    public bool ready = false;
    private float lastEvaluation = float.NaN;

    public GRES_Solver()
    {
    }

    public GRES_Solver(string expression)
    {
        infix = expression;
    }

    private bool TokenChar(char c)
    {
        return ('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z') || ('0' <= c && c <= '9') || c == '.' || c == '$' || c == '#';
    }

    private bool OperatorChar(char c)
    {
        return c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '%';
    }

    private string StringifyTokenList(List<GRES_Token> list, bool print = false, bool reverse = false)
    {
        string[] array = new string[list.Count];

        for (int i = 0; i < array.Length; i++)
        {
            array[reverse ? array.Length - 1 - i : i] = list[i].ToString();
        }

        string str = "[" + string.Join(" _ ", array) + "]";

        if (print)
            Debug.Log(str);

        return str;
    }

    private GRES_Token Last(List<GRES_Token> list)
    {
        return list[list.Count - 1];
    }


    private GRES_Token MakeToken(string token)
    {
        if (int.TryParse(token.Substring(0, token.Length - 1), out _) && token[token.Length - 1] == 'i')
        {
            return new GRES_Token(token.Substring(0, token.Length - 1), TokenType.Integer);
        }
        else if (token[0] == '$')
        {
            return new GRES_Token("$", TokenType.Variable);
        }

        return new GRES_Token(token, TokenType.Float);
    }

    private List<GRES_Token> ClonePostfix()
    {
        List<GRES_Token> clone = new List<GRES_Token>();

        for (int i = 0; i < postfix.Count; i++)
        {
            clone.Add(new GRES_Token(postfix[i]));
        }

        return clone;
    }

    public void SetExpression(string expression)
    {
        infix = expression;
        ready = false;
    }

    private void SeparateTokens()
    {
        infix = infix.Trim();

        tokens = new List<GRES_Token>();
        string token = "";

        for (int i = 0; i < infix.Length; i++)
        {
            if (TokenChar(infix[i]))
            {
                token += infix[i];
            }
            else if (infix[i] == ' ' || infix[i] == '\t' || infix[i] == '\n')
            {
                if (token.Length != 0)
                {
                    tokens.Add(MakeToken(token));
                }
                token = "";
            }
            else
            {
                if (token.Length != 0)
                    tokens.Add(MakeToken(token));

                tokens.Add(new GRES_Token(infix[i]));
                token = "";
            }
        }

        if (token.Length != 0)
            tokens.Add(MakeToken(token));

        if (debugMode)
        {
            Debug.Log("Separate Tokens:");
            StringifyTokenList(tokens, true);
        }
    }

    private void AnalyzeTokens()
    {
        if (tokens.Count == 0)
        {
            tokens.Add(new GRES_Token(0));
        }

        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].TS().ToLower() == "pi") tokens[i] = new GRES_Token(Mathf.PI);
            if (tokens[i].TS().ToLower() == "halfpi") tokens[i] = new GRES_Token(Mathf.PI / 2);
            if (tokens[i].TS().ToLower() == "twopi") tokens[i] = new GRES_Token(Mathf.PI * 2);
            if (tokens[i].TS().ToLower() == "quarterpi") tokens[i] = new GRES_Token(Mathf.PI / 4);
            if (tokens[i].TS().ToLower() == "e") tokens[i] = new GRES_Token(2.7182818285f);
        }

        if (tokens[0].TS() == "-" && tokens.Count > 1)
        {
            if(tokens[1].type == TokenType.Integer)
            {
                tokens.RemoveAt(0);
                tokens[0] = new GRES_Token(-1 * tokens[0].TI());
            }
            else if (tokens[1].type == TokenType.Float)
            {
                tokens.RemoveAt(0);
                tokens[0] = new GRES_Token(-1 * tokens[0].TF());
            }
            else
            {
                tokens[0] = new GRES_Token("-1", TokenType.Float);
                tokens.Insert(1, new GRES_Token('*'));
            }
        }

        for (int i = tokens.Count - 2; i > 0; i--)
        {
            GRES_Token t0 = tokens[i - 1];
            GRES_Token t1 = tokens[i];
            GRES_Token t2 = tokens[i + 1];

            if (t1.TS() == "-" && (t0.IsOp() || t0.TS() == "("))
            {
                tokens.RemoveAt(i);

                if (t2.type == TokenType.Variable)
                {
                    tokens.Insert(i + 1, new GRES_Token(')'));
                    tokens.Insert(i, new GRES_Token('*'));
                    tokens.Insert(i, new GRES_Token(-1f));
                    tokens.Insert(i, new GRES_Token('('));
                }
                else if(t2.type == TokenType.Parenthesis)
                {
                    tokens.Insert(ParenthesisEnd(i), new GRES_Token(')'));
                    tokens.Insert(i, new GRES_Token('*'));
                    tokens.Insert(i, new GRES_Token(-1f));
                    tokens.Insert(i, new GRES_Token('('));
                }
                else if(t2.IsNumber())
                {
                    tokens[i] = new GRES_Token(t2.type == TokenType.Integer? -t2.TI() : -t2.TF());
                }
            }
        }

        if (debugMode)
        {
            Debug.Log("Fix Tokens:");
            StringifyTokenList(tokens, true);
        }
    }

    private int ParenthesisEnd(int i)
    {
        int nest = 1;
        int p = i + 2;
        for (; p < tokens.Count && nest > 0; p++)
        {
            if (tokens[p].TS() == "(") nest++;
            else if (tokens[p].TS() == ")") nest--;
        }

        return p;
    }

    public void MakePostfix()
    {
        postfix = new List<GRES_Token>();
        List<GRES_Token> op = new List<GRES_Token>();

        string debug = "";

        for (int i = 0; i < tokens.Count; i++)
        {
            if (debugMode) debug = tokens[i] + " -> ";

            if (tokens[i].TS() == "(")
            {
                op.Add(tokens[i]);
            }
            else if (tokens[i].TS() == ")")
            {
                while (op.Count > 0 && Last(op).TS() != "(")
                {
                    postfix.Add(Last(op));
                    op.RemoveAt(op.Count - 1);
                }
                op.RemoveAt(op.Count - 1);
            }
            else if (tokens[i].IsOp())
            {
                if (op.Count == 0)
                {
                    op.Add(tokens[i]);
                }
                else
                {
                    while (op.Count > 0 && Last(op).TS() != "(" &&
                          ((Last(op).TS() != "^" && Last(op).Precedence() >= tokens[i].Precedence()) ||
                          (Last(op).TS() == "^" && tokens[i].TS() != "^")))
                    {
                        postfix.Add(Last(op));
                        op.RemoveAt(op.Count - 1);
                    }

                    op.Add(tokens[i]);
                }
            }
            else if (!tokens[i].IsEmpty())
            {
                postfix.Add(tokens[i]);
            }

            if (debugMode)
            {
                debug += StringifyTokenList(postfix) + " - ";
                debug += StringifyTokenList(op, false, true);

                Debug.Log(debug);
            }
        }

        for (int i = op.Count - 1; i >= 0; i--)
        {
            postfix.Add(op[i]);
        }

        ready = true;

        if (debugMode) StringifyTokenList(postfix, true);
    }

    public float Evaluate()
    {
        if(postfix == null || !ready) {
            Prepare();
        }

        List<GRES_Token> clone = ClonePostfix();
        if (printPostfix)
            StringifyTokenList(clone, true);

        string debug = "";

        for (int i = 0; i < clone.Count; i++)
        {
            if (debugMode) debug = clone[i] + "   ";

            if (clone[i].IsOp() || clone[i].type == TokenType.Parenthesis)
            {
                string op = clone[i].TS();

                GRES_Token[] argTokens = new GRES_Token[2];

                argTokens[0] = clone[i - 2];
                argTokens[1] = clone[i - 1];

                clone[i] = ExecuteOperation(op, argTokens);

                for (int a = 0; a < 2; a++)
                    clone.RemoveAt(i - 2);

                i -= 2;

                if (debugMode)
                {
                    debug += StringifyTokenList(clone) + "   " + string.Join<GRES_Token>(", ", argTokens) + " - " + op + " = " + clone[i];
                    Debug.Log(debug);
                }
            }
            else
                if (debugMode) Debug.Log(StringifyTokenList(clone));
        }

        if (clone.Count == 1)
        {
            float result = 0;

            if (clone[0].IsNumber())
                result = clone[0].TF();
            else if (clone[0].type == TokenType.Variable)
                result = variable;

            lastEvaluation = result;
            return result;
        }

        lastEvaluation = float.NaN;
        return float.NaN;
    }

    public float LastEvaluation() {
        return lastEvaluation;
    }

    public void Prepare()
    {
        SeparateTokens();
        AnalyzeTokens();
        MakePostfix();
    }

    private GRES_Token ExecuteOperation(string op, GRES_Token[] argTokens)
    {
        float[] args = new float[argTokens.Length];
        for (int a = 0; a < argTokens.Length; a++)
        {
            if (argTokens[a].type == TokenType.Variable)
                args[a] = variable;
            else
                args[a] = argTokens[a].TF();
        }

        float val = 0;

        if (op == "+") val = args[0] + args[1];
        else if (op == "-") val = args[0] - args[1];
        else if (op == "/") val = args[0] / args[1];
        else if (op == "*") val = args[0] * args[1];
        else if (op == "^") val = Mathf.Pow(args[0], args[1]);
        else if (op == "%") val = args[0] % args[1];

        return new GRES_Token(val);
    }


    public void SetVariable(float value)
    {
        variable = value;
    }
}