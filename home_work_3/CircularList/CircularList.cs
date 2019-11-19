using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircularList
{

    public class Node<T> //where T : IComparable<T>// класс, описывающий узел нашего кольцевого списка, содержит:
    {
        public Node(T val)
        {
            this.Value = val;
        }
        public T Value { get; set; } // значение узла
        public Node<T> Next { get; set; } // указатель на следующий элемент списка
        public override string ToString()
        {
            return Value.ToString();
        }
    }
    public class CircularList<T> : IEnumerable<T>, IAlgorithm where T: IComparable<T>// класс кольцевого списка. достаточно конструктора по умолчанию, так как список состоит, по сути, только из головной ноды и методов
    {
        Node<T> _head; // головная нода
        int _count; // необязательно, но на всякий случай будем хранить количество элементов в списке

        /// <summary>
        /// определим методы нашего кольцевого списка.
        /// 1) Добавление элемента. Так как пользователю, по-большому счету все равно, что считается
        /// началом или концом в кольцевом списке, то реализуем только метод AddAfter
        /// 2) Вывод витка списка, начиная с заданного элемента
        /// 3) Удаление элемента списка
        /// 4) Проверка, пустой ли список
        /// 5) Количество элементов
        /// 6) Содержит ли список конкретный элемент
        /// 7) Энумератор
        /// </summary>
        #region Private Utils Logic
        Node<T> GetPrev(Node<T> current = null) // заглушка для нахождения предыдущего элемента перед данным
        // будет использоваться для реализации вставки элементов
        {
            Node<T> currentHead = current == null ? _head : current;
            if (current != null)
                while (current.Next != currentHead)
                    current = current.Next;
            return current;
        }
        Node<T> GetElementNode(T element) // возвращает первый попавшийся элемент с заданным значением 
        {
            Node<T> currentNode = _head;
            int counter = 0;
            while (!currentNode.Value.Equals(element) && counter < _count)
            {
                currentNode = currentNode.Next;
                counter++;
            }
            if (counter <= _count)
                return currentNode;
            else
                throw new Exception("Element not found!");
        }
        Node<T>[] GetElementNodeList(T element) // возвращает все элементы списка с заданным значением
        {
            List<Node<T>> lst = new List<Node<T>>();
            Node<T> currentNode = _head;
            int counter = 0;
            while (counter < _count)
            {
                currentNode = currentNode.Next;
                counter++;
                if (currentNode.Value.Equals(element))
                    lst.Add(currentNode);
            }
            if (lst.Count > 0)
                return lst.ToArray();
            else
                throw new Exception("Element not found!");
        }

        #endregion
        #region Add Logic
        /// <summary>
        /// Унифицированное добавление в наш список.
        /// Если пользователь знает ноду, после которой надо добавить - то ее можно указать
        /// В противном случае добавится в конец списка
        /// </summary>
        /// <param name="val"></param> Добавляемый объект типа Т
        /// <param name="place"></param> Нода, после которой будет добавлен новый элемент (по умолчанию null и добавление происходит после "хвостовой" ноды)

        public Node<T> Add(T val, Node<T> place = null)
        {
            Node<T> currentHead = place == null ? GetPrev(_head) : place;
            Node<T> newNode = new Node<T>(val);
            if (_head == null)
            {
                newNode.Next = newNode;
                _head = newNode;
            }
            else
            {
                newNode.Next = currentHead.Next;
                currentHead.Next = newNode;
            }
            _count++;
            return newNode;
        }
        /// <summary>
        /// Метод добавления нового элемента после первого вхождения заданного элемента
        /// </summary>
        /// <param name="val"></param> значение, которое будем добавлять
        /// <param name="place"></param> значение, после которого будем добавлять
        /// <returns></returns>
        public Node<T> AddAfterFirst(T val, T place)
        {
            Node<T> searchable;
            try
            {
                searchable = GetElementNode(place);
                return Add(val, searchable);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Метод добавления нового элемента после каждого вхождения заданного элемента
        /// </summary>
        /// <param name="val"></param> значение, которое будем добавлять
        /// <param name="place"></param> значение, после которого будем добавлять
        /// <returns></returns>
        public bool AddAfterAll(T val, T place)
        {
            Node<T>[] searchable;
            try
            {
                searchable = GetElementNodeList(place);
                foreach (Node<T> node in searchable)
                    Add(val, node);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        #region Get Slice Logic
        public T[] GetSlice(Node<T> startNode = null) // возвращаем виток нашего кольцевого списка начиная с указанного элемента
        {
            List<T> slice = new List<T>();
            Node<T> currentHead = startNode == null ? _head : startNode;
            Node<T> currentNode = currentHead;
            if (currentNode != null)
            {
                do
                {
                    slice.Add(currentNode.Value);
                    currentNode = currentNode.Next;
                }
                while (currentNode != currentHead); //for tail node
            }
            return slice.ToArray();
        }
        public T[] GetSlice(T fromElement) // возвращаем виток нашего кольцевого списка начиная с указанного элемента
        {
            Node<T> searchable;
            try
            {
                searchable = GetElementNode(fromElement);
                return GetSlice(searchable);
            }
            catch
            {
                return null;
            }
        }
        #endregion
        public int Count { get { return _count; } } // получение количества уникальных нод
        public bool IsEmpty { get { return _count == 0; } } // проверка, есть ли элементы в списке
        public bool IsContain(T element)
        {
            Node<T> searchable;
            try
            {
                searchable = GetElementNode(element);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #region Remove Node Logic
        public bool Remove(Node<T> element)
        {
            if (IsEmpty)
                return false;
            if (_count > 1)
            {
                GetPrev(element).Next = element.Next;
                if (element == _head)
                    _head = _head.Next;
            }
            else
                _head = null;
            _count--;
            return true;
        }
        public bool RemoveFirst(T element) // удалим первое вхождение элемента element
        {
            try
            {
                Node<T> toDelete = GetElementNode(element);
                return Remove(toDelete);
            }
            catch
            {
                return false;
            }
        }
        public bool RemoveAll(T element)// удалим все вхождения элемента element
        {
            try
            {
                Node<T>[] toDelete = GetElementNodeList(element);
                foreach (Node<T> node in toDelete)
                    Remove(node);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = _head;
            do
            {
                if (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }
            while (current != _head);
        }

        public void BubleSort() // осртировка пузырьком
        {
            Node<T> a = null;
            Node<T> b = null;
            Node<T> c = null;
            Node<T> e = null;
            Node<T> tmp = null;

            Node<T> tail = GetPrev(_head);
            tail.Next = null; // разорвем список
            
            while (e != _head.Next)
            {
                c = a = _head;
                b = a.Next;
                while (a != e)
                {
                    if (a.Value.CompareTo(b.Value)==1)
                    {
                        if (a == _head)
                        {
                            tmp = b.Next;
                            b.Next = a;
                            a.Next = tmp;
                            _head = b;
                            c = b;
                        }
                        else
                        {
                            tmp = b.Next;
                            b.Next = a;
                            a.Next = tmp;
                            c.Next = b;
                            c = b;
                        }
                    }
                    else
                    {
                        c = a;
                        a = a.Next;
                    }
                    b = a.Next;
                    if (b == e)
                        e = a;
                }
            }

            Node<T> current = _head;
            while (current.Next != null)
                current = current.Next;
            current.Next = _head; // восстановим список
        }


    }
}