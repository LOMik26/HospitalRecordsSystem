# Система учёта медицинских записей в больнице

WPF-приложение на .NET 8 для автоматизации учёта медицинских записей. Пациенты подают заявки на приём, сотрудники — управляют заявками, приёмами и данными пациентов.

---

## Требования

| Компонент | Версия |
|---|---|
| Windows | 10 / 11 |
| Visual Studio | 2022 (версия 17.x) |
| Рабочая нагрузка Visual Studio | **.NET desktop development** |
| .NET SDK | 8.0 |
| SQL Server | LocalDB (входит в состав Visual Studio) |

> **Примечание.** LocalDB устанавливается автоматически при выборе рабочей нагрузки «Разработка классических приложений .NET» в установщике Visual Studio.

---

## Как открыть проект в Visual Studio

1. **Клонировать репозиторий**

   ```
   git clone https://github.com/LOMik26/HospitalRecordsSystem.git
   ```

   Или нажмите **Code → Download ZIP** на GitHub и распакуйте архив.

2. **Открыть решение**

   Запустите Visual Studio 2022, затем:

   - **File → Open → Project/Solution...**
   - Выберите файл `HospitalRecordsSystem.sln` в корне репозитория.

   Либо просто **дважды щёлкните** на `HospitalRecordsSystem.sln` в Проводнике Windows — Visual Studio откроется автоматически.

3. **Восстановить пакеты NuGet**

   Visual Studio сделает это автоматически при первом открытии. Если нет — нажмите правой кнопкой на решение в **Solution Explorer** и выберите **Restore NuGet Packages**.

---

## Настройка базы данных

Приложение использует **SQL Server LocalDB** (встроен в Visual Studio). База данных создаётся автоматически при первом запуске через Entity Framework Core.

Если требуется создать базу данных вручную (через миграции EF Core):

1. Откройте **Package Manager Console** (Tools → NuGet Package Manager → Package Manager Console).
2. Выберите проект `Hospital.Data` как **Default project**.
3. Выполните команды:

   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

> Строка подключения задана в `Hospital.Data/DbContext.cs`:
> ```
> Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=HospitalDB;
> ```
> Менять её не нужно — LocalDB доступен на любой машине с установленным Visual Studio.

---

## Запуск приложения

1. Убедитесь, что стартовым проектом является **Hospital.UI** (жирный шрифт в Solution Explorer). Если нет — нажмите на него правой кнопкой и выберите **Set as Startup Project**.
2. Нажмите **F5** (или кнопку **▶ Start**).

При первом запуске автоматически создаётся учётная запись администратора:

| Логин | Пароль |
|---|---|
| `admin` | `admin` |

---

## Структура решения

```
HospitalRecordsSystem.sln
├── Hospital.Domain   — сущности и перечисления (Patient, Appointment, ...)
├── Hospital.Data     — DbContext и настройки EF Core
└── Hospital.UI       — WPF-интерфейс (окна, ViewModel-ы)
```

---

## Учётные записи по умолчанию

- **Сотрудник:** логин `admin`, пароль `admin` — создаётся при первом запуске.
- **Пациент:** регистрируется через кнопку **Регистрация** на экране входа.
