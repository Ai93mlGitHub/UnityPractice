using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Develop.DI
{
    public class DIContainer
    {

        private readonly Dictionary<Type, Registration> _container = new();

        private readonly DIContainer _parent;

        private readonly List<Type> _requests = new();

        public DIContainer() : this(null)
        {

        }
        
        public DIContainer(DIContainer parent) => _parent = parent;
            
        public Registration RegisterAsSingle<T>(Func<DIContainer, T> creator)
        {
            if (IsAlreadyRegister<T>())
                throw new InvalidOperationException($"{typeof(T)} is already register");

            Registration registration = new Registration(container => creator(container));
            _container[typeof(T)] = registration;
            return registration;
        }

        public bool IsAlreadyRegister<T>()
        {
            if (_container.ContainsKey(typeof(T)))
                return true;

            if (_parent != null)
                return _parent.IsAlreadyRegister<T>();

            return false;
        }

        public T Resolve<T>()
        {
            if (_requests.Contains(typeof(T)))
                throw new InvalidOperationException($"Cycle resolve for {typeof(T)}");

            _requests.Add(typeof(T));

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))
                    return CreateFrom<T>(registration);

                if (_parent != null)
                    return _parent.Resolve<T>();
            }
            finally
            {
                _requests.Remove(typeof(T));
            }

            throw new InvalidOperationException($"Registration for {typeof(T)} not exist");
        }

        private T CreateFrom<T>(Registration registration)
        {
            if (registration.Instance == null && registration.Creator != null)
                registration.Instance = registration.Creator(this);

            return (T)registration.Instance;
        }

        public class Registration
        {
            public Func<DIContainer, object> Creator {get;}
                        
            public object Instance { get; set; }

            public Registration(object instance) => Instance = instance;

            public Registration(Func<DIContainer, object> creator) => Creator = creator;
        }
    }
}