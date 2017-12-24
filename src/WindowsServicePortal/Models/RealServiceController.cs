using System;
using System.ServiceProcess;

namespace WindowsServicePortal.Models
{
    public class RealServiceController : IServiceController
    {
        private ServiceController _serviceController;
        public RealServiceController(string machineName, string serviceName)
        {
            _serviceController = new ServiceController(machineName, serviceName);
        }
        public bool CanStop()
            => _serviceController.CanStop;

        public ServiceControllerStatus GetStatus()
            => _serviceController.Status;

        public void Start()
            =>_serviceController.Start();


        public void Stop()
            =>_serviceController.Stop();

        public void WaitForStatus(ServiceControllerStatus status)
            =>_serviceController.WaitForStatus(status);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _serviceController.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RealServiceController() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

}