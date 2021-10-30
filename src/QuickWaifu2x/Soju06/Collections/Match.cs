using System;
using System.Collections;
using System.Collections.Generic;

namespace Soju06.Collections {
    public class Match<T1, T2> : IEnumerable<Match<T1, T2>.MatchPair> {
        public Match() { }

        public Match(IDictionary<T1, T2> pairs) {
            AddRange(pairs);
        }
        
        public Match(Match<T1, T2> matches) {
            AddRange(matches);
        }
        
        public Match(params MatchPair[] matches) {
            AddRange(matches);
        }

        readonly List<MatchPair> Pairs = new();

        public int Find(T1 t1) {
            lock(Pairs) {
                var l = Pairs.Count;
                for (int i = 0; i < l; i++)
                    if (Pairs[i].T1.Equals(t1)) return i;
            } return -1;
        }

        public T2 this[T1 t1] {
            get => Get(t1);
            set => Set(t1, value);
        }

        public T1 this[T2 t2] {
            get => Get(t2);
            set => Set(t2, value);
        }

        public int Find(T2 t2) {
            lock(Pairs) {
                var l = Pairs.Count;
                for (int i = 0; i < l; i++)
                    if (Pairs[i].T2.Equals(t2)) return i;
            } return -1;
        }

        public T2 Get(T1 t1) {
            lock(Pairs) {
                var l = Pairs.Count;
                MatchPair e;
                for (int i = 0; i < l; i++)
                    if ((e = Pairs[i]).T1.Equals(t1)) return e.T2;
            } throw new KeyNotFoundException();
        }
        
        public T1 Get(T2 t2) {
            lock(Pairs) {
                var l = Pairs.Count;
                MatchPair e;
                for (int i = 0; i < l; i++)
                    if ((e = Pairs[i]).T2.Equals(t2)) return e.T1;
            } throw new KeyNotFoundException();
        }

        public void Set(T1 key, T2 t2) {
            lock(Pairs) {
                if (Find(t2) != -1) throw new Exception("already exist");
                var l = Pairs.Count;
                for (int i = 0; i < l; i++)
                    if (Pairs[i].T1.Equals(key)) {
                        Pairs[i] = new MatchPair(key, t2);
                        return;
                    }
            } throw new KeyNotFoundException();
        }
        
        public void Set(T2 key, T1 t1) {
            lock(Pairs) {
                if (Find(t1) != -1) throw new Exception("already exist");
                var l = Pairs.Count;
                for (int i = 0; i < l; i++)
                    if (Pairs[i].T2.Equals(key)) {
                        Pairs[i] = new MatchPair(t1, key);
                        return;
                    }
            } throw new KeyNotFoundException();
        }

        public void Add(T1 t1, T2 t2) {
            lock (Pairs) {
                if (t1 == null) throw new ArgumentNullException(nameof(t1));
                if (t2 == null) throw new ArgumentNullException(nameof(t2));
                if (Find(t1) != -1 || Find(t1) != -1) throw new Exception("already exist");
                Pairs.Add(new MatchPair(t1, t2));
            }
        }
        
        public void AddRange(IDictionary<T1, T2> pairs) {
            lock (Pairs) foreach (var item in pairs) Add(item.Key, item.Value);
        }

        public void AddRange(Match<T1, T2> matches) {
            lock (Pairs) foreach (var item in matches) Add(item.T1, item.T2);
        }
        
        public void AddRange(MatchPair[] pairs) {
            lock (Pairs) foreach (var item in pairs) Add(item.T1, item.T2);
        }

        public Match<T1, T2> Copy() => new(this);

        public bool Remove(T1 t1) {
            lock(Pairs) {
                var p = Find(t1);
                if (p == -1) return false;
                RemoveAt(p);
                return true;
            }
        }
        
        public bool Remove(T2 t2) {
            lock(Pairs) {
                var p = Find(t2);
                if (p == -1) return false;
                RemoveAt(p);
                return true;
            }
        }

        public void RemoveAt(int i) {
            lock (Pairs) Pairs.RemoveAt(i);
        }

        public void Clear() => Pairs.Clear();

        public IEnumerator<Match<T1, T2>.MatchPair> GetEnumerator() =>
            Pairs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public struct MatchPair {
            public MatchPair(T1 t1, T2 t2) {
                T1 = t1; T2 = t2;
            }

            public T1 T1 { get; private set; }
            public T2 T2 { get; private set; }
        }
    }
}
