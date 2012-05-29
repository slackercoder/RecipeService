using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace RecipeTests
{
    /// <summary>
    /// This class provides the means of generating a self-hosting instance of a WCF service.
    /// It's primary use is to facilitate writing integration tests using MSTest for WCF services.
    /// </summary>
    /// <typeparam name="TContract">This should be the contract defined for the service.</typeparam>
    /// <typeparam name="TService">This is the specific service that implements the interface associated with
    /// the type parameter TContract</typeparam>
    public class ServiceManager<TContract,TService> where TService : TContract  
    {
        #region Private fields and public properties

        ServiceHost hostInstanceField = null;
        Binding binding = null;
        string address = null;

        /// <summary>
        /// Exposes an instance of the ServiceHost.
        /// </summary>
        public ServiceHost HostInstance
        {
            get
            {
                return hostInstanceField;
            }

            private set
            {
                hostInstanceField = value;
            }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ServiceManager class.
        /// This constructor takes only the base address for the service and uses this to 
        /// create the appropiate binding.
        /// </summary>
        /// <param name="baseAddress">The base address to used for the service.</param>
        public ServiceManager(string baseAddress)
        {
            this.address = baseAddress;
            this.binding = GetBindingForAddress(address);
        }

        /// <summary>
        /// Initializes a new instance of the ServiceManager class.
        /// This constructor takes both a Binding and base address as parameters.
        /// This allows the developer to specify a specific if it is required.
        /// </summary>
        /// <param name="binding">An instance of a class that has inherited from the Binding object.</param>
        /// <param name="baseAddress">The base address to used for the service.</param>
        public ServiceManager(Binding binding, string baseAddress)
        {
            this.binding = binding;
            this.address = baseAddress;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// This method creates a new instance of Service Host for the defined service,
        /// adds a service endpoint for the defined contract, binding and address and 
        /// then opens it.
        /// </summary>
        public void StartService()
        {
            HostInstance = new ServiceHost(typeof(TService));
            HostInstance.AddServiceEndpoint(typeof(TContract), binding, address);
            HostInstance.Open();
        }

        /// <summary>
        /// This method stops the current service host instance.
        /// </summary>
        public void StopService()
        {
            if (HostInstance.State != CommunicationState.Closed)
                HostInstance.Close();
        }

        /// <summary>
        /// This method creates a new instance of ChannelFactory for the defined contract and using 
        /// the current binding and base address.
        /// </summary>
        /// <returns>Returns and instance of the generic ChannelFactory</returns>
        public ChannelFactory<TContract> GetChannelFactory()
        {
            return new ChannelFactory<TContract>(binding, address);
        }
        #endregion

        #region Private methods


        /// <summary>
        /// This method creates a new Binding object determined by the scheme in the defined in the address.
        /// </summary>
        /// <param name="address">The address of the service.</param>
        /// <returns>A Binding object specific to the scheme of the addres. E.g. if the scheme is "http" then the 
        /// binding object returned is of the type WSHttpBinding.</returns>
        private Binding GetBindingForAddress(string address)
        {
            Binding newBinding = null;

            Uri uri = new Uri(address);

            switch (uri.Scheme.ToLower())
            {
                case "net.tcp":
                    newBinding = new NetTcpBinding();
                    break;
                case "net.pipe":
                    newBinding = new NetNamedPipeBinding();
                    break;
                case "http":
                    newBinding = new WSHttpBinding();
                    break;
                case "https":
                    newBinding = new WSHttpBinding();
                    break;
            }

            return newBinding;
        }
        #endregion

     }
}