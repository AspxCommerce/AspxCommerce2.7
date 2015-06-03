using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ServiceInvoker
{
    public class Converter
    {
        public static object DictionaryToObj(Dictionary<string, object> dict, Type type)
        {

            Object paramObj = Activator.CreateInstance(type);

            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (!dict.Any(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase)))
                    continue;

                KeyValuePair<string, object> item = dict.First(x => x.Key.Equals(property.Name, StringComparison.InvariantCultureIgnoreCase));

                // Find which property type (int, string, double? etc) the CURRENT property is...
                Type tPropertyType = paramObj.GetType().GetProperty(property.Name).PropertyType;

                // Fix nullables...
                Type newT = Nullable.GetUnderlyingType(tPropertyType) ?? tPropertyType;
                string paramType = newT.FullName.ToLower();

                //checking and handling nullable type
                if (newT.IsGenericType && newT.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    paramType = newT.GetGenericArguments()[0].FullName.ToLower();

                    if (paramType == "system.datetime" || paramType == "system.single" || paramType == "system.boolean" || paramType == "system.string" || paramType == "system.int32" || paramType == "system.int64" || paramType == "system.decimal" || paramType == "system.double")
                    {
                        if (item.Value != null)
                        {
                            object newA = Convert.ChangeType(item.Value, Type.GetType(newT.GetGenericArguments()[0].FullName));
                            paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, newA, null);
                        }
                    }
                }
                //ok for string decimal int not for class object type
                else if (paramType == "system.datetime" || paramType == "system.single" || paramType == "system.boolean" || paramType == "system.string" || paramType == "system.int32" || paramType == "system.int64" || paramType == "system.decimal" || paramType == "system.double")
                {
                    if (item.Value != null)
                    {
                        object newA = Convert.ChangeType(item.Value, newT);
                        paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, newA, null);
                    }
                }
                else
                {
                    if (newT.IsClass == true && newT.IsGenericType == true)
                    {
                        Type objectParamType = Type.GetType(newT.FullName);

                        if (objectParamType == null)
                        {

                            if (newT.Assembly.Location != null && newT.Assembly.Location != string.Empty)
                            {
                                Assembly assembly = Assembly.LoadFile(newT.Assembly.Location);
                                objectParamType = assembly.GetType(newT.FullName);
                            }
                        }

                        var obj = DictionaryToList((IList)item.Value, objectParamType);
                        paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, obj, null);

                    }
                    else if (paramType == "system.array")
                    {
                        //value contain arraysvalue
                        Array arrValues = DictionaryToArray((IList)item.Value);
                        paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, arrValues, null);

                    }
                    else if (paramType == "system.collections.arraylist")
                    {
                        //value contains array
                        var x = (IList)item.Value;

                        if (x.Count > 0)
                        {
                            ArrayList arrList = DictionaryToArrayList((IList)item.Value);
                            paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, arrList, null);
                        }
                    }
                    else
                    {

                        Type objectParamType = Type.GetType(newT.FullName);
                        if (objectParamType == null)
                        {

                            if (newT.Assembly.Location != null && newT.Assembly.Location != string.Empty)
                            {
                                Assembly assembly = Assembly.LoadFile(newT.Assembly.Location);
                                objectParamType = assembly.GetType(newT.FullName);
                            }
                        }
                        var obj = DictionaryToObj((Dictionary<string, object>)item.Value, objectParamType);
                        paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, obj, null);
                    }
                }
            }
            return paramObj;
        }


        public static object DictionaryToList(IList dicts, Type type)
        {
            //get single class type from generic type
            Type objectParamType = Type.GetType(type.GetGenericArguments()[0].FullName);

            if (objectParamType == null)
            {

                if (type.GetGenericArguments()[0].Assembly.Location != null && type.GetGenericArguments()[0].Assembly.Location != string.Empty)
                {
                    Assembly assembly = Assembly.LoadFile(type.GetGenericArguments()[0].Assembly.Location);
                    objectParamType = assembly.GetType(type.GetGenericArguments()[0].FullName);
                }
            }
            // Fix nullables...
            Type newT = Nullable.GetUnderlyingType(objectParamType) ?? objectParamType;
            Object holder = Activator.CreateInstance(type);
            string paramType = newT.FullName.ToLower();
            foreach (var item in dicts)
            {
                //checking and handling nullable type
                if (newT.IsGenericType && newT.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    paramType = newT.GetGenericArguments()[0].FullName.ToLower();

                    if (paramType == "system.datetime" || paramType == "system.single" || paramType == "system.boolean" || paramType == "system.string" || paramType == "system.int32" || paramType == "system.int64" || paramType == "system.decimal" || paramType == "system.double")
                    {
                        var objItem = Convert.ChangeType(item, Type.GetType(newT.GetGenericArguments()[0].FullName));
                        MethodInfo method = type.GetMethod("Add", BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public);
                        method.Invoke(holder, new object[] { objItem });
                    }
                }
                //ok for string decimal int not for class object type
                else
                    if (paramType == "system.datetime" || paramType == "system.single" || paramType == "system.boolean" || paramType == "system.string" || paramType == "system.int32" || paramType == "system.int64" || paramType == "system.decimal" || paramType == "system.double")
                    {
                        var objItem = Convert.ChangeType(item, Type.GetType(newT.FullName));
                        MethodInfo method = type.GetMethod("Add", BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public);
                        method.Invoke(holder, new object[] { objItem });
                    }
                    else if (paramType == "system.array")
                    {
                        //value contain arraysvalue
                        Array arrValues = DictionaryToArray((IList)item);
                        //paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, arrValues, null);
                        MethodInfo method = type.GetMethod("Add", BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public);
                        method.Invoke(holder, new object[] { arrValues });
                    }
                    else if (paramType == "system.collections.arraylist")
                    {
                        //value contains array
                        ArrayList arrList = DictionaryToArrayList((IList)item);
                        //paramObj.GetType().GetProperty(property.Name).SetValue(paramObj, arrList, null);
                        MethodInfo method = type.GetMethod("Add", BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public);
                        method.Invoke(holder, new object[] { arrList });

                    }
                    else
                    {
                        Dictionary<string, object> dict = (Dictionary<string, object>)item;
                        var objItem = DictionaryToObj(dict, newT);
                        MethodInfo method = type.GetMethod("Add", BindingFlags.Default | BindingFlags.Instance | BindingFlags.Public);
                        method.Invoke(holder, new object[] { objItem });
                    }
            }
            return holder;
        }


        public static Array DictionaryToArray(IList arryItems)
        {

            List<object> arrlist = new List<object>();
            foreach (var item in arryItems)
            {
                arrlist.Add(item);
            }
            return arrlist.ToArray();
        }

        public static ArrayList DictionaryToArrayList(IList arryItems)
        {
            ArrayList arrlist = new ArrayList();
            var listArr = (IList)arryItems;
            if (listArr[0].GetType() == typeof(Array))
            {
                foreach (IList arrys in listArr)
                {
                    List<object> arr = new List<object>();

                    foreach (var item in arrys)
                    {
                        arr.Add(item);
                    }

                    arrlist.Add(arr.ToArray());
                }
                return arrlist;
            }
            else
            {
                var arr = DictionaryToArray(arryItems);
                arrlist.Add(arr);
                return arrlist;
            }
        }


    }
}
