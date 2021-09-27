/* ========= Soju06 I LOVE PYTHON =========
 * RUNTIME: Csharp 9.0 | .NET Framework 4.7.2
 * NAMESPACE: _
 * VERSION: 38
 * LICENSE: MIT
 * Copyright by Soju06
 * ========= Soju06 I LOVE PYTHON ========= */

using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

internal static class il {
    public const double _I_LOVE_PYTHON_VERSION_ = 38;

    public static byte[] Encode(this string s, Encoding encoding = null) =>
        (encoding ?? Encoding.UTF8).GetBytes(s);
    public static string Decode(this byte[] s, Encoding encoding = null) =>
        (encoding ?? Encoding.UTF8).GetString(s);
    public static byte[] DecodeBase64(this string s) =>
        System.Convert.FromBase64String(s);
    public static string EncodeBase64(this string s, Encoding encoding = null) =>
        System.Convert.ToBase64String(s.Encode(encoding ?? Encoding.UTF8));
    public static string EncodeBase64(this byte[] s) =>
        System.Convert.ToBase64String(s);

    public static string Join(this IEnumerable<string> s, string sep) => s == null ? "" : string.Join(sep, s);

    public static Uri Join(this Uri uri, params Uri[] uris) {
        var r = uri;
        for (int i = 0; i < uris.Length; i++)
            r = new Uri(r, uris[i]);
        return r;
    }
        
    public static Uri Join(this Uri uri, params string[] uris) {
        var r = uri;
        for (int i = 0; i < uris.Length; i++)
            r = new Uri(r, uris[i]);
        return r;
    }

    public static Uri JoinUri(this string s, params string[] uris) {
        Uri r = new(s);
        for (int i = 0; i < uris.Length; i++)
            r = new Uri(r, uris[i]);
        return r;
    }
    
    public static Uri Uri(this string s) => new(s);

    public static bool TryUri(this string s, out Uri uri) {
        try {
            uri = new(s);
            return true;
        } catch {
            uri = null;
            return false;
        }
    }

    public static string JoinDir(this string s, params string[] ss) {
        var a = new string[ss.Length + 1];
        for (int i = 1; i < a.Length; i++)
            a[i] = ss[i - 1];
        a[0] = s;
        return Path.Combine(a);
    }

    public static TItem[] Copy<TItem>(this TItem[] s) {
        var t = new TItem[s.Length];
        s.CopyTo(t, 0);
        return t;
    }

    public static TItem[] ToTArray<TItem>(this IEnumerable<TItem> s) where TItem : IEnumerable<TItem> {
        var w = new List<TItem>();
        w.AddRange(s);
        return w.ToArray();
    }

    public static List<T> ToTList<T>(this IEnumerable<T> s) where T : IEnumerable<T> {
        var w = new List<T>();
        w.AddRange(s);
        return w;
    }

    public static TItem[] ToTArray<TItem>(this IList<TItem> list) {
        var items = new TItem[list.Count];
        list.CopyTo(items, 0);
        return items;
    }

    public static TList ToOList<TItem, TList>(this IEnumerable<TItem> s) 
            where TList : IList<TItem>, new() {
        var list = new TList();
        foreach (var item in s) list.Add(item); 
        return list;
    }

    public static TList ToOList<TItem, TList>(this IEnumerable<TItem> s, TList list)
            where TList : IList<TItem> {
        foreach (var item in s) list.Add(item);
        return list;
    }

    public static T New<T>(this Type type) =>
        (T)Activator.CreateInstance(type);

    public static T New<T>(this Type type, params object[] obs) =>
        (T)Activator.CreateInstance(type, obs);

    public static bool IsNullOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s);
    public static bool IsNullOrEmpty(this string? s) => string.IsNullOrEmpty(s);

    public static long ToUnixTimestamp(this DateTime time) => ((DateTimeOffset)time).ToUnixTimeSeconds();
    public static DateTime UnixTimestampToDateTime(this long time) => new DateTime(1970, 1, 1).AddSeconds(time);

    public static bool IsZero(this IntPtr ptr) => ptr == IntPtr.Zero;
    public static bool IsZero(this UIntPtr ptr) => ptr == UIntPtr.Zero;

    public static List<T> Filter<T>(this IList<T> list, Func<T, bool> handle) {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (handle == null) throw new ArgumentNullException(nameof(handle));
        var h = new List<T>();
        for (int i = 0; i < list.Count; i++) {
            var g = list[i]; if (handle.Invoke(g)) h.Add(g);
        } return h;
    }

    public static List<TRequest> Filter<T, TRequest>(this IList<T> list, Func<T, TRequest> handle) {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (handle == null) throw new ArgumentNullException(nameof(handle));
        var h = new List<TRequest>();
        for (int i = 0; i < list.Count; i++) {
            var o = handle.Invoke(list[i]);
            if(o != null) h.Add(o);
        } return h;
    }
    
    public static List<TRequest> Filter<T, TRequest>(this IEnumerable<T> list, Func<T, TRequest> handle) {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (handle == null) throw new ArgumentNullException(nameof(handle));
        var h = new List<TRequest>();
        foreach (var item in list) {
            var o = handle.Invoke(item);
            if (o != null) h.Add(o);
        } return h;
    }

    public static T XSelect<T>(this IList<T> list, Func<T, bool> handle) {
        if (list == null) throw new ArgumentNullException("list");
        if (handle == null) throw new ArgumentNullException("handle");
        for (int i = 0; i < list.Count; i++) {
            var g = list[i]; if (handle.Invoke(g)) return g;
        }
        return default;
    }

    public static T TryGetValue<T>(this IList<T> list, int index) {
        if (index < 0 || list == null || index >= list.Count) return default;
        return list[index];
    }
    
    public static T TryGet<T>(this IList<T> list, int index) where T : class {
        if (index < 0 || list == null || index >= list.Count) return null;
        return list[index];
    }
    
    public static T? TryGetS<T>(this IList<T> list, int index) where T : struct {
        if (index < 0 || list == null || index >= list.Count) return null;
        return list[index];
    }

    public static bool IsNull<T>(this T obj) where T : class => obj is null;

    public static bool In<T>(this T e, params T[] es) => es?.Contains(e) == true;

    public static TEnum ToEnum<TEnum>(this string s, bool ignoreCase = false) where TEnum : Enum => 
        (TEnum)Enum.Parse(typeof(TEnum), s, ignoreCase);

    public static bool ToTryEnum<TEnum>(this string s, out TEnum @enum,
        bool ignoreCase = false) where TEnum : struct, Enum =>
        Enum.TryParse(s, ignoreCase, out @enum);
    
    public static string Format(this string q, string[][] s) {
        for (int i = 0; i < s.Length; i++) {
            var w = s[i];
            var e = w[w.Length - 1];
            for (int l = 0; l < w.Length - 1; l++)
                q = q.Replace(w[l], e);
        } return q;
    }

    public static string Format(this string q, params string[] s) => string.Format(q, s);

    public static string ToHex(this byte b, bool lower = true) {
        var r = BitConverter.ToString(new byte[] { b });
        if (lower) r = r.ToLower();
        return r;
    }

    public static string ToHex(this byte[] b, bool lower = true, string splitStr = null) {
        var r = BitConverter.ToString(b);
        if (lower) r = r.ToLower();
        if(splitStr != "-") 
            r = r.Replace("-", splitStr ?? "");
        return r;
    }

    public static void Run(this Task task) =>
        Task.Run(async () => await task).ConfigureAwait(false);

    public static void Run<TRes>(this Task<TRes> task) =>
        Task.Run(async () => await task);

    public static TRes RunWait<TRes>(this Task<TRes> task) {
        var t = Task.Run(async () => await task);
        t.Wait();
        return t.Result;
    }
    
    public static void RunWait(this Task task) =>
        Task.Run(async () => await task).Wait();

    public static T XSelect<T>(this IEnumerable<T> e, Func<T, bool> f) where T : class {
        foreach (var item in e) if (f.Invoke(item)) return item;
        return null;
    }
    
    public static T? XSelectI<T>(this IEnumerable<T> e, Func<T, bool> f) where T : struct {
        foreach (var item in e) if (f.Invoke(item)) return item;
        return null;
    }

    public static List<T> XFilter<T>(this IEnumerable<T> e, Func<T, bool> handle) {
        if (e == null) throw new ArgumentNullException("list");
        if (handle == null) throw new ArgumentNullException("handle");
        var h = new List<T>();
        foreach (var item in e) 
            if (handle.Invoke(item)) h.Add(item);
        return h;
    }

    public static ushort ToUInt16(this short i) =>
        BitConverter.ToUInt16(BitConverter.GetBytes(i), 0);

    public static uint ToUInt32(this int i) =>
        BitConverter.ToUInt32(BitConverter.GetBytes(i), 0);

    public static ulong ToUInt64(this long i) =>
        BitConverter.ToUInt64(BitConverter.GetBytes(i), 0);

    public static short ToInt16(this uint i) =>
        BitConverter.ToInt16(BitConverter.GetBytes(i), 0);

    public static int ToInt32(this uint i) =>
        BitConverter.ToInt32(BitConverter.GetBytes(i), 0);

    public static long ToInt64(this uint i) =>
        BitConverter.ToInt64(BitConverter.GetBytes(i), 0);

    public static byte[] NextBytes(this Random random, int length) {
        byte[] buf = new byte[length];
        random.NextBytes(buf);
        return buf;
    }

    public static dynamic ToExpandoObject(this object obj) {
        IDictionary<string, object> expando = new ExpandoObject();
        foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj.GetType()))
            expando.Add(property.Name, property.GetValue(obj));
        return expando as ExpandoObject;
    }

    public static TResult Using<TDisposbleObject, TResult>(this TDisposbleObject obj, 
        Func<TDisposbleObject, TResult> func) where TDisposbleObject : IDisposable {
        using (var t = obj) return func.Invoke(t);
    }
    
    public static void Using<TDisposbleObject>(this TDisposbleObject obj, 
        Action<TDisposbleObject> func) where TDisposbleObject : IDisposable {
        using (var t = obj) func.Invoke(t);
    }

    public static List<T> NullClear<T>(this List<T?> ts) where T : struct =>
        ts.Where(t => t != null).Select(x => x.Value).ToList();

    public static List<T?> NullClear<T>(this List<T?> ts) where T : class =>
        ts.Where(t => t != null).ToList();

    public static T[] NullClear<T>(this T?[] ts) where T : struct =>
        ts.Where(t => t != null).Select(x => x.Value).ToArray();

    public static T[] NullClear<T>(this T[] ts) where T : class =>
        ts.Where(t => t != null).ToArray();

#pragma warning disable CA1416 // 플랫폼 호환성 유효성 검사
    /// <summary>
    /// For WebAssembly it is not compatible.
    /// </summary>
    public static void WaitAll(this Task[] tasks) {
        if (tasks == null || tasks.Length < 1) return;
        Task.WaitAll(tasks);
    }
    /// <summary>
    /// For WebAssembly it is not compatible.
    /// </summary>
    public static Task Wait(this Task[] tasks) =>
        tasks == null || tasks.Length < 1 ? Task.CompletedTask : Task.Run(() => Task.WaitAll(tasks));
#pragma warning restore CA1416 // 플랫폼 호환성 유효성 검사

    public static void Dispose(this IEnumerable<IDisposable> disposables) {
        foreach (var item in disposables) item?.Dispose();
    }

    public static void Dispose(this IDisposable[] disposables) {
        for (int i = 0; i < disposables.Length; i++) disposables[i]?.Dispose();
    }

    public static TResult TryRun<TResult, TObject>(this TObject obj, Func<TObject, TResult> func)
        where TResult : class where TObject : class => obj != null ? func.Invoke(obj) : null;

    public static TResult? TryRunS<TResult, TObject>(this TObject obj, Func<TObject, TResult> func)
        where TResult : struct where TObject : class => obj != null ? func.Invoke(obj) : null;

    public static TResult TryRunS<TResult, TObject>(this TObject? obj, Func<TObject, TResult> func)
        where TResult : class where TObject : struct => obj.HasValue ? func.Invoke(obj.Value) : null;

    public static TResult? TryRunA<TResult, TObject>(this TObject? obj, Func<TObject, TResult> func)
        where TResult : struct where TObject : struct => obj.HasValue ? func.Invoke(obj.Value) : null;

    public static void TryRun<TObject>(this TObject? obj, Action<TObject> func)
        where TObject : struct { if (obj != null) func?.Invoke(obj.Value); }
    
    public static void TryRun<TObject>(this TObject obj, Action<TObject> func)
        where TObject : class { if (obj != null) func?.Invoke(obj); }

    public static bool TryGet<TObject>(this TObject? obj, out TObject oo) where TObject : struct {
        if (obj.HasValue) {
            oo = obj.Value;
            return true;
        } else {
            oo = default;
            return false;
        }
    }

    public static Task TryRun(this Task obj) => obj ?? Task.CompletedTask;
    public static Task<T> TryRun<T>(this Task<T> obj, T defaultValue = default) => obj ?? Task.FromResult<T>(defaultValue);

    public static string Format(this string s, params object[] parms) =>
        string.Format(s, args: parms);

    public static string GetHex(this Guid g, string joinSep = "") =>
        BitConverter.ToString(g.ToByteArray()).ToLower().Replace("-", joinSep);

    public static bool ToBoolean(this string s, bool defaultValue = false) => bool.TryParse(s, out var e) ? e : defaultValue;
    public static bool ToBoolean(this bool defaultValue, string s) => ToBoolean(s, defaultValue);
    public static byte ToByte(this string s, byte defaultValue = 0) => byte.TryParse(s, out var e) ? e : defaultValue;
    public static byte ToByte(this byte defaultValue, string s) => ToByte(s, defaultValue);
    public static sbyte ToSByte(this string s, sbyte defaultValue = 0) => sbyte.TryParse(s, out var e) ? e : defaultValue;
    public static sbyte ToSByte(this sbyte defaultValue, string s) => ToSByte(s, defaultValue);
    public static short ToInt16(this string s, short defaultValue = 0) => short.TryParse(s, out var e) ? e : defaultValue;
    public static short ToInt16(this short defaultValue, string s) => ToInt16(s, defaultValue);
    public static int ToInt32(this string s, int defaultValue = 0) => int.TryParse(s, out var e) ? e : defaultValue;
    public static int ToInt32(this int defaultValue, string s) => ToInt32(s, defaultValue);
    public static uint ToUInt32(this string s, uint defaultValue = 0) => uint.TryParse(s, out var e) ? e : defaultValue;
    public static uint ToUInt32(this uint defaultValue, string s) => ToUInt32(s, defaultValue);
    public static long ToInt64(this string s, long defaultValue = 0) => long.TryParse(s, out var e) ? e : defaultValue;
    public static long ToInt64(this long defaultValue, string s) => ToInt64(s, defaultValue);
    public static ulong ToUInt64(this string s, ulong defaultValue = 0) => ulong.TryParse(s, out var e) ? e : defaultValue;
    public static ulong ToUInt64(this ulong defaultValue, string s) => ToUInt64(s, defaultValue);

    public static int IndexOf<T>(this T[] ts, T obj) => Array.IndexOf(ts, obj);
    public static int IndexOf(this Array ts, object obj) => Array.IndexOf(ts, obj);

    public static bool ToTryDateTime(this string s, out DateTime date) => DateTime.TryParse(s, out date);
    public static bool ToTryTimeSpan(this string s, out TimeSpan time) => TimeSpan.TryParse(s, out time);

    public static void Replace(this IList<string> q, string n, string w) {
        for (int i = 0; i < q.Count; i++)
            q[i] = q[i].Replace(n, w);
    }

    public static void Replace(this IList<string> q, char n, char w) {
        for (int i = 0; i < q.Count; i++)
            q[i] = q[i].Replace(n, w);
    }

    public static TRes Convert<TRes, TReq>(this TReq obj) => 
        (TRes)System.Convert.ChangeType(obj, typeof(TRes));

    public static object Convert(this object obj, Type conversionType) =>
        System.Convert.ChangeType(obj, conversionType);

    public static void All<T>(this IEnumerable<T> ts, Action<T> func) {
        foreach (var item in ts) func.Invoke(item);
    }

    public static bool All<T>(this IEnumerable<T> ts, Func<T, bool> func) {
        foreach (var i in ts) if (!func.Invoke(i)) return false;
        return true;
    }

    public static string UriEscape(this string s) => HttpUtility.UrlEncodeUnicode(s);

    public static Dictionary<string, object> ToDictionary(this object obj) {
        var dictionary = new Dictionary<string, object>();
        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(obj))
            dictionary.Add(propertyDescriptor.Name, propertyDescriptor.GetValue(obj));
        return dictionary;
    }

    public static TIn Declaration<TIn>(this TIn obj, out TIn o) => o = obj;
    public static TRe Declaration<TIn, TRe>(this TIn obj, out TIn o, Func<TRe> func) {
        o = obj; return func.Invoke();
    }

    public static TRe Run<TRe>(this TRe o, Action<TRe> func) {
        func.Invoke(o); return o;
    }
    public static TRe Run<TIn, TRe>(this TIn o, Func<TIn, TRe> func) => func.Invoke(o);

    public static string S(this object o) => o?.ToString() ?? string.Empty;

    public static IEnumerable<T> Filter<T>(this IEnumerable<T> ls, Func<T, bool> func) {
        foreach (var i in ls) if (func.Invoke(i)) yield return i;
    }
    
    public static IEnumerable<T> Filter<T>(this IEnumerable<T> ls, Func<T, int, bool> func) {
        var l = 0;
        foreach (var i in ls) if (func.Invoke(i, l++)) yield return i;
    }

    public static string Concat(this IEnumerable<char> cs) => string.Concat(cs);

    public static T Return<T>(this Action func, T t) {
        func.Invoke(); return t;
    }
    public static T Return<T, T1>(this Action<T1> func, T1 t1, T t) {
        func.Invoke(t1); return t;
    }
    public static T Return<T, T1, T2>(this Action<T1, T2> func, T1 t1, T2 t2, T t) {
        func.Invoke(t1, t2); return t;
    }
    public static T Return<T, T1, T2, T3>(this Action<T1, T2, T3> func, T1 t1, T2 t2, T3 t3, T t) {
        func.Invoke(t1, t2, t3); return t;
    }
    public static T Return<T, T1, T2, T3, T4>(this Action<T1, T2, T3, T4> func, T1 t1, T2 t2, T3 t3, T4 t4, T t) {
        func.Invoke(t1, t2, t3, t4); return t;
    }
    public static T Return<T, T1, T2, T3, T4, T5>(this Action<T1, T2, T3, T4, T5> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T t) {
        func.Invoke(t1, t2, t3, t4, t5); return t;
    }
    public static T Return<T, T1, T2, T3, T4, T5, T6>(this Action<T1, T2, T3, T4, T5, T6> func, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T t) {
        func.Invoke(t1, t2, t3, t4, t5, t6); return t;
    }
}

internal struct Try {
    Try(Action action, Action<Exception> catchFunc = null, Action<bool> finallyFunc = null) {
        Action = action;
        CatchFunc = catchFunc;
        FinallyFunc = finallyFunc;
    }

    readonly Action Action;
    Action<Exception> CatchFunc;
    Action<bool> FinallyFunc;

    public Try Catch(Action<Exception> catchFunc = null) {
        CatchFunc = catchFunc; return this;
    }
    
    public void CatchRun(Action<Exception> catchFunc = null) {
        CatchFunc = catchFunc; Start();
    }
    
    public Try Finally(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; return this;
    }
    
    public void FinallyRun(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; Start();
    }

    public void Start() {
        var i = false;
        try {
            Action?.Invoke();
            i = true;
        } catch (Exception ex) {
            CatchFunc?.Invoke(ex);
        } finally {
            FinallyFunc?.Invoke(i);
        }
    }

    public static Try Run(Action action, Action<Exception> catchFunc = null, Action<bool> finallyFunc = null) => 
        new(action, catchFunc, finallyFunc);

    public static Try<TResult> Run<TResult>(Func<TResult> action,
        Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) =>
        new(action, catchFunc, finallyFunc);

    public static TResult EndRun<TResult>(Func<TResult> action,
        Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) {
        var result = default(TResult);
        var isSuccess = false;
        try {
            result = action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {  
            if (catchFunc != null) result = catchFunc.Invoke(ex);
            else result = default;
        } finally {
            finallyFunc?.Invoke(isSuccess);
        } return result;
    }
    
    public async static Task<TResult> EndRunAsync<TResult>(Func<Task<TResult>> action, 
        Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) {
        var result = default(TResult);
        var isSuccess = false;
        try {
            result = await action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            if (catchFunc != null) result = catchFunc.Invoke(ex);
            else result = default;
        } finally {
            finallyFunc?.Invoke(isSuccess);
        } return result;
    }
    
    public async static Task EndRunAsync(Func<Task> action, 
        Action<Exception> catchFunc = null, Action<bool> finallyFunc = null) {
        var isSuccess = false;
        try {
            await action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            if (catchFunc != null) catchFunc.Invoke(ex);
        } finally {
            finallyFunc?.Invoke(isSuccess);
        }
    }
    
    public async static Task EndRunAsync(Func<Task> action, 
        Func<Exception, Task> catchFunc = null, Action<bool> finallyFunc = null) {
        var isSuccess = false;
        try {
            await action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            if (catchFunc != null) await catchFunc.Invoke(ex);
        } finally {
            finallyFunc?.Invoke(isSuccess);
        }
    }

    public static void EndRun(Action action) {
        try {
            action.Invoke();
        } finally { }
    }
}

internal struct Try<TResult> {
    internal Try(Func<TResult> action, Func<Exception, TResult> catchFunc = null, Action<bool> finallyFunc = null) {
        Action = action;
        CatchFunc = catchFunc;
        FinallyFunc = finallyFunc;
    }

    readonly Func<TResult> Action;
    Func<Exception, TResult> CatchFunc;
    Action<bool> FinallyFunc;

    public Try<TResult> Catch(Func<Exception, TResult> catchFunc = null) {
        CatchFunc = catchFunc; return this;
    }
    
    public TResult CatchRun(Func<Exception, TResult> catchFunc = null) {
        CatchFunc = catchFunc; return Start();
    }
    
    public Try<TResult> Finally(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; return this;
    }
    
    public TResult FinallyRun(Action<bool> finallyFunc = null) {
        FinallyFunc = finallyFunc; return Start();
    }

    public TResult Start() {
        TResult result = default;
        bool isSuccess = false;
        try {
            result = Action.Invoke();
            isSuccess = true;
        } catch (Exception ex) {
            if (CatchFunc != null)
                result = CatchFunc.Invoke(ex);
            else result = default;
        } finally {
            FinallyFunc?.Invoke(isSuccess);
        } return result;
    }
}

internal static class Ran {
    public static Random Current { get; } = new();

    public static int Next(int min = int.MinValue, int max = int.MaxValue) => Current.Next(min, max);
    public static double NextDouble() => Current.NextDouble();
    public static byte[] NextBytes(int length) => Current.NextBytes(length);
    public static void NextBytes(byte[] bf) => Current.NextBytes(bf);
}