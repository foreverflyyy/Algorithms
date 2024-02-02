from typing import List

import pandas as pd


def dropDuplicateEmails(customers: pd.DataFrame) -> pd.DataFrame:
    return customers.drop_duplicates(subset='email')[['customer_id', 'name', 'email']]

def dropMissingData(students: pd.DataFrame) -> pd.DataFrame:
    return students[students['name'].notnull()]

def doubleNum(number):
    if number is None:
        return None
    return int(number) * 2

def modifySalaryColumn(employees: pd.DataFrame) -> pd.DataFrame:
    return employees.assign(salary=2 * employees['salary'])
    # employees["salary"] = list(map(doubleNum, employees["salary"]))
    # return employees


def renameColumns(students: pd.DataFrame) -> pd.DataFrame:
    students = students.rename(
        columns={
            "id": "student_id",
            "first": "first_name",
            "last": "last_name",
            "age": "age_in_years",
        }
    )
    return students

def changeType(number):
    if number is None:
        return None
    return int(number)

def changeDatatype(students: pd.DataFrame) -> pd.DataFrame:
    students["grade"] = list(map(doubleNum, students["grade"]))
    return students

def fillMissingValues(products: pd.DataFrame) -> pd.DataFrame:
    products['quantity']= products['quantity'].fillna(0)
    return products

def concatenateTables(df1: pd.DataFrame, df2: pd.DataFrame) -> pd.DataFrame:
    return pd.concat([df1, df2], ignore_index=True)


def pivotTable(weather: pd.DataFrame) -> pd.DataFrame:
    return weather.pivot(values='temperature', index=['month'], columns=['city'])
    # return pd.pivot_table(weather, values='temperature', index=['month'], columns=['city'], aggfunc=max, fill_value=0)

    # data = {}
    # months = set()
    # for row in weather.iterrows():
    #     city = row[1][0]
    #     month = row[1][1]
    #     temp = row[1][2]
    #
    #     if not data.get(city):
    #         data.setdefault(city, {month: temp})
    #     else:
    #         data[city].setdefault(month, temp)
    #     months.add(month)
    #
    # result_df = pd.DataFrame()
    # result_df['month'] = list(months)
    # for city in list(data.keys()):
    #     temps = []
    #     data_by_city = data[city]
    #     for month in months:
    #         temps.append(data_by_city[month])
    #     result_df[city] = temps
    # return result_df

def meltTable(report: pd.DataFrame) -> pd.DataFrame:
    return report.melt(id_vars=["product"], var_name="quarter", value_name="sales")



def findHeavyAnimals(animals: pd.DataFrame) -> pd.DataFrame:
    data = animals.sort_values(by='weight', ascending=False)
    return data.loc[data.weight > 100, ["name"]]

# data_frame = pd.DataFrame(
#     [["Umbrella",417,224,379,611],
#         ["SleepingBag",800,936,93,875]],
#     columns=["product", "quarter_1", "quarter_2", "quarter_3", "quarter_4"])
data_frame = pd.DataFrame(
    [["Umbrella",417],
        ["SleepingBag",800],
        ["Jonathan",41]],
    columns=["name", "weight"])
print(findHeavyAnimals(data_frame))
