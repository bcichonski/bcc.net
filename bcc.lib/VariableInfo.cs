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
                return base.IlType+"[]";
            }
        }
    }

    public class VariableInfo
    {
        public string Name { get; set; }
        public int IlNo { get; set; }
        public VariableType Type { get; set; }
    }
}
