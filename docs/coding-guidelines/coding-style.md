C# Coding Style
===============
Общее правило, которому мы следуем, — «использовать настройки Visual Studio по умолчанию».

1. Мы используем фигурные скобки [в стиле Allman](http://en.wikipedia.org/wiki/Indent_style#Allman_style), где каждая фигурная скобка начинается с новой строки. Однострочный блок операторов может быть без фигурных скобок, но этот блок должен иметь правильный отступ на своей собственной строке и не должен быть вложен в другие блоки операторов, использующие фигурные скобки (подробнее см. правило 18). Единственным исключением является то, что оператор «using» может быть вложен в другой оператор «using», начиная со следующей строки на том же уровне отступа, даже если вложенный оператор «using» содержит контролируемый блок.
2. Мы используем четыре пробела отступа (без табуляции).
3. Мы используем `_camelCase` для внутренних и частных полей и используем `readonly`, где это возможно. Префикс внутренних и частных полей экземпляра — `_`, статических полей — `s_`, а статических полей потока — `t_`. При использовании в статических полях `readonly` должно стоять после `static` (например, `static readonly`, а не `readonly static`). Публичные поля следует использовать с осторожностью и использовать PascalCasing без префикса.
4. Мы избегаем «this.», если в этом нет крайней необходимости.
5. Мы всегда указываем видимость, даже если она установлена ​​по умолчанию (например,
   `private string _foo`, а не `string _foo`). Видимость должна быть первым модификатором (например,
   `public abstract`, а не `public abstract`).
6. Импорт пространства имен должен быть указан в начале файла, внешний
   `namespace`, и должны быть отсортированы в алфавитном порядке, за исключением пространств имен `System.*`, которые должны быть размещены поверх всех остальных.
7. Избегайте одновременно более одной пустой строки. Например, нет двух
   пустые строки между элементами типа.
8. Избегайте ложных свободных пространств.
   Например, избегайте `if (someVar == 0)...`, где точками отмечены ложные пробелы.
   Рассмотрите возможность включения «Просмотр пробелов (Ctrl+R, Ctrl+W)» или «Правка -> Дополнительно -> Просмотр пробелов», если вы используете Visual Studio для облегчения обнаружения.
9. Если стиль файла отличается от этих рекомендаций (например, приватные члены называются `m_member`
   а не `_member`), существующий стиль в этом файле имеет приоритет.
10. Мы используем «var» только в том случае, если тип явно назван справа, обычно из-за «new» или явного приведения, например. `var stream = new FileStream(...)`, а не `var stream = OpenStandardInput()`.
    - Точно так же `new()` с целевым типом может использоваться только тогда, когда тип явно назван слева, в операторе определения переменной или операторе определения поля. например `FileStream stream = new(...);`, но не `stream = new(...);` (где тип был указан в предыдущей строке).
11. Мы используем ключевые слова языка вместо типов BCL (например, `int, string, float` вместо `Int32, String, Single` и т. д.) как для ссылок на типы, так и для вызовов методов (например, `int.Parse` вместо ` Int32.Parse`). Примеры см. в выпуске [#13976](https://github.com/dotnet/runtime/issues/13976).
12. Мы используем PascalCasing для именования всех наших постоянных локальных переменных и полей. Единственным исключением является код взаимодействия, где постоянное значение должно точно совпадать с именем и значением кода, который вы вызываете через взаимодействие.
13. Мы используем PascalCasing для всех имен методов, включая локальные функции.
14. Мы используем ```nameof(...)``` вместо ```"..."``` везде, где это возможно и уместно.
15. Поля должны быть указаны вверху в объявлениях типов.
16. При включении символов, отличных от ASCII, в исходный код используйте escape-последовательности Unicode (\uXXXX) вместо буквенных символов. Буквенные символы, отличные от ASCII, иногда искажаются инструментами или редакторами.
17. При использовании меток (для перехода) делайте отступ метки на единицу меньше текущего отступа.
18. При использовании одного оператора if мы следуем этим соглашениям:
    - Никогда не используйте однострочную форму (например: `if (source == null) throw new ArgumentNullException("source");`)
    - Использование фигурных скобок всегда допускается и требуется, если какой-либо блок составного оператора `if`/`else if`/.../`else` использует фигурные скобки или если тело одного оператора занимает несколько строк.
    - Фигурные скобки могут быть опущены только в том случае, если тело *каждого* блока, связанного с составным оператором `if`/`else if`/.../`else`, размещено на одной строке.
19. Сделайте все внутренние и частные типы статическими или запечатанными, если не требуется производных от них. Как и в случае с любой деталью реализации, их можно изменить, если/когда потребуется деривация в будущем.

Файл [EditorConfig](https://editorconfig.org «Домашняя страница EditorConfig») (`.editorconfig`) находится в корне репозитория среды выполнения, обеспечивая автоматическое форматирование C# в соответствии с приведенными выше рекомендациями.

Мы также используем [Инструмент форматирования кода .NET](https://github.com/dotnet/codeformatter), чтобы гарантировать, что кодовая база поддерживает постоянный стиль с течением времени. Этот инструмент автоматически исправляет кодовую базу, чтобы она соответствовала рекомендациям, изложенным выше.

### Example File:

``ObservableLinkedList`1.cs:``

```C#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Win32;

namespace System.Collections.Generic
{
    public partial class ObservableLinkedList<T> : INotifyCollectionChanged, INotifyPropertyChanged
    {
        private ObservableLinkedListNode<T> _head;
        private int _count;

        public ObservableLinkedList(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (T item in items)
            {
                AddLast(item);
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public int Count
        {
            get { return _count; }
        }

        public ObservableLinkedListNode AddLast(T value)
        {
            var newNode = new LinkedListNode<T>(this, value);

            InsertNodeBefore(_head, node);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            NotifyCollectionChangedEventHandler handler = CollectionChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void InsertNodeBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            ...
        }

        ...
    }
}
```

``ObservableLinkedList`1.ObservableLinkedListNode.cs:``

```C#
using System;

namespace System.Collections.Generics
{
    partial class ObservableLinkedList<T>
    {
        public class ObservableLinkedListNode
        {
            private readonly ObservableLinkedList<T> _parent;
            private readonly T _value;

            internal ObservableLinkedListNode(ObservableLinkedList<T> parent, T value)
            {
                Debug.Assert(parent != null);

                _parent = parent;
                _value = value;
            }

            public T Value
            {
                get { return _value; }
            }
        }

        ...
    }
}
```

Для других языков в настоящее время нашим лучшим руководством является согласованность. При редактировании файлов следите за тем, чтобы новый код и изменения соответствовали стилю файлов. Для новых файлов он должен соответствовать стилю этого компонента. Если есть совершенно новый компонент, подойдет все, что достаточно широко принято. Файлы скриптов см. в блоге скриптов, где можно найти [советы](https://devblogs.microsoft.com/scripting/tag/powertip) и [лучшие практики](https://devblogs.microsoft.com/scripting/tag /лучшие практики).
