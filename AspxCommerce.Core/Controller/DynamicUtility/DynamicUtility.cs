using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspxCommerce.Core
{
    public static class DynamicUtility
    {


        /// ///////////////////// InvokeStringMethod //////////////////////
        ///
        /// <summary>
        /// Calls a static public method. 
        /// Assumes that the method returns a string, and doesn't have parameters.
        /// </summary>
        /// <param name="className">name of the class in which the method lives.</param>
        /// <param name="methodName">name of the method itself.</param>
        /// <returns>the string returned by the called method.</returns>
        /// 
        public static List<T> InvokeMethodAsList<T>(string className, string methodName)
        {
            List<T> tList = new List<T>();
            // Get the Type for the class
            Type calledType = Type.GetType(className);

            // Invoke the method itself. The string returned by the method winds up in s
            tList = (List<T>) calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null,
                null,
                null);

            // Return the string that was returned by the called method.
            return tList;
        }

        public static T InvokeMethodAsObject<T>(string className, string methodName)
        {
            // Get the Type for the class
            Type calledType = Type.GetType(className);

            // Invoke the method itself. The string returned by the method winds up in s
            var tList = (T) calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null,
                null,
                null);

            // Return the string that was returned by the called method.
            return tList;
        }

        public static T InvokeMethodAsObject<T>(string className, string methodName, string stringParam)
        {
            // Get the Type for the class
            Type calledType = Type.GetType(className);

            // Invoke the method itself. The string returned by the method winds up in s.
            // Note that stringParam is passed via the last parameter of InvokeMember,
            // as an array of Objects.
            var s = (T) calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null,
                null,
                new Object[] {stringParam});

            // Return the string that was returned by the called method.
            return s;
        }

        public static List<T> InvokeMethodAsList<T>(string className, string methodName, string stringParam)
        {
            List<T> tList = new List<T>();
            // Get the Type for the class
            Type calledType = Type.GetType(className);

            // Invoke the method itself. The string returned by the method winds up in s.
            // Note that stringParam is passed via the last parameter of InvokeMember,
            // as an array of Objects.
            tList = (List<T>) calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null,
                null,
                new Object[] {stringParam});

            // Return the string that was returned by the called method.
            return tList;
        }

        /// ///////////////////// InvokeStringMethod3 //////////////////////
        ///
        /// <summary>
        /// Calls a static public method. 
        /// Assumes that the method returns a string, and doesn't have parameters.
        /// </summary>
        /// <param name="assemblyName">[dlls or project name ] name of the assembly containing the class in which the method lives.</param>
        /// <param name="namespaceName">namespace of the class.</param>
        /// <param name="className">name of the class in which the method lives.</param>
        /// <param name="methodName">name of the method itself.</param>
        /// <returns>the string returned by the called method.</returns>
        /// 
        public static List<T> InvokeMethodAsList<T>(string assemblyName, string namespaceName, string className,
                                                    string methodName, Object[] obj)
        {
            List<T> tList = new List<T>();
            // Get the Type for the class
            Type calledType = Type.GetType(namespaceName + "." + className + "," + assemblyName);

            // Invoke the method itself. The string returned by the method winds up in s
            tList = (List<T>) calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null,
                null,
                obj);

            // Return the string that was returned by the called method.
            return tList;
        }

        public static T InvokeMethodAsObject<T>(string assemblyName, string namespaceName, string className,
                                                string methodName, Object[] obj)
        {

            // Get the Type for the class
            Type calledType = Type.GetType(namespaceName + "." + className + "," + assemblyName);

            // Invoke the method itself. The string returned by the method winds up in s
            var tList = (T) calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null,
                null,
                obj);

            // Return the string that was returned by the called method.
            return tList;
        }

        public static object InvokeMethodAsObject(string assemblyName, string namespaceName, string className,
                                                  string methodName)
        {
            // Get the Type for the class
            Type calledType = Type.GetType(namespaceName + "." + className + "," + assemblyName);

            var obj = calledType.InvokeMember(
                methodName,
                BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static,
                null,
                null,
                null);
            return obj;
        }

        public static T Cast<T>(Object myobj)
        {
            Type objectType = myobj.GetType();
            Type target = typeof (T);
            var x = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members =
                d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = typeof (T).GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);
                if (value != null)
                    propertyInfo.SetValue(x, value, null);
            }
            return (T) x;
        }

        public static Object PassObjectToClass(string assemblyName, Object dataToPass)
        {
            Type objectType = Type.GetType(assemblyName);
            Type target = dataToPass.GetType();
            var passedData = Activator.CreateInstance(objectType, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members =
                d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                //property type of object which value are going to set
                propertyInfo = objectType.GetProperty(memberInfo.Name);
                value = target.GetProperty(memberInfo.Name).GetValue(dataToPass, null);
                if (value != null)
                {
                    if (value.Equals(""))
                    {

                    }
                    else
                    {
                        propertyInfo.SetValue(passedData,
                                              Convert.ChangeType(value, Type.GetType(propertyInfo.PropertyType.FullName)),
                                              null);

                    }
                }
            }
            return passedData;
        }


        public static Object PassObject(Object myobj, Object dataToPass)
        {
            Type objectType = myobj.GetType();
            Type target = dataToPass.GetType();
            var passedData = Activator.CreateInstance(target, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in target.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members =
                d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();
            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {
                propertyInfo = target.GetProperty(memberInfo.Name);
                value = myobj.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);
                if (value != null)
                    propertyInfo.SetValue(passedData,
                                          Convert.ChangeType(value, Type.GetType(propertyInfo.PropertyType.FullName)),
                                          null);

            }
            return passedData;
        }

        public static List<T> CastToList<T>(Object myobj)
        {
            List<T> tlist = new List<T>();

            IList collection = (IList) myobj;

            foreach (var list in collection)
            {
                Type objectType = list.GetType();
                Type target = typeof (T);
                var x = Activator.CreateInstance(target, false);
                var z = from source in objectType.GetMembers().ToList()
                        where source.MemberType == MemberTypes.Property
                        select source;
                var d = from source in target.GetMembers().ToList()
                        where source.MemberType == MemberTypes.Property
                        select source;
                List<MemberInfo> members =
                    d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();
                PropertyInfo propertyInfo;
                object value;
                foreach (var memberInfo in members)
                {
                    propertyInfo = typeof (T).GetProperty(memberInfo.Name);
                    if (objectType.GetProperty(memberInfo.Name) != null)
                    {
                        value = list.GetType().GetProperty(memberInfo.Name).GetValue(list, null);
                        if (value != null)
                        {
                            TypeCode typeCode = Type.GetTypeCode(value.GetType());
                            if (TypeCode.DateTime == typeCode)
                            {
                                value = DateTime.Parse(value.ToString()).ToString("dddd, dd MMMM yyyy");
                                propertyInfo.SetValue(x,
                                                      Convert.ChangeType(value,
                                                                         Type.GetType(propertyInfo.PropertyType.FullName)),
                                                      null);

                            }
                            else
                            {
                                propertyInfo.SetValue(x,
                                                      Convert.ChangeType(value,
                                                                         Type.GetType(propertyInfo.PropertyType.FullName)),
                                                      null);
                            }
                        }
                    }

                }
                tlist.Add((T) x);
            }



            return tlist; //(T)x;
        }


        public static Object PassMembersValue(Object myobj, Type to, string assemblyName)
        {
            Type objectType = myobj.GetType();
            Type target = to.GetType(); //typeof (T);
            var x = Activator.CreateInstance(to, false);
            var z = from source in objectType.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            var d = from source in to.GetMembers().ToList()
                    where source.MemberType == MemberTypes.Property
                    select source;
            List<MemberInfo> members =
                d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();
            List<MemberInfo> membersF =
                z.Where(memberInfo => z.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();

            PropertyInfo propertyInfo;
            object value;
            foreach (var memberInfo in members)
            {

                propertyInfo = to.GetProperty(memberInfo.Name);

                var xx = objectType.GetProperty("ShipToAddress");
                var yy = objectType.GetProperty("WareHouseAddress");
                var basket = objectType.GetProperty("BasketItems");

                var shiptoAddress = xx.GetValue(myobj, null);
                var wareHouseAddress = yy.GetValue(myobj, null);
                var basketItems = (List<BasketItem>)basket.GetValue(myobj, null);

                foreach (var item in basketItems)
                {
                    if (item.GetType().GetProperty(memberInfo.Name) != null)
                    {
                        value = item.GetType().GetProperty(memberInfo.Name).GetValue(item, null);
                        if (value != null)
                            propertyInfo.SetValue(x,
                                                  Convert.ChangeType(value,
                                                                     Type.GetType(propertyInfo.PropertyType.FullName)),
                                                  null);

                    }
                    else
                    {
                        break;
                    }

                }

                if (shiptoAddress != null)
                {
                    if (shiptoAddress.GetType().GetProperty(memberInfo.Name) != null)
                    {
                        value = shiptoAddress.GetType().GetProperty(memberInfo.Name).GetValue(shiptoAddress, null);
                        if (value != null)
                            propertyInfo.SetValue(x,
                                                  Convert.ChangeType(value,
                                                                     Type.GetType(propertyInfo.PropertyType.FullName)),
                                                  null);

                    }
                }

                if (wareHouseAddress != null)
                {
                    if (wareHouseAddress.GetType().GetProperty(memberInfo.Name) != null)
                    {
                        value = wareHouseAddress.GetType().GetProperty(memberInfo.Name).GetValue(wareHouseAddress, null);
                        if (value != null)
                            propertyInfo.SetValue(x,
                                                  Convert.ChangeType(value,
                                                                     Type.GetType(propertyInfo.PropertyType.FullName)),
                                                  null);
                    }
                }


                //PropertyInfo exposedInnerClassInfo = objectType.GetProperty("ExposedInnerClass");
                // PropertyInfo propertyInfo2 = baseType.GetProperty("Address");



                if (objectType.GetProperty(memberInfo.Name) != null)
                {

                    object dd = objectType.GetProperty(memberInfo.Name).GetValue(myobj, null);
                    if (dd.GetType().GetProperty(memberInfo.Name) != null)
                    {
                        value = dd.GetType().GetProperty(memberInfo.Name).GetValue(myobj, null);
                        if (value != null)
                            propertyInfo.SetValue(x,
                                                  Convert.ChangeType(value,
                                                                     Type.GetType(propertyInfo.PropertyType.FullName)),
                                                  null);
                    }
                    else
                    {
                        value = objectType.GetProperty(memberInfo.Name).GetValue(myobj, null);
                        if (value != null)
                        {
                            TypeCode typeCode = Type.GetTypeCode(value.GetType());

                            switch (typeCode)
                            {
                                case TypeCode.Object:

                                    var obj =
                                        PassObjectToClass(
                                            propertyInfo.PropertyType.FullName + "," + assemblyName, value);
                                    propertyInfo.SetValue(x, obj, null);

                                    break;

                                //  case TypeCode.Double:
                                //   break;
                                default:
                                    propertyInfo.SetValue(x,
                                                          Convert.ChangeType(value,
                                                                             Type.GetType(
                                                                                 propertyInfo.PropertyType.FullName)),
                                                          null);
                                    break;
                            }
                        }

                    }


                }
            }
            return x;
        }

        public static Object TransferDataToList(Object listItemData, Object additionalObj, Type to, string assemblyName)
        {
            IList collection = (IList)listItemData;
            var itemList = new ArrayList();
            foreach (var product in collection)
            {
                Type objectType = product.GetType();
                Type additionalType = additionalObj.GetType();
                //   Type target = to.GetType(); //typeof (T);
                var x = Activator.CreateInstance(to, false);
                var z = from source in objectType.GetMembers().ToList()
                        where source.MemberType == MemberTypes.Property
                        select source;

                var d = from source in to.GetMembers().ToList()
                        where source.MemberType == MemberTypes.Property
                        select source;

                var addit = from source in additionalType.GetMembers().ToList()
                            where source.MemberType == MemberTypes.Property
                            select source;

                List<MemberInfo> members =
                    d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();

                List<MemberInfo> additionalmembers =
                    addit.Where(memberInfo => addit.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();



                foreach (var memberInfo in members)
                {

                    PropertyInfo propertyInfo = to.GetProperty(memberInfo.Name);


                    if (product.GetType().GetProperty(memberInfo.Name) != null)
                    {
                        object value = product.GetType().GetProperty(memberInfo.Name).GetValue(product, null);
                        if (value != null)
                            propertyInfo.SetValue(x,
                                                  Convert.ChangeType(value,
                                                                     Type.GetType(propertyInfo.PropertyType.FullName)),
                                                  null);
                    }

                }

                foreach (var additionalmember in additionalmembers)
                {
                    //property info of which data is going to set i.e to 
                    PropertyInfo additionalInfo = to.GetProperty(additionalmember.Name);

                    if (to.GetProperty(additionalmember.Name) != null)
                    {
                        object value;
                        object dd = additionalType.GetProperty(additionalmember.Name).GetValue(additionalObj, null);
                        if (dd.GetType().GetProperty(additionalmember.Name) != null)
                        {
                            value = dd.GetType().GetProperty(additionalmember.Name).GetValue(additionalObj, null);
                            if (value != null)
                                additionalInfo.SetValue(x, Convert.ChangeType(value, Type.GetType(additionalInfo.PropertyType.FullName)), null);
                        }
                        else
                        {
                            value = additionalType.GetProperty(additionalmember.Name).GetValue(additionalObj, null);
                            if (value != null)
                            {
                                TypeCode typeCode = Type.GetTypeCode(value.GetType());

                                switch (typeCode)
                                {
                                    case TypeCode.Object:

                                        Type st =
                                            Type.GetType(additionalInfo.PropertyType.FullName + "," +
                                                         additionalInfo.PropertyType.Assembly.FullName + "");

                                        var obj = PassMembersValue(value, st);
                                        //DynamicUtility.PassObjectToClass(
                                        //    additionalInfo.PropertyType.FullName + "," + assemblyName, value);
                                        additionalInfo.SetValue(x, obj, null);

                                        break;

                                    default:
                                        additionalInfo.SetValue(x, Convert.ChangeType(value, Type.GetType(additionalInfo.PropertyType.FullName)),
                                                                null);
                                        break;
                                }
                            }

                        }


                    }
                }


                //for additional data 
                itemList.Add(x);

            }
            return itemList;
        }

        public static Object PassMembersValue(Object dataObj,Type toData)
        {
           
                Type objectType = dataObj.GetType();
                var x = Activator.CreateInstance(toData, false);
                var d = from source in toData.GetMembers().ToList()
                        where source.MemberType == MemberTypes.Property
                        select source;
                List<MemberInfo> members =
                    d.Where(memberInfo => d.Select(c => c.Name).ToList().Contains(memberInfo.Name)).ToList();

                foreach (var memberInfo in members)
                {

                    PropertyInfo propertyInfo = toData.GetProperty(memberInfo.Name);

                    if (objectType.GetProperty(memberInfo.Name) != null)
                    {
                        object value = objectType.GetProperty(memberInfo.Name).GetValue(dataObj, null);
                        if (value != null)
                            propertyInfo.SetValue(x,
                                                  Convert.ChangeType(value,
                                                                     Type.GetType(propertyInfo.PropertyType.FullName)),
                                                  null);
                    }
                }


            return x;
        }

    }
}
