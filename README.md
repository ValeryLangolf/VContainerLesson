<div align="center">

# 💉 VContainer Lesson

### Урок по Dependency Injection в Unity на базе VContainer

[![Unity](https://img.shields.io/badge/Unity-2021.3%2B-000000?logo=unity&logoColor=white)](https://unity.com/)
[![VContainer](https://img.shields.io/badge/VContainer-DI%20Framework-2C5F2D)](https://github.com/hadashiA/VContainer)
[![C#](https://img.shields.io/badge/C%23-9.0%2B-239120?logo=csharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

*Учебный материал: от базовой теории Dependency Injection и SOLID до практических рецептов работы с VContainer.*

</div>

---

## 📚 Оглавление

- [О проекте](#-о-проекте)
- [Путь разработчика в Unity](#-путь-разработчика-в-unity)
- [DIP — принцип инверсии зависимостей](#-dip--принцип-инверсии-зависимостей)
- [Связь DI и DIP](#-связь-di-и-dip)
- [Что такое Dependency Injection?](#-что-такое-dependency-injection)
- [Зачем нужен DI?](#-зачем-нужен-di)
- [DI-контейнер (IoC-контейнер)](#-di-контейнер-ioc-контейнер)
- [Сравнение DI-контейнеров](#-сравнение-di-контейнеров-для-unity)
- [Почему именно VContainer?](#-почему-именно-vcontainer)
- [Основные термины VContainer](#-основные-термины-vcontainer)
- [Рецепты регистрации MonoBehaviour](#-рецепты-регистрации-monobehaviour)
- [Группировка регистраций через `UseComponents`](#-группировка-регистраций-через-usecomponents)
- [Build- и Dispose-callback'и](#-build--и-dispose-callback)
- [Регистрация по ключам](#-регистрация-по-ключам)
- [Точки входа (Entry Points)](#-точки-входа-entry-points)
- [Фабрики](#-фабрики)
- [Пример графа зависимостей](#-пример-графа-зависимостей)
- [Материалы урока](#-материалы-урока)

---

## 🎯 О проекте

Этот репозиторий — учебный конспект по **Dependency Injection** в Unity с использованием библиотеки [VContainer](https://github.com/hadashiA/VContainer). Внутри: теоретический минимум (DI, DIP, SOLID), сравнение DI-фреймворков, разбор ключевых API VContainer и набор практических примеров.

Подходит, если ты:

- впервые сталкиваешься с DI-контейнером в Unity;
- уже пробовал Zenject и хочешь альтернативу полегче и побыстрее;
- хочешь иметь под рукой шпаргалку по `RegisterComponent*`, `EntryPoint`, фабрикам и ключам.

---

## 🛤 Путь разработчика в Unity

Типичная эволюция архитектуры в проектах на Unity:

```
Статические классы-прослойки  →  Синглтоны  →  Сервис-локатор  →  DI Container
```

Каждый следующий шаг снижает связанность и увеличивает контроль над временем жизни объектов. **DI-контейнер** — финальная точка эволюции, в которой управление зависимостями вынесено из бизнес-кода в декларативную конфигурацию.

---

## 🧱 DIP — принцип инверсии зависимостей

**Dependency Inversion Principle** — буква **D** в SOLID.

> 1. Модули верхних уровней не должны зависеть от модулей нижних уровней. Оба должны зависеть от **абстракций** (интерфейсов, абстрактных классов).
> 2. Абстракции не должны зависеть от деталей. Детали должны зависеть от абстракций.

**Проще говоря:** твоей бизнес-логике не должно быть важно, как именно сохраняются данные — через `PlayerPrefs`, `SQLite` или облако. Она работает с интерфейсом `ISaveRepository`, а конкретная реализация подставляется извне.

---

## 🔗 Связь DI и DIP

| Понятие | Что это |
|---|---|
| **DIP** | Архитектурный принцип — *что* нужно делать |
| **DI**  | Паттерн реализации — *как* это сделать |

**Прямая зависимость (DI → DIP).** DIP требует, чтобы зависимости поставлялись извне, а не создавались внутри класса. Именно это и делает DI: класс не вызывает `new Repository()`, а получает готовый объект через конструктор, метод или свойство.

Без DIP применение DI теряет ценность: если ты внедряешь конкретный `FileRepository` вместо `IRepository`, класс по-прежнему жёстко привязан к деталям. DI естественным образом подталкивает к мышлению абстракциями:

```csharp
builder.Register<IRepository, SqlRepository>(Lifetime.Singleton);
```

**Обратная зависимость (DIP → DI).** DIP можно реализовать и без DI-контейнера — через фабрики, сервис-локатор или ручное внедрение в корне композиции. Но DI-контейнеры делают это удобнее и автоматизированнее.

---

## 💉 Что такое Dependency Injection?

> **Dependency Injection (DI)** — паттерн проектирования, при котором компонент получает свои зависимости извне, а не создаёт их самостоятельно.

Объект не создаёт внутри себя то, что ему нужно для работы, а получает это «снаружи» — как гость берёт готовые салфетки в ресторане, а не приносит свои из дома.

```csharp
public class EnemySpawner
{
    private readonly IEnemyFactory _factory;
    private readonly IAudioService _audio;

    // Зависимости приходят извне через конструктор — это и есть DI
    public EnemySpawner(IEnemyFactory factory, IAudioService audio)
    {
        _factory = factory;
        _audio = audio;
    }
}
```

---

## 🎁 Зачем нужен DI?

1. **Снижение связанности (decoupling)** — классы зависят от абстракций, а не от конкретных реализаций.
2. **Упрощение тестирования** — зависимости легко подменяются на моки в Unit-тестах.
3. **Гибкость и переиспользуемость кода** — замена реализации не требует правок в классах-потребителях.
4. **Поддержка и масштабирование** — крупные проекты остаются управляемыми.
5. **Централизованный контроль времени жизни** объектов: `Singleton` / `Scoped` / `Transient`.

---

## 📦 DI-контейнер (IoC-контейнер)

Это специальная библиотека, которая автоматизирует процесс внедрения зависимостей: сама создаёт объекты, разрешает граф зависимостей и управляет их жизненными циклами. По сути — «умная фабрика», которая по запросу выдаёт готовые объекты со всеми вложенными зависимостями.

---

## ⚖️ Сравнение DI-контейнеров для Unity

| Контейнер | Позиционирование | Ключевые особенности |
|---|---|---|
| **Zenject / Extenject** | Самый популярный, «тяжёлый» и многофункциональный DI-фреймворк для Unity | Мощный API, большое сообщество, но медленный, высокий порог входа, давно не обновляется |
| **VContainer** | Современная, лёгкая и очень быстрая альтернатива | В 5–10× быстрее Zenject, нулевые аллокации, активная поддержка, простой API |
| **Reflex** | Минималистичный DI-фреймворк | Blazing fast, очень небольшой размер кода |

---

## ⚡ Почему именно VContainer?

**VContainer** — экстра-быстрая DI-библиотека для Unity с минимальным размером кода и без аллокаций GC при резолвинге.

- ⚡ **Сверхбыстрый** — в 5–10× быстрее Zenject.
- ♻️ **Без аллокаций** при запросе зависимостей.
- 🪶 **Очень маленький размер** скомпилированного кода.
- 🧼 **Прозрачный и простой API** для корректного DI.
- 🔒 **Потокобезопасность**.
- 🎮 **Тонкая интеграция** с Unity-объектами (`MonoBehaviour`, `ScriptableObject`).
- 🛠 **Активная поддержка** и развитие.

> VContainer имеет смысл, если нужен лёгкий, быстрый и простой в использовании DI-контейнер.

---

## 🧩 Основные термины VContainer

| Термин | Описание |
|---|---|
| **`LifetimeScope`** | Главный строительный блок. Наследуется от `MonoBehaviour`, вешается на `GameObject` или префаб. Определяет область видимости контейнера. |
| **`Lifetime.Transient`** | Каждый раз создаётся новый экземпляр. |
| **`Lifetime.Scoped`** | Один экземпляр в рамках одного `LifetimeScope`. |
| **`Lifetime.Singleton`** | Один экземпляр на всё приложение. |
| **`IContainerBuilder`** | Интерфейс для регистрации типов внутри метода `Configure`. |
| **`RegisterEntryPoint<T>`** | Способ инициализации чистой C#-логики, имитирующей жизненный цикл `MonoBehaviour` через `IStartable`, `ITickable` и др. |
| **`RegisterComponentInHierarchy<T>` / `RegisterComponentOnNewGameObject<T>`** | Удобные методы для регистрации Unity-компонентов. |
| **`ContainerBuilder`** | Объект, собирающий регистрации перед сборкой контейнера. |
| **`IObjectResolver`** | Интерфейс для ручного получения сервисов из контейнера (обычно не нужен — инъекция автоматическая). |

---

## 🧪 Рецепты регистрации MonoBehaviour

> Все примеры исполняются внутри метода `Configure(IContainerBuilder builder)` твоего `LifetimeScope`.

### 1. Уже существующий компонент через `[SerializeField]`

```csharp
[SerializeField] private YourBehaviour _yourBehaviour;

builder.RegisterComponent(_yourBehaviour);
```

### 2. Поиск компонента в иерархии сцены

```csharp
builder.RegisterComponentInHierarchy<YourBehaviour>();
```

### 3. Создание из префаба

```csharp
[SerializeField] private YourBehaviour _prefab;

builder.RegisterComponentInNewPrefab(_prefab, Lifetime.Scoped);
```

### 4. Создание нового `GameObject` с компонентом

```csharp
builder.RegisterComponentOnNewGameObject<YourBehaviour>(
    Lifetime.Scoped,
    "NewGameObjectName");
```

### 5. Регистрация компонента под все его интерфейсы

```csharp
builder.RegisterComponentInHierarchy<YourBehaviour>()
    .AsImplementedInterfaces();
```

### 6. Размещение под определённым `Transform`

```csharp
// Создать новый GameObject под указанным родителем
builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped)
    .UnderTransform(parent);

// Создать экземпляр префаба под указанным родителем
builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped)
    .UnderTransform(parent);

// Найти компонент под указанным родителем
builder.RegisterComponentInHierarchy<YourBehaviour>()
    .UnderTransform(parent);

// Создать новый, выполнить дополнительные шаги, положить в родителя
builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped)
    .UnderTransform(() =>
    {
        // ...
        return parent;
    });

// Создать новый GameObject как DontDestroyOnLoad
builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped)
    .DontDestroyOnLoad();

// Создать новый prefab как DontDestroyOnLoad
builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped)
    .DontDestroyOnLoad();
```

---

## 🧰 Группировка регистраций через `UseComponents`

Когда регистраций много, их удобно объединять в один блок.

**Было:**

```csharp
builder.RegisterComponent(yourBehaviour);
builder.RegisterComponentInHierarchy<YourBehaviour>();
builder.RegisterComponentInNewPrefab(prefab, Lifetime.Scoped);
builder.RegisterComponentOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "name");
```

**Стало:**

```csharp
builder.UseComponents(components =>
{
    components.AddInstance(yourBehaviour);
    components.AddInHierarchy<YourBehaviour>();
    components.AddInNewPrefab(prefab, Lifetime.Scoped);
    components.AddOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "name");
});
```

**С общим родительским `Transform`:**

```csharp
builder.UseComponents(parentTransform, components =>
{
    // GetComponentInChildren под `parentTransform`
    components.AddInHierarchy<YourBehaviour>();

    // Instantiate под `parentTransform`
    components.AddInNewPrefab(prefab, Lifetime.Scoped);
    components.AddOnNewGameObject<YourBehaviour>(Lifetime.Scoped, "name");
});
```

---

## 🛎 Build- и Dispose-callback'и

### Build callback — действия в момент сборки контейнера

```csharp
builder.RegisterBuildCallback(container =>
{
    var serviceA = container.Resolve<ServiceA>();
    var serviceB = container.Resolve<ServiceB>();
    // ...
});
```

### Dispose callback — например, для сохранения прогресса

```csharp
builder.RegisterDisposeCallback(container =>
{
    // Вызывается, когда у LifetimeScope срабатывает Dispose()
    // (обычно при закрытии сцены или уничтожении GameObject со scope-ом)
});
```

---

## 🔑 Регистрация по ключам

Иногда под один интерфейс нужно несколько реализаций. VContainer поддерживает ключи: `enum`, `string`, `int`.

### Ключ-`enum`

```csharp
public enum WeaponType { Primary, Secondary, Special }

builder.Register<IWeapon, Sword>(Lifetime.Singleton).Keyed(WeaponType.Primary);
builder.Register<IWeapon, Bow>(Lifetime.Singleton).Keyed(WeaponType.Secondary);
builder.Register<IWeapon, MagicStaff>(Lifetime.Singleton).Keyed(WeaponType.Special);
```

### Ключ-`string`

```csharp
builder.Register<IEnemy, Goblin>(Lifetime.Singleton).Keyed("goblin");
builder.Register<IEnemy, Orc>(Lifetime.Singleton).Keyed("orc");
```

### Ключ-`int`

```csharp
builder.Register<ILevel, Level1>(Lifetime.Singleton).Keyed(1);
builder.Register<ILevel, Level2>(Lifetime.Singleton).Keyed(2);
```

### Использование `[Key]` в конструкторе

```csharp
public class WeaponSystem
{
    public WeaponSystem(
        [Key(WeaponType.Primary)]   IWeapon primaryWeapon,
        [Key(WeaponType.Secondary)] IWeapon secondaryWeapon)
    {
        // ...
    }
}
```

---

## 🚪 Точки входа (Entry Points)

VContainer умеет «оживлять» обычные C#-классы — без `MonoBehaviour`. Достаточно реализовать нужный интерфейс жизненного цикла:

```csharp
public class FooController : IStartable
{
    void IStartable.Start()
    {
        // Стартовая логика
    }
}

builder.RegisterEntryPoint<FooController>();
```

### Полный список интерфейсов жизненного цикла

| Интерфейс | Когда вызывается |
|---|---|
| `IInitializable.Initialize()`        | Сразу после сборки контейнера |
| `IPostInitializable.PostInitialize()`| После `Initialize()` |
| `IStartable.Start()`                 | Аналог `MonoBehaviour.Start()` |
| `IAsyncStartable.StartAsync()`       | Асинхронный аналог `Start()` |
| `IPostStartable.PostStart()`         | После `Start()` |
| `IFixedTickable.FixedTick()`         | Аналог `FixedUpdate()` |
| `IPostFixedTickable.PostFixedTick()` | После `FixedUpdate()` |
| `ITickable.Tick()`                   | Аналог `Update()` |
| `IPostTickable.PostTick()`           | После `Update()` |
| `ILateTickable.LateTick()`           | Аналог `LateUpdate()` |
| `IPostLateTickable.PostLateTick()`   | После `LateUpdate()` |

### Обработка исключений в Entry Points

Исключения в `Start()` / `Tick()` нельзя поймать снаружи. По умолчанию VContainer логирует их через `UnityEngine.Debug.LogException`. Можно переопределить обработчик для каждого `LifetimeScope`:

```csharp
builder.RegisterEntryPointExceptionHandler(ex =>
{
    // Своя обработка ошибок
});
```

---

## 🏭 Фабрики

Фабрики позволяют создавать объекты с **рантайм-параметрами**.

### Простая фабрика с параметром

```csharp
builder.RegisterFactory<float, Enemy>(speed => new Enemy(speed));

public class Enemy
{
    private readonly float _speed;

    public Enemy(float speed)
    {
        _speed = speed;
    }
}
```

### Фабрика с параметром + разрешение зависимости из контейнера

```csharp
public class Enemy
{
    private readonly Player _player;
    private readonly float _speed;

    public Enemy(float speed, Player player)
    {
        _player = player;
        _speed = speed;
    }
}

builder.RegisterFactory<float, Enemy>(container =>
{
    var player = container.Resolve<Player>();
    return speed => new Enemy(speed, player);
}, Lifetime.Scoped);
```

---

## 🗺 Пример графа зависимостей

Небольшой пример того, как зависимости могут выстраиваться в проекте:

```text
Spawner
  └── Pool
        └── Factory
              └── Create() ──► Enemy
                                ├── AudioService
                                └── ParticleService
```

`Spawner` зависит от `Pool`, тот — от `Factory`. Фабрика создаёт `Enemy`, которому нужны `AudioService` и `ParticleService`. DI-контейнер собирает весь этот граф автоматически — тебе достаточно зарегистрировать каждый сервис под своей абстракцией.

---

## 📂 Материалы урока

В репозитории лежат вспомогательные материалы:

| Файл | Что внутри |
|---|---|
| 📊 `VContainer.pptx` | Презентация: теория DI/DIP/SOLID, сравнение контейнеров, термины VContainer |
| 📊 `Примерчик.pptx` | Пример графа зависимостей `Spawner → Pool → Factory → Enemy` |
| 💻 `VC.cs`           | Шпаргалка с готовыми сниппетами для `Configure(IContainerBuilder builder)` |

---

<div align="center">

### 🌟 Полезные ссылки

[Официальный репозиторий VContainer](https://github.com/hadashiA/VContainer) ·
[Документация VContainer](https://vcontainer.hadashikick.jp/) ·
[Zenject (для сравнения)](https://github.com/modesttree/Zenject)

*Made with ☕ and a healthy dose of `[Inject]`*

</div>
