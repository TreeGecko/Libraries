using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace TreeGecko.Library.Common.Helpers
{
    /// <summary>
    /// Summary description for ReflectionHelper.
    /// </summary>
    public class ReflectionHelper
    {
        private static readonly HybridDictionary assemblies = new HybridDictionary();

        //Getclass but pass args to constructor
        /// <summary>
        /// Create new object, passing constructor arguments
        /// </summary>
        /// <param name="_assemblyName">Assembly housing class</param>
        /// <param name="_className">Classname being requested</param>
        /// <param name="_constructorArgs">Object array of arguments to pass to constructor</param>
        /// <returns>New object for given class</returns>
        public static object GetClass(string _assemblyName, string _className,
            object[] _constructorArgs)
        {
            Assembly assembly = GetAssembly(_assemblyName);
            Type objType = assembly.GetType(_className, true, true);
            return Activator.CreateInstance(objType, _constructorArgs);
        }

        /// <summary>
        /// Gets the Type definition from the supplied class in the CIL library.
        /// </summary>
        /// <param name="_className"></param>
        /// <returns></returns>
        public static Type getType(string _className)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            return assembly.GetType(_className, true, true);
        }

        public static Type getType(string _assemblyName, string _className)
        {
            Assembly assembly = GetAssembly(_assemblyName);
            return assembly.GetType(_className, true, true);
        }

        /// <summary>
        /// Gets a class from the CIL assembly
        /// </summary>
        /// <param name="_className"></param>
        /// <returns></returns>
        public static object GetClass(string _className)
        {
            Type objType = getType(_className);
            return Activator.CreateInstance(objType);
        }

        /// <summary>
        /// Create new object of given Class from given Assembly, no constructor arguments supplied
        /// </summary>
        /// <param name="_assemblyName">Assembly housing class</param>
        /// <param name="_className">Classname being requested</param>
        /// <returns>New object for given class</returns>
        public static object GetClass(string _assemblyName, string _className)
        {
            Assembly assembly = GetAssembly(_assemblyName);
            Type type = assembly.GetType(_className, true, true);

            return GetClass(type);
        }


        public static object GetClass(string _className, Assembly _assembly)
        {
            Type type = _assembly.GetType(_className, true, true);

            //create instance of transformer class, set runtime collection and execute
            return GetClass(type);
        }

        /// <summary>
        /// Create new object of given type, no constructor arguments supplied
        /// </summary>
        /// <param name="_type">Type of object requested</param>
        /// <returns>New object for given Type</returns>
        public static object GetClass(Type _type)
        {
            //create instance of transformer class, set runtime collection and execute
            return Activator.CreateInstance(_type);
        }

        /// <summary>
        /// Get Assembly by name
        /// </summary>
        /// <param name="_assemblyName">Name to find</param>
        /// <returns>Assembly found</returns>
        public static Assembly GetAssembly(string _assemblyName)
        {
            Assembly assembly;

            if (assemblies.Contains(_assemblyName))
            {
                assembly = (Assembly) assemblies[_assemblyName];
            }
            else
            {
                assembly = Assembly.LoadFrom(_assemblyName);
                assemblies.Add(_assemblyName, assembly);
            }

            return assembly;
        }

        /// <summary>
        /// Call given Method of given Class in given Assembly 
        /// </summary>
        /// <param name="_assemblyName">Assembly housing class</param>
        /// <param name="_className">Class to create</param>
        /// <param name="_methodName">Method to invoke</param>
        /// <param name="_parameters">Parameters to pass to Method</param>
        /// <returns>string value from called Method</returns>
        public static string InvokeFunction(string _assemblyName, string _className,
            string _methodName, object[] _parameters)
        {
            try
            {
                Assembly assembly = GetAssembly(_assemblyName);
                Type type = assembly.GetType(_className);
                object o = Activator.CreateInstance(type);

                MethodInfo method = type.GetMethod(_methodName);

                return (string) method.Invoke(o, BindingFlags.Public, null, _parameters, null);
            }
            catch (Exception e)
            {
                TraceFileHelper.Error("Exception in invokeFunction : Assembly-" + _assemblyName + ", ClassName-" +
                                      _className + ", MethodName-" + _methodName + ", ParametersCount-" +
                                      _parameters.GetLength(0));
                throw e;
            }
        }

        /// <summary>
        /// Create List<> of Properties of object of given Type
        /// </summary>
        /// <param name="_type">Type of object to query</param>
        /// <returns>List<string> of object type properties</returns>
        public static List<string> GetProperties(Type _type)
        {
            List<string> properties = new List<string>();

            PropertyInfo[] props = _type.GetProperties();

            foreach (PropertyInfo prop in props)
            {
                string propName = prop.Name;

                properties.Add(propName);
            }

            return properties;
        }

        /// <summary>
        /// Return value of given object property
        /// </summary>
        /// <param name="_current">Object to search</param>
        /// <param name="_propertyName">Property to search</param>
        /// <returns>Value of given property as object</returns>
        public static object GetPropertyObjectValue(object _current, string _propertyName)
        {
            try
            {
                Type type = _current.GetType();

                PropertyInfo property = type.GetProperty(_propertyName);

                if (property != null)
                {
                    return property.GetValue(_current, BindingFlags.Public, null, null, null);
                }
                else
                {
                    throw new Exception("Property does not exist - " + _propertyName);
                }
            }
            catch (Exception e)
            {
                TraceFileHelper.Error("Exception in getPropertyObjectValue : PropertyName-" + _propertyName);
                throw e;
            }
        }

        /// <summary>
        /// Set value of given object property
        /// </summary>
        /// <param name="_current">Object to change</param>
        /// <param name="_propertyName">Property to change</param>
        /// <param name="_propertyValue">New property value</param>
        public static void SetPropertyValue(ref object _current, string _propertyName,
            string _propertyValue)
        {
            try
            {
                Type type = _current.GetType();

                PropertyInfo property = type.GetProperty(_propertyName);

                if (property != null)
                {
                    property.SetValue(_current, _propertyValue, BindingFlags.Public, null, null, null);
                }
                else
                {
                    throw new Exception("Property does not exist - " + _propertyName);
                }
            }
            catch (Exception e)
            {
                TraceFileHelper.Error("Exception in getPropertyValue : PropertyName-" + _propertyName);
                throw e;
            }
        }

        public static void SetPropertyValue(ref object _current, string _propertyName,
            object _propertyValue)
        {
            try
            {
                Type type = _current.GetType();

                PropertyInfo property = type.GetProperty(_propertyName);

                if (property != null)
                {
                    property.SetValue(_current, _propertyValue, BindingFlags.Public, null, null, null);
                }
                else
                {
                    throw new Exception("Property does not exist - " + _propertyName);
                }
            }
            catch (Exception e)
            {
                TraceFileHelper.Error("Exception in getPropertyValue : PropertyName-" + _propertyName);
                throw e;
            }
        }


        /// <summary>
        /// Return value of given object property, using defaultvalue if not found
        /// </summary>
        /// <param name="_current">Object to search</param>
        /// <param name="_propertyName">Property to search</param>
        /// <param name="_defaultValue">Default value if property not found</param>
        /// <returns>String property value</returns>
        public static string GetPropertyValue(object _current, string _propertyName,
            string _defaultValue)
        {
            try
            {
                Type type = _current.GetType();

                PropertyInfo property = type.GetProperty(_propertyName);

                if (property != null)
                {
                    string propertyValue = (string) property.GetValue(_current, BindingFlags.Public, null, null, null);

                    if (propertyValue == null)
                    {
                        return _defaultValue;
                    }

                    return propertyValue;
                }

                return _defaultValue;
            }
            catch (Exception e)
            {
                TraceFileHelper.Error("Exception in getPropertyValue : PropertyName-" + _propertyName);
                throw e;
            }
        }

        /// <summary>
        /// Return value of given object property
        /// </summary>
        /// <param name="_current">Object to search</param>
        /// <param name="_propertyName">Property to search</param>
        /// <returns>String property value</returns>
        public static string GetPropertyValue(object _current, string _propertyName)
        {
            try
            {
                Type type = _current.GetType();

                PropertyInfo property = type.GetProperty(_propertyName);

                if (property != null)
                {
                    return Convert.ToString(property.GetValue(_current, BindingFlags.Public, null, null, null));
                }
                else
                {
                    throw new Exception("Property does not exist - " + _propertyName);
                }
            }
            catch (Exception e)
            {
                TraceFileHelper.Error("Exception in getPropertyValue : PropertyName-" + _propertyName);
                throw e;
            }
        }

        /// <summary>
        /// Call given method and return its returned value
        /// </summary>
        /// <param name="_current">Object to call into</param>
        /// <param name="_methodName">Method to call</param>
        /// <param name="_parameters">Parms to pass into method</param>
        /// <returns>Returned object from called Method</returns>
        public static object GetMethodValue(object _current, string _methodName,
            object[] _parameters)
        {
            try
            {
                object obj = null;

                if (_parameters == null)
                {
                    Type type = _current.GetType();
                    MethodInfo method = type.GetMethod(_methodName);
                    obj = method.Invoke(_current, _parameters);
                }
                else
                {
                    Type type = _current.GetType();
                    Type[] types = GetTypes(_parameters);
                    MethodInfo method = type.GetMethod(_methodName, types);
                    obj = method.Invoke(_current, _parameters);
                }
                return obj;
            }
            catch (Exception ex)
            {
                TraceFileHelper.Error("Exception : MethodName-" + _methodName);
                throw ex;
            }
        }

        private static Type[] GetTypes(object[] _parameters)
        {
            Type[] types = new Type[_parameters.Length];
            int i = 0;

            foreach (object o in _parameters)
            {
                types[i] = o.GetType();
                i++;
            }

            return types;
        }

        public static bool IsRelated(Type _itemType, Type _baseType)
        {
            bool returnValue = false;

            if (_itemType == _baseType)
            {
                returnValue = true;
            }
            else if (_itemType.IsSubclassOf(_baseType))
            {
                returnValue = true;
            }
            else if (_baseType.IsInterface && ReflectionHelper.ImplementsInterface(_itemType, _baseType))
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Return assembly version for currently running assembly
        /// </summary>
        /// <returns>Version string</returns>
        public static string GetVersion()
        {
            string Version = Assembly.GetEntryAssembly().GetName().Version.ToString();

            string[] parts = Version.Split('.');

            return string.Format("v{0}.{1}.{2} Build {3}",
                parts[0], parts[1], parts[2], parts[3]);
        }

        /// <summary>
        /// Test if given object type implements given Interface
        /// </summary>
        /// <param name="_objectType">Type to test</param>
        /// <param name="_interfaceType">Interface to check</param>
        /// <returns>True if implemented</returns>
        public static bool ImplementsInterface(Type _objectType, Type _interfaceType)
        {
            if (_interfaceType.IsInterface)
            {
                Type[] types = _objectType.GetInterfaces();

                foreach (Type type in types)
                {
                    if (type.Name == _interfaceType.Name)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public static string GetTypeName(Type _type)
        {
            string typeName = _type.Namespace + "." + _type.Name;

            return typeName;
        }

        public static double GetVersionDouble()
        {
            AssemblyName an = Assembly.GetEntryAssembly().GetName();
            Version v = an.Version;

            string temp = string.Format("{0}.{1}",
                v.Major,
                v.Minor);

            return Convert.ToDouble(temp);
        }
    }
}