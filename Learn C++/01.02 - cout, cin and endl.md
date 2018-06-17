# 01.02 - cout, cin and endl

## std::cout

이전 포스트의 내용에서 st::cout 객체(iostream 라이브러리)를 사용해서 콘솔에 텍스트를 출력할 수 있었다. 

```cpp
#include <iostream>
 
int main()
{
    std::cout << "Hello world!";
    return 0;
}

// Hello world!
```

같은 행에서 출력 연산자(<<)를 사용해서 둘 이상의 것을 출력할 수 있다.

```cpp
#include <iostream>
 
int main()
{
    int x = 4;
    std::cout << "x is equal to: " << x;
    return 0;
}

// x is equal to: 4
```

아래 코드는 무엇은 출력할까?

```cpp
#include <iostream>
 
int main()
{
    std::cout << "Hi!";
    std::cout << "This blog name is boycoding.";
    return 0;
}

// Hi!This blog name is boycoding.
```

---

## std::endl

여러 줄을 출력하려면 std::endl을 사용하면 된다.

std::cout와 함께 사용하면 std::endl은 줄 바꿈 문자를 삽입하여 커서가 다음 줄의 시작부분으로 이동하게 된다.

```cpp
#include <iostream>
 
int main()
{
    std::cout << "Hi!" << std::endl;
    std::cout << "This blog name is boycoding.";
    return 0;
}

// Hi!
// This blog name is boycoding.
```

---

## std::cin

std::cin은 std::cout의 반대다. 

std::cout은 출력 연산자(<<)를 사용하여 콘솔에 데이터를 출력하고, std::cin은 입력 연산자(>>)를 사용하여 콘솔로부터 사용자의 입력을 읽는다.

이전 포스트에서 변수에 대한 기본적인 이해를 했으므로 std::cin을 사용하여 데이터를 사용자로부터 입력을 받에 변수에 할당이 가능하다.

```cpp
#include <iostream>
 
int main()
{
    std::cout << "Enter a number: "; 
    int x;         // 다음 라인에서 값을 덮어 쓸 것이기 때문에 굳이 변수 x를 초기화할 필요가 없다.
    std::cin >> x; // 콘솔에서 숫자를 입력받고 변수 x에 할당한다.
    std::cout << "You entered " << x << std::endl;
    return 0;
}

// Enter a anumber: 2
// You entered 2
```

위 프로그램을 컴파일하고 직접 실행해 보면 'Enter a number: '이 콘솔창에 출력되고 다음 입력을 기다린다. 숫자를 입력하고(예를 들어 2), Enter 키를 누르면 'You entered '와 함께 방금 입력한 숫자가 출력된다.

이것은 사용자로부터 입력을 받는 쉬운 방법으로 앞으로 많은 포스트에서 사용할 것이다.

- std::cin과 std::cout은 항상 명령문(statement)의 왼쪽에 있다.
- std::cout은 값을 출력하는데 사용된다. (cout = character output)
- std::cin은 입력값을 얻는데 사용된다. (cin = character input)
- 출력 연산자(<<) 는 std::cout과 함께 사용되며 r-value에서 콘솔로 이동하는 방향을 보여준다. (std::cout << 4)
- 입력 연산자(>>) 는 std::cin과 함께 사용되며 콘솔에서 변수로 이동하는 방향을 보여준다. (std::cin >> x)

---

이 포스트의 원문은 [http://www.learncpp.com/cpp-tutorial/1-3a-a-first-look-at-cout-cin-endl/](http://www.learncpp.com/cpp-tutorial/1-3a-a-first-look-at-cout-cin-endl/ ) 입니다.