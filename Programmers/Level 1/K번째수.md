# K번째수

```cpp
배열 array의 i번째 숫자부터 j번째 숫자까지 자르고 정렬했을 때, k번째에 있는 수를 구하려 합니다.

예를 들어 array가 [1, 5, 2, 6, 3, 7, 4], i = 2, j = 5, k = 3이라면

array의 2번째부터 5번째까지 자르면 [5, 2, 6, 3]입니다.
1에서 나온 배열을 정렬하면 [2, 3, 5, 6]입니다.
2에서 나온 배열의 3번째 숫자는 5입니다.
배열 array, [i, j, k]를 원소로 가진 2차원 배열 commands가 매개변수로 주어질 때, commands의 모든 원소에 대해 앞서 설명한 연산을 적용했을 때 나온 결과를 배열에 담아 return 하도록 solution 함수를 작성해주세요.
```

쉽게 눈치챌 수 있는 함정이 있다. 저기서 *i*와 *j*는 0부터 시작하는 일반적인 프로그래밍 언어의 배열 인덱스가 아니다. 배열의 인덱스 0을 1로 표현하고 있다. 그러므로 정확히는 *i-1* 번째부터 *j* 번째 인덱스 까지다. 반환하는 *k* 번째 역시 배열에서는 *k-1*번째 인덱스가 된다. 

## vector::assign()

반복문을 이용해서 *i*부터 *j*까지의 값을 가져올 수 있겠지만 `C++ STL`에서는 좀 더 편한 메서드를 제공한다.

```
template <class InputIterator>
void assign ( InputIterator first, InputIterator last );
void assign ( size_type n, const T& u );
```

첫 번째 메서드의 경우 `first`부터 `last` 이전까지의 모든 원소의 내용이 들어가게 된다. 두 번째 메서드는 원소 `u`를 `n`개 가지는 벡터를 만든다.

`vector.assign()` 메서드를 이용해서 *i-1*부터 *j*까지 쉽게 할당할 수 있다.

```cpp
vector<int> a { 1, 2, 3, 4, 5, 6, 7, 8, 9};
vector<int> b;
int i = 2, j = 5, k = 3;
b.assign(a.begin() + i - 1, a.begin() + j); // {2, 3, 4, 5, 6}
return b[k-1];
```

위 내용을 적용해서 반복문으로 풀은 내용은 아래와 같다.

```cpp
vector<int> solution(vector<int> array, vector<vector<int>> commands) 
{
    vector<int> answer;

    vector<int> temps;
    for (vector<int>& arr : commands)
    {
        temps.clear();

        temps.assign(array.begin() + arr[0] - 1, array.begin() + arr[1]);
        std::sort(temps.begin(), temps.end());

        answer.push_back(temps[arr[2] - 1]);
    }

    return answer;
}
```

---