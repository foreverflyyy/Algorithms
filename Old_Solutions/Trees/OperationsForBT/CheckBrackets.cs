namespace CheckBracket;

public class CheckBrackets
{
    static bool StackEmpty(string[] stack)
        {
            if (stack[0] == "")
                return true;
            else
                return false;
        }

    static void PushStack(ref string[] stack, string smb)
        {
            for (int i = 0; i < stack.Length; i++)
                if (stack[i] == "")
                {
                    stack[i] = smb;
                    break;
                }
        }

    static void PopStack(ref string[] stack)
        {
            if (!StackEmpty(stack))
                for (int i = 0; i < stack.Length; i++)
                    if (stack[i] == "")
                    {
                        stack[i - 1] = "";
                        break;
                    }
        }
    static bool SearchElStack(string[] stack, string elem)
        {
            if (!StackEmpty(stack))
                for (int i = 0; i < stack.Length; i++)
                    if (stack[i] == "")
                        if(stack[i-1] == elem)
                            return true;
            return false;
        }
    public bool Check(string brackets)
        {
            int lenghtLine = brackets.Length;
            
            string[] line = new string[lenghtLine];
            for (int i = 0; i < lenghtLine; i++)
                line[i] = Convert.ToString(brackets[i]);
           
            string[] stack = new string[lenghtLine];
            for (int i = 0; i < lenghtLine; i++)
            {
                stack[i] = "";
            }

            for (int i = 0; i < lenghtLine; i++)
            {
                string symbol;
                symbol = line[i];
                if ((symbol == "}" && SearchElStack(stack, "{")) || (symbol == ")" && SearchElStack(stack, "(")) ||
                    (symbol == "]" && SearchElStack(stack, "[")))
                    PopStack(ref stack);
                else if (symbol == "(" || symbol == "{" || symbol == "[" || symbol == ")" || symbol == "}" || symbol == "]")
                    PushStack(ref stack, line[i]);
            }

            if (StackEmpty(stack))
                return true;
            else 
                return false;
        }
}
