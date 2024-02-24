using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// TODO 정리 or 구현
public class Container
{

    private Dictionary<string, object> components = new();

    private void InitializeComponent()
    {
    }

    private ISet<Type> DoScan()
    {
        var currentDomain = AppDomain.CurrentDomain;
        Assembly assembly = currentDomain.Load("Assembly-CSharp");
        return assembly.GetTypes().Where(type => type.IsDefined(typeof(Component))).ToHashSet();
    }
    
    private T CreateInstance<T>(Type type)
    {
        // ConstructorInfo[] constructorInfos = type.GetConstructors();
        // foreach (var constructorInfo in constructorInfos)
        // {
        //     constructorInfo.Invoke(null);
        // }
        throw new NotImplementedException();
    }
}