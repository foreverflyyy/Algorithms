using Trees;
using CheckBracket;

namespace LineBracket;

public class LineBracketBT
{
    public int StringToInt(string smb)
    {
        return Convert.ToInt32(smb);
    }
    
    public void CreateLeafTree(ref BinaryTree<int> tree, string line, ref int i)
    {
        string number = "";
        for (; i < line.Length; i++)
        {
            if (line[i] == '(')
            {
                i++;
                CreateLeafTree(ref tree, line, ref i);
                if (i == line.Length-1) return;
                else i++;
            }
            if (line[i] == ')') return;
            if (line[i] == ',') continue;
            
            number += line[i];
            if(line[i+1] == '(' || line[i+1] == ')' || line[i+1] == ',')
            {
                tree.Add(StringToInt(number));
                number = "";
            }
        }
    }
    public BinaryTree<int> CreateTreeWithLine(string line)
    {
        CheckBrackets checkBrackets = new CheckBrackets();
        if(!checkBrackets.Check(line)) 
            throw new Exception("Brackets incorrect!");

        string vertexTreeStr = "";
        int j = 0;
        for (; j < line.Length; j++)
        {
            if (line[j] == '(') break;
            vertexTreeStr += line[j];
        }
        
        int vertexTree = StringToInt(vertexTreeStr);
        BinaryTree<int> tree = new BinaryTree<int>(vertexTree, null);
        
        CreateLeafTree(ref tree, line, ref j);
        return tree;
    }
}