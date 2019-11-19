//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace home_work_3
//{
//    public class CircularList<T>: IEnumerable
//    {
//        List<T> array;
//        public CircularList()
//        {
//            array = new List<T>();
//        }
//        public IEnumerator GetEnumerator()
//        {
//            return new CircularEnumerator<T>(array);
//        }
//    }
//    class CircularEnumerator<T> : IEnumerator
//    {
//        public CircularEnumerator(List<T> list)
//        {
//            _arr = list;
//            _position = -1; // первоначально енумератор находится за "границами" итерируемого массива
//            _arrayLen = list.Count;
//        }
//        private List<T> _arr; // локальная копия 
//        private int _position;
//        private int _arrayLen;
//        public object Current
//        {
//            get { return _arr[_position % _arrayLen]; } // возвращаем актуальный номер элемента, рассчитанный как остаток от деления на длину массива
//        }

//        public bool MoveNext()
//        {
//            _position += 1;
//            return true; //  так как список кольцевой, то двигаться вперед всегда будет куда
//        }

//        public void Reset()
//        {
//            _position = -1; // сбрасываем счетчик позиций
//        }
//    }
//}
