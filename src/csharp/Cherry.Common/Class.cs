using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Cherry.Common
{
    public class Class
    {
        internal class VolatileRecord
        {
            public DateTime AccessTime;
        }

        internal class ClassInfoRecord: VolatileRecord
        {
            public Type Type;
            public string Name;
            public FieldInfo[] Fields;
            public PropertyInfo[] Properties;

            public bool IsSuccessor(Type type)
            {
                return this.Type.IsSubclassOf(type);
            }
        }

        internal class ClassInfoList : List<ClassInfoRecord> { }

        internal class CacheClass : ClassInfoList
        {
            private DateTime LastAccess = DateTime.MinValue;
            public TimeSpan RetireTime = TimeSpan.MinValue;

            private readonly object _Lock = new object();

            public struct FindConditions
            {
                public Type Type;
                public string Name;
                public string Field;
                public string Property;
                public bool IncludeSuccessors;
                public bool IgnoreCase;
            }

            private bool IsSameOrSubclass(Type potentialBase, Type potentialDescendant)
            {
                return potentialDescendant.IsSubclassOf(potentialBase) || potentialDescendant == potentialBase;
            }

            public ClassInfoRecord Find(FindConditions conditions)
            {
                lock (_Lock)
                {
                    bool? purge = false;
                    try
                    {
                        DateTime expectancy = DateTime.MinValue;
                        if (RetireTime == TimeSpan.MinValue || LastAccess == DateTime.MinValue)
                        {
                            purge = null;
                        }
                        else
                        {
                            expectancy = DateTime.Now - RetireTime;
                        }

                        for (int i = 0, c = this.Count; i < c; i++)
                        {
                            var o = this[i];

                            if (true
                                && DateTime.MinValue != expectancy
                                && DateTime.MinValue != o.AccessTime
                                && expectancy > o.AccessTime
                                )
                            {
                                //if (true != purge)
                                //{
                                //    purge = true;
                                //}
                                purge = true;
                                continue;
                            }

                            if (null != conditions.Type)
                            {
                                if (false
                                    || o.Type != conditions.Type
                                    || conditions.IncludeSuccessors && !o.IsSuccessor(conditions.Type)
                                    )
                                {
                                    continue;
                                }
                            }

                            if (null != conditions.Name)
                            {
                                if (0 != string.Compare(o.Name, conditions.Name, conditions.IgnoreCase))
                                {
                                    continue;
                                }
                            }

                            bool b = true;

                            if (null != conditions.Field)
                            {
                                if (null != o.Fields)
                                {
                                    for (int n = 0, m = o.Fields.Length; n < m; n++)
                                    {
                                        var f = o.Fields[n];
                                        if (0 == string.Compare(f.Name, conditions.Field, conditions.IgnoreCase))
                                        {
                                            b = true;
                                            break;
                                        }
                                    }
                                    if (b)
                                    {
                                        return o;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }

                            if (b && null != conditions.Field)
                            {
                                if (null != o.Fields)
                                {
                                    for (int n = 0, m = o.Fields.Length; n < m; n++)
                                    {
                                        var f = o.Fields[n];
                                        if (0 == string.Compare(f.Name, conditions.Field, conditions.IgnoreCase))
                                        {
                                            b = false;
                                            break;
                                        }
                                    }
                                    if (b)
                                    {
                                        continue;
                                    }
                                }
                            }

                            if (b && null != conditions.Property)
                            {
                                if (null != o.Properties)
                                {
                                    for (int n = 0, m = o.Properties.Length; n < m; n++)
                                    {
                                        var p = o.Fields[n];
                                        if (0 == string.Compare(p.Name, conditions.Field, conditions.IgnoreCase))
                                        {
                                            b = false;
                                            break;
                                        }
                                    }
                                    if (b)
                                    {
                                        continue;
                                    }
                                }
                            }

                            o.AccessTime = DateTime.MinValue;
                            return o;
                        }

                        return null;
                    }
                    finally
                    {
                        if (true == purge)
                        {
                            Purge();
                        }
                    }
                }
            }

            private void Purge()
            {
                throw new NotImplementedException();
            }
        }

        private static CacheClass _Cache;
        /// <summary>Cache</summary>
        internal static CacheClass Cache
        {
            get
            {
                if (_Cache == null)
                {
                    lock (typeof(ClassInfoList))
                    {
                        if (_Cache == null)
                        {
                            _Cache = new CacheClass();
                        }
                    }
                }
                return _Cache;
            }
        }

        public static object GetElementValue(object o, string elementName, bool ignoreCase, bool includePrivate)
        {
            if (null == o)
            {
                return null;
            }
            if (null == elementName)
            {
                return null;
            }
            Type classType = o.GetType();
            BindingFlags bindingAttr = default;
            bindingAttr |= BindingFlags.Instance;
            bindingAttr |= BindingFlags.Public;
            if (null != elementName)
            {
                foreach (var propertyInfo in classType.GetProperties(bindingAttr: bindingAttr))
                {
                    if (0 == string.Compare(elementName, propertyInfo.Name, ignoreCase))
                    {
                        try
                        {
                            return propertyInfo.GetValue(o, null);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }
            if (null != elementName)
            {
                if (includePrivate)
                {
                    bindingAttr |= BindingFlags.NonPublic;
                }
                foreach (var fieldInfo in classType.GetFields(bindingAttr: bindingAttr))
                {
                    if (0 == string.Compare(elementName, fieldInfo.Name, ignoreCase))
                    {
                        try
                        {
                            return fieldInfo.GetValue(o);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }
            return null;
        }
    }
}
