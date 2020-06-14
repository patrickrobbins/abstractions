﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Resolution;

namespace Unity.Injection
{
    /// <summary>
    /// Base class for generic type parameters.
    /// </summary>
    public abstract class GenericParameterBase : ParameterValue,
                                                 IResolverFactory<Type>,
                                                 IResolverFactory<ParameterInfo>
    {
        #region Fields

        private readonly string? _name;
        private readonly bool _isArray;
        private readonly string _genericParameterName;

        #endregion


        #region Constructors

        /// <summary>
        /// Create a new <see cref="GenericParameter"/> instance that specifies
        /// that the given named generic parameter should be resolved.
        /// </summary>
        /// <param name="genericParameterName">The generic parameter name to resolve.</param>
        protected GenericParameterBase(string genericParameterName)
            : this(genericParameterName, null)
        { }

        /// <summary>
        /// Create a new <see cref="GenericParameter"/> instance that specifies
        /// that the given named generic parameter should be resolved.
        /// </summary>
        /// <param name="genericParameterName">The generic parameter name to resolve.</param>
        /// <param name="resolutionName">Registration name to use when looking up in the container.</param>
        protected GenericParameterBase(string genericParameterName, string? resolutionName)
        {
            if (null == genericParameterName) throw new ArgumentNullException(nameof(genericParameterName));

            if (genericParameterName.EndsWith("[]", StringComparison.Ordinal) ||
                genericParameterName.EndsWith("()", StringComparison.Ordinal))
            {
                _genericParameterName = genericParameterName.Replace("[]", string.Empty).Replace("()", string.Empty);
                _isArray = true;
            }
            else
            {
                _genericParameterName = genericParameterName;
                _isArray = false;
            }
            _name = resolutionName;
        }


        #endregion


        #region Public Properties

        /// <summary>
        /// Name for the type represented by this <see cref="ParameterValue"/>.
        /// This may be an actual type name or a generic argument name.
        /// </summary>
        public virtual string ParameterTypeName => _genericParameterName;

        #endregion


        #region  Overrides

        public override bool Equals(Type? type)
        {
            if (null == type) return false;
            if (!_isArray) return type.IsGenericParameter && type.Name == _genericParameterName;
            if (!type.IsArray) return false;

            var element = type.GetElementType()!;
            return element.IsGenericParameter && element.Name == _genericParameterName;
        }

        public override bool Equals(ParameterInfo? other)
        {
            if (null == other) return false;

#if NETCOREAPP1_0 || NETSTANDARD1_0
            if (!other.Member.DeclaringType!.GetTypeInfo().IsGenericType) return false;
#else
            if (!other.Member.DeclaringType!.IsGenericType) return false;
#endif
            var info = GenericParameterInfo(other);

            return Equals(info.ParameterType);
        }

        #endregion


        #region IResolverFactory

        public virtual ResolveDelegate<TContext> GetResolver<TContext>(Type type)
            where TContext : IResolveContext
        {
            return GetResolver<TContext>(type, _name);
        }

        public virtual ResolveDelegate<TContext> GetResolver<TContext>(ParameterInfo info)
            where TContext : IResolveContext
        {
            var type = info.ParameterType;
            return GetResolver<TContext>(type, _name);
        }

        #endregion


        #region Implementation

        protected virtual ResolveDelegate<TContext> GetResolver<TContext>(Type type, string? name)
            where TContext : IResolveContext
        {
            return (ref TContext context) => context.Resolve(type, name);
        }

        protected ParameterInfo GenericParameterInfo(ParameterInfo other)
        {
            var definition = other.Member.DeclaringType!.GetGenericTypeDefinition();

#if NETSTANDARD1_0
            var memberType = other.Member.Name == ".ctor" ? MemberTypes.Constructor : MemberTypes.Method;
#else
            var memberType = other.Member.MemberType;
#endif
            IEnumerable < MethodBase> members = memberType switch
            {
                MemberTypes.Constructor => definition.SupportedConstructors(),
                MemberTypes.Method      => definition.SupportedMethods(),
                _ => throw new InvalidOperationException()
            };

            var expected = ((MethodBase)other.Member).GetParameters()
                                                     .Select(p => p.Name)
                                                     .ToArray();
            foreach (MethodBase member in members)
            {
                var parameters = member.GetParameters();
                if ( expected.SequenceEqual(parameters.Select(p => p.Name)))
                    return parameters[other.Position];
            }

            throw new InvalidOperationException();
        }

        #endregion
    }
}