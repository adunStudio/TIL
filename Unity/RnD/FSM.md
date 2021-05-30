# 유니티 FSM: 유한 상태 머신 (Finite State Machine)

> **유한 상태 머신(Finite State Machine, FSM)**은 게임 에이전트에게 환상적인 지능을 부여하기 위한 선택 도구로 사용되어왔다. 
> 다시 말해, 유한 상태 머신은, 주어지는 모든 시간에서 처해 있을 수 있는 유한개의 상태를 가지고 주어지는 입력에 따라 어떤 상태에서 다른 상태로 전환하거나 출력이나 액션이 일어나게 하는 장치 또는 그런 장치를 나타낸 모델이다.

**상태(State)**: 게임에 정의된 여러 동작, 적 캐릭터뿐만 아니라 게이머에게도 적용될 수 있다.

- Idle, Run, Attack, ... , 공격할 수 없는 상태, 캐릭터의 마나가 없어 마법 공격할 수 없는 상태 등
- 한 상태에서 다른 상태로 전화할 수 있고, 동시에 여러 상태를 실행할 수는 없다.

**전이(Transition)**: 한 상태에서 다른 상태로 전화하는 것

- 각 상태 로직 또는 외부에서 전이 조건에 의해 전이될 수 있다.

![](Resource\FSM_1.PNG)

**장점**

- AI 개념을 프로그래머 외에 기획자 또는 제 3자가 쉽게 확인/설계 할 수있다.

- 직관적이다.

**단점**

- 확장이 힘들다. (FSM의 상태를 계속 추가하다 보면 다시 연결하기가 머리 아프다.)

---

### 여러 가지 구현 방법

#### 1. 일반적인 구현:

- https://www.raywenderlich.com/6034380-state-pattern-using-unity
- https://github.com/dubit/unity-fsm

어떻게 구현하느냐에 따라 좋을 수도 안 좋을 수도, 확장성이 있을 수도 없을 수도 있다.

#### 2. 플러그인 방식:

- https://www.youtube.com/watch?v=cHUXh5biQMg&list=PLX2vGYjWbI0ROSj_B0_eir_VkHrEkd4pi&index=2
- https://assetstore.unity.com/packages/templates/systems/topdown-engine-89636?locale=ko-KR

유니티 에디터의 인스펙터에서 조합하는게 보통 노가다가 아니다.

각 상태 / 전이 클래스에서 에이전트에 대한 2중 컴포넌트 참조가 귀찮다.

#### 3. 애니메이터와 StageMachineBehaviour 이용:

- https://docs.unity3d.com/kr/530/ScriptReference/StateMachineBehaviour.html

애니메이션 하나당 상태 1개 -> 상태가 많아지면..?

#### 4. GUI 기반 에셋:

- https://assetstore.unity.com/packages/tools/visual-scripting/behavior-designer-behavior-trees-for-everyone-15277

근본이 코드리스이므로 프로그래머의 코드와 합치려면 여간 귀찮다.

---

### 추천 라이브러리: MonsterLove FSM

https://github.com/thefuntastic/Unity3d-Finite-State-Machine

유니티에서 상태 머신 개념을 사용하고 싶어 여러 가지 에셋/라이브러리를 사용해본 결과 현재는 MonsterLove FSM을 즐겨 쓰고 있다.

#### 장점과 특징

- 리플렉션 기반으로 한 코드 내에서 빠르게 코딩할 수 있다. 
  - (외부 클래스에서 복잡한 컴포넌트 참조가 불필요하다.)
- 구현 방법이 단순하고 직관적이며, 상태는 Enum 필드를 추가하기만 하면 된다.
- 상태의 메서드는 밑줄 규칙 StateName_MethodName (상태명_메서드명)로 정의된다.

```c#
using MonsterLove.StateMachine; //1. Remember the using statement

public class MyGameplayScript : MonoBehaviour
{
    public enum States
    {
        Init, 
        Play, 
        Win, 
        Lose
    }
    
    StateMachine<States> fsm;
    
    void Awake()
    {
        fsm = new StateMachine<States>(this); //2. The main bit of "magic". 

        fsm.ChangeState(States.Init); //3. Easily trigger state transitions
    }

    void Init_Enter()
    {
        Debug.Log("Ready");
    }

    void Play_Enter()
    {      
        Debug.Log("Spawning Player");    
    }

    void Play_FixedUpdate()
    {
        Debug.Log("Doing Physics stuff");
    }

    void Play_Update()
    {
        if(player.health <= 0)
        {
            fsm.ChangeState(States.Lose); //3. Easily trigger state transitions
        }
    }

    void Play_Exit()
    {
        Debug.Log("Despawning Player");    
    }

    void Win_Enter()
    {
        Debug.Log("Game Over - you won!");
    }

    void Lose_Enter()
    {
        Debug.Log("Game Over - you lost!");
    }
}
```

라이브러리에 내장된 메서드는 `ChangeState(새로운 상태)` 호출에 의해 자동으로 트리거 된다.

#### 내장 메서드 목록

- `Enter`
- `Update`
- `FixedUpdate`
- `LateUpdate`
- `Exit`
- `Finally`
- `OntriggerEnter2D`
- ...

#### 리플렉션

MonsterLove FSM의 가장 큰 특징은 한 코드 내에서 StateName_MethodName (Ex. Idle_Update) 규칙을 기반으로 자동으로 호출이 된다는 것이다.  이것은 리플렉션을 이용해서 구현했다.

```c#
MethodInfo[] methods = component.GetType().GetMethods(bindingFlags);

//Bind methods to states
for (int i = 0; i < methods.Length; i++)
{
	TState state;
    string evtName;
    if (!ParseName(methods[i], out state, out evtName))
    {
        continue; //Skip methods where State_Event name convention could not be parsed
    }

    StateMapping<TState, TDriver> mapping = stateLookup[state];

    if (eventFieldsLookup.ContainsKey(evtName))
    {
        //Bind methods defined in TDriver
        // driver.Foo.AddListener(StateOne_Foo);
        FieldInfo eventField = eventFieldsLookup[evtName];
        BindEvents(rootDriver, component, state, enumConverter(state), methods[i], eventField);
     }
     else
     {
         //Bind Enter, Exit and Finally Methods
         BindEventsInternal(mapping, component, methods[i], evtName);
      }
}
```

위 코드는 클래스의 메서드 정보를 읽어와서 메서드의 이름이 라이브러리에서 정의한 이름 규칙에 맞는지 검사 후 FSM 구현을 위해 바인딩 하는 코드 일부다.

#### 퍼포먼스

**리플렉션**

> 리플렉션은 어셈블리, 모듈 및 형식을 설명하는 개체([Type](https://docs.microsoft.com/ko-kr/dotnet/api/system.type) 형식)를 제공합니다.

클래스의 메서드 정보를 가져오고 바인딩하기 위해 사용했으며, 일반적으로 리플렉션 사용은 컴파일 시점이 아닌 런타임 시점에 동적으로 실행되기 때문에 오버헤드를 일으킨다고 한다.

그러나 MonsterLove FSM 라이브러리의 StateMachine 인스턴스는 리플렉션 사용을 초기화 단계에서 단 한 번 사용으로 제한함으로써 오버헤드를 최대한 피했다. 이미 우리 게임은 최적화를 위해 오브젝트 풀링과 같은 여러 기법을 이미 사용하기 때문에 게임 유저들은 이 오버헤드를 알아차리지 못할 것이다.

**Invoke()**

라이브러리 내부에서는 정의된 이름 규칙에 맞는 메서드들을 검색 후 delegate와 같은 대리자에 바인딩한 후 `Invoke()` 함수 호출을 통해 상태에 맞는 행동을 트리거한다.

`Invoke()` 함수는 일반적인 함수 호출보다 좀 더 느리다. 수만 개가 넘는 인스턴스 때문에 상당한 오버헤드가 발생할 수 있다. 그러나 일반적인 사용(매니저 클래스 또는 인스턴스 10 ~ 100개)에는 이 성능 차이를 반드시 고려할 필요는 없다.

**Update()** 

`Monobehaviour.Update()`와 같은 유니티 이벤트 함수는 기본적으로 프레임마다 자동으로 호출된다. 이것을 최적화하는 방법중 하나는 Update()를 쓰지 않는 것이다. 정확히 말하면 한 번만 쓰는것이다. 유니티는 Update()를 호출할 때 GameObject의 원시 코드와 중간 코드 연결에 많은 자원을 사용한다.

이러한 추가 자원 사용을 최소화 하려면 Update()를 적게 사용해 불필요한 연결을 최소화해야 한다. 자체적인 업데이트 스타일을 함수가 담긴 구성요소와 이를 제어하는 만능 클래스라면 가능할 것이다. MonsterLove FSM은 이런 유니티 이벤트를 중앙에서 제어하는 설계를 가지고 있다.

```
void FixedUpdate() { /*** 생략 ***/}

void Update()
{
	for (int i = 0; i < stateMachineList.Count; i++)
	{
		var fsm = stateMachineList[i];
		if (!fsm.IsInTransition && fsm.Component.enabled)
		{
			fsm.Driver.Update.Invoke();
		}
	}
}		

void LateUpdate() { /*** 생략 ***/}
```

추가적으로 이러한 설계 덕분에 MonsterLove FSM의 퍼포먼스도 손쉽게 확인할 수 있다.

![](Resource\FSM_2.PNG)

---

### 사용 예시

아래 코드는 이 포스트의 제일 상단에 있는 몬스터의 FSM을 일부 구현한 것이다.

![](Resource\FSM_3.PNG)

```c#
public abstract Entity : GameBehaviour
{
    [Header("스탯")] public Stat Stat;
    public Vector3 Position => transform.position;

    protected SpriteRenderer m_SpriteRenderer;
    
    protected Entity m_Target;

    protected bool m_Death;
    public bool Death => m_Death;
  
    protected virtual void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public abstract void TakeHit(int damage);
    
    public abstract void Init(int dataIndex);
}

public class Monster : Entity
{

    // 몬스터의 상태 정의: 열거형
    public enum States
    {
        Spawn,
        Idle,
        Run,
        Attack,
        Death,
    }
    
    // FSM 
    private StateMachine<States> FSM;

    protected override void Awake()
    {
        base.Awake();
        
        FSM =  new StateMachine<States>(this);
        // FSM = StateMachine<States>.Initialize(this);
        // FSM.ChangeState(States.Idle);
    }
  
    public void TakeHit(int damage)
    {
        if (m_Death) return;

        Stat.Health -= damage;
        
        // OnTakeHit(damage);

        if (Stat.Health <= 0)
        {
            // OnDeath(damage);
            FSM.ChangeState(States.Death, StateTransition.Overwrite);
        }
    }

    public void Init(int monsterIndex)
    {
        // 외형 설정
        // 데이터 설정 (스탯...)
        
        m_Death = false;
        
        FSM.ChangeState(States.Spawn);
    }
    
    /**************************************** Spawn ************************************/
    protected virtual IEnumerator Spawn_Enter()
    {
        var duration = PlayAnimationAndGetDuration("Spawn");
        
        yield return new WaitForSeconds(duration);

        BattleManager.Instance.AddMonster(this);
        
        FSM.ChangeState(States.Idle);
    }
    
    /**************************************** Idle ************************************/
    protected virtual void Idle_Enter()
    {
        PlayAnimation("Idle");
    }
    
    protected virtual void Idle_Update()
    {
        var distance = GetDistance(Position, m_Target.Position);
        
        if (distance <= Stat.AttakRange && Stat.AttackCoolTimer <= 0)
        {
            FSM.ChangeState(States.Attack);
        }

        if (distance > Stat.AttackRange)
        {
            FSM.ChangeState(States.Run);
        }
    }

    protected virtual void Idle_Exit()
    {
        Debug.Log(gameObject.name + ": Idle_Exit");
    }
    
    /**************************************** Run ************************************/
    protected virtual void Run_Enter()
    {
        PlayAnimation("Run");
    }
    
    protected virtual void Run_Update()
    {
        if (GetDistance(Position, m_Target.Position) <= Stat.AttackRange)
        {
            FSM.ChangeState(States.Attack);
        }
        
        Vector2.MoveTowards(Position, m_Target.Position, Time.deltaTime * Stat.MoveSpeed);
    }

    protected virtual void Run_Exit()
    {
        Debug.Log(gameObject.name + ": Run_Exit");
    }
    
    
    /**************************************** Attack ************************************/
    protected virtual IEnumerator Attack_Enter()
    {
        var duration = PlayAnimationAndGetDuration("Attack");
        
        var halfWait = new WaitForSeconds(duration / 2f);

        yield return halfWait;

        m_Target.TakeHit(Stat.Damage);
        
        yield return halfWait;
        
        FSM.ChangeState(States.Idle);
    }
    
    
    
    /**************************************** Death ************************************/
    protected virtual IEnumerator Death_Enter()
    {
        m_Death = true;
        
        var duration = PlayAnimationAndGetDuration("Death");
        
        yield return new WaitForSeconds(duration);

        BattleManager.Instance.RemoveMonster(this);

        SafeSetActive(false);
    }
}
```

방치형 게임과 같이 몬스터의 상태 개수가 적고 단순할 경우 위처럼 한 클래스 내에서 빠르게 코드를 작성할 수 있는 장점이 있다.

---

### 마무리

FSM을 주제로 R&D를 진행하면서 직접 구현해보기도 하고, 많은 라이브러리/에셋을 사용해 봤다. 결론은 좋고 나쁘고를 떠나 어떤 게임을 개발하느냐에 따라 자신에게 맞는 것을 사용하는 게 좋을 거 같다.

MonsterLove FSM은 여기서 설명한 것 외에도 더 많은 메커니즘이 있다. 사용하고 싶다면 꼭 공식 Git에서 API를 확인해보자.