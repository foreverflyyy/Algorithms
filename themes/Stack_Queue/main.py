import math
from typing import List


class Solution:
    def evalRPN(self, tokens: List[str]) -> int:
        stack = []
        for token in tokens:
            if token not in "+-*/":
                stack.append(int(token))
            else:
                r, l = stack.pop(), stack.pop()
                if token == "+":
                    stack.append(l + r)
                elif token == "-":
                    stack.append(l - r)
                elif token == "*":
                    stack.append(l * r)
                else:
                    stack.append(int(float(l) / r))
        return stack.pop()




solution = Solution()
print(solution.evalRPN(["18"]))
print(solution.evalRPN(["2","1","+","3","*"]))
print(solution.evalRPN(["4","13","5","/","+"]))
print(solution.evalRPN(["10","6","9","3","+","-11","*","/","*","17","+","5","+"]))
print(solution.evalRPN(["3","11","5","+","-"]))
print(solution.evalRPN(["4","-2","/","2","-3","-","-"]))
