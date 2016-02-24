using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bcc.lib
{
    public enum VariableType
    {
        @Int32,
        @Char
    }

    public class TypeDescriptor
    {
        public VariableType PrimitiveType { get; private set; }
        public TypeDescriptor(VariableType type)
        {
            this.PrimitiveType = type;
        }
        public TypeDescriptor(TypeDescriptor tdesc)
        {
            this.PrimitiveType = tdesc.PrimitiveType;
        }
        public TypeDescriptor(string type) : this(ToVarType(type))
        {
        }

        public static implicit operator TypeDescriptor(VariableType type)
        {
            return new TypeDescriptor(type);
        }

        public static implicit operator VariableType(TypeDescriptor td)
        {
            return td.PrimitiveType;
        }

        public override bool Equals(object obj)
        {
            if (obj is VariableType)
            {
                var v = (VariableType)obj;
                return v == this.PrimitiveType;
            }
            else
                if (obj is TypeDescriptor && !(obj is ArrayTypeDescriptor))
            {
                var v = (TypeDescriptor)obj;
                return v.PrimitiveType == this.PrimitiveType;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.PrimitiveType.GetHashCode();
        }

        public virtual string IlType
        {
            get
            {
                return ToIlType(this.PrimitiveType);
            }
        }

        protected static string ToIlType(VariableType type)
        {
            if (type == VariableType.Char)
                return "char";
            if (type == VariableType.Int32)
                return "int32";
            throw new ArgumentException("Unknown type " + type);
        }

        protected static VariableType ToVarType(string type)
        {
            if (type == "char")
                return VariableType.Char;
            if (type == "int")
                return VariableType.Int32;
            throw new ArgumentException("Unknown type " + type);
        }

        public override string ToString()
        {
            return PrimitiveType.ToString();
        }

        public static bool operator ==(TypeDescriptor x, TypeDescriptor y)
        {
            return x?.Equals(y) ?? false;
        }

        public static bool operator !=(TypeDescriptor x, TypeDescriptor y)
        {
            return !x?.Equals(y) ?? false;
        }
    }

    public class ArrayTypeDescriptor : TypeDescriptor
    {

        public ArrayTypeDescriptor(VariableType type) : base(type)
        {
        }
        public ArrayTypeDescriptor(TypeDescriptor tdesc) : base(tdesc)
        {
        }
        public ArrayTypeDescriptor(ArrayTypeDescriptor arr) : this(arr.PrimitiveType)
        {
        }

        public override bool Equals(object obj)
        {
            if (obj is VariableType)
                return base.Equals(obj);
            else if (obj is ArrayTypeDescriptor)
            {
                var v = (ArrayTypeDescriptor)obj;
                return this.PrimitiveType == v.PrimitiveType;
            }
            else return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string IlType
        {
            get
            {
                return base.IlType + "[]";
            }
        }

        public string NewArrType
        {
            get
            {
                if (this.PrimitiveType == VariableType.Char)
                    return "[mscorlib]System.Char";
                if (this.PrimitiveType == VariableType.Int32)
                    return "[mscorlib]System.Int32";
                else throw new NotImplementedException();
            }
        }

        public string ArrayElemSuffix
        {
            get
            {
                if (this.PrimitiveType == VariableType.Int32)
                    return "i4";
                else if (this.PrimitiveType == VariableType.Char)
                    return "i2";
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            return base.ToString() + "[]";
        }
    }

    public class VariableInfo
    {
        public string Name { get; set; }
        public int IlNo { get; set; }
        public TypeDescriptor Type { get; set; }
    }
}
