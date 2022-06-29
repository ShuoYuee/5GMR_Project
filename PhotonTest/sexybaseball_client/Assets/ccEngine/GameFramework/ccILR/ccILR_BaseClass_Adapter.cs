using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ccILR;
using System.Collections.Generic;


public class ccILR_BaseClass_Adapter : CrossBindingAdaptor
{
    class CallClassFunctionDT
    {
        public IMethod m_IMethod = null;
        public bool m_bIsGetInvoking = false;
    }

    public override Type BaseCLRType
    {
        get
        {
            return typeof(ccILR_BaseClass);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(BaseClass_Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new BaseClass_Adaptor(appdomain, instance);
    }

    class BaseClass_Adaptor : ccILR_BaseClass, CrossBindingAdaptorType
    {
        private ILTypeInstance instance;
        private ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        private Dictionary<string, CallClassFunctionDT> _aCreateFuncton = new Dictionary<string, CallClassFunctionDT>();

        public BaseClass_Adaptor()
        {

        }

        public BaseClass_Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance
        {
            get
            {
                return instance;
            }
        }

        public override string ToString()
        {
            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
            m = instance.Type.GetVirtualMethod(m);
            if (m == null || m is ILMethod)
            {
                return instance.ToString();
            }
            else
                return instance.Type.FullName;
        }

        public new T f_CallFunction<T>(string strFunctonName, params object[] p)
        {
            IMethod tIMethod = instance.Type.GetMethod(strFunctonName);
            return (T)this.appdomain.Invoke(tIMethod, instance);
        }
    }
}