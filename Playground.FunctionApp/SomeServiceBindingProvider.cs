using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace Playground.FunctionApp
{
    public class SomeServiceBindingProvider : IBindingProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public SomeServiceBindingProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            ParameterInfo parameter = context.Parameter;
            if (!typeof(ISomeService).IsAssignableFrom(parameter.ParameterType))
            {
                return Task.FromResult<IBinding>(null);
            }

            IBinding binding = new SomeServiceBinding(parameter, _serviceProvider);
            return Task.FromResult(binding);
        }
    }

    public class SomeServiceBinding : IBinding
    {
        private readonly ParameterInfo _parameter;
        private readonly IServiceProvider _serviceProvider;

        public SomeServiceBinding(ParameterInfo parameter, IServiceProvider serviceProvider)
        {
            _parameter = parameter;
            _serviceProvider = serviceProvider;
        }

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            if (value == null || !_parameter.ParameterType.IsInstanceOfType(value))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                    "Unable to convert value to {0}.", _parameter.ParameterType));
            }

            IValueProvider valueProvider = new ValueBinder(value, _parameter.ParameterType);
            return Task.FromResult<IValueProvider>(valueProvider);

        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var service = _serviceProvider.GetService(_parameter.ParameterType);

            return BindAsync(service, context.ValueContext);

        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor
            {
                Name = _parameter.Name
            };
        }

        public bool FromAttribute => false;


        private sealed class ValueBinder : IValueBinder
        {
            private readonly object _tracer;

            public ValueBinder(object tracer, Type type)
            {
                _tracer = tracer;
                Type = type;
            }

            public Type Type { get; }

            public Task<object> GetValueAsync() => Task.FromResult(_tracer);

            public string ToInvokeString() => null;

            public Task SetValueAsync(object value, CancellationToken cancellationToken) => Task.CompletedTask;
        }
    }
}
