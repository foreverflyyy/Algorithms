class Solution:
    field = []
    length = 0

    def find_queen_by_row(self, row_index):
        row = self.field[row_index]
        for i in range(len(row)):
            if row[i] is True:
                return i
        return -1

    # отмена изменений, либо другая ячейка строки, либо пересобираем доску
    def remove_queen(self, row, col):
        self.field[row][col] = None

        if row == 0:
            return -1, -1
        if col != (self.length - 1):
            return row, col + 1

        index_prev_queen = self.find_queen_by_row(row - 1)
        if index_prev_queen == (self.length - 1):
            return self.remove_queen((row - 1), index_prev_queen)

        self.field[row - 1][index_prev_queen] = None
        return (row - 1), (index_prev_queen + 1)

    def is_not_under_attack(self, row, col) -> bool:
        # проверка, не попадает ли размещение на позицию ферзя под атаку других
        current_row = row
        while current_row > 0:
            current_row -= 1
            if self.field[current_row][col] is True:
                return False

        current_row = row
        current_left_column = col
        current_right_column = col
        while current_row > 0:
            current_row -= 1
            current_left_column -= 1
            current_right_column += 1
            if current_left_column >= 0 \
                    and self.field[current_row][current_left_column] is True:
                return False
            elif current_right_column < self.length \
                    and self.field[current_row][current_right_column] is True:
                return False
            else:
                break
        return True

    def totalNQueens(self, n: int) -> int:
        self.length = n
        for i in range(n):
            row = []
            for j in range(n):
                row.append(None)
            self.field.append(row)

        count = 0
        row = 0
        col = 0
        while True:
            if self.is_not_under_attack(row, col):
                self.field[row][col] = True
                if row == (len(self.field) - 1):
                    count += 1
                    in_first_row_column_index = self.find_queen_by_row(0)
                    if in_first_row_column_index == (len(self.field) - 1):
                        break

                    row = 0
                    col = in_first_row_column_index + 1
                else:
                    row += 1
                    col = 0
            else:
                row, col = self.remove_queen(row, col)
                if row == -1 and col == -1:
                    break
        return count


solution = Solution()
print(solution.totalNQueens(1))
