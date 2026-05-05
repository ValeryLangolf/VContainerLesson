
// MonoBehaviour
1.
    [SerializeField] private YourBehaviour _yourBehaviour;

    builder.RegisterComponent(_yourBehaviour);

2.
    builder.RegisterComponentInHierarchy<YourBehaviour>();

3.
    [SerializeField] private YourBehaviour _prefab;

    builder.RegisterComponentInNewPrefab(_prefab, Lifetime.Scoped);

4.
    builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "NewGameObjectName");

5.
    builder.RegisterComponentInHierarchy<YourBehaviour>()
        .AsImplementedInterfaces();

6.
// Создать экземпляр нового игрового объекта (GameObject) в соответствии с указанным преобразованием
    builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped)
        .UnderTransform(parent);

// Создать экземпляр нового сборного объекта (префаба) в соответствии с указанным преобразованием
    builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped)
        .UnderTransform(parent);

// Найти компонент в соответствии с указанным преобразованием
    builder.RegisterComponentInHierarchy<YourBehaviour>()
        .UnderTransform(parent);

// Создать новый, выполнить дополнительные шаги, положить в родителя
    builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped)
        .UnderTransform(() => {
            // ...
            return parent;
        });

// Создать новый GameObject как DontDestroyOnLoad
    builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped)
        .DontDestroyOnLoad();

// Создать новый prefab как DontDestroyOnLoad
    builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped)
        .DontDestroyOnLoad();

7.
// Такое:
    builder.RegisterComponent(yourBehaviour);
    builder.RegisterComponentInHierarchy<YourBehaviour>();
    builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped);
    builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "name");

// можно сгруппировать так:
    builder.UseComponents(components =>
    {
        components.AddInstance(yourBehaviour);
        components.AddInHierarchy<YourBehaviour>();
        components.AddInNewPrefab(prefab, Lifetime.Scoped);
        components.AddOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "name");
    });

// Можно создать группу с указанным родительским элементом
// Такое:
    builder.RegisterComponentInHierarchy<YourBehaviour>()
        .UnderTransform(parentTransform);

    builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped)
        .UnderTransform(parentTransform);
    builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "name")
        .UnderTransform(parentTransform);

// можно сгруппировать так:
    builder.UseComponents(parentTransform, components =>
    {
        // GetComponentInChildren under `parentTransform`
        components.AddInHierarchy<YourBehaviour>();

        // Instantiate under `parentTransform`
        components.AddInNewPrefab(prefab, Lifetime.Scoped);
        components.AddOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "name");
    })

8.
// Зарегистрировать любое произвольное действие, которое должно произойти во время сборки контейнера, зарегистрировав функцию обратного вызова сборки
    builder.RegisterBuildCallback(container =>
    {
        var serviceA = container.Resolve<ServiceA>();
        var serviceB = container.Resolve<ServiceB>();
        // ...
    });

9.
// Подойдёт для сохранения прогресса
    builder.RegisterDisposeCallback(container =>
    {
        // Момент вызова – когда у LifetimeScope вызывается метод Dispose() (обычно это происходит автоматически при закрытии сцены или уничтожении игрового объекта со скопом).
    });

10.
// Регистрация с помощью ключей
    
    10.1. 
    // Регистрация с enum ключами
    public enum WeaponType
    {
        Primary,
        Secondary,
        Special
    }

    builder.Register<IWeapon, Sword>(Lifetime.Singleton)
        .Keyed(WeaponType.Primary);

    builder.Register<IWeapon, Bow>(Lifetime.Singleton)
        .Keyed(WeaponType.Secondary);

    builder.Register<IWeapon, MagicStaff>(Lifetime.Singleton)
        .Keyed(WeaponType.Special);

    10.2.
    // Регистрация с string ключами
    builder.Register<IEnemy, Goblin>(Lifetime.Singleton)
        .Keyed("goblin");

    builder.Register<IEnemy, Orc>(Lifetime.Singleton)
        .Keyed("orc");

    10.3.
    // Регистрация с int ключами
    builder.Register<ILevel, Level1>(Lifetime.Singleton)
        .Keyed(1);

    builder.Register<ILevel, Level2>(Lifetime.Singleton)
        .Keyed(2);

// Для обработки регистраций по ключу используйте Key атрибут в механизме внедрения или непосредственно API контейнера
    class WeaponSystem
    {
        public WeaponSystem(
            [Key(WeaponType.Primary)] IWeapon primaryWeapon,
            [Key(WeaponType.Secondary)] IWeapon secondaryWeapon)
        {
            // ...
        }
    }

11.
// Точка входа в обычный C#

    class FooController : IStartable
    {
        void IStartable.Start()
        {
            // Do something ...
        }
    }

    builder.RegisterEntryPoint<FooController>();

// интерфейсы точки входа
    IInitializable.Initialize() // Сразу после сборки контейнера
    IPostInitializable.PostInitialize()	// После IInitializable.Initialize()
    IStartable.Start() // Примерно MonoBehaviour.Start()
    IAsyncStartable.StartAsync() //	Примерно MonoBehaviour.Start()
    IPostStartable.PostStart() // После MonoBehaviour.Start()
    IFixedTickable.FixedTick() // Примерно MonoBehaviour.FixedUpdate()
    IPostFixedTickable.PostFixedTick() // После MonoBehaviour.FixedUpdate()
    ITickable.Tick() // Примерно MonoBehaviour.Update()
    IPostTickable.PostTick() // После MonoBehaviour.Update()
    ILateTickable.LateTick() // Примерно MonoBehaviour.LateUpdate()
    IPostLateTickable.PostLateTick() // После MonoBehaviour.LateUpdate()

// На стороне приложения исключения, возникающие в таких процессах, как Start() и Tick(), не могут быть перехвачены извне.
// По умолчанию VContainer регистрирует необработанные исключения как UnityEngine.Debug.LogException`.`.
// В качестве альтернативы вы можете зарегистрировать функцию обратного вызова для каждого `LifetimeScope`.

    builder.RegisterEntryPointExceptionHandler(ex =>
    {
        // ...
    });

12.
    // Фабрика с параметрами
    builder.RegisterFactory<float, Enemy>(speed => new Enemy(speed));

    public class Enemy
    {
        readonly float speed;

        public Enemy(float speed)
        {
            this.speed = speed;
        }
    }

    // Фабрика с параметрами и разрешением зависимости
    public class Enemy
    {
        readonly Player player;
        readonly float speed;

        public Enemy(float speed, Player player)
        {
            this.player = player;
            this.speed = speed;
        }
    }

    builder.RegisterFactory<float, Enemy>(container =>
    {
        var player = container.Resolve<Player>();
        return speed => new Enemy(speed, player);
    }, Lifetime.Scoped);

